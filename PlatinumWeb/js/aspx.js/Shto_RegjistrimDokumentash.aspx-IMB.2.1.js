;
if (!window.console) {
    var noOp = function () { }; // no-op function
    console = {
        log: noOp,
        warn: noOp,
        error: noOp
    };
}
var sasiaEFillimit = 0;
var pageState = {
    webhook: {},    
    formatVleftaDB: 7,
    idMonedheNdermarrje: -1,
    kushte: {},
    gridaSelector: "#rowed5",
    gridaKomision: "#rowed6",
    kastratiTollona: {
        seriale: {
            TD05: ["BC01L0005"],
            TD010: ["BC01L0010"],
            TB05: ["BC03L0005"],
            TB010: ["BC03L0010"],
            TB020: ["BC03L0020"],
            TD020: ["BC01L0020"],
            TG10: ["BC04L0010"],
            TB110: ["BC05L0010"]
        },
        gjatesi: 19,
        isValid: function (grida) {
            if (!pageState.kushte.RSHTTK)
                return true;
            var ids = grida.getDataIDs();
            var isValid = true;
            $.each(ids, function (index, item) {
                if (!changeShenime(null, item)) {
                    isValid = false;
                    return;
                }
            });
            return isValid;
        },
        getSerialNumber: function (changedShenime, mySerials) {
            var serialNumber;
            if (typeof mySerials == "undefined")
                return serialNumber;
            $.each(mySerials, function (index, item) {
                if (changedShenime.indexOf(item) == 0) {
                    serialNumber = changedShenime.substring(item.length + 1);
                    return;
                }
            });
            return serialNumber;
        }
    },
    peshore: {
        aktiv: function (vleraKodi) { return this.skema && this.skema.length > 0 && vleraKodi.length === this.skema.length; },
        skema: "99kkkkkkppmmmm",
        checkPeshore: function (vleraKodi) {
            if (!this.aktiv(vleraKodi))
                return false;
            var startWith = this.skema.substring(0, this.skema.indexOf("k"));
            if (vleraKodi.indexOf(startWith) !== 0)
                return false;
            var kodArt = vleraKodi.substring(this.skema.indexOf("k"), this.skema.lastIndexOf("k") + 1);
            var sasiPlote = vleraKodi.substring(this.skema.indexOf("p"), this.skema.lastIndexOf("p") + 1);
            var sasiDhjetore = vleraKodi.substring(this.skema.indexOf("m"), this.skema.lastIndexOf("m") + 1);
            var sasia = parseFloat(sasiPlote + "." + sasiDhjetore);
      
            return { kodArt: kodArt, sasia: parseFloat(sasia.toFixed(3)) };//hedhim poshte shifren e katert mbas presjes nqs ka se i jep problem kases.
        }
    },
    EshteVisibleKont: false,
    kontrolliMB: "",

    EshteVisibletxtTotal2: false,
    EshteVisibletxtTotalMeZbritje2: false,
    EshteVisibletxtTVSH2: false,
    EshteVisibletxtTotaliPaTVSH2: false,
    EshteVisibletxtTotaliMeZbritjePaTVSH2: false,
    EshteVisiblelblMonedhaBaze: false,
    EshtedokBije: false,
    ShtoFushaVartesi: true,
    widthLupaQendraKosto: 1100
};
var kodkodbar = 1;
var formati = 2;
var mbushGrideKomision = false;
var arrayMeMagazina = new Array();
var colMagazina;
var listComboNjesiArt = new Array();
var magazina1;
var tvshArt = 0;
var pershk = 1;
var arrFormati = new Array();
var arrayMeLloje;
var arrayMeNjesi = new Array();
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var ndryshuarData = false;
var marresi = false;
var enablekursi;
var brutoNetoNivelbimi = 0;
var countHapLupeArtikujsh = 0;
var kaVlereDefaultDegaAdmin = false;
var editorkf = "";
var identikuesPerPopupKursi = "ShtoRegjistrimDokumentash";
var llojkursi = 1; //perdoret per te ruajtur llojin e kursit te zgjedhur tek konfigurimi. Ne qofte se nuk ka asnje lloj te zgjedhur, atehere merret lloji i pare.
var rreshtidyfish;
var editorData;
var previousMag = undefined;
var kaSeriale = false;
var memoryArt, memoryLlog;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'Shitja'
};

var urlMedianInputCheck = "";
var organizataMIC = "";
$.ajax({
    url: Utils.getServerApiUrl("Rregjistrime", "merrurlMedianInputCheck"),
    data: JSON.stringify({ idNdermarrje: pageState.idNdermarrje })
}).done(function (result) {
    urlMedianInputCheck = result[0];
    organizataMIC = result[1];
});

$(document).ready(function () {
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    memoryArt = new memory("IdArtikulli");
    memoryLlog = new memory("IdLlogari");
    pageState.idMonedheNdermarrje = hfState.Get("idMonedheNderm");
    pageState.idViti = hfState.Get("idViti");
    pageState.idNdermarrjeVit = hfState.Get("idNdermarrjeVit");
    pageState.idNdermarrje = hfState.Get("idNdermarrje");
    pageState.idPerdoruesi = hfState.Get("idPerdoruesi");
    pageState.llojDetajimNdermarrje = hfState.Get("llojDetajimNdermarrje");
    pageState.idGjuha = hfState.Get('idGjuha');
    pageState.veprimi = hfState.Get("veprimi");
    pageState.Kf = { alternativaKushtZbritje: false };
    pageState.Klient = "";
    pageState.TaksaKF = new Object();
    $(window).on("resize", function () {
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
        }
        var grida = $(pageState.gridaSelector);
        var gridaKomision = $(pageState.gridaKomision);//shtojme kontrolle nese eshte e shfaqur grida apo jo
        if ($('#divgride2Komision').width() != null) {
            myJQGrid.fixGridWidth(gridaKomision, $('#divgride2Komision'));
        }
        if ($('#divgride2').width() != null) {
            myJQGrid.fixGridWidth(grida, $('#divgride2'));
        }
    }).trigger("resize");

    $(document).on("keydown", function (e) {
        switch (e.which) {
            case 9:
                if (myMesazh.pyetjeEHapur)
                    e.preventDefault();
                break;
            case 13:
                e.preventDefault();
                break;
            case 120:
                e.preventDefault();
                if ($(e.target).prop('tagName') === 'INPUT' && $(e.target).prop('type') === 'text')
                    $(e.target).trigger('blur');
                if ($('#hfShtimModifikim').val() == 'modifikim' && $('#hfSkema').val() > 0 && lblStatusAprovimi.GetText() == 'Per Aprovim')
                {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNeProcesAprovimi"));
                    return;
                }
                if (($('#hfShtimModifikim').val() == 'shtim' && $('#hfSkema').val() > 0) || ($('#hfShtimModifikim').val() == 'modifikim' && $('#hfSkema').val() > 0 && lblStatusAprovimi.GetText() != 'Aprovuar')) {
                    var eventData = {
                        item: {
                            name: 'Aprovo',
                            index: 6
                        },
                        processOnServer: true
                    };

                }
                else {
                    var eventData = {
                        item: {
                            name: 'Ruaj',
                            index: 0
                        },
                        processOnServer: true
                    };
                }
                var sender = 'tastiera';
                menu_click(sender, eventData);
                break;
            case 116: //F5
                if (window.parent !== undefined)
                    window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
                break;
            default:
                break;
        }
    });
});

//$(window).on("load", function () {    
//    Utils.resizeSplitter();
//    Init();
//});

function DevExControlsInitialized(s, e) {
    if (e.isCallback)
        return;
    Utils.resizeSplitter();
    Init();    
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

function ShtoNeHfColKlienteFurnitoreVartes() {
    var selectedOptions = [];
    var items = btneKlientfurnitorVartes.GetSelectedItems();
    for (var i = 0; i < items.length; i++) {
        selectedOptions[i] = btneKlientfurnitorVartes.GetItem(items[i]);
    }
    hfState.Set("colKlienteFurnitoreVartes", JSON.stringify(selectedOptions));
}
var options;
function KrijoKlientFurnitorVartes() {
    if (!hfState.Get("colKlienteFurnitoreVartes"))
        return;

    options = JSON.parse(hfState.Get("colKlienteFurnitoreVartes"));
    pageState.KFVartesNgaNgarkimiDokumentit = true;
    var value = [];
    for (var i = 0; i < options.length; i++) {
        value[i] = options[i].IdKlientFurnitor;
    }
    hfState.Remove("colKlienteFurnitoreVartes");

    btneKlientfurnitorVartes = new MultiSelect({
        container: "tblFillim",
        valueField: "IdKlientFurnitor",
        labelField: "KodKlientFurnitor",
        searchField: ["KodKlientFurnitor", "EmertimiKF"],
        options: options,
        value: value,
        meLupe: true,
        onButtonClickLupa: ButtonClickKlientiVartes,
        multiSelectId: "btneKlientfurnitorVartes",
        init: false,
        load: function (query, callback) {
            if (!query.length)
                return callback();
            if (!btneKlientfurnitorVartes.Contains(query))
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "KtheKlientFurnitorSipasKoditLike"),
                    data: JSON.stringify({
                        kodiKlientFurnitor: query, kodModeli: cmbModeli.GetText(), idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi,
                        idGjuha: pageState.idGjuha, idKlientFurnitorKryesor: btnKlienti.GetValue() == null ? 0 : btnKlienti.GetValue(), idKonfigLupaKf: $("#hfLupaKlientFurnitorvartes").val()
                    }),
                    error: function () {
                        callback();
                    },
                    success: function (res) {
                        pageState.KFVartesNgaNgarkimiDokumentit = false;
                        callback(res.slice(0, 10));
                    }
                });
        },
        render: {
            option: function (item, escape) {
                return '<div class="option">' +
                    '<span class="kodiSelectize">' + escape(item.KodKlientFurnitor) + '</span>' +
                    '<span class="pershkrimiSelectize">' + escape(item.EmertimiKF) + '</span>' +
                    '</div>';

            },
            item: function (item, escape) {
                return '<div class="option">' +
                    '<span class="kodiSelectize">' + escape(item.KodKlientFurnitor) + '</span>' +
                    '<span class="pershkrimiSelectize">' + escape(item.EmertimiKF) + '</span>' +
                    '</div>';
            }
        },
        onItemAdd: function (data) { KlientVartesSelected(data); }
    });
}

function KlientVartesSelected(data) {
    //if (!options.filter(function (el) { return el.IdKlientFurnitor == data })[0]) {
    if (!pageState.KFVartesNgaNgarkimiDokumentit) {
        var klient = btneKlientfurnitorVartes.GetItem(data);
        SucceededCallbackOKFDefaultVartes({ kf: klient, oColAdresatKF: klient.OColAdresat, mosPlotesoTeDhena: true });
    }
}

/*
Function:
ekzekutohet sa here i behet resize faqes, dhe ben resize te grides
*/
function Init() {//po

    if (!(typeof (isPostBack) == "undefined"))
        return;
    KrijoKlientFurnitorVartes();

    keyGlobal = -1;
    kursifundit = txtKursi.GetValue();
    identikuesPerPopupKlientFurnitori = "RegjistrimDokumentash";
    identikuesPerPopupMenyraTransporti = "RegjistrimDokumentash";
    identikuesPerPopupKushteDergimi = "RegjistrimDokumentash";
    identikuesPerPopupAgjenteShitje = "RegjistrimDokumentash";
    identikuesPerPopupKushtePagese = "RegjistrimDokumentash";
    identikuesPerPopupAfateMaturimi = "RegjistrimDokumentash";
    identikuesPerPopupLlogari = "RegjistrimDokumentash";
    //identikuesPerPopupArtikulli = "RegjistrimDokumentash";
    identifikuesPerPopupDetajime = "RegjistrimDokumentash";
    identifikuesPerPopupMakro = "RegjistrimDokumentash";
    identifikuesPerPopupMagazina = "RegjistrimDokumentash";
    editorData = data_DateEdit;
    // vendosja e tesktit te menuve koke, trup dhe fund
    document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
    document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
    document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
    document.getElementById("trupKonfigurimiKomision").innerHTML = hfState.Get("MenuTrupDokumentiKomision");
    pageState.identifikuesPopUp = "RegjistrimDokumentash";
    pageState.vendosKodNgaLupa = function (params) {
        var index = $(pageState.gridaSelector).getLastSel2();//window.parent.lastsel2;
        var idKontrolli = "#txtKodi" + index;
        selectFunc(null, null, idKontrolli, params.idKodi, params.kodi);
        $(idKontrolli).focus();
    };

    if (hfState.Contains("KaSeriale")) {
        kaSeriale = hfState.Get("KaSeriale");
        hfState.Remove("KaSeriale");
    }
    pageState.vendosMagazineNgaLupa = function (params) {
        var index = $(pageState.gridaSelector).getLastSel2();//window.parent.lastsel2;
        var idKontrolli = "#txtMagazina" + index;
        selectFuncMag(null, null, idKontrolli, params.idKodi, params.kodi);
        $(idKontrolli).focus();
    };
    pageState.listeIds = null;
    pageState.lloji = $('#hfShtimModifikim').val();
    pageState.aprovim = false;
    pageState.peshore.skema = hfState.Get("skemeBarkodiPeshore");
    pageState.periudha = $.parseJSON(hfState.Get("periudha"));
    pageState.guidString = hfState.Get("guidString");
    pageState.EshtedokBije = hfState.Get("dokumentBije") == "True";
    if (pageState.lloji == 'shtim' || pageState.lloji == 'shtimraport' || pageState.lloji == 'kthim' || pageState.lloji == 'kthimVod' || pageState.lloji == 'bli')
        setPeriudhe();
    changeName();
    //vendosTotaleMonedheFature(); //duhet pare kjo nese duhet
    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    if ((pageState.lloji == 'modifikim' || pageState.lloji == 'konvertim' || pageState.lloji == 'konvertimblerje') && txtNumerSerial.GetText() !== "" && !kupon)
        serialShenuarNgaPerdoruesi = true;
    if (pageState.lloji == 'modifikim' && pageState.listeIds != null)
        $("#toolbar").show();
    //ndryshoKonfigurimin(false);       
    SucceededCallbackKonfig(JSON.parse(hfState.Get("konfigFillestar")));
    hfState.Set("konfigFillestar", "");
    ngarkoKategoriSerialesh();
    merrKonfigurimeWebhook(pageState.idNdermarrje)
}

function setPeriudhe() {
    var dateDefault = Utils.ktheDateDefault(pageState.periudha);
    data_DateEdit.SetDate(dateDefault);
    dteAfatiKohor.SetDate(dateDefault);
    cmbMuajRaportimi.SetValue(dateDefault.getMonth() + 1);
    cmbVitRaportimi.SetText(dateDefault.getFullYear());
    var dataSot = Utils.zeroOren(new Date());
    dateRegjistrimi_DateEdit.SetDate(dataSot);
    if (dateMaturimi_DateEdit.GetVisible())
        dateMaturimi_DateEdit.SetDate(dataSot);
    else dateMaturimi_DateEdit.SetDate(data_DateEdit.GetDate());
    DtFillimi_DateEdit.SetDate(dateDefault);
    DtMbarimi_DateEdit.SetDate(dateDefault);
    DtKerkese_DateEdit.SetDate(dateDefault);

}
lejomod = true;
/*
Function: inicializoGride

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
*/

function inicializoGride(isLidhur, emerGride) {
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    mbushArrayMagazinat();
    var classes = '';
    if (isLidhur === true || (hfTeDrejta.Get("KonvertimSipasUSH") == true && hfState.Get("konvNgaUshNeFsh") == true))
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2],
    arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6],
    arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10],
    arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13], arrayPershkrimiKolonaGrides[14],
    arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17], arrayPershkrimiKolonaGrides[18],
    arrayPershkrimiKolonaGrides[19], arrayPershkrimiKolonaGrides[20], arrayPershkrimiKolonaGrides[21], arrayPershkrimiKolonaGrides[22],
    arrayPershkrimiKolonaGrides[23], arrayPershkrimiKolonaGrides[24], arrayPershkrimiKolonaGrides[25], arrayPershkrimiKolonaGrides[26],
    arrayPershkrimiKolonaGrides[27], arrayPershkrimiKolonaGrides[28], arrayPershkrimiKolonaGrides[29], arrayPershkrimiKolonaGrides[30],
    arrayPershkrimiKolonaGrides[31], arrayPershkrimiKolonaGrides[32], arrayPershkrimiKolonaGrides[33], arrayPershkrimiKolonaGrides[34],
    arrayPershkrimiKolonaGrides[35], arrayPershkrimiKolonaGrides[36],
        arrayPershkrimiKolonaGrides[37], arrayPershkrimiKolonaGrides[38], arrayPershkrimiKolonaGrides[39], arrayPershkrimiKolonaGrides[40], arrayPershkrimiKolonaGrides[41], arrayPershkrimiKolonaGrides[42], arrayPershkrimiKolonaGrides[43]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodbari, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextArea }, formatter: myJQGrid.textAreaFormat, unformat: myJQGrid.textAreaUnFormat },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDetajimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDetajimi2, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesiaSup, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjeresia, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjatesia, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiPermasa, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasia, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemCmimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemZbritja, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemVlefta, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboTVSHSup, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[18], index: arrayIdKolonaGrides[18], width: arrayWidthKolonaGrides[18], hidden: arrayVisibleKolonaGrides[18], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemVleftaTVSH, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[19], index: arrayIdKolonaGrides[19], width: arrayWidthKolonaGrides[19], hidden: arrayVisibleKolonaGrides[19], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemShenime, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[20], index: arrayIdKolonaGrides[20], width: arrayWidthKolonaGrides[20], hidden: arrayVisibleKolonaGrides[20], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemDtFillimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[21], index: arrayIdKolonaGrides[21], width: arrayWidthKolonaGrides[21], hidden: arrayVisibleKolonaGrides[21], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemDtMbarimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[22], index: arrayIdKolonaGrides[22], width: arrayWidthKolonaGrides[22], hidden: arrayVisibleKolonaGrides[22], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[23], index: arrayIdKolonaGrides[23], width: arrayWidthKolonaGrides[23], hidden: arrayVisibleKolonaGrides[23], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiKonvertimi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[24], index: arrayIdKolonaGrides[24], width: arrayWidthKolonaGrides[24], hidden: arrayVisibleKolonaGrides[24], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiaRez, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[25], index: arrayIdKolonaGrides[25], width: arrayWidthKolonaGrides[25], hidden: arrayVisibleKolonaGrides[25], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemRezervuar, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[26], index: arrayIdKolonaGrides[26], width: arrayWidthKolonaGrides[26], hidden: arrayVisibleKolonaGrides[26], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiRezervimi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[27], index: arrayIdKolonaGrides[27], width: arrayWidthKolonaGrides[27], hidden: arrayVisibleKolonaGrides[27], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiaMbetur, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[28], index: arrayIdKolonaGrides[28], width: arrayWidthKolonaGrides[28], hidden: arrayVisibleKolonaGrides[28], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiTrasferimi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[29], index: arrayIdKolonaGrides[29], width: arrayWidthKolonaGrides[29], hidden: arrayVisibleKolonaGrides[29], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSeriali, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[30], index: arrayIdKolonaGrides[30], width: arrayWidthKolonaGrides[30], hidden: arrayVisibleKolonaGrides[30], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemLlogShpenz, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[31], index: arrayIdKolonaGrides[31], width: arrayWidthKolonaGrides[31], hidden: arrayVisibleKolonaGrides[31], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiKthim, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[32], index: arrayIdKolonaGrides[32], width: arrayWidthKolonaGrides[32], hidden: arrayVisibleKolonaGrides[32], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiKonvertimBlerje, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[33], index: arrayIdKolonaGrides[33], width: arrayWidthKolonaGrides[33], hidden: arrayVisibleKolonaGrides[33], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemMagPershkrim, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[34], index: arrayIdKolonaGrides[34], width: arrayWidthKolonaGrides[34], hidden: arrayVisibleKolonaGrides[34], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiLitra, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[35], index: arrayIdKolonaGrides[35], width: arrayWidthKolonaGrides[35], hidden: arrayVisibleKolonaGrides[35], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemShenime2, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[36], index: arrayIdKolonaGrides[36], width: arrayWidthKolonaGrides[36], hidden: arrayVisibleKolonaGrides[36], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemZbritjaVlere, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[37], index: arrayIdKolonaGrides[37], width: arrayWidthKolonaGrides[37], hidden: arrayVisibleKolonaGrides[37], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboZbritja, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[38], index: arrayIdKolonaGrides[38], width: arrayWidthKolonaGrides[38], hidden: arrayVisibleKolonaGrides[38], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKategoriShpenzimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[39], index: arrayIdKolonaGrides[39], width: arrayWidthKolonaGrides[39], hidden: arrayVisibleKolonaGrides[39], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimiKategoria, custom_value: myJQGrid.myValueTextArea }, formatter: myJQGrid.textAreaFormat, unformat: myJQGrid.textAreaUnFormat },
        { name: arrayIdKolonaGrides[40], index: arrayIdKolonaGrides[40], width: arrayWidthKolonaGrides[40], hidden: arrayVisibleKolonaGrides[40], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemCmimiTvsh, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[41], index: arrayIdKolonaGrides[41], width: arrayWidthKolonaGrides[41], hidden: arrayVisibleKolonaGrides[41], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemLlogariKomisioni, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[42], index: arrayIdKolonaGrides[42], width: arrayWidthKolonaGrides[42], hidden: arrayVisibleKolonaGrides[42], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonSerialeUnike, custom_value: myElemButtonSerialeUnike }, hidedlg: true },
        { name: arrayIdKolonaGrides[43], index: arrayIdKolonaGrides[43], width: arrayWidthKolonaGrides[43], hidden: arrayVisibleKolonaGrides[43], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }

    ];
    var selektoriGrides = "#rowed5";
    var selektorGrideKomision = "#rowed6";

    var gridParams = {
        emergride: emerGride,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtKodi",
        emerEditorLloji: "cmbLloji",
        emerEditorCmimi: "txtCmimi",
        emerEditorCmimi: "txtCmimiTvsh",
        emerEditorDetajimi1: "txtDetajimi",
        cmimzero: cmimzero,
        widthi: $('#divgride2').width(),
        fokus: pageState.kushte.F,
        mosshtorresht: !lejomod,
        emerEditorMagazina: "txtMagazina",
        emerEditorPershkrimMagazina: "txtPershkrimmag",
        butonMagazina: btnMagazina,
        resetRreshtKorent: resetRreshtKorent,
        enable: enable,
        magazinaPare: colMagazina[0],
        kontrolloCmiminArtSipasKushtit: kontrolloCmiminArtSipasKushtit,
        btnKlienti: btnKlienti,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        idGjuha: hfState.Get("idGjuha"),
        callWebServiceInfoRow: callWebServiceInfoRow,
        autocompleteList: [
            { emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
            { emerEditor: "txtIdLlogShpenzimi", selectFunc: selectFuncLlogShpenzimi, changeFunc: changeFuncLlogShpenzimi, shtoDataKod: false },
            { emerEditor: "txtCmimi", selectFunc: selectFunc6, changeFunc: changeFunc6, shtoDataKod: false },
            { emerEditor: "txtDetajimi", selectFunc: selectFunc2, changeFunc: changeFunc2, shtoDataKod: true },
            { emerEditor: "txtDetajimi2t", selectFunc: selectFunc3, changeFunc: changeFunc3, shtoDataKod: true },
            { emerEditor: "txtKategoriShpenzimi", selectFunc: selectFunc4, changeFunc: changeFunc4, shtoDataKod: false },
            { emerEditor: "txtCmimiTvsh", selectFunc: selectFunc6, changeFunc: changeFunc6, shtoDataKod: false },
            { emerEditor: "txtMagazina", selectFunc: selectFuncMag, changeFunc: changeFuncMag, shtoDataKod: false }
        ],
        konfigToolbar: {
            identifikuesNrRreshta: varKonfig.identifikuesPerLocalStorageKey,
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            shtoArtikull: $('#hfTeDrejtaArtRi').val(),
            modArtikull: $('#hfTeDrejtaArtMod').val(),
            infoArtikullPerberes: $('#hfTeDrejtaArtPerberes').val(),
            infoGrupimPerberes: $('#hfTeDrejtaGrupimPerberes').val(),
            kaTeDrejtaArkive: $('#hfTeDrejtaArtImazhe').val(),
            ruajKolonatEGrides: ruajKolonatEGrides,
            hapPopUpModifikoArt: hapPopUpModifikoArt,
            hapPopUpRi: hapPopUpRi,
            hapPopUpInfoArtPerberes: hapPopUpInfoArtPerberes,
            hapPopUpinfoGrupimPerberes: hapPopUpinfoGrupimPerberes,
            hapPopUpImazheArkive: onHapPopUpImazheArkive,
            hapPopUpArkive: onHapLupeArkive,
            exportExcel: true,        //Ben enable exportin e F
            EmerExporti: "Shitje/Blerje", //Emri i filet .xls qe gjenerohet
            hapNgarkimSerialesh: hapNgarkimSerialesh,
            importoSeriale: $("#hfTeDrejtaImportoSeriale").val()
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
    grida.setVlereDefault('txtCmimi', 0);
    grida.setVlereDefault('txtCmimiTvsh', 0);
    grida.setVlereDefault('txtVleftaTVSH', 0);
    grida.setVlereDefault('txtVlefta', 0);
    grida.setVlereDefault('txtZbritja', 0);
    grida.setVlereDefault('txtZbritjaVlere', 0);
    grida.setVlereDefault('txtSasiPermase', 1);
    grida.setVlereDefault('txtSasia', 1);
    grida.setVlereDefault('txtGjeresi', 1);
    grida.setVlereDefault('txtGjatesi', 1);
    grida.setVlereDefault('txtSasiaRez', 1);
    grida.setVlereDefault('txtSasiMbetur', 0);
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
    grida.setShifraPasPresjes('txtCmimiTvsh', formatNumri.ShifraPasPresjesCmimi);
    grida.setShifraPasPresjes('txtVleftaTVSH', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtZbritja', formatNumri.ShifraPasPresjesZbritja);
    grida.setShifraPasPresjes('txtZbritjaVlere', formatNumri.ShifraPasPresjesZbritja);
    grida.setShifraPasPresjes('txtSasiPermase', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasia', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtGjeresi', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtGjatesi', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaRez', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiMbetur', formatNumri.ShifraPasPresjesSasia);
    Utils.setFormatNumri(txtKursi, formatkursi);
    Utils.setFormatNumri(txtKursiPagese, formatkursi);
    Utils.setFormatNumri(txtVleftePagese, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtPaguar, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtResto, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtCash, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtPerqindje, formatNumri.ShifraPasPresjesZbritja);
    Utils.setFormatNumri(txtVlefte, formatNumri.ShifraPasPresjesZbritja);
    Utils.setFormatNumri(txtTotalMeZbritje1, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotaliPaTVSH1, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotaliMeZbritjePaTVSH1, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTVSH1, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotal1, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotalMeZbritje2, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotaliPaTVSH2, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotaliMeZbritjePaTVSH2, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotalLitra, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTVSH2, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotal2, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtPerqindjeAgjent3, formatNumri.ShifraPasPresjesZbritja);
    Utils.setFormatNumri(txtPerqindjeAgjent2, formatNumri.ShifraPasPresjesZbritja);
    Utils.setFormatNumri(txtPerqindjeAgjent, formatNumri.ShifraPasPresjesZbritja);
}

/*
Formaton vlerat e fushave sipas formatit perkates ne te gjithe rreshtat e grides
*/
function vendosKonfigFormatNumri() {
    var grida = $(pageState.gridaSelector);
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtSasia', idRreshti);
        grida.formatoQelize('txtGjeresi', idRreshti);
        grida.formatoQelize('txtGjatesi', idRreshti);
        grida.formatoQelize('txtSasiPermase', idRreshti);
        grida.formatoQelize('txtSasiaRez', idRreshti);
        grida.formatoQelize('txtSasiMbetur', idRreshti);
        grida.formatoQelize('txtCmimi', idRreshti);
        grida.formatoQelize('txtCmimiTvsh', idRreshti);
        grida.formatoQelize('txtZbritja', idRreshti);
        grida.formatoQelize('txtZbritjaVlere', idRreshti);
        grida.formatoQelize('txtVleftaTVSH', idRreshti);
        grida.formatoQelize('txtVlefta', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtPerqindjeAgjent3);
    Utils.formatoTextBox(txtPerqindjeAgjent2);
    Utils.formatoTextBox(txtPerqindjeAgjent);
    Utils.formatoTextBox(txtPerqindje);
    Utils.formatoTextBox(txtVlefte);
    Utils.formatoTextBox(txtTotalMeZbritje1);
    Utils.formatoTextBox(txtTotaliPaTVSH1);
    Utils.formatoTextBox(txtTotaliMeZbritjePaTVSH1);
    Utils.formatoTextBox(txtTVSH1);
    Utils.formatoTextBox(txtTotal1);
    Utils.formatoTextBox(txtTotalMeZbritje2);
    Utils.formatoTextBox(txtTotaliPaTVSH2);
    Utils.formatoTextBox(txtTotaliMeZbritjePaTVSH2);
    Utils.formatoTextBox(txtTotalLitra);
    Utils.formatoTextBox(txtTVSH2);
    Utils.formatoTextBox(txtTotal2);
    Utils.formatoTextBox(txtKursi);
    Utils.formatoTextBox(txtKursiPagese);
    Utils.formatoTextBox(txtVleftePagese);
    Utils.formatoTextBox(txtPaguar);
    Utils.formatoTextBox(txtResto);
    Utils.formatoTextBox(txtCash);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtPerqindjeAgjent3);
    Utils.unFormatoTextBox(txtPerqindjeAgjent2);
    Utils.unFormatoTextBox(txtPerqindjeAgjent);
    Utils.unFormatoTextBox(txtPerqindje);
    Utils.unFormatoTextBox(txtVlefte);
    Utils.unFormatoTextBox(txtTotalMeZbritje1);
    Utils.unFormatoTextBox(txtTotaliPaTVSH1);
    Utils.unFormatoTextBox(txtTotaliMeZbritjePaTVSH1);
    Utils.unFormatoTextBox(txtTVSH1);
    Utils.unFormatoTextBox(txtTotal1);
    Utils.unFormatoTextBox(txtTotalMeZbritje2);
    Utils.unFormatoTextBox(txtTotaliPaTVSH2);
    Utils.unFormatoTextBox(txtTotaliMeZbritjePaTVSH2);
    Utils.unFormatoTextBox(txtTotalLitra);
    Utils.unFormatoTextBox(txtTVSH2);
    Utils.unFormatoTextBox(txtTotal2);
    Utils.unFormatoTextBox(txtKursi);
    Utils.unFormatoTextBox(txtKursiPagese);
    Utils.unFormatoTextBox(txtVleftePagese);
    Utils.unFormatoTextBox(txtPaguar);
    Utils.unFormatoTextBox(txtResto);
    Utils.unFormatoTextBox(txtCash);
}

function selectFunc(event, ui, emerfushe, idArt, kodArt) { //po per artikull ose llogarin
    var grida = $(pageState.gridaSelector);
    var magazine = "";
    var idRresht;
    var idKod = "txtKodi";
    if (emerfushe === undefined || emerfushe === null)
        idRresht = myJQGrid.getIndexFromEvent(event, idKod);
    else
        idRresht = emerfushe.split(idKod)[1];
    if (pageState.kushte.GJDM) {
        magazine = grida.getTekstQelize('txtMagazina', idRresht);
    }

    var kodArtShtim = (ui !== null && ui.item != null) ? ui.item.label : kodArt;
    var sasiaNeGride = myJQGrid.ktheObjektMeSasitePerArtikullinMeDetajimNeGride(kodArtShtim, grida, $('#hfShtimModifikim').val(), hfState.Get("idDok"), true, true, undefined, { detajimi: 'txtDetajimi', detajimi2: 'txtDetajimi2t', njesia: 'cmbNjesia', sasia: 'txtSasia', kodi: 'txtKodi', kodbari: 'txtKodbari', idKodi: 'txtIdKodi' });

    var lloj = pageState.veprimi;// pageState.veprimi;
    if (idArt && kodArt && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(kodArt);
        callWebserviceArtikulliIPlote({ idArt: idArt, idRresht: idRresht, magazine: magazine, kodKodbarArt: "", peshoreArt: "", listeIMEIArtikull: new Array(), sasiaNeGride: sasiaNeGride });
        return false;
    }
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
    if (ui !== null && ui.item != null) {
        //$(emerfushe).val(ui.item.label);
        grida.setTekstQelize(idKod, idRresht, ui.item.label);
        if (vleraLlojit == "Artikull") {
            callWebserviceArtikulliIPlote({ idArt: ui.item.value, idRresht: idRresht, magazine: magazine, kodKodbarArt: "", peshoreArt: "", listeIMEIArtikull: new Array(), sasiaNeGride: sasiaNeGride });
        }
        else if (vleraLlojit == "Llogari") { //Llogari
            var llogari = {};
            llogari[0] = { "idja": ui.item.value, "index": idRresht, "llojTvsh": tvshkont, "idPerdoruesi": pageState.idPerdoruesi, "TaksaKF": pageState.TaksaKF };
            callWebServiceLlogariPlote(llogari, false, 0);
        }
        return false;
    }
}

function ktheSasitePerArtikullinMeDetajimNeGride() {
    var objektSasiArtikulli = {
        shtimModifikim: $('#hfShtimModifikim').val(), idDok: hfState.Get("idDok"), dokShitje: true,
        detSasite: myJQGrid.ktheSasiPerArtikullinMeDetajim(kodArtShtim, dataset, fushat)
    };

    return objektSasiArtikulli;
}

function merrTVshSipasARtikullit(arridartikulli, arrlloji) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheTvshSipasArtikullit"), data: JSON.stringify({ idartikulli: arridartikulli, lloji: arrlloji })
    }).done(SucceededCallbackVendosTVSH);
}

function changeFunc(event, ui, emerKodi, index) {
    var grida = $(pageState.gridaSelector);
    var vleraKodit = grida.getTekstQelize(emerKodi, index);
    if (vleraKodit === false)//kur rreshti eshte fshire mos bej gje
        return;
    var magazine = "";
    if (pageState.kushte.GJDM) {
        magazine = grida.getTekstQelize('txtMagazina', index);
    }
    var vleraLlojit = grida.getTekstQelize("cmbLloji", index);
    var peshoreArt;
    var kerkoMeKodbar = grida.merrTeDhenaPerQelizen("txtKodi", index, "kerkoMeKodbar");
    if ((grida.merrTeDhenaPerQelizen("txtKodi", index, "kodiEkzistues") == vleraKodit && kerkoMeKodbar != undefined && kerkoMeKodbar === true) ||
        (grida.merrTeDhenaPerQelizen("txtKodi", index, "barkodiEkzistues") == vleraKodit && kerkoMeKodbar != undefined && kerkoMeKodbar === false)) {
        grida.setTekstQelize(emerKodi, index, grida.merrTeDhenaPerQelizen("txtKodi", index, "kodiEkzistues"));
        return;
    }
    if (vleraKodit != "") {
        switch (vleraLlojit) {
            case "Artikull":
                grida.vendosTeDhenaPerQelizen("txtKodi", index, "barkodiEkzistues", vleraKodit);
                if (pageState.veprimi != "blerje" && pageState.veprimi != "shitje" && pageState.veprimi != "shitjediscount" && pageState.veprimi != "bazaar") {
                    myMesazh.ShtoMesazhGabimi("GABIM: lloj dokumenti i panjohur!!! veprimi: " + pageState.veprimi);
                    return;
                }
                var listeIMEIArtikull = new Array();
                if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar')) {
                    peshoreArt = pageState.peshore.checkPeshore(vleraKodit);
                    if (peshoreArt) {
                        vleraKodit = peshoreArt.kodArt;

                    }
                    listeIMEIArtikull = merrImeiDheArtikull(index);
                }
                var rreshtRi = true;
                var kodArtShtim = grida.getTekstQelize('txtKodi', grida.getLastSel2());
                if (index != grida.getLastSel2()) {
                    rreshtRi = false;
                    kodArtShtim = vleraKodit;
                }
                var sasiaNeGride = myJQGrid.ktheObjektMeSasitePerArtikullinMeDetajimNeGride(kodArtShtim, grida, $('#hfShtimModifikim').val(), hfState.Get("idDok"), true, rreshtRi, index, { detajimi: 'txtDetajimi', detajimi2: 'txtDetajimi2t', njesia: 'cmbNjesia', sasia: 'txtSasia', kodi: 'txtKodi', kodbari: 'txtKodbari', idKodi: 'txtIdKodi' });
                callWebserviceArtikulliIPlote({ idArt: -1, idRresht: index, magazine: magazine, kodKodbarArt: vleraKodit, peshoreArt: peshoreArt, listeIMEIArtikull: listeIMEIArtikull, sasiaNeGride: sasiaNeGride });
                return;
            case "Llogari":
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogKodTvsh"),
                    data: JSON.stringify({ kodi: vleraKodit, index: index, llojTvsh: tvshkont, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, TaksaKF: pageState.TaksaKF })
                }).done(function (result) { SucceededCallbackLlogPlote(result, false, 0); });
                return;
            default:
                myMesazh.ShtoMesazhGabimi("GABIM: lloj rreshti i panjohur!!! vleraLlojit: " + vleraLlojit);
                break;
        }
    }
    if (vleraLlojit == "Artikull") {
        vendosArtPlote({ artikulli: null }, index);
        return;
    }
    if (vleraLlojit == "Llogari") {
        vendosLlogPlote({ idRreshti: index, llogaria: null }, false, 0);
        return;
    }
    return;
}

function merrImeiDheArtikull(idrreshti) {
    if (!pageState.kushte.AFI)
        return new Array();
    var grida = $('#rowed5');

    var ids = jQuery("#rowed5").getDataIDs();
    var listeImei = new Array();
    for (i = 0; i < ids.length; i++) {
        if (ids[i] == idrreshti)
            continue;
        if (grida.getTekstQelize('txtKodi', ids[i]) != "")
            listeImei.push([grida.getTekstQelize('txtKodi', ids[i]), grida.getTekstQelize('txtDetajimi', ids[i])]);

    }
    return listeImei;
}
function changeFuncLlogShpenzimi(event, ui, emerKodi, index) {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    //var index = -1;
    //var idKod = 'txtIdLlogShpenzimi';
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];
    //var lloj = pageState.veprimi;//pageState.veprimi;
    if (index == idRresht) {
        var emerfushe = '#' + emerKodi + index;
        if (ui == null || ui.item == null) {
            if ($(emerfushe).val() != "") {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogShpenzimiMeKod"),
                    data: JSON.stringify({ kodi: $(emerfushe).val(), index: index, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
                }).done(SucceededCallbackLlogShpenzimi);

                return;
            }
            vendosLlogShpenzimi({ idRreshti: index, llogaria: null });
            return;
        }
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogShpenzimiID"), data: JSON.stringify({ idja: ui.item.value, index: index, idPerdoruesi: pageState.idPerdoruesi })
        }).done(SucceededCallbackLlogShpenzimi);
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);
    if (ui == null || ui.item == null) {
        if (rreshti.txtIdLlogShpenzimi != "") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogShpenzimiMeKod"),
                data: JSON.stringify({ kodi: rreshti.txtIdLlogShpenzimi, index: index, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
            }).done(SucceededCallbackLlogShpenzimi);

            return;
        }
        vendosLlogShpenzimi({ idRreshti: index, llogaria: null });
        return;
    }
}
function changeFunc4(event, ui, emerKodi, index) {//po

    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var index = -1;
    //var idKod = 'txtKategoriShpenzimi';
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];
    //var lloj = pageState.veprimi;
    if (index == idRresht) {
        var emerfushe = '#' + emerKodi + index;

        if (ui == null || ui.item == null) {
            if ($(emerfushe).val() != "") {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraKatShpenzimiMeKod"),
                    data: JSON.stringify({ kodi: $(emerfushe).val(), index: index, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
                }).done(SucceededCallbackKatShpenzimi);

                return;
            }
            vendosKatShpenzimi({ idRreshti: index, Kodi: null });
            return;
        }
    }
}
var kaPyetjeHapurMagazina = false;
function changeFuncMag(event, ui, emerKodi, index) {//po
    if (kaPyetjeHapurMagazina) return;
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var idkodi = grida.getTekstQelize("txtIdKodi", index);
    var magazina = grida.getTekstQelize(emerKodi, index);
    var art = memoryArt.Get(idkodi);
    var magazinaTrupit = art == null ? magazina : art[emerKodi];

    if (!(index == idRresht || magazinaTrupit.Kodi != magazina))
        return;

    var llojartikulli = kthellojArt(idRresht);
    if (!(ui == null || ui.item == null || magazinaTrupit.Kodi != magazina))
        return;

    var previousMagTrupi = grida.merrTeDhenaPerQelizen("txtMagazina", idRresht, "IshMagazina");
    if (magazina == previousMagTrupi || pageState.veprimi == "blerje" || !kaSeriale || art == undefined || (art != undefined && art.IdFormatSeriali == 0 && art.Klasa != 4)) {
        changeFuncMagVendosMagazine(magazina, llojartikulli, index, idkodi, data_DateEdit.GetDate());
        return;
    }
    kaPyetjeHapurMagazina = true;
    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: hfState.Get("msgSerialet1ArtDoTeFshihen1"),
        modal: true,
        layout: "center",
        idGjuha: pageState.idGjuha,
        okClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            FshiSeriale(idkodi, previousMagTrupi);
            changeFuncMagVendosMagazine(magazina, llojartikulli, index, idkodi, data_DateEdit.GetDate());
        },
        cancelClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            grida.setTekstQelize("txtMagazina", idRresht, previousMagTrupi);
        }
    });
}
function changeFuncMagVendosMagazine(magazina, llojartikulli, index, idja, datedok) {
    if (!Utils.IsNullOrEmpty(magazina)) {
        $.ajax({
            index: index,
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraMagazinaMeKod"),
            data: JSON.stringify({
                kodi: magazina, idNdermarrje: pageState.idNdermarrje, llojArt: llojartikulli, idja: idja, datedok: datedok
            })
        }).done(function (result) { SucceededCallbackMagazina(result, this.index); });
        return;
    }
    vendosMagazinen({ idRreshti: index, Kodi: null });
}

function selectFunc2(event, ui, emerfushe, iddetajim, koddetajim, idRreshtiPare) { //po
    selectFuncDetajimi(ui, emerfushe, iddetajim, koddetajim, idRreshtiPare, 1);
}

function selectFunc3(event, ui, emerfushe, iddetajim, koddetajim, idRreshtiPare) { //po
    selectFuncDetajimi(ui, emerfushe, iddetajim, koddetajim, idRreshtiPare, 2); 
}

function selectFuncDetajimi(ui, emerfushe, iddetajim, koddetajim, idRreshtiPare, lloji) {
    var grida = $(pageState.gridaSelector);
    var idRresht;
    var magazine = "";

    if (idRreshtiPare)
        idRresht = idRreshtiPare;
    else
        idRresht = grida.getLastSel2();
    var idja = (iddetajim && koddetajim) ? iddetajim : ui.item != null ? ui.item.value : 0;
    var kodi = (iddetajim && koddetajim) ? koddetajim : ui.item != null ? ui.item.label : '';
    if (idja == 0 && kodi == '')
        return;
    if (pageState.kushte.GJDM)
        magazine = grida.getTekstQelize('txtMagazina', idRresht);
    var kodArtikull = grida.getTekstQelize('txtKodi', idRresht);
    var listeImei = lloji == 1 ? merrImeiPerArtikull(kodArtikull, idRresht) : new Array(); 
    $(emerfushe).val(kodi);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraDetajimMeID"),
        data: JSON.stringify({ idja: idja, index: idRresht, lloji: lloji, kodartikulli: kodArtikull, idkokamagazina: 0, idNdermarrje: pageState.idNdermarrje, idPerdorues: pageState.idPerdoruesi, magazine: magazine, date: data_DateEdit.GetText(), kontrolloImeiFifo: pageState.kushte.AFI, listeImei: listeImei, promocione: cmbModeli.GetText().indexOf('USHmag') == -1, DokumentTransferimiOwn: false, merrPerberesit: pageState.kushte["NDPAP"] })
    }).done(SucceededCallbackDetajim);
    return false;
}

function merrImeiPerArtikull(kodartikulli, idrreshti) {
    if (!pageState.kushte.AFI)
        return new Array();
    var grida = $('#rowed5');

    var ids = jQuery("#rowed5").getDataIDs();
    var listeImei = new Array();
    for (i = 0; i < ids.length; i++) {
        if (ids[i] == idrreshti)
            continue;
        if (grida.getTekstQelize('txtKodi', ids[i]) == kodartikulli && grida.getTekstQelize('txtDetajimi', ids[i]) != "")
            listeImei.push(grida.getTekstQelize('txtDetajimi', ids[i]));
    }
    return listeImei;
}


function selectFuncLlogShpenzimi(event, ui, emerfushe, idLlog, nrLlog) { //po
    var idRresht = $(pageState.gridaSelector).getLastSel2();
    if (idLlog && nrLlog && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(nrLlog);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogShpenzimiID"),
            data: JSON.stringify({ idja: idLlog, index: idRresht, idPerdoruesi: pageState.idPerdoruesi })
        }).done(SucceededCallbackLlogShpenzimi);

        return false;
    }

    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogShpenzimiID"),
            data: JSON.stringify({ idja: ui.item.value, index: idRresht, idPerdoruesi: pageState.idPerdoruesi })
        }).done(SucceededCallbackLlogShpenzimi);

        return false;
    }
}
function selectFunc4(event, ui, emerfushe, idKat, kodiKat) { //po
    var idRresht = $(pageState.gridaSelector).getLastSel2();
    if (idKat && kodiKat && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(kodiKat);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraKatShpenzimiMeId"),
            data: JSON.stringify({ id: idKat, index: idRresht, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackKatShpenzimi);

        return false;
    }

    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraKatShpenzimiMeId"),
            data: JSON.stringify({ id: ui.item.value, index: idRresht, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackKatShpenzimi);

        return false;
    }
}

function selectFuncMag(event, ui, emerfushe, idMag, kodiMag) { //po
    if (kaPyetjeHapurMagazina) return;
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var idArt = grida.getTekstQelize("txtIdKodi", idRresht);
    var mag = kodiMag ? kodiMag : ui.item.label;

    var art = memoryArt.Get(idArt);

    var previousMagTrupi = grida.merrTeDhenaPerQelizen("txtMagazina", idRresht, "IshMagazina");

    if (mag == previousMagTrupi || pageState.veprimi == "blerje" || !kaSeriale || art == undefined || (art != undefined && art.IdFormatSeriali == 0 && art.Klasa != 4)) {
        selectFuncMagVendosMagazinen(event, ui, emerfushe, idMag, kodiMag, idArt, data_DateEdit.GetDate());
        return;
    }
    kaPyetjeHapurMagazina = true;
    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: hfState.Get("msgSerialet1ArtDoTeFshihen1"),
        modal: true,
        layout: "center",
        idGjuha: pageState.idGjuha,
        okClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            FshiSeriale(idArt, previousMagTrupi);
            selectFuncMagVendosMagazinen(event, ui, emerfushe, idMag, kodiMag, idArt, data_DateEdit.GetDate());
        },
        cancelClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            grida.setTekstQelize("txtMagazina", idRresht, previousMagTrupi);
        }
    });

}

function selectFuncMagVendosMagazinen(event, ui, emerfushe, idMag, kodiMag, idja, datedok) {
    var idRresht = $(pageState.gridaSelector).getLastSel2();
    var llojartikulli = kthellojArt(idRresht);
    if (idMag && kodiMag && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(kodiMag);
        $.ajax({
            pritPergjigje: true,
            index: idRresht,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraMagazinaMeID"),
            data: JSON.stringify({ id: idMag, index: idRresht, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, llojArt: llojartikulli, idja: idja, datedok: datedok })
        }).done(function (result) { SucceededCallbackMagazina(result, this.index); });

        return false;
    }

    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            index: idRresht,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraMagazinaMeID"),
            data: JSON.stringify({
                id: ui.item.value, index: idRresht, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idja: idja, datedok: datedok
            })
        }).done(function (result) { SucceededCallbackMagazina(result, this.index); });

        return false;
    }
}
function selectFunc6(event, ui, idFushe, emerKodi) { //po per cmimet
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    if (ui.item != null) {
        grida.setTekstQelize(emerKodi, idRresht, ui.item.value);

        if (emerKodi == 'txtCmimi')
            changedCmimireshti(event, null, emerKodi, idRresht);
        else
            changedCmimireshtiTvsh(event, null, emerKodi, idRresht);
        return false;
    }
}

function changeFunc2(event, ui, emerKodi, index) {//po
    changeFuncDetajimi(index, 1, event);
}

function changeFunc3(event, ui, emerKodi, index) {//po
    changeFuncDetajimi(index, 2, event);
}

function changeFuncDetajimi(index, lloji, event) {
    var grida = $(pageState.gridaSelector);

    var data = grida.getTeDhenaRreshti(index);
    var rreshti = (data.length > 0) ? data[0] : data;
    var detajimi = lloji == 1 ? rreshti.txtDetajimi : rreshti.txtDetajimi2t;
    if (detajimi != "" && rreshti.txtKodi != "") {
        var listeImei = lloji == 1 ? merrImeiPerArtikull(grida.getTekstQelize('txtKodi', index), index) : new Array();
        var magazine = pageState.kushte.GJDM ? rreshti.txtMagazina : "";
        Utils.shfaqLoadingGif();
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraDetajimMeKod"),
            data: JSON.stringify({
                kodi: detajimi, index: index, lloji: lloji, kodartikulli: rreshti.txtKodi, idkokamag: 0, idNdermarrje: pageState.idNdermarrje, idPerdorues: pageState.idPerdoruesi,
                magazine: magazine, date: data_DateEdit.GetText(), kontrolloImeiFifo: pageState.kushte.AFI, listeImei: listeImei, promocione: cmbModeli.GetText().indexOf('USHmag') == -1,
                DokumentTransferimiOwn: false, merrPerberesit: pageState.kushte["NDPAP"]
            })
        }).done(function (result) { SucceededCallbackDetajim(result, event); });
    }
    else {
        var art = memoryArt.Get(rreshti.txtIdKodi);
        if (!art)
            return;
        var idKategoriDetajimi = lloji == 1 ? art.IdKategoriDetajimi : art.IdKategoriDetajimi2;
        if (art && idKategoriDetajimi)
            vendosDetajim([index, null, lloji, idKategoriDetajimi, null], event);
    }

}

function changeFunc6(event, ui, emerKodi, index) {//po
    var idRresht = $(pageState.gridaSelector).getLastSel2();
    if (emerKodi == 'txtCmimi')
        changedCmimireshti(event, ui, emerKodi, index);
    else
        changedCmimireshtiTvsh(event, ui, emerKodi, index);
}

function resetRreshtKorent(idRreshti) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);//heq serialet
    if (hfSeriale.Contains(idkodi + '_' + idRreshti))
        hfSeriale.Remove(idkodi + '_' + idRreshti);
    if (hfSasiSeriale.Contains(idkodi + '_' + idRreshti))
        hfSasiSeriale.Remove(idkodi + '_' + idRreshti);
    if (idRreshti == idRow && $('#txtKodi' + idRow).val() != undefined) {
        grida.setTekstQelize('txtCmimi', idRreshti);
        grida.setTekstQelize('txtCmimiTvsh', idRreshti);
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');
        grida.setTekstQelize('txtRezervuar', idRreshti, 'false');
        grida.setTekstQelize('txtIdTrupi', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiKonvertimi', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiKonvertimBlerje', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiKthim', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiTransferimi', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiRezervimi', idRreshti, '0');
        grida.setTekstQelize('txtPershkrimi', idRreshti, '');
        grida.setTekstQelize('txtKodbari', idRreshti, '');
        grida.setTekstQelize('txtPershkrimiKatShpenzimi', idRreshti, '');
        grida.setTekstQelize('txtSasia', idRreshti);
        grida.setTekstQelize('txtSasiaRez', idRreshti);
        grida.setTekstQelize('txtSasiMbetur', idRreshti);
        grida.setTekstQelize('txtGjeresi', idRreshti);
        grida.setTekstQelize('txtGjatesi', idRreshti);
        grida.setTekstQelize('txtSasiPermase', idRreshti);

        grida.setTekstQelize('txtVleftaTVSH', idRreshti);
        grida.setTekstQelize('txtVlefta', idRreshti);
        grida.setTekstQelize('txtZbritjaVlere', idRreshti);
        grida.setTekstQelize('txtZbritja', idRreshti);
        grida.setTekstQelize('txtShenime', idRreshti, '');
        grida.setTekstQelize('txtSerial', idRreshti, '');
        grida.setTekstQelize('txtSerial', idRreshti, '');
        var objTmp = new Object();
        objTmp.value = "";
        objTmp.text = "";
        var arrayOptions = new Array(1);
        arrayOptions[0] = objTmp;
        $('#cmbNjesia' + idRreshti).replaceWith(myJQGrid.myElemCombo("", 'cmbNjesia', idRreshti, updateCmimiRowKorrent, arrayOptions, true).children()[0]);

        grida.setTekstQelize('cbTVSH', idRreshti, "", null, null, myelemComboTVSHSup);
        changedTVSHReshti(idRreshti);
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    grida.rregulloNrRendorMeTeMadh(idRreshti);
    return;
}

function kontrolloGjendjeMinMaxNeGride(artikulli, Gjendjemin, Gjendjemax, magazina) {
    if (!artikulli) {
        console.info("Te te vije rende, artikulli eshte null");
        return;
    }

    var grida = $(pageState.gridaSelector);
    var ids = grida.getDataIDs();
    var totali = 0;
    var totaliMag = 0;

    // Ne modifikim
    var sasiaNeGridParaTot = 0;
    var sasiaNeGridParaMag = 0;
    if (colTrup != undefined && colTrup != null && magazina != "") {
        for (var i = 0; i < colTrup.length; i++) {
            if (colTrup[i].IdKodi == artikulli.IdArtikulli) {
                if (colTrup[i].IdMagazina == colNjesAdminis.find(function (o) { return o.Kodi === magazina; }).IdNjesiAdministrative)
                    sasiaNeGridParaMag += colTrup[i].Sasia;
                sasiaNeGridParaTot += colTrup[i].Sasia;
            }
        }
    }

    for (var i = 0; i < ids.length; i++) {
        if (grida.getTekstQelize('txtKodi', ids[i]) === artikulli.KodArtikulli) {
            if (grida.getTekstQelize('txtMagazina', ids[i]) == magazina) {
                if (artikulli.KodNjesia1 === grida.getTekstQelize('cmbNjesia', ids[i]))
                    totaliMag = totaliMag + parseFloat(grida.getTekstQelize('txtSasia', ids[i]));
                else
                    totaliMag = totaliMag + parseFloat((grida.getTekstQelize('txtSasia', ids[i]) * artikulli.KoeficientArtikulli));
            }
            if (artikulli.KodNjesia1 === grida.getTekstQelize('cmbNjesia', ids[i]))
                totali = totali + parseFloat(grida.getTekstQelize('txtSasia', ids[i]));
            else
                totali = totali + parseFloat((grida.getTekstQelize('txtSasia', ids[i]) * artikulli.KoeficientArtikulli));
        }
    }

    var sasi, blerjeShitje;
    if (pageState.veprimi == 'blerje') {
        blerjeShitje = true;
        if (magazina != "")
            sasi = artikulli.gjendjeMag - sasiaNeGridParaMag + totaliMag;
        else
            sasi = artikulli.gjendjetot - sasiaNeGridParaTot + totali;
    }
    else {
        blerjeShitje = false;
        if (magazina != "")
            sasi = artikulli.gjendjeMag - sasiaNeGridParaMag - totaliMag;
        else
            sasi = artikulli.gjendjetot - sasiaNeGridParaTot - totali;
    }

    if ((Gjendjemin) || (Gjendjemax))
        myJQGrid.WarnGjendje(Gjendjemin, Gjendjemax, sasi, magazina, artikulli.KodArtikulli, blerjeShitje);
    else
        myJQGrid.WarnGjendje(artikulli.MinimumArtikulli, artikulli.MaximumArtikulli, sasi, magazina, artikulli.KodArtikulli, blerjeShitje);
}

//kontrollojme gjendjen e artikullit per kete magazine ne kete date per keto detajime.
function kontrolloGjendje(idartikuli, mag, data, koddetajim, koddetajim2, iddok, idndermarje, idKonfigAmbjente, idrreshti) {
    var grida = $(pageState.gridaSelector);
    if (hfState.Get("KGJAG") && $('#txtSasia' + idrreshti).val() !== '') {//kontrollojme gjendjen vetem nqs kemi kushtin
        var ids = grida.getDataIDs();
        var totali = 0;
        var totalDetajimpare = 0;
        var totaliDetajimdyte = 0;
        if (idartikuli == "")
            return;
        var artikulli = memoryArt.Get(idartikuli);
        ///gjejme totalet e artikullit, detajimit 1 dhe detajimit 2 ne gride
        for (var i = 0; i < ids.length; i++) {
            if (grida.getTekstQelize('txtIdKodi', ids[i]) == idartikuli) {
                var sasia = parseFloat(grida.getTekstQelize('txtSasia', ids[i]));
                if (isNaN(sasia))
                    return;
                if (artikulli.KodNjesia1 === grida.getTekstQelize('cmbNjesia', ids[i]))
                    totali = totali + sasia;
                else
                    totali = totali + (sasia * artikulli.KoeficientArtikulli);
                var detajimi1 = grida.getTekstQelize('txtDetajimi', idrreshti);
                if (detajimi1 != "" && detajimi1 == grida.getTekstQelize('txtDetajimi', ids[i])) {
                    if (artikulli.KodNjesia1 === grida.getTekstQelize('cmbNjesia', ids[i]))
                        totalDetajimpare = totalDetajimpare + sasia;
                    else
                        totalDetajimpare = totalDetajimpare + (sasia * artikulli.KoeficientArtikulli);
                }
                if (grida.getTekstQelize('txtDetajimi2t', idrreshti) != "" && grida.getTekstQelize('txtDetajimi2t', idrreshti) == grida.getTekstQelize('txtDetajimi2t', ids[i])) {
                    if (artikulli.KodNjesia1 === grida.getTekstQelize('cmbNjesia', ids[i]))
                        totaliDetajimdyte = totaliDetajimdyte + sasia;
                    else
                        totaliDetajimdyte = totaliDetajimdyte + (sasia * artikulli.KoeficientArtikulli);
                }
            }
        }
        //therasim web servicin qe ben kontrollin e gjendjes
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheGjendjeArtikulli"),
            data: JSON.stringify({
                idja: idartikuli, mag: mag, data: data, koddetajim: koddetajim, koddetajim2: koddetajim2, iddok: iddok, idndermarje: idndermarje,
                idKonfigAmbjente: idKonfigAmbjente, idreshti: idrreshti, totalartikulli: totali, totaldetajim1: totalDetajimpare, totaldetajim2: totaliDetajimdyte,
                shitje_blerje: pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar', ekzekutimProdhim:false
            })
        }).done(SucceededCallbackKontrollGjendje);

    }
}


///funksioni qe kthen pergjigje per kontrollin e gjendjes. shfaq mesazhin e kthyer nqs eshte mesazh errori.
function SucceededCallbackKontrollGjendje(result) {
    if (!result.Status)
        myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
}

function SucceededCallbackGjendjeArtikulli(colGjendjeArt, artikulli) {

    var magazina = '';
    var Gjendjemin = null, Gjendjemax = null;
    if (colGjendjeArt != null && colGjendjeArt.length > 0) {
        magazina = colGjendjeArt[0].Magazina;
        Gjendjemin = colGjendjeArt[0].GjendjaMin;
        Gjendjemax = colGjendjeArt[0].GjendjaMax;
    }
    kontrolloGjendjeMinMaxNeGride(artikulli, Gjendjemin, Gjendjemax, magazina);

}
var identifikuesRreshti;
function ShtoPyetjeCeljeDetajim(grida, idDet, idRreshti, kategoria, lloji) {
    if (grida.getTekstQelize(idDet, idRreshti) != "" && kategoria != 0) {
        if (lloji == 1)
            identifikuesPyetje = 'detajimi1';
        else identifikuesPyetje = 'detajimi2';
        identifikuesRreshti = idRreshti;
        myMesazh.ShtoPyetje(hfState.Get("msgDetajimiVendosurNukEkzistonDoniTaCelni"));
        return;
    }
}

function vendosDetajim(result, event) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    if (result == null)
        return;

    var idRreshti = result[0];
    var detajim = result[1];
    var lloji = result[2];
    var kategoria = result[3];
    var detajimfundit = result[6];
    if (detajimfundit)
        lblMsgbox11.SetText(detajimfundit.PershkrimMesazhi);
    if (lloji == 1)
        var det = "#txtDetajimi" + idRreshti;
    else det = "#txtDetajimi2t" + idRreshti;
    var idDet = "txtDetajimi";
    if (lloji != 1)
        idDet = "txtDetajimi2t";
    var kaDetajimArtikulli = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRreshti)).DetajimArtikulli;
    if (!kaDetajimArtikulli && grida.getTekstQelize(idDet, idRreshti) != '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikullpaDetajim"));
        grida.setTekstQelize(idDet, idRreshti, undefined);
        $(det).data('Kodi', '');
        return;
    }
    if (detajim && detajim.IdDetajimArtikulli > 0) {
        if (!result[6].Status && result[6].KodMesazhi == 501) {// detajimi ekziston por nuk eshte i lidhur me artikullin.
            if (kategoria == 0) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtSkaKategoriPerDetajim"));
                grida.setTekstQelize(idDet, idRreshti, '');
                $(det).data('Kodi', '');
                return;
            }
            else {
                if (detajim.KategoriDetajimi != kategoria) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgDetajimMeKategoriTjeter"));
                    grida.setTekstQelize(idDet, idRreshti, '');
                    $(det).data('Kodi', '');
                    return;
                }
                else {
                    if (lloji == 1)
                        identifikuesPyetje = 'detajimi1Lidhje';
                    else identifikuesPyetje = 'detajimi2Lidhje';
                    identifikuesRreshti = idRreshti;
                    if (kategoria == 3 || kategoria == 4) {
                        Utils.RuajLidhjeDetajim(lloji, identifikuesRreshti, grida, pageState.idNdermarrje, pageState.idPerdoruesi);
                    }
                    else
                        myMesazh.ShtoPyetje(hfState.Get("msgDetajimJoLidhur"));
                    return;
                }
            }
        }
        //else vazhdon poshte (pas else te if-it me siper)
    }
    else {
        if ((detajimfundit && !detajimfundit.Status) && detajimfundit.KodMesazhi != 501) {
            popIMEI.Show();
        }
        switch (kategoria) {
            case 1: //detajim
            case 2: //serial
                ShtoPyetjeCeljeDetajim(grida, idDet, idRreshti, kategoria, lloji);
                break;
            case 4: //seri
                if (grida.getTekstQelize(idDet, idRreshti) != '')
                    CelDheLidhDetajimMeArtikull(grida.getTekstQelize('txtKodi', idRreshti), grida.getTekstQelize('txtIdKodi', idRreshti), lloji, kategoria, 1, grida.getTekstQelize(idDet, idRreshti));
                //ShtoPyetjeCeljeDetajim(grida, idDet, idRreshti, kategoria, lloji);
                break;
            case 3: //date skadence
                if (grida.getTekstQelize(idDet, idRreshti) != '') {
                    if ((Date.parseLocale(grida.getTekstQelize(idDet, idRreshti), "dd/MM/yyyy")) == null || grida.getTekstQelize(idDet, idRreshti).length != 10) {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKodiDateSkadenceDuhetFormat"));
                        if (idRreshti != idRow) {
                            grida.SetTekst(idDet, idRreshti, '');
                        }
                        else {
                            grida.setTekstQelize(idDet, idRreshti, '');
                            $(det).data('Kodi', '');
                        }

                    }
                    else if (grida.getTekstQelize(idDet, idRreshti) != '') {
                        CelDheLidhDetajimMeArtikull(grida.getTekstQelize('txtKodi', idRreshti), grida.getTekstQelize('txtIdKodi', idRreshti), lloji, kategoria, 3, grida.getTekstQelize(idDet, idRreshti));
                        //ShtoPyetjeCeljeDetajim(grida, idDet, idRreshti, kategoria, lloji);
                    }
                }
                break;
            default://detajimi nuk ekziston por nuk mund te celet sepse ska kategori detajimi ne kartele artikulli
                if (grida.getTekstQelize(idDet, idRreshti) != '') {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtSkaKategoriPerDetajim"));
                    grida.setTekstQelize(idDet, idRreshti, '');
                    $(det).data('Kodi', '');
                }
                break;
        }
        if (lloji == pageState.llojDetajimNdermarrje)
            callWebserviceCmimArtikulliRow(grida.getTekstQelize("txtKodi", idRreshti), NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), grida.getTekstQelize('cmbNjesia', idRreshti), txtKursi.GetText(), idRreshti, grida.getTekstQelize("txtSasia", idRreshti), merrDetajim(grida, idRreshti), true);
        return;
    }
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && detajim.Loan == 1 && !hfState.Get('Meme')) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKySerialMundTePerdoretVetemPerLoan"));
        grida.setTekstQelize(idDet, idRreshti, undefined);
        $(det).data('Kodi', '');
        if (lloji == pageState.llojDetajimNdermarrje)
            callWebserviceCmimArtikulliRow(grida.getTekstQelize("txtKodi", idRreshti), NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), grida.getTekstQelize('cmbNjesia', idRreshti), txtKursi.GetText(), idRreshti, grida.getTekstQelize("txtSasia", idRreshti), merrDetajim(grida, idRreshti), true);
        return;
    }
    if ((detajimfundit && !detajimfundit.Status) && detajimfundit.KodMesazhi != 501) {
        popIMEI.Show();
        grida.setTekstQelize(idDet, idRreshti, undefined);
        if (lloji == pageState.llojDetajimNdermarrje)
            callWebserviceCmimArtikulliRow(grida.getTekstQelize("txtKodi", idRreshti), NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), grida.getTekstQelize('cmbNjesia', idRreshti), txtKursi.GetText(), idRreshti, grida.getTekstQelize("txtSasia", idRreshti), merrDetajim(grida, idRreshti), true);
        return;
    }
    grida.setTekstQelize(idDet, idRreshti, detajim.KodDetajimArtikulli);
    $(det).data('Kodi', detajim.KodDetajimArtikulli);
    $(det).data('VendosDetajim', true);
    if(event && event.keyCode == 9)
        $(det).siblings("button").focus();
    if ((kategoria == 3 || kategoria == 4) && lloji == 1 && pageState.veprimi != 'blerje') {
        var art = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRreshti));
        if ((kategoria == 3 && art.IdKategoriDetajimi2 == 4) || (kategoria == 4 && art.IdKategoriDetajimi2 == 3)) {
            var det2 = result[4];
            grida.setTekstQelize('txtDetajimi2t', idRreshti, det2.KodDetajimArtikulli);
            $('#txtDetajimi2t').data('Kodi', det2.KodDetajimArtikulli);
        }
    }
    kontrolloGjendje(grida.getTekstQelize('txtIdKodi', idRreshti), grida.getTekstQelize('txtMagazina', idRreshti), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRreshti), grida.getTekstQelize('txtDetajimi2t', idRreshti), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRreshti);
    if (lloji == pageState.llojDetajimNdermarrje)
        callWebserviceCmimArtikulliRow(grida.getTekstQelize("txtKodi", idRreshti), NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), grida.getTekstQelize('cmbNjesia', idRreshti), txtKursi.GetText(), idRreshti, grida.getTekstQelize("txtSasia", idRreshti), merrDetajim(grida, idRreshti), true);
    if (idRreshti == idRow)
        callWebServiceInfoRow(idRow);
    return;
}

function CelDheLidhDetajimMeArtikull(kodiArtikullit, idArtikulli, detajimPareApoDyte, kategoria, llojDetajimi, kodDetajimi) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "CelDheLidhDetajimMeArtikull"),
        showLoading: true,
        data: JSON.stringify({
            idArtikulli: idArtikulli,
            detajimPareApoDyte: detajimPareApoDyte,
            llojDetajimi: llojDetajimi,
            kategoriDetajimi: kategoria,
            kodDetajimi: kodDetajimi
        })
    }).done(function (result) {
        if (result.mesazhi.Status)
            myMesazh.ShtoMesazhInformues(result.mesazhi.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhSesioni(result.mesazhi);
    });
}

function merrDetajim(grida, idRow) {
    switch (pageState.llojDetajimNdermarrje) {
        case 0:
            return "";
        case 1:
            return grida.getTekstQelize('txtDetajimi', idRow);
        case 2:
            return grida.getTekstQelize('txtDetajimi2t', idRow);
        default:
            return "";
    }
}

function callWebserviceArtikulliIPlote(object) {
    if (object.idArt === undefined || object.idArt === "undefined" || typeof (object.idArt) === "undefined")
        return;
    var grida = $(pageState.gridaSelector);

    var magazinatKoka = merrMagazinatKoka(object.KodMagSerialesh);

    myJQGrid.beginWebserviceCmimArtikulliRow(object.idRresht, "txtCmimi");
    myJQGrid.beginWebserviceCmimArtikulliRow(object.idRresht, "txtCmimiTvsh");
    var counterWsKodi = beginWebserviceKodArtikulliRow(object.idRresht);
    var doneFunc;
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && object.idArt == -1)
        doneFunc = function (result) {
            vendosArtPlote(result, object.idRresht, object.ngaLupa, object.peshoreArt);
            postShtimModifikimNgaLupa(object, grida);
        };
    else
        doneFunc = function (result) {
            vendosArtPlote(result, object.idRresht, object.ngaLupa);
            postShtimModifikimNgaLupa(object, grida);
        };
    var detajim = merrDetajim(grida, object.idRresht);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraIdArtTvshEPlote"),
        data: JSON.stringify({
            idArt: object.idArt,
            kodKodbarArt: object.kodKodbarArt,
            idRreshti: object.idRresht,
            data: data_DateEdit.GetDate(),
            idPerdoruesi: pageState.idPerdoruesi,
            llojTvsh: tvshkont,
            meDetajim: pageState.veprimi != 'blerje',
            merrPershkrimMagazine: btnMagazina.GetSelectedItem() == null,
            magazine: object.KodMagSerialesh ? object.KodMagSerialesh : object.magazine, //colmagazina[0]
            merrZbritjeAnalitike: ZbritjaKlientit != undefined && ZbritjaKlientit !== "" && ZbritjaKlientit != 0,
            ZbritjaKlientit: ZbritjaKlientit,
            idNdermarrje: pageState.idNdermarrje,
            merrCmim: pageState.kushte.ZT == false && ndryshocmime,
            NivelCmimi: NivelCmimi,
            monedha: cmbMonedha.GetText(),
            njesiDef: njesiDef,
            kursi: txtKursi.GetText() == "" ? 1 : txtKursi.GetText(),
            sasia: grida.getTekstQelize('txtSasia', object.idRresht) == '' ? grida.getVlereDefault('txtSasia') : grida.getTekstQelize('txtSasia', object.idRresht),
            shitjeblerje: (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? 0 : 1,
            gjendjeMinMax: gjendjeartminmax == 1,
            kontrolloImeiFifo: pageState.kushte.AFI,
            listeIMEIArtikull: object.listeIMEIArtikull,
            promocione: cmbModeli.GetText().indexOf('USHmag') == -1,
            TaksaKF: pageState.TaksaKF,
            sasiaNeGride: object.sasiaNeGride,
            magazinatKoka: magazinatKoka,
            merrMagMeAutorizim: true,
            detajim: detajim ? detajim : "",
            merrSipasDetajimit: pageState.kushte["NAGDN"],
            counterWsKodi: counterWsKodi
        })
    }
    ).done(function (res) {
        myJQGrid.endWebserviceCmimArtikulliRow(object.idRresht, "txtCmimi");
        myJQGrid.endWebserviceCmimArtikulliRow(object.idRresht, "txtCmimiTvsh");
        //Nqs jane nisur disa ws per te marre artikullin, do te vendoset vetem ai i fundit.
        if (res.counterWsKodi < parseInt($("#txtKodi" + object.idRresht).data('pending'))) {
            //removeCounterWsKodArtikulliRow(object.idRresht, counterWsKodi);
            return;
        }
        doneFunc(res);
        removeCounterWsKodArtikulliRow(object.idRresht, counterWsKodi);
        Utils.hiqLoadingGif();
    });
}

function beginWebserviceKodArtikulliRow(idRreshti) {
    //shohim nese ka ws te nisura per kodin e artikullit.
    var counterWsKodi = 0;
    if ($("#txtKodi" + idRreshti).data('pending') && $("#txtKodi" + idRreshti).data('pending') != "")
        counterWsKodi = parseInt($("#txtKodi" + idRreshti).data('pending'));
    $("#txtKodi" + idRreshti).data('pending', counterWsKodi + 1);
    return counterWsKodi + 1;
}


function removeCounterWsKodArtikulliRow(idRreshti, counterWsKodi) {
    $("#txtKodi" + idRreshti).data('pending', 0);
}

function merrMagazinatKoka(KodMagSerialesh) {
    var magArtKoka = "";
    if (KodMagSerialesh)
        magArtKoka = KodMagSerialesh;
    else if (btnMagazina.GetSelectedItem() != null)
        magArtKoka = btnMagazina.GetSelectedItem().GetColumnText('Kodi');
    return { magArtKoka: magArtKoka };
}

function postShtimModifikimNgaLupa(object, grida) {
    if (object.ngaLupa) {
        popupUniversal.Hide();
        grida.jqGrid("setSelection", object.idRresht);
    }
}

//var artikulli, gjendjetot;

function vendosArtPlote(result, idRreshti, ngaLupa, artPeshore) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    if (result == null) {
        return;
    }
    var vleraKodi = grida.getTekstQelize('txtKodi', idRreshti);
    var artikulli = result.artikulli;
    var detajimi = result.detajimiPare;
    var detajimi2 = result.detajimiDyte;
    var detajimfundit = result.detajimFundit;


    if (!artikulli || artikulli.IdArtikulli == -1) {
        if (vleraKodi != "") {
            grida.disable('txtKodi', idRow, true);
            if (pageState.kushte.F == 1)
                grida.disable('txtSasia', idRow, true);
            window.parent.myMesazh.ShtoMesazh({
                type: "confirm",
                UseCancelButton: false,
                text: hfState.Get("artMeKodNukExiston").replace("#kodi", vleraKodi),
                modal: true,
                layout: "center",
                idGjuha: 1,
                okClick: function () {
                    grida.disable('txtKodi', idRow, false);
                    if (pageState.kushte.F == 1)
                        grida.disable('txtSasia', idRow, false);
                }
            });
        }
        resetRreshtKorent(idRreshti);
        return;
    }
    if (!result.magazinatTrupi || Object.keys(result.magazinatTrupi[0]).length == 0) {
        myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta ne asnje magazine!");
        resetRreshtKorent(idRreshti);
        return;
    }
    var magazinaArt = result.magazinatTrupi ? (result.magazinatTrupi[0] != undefined ? result.magazinatTrupi[0].magazina : "") : "";

    if (artPeshore) {
        if (!artikulli.PerPeshore) {
            myMesazh.ShtoMesazhGabimi("Artikulli: " + artPeshore.kodArt + " nuk eshte artikull peshore edhe pse perkon me skemen!");
            resetRreshtKorent(idRreshti);
            return;
        }
        vleraKodi = artPeshore.kodArt;
        grida.setTekstQelize("txtKodi", idRreshti, vleraKodi);
        grida.setTekstQelize("txtSasia", idRreshti, artPeshore.sasia);
    }
    var kodbarisele = result.kodbari ? result.kodbari : "";
   
    var kodbariArtNeGride = result.artikulli.OColKodbare.filter(function (kodbari) { return grida.getTekstQelize("txtKodbari", idRreshti) == kodbari.Pershkrimi; })[0];
    if (!pageState.kushte['NKAKNKA'])
        kodbariArtNeGride = "";
    else
    kodbariArtNeGride = (kodbariArtNeGride) ? kodbariArtNeGride : result.artikulli.OColKodbare[0];

    if (kodbarisele == "" && result.artikulli.OColKodbare.length > 0) {
        //if (kodbariArtNeGride != null)//rasti kur rreshti qe po fokusohem ka te vendosur nje kodbar per kete artikull, duhet te mbahet ai qe eshte nese nuk e ndryshon
        kodbarisele = kodbariArtNeGride.Pershkrimi;
        // else
        // kodbarisele = result.artikulli.OColKodbare[0].Pershkrimi;
    }
    var lloji = grida.getTekstQelize('cmbLloji', idRreshti);
    var njesiaAktuale = grida.getTekstQelize('cmbNjesia', idRreshti);
    var njesiaArt;

    artikulli.listeTvsh = result.listeTvsh;
    artikulli.gjendjetot = result.gjendjeTot;
    artikulli.gjendjeMag = result.gjendjeMag;
    memoryArt.Set(artikulli);
    var isTheSame = false;
    if (result.kerkoMeKodbar && kodbariArtNeGride != null && result.njesia == 0)//rasti kur eshte i njejti kodbar ne ate rresht, te vendoset njesia sipas kodbarit
    {
        njesiaArt = kodbariArtNeGride.Njesia == 1 ? artikulli.KodNjesia1 : artikulli.KodNjesia2;
    }
    else {
        njesiaArt = result.kerkoMeKodbar && result.kodbari ? (result.njesia == 1 ? artikulli.KodNjesia1 : artikulli.KodNjesia2) : (njesiDef == 1 ? artikulli.KodNjesia1 : artikulli.KodNjesia2);
    }
    if (!ngaLupa && myJQGrid.checkIfIsTheSame(grida, artikulli, detajimi, idRreshti, lloji, kodbariArtNeGride != null ? kodbariArtNeGride.Pershkrimi : "", kodbarisele, result.kerkoMeKodbar))
        return;//eshte i njejti artikull

    grida.vendosTeDhenaPerQelizen("txtKodi", idRreshti, "kerkoMeKodbar", result.kerkoMeKodbar);
    grida.vendosTeDhenaPerQelizen("txtKodi", idRreshti, "kodiEkzistues", result.artikulli.KodArtikulli);
    //var magazinaArt = result.magazinatTrupi ? (result.magazinatTrupi[0] != undefined ? result.magazinatTrupi[0].magazina : "") : "";

    if (artikulli.Klasa == 4)
        $('#' + idRreshti +'_txtSerialUnik').removeAttr("disabled");

    if (artikulli.Aktiv == false) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliEshteInaktiv"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if ((artikulli.Klasa === 5 || artikulli.Klasa === 6) && pageState.veprimi == 'blerje') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKryhenVeprimeMeArtikujProdhim"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if ((artikulli.Klasa === 6) && (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar')) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKryhenVeprimeMeArtikujProdhimProces"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if (artikulli.Klasa === 4 && pageState.veprimi == 'blerje' && hfState.Get("LEJOARTPERB") === "Jo") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryeniVeprimeMeArtikujTePerbere"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if (artikulli.Klasa === 4 && hfState.Get("LNZAP") === true) {
        myMesazh.ShtoMesazhInformues(hfState.Get("msgKyartikullEshteIPerbere"));
    }

    if (merrSipasGrupit) {
        if (cmbGrup1.GetText() == "Karta" || cmbGrup1.GetText() == "Loan") {
            if (artikulli.KodKodifikimi2 != cmbGrup1.GetText()) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliNukIPerketKetijGrupDokumenti"));
                resetRreshtKorent(idRreshti);
                return;
            }
        }
        else if (cmbGrup1.GetText() == "Aparate" || cmbGrup1.GetText() == "Aparate ekspozitore") {
            if (artikulli.KodKodifikimi2 != "Aparate" && artikulli.KodKodifikimi2 != "Loan") {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliNukIPerketKetijGrupDokumenti"));
                resetRreshtKorent(idRreshti);
                return;
            }
        }
    }
    if (merrDhurata) {
        if (!artikulli.Dhurate) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTePerdorenArtikujtJoDhurate"));
            resetRreshtKorent(idRreshti);
            return;
        }
    }
    if (detajimfundit && !detajimfundit.Status) {
        lblMsgbox11.SetText(detajimfundit.PershkrimMesazhi);
        popIMEI.Show();
        resetRreshtKorent(idRreshti);
        return;
    }

    if (artikulli.LlojiArt && !pageState.kushte.GJDM)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyDokumentGjeneronDokumentMagazine"));

    if (lloji == 'Artikull') {//heq serialet e artikullit te vjeter ne rast se po modifikohet i njejti rresht
        var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);
        if (hfSeriale.Contains(idkodi + '_' + idRreshti))
            hfSeriale.Remove(idkodi + '_' + idRreshti);
        if (hfSasiSeriale.Contains(idkodi + '_' + idRreshti))
            hfSasiSeriale.Remove(idkodi + '_' + idRreshti);
    }
    //var njesiaArt = result.kodbari ? (result.njesia == 1 ? artikulli.KodNjesia1 : artikulli.KodNjesia2) : (njesiDef == 1 ? artikulli.KodNjesia1 : artikulli.KodNjesia2);

    grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
    grida.vendosTeDhenaPerQelizen("txtKodi", idRreshti, "IdLlogari", artikulli.IdLlogariKomisioni);
    grida.vendosTeDhenaPerQelizen("txtKodi", idRreshti, "MeKomision", artikulli.LlogaritKomision);
    myJQGrid.closeAutocomplete('txtKodi', idRreshti);

    grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);

    grida.setTekstQelize('txtKodbari', idRreshti, kodbarisele);

    if (idRreshti == idRow && vleraKodi != undefined) {
        var comboNjesia = $('#cmbNjesia' + idRreshti);
        comboNjesia.replaceWith(myelemComboNjesiaSup(njesiaArt, null, idRreshti, artikulli).children()[0]);
    }
    else
        grida.setTekstQelize('cmbNjesia', idRreshti, njesiaArt);

    if (artikulli.KodArtikulli != "" && detajimi != null && pageState.veprimi != 'blerje') //detajimi pare
    {
        if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && detajimi.Loan == 1 && !hfState.Get('Meme')) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKySerialMundTePerdoretVetemPerLoan"));
            grida.setTekstQelize('txtDetajimi', idRreshti, '');
            $('#txtDetajimi' + idRreshti).data('Kodi', '');
        }
        else {
            grida.setTekstQelize('txtDetajimi', idRreshti, detajimi.KodDetajimArtikulli);
            $('#txtDetajimi' + idRreshti).data('Kodi', detajimi.KodDetajimArtikulli);
        }
    }
    else {
        grida.setTekstQelize('txtDetajimi', idRreshti, '');
        $('#txtDetajimi' + idRreshti).data('Kodi', '');
    }

    if (artikulli.KodArtikulli != "" && detajimi2 != null && pageState.veprimi != 'blerje') { //detajimi dyte
        grida.setTekstQelize('txtDetajimi2t', idRreshti, detajimi2.KodDetajimArtikulli);
        $('#txtDetajimi2t' + idRreshti).data('Kodi', detajimi2.KodDetajimArtikulli);
    }
    else {
        grida.setTekstQelize('txtDetajimi2t', idRreshti, '');
        $('#txtDetajimi2t' + idRreshti).data('Kodi', '');
    }

    if (!kontrolloRow(idRreshti, artikulli)) {
        return;
    }
    artikulli['txtMagazina'] = magazinaArt;
    memoryArt.Set(artikulli);

    if (result.kodbari)
        llogaritSasiGjeresiGjatesiArtSipasFormulesKodbarit(artikulli, result.kodbari, idRreshti, grida);
    if (artikulli.IRezervueshem) {
        grida.setTekstQelize('txtRezervuar', idRreshti, 'true');
    }
    else {
        grida.setTekstQelize('txtSasiaRez', idRreshti);
        grida.setTekstQelize('txtRezervuar', idRreshti, 'false');
    }

    //fusha sasi litra per tollonat
    if (artikulli.KodArtikulli != "" && artikulli.Klasa === 4) {
        vendosSasiLiter(result.koefArtPerbere, idRreshti);
        updateTotalSasiLitra();
    }


    var comboMag = $('#txtMagazina' + idRreshti);
    grida.setTekstQelize('txtMagazina', idRreshti, magazinaArt.Kodi);
    grida.vendosTeDhenaPerQelizen('txtMagazina', idRreshti, "IshMagazina", magazinaArt.Kodi);
    grida.setTekstQelize('txtPershkrimmag', idRreshti, magazinaArt.Pershkrimi);

    if (artikulli.LlojiArt && comboMag.val() == "")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeCaktoniMagazinenPerArtikujtAfatgjate"));

    if (pershk == 1) {
        grida.setTekstQelize('txtPershkrimi', idRreshti, artikulli.PershkrimArtikulli);
    }
    else {
        if (artikulli.PershkrimiAngArtikulli !== "")
            grida.setTekstQelize('txtPershkrimi', idRreshti, artikulli.PershkrimiAngArtikulli);
        else
            grida.setTekstQelize('txtPershkrimi', idRreshti, artikulli.PershkrimArtikulli);
    }

    //vendos tvsh-ne
    if (dogana === 2) {
        grida.setTekstQelize('cbTVSH', idRreshti, result.listeTvsh[0].text, null, null, myelemComboTVSHSup);
    }
    else {
        grida.setTekstQelize('cbTVSH', idRreshti, 'Pa TVSH', null, null, myelemComboTVSHSup);
    }

    if (result.zbritjeAnalitike)
        SucceededCallbackZbritjeArtikulliRow(result.zbritjeAnalitike, idRreshti);

    if (result.cmimArtikulliResult) {
        SucceededCallbackCmimArtikulliRow(result.cmimArtikulliResult);
    }

    if ((pageState.kushte.ZADHG == false && grida.getTekstQelize('txtCmimi', idRreshti) != 0)) {
        changedSasia(null, idRreshti);
    }
    if (!ndryshocmime)
        changedCmimireshti(idRreshti);
    if (pageState.kushte.ZT) {
        grida.setTekstQelize('txtCmimi', idRreshti, 1);
        changedCmimireshti(null, null, 'txtCmimi', idRreshti);
    }
    else
        if (!ndryshocmime)
            changedCmimireshti(null, null, 'txtCmimi', idRreshti);

    if (result.colGjendjeArtikulli) {
        SucceededCallbackGjendjeArtikulli(result.colGjendjeArtikulli, artikulli);
    }
    //if (grida.getTekstQelize("cmbLloji", idRreshti) == "")
    //    grida.selektoRreshtin(idRreshti, true);
    //Nuk i bashkojme keto dy ws me te tjeret pasi gjendja e artikullit nuk do te vendoset ne gride dhe do jete e kushtueshme
    //ndersa info e artikullit gjithashtu ka kosto dhe eshte e pavarur nga veprimet e tjera ne gride
    kontrolloGjendje(artikulli.IdArtikulli, grida.getTekstQelize('txtMagazina', idRreshti), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRreshti), grida.getTekstQelize('txtDetajimi2t', idRreshti), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRreshti);
    if (result.artikulli.LlogaritKomision && btnKlienti.GetValue() != "" && btnKlienti.GetValue() != null) {
        if (pageState.Klient == null && pageState.Klient == undefined && pageState.Klient == "")
            grida.setTekstQelize('txtVleraKomisionit', idRreshti, ktheVlereKomision((result.zbritjeAnalitike != null || result.zbritjeAnalitike != undefined) ? result.zbritjeAnalitike.Zbritja : 0, grida.getTekstQelize("txtVleftaTVSH", idRreshti)));
        else {
            var klienti = JSON.parse(pageState.Klient);
            if (klienti.kf.LlogaritKomision)
                grida.setTekstQelize('txtVleraKomisionit', idRreshti, ktheVlereKomision((result.zbritjeAnalitike != null || result.zbritjeAnalitike != undefined) ? result.zbritjeAnalitike.Zbritja : 0, grida.getTekstQelize("txtVleftaTVSH", idRreshti)));
        }
    }
    else
        grida.setTekstQelize('txtVleraKomisionit', idRreshti, 0);

    if (idRreshti == idRow && vleraKodi != undefined)
        callWebServiceInfoRow(idRreshti);
    if (Utils.getUrlVar('shitje_blerje') == 'blerje')
        changedCmimireshti(idRreshti);
    return;

}

function vendosArtNgaLupa(result) {
    var artikulli = result.artikulli;
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var rreshtatNjesoj = grida.gjejRreshtaNjesoj(artikulli.IdArtikulli, 'Artikull', 'txtIdKodi', 'cmbLloji');

    for (i = 0; i < rreshtatNjesoj.length; i++) {
        var index = rreshtatNjesoj[i];
        var sasia = 0;
        var kodbar = "";
        if (artikulli.OColKodbare.length > 0) kodbar = artikulli.OColKodbare[0].Pershkrimi;
        grida.setTekstQelize('txtKodbari', index, kodbar);
        if (pershk == 1)
            grida.setTekstQelize('txtPershkrimi', index, artikulli.PershkrimArtikulli);
        if (btnMagazina.GetSelectedItem() != null) {
            var mag = btnMagazina.GetSelectedItem().GetColumnText('Kodi');
            //   btnMagazina.GetText().split(' ')[0];
            var pershkrimmag = btnMagazina.GetSelectedItem().GetColumnText('Kodi');
            //btnMagazina.GetText().split(' ')[0];
        }
        if (artikulli.IRezervueshem)
            grida.setTekstQelize('txtRezervuar', index, 'true');
        else {
            grida.setTekstQelize('txtRezervuar', index, 'false');
        }
        if (index == idRow) { //per rreshtin e selektuar
            var comboNjesia = $('#cmbNjesia' + index);
            if (njesiDef == 1)
                comboNjesia.replaceWith(myelemComboNjesiaSup(artikulli.KodNjesia1, null, index, artikulli).children()[0]);
            else
                comboNjesia.replaceWith(myelemComboNjesiaSup(artikulli.KodNjesia2, null, index, artikulli).children()[0]);
            var comboMag = $('#txtMagazina' + index);
            if (((artikulli.IdMagazina != 0) && (mag == '' || mag == undefined))) //magazina
                mag = artikulli.Magazina; //ishte IdMagazina
            grida.setTekstQelize('txtMagazina', index, mag);
            grida.vendosTeDhenaPerQelizen('txtMagazina', index, "IshMagazina", mag);
            sasia = grida.getTekstQelize('txtSasia', index);
            if (dogana === 2) {
                grida.setTekstQelize('cbTVSH', index, result.listeTvsh[0].text, null, null, myelemComboTVSHSup);
            }
            else {
                grida.setTekstQelize('cbTVSH', index, 'Pa TVSH', null, null, myelemComboTVSHSup);
            }
            changedSasia(null, index);
        }
        else {
            if (njesiDef == 1)
                grida.setTekstQelize('cmbNjesia', index, artikulli.KodNjesia1);
            else
                grida.setTekstQelize('cmbNjesia', index, artikulli.KodNjesia2);
            if (dogana === 2) {
                grida.setTekstQelize('cbTVSH', index, result.listeTvsh[0].text, null, null, myelemComboTVSHSup);
            }
            else {
                grida.setTekstQelize('cbTVSH', index, 'Pa TVSH', null, null, myelemComboTVSHSup);
            }
            if (artikulli.IdMagazina != 0 && (mag == '' || mag == undefined)) //magazina
            {
                grida.setTekstQelize('txtMagazina', index, artikulli.Magazina);
                grida.vendosTeDhenaPerQelizen('txtMagazina', index, "IshMagazina", artikulli.Magazina);
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimMagSipasId"),
                    data: JSON.stringify({ id: artikulli.IdMagazina })
                }).done(function (result) { SucceededCallbackChangeMag(result); });
            }
            sasia = grida.getTekstQelize('txtSasia', index);
            changedSasia(null, index);
        }
        if (grida.getTekstQelize('cmbLloji', index) != "Llogari" && artikulli.KodArtikulli != "" && pageState.kushte.ZT == false && ndryshocmime && !(pageState.kushte.ZADHG == false && grida.getTekstQelize('txtCmimi', index) != 0)) {//&& pageState.veprimi != 'blerje'&& NivelCmimi != undefined && NivelCmimi != "" && NivelCmimi != 0
            callWebserviceCmimArtikulliRow(artikulli.KodArtikulli, NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), grida.getTekstQelize('cmbNjesia', rreshtatNjesoj[i]), txtKursi.GetText(), rreshtatNjesoj[i], sasia, "");
        }
        if ((pageState.kushte.ZADHG == false && grida.getTekstQelize('txtCmimi', idRreshti) != 0)) {
            changedSasia(null, idRreshti);
        }
    }
    var rreshtiFundit = grida.getGridParam('reccount');
    if (grida.getCell(rreshtiFundit, 'txtKodi') != "") {//Shto nje rresht bosh ne fund nese nuk ka nje te tille
        var rreshtiTjeter = parseInt(rreshtiFundit) + 1;
        var disable = (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && $("input[id$='hfShtimModifikim']").val() == "konvertim") || (lidhur && $("input[id$='hfShtimModifikim']").val() != "bli"));


        be = myJQGrid.myValueButtonFshi(disable, rreshtiTjeter, "#rowed5");
        grida.addRowData(parseInt(rreshtiTjeter), { txtFshi: be });
    }
    popupUniversal.Hide();
}

function SucceededCallbackDetajim(result, event) {//po
    Utils.hiqLoadingGif();
    vendosDetajim(result, event);
}

function callWebServiceInfoKF() {
    if (!eshteInfoHapur() || !infoKf) return;
    if (btnKlienti.GetValue()) {
        pastroInfoKf();
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheInfoKF"), data: JSON.stringify({ idja: btnKlienti.GetValue(), data: data_DateEdit.GetDate(), idInfo: idInfoKf })
        }).done(SucceededCallbackInfoKF);
    }
}

function callWebServiceInfoRow(idRresht) {
    if (!eshteInfoHapur()) return;
    var grida = $(pageState.gridaSelector);
    if (idRresht === undefined)
        idRresht = grida.getLastSel2();
    var idKodi = grida.getTekstQelize('txtIdKodi', idRresht);
    if (!idKodi)
        return;
    var lloji = grida.getTekstQelize('cmbLloji', idRresht);
    switch (lloji) {
        case "Artikull":
            if (infoArt) {
                var magtrup = grida.getTekstQelize('txtMagazina', idRresht);
                var detajim = grida.getTekstQelize('txtDetajimi', idRresht);
                var njesiart = grida.getTekstQelize('cmbNjesia', idRresht);
                detajim = (!detajim) ? -1 : detajim;
                var detajim2 = grida.getTekstQelize('txtDetajimi2t', idRresht);
                detajim2 = (!detajim2) ? -1 : detajim2;
                var idViti = hfState.Get('idViti');
                var idklient = 0;
                if (btnKlienti.GetValue())
                    idklient = btnKlienti.GetValue();
                pastroInfoArt();
                pastroInfoLlog();

                var idkarta = 0;
                if (cmbKarta.GetValue())
                    idkarta = cmbKarta.GetValue();
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "mbushInfoArtikulliMeDetajime"),
                    data: JSON.stringify({ idkodi: idKodi, data: data_DateEdit.GetDate(), detajim: detajim, index: idRresht, idInfo: idInfoArt, detajim2: detajim2, idViti: idViti, idPerdoruesi: pageState.idPerdoruesi, idklient: idklient, mag: magtrup, njesiart: njesiart, idKarta: idkarta })
                }).done(SucceededCallbackInfoArt);
            }
            break;
        case "Llogari":
            if (infoLl) {
                pastroInfoArt();
                pastroInfoLlog();
                var idNdermarrjeVit = hfState.Get('idNdermarrjeVit');
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "mbushInfoLlogarie"),
                    data: JSON.stringify({ idKodi: idKodi, data: data_DateEdit.GetDate(), index: idRresht, idInfoLlog: idInfoLlog, idNderVit: idNdermarrjeVit })
                }).done(SucceededCallbackInfoLlogari);
            }
            break;
        default: return;
    }
}

function pastroInfoArt() {
    navbar.GetGroupByName('Artikulli').SetExpanded(false);
    lbxZgjedhur.ClearItems();
}

function pastroInfoLlog() {
    navbar.GetGroupByName('Llogari').SetExpanded(false);
    lbxLlogari.ClearItems();
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
    var artExpanded = navbar.GetGroupByName('Artikulli').GetExpanded();
    navbar.GetGroupByName('Artikulli').SetExpanded(!artExpanded);
    navbar.GetGroupByName('Artikulli').SetExpanded(artExpanded);
    var llogExpanded = navbar.GetGroupByName('Llogari').GetExpanded();
    navbar.GetGroupByName('Llogari').SetExpanded(!llogExpanded);
    navbar.GetGroupByName('Llogari').SetExpanded(llogExpanded);
    var kfExpanded = navbar.GetGroupByName('KF').GetExpanded();
    navbar.GetGroupByName('KF').SetExpanded(!kfExpanded);
    navbar.GetGroupByName('KF').SetExpanded(kfExpanded);
}

function SucceededCallbackInfoArt(result) {
    if (result.idRreshti == $(pageState.gridaSelector).getLastSel2() && result.colInfoTrupi.length != 0) {
        navbar.GetGroupByName('Llogari').SetExpanded(false);
        navbar.GetGroupByName('Artikulli').SetExpanded(true);
        vendosInfo(lbxZgjedhur, result);
    }
}

function SucceededCallbackInfoLlogari(result) {
    if (result.idRreshti == $(pageState.gridaSelector).getLastSel2() && result.colInfoTrupi.length != 0) {
        navbar.GetGroupByName('Artikulli').SetExpanded(false);
        navbar.GetGroupByName('Llogari').SetExpanded(true);
        vendosInfo(lbxLlogari, result);
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
    callWebServiceInfoRow($(pageState.gridaSelector).getLastSel2());
    callWebServiceInfoKF();
}

function spliterPaneCollapsing(s, e) {
    hapMbyllInfo(false);
}

function hapMbyllInfo(infoExpanded) {
    if (infoExpanded)
        splitter.GetPane(1).SetSize(300);//madhesia default

    if (infoExpanded && (infoArt || infoKf || infoLl)) {
        if (!eshteInfoHapur())
            splitter.GetPane(1).Expand();
        if (infoArt)
            navbar.GetGroupByName('Artikulli').SetVisible(true);
        else
            navbar.GetGroupByName('Artikulli').SetVisible(false);
        if (infoLl)
            navbar.GetGroupByName('Llogari').SetVisible(true);
        else
            navbar.GetGroupByName('Llogari').SetVisible(false);
        if (infoKf)
            navbar.GetGroupByName('KF').SetVisible(true);
        else
            navbar.GetGroupByName('KF').SetVisible(false);
        navbar.GetGroupByName('Artikulli').SetExpanded(false);
        navbar.GetGroupByName('Llogari').SetExpanded(false);
        navbar.GetGroupByName('KF').SetExpanded(false);
    }
    else if (!infoExpanded && eshteInfoHapur()) {
        if (infoArt)
            navbar.GetGroupByName('Artikulli').SetVisible(true);
        else
            navbar.GetGroupByName('Artikulli').SetVisible(false);
        if (infoLl)
            navbar.GetGroupByName('Llogari').SetVisible(true);
        else
            navbar.GetGroupByName('Llogari').SetVisible(false);
        if (infoKf)
            navbar.GetGroupByName('KF').SetVisible(true);
        else
            navbar.GetGroupByName('KF').SetVisible(false);
        navbar.GetGroupByName('Artikulli').SetExpanded(false);
        navbar.GetGroupByName('Llogari').SetExpanded(false);
        navbar.GetGroupByName('KF').SetExpanded(false);
    }
    else {
        if (eshteInfoHapur())
            splitter.GetPane(1).CollapseBackward();
        navbar.GetGroupByName('Artikulli').SetVisible(false);
        navbar.GetGroupByName('Llogari').SetVisible(false);
        navbar.GetGroupByName('KF').SetVisible(false);
    }
    //var grida = $(pageState.gridaSelector);
    //$('#divgride2').width($('#ASPxSplitter1').width() - ((infoExpanded) ? 335 : 35))
    //myJQGrid.fixGridWidth(grida, $('#divgride2'));


}

function SucceededCallbackLlogPlote(result, eKomision, llogariGurpuar) {//po
    vendosLlogPlote(result, mbushGrideKomision, llogariGurpuar);
}

function vendosLlogPlote(result, mbushGrideKomision, llogariGurpuar) {
    var grida;
    rreshtat = new Array();

    if (mbushGrideKomision)
        grida = $(pageState.gridaKomision);
    else
        grida = $(pageState.gridaSelector);

    var idRow = grida.getLastSel2();
    if (result != null) {
        if (mbushGrideKomision)//grida komision
        {
            grida.jqGrid('clearGridData');
            for (var i = 0; i < result.length; i++) {
                var rreshti = krijoRreshtBosh(i + 1, "");
                rreshti.cmbLloji = "Llogari";
                rreshti.txtIdKodi = result[i].llogaria.IdLlogari;
                rreshti.txtKodi = result[i].llogaria.NrLlogari;
                rreshti.txtPershkrimi = result[i].llogaria.EmerLlogari1;
                rreshti.txtSasia = 1;
                rreshti.txtCmimi = 0 - llogariGurpuar[i].sasia;
                rreshti.txtVleftaTVSH = 1 * (0 - llogariGurpuar[i].sasia);
                rreshti.txtVlefta = 1 * (0 - llogariGurpuar[i].sasia);
                rreshti.cbTVSH = "Pa TVSH";
                rreshti.txtVleraKomisionit = llogariGurpuar[i].sasia;
                rreshtat.push(rreshti);
            }
            grida[0].addJSONData(rreshtat);
            return;
        }
        else {
            /* var newIdRreshti = grida.gjeRreshtBoshPasKetijRreshti("txtKodi", undefined, undefined, idRreshti); //edhe ketu
              if (newIdRreshti == -1) {
                  grida.shtoRresht(buttonFshiEnable, pageState.gridaKomision);
              }
             */
            if (!result.length) {// ne rastin kur llogaria merret sipas kodit dhe ws nuk kthen nje array objektesh 
                var aresult = result;
                result = new Array();
                result.push(aresult);
            }
            for (var i = 0; i < result.length; i++) {
                var idRreshti = result[i].idRreshti;
                var llogaria = result[i].llogaria;
                if (!llogaria || llogaria.NrLlogari == -1) {
                    resetRreshtKorent(idRreshti);
                    return;
                }
                if (grida.getTekstQelize('txtIdKodi', idRreshti) == llogaria.IdLlogari && grida.getTekstQelize('cmbLloji', idRreshti) == "Llogari") {
                    if (grida.getTekstQelize('txtKodi', idRreshti) != llogaria.NrLlogari) {
                        grida.selektoRreshtin(idRreshti, true);
                        grida.setTekstQelize('txtKodi', idRreshti, llogaria.NrLlogari);
                    }
                    return; //eshte i njejta llogari
                }
                grida.selektoRreshtin(idRreshti, true);
                llogaria.listeTvsh = result[i].listeTvsh;
                //HfLlog.Set(llogaria.IdLlogari, llogaria);
                memoryLlog.Set(llogaria);
                grida.setTekstQelize('txtKodbari', idRreshti, '');
                grida.setTekstQelize('txtKodi', idRreshti, llogaria.NrLlogari);
                grida.setTekstQelize('txtIdKodi', idRreshti, llogaria.IdLlogari);
                if (pershk == 1)
                    grida.setTekstQelize('txtPershkrimi', idRreshti, llogaria.EmerLlogari1);
                else {
                    if (llogaria.EmerLlogari2 !== "")
                        grida.setTekstQelize('txtPershkrimi', idRreshti, llogaria.EmerLlogari2);
                    else grida.setTekstQelize('txtPershkrimi', idRreshti, llogaria.EmerLlogari1);
                }
                grida.setTekstQelize('cbTVSH', idRreshti, result[i].listeTvsh[0].text, null, null, myelemComboTVSHSup);
                if (idRreshti == idRow) {
                    callWebServiceInfoRow(idRow);
                    changedTVSHReshti(idRow);
                }

            }
            return;
        }
    }
}


function SucceededCallbackLlogShpenzimi(result) {//po
    vendosLlogShpenzimi(result);
}

function SucceededCallbackKatShpenzimi(result) {//po
    vendosKatShpenzimi(result);
}

function SucceededCallbackMagazina(result, index) {
    vendosMagazinen(result, index);
}

function vendosLlogShpenzimi(result) {
    var grida = $(pageState.gridaSelector);
    if (result != null) {
        var llogaria = result.llogaria;
        if (llogaria != null)
            grida.setTekstQelize('txtIdLlogShpenzimi', grida.getLastSel2(), llogaria.NrLlogari);
    }
    return;
}

function vendosKatShpenzimi(result) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    if (result != null) {
        if (!Utils.IsNullOrEmpty(result.Kodi)) {
            grida.setTekstQelize('txtKategoriShpenzimi', idRow, result.Kodi);
            grida.setTekstQelize('txtPershkrimiKatShpenzimi', idRow, result.Pershkrimi);
        }
    }
    return;

}

function vendosMagazinen(result, index) {
    if (result.magazina == null || Utils.IsNullOrEmpty(result.magazina.Kodi))
        return;

    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    if (index != undefined && idRow != index)
        idRow = index;

    var llojartikulli = kthellojArt(idRow);
    var iPerketGrupit = (llojartikulli == -1 || (llojartikulli == 0 && (result.magazina.IdLlojMagazine == 1 || result.magazina.IdLlojMagazine == 3)) ||
        (llojartikulli == 1 && (result.magazina.IdLlojMagazine == 2 || result.magazina.IdLlojMagazine == 3)));
    iPerketGrupit = pageState.kushte.GJDM ? iPerketGrupit : true; //per te vendosur cfare magazine te duash kur nuk gjeneron dokument magazine
    if (iPerketGrupit) {
        grida.setTekstQelize('txtMagazina', idRow, result.magazina.Kodi);
        grida.vendosTeDhenaPerQelizen('txtMagazina', idRow, "IshMagazina", result.magazina.Kodi);
        grida.setTekstQelize('txtPershkrimmag', idRow, result.magazina.Pershkrimi);
        var artikulli = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRow));
        if (artikulli != null) {
            artikulli['txtMagazina'] = result.magazina;
            artikulli.gjendjeMag = result.gjendjeMag;
            memoryArt.Set(artikulli);
        }
    }
    else {
        myMesazh.ShtoMesazhGabimi("Magazina " + result.magazina.Kodi + " nuk i perket llojit per artikullin " + grida.getTekstQelize('txtKodi', idRow));
        grida.setTekstQelize('txtMagazina', idRow, null);
        grida.vendosTeDhenaPerQelizen('txtMagazina', idRow, "IshMagazina", null);
        grida.setTekstQelize('txtPershkrimmag', idRow, null);
        grida.selektoRreshtin(idRow, true);
        grida.setFocus('txtMagazina', idRow);
    }
}

function formGridColsArray(isLidhur) {//po
    var hfGridKod = $('#hfGridaKodi');
    var hfGridDetajim = $('#hfGridaDetajimi');
    var hfLupaLlogShpenz = $('#hfLupaLlogShpenz');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtKodi" }, { hfVar: hfGridDetajim, kodiText: "txtDetajimi" }, { hfVar: hfLupaLlogShpenz, kodiText: "txtIdLlogShpenzimi" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
    if ($("input[id$='hfShtimModifikim']").val() == "bli")
        arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] = "False";

}

/*
Function: myelemCmimi

Nderton nje textbox per te vendosur cmimin.
*/
function myelemCmimi(value, options) {//po
    var disable = 'False';
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    //  var vleraLlojit = grida.getTekstQelize('cmbLloji', idRow);
    //if ((arrayReadOnlyKolonaGrides[14] == 'True') || !lejomod || vleraLlojit == "Llogari" || (hfTeDrejta.Get("NdryshoCmimShitje") == false && pageState.veprimi == 'shitje') || (hfTeDrejta.Get("NdryshoCmimBlerje") == false && pageState.veprimi == 'blerje'))
    //    disable = 'True';
    var shitje;
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar'))
        shitje = true;
    else shitje = false;
    return myJQGrid.myElemCmimiAutocomplete({ value: value, options: options, disabled: ((arrayReadOnlyKolonaGrides[14] == "True") || (readOnly(grida, idRow) ? "True" : "False")), idRreshti: idRow, id: 'txtCmimi', buttonClickKodi: ButtonClickCmimi, keyUpKodi: keyPressCmimi, changefunction: changedCmimireshti, fokusi: pageState.kushte.F, lostFocusKoloneFundit: lostFocusKoloneFundit, shitje: shitje, hfFormatNumri: hfFormatNumri });
}

function myelemCmimiTvsh(value, options) {//po
    var disable = 'False';
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    //  var vleraLlojit = grida.getTekstQelize('cmbLloji', idRow);
    //if ((arrayReadOnlyKolonaGrides[14] == 'True') || !lejomod || vleraLlojit == "Llogari" || (hfTeDrejta.Get("NdryshoCmimShitje") == false && pageState.veprimi == 'shitje') || (hfTeDrejta.Get("NdryshoCmimBlerje") == false && pageState.veprimi == 'blerje'))
    //    disable = 'True';
    var shitje;
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar'))
        shitje = true;
    else shitje = false;
    return myJQGrid.myElemCmimiAutocomplete({ value: value, options: options, disabled: ((arrayReadOnlyKolonaGrides[40] == "True") || (readOnly(grida, idRow) ? "True" : "False")), idRreshti: idRow, id: 'txtCmimiTvsh', buttonClickKodi: ButtonClickCmimi, keyUpKodi: keyPressCmimi, changefunction: changedCmimireshtiTvsh, fokusi: pageState.kushte.F, lostFocusKoloneFundit: lostFocusKoloneFundit, shitje: shitje, hfFormatNumri: hfFormatNumri });
}

function myElemToolTip(rowId, cellValue, rowObject) {
    return ' title="' + cellValue + '"';
}

/*
Function: myelemCombo

Nderton combo-n Lloji per griden. Combo ka vlerat: Artikull, Makro, Llogari,  Text, Credit Note, Nentotali
*/
function myelemCombo(value) {
    var disabled = false;
    var objTmp;
    var arrayOptions = new Array(arrayMeLloje.ColKonfLlojRreshtiVlere.length);
    for (var i = 0; i < arrayMeLloje.ColKonfLlojRreshtiVlere.length; i++) {
        objTmp = new Object();
        objTmp.value = arrayMeLloje.ColKonfLlojRreshtiVlere[i].IdLlojRreshti.toString();
        objTmp.text = arrayMeLloje.ColKonfLlojRreshtiVlere[i].KodLlojRreshti.toString();
        arrayOptions[i] = objTmp;
    }
    if ((arrayReadOnlyKolonaGrides[1] == 'True') || !lejomod || cmbModeli.GetText() == "FBT" || cmbModeli.GetText() == "FST") {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, 'cmbLloji', $(pageState.gridaSelector).getLastSel2(), change, arrayOptions, disabled);
}

function KlientFurnitor() {
    if (regjistrimKF != 1)
        if (btnKlienti.GetSelectedItem() == null) {//.GetColumnText('KodKlientFurnitor') == "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniKlientFurnitorin"));
        }
}

function changell(kontrolli, idRreshti) {//po
    var grida = $(pageState.gridaSelector);
    if (typeof idRreshti == "undefined")
        idRreshti = grida.getLastSel2();

    var setDisabledZbritjaVlere = arrayReadOnlyKolonaGrides[36] == 'True' || !lejomod || grida.getTekstQelize('cmbLloji', idRreshti) == "Llogari" || !hfTeDrejta.Get("NdryshoZbritjeAnalitike");
    var setDisabledZbritjaPerqindje = arrayReadOnlyKolonaGrides[15] == 'True' || !lejomod || grida.getTekstQelize('cmbLloji', idRreshti) == "Llogari" || !hfTeDrejta.Get("NdryshoZbritjeAnalitike");

    if (grida.getTekstQelize('txtLlojZbritje', idRreshti) == 'Perqindje') {
        $('#txtZbritjaVlere' + idRreshti).attr('disabled', true);
        $('#txtZbritja' + idRreshti).attr('disabled', setDisabledZbritjaPerqindje);
    }
    else {
        $('#txtZbritjaVlere' + idRreshti).attr('disabled', setDisabledZbritjaVlere);
        $('#txtZbritja' + idRreshti).attr('disabled', true);
    }
}

function change() {//pot
    var idRow = $(pageState.gridaSelector).getLastSel2();
    resetRreshtKorent(idRow);
    enable(idRow);
}

/*
Function: enable

Ben enable/disable disa nga fushat e grides ne varesi te llojit te zgjedhur ne gride.
*/
function enable(idRow) {//po
    var grida = $(pageState.gridaSelector);
    var lloji = $('#cmbLloji' + idRow)[0];

    if (lloji !== undefined) {
        var vleraLlojit = grida.getTekstQelize('cmbLloji', idRow);
        var isReadOnly = readOnly(grida, idRow);
        switch (vleraLlojit) {
            case "Artikull":
                var kushtiPerCmimet = (hfTeDrejta.Get("NdryshoCmimShitje") == false && pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') || (hfTeDrejta.Get("NdryshoCmimBlerje") == false && pageState.veprimi == 'blerje');

                $('#txtCmimi' + idRow).attr('disabled', kushtiPerCmimet || arrayReadOnlyKolonaGrides[14] == "True" || isReadOnly);
                $('#fTvsh' + idRow).attr('disabled', kushtiPerCmimet || arrayReadOnlyKolonaGrides[40] == "True" || isReadOnly);
                $('#txtVleftaTVSH' + idRow).attr('disabled', kushtiPerCmimet || arrayReadOnlyKolonaGrides[16] == "True" || isReadOnly);
                $('#txtVlefta' + idRow).attr('disabled', kushtiPerCmimet || arrayReadOnlyKolonaGrides[18] == "True" || isReadOnly);
                $('#txtSasia' + idRow).attr('disabled', hfState.Get("MosModifikoTrup") || arrayReadOnlyKolonaGrides[13] == 'True');
                $('#txtGjeresi' + idRow).attr('disabled', arrayReadOnlyKolonaGrides[10] == 'True' || !lejomod);
                $('#txtGjatesi' + idRow).attr('disabled', arrayReadOnlyKolonaGrides[11] == 'True' || !lejomod);
                $('#txtSasiPermase' + idRow).attr('disabled', arrayReadOnlyKolonaGrides[12] == 'True' || !lejomod);

                $('#txtDetajimi' + idRow).attr('disabled', hfState.Get("MosModifikoTrup") ? false : arrayReadOnlyKolonaGrides[6] == 'True' || !lejomod);
                $('#txtDetajimi2t' + idRow).attr('disabled', hfState.Get("MosModifikoTrup") ? false : arrayReadOnlyKolonaGrides[7] == 'True' || !lejomod);
                $('#cmbNjesia' + idRow).attr('disabled', arrayReadOnlyKolonaGrides[8] == 'True' || !lejomod);
                return;
            case "Llogari":
                $('#txtCmimi' + idRow).attr('disabled', true);
                $('#txtCmimiTvsh' + idRow).attr('disabled', true);
                $('#txtSasia' + idRow).attr('disabled', true);
                $('#txtDetajimi' + idRow).attr('disabled', true);
                $('#txtDetajimi2t' + idRow).attr('disabled', true);
                $('#txtGjeresi' + idRow).attr('disabled', true);
                $('#txtGjatesi' + idRow).attr('disabled', true);
                $('#txtSasiPermase' + idRow).attr('disabled', true);
                $('#txtZbritja' + idRow).attr('disabled', true);
                $('#txtZbritjaVlere' + idRow).attr('disabled', true);
                $('#cmbNjesia' + idRow).attr('disabled', true);
                $('#txtVleftaTVSH' + idRow).attr('disabled', arrayReadOnlyKolonaGrides[16] == "True" || isReadOnly);
                $('#txtVlefta' + idRow).attr('disabled', arrayReadOnlyKolonaGrides[18] == "True" || isReadOnly);
                $('#' + idRow + '_txtSerialUnik').attr('disabled', true);
                return;
        }
    }
}


function myelemKategoriShpenzimi(value, options) {//po
    return myJQGrid.myElemKodi(value, options, (arrayReadOnlyKolonaGrides[38] == 'True') || !lejomod, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[38], ButtonClickKategoriShpenzimi, keyPressKategoriShpenzimi, changeFunc4, pageState.kushte.F, lostFocusKoloneFundit);
}

function myelemSeriali(value, options) {//po
    //var grida = $(pageState.gridaSelector);
    //var idRow = grida.getLastSel2();
    //return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[29] == 'True') || !lejomod, indexRow: idRow, id: "txtSerial", onFocusout: changeFuncSeriali, buttonClick: keyPressSeriali });
    return myJQGrid.myElemKodi(value, options, (arrayReadOnlyKolonaGrides[29] == 'True') || !lejomod, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[29], ButtonClickSeriali, keyPressSeriali, changeFuncSeriali);
}

function myelemIdKodi(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[2] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[2]);
}

function myelemIdTrupi(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[22] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[22]);
}

function myelemIdTrupiKonvertimi(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[23] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[23]);
}

function myelemIdTrupiTrasferimi(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[28] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[28]);
}

function myelemIdTrupiKthim(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[31] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[31]);
}

function myelemIdTrupiKonvertimBlerje(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[32] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[32]);
}

function myelemMagPershkrim(value, options) {
    var disabled = (arrayReadOnlyKolonaGrides[33] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[33]);
}

function myelemSasiLitra(value, options) {
    var disabled = (arrayReadOnlyKolonaGrides[34] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[34]);
}

function myelemIdTrupiRezervimi(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[26] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[26]);
}

function myelemRezervuar(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[25] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[25]);
}
function myElemLlogariKomisioni(value, options) {//po
    var disabled = (arrayReadOnlyKolonaGrides[41] == 'True') || !lejomod;
    return myJQGrid.myElemIdKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[41]);
}

function keyPressSeriali(e) {
    if (e.which == 13) {

        var grida = $(pageState.gridaSelector);
        var idRreshti = grida.getLastSel2();
        var serial = grida.getTekstQelize('txtSerial', idRreshti);
        var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);
        if (grida.getTekstQelize('cmbLloji', idRreshti) == "Llogari" || grida.getTekstQelize('txtKodi', idRreshti) == "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
            grida.setTekstQelize('txtSerial', idRreshti, "");
            return;
        }
        var artikulli = memoryArt.Get(idkodi);
        if (grida.getTekstQelize('cmbLloji', idRreshti) === "Artikull" && artikulli) {
            if (!artikulli.LlojiArt) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
                grida.setTekstQelize('txtSerial', idRreshti, "");
                return;
            }
        }
        if (serial != "" && serial != "Me serial" && serial != "Pa serial") {
            var gridIds = grida.jqGrid('getDataIDs');
            var arrayMeSeriale = new Array();
            var a = 0;
            var sasishumaperserial = 0;
            for (i = 0; i < gridIds.length; i++) {
                if (idRreshti == gridIds[i])
                    continue;
                if (hfSeriale.Contains(idkodi + '_' + gridIds[i])) {
                    arrayMeSeriale[a] = hfSeriale.Get(idkodi + '_' + gridIds[i]);
                    if (JSON.parse(arrayMeSeriale[a])[0].AqtSerialKod == serial)//per serialet e ndashem kemi vetem nje serial per resht dhe marrim sasine
                        sasishumaperserial += hfSasiSeriale.Get(idkodi + '_' + gridIds[i]);
                    a++;
                }
            }
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "CelSerialNqsNukEkziston"),
                data: JSON.stringify({ kodserial: serial, idndermarje: pageState.idNdermarrje, idperdoruesi: pageState.idPerdoruesi, idartikulli: idkodi, lastsel: idRreshti, serialeekzistuese: hfSeriale.Contains(idkodi + '_' + idRreshti) ? hfSeriale.Get(idkodi + '_' + idRreshti) : "", serialeteperdoruraNeKeteFature: arrayMeSeriale, celnqsnukekziston: (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? false : true, magazina: grida.getTekstQelize('txtMagazina', idRreshti), sasishuma: sasishumaperserial, iddokmodmagazine: $('#hfIdMag').val() })
            }).done(SucceededCallbackSerial);
        }
        grida.setTekstQelize('txtSerial', idRreshti, "");
    }
}

function SucceededCallbackSerial(result) {
    if (result[2] != "") {
        myMesazh.ShtoMesazhGabimi(result[2]);
        return;
    }
    var artikulli = memoryArt.Get(result[0]);
    var grida = $(pageState.gridaSelector);
    if (artikulli.MeSerial)
        grida.setTekstQelize('txtSasia', result[4], result[3]);
    changedSasia();
    if (hfSeriale.Contains(result[0] + '_' + result[4])) {
        hfSeriale.Set(result[0] + '_' + result[4], result[1]);
    }
    else hfSeriale.Add(result[0] + '_' + result[4], result[1]);
    if (hfSasiSeriale.Contains(result[0] + '_' + result[4])) {
        hfSasiSeriale.Set(result[0] + '_' + result[4], artikulli.MeSerial ? 1 : grida.getTekstQelize('txtSasia', result[4]));
    }
    else hfSasiSeriale.Add(result[0] + '_' + result[4], artikulli.MeSerial ? 1 : grida.getTekstQelize('txtSasia', result[4]));
}

function changeFuncSeriali() {
    var grida = $(pageState.gridaSelector);
    var idRreshti = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);
    if (hfSeriale.Contains(idkodi + '_' + idRreshti) && hfSeriale.Get(idkodi + '_' + idRreshti) != "[]")
        grida.setTekstQelize('txtSerial', idRreshti, "Me serial");
    else grida.setTekstQelize('txtSerial', idRreshti, "Pa serial");
}

function ButtonClickSeriali() {
    var grida = $(pageState.gridaSelector);
    var idRreshti = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);
    if (grida.getTekstQelize('txtKodi', idRreshti) == "" || grida.getTekstQelize('cmbLloji', idRreshti) == "Llogari") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
        return;
    }
    var artikulli = memoryArt.Get(idkodi);
    if (grida.getTekstQelize('cmbLloji', idRreshti) === "Artikull" && artikulli) {
        if (!artikulli.LlojiArt) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
            return;
        }
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniSerialet"), 'LupaSeriale.aspx?vjennga=' + pageState.veprimi + '&magazina=' + grida.getTekstQelize('txtMagazina', idRreshti) + '&id=' + grida.getTekstQelize('txtIdKodi', idRreshti) + '&lastsel=' + idRreshti + '&mecope=' + !artikulli.MeSerial + '&sasia=' + (artikulli.MeSerial ? grida.getTekstQelize('txtSasia', idRreshti) : 1) + '&data=' + data_DateEdit.GetText() + '&iddokmag=' + $('#hfIdMag').val(), 900, 600);
    }
}

/*
Function: keyPressKodi

Shton nje rresht te ri ne gride (nese nuk ka) dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressKodi(event, idRreshti) {
    var idRow = $(pageState.gridaSelector).getLastSel2();
    if (typeof idRreshti == "undefined")
        idRreshti = idRow;
    var vlera = event.target.value;
    if (vlera == "" && idRreshti == idRow) {
        $('#' + arrayIdKolonaGrides[3] + idRreshti).autocomplete("close");
    }
    else
        callWebserviceKodi(vlera, idRreshti);
}

function keyPressMagazina(event, idRreshti) {
    var idRow = $(pageState.gridaSelector).getLastSel2();
    if (typeof idRreshti == "undefined")
        idRreshti = idRow;
    var vlera = event.target.value;
    if (vlera == "" && idRreshti == idRow)
        $('#' + arrayIdKolonaGrides[9] + idRreshti).autocomplete("close");
    else
        callWebserviceMagazina(vlera, idRreshti);
}

function keyPressKategoriShpenzimi(event, idRreshti) {
    var idRow = $(pageState.gridaSelector).getLastSel2();
    if (typeof idRreshti == "undefined")
        idRreshti = idRow;
    var vlera = event.target.value;
    if (vlera == "" && idRreshti == idRow) {
        $('#' + arrayIdKolonaGrides[38] + idRreshti).autocomplete("close");
    }
    else
        callWebserviceKategoriShpenzimi(vlera, idRreshti);
}
/*
Function: keyPressKodi

Shton nje rresht te ri ne gride (nese nuk ka) dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressLlogariShpenzimi(event) {
    var idRow = $(pageState.gridaSelector).getLastSel2();
    try {
        var vlera = event.target.value;
        if (vlera == "")
            $('#' + arrayIdKolonaGrides[3] + idRow).autocomplete("close");
        else {
            try {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish"),
                    data: JSON.stringify({ infixText: vlera, pershk: pershk, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
                }).done(function (result) { SucceededCallbackLlogariShpenzimi(result, idRow); });
            }
            catch (e) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
            }
        }
    }
    catch (e) { console.error(e.toString()); }
}

/*
Function: keyPressKodi

Shton nje rresht te ri ne gride (nese nuk ka) dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressCmimi(event, idRow, id) {     //po
    var grida = $(pageState.gridaSelector);
    var vlera = event.target.value;
    if (vlera == "") {
        grida.setTekstQelize(id, idRow);
        $("#" + id).focus();
        $("#" + id).autocomplete("close");
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetCmimZeroNeGride"));
    }
}

function myValueButtonFshi(elem, operation, value) {//po
    var idRow = $(pageState.gridaSelector).getLastSel2();
    var disabled = (hfState.Get("MosModifikoTrup") || arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && $("input[id$='hfShtimModifikim']").val() == "konvertim"));

    return myJQGrid.myValueButtonFshi(disabled, idRow, "#rowed5");
}

function myValueButtonSerialeUnike(elem, operation, value) {
    var idRow = $(pageState.gridaSelector).getLastSel2();
    var disabled = arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True';
    return myJQGrid.myValueButtonSerialeUnike(true, idRow);
}

/*
Function: myelemDtFillimi

Nderton nje dateedit ku vendoset data
*/
function myelemDtFillimi(value) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    if (value == "")
        value = data_DateEdit.GetText();
    //var hf = document.getElementById("hfShtimModifikim");
    disabled = (arrayReadOnlyKolonaGrides[20] == 'True') || !lejomod;

    if (pageState.lloji == "modifikim" && grida.getTekstQelize('txtKodi', idRow) !== '')
        disabled = 'True';
    return myJQGrid.myelemData(value, disabled, idRow, 'txtDtFillimi', undefined, onchanged);
}

/*
Function: myelemDtMbarimi

Nderton nje dateedit ku vendoset data
*/
function myelemDtMbarimi(value) {
    if (value == "")
        value = data_DateEdit.GetText();
    //var hf = document.getElementById("hfShtimModifikim");
    disabled = (arrayReadOnlyKolonaGrides[21] == 'True') || !lejomod;
    var grida = $(pageState.gridaSelector);
    var idRreshti = grida.getLastSel2();
    if (pageState.lloji == "modifikim" && grida.getTekstQelize('txtKodi', idRreshti) !== '')
        disabled = 'True';
    return myJQGrid.myelemData(value, disabled, idRreshti, 'txtDtMbarimi', undefined, onchanged);
}

function onchanged() {
    try {
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

/*
Function: myElemButon

Nderton nje buton per te fshire nje rresht te grides
*/
function myElemButtonFshi() {//po
    var idRreshti = $(pageState.gridaSelector).getLastSel2();
    callWebServiceInfoRow(idRreshti);
    changell(null, idRreshti);
    var disabled = hfState.Get("MosModifikoTrup") || arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && $("input[id$='hfShtimModifikim']").val() == "konvertim");

    return myJQGrid.myElemButtonFshi(disabled, idRreshti, "#rowed5", lostFocusKoloneFundit);

}

function myElemButtonSerialeUnike() {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRow);
    var idkodi = grida.getTekstQelize('txtIdKodi', idRow);
    var art = memoryArt.Get(idkodi);
    var disabled = vleraLlojit == "Llogari" || (!art || art && art.Klasa != 4) || (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && Utils.getUrlVar("kthehu") == "kthehu");
    return myJQGrid.myElemButtonSerialetUnike(disabled, idRow);
}

function PercaktoButtonSerialeUnikeEnabled(klasa) {
    return (!klasa || (klasa && klasa != 4));
}

function ButtonSerialeUnikeClicked(index, event) {
    event.preventDefault();
    var grida = $(pageState.gridaSelector);
    var data = grida.getTeDhenaRreshti(index);
    var rreshti = (data.length > 0) ? data[0] : data;
    HapLupeSerialeshUnike(rreshti.txtIdKodi);
}

/*
Function: myElemPershkrimi

Nderton nje textbox per te vendosur emertimin (e artikullit, llogarise etj) sipas zgjedhjes se bere te lloji dhe kodi.
*/
function myElemPershkrimi(value) {//po
    disabled = (arrayReadOnlyKolonaGrides[5] == 'True') || !lejomod;
    return myJQGrid.myElemEmertimGjate(value, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[5]);
}
function myElemPershkrimiKategoria(value) {//po
    disabled = (arrayReadOnlyKolonaGrides[39] == 'True') || !lejomod;
    return myJQGrid.myElemEmertimGjate(value, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[39]);
}

/*
Function: myElemNrRendor

Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendor(value, options) {//po
    var grida = $("#rowed5");
    disabled = (arrayReadOnlyKolonaGrides[0] == 'True') || !lejomod;
    return myJQGrid.myElemNrRendor(value, options, $(pageState.gridaSelector).getLastSel2(), 'txtNrRendor', grida);
}

/*
Function: myElemShenime
Nderton nje textbox per te vendosur shenime tek rreshtat e grides.
*/
function myElemShenime(value) {//po
    disabled = (arrayReadOnlyKolonaGrides[19] == 'True') || !lejomod;
    return myJQGrid.myElemEmertimi(value, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[19], pageState.kushte.RSHTTK ? changeShenime : null);
}

/*
Function: myElemShenime
Nderton nje textbox per te vendosur shenime tek rreshtat e grides.
*/
function myElemShenime2(value) {//po
    disabled = (arrayReadOnlyKolonaGrides[35] == 'True') || !lejomod;

    return myJQGrid.myElemEmertimi(value, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[35], pageState.kushte.RSHTTK ? changeShenime : null);
}

function myElemKodbari(value) {//po
    disabled = (arrayReadOnlyKolonaGrides[4] == 'True') || !lejomod;
    return myJQGrid.myElemEmertimi(value, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[4]);
}

/*
 * Perdoret si ne change te shenimeve si ne fund per te kontrolluar saktesine
 * @param {} idElem - nese eshte null eshte rasti ne fund
 * @param {} idRreshti - id e rreshtit qe po analizohet
 * @returns {} true apo false nese eshte valid change-i - nderkohe lajmeron perdoruesin me mesazhe
 */
function changeShenime(idElem, idRreshti) {
    var grida = $(pageState.gridaSelector);
    var kodArt = grida.getTekstQelize(arrayIdKolonaGrides[3], idRreshti);
    var lloji = grida.getTekstQelize("cmbLloji", idRreshti);
    if (kodArt == "" || lloji != "Artikull")
        return true;
    var isShenime1 = arrayIdKolonaGrides[19] == idElem ? true : false;
    var shenime1 = grida.getTekstQelize(arrayIdKolonaGrides[19], idRreshti);
    var shenime2 = grida.getTekstQelize(arrayIdKolonaGrides[35], idRreshti);
    var changedShenime = isShenime1 ? shenime1 : shenime2;
    var validShenime = (isShenime1 ? "shenime1" : "shenime2");
    var mySerials = pageState.kastratiTollona.seriale[kodArt];
    var serialNumber1 = pageState.kastratiTollona.getSerialNumber(shenime1, mySerials);
    var serialNumber2 = pageState.kastratiTollona.getSerialNumber(shenime2, mySerials);

    var validSerialNumber = (isShenime1 ? serialNumber1 : serialNumber2);
    var suggestedSasi = parseInt(serialNumber2) - parseInt(serialNumber1) + 1;
    var nrRreshti = grida.getTekstQelize(arrayIdKolonaGrides[0], idRreshti);
    if (idElem) {
        if (changedShenime.length != pageState.kastratiTollona.gjatesi || !validSerialNumber) {
            myMesazh.ShtoMesazhGabimi("Seriali i tollonit ne " + validShenime + ": " + changedShenime + " per rreshtin: " + nrRreshti + " nuk eshte i sakte!!!");
            return false;
        }
        if (!isNaN(suggestedSasi) && suggestedSasi > 0) {
            grida.setTekstQelize(arrayIdKolonaGrides[13], idRreshti, suggestedSasi);
            changedSasia(null, idRreshti);
        }
        return true;
    }
    if (shenime1.length != pageState.kastratiTollona.gjatesi || !serialNumber1) {
        myMesazh.ShtoMesazhGabimi("Seriali i tollonit ne shenime1 " + shenime1 + " per rreshtin: " + nrRreshti + " nuk eshte i sakte!!!");
        return false;
    }
    if (shenime2.length != pageState.kastratiTollona.gjatesi || !serialNumber2) {
        myMesazh.ShtoMesazhGabimi("Seriali i tollonit ne shenime2 " + shenime2 + " per rreshtin: " + nrRreshti + " nuk eshte i sakte!!!");
        return false;
    }
    if (Math.abs(suggestedSasi) != Math.abs(parseInt(grida.getTekstQelize(arrayIdKolonaGrides[13], idRreshti)))) {
        myMesazh.ShtoMesazhGabimi("Shuma e serialeve ne rreshtin: " + nrRreshti + " nuk eshte e sakte!!!");
        return false;
    }
    return true;
}

/*
Function: myElemDetajimi

Nderton nje textbox dhe nje buton per te zgjedhur detajimet e artikullit.
*/
function myElemDetajimi(value, options) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRow);
    var disabled = (arrayReadOnlyKolonaGrides[6] == 'True') || !lejomod || vleraLlojit == "Llogari";
    if (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && Utils.getUrlVar("kthehu") == "kthehu")
        disabled = 'True';
    else if (hfState.Get("MosModifikoTrup"))
        disabled = 'False';
    return myJQGrid.myElemKodi(value, options, disabled, idRow, arrayIdKolonaGrides[6], ButtonClickDetajimet, keyPressDet, changeFunc2, pageState.kushte.F, lostFocusKoloneFundit);
}

function myElemDetajimi2(value, options) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRow);
    disabled = (arrayReadOnlyKolonaGrides[7] == 'True') || !lejomod || vleraLlojit == "Llogari";
    if (hfState.Get("MosModifikoTrup"))
        disabled = false;
    return myJQGrid.myElemKodi(value, options, disabled, idRow, arrayIdKolonaGrides[7], ButtonClickDetajimet2, keyPressDet2, changeFunc3, pageState.kushte.F, lostFocusKoloneFundit);
}

function myelemLlogShpenz(value, options, idRreshti) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    if (idRreshti === undefined)
        idRreshti = idRow;

    disabled = (arrayReadOnlyKolonaGrides[30] == 'True') || !lejomod;
    return myJQGrid.myElemKodi(value, options, disabled, idRreshti, 'txtIdLlogShpenzimi', ButtonClickLlogShpenzimi, keyPressLlogariShpenzimi, changeFuncLlogShpenzimi, pageState.kushte.F, lostFocusKoloneFundit);
}

/*
Function: myelemGjeresia

Nderton nje textbox per te vendosur sasine.
*/
function myelemGjeresia(value, options) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[10] == 'True') || !lejomod || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" ? "True" : "False", indexRow: idRow, id: "txtGjeresi", changed: changedGjeresiGjatesiSasiPermase, onFocusout: focusoutSasia });
}

/*
Function: myelemGjatesia

Nderton nje textbox per te vendosur sasine.
*/
function myelemGjatesia(value, options) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[11] == 'True') || !lejomod || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" ? "True" : "False", indexRow: idRow, id: "txtGjatesi", changed: changedGjeresiGjatesiSasiPermase, onFocusout: focusoutSasia });
}

function myelemSasiPermasa(value, options) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[12] == 'True') || !lejomod || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" ? "True" : "False", indexRow: idRow, id: "txtSasiPermase", changed: changedGjeresiGjatesiSasiPermase, onFocusout: focusoutSasia });
}

/*
Function: myelemSasia

Nderton nje textbox per te vendosur sasine.
*/
function myelemSasia(value, options) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (hfState.Get("MosModifikoTrup") || arrayReadOnlyKolonaGrides[13] == 'True') || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" ? "True" : "False", indexRow: idRow, id: "txtSasia", changed: changedSasia, onFocusout: focusoutSasia });
}

function myelemSasiaMbetur(value, options) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[27] == 'True') || !lejomod || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" ? "True" : "False", indexRow: idRow, id: "txtSasiMbetur", onKeyDown: changedSasia, onFocusout: focusoutSasia });
}

function myelemSasiaRez(value, options) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[24] == 'True') || !lejomod || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" ? "True" : "False", indexRow: idRow, id: "txtSasiaRez", onKeyDown: changedSasiaRez, onFocusout: focusoutSasiaRez });
}
function focusoutSasiaRez() {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var sasiaRez = grida.getTekstQelize('txtSasiaRez', idRow);
    if (sasiaRez === "" || isNaN(sasiaRez))
        grida.setTekstQelize('txtSasiaRez', idRow);
}
function changedSasiaRez() {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    editorSasia = $("#" + 'txtSasia' + idRow);
    editorSasiaRez = $("#" + 'txtSasiaRez' + idRow);
    var sasia;
    if (editorSasiaRez.val() == "" || isNaN(editorSasiaRez.val())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaRezervimitDuhetNumer"));
        editorSasiaRez.focus();
    }
    if (sasia == null)
        sasia = grida.getTekstQelize('txtSasiaRez', idRow);
    if (grida.getTekstQelize('txtSasiaRez', idRow) > grida.getTekstQelize('txtSasia', idRow)) {
        grida.setTekstQelize('txtSasiaRez', idRow, grida.getTekstQelize('txtSasia', idRow));
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaRezervimitNukDuhetMeEMadheSeSasiaNeFature"));
    }
}
function myelemZbritja(value, options) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[15] == 'True') || !lejomod || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" || hfTeDrejta.Get("NdryshoZbritjeAnalitike") == false ? "True" : "False", indexRow: idRow, id: "txtZbritja", onKeyDown: changedZbritjaReshti });
}
function myelemZbritjaVlere(value, options) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: (arrayReadOnlyKolonaGrides[36] == 'True') || !lejomod || grida.getTekstQelize('cmbLloji', idRow) == "Llogari" || hfTeDrejta.Get("NdryshoZbritjeAnalitike") == false ? "True" : "False", indexRow: idRow, id: "txtZbritjaVlere", onKeyDown: changedZbritjaReshti });
}
function myelemVlefta(value, options) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: ((arrayReadOnlyKolonaGrides[16] == "True") || (readOnly(grida, idRow) ? "True" : "False")), indexRow: idRow, id: "txtVleftaTVSH", onKeyDown: changedVlefta, onFocusout: focusoutVlefta });
}
function myelemVleftaTVSH(value, options) {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: ((arrayReadOnlyKolonaGrides[18] == "True") || (readOnly(grida, idRow) ? "True" : "False")), indexRow: idRow, id: "txtVlefta", onKeyDown: changedVleftaTVSH, onFocusout: focusoutVleftaTvsh });
}

function readOnly(grida, idRow) {
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRow);
    return !lejomod || (vleraLlojit !== "Llogari" && (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && hfTeDrejta.Get("NdryshoCmimShitje") == false) || (vleraLlojit !== "Llogari" && pageState.veprimi == 'blerje' && hfTeDrejta.Get("NdryshoCmimBlerje") == false);
}

function kontrolloRow(rowid, artikulli) {
    if (konfirmimArt != 1 && pageState.kushte.ZADHG)
        return true;
    var grida = $(pageState.gridaSelector);
    var lloji = grida.getTekstQelize('cmbLloji', rowid);
    var name = grida.getTekstQelize('txtKodi', rowid);
    var njesia = grida.getTekstQelize('cmbNjesia', rowid);
    var sasia = grida.getTekstQelize('txtSasia', rowid);
    var detajim1 = grida.getTekstQelize('txtDetajimi', rowid);
    var detajim2 = grida.getTekstQelize('txtDetajimi2t', rowid);
    var gridIds = grida.jqGrid('getDataIDs');
    for (i = 0; i < gridIds.length; i++) {
        if (rowid == gridIds[i])
            continue;
        var editorkodi = grida.getTekstQelize('txtKodi', gridIds[i]);
        var editorlloji = grida.getTekstQelize('cmbLloji', gridIds[i]);
        var editornjesia = grida.getTekstQelize('cmbNjesia', gridIds[i]);
        var editordetajimi1 = grida.getTekstQelize('txtDetajimi', gridIds[i]);
        var editordetajimi2 = grida.getTekstQelize('txtDetajimi2t', gridIds[i]);
        if (editorkodi == name && editordetajimi1 == detajim1 && editordetajimi2 == detajim2 && editorkodi != "" && editorlloji == lloji && editornjesia == njesia) {
            if (pageState.kushte.ZADHG == false) {
                kaloNeRreshtinERiTeDhenat(grida, rowid, gridIds[i], sasia, artikulli);
                resetRreshtKorent(gridIds[i]);
                updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
                return false;
            }
            if (konfirmimArt == 1) {
                myMesazh.vendosClient();
                identifikuesPyetje = "artikulli";
                rreshtidyfish = rowid;
                myMesazh.ShtoPyetje(hfState.Get("msgEkzistonArtikullNeGride"), true);
                return true;
            }
        }
    }
    return true;
}

function kaloNeRreshtinERiTeDhenat(grida, rowid, indeksiFillestar, sasia, artikulli) {
    grida.setTekstQelize('txtSasia', rowid, parseFloat(grida.getTekstQelize('txtSasia', indeksiFillestar)) + parseFloat(sasia));
    grida.setTekstQelize('txtGjeresi', rowid, parseFloat(grida.getTekstQelize('txtGjeresi', indeksiFillestar)));
    grida.setTekstQelize('txtGjatesi', rowid, parseFloat(grida.getTekstQelize('txtGjatesi', indeksiFillestar)));
    grida.setTekstQelize('txtSasiaRez', rowid, parseFloat(grida.getTekstQelize('txtSasiaRez', indeksiFillestar)));
    grida.setTekstQelize('txtSasiPermase', rowid, parseFloat(grida.getTekstQelize('txtSasiPermase', indeksiFillestar)));

    grida.setTekstQelize('txtCmimi', rowid, parseFloat(grida.getTekstQelize('txtCmimi', indeksiFillestar)));
    grida.setTekstQelize('cbTVSH', rowid, grida.getTekstQelize('cbTVSH', indeksiFillestar), null, null, myelemComboTVSHSup);

    grida.setTekstQelize('txtMagazina', rowid, grida.getTekstQelize('txtMagazina', indeksiFillestar));
    grida.vendosTeDhenaPerQelizen('txtMagazina', rowid, "IshMagazina", grida.getTekstQelize('txtMagazina', indeksiFillestar));
    grida.setTekstQelize('txtDtFillimi', rowid, grida.getTekstQelize('txtDtFillimi', indeksiFillestar));
    grida.setTekstQelize('txtDtMbarimi', rowid, grida.getTekstQelize('txtDtMbarimi', indeksiFillestar));
    grida.setTekstQelize('txtSerial', rowid, grida.getTekstQelize('txtSerial', indeksiFillestar));
    grida.setTekstQelize('txtIdLlogShpenzimi', rowid, grida.getTekstQelize('txtIdLlogShpenzimi', indeksiFillestar));
    grida.setTekstQelize('txtPershkrimmag', rowid, grida.getTekstQelize('txtPershkrimmag', indeksiFillestar));
    grida.setTekstQelize('txtCmimiTvsh', rowid, parseFloat(grida.getTekstQelize('txtCmimiTvsh', indeksiFillestar)));
    grida.setTekstQelize('txtDetajimi', rowid, grida.getTekstQelize('txtDetajimi', indeksiFillestar));
    grida.setTekstQelize('txtDetajimi2t', rowid, grida.getTekstQelize('txtDetajimi2t', indeksiFillestar));
    grida.setTekstQelize('txtShenime', rowid, grida.getTekstQelize('txtShenime', indeksiFillestar));
    grida.setTekstQelize('txtShenime2', rowid, grida.getTekstQelize('txtShenime2', indeksiFillestar));
    grida.setTekstQelize('txtPershkrimiKatShpenzimi', rowid, grida.getTekstQelize('txtPershkrimiKatShpenzimi', indeksiFillestar));
    grida.setTekstQelize('txtKategoriShpenzimi', rowid, grida.getTekstQelize('txtKategoriShpenzimi', indeksiFillestar));
    grida.setTekstQelize('txtPershkrimiKatShpenzimi', rowid, grida.getTekstQelize('txtPershkrimiKatShpenzimi', indeksiFillestar));
    grida.setTekstQelize('txtPershkrimi', rowid, grida.getTekstQelize('txtPershkrimi', indeksiFillestar));

    if (grida.getTekstQelize('txtLlojZbritje', indeksiFillestar) == 'Vlere') {
        grida.setTekstQelize("txtLlojZbritje", rowid, "Vlere", null, null, myelemComboZbritja);
        grida.setTekstQelize('txtZbritjaVlere', rowid, parseFloat(grida.getTekstQelize('txtZbritjaVlere', indeksiFillestar)));
    }
    else {
        grida.setTekstQelize("txtLlojZbritje", rowid, "Perqindje", null, null, myelemComboZbritja);
        grida.setTekstQelize('txtZbritja', rowid, parseFloat(grida.getTekstQelize('txtZbritja', indeksiFillestar)));
    }

    changell(null, rowid);
    memoryArt.Set(artikulli);

    if (gjendjeartminmax && grida.getTekstQelize('cmbLloji', rowid) != "Llogari") {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
            data: JSON.stringify({ idartikulli: artikulli.IdArtikulli, mag: grida.getTekstQelize('txtMagazina', rowid), idNdermarrje: pageState.idNdermarrje })
        }).done(function (result) { SucceededCallbackGjendjeArtikulli(result, artikulli); });
    }
    changedCmimireshti(null, null, 'txtCmimi', rowid);
}

function fshiClicked(index) {//po
    var grida = $(pageState.gridaSelector);
    if (grida.getTekstQelize('cmbLloji', index) == 'Artikull') {
        var idkodi = grida.getTekstQelize('txtIdKodi', index);
        if (hfSeriale.Contains(idkodi + '_' + index))
            hfSeriale.Remove(idkodi + '_' + index);
        if (hfSasiSeriale.Contains(idkodi + '_' + index))
            hfSasiSeriale.Remove(idkodi + '_' + index);
        var art = memoryArt.Get(idkodi);
        if (art != undefined && (art.IdFormatSeriali != 0 || art.Klasa == 4))
            FshiSeriale(idkodi, grida.getTekstQelize("txtMagazina", index));
    }
    grida.vendosTeDhenaPerQelizen("txtKodi", index, "IdLlogari", "");
    grida.vendosTeDhenaPerQelizen("txtKodi", idRow, "MeKomision", false);
    grida.vendosTeDhenaPerQelizen("txtKodi", idRow, "ZbritjaKlientit", 0);
    grida.rregulloNrRendorMeTeMadh(index);
    myJQGrid.fshiClicked(index, pageState.gridaSelector, inicializoGride);
    var idRow = grida.getLastSel2();
    ShtoRreshtaKomision();
    updateTotalet(txtPerqindje.GetText(), txtVlefte.GetText());
}

function FshiSeriale(idArtikulli, mag) {
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("SerialeUnike", "HiqSerialetPerArtikullDheMagazine"),
        data: JSON.stringify({ idArtikulli: idArtikulli, mag: pageState.veprimi == "blerje" ? "" : mag, guidString: pageState.guidString, idNdermarrje: pageState.idNdermarrje, Set: false })
    }).done(function (result) {
        kaSeriale = result;
    });
}

function myelemComboZbritja(value, options, idRreshti, replace) {//po
    if (replace === undefined)
        replace = false;
    var disabled = arrayReadOnlyKolonaGrides[37];
    var arrayOptions = new Array();
    if (idRreshti === undefined)
        idRreshti = $(pageState.gridaSelector).getLastSel2();
    arrayOptions.push({ value: Utils.llojZbritje.Perqindje, text: "Perqindje" });
    arrayOptions.push({ value: Utils.llojZbritje.Vlere, text: "Vlere" });
    if (value == "") {
        //if (pageState.kushte.PLLZD)
        value = pageState.kushte.PLLZD;
        //else value = "Vlere";
    }
    var myCombo = myJQGrid.getMyCombo($(pageState.gridaSelector), "txtLlojZbritje", idRreshti, arrayOptions, value, replace, disabled, changell);
    if (myCombo)
        return myCombo;
    return myJQGrid.myElemCombo(value, "txtLlojZbritje", idRreshti, changell, arrayOptions, disabled);
}
function myelemComboTVSHSup(value, options, idRreshti, replace) {//po
    if (replace === undefined)
        replace = false;
    var valueNew = value;
    var grida = $(pageState.gridaSelector);
    if (idRreshti === undefined)
        idRreshti = grida.getLastSel2();

    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRreshti);
    var vleraKodit = grida.getTekstQelize('txtKodi', idRreshti);
    var idKodi = grida.getTekstQelize('txtIdKodi', idRreshti);
    var disabled;
    if (dogana === 2)
        disabled = (arrayReadOnlyKolonaGrides[17] == 'True') || !lejomod;
    else {
        disabled = true;
        value = 'Pa TVSH';
    }
    if (vleraLlojit === "Artikull") {
        var artikulli = memoryArt.Get(idKodi);
        if (artikulli) {
            if (replace && valueNew != '' && valueNew != 'Pa TVSH') {
                var indexchange = 0;
                for (var a = 0; a < artikulli.listeTvsh.length; a++) {
                    if (artikulli.listeTvsh[a].text == valueNew) {
                        indexchange = a;
                        break;
                    }
                }
                var exhange = artikulli.listeTvsh[0];
                artikulli.listeTvsh[0] = artikulli.listeTvsh[indexchange];
                artikulli.listeTvsh[indexchange] = exhange;

            }

            var tvshComboArt = myJQGrid.getMyCombo(grida, "cbTVSH", idRreshti, artikulli.listeTvsh, value, replace, disabled, changedTVSH);
            if (tvshComboArt)
                return tvshComboArt;
        }
    }
    if (vleraLlojit === "Llogari") {
        var llogaria = memoryLlog.Get(idKodi);
        if (llogaria) {
            if (replace && valueNew != '' && valueNew != 'Pa TVSH') {
                var indexchange = 0;
                for (var a = 0; a < llogaria.listeTvsh.length; a++) {
                    if (llogaria.listeTvsh[a].text == valueNew) {
                        indexchange = a;
                        break;
                    }
                }
                var exhange = llogaria.listeTvsh[0];
                llogaria.listeTvsh[0] = llogaria.listeTvsh[indexchange];
                llogaria.listeTvsh[indexchange] = exhange;

            }
            var tvshComboLlog = myJQGrid.getMyCombo(grida, "cbTVSH", idRreshti, llogaria.listeTvsh, value, replace, disabled, changedTVSH);
            if (tvshComboLlog)
                return tvshComboLlog;
        }
    }
    var objTmp = new Object();
    objTmp.value = -1;
    objTmp.text = "Pa TVSH";
    objTmp.norma = 0;
    var arrayOptions = new Array(1);
    arrayOptions[0] = objTmp;
    vendosVlereKomisioniSipasArtikullitAndKlientit(pageState.Klient, idRreshti, grida);
    ShtoRreshtaKomision();
    return myJQGrid.myElemCombo(value, 'cbTVSH', idRreshti, changedTVSH, arrayOptions, true);
}
function myelemComboNjesiaSup(value, options, idRreshti, artikulli) {

    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    if (idRreshti === undefined)
        idRreshti = idRow;

    var lloji = grida.getTekstQelize('cmbLloji', idRreshti);
    var idArt = grida.getTekstQelize('txtIdKodi', idRreshti);
    var arrayOptions;
    if (!artikulli) artikulli = memoryArt.Get(idArt);
    if (lloji === "Artikull" && artikulli) {// && memoryArt.Contains(idArt)) {
        //var artikulli = memoryArt.Get(idArt);
        arrayOptions = grida.formoArrayOptinosNjesia(artikulli);
        if (value === '')
            value = arrayOptions[0].text;
        if (idRreshti == idRow)
            return myJQGrid.myElemCombo(value, 'cmbNjesia', idRreshti, updateCmimiRowKorrent, arrayOptions, (arrayReadOnlyKolonaGrides[8] == 'True') || !lejomod);
        else {
            grida.jqGrid('setCell', idRreshti, 'cmbNjesia', value, '', '', disabled);
            return arrayOptions[0];
        }
    }
    var objNjesi = new Object();
    objNjesi.value = "";
    objNjesi.text = "";
    arrayOptions = new Array(1);
    arrayOptions[0] = objNjesi;
    var kodi = grida.getTekstQelize('txtKodi', idRreshti);
    if (lloji === "Artikull" && kodi !== "")
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheNjesiArtikulliComboMeKodRow"), data: JSON.stringify({ kodi: kodi, index: idRreshti, textNjesiZgjedhur: value, idNdermarrje: pageState.idNdermarrje }) }).done(function (result) { SucceededCallbackComboNjesiArtikulli(result, idRreshti); });

    return myJQGrid.myElemCombo(value, 'cmbNjesia', idRreshti, updateCmimiRowKorrent, arrayOptions, true);
}

/*
Function: myelemComboMagazina

nderton nje combo per magazinat
*/
function myelemComboMagazina(value, options, index) {//po
    var grida = $("#rowed5");
    if (index == undefined)
        index = grida.getLastSel2();
    var disabled = (arrayReadOnlyKolonaGrides[9] == 'True') || !lejomod;
    return myJQGrid.myElemKodi(value, options, disabled, index, arrayIdKolonaGrides[9], ButtonClickMagazinaTrupi, keyPressMagazina, changeFuncMag, lostFocusKoloneFundit);
}


/*
Function: myelemKodi

Nderton nje textbox dhe nje buton per te zgjedhur artikujt, llogarite, makrot etj sipas llojit te zgjedhur tek combo Lloji.
*/
function myelemKodi(value, options) {//po
    var grida = $("#rowed5");
    var index = grida.getLastSel2();
    var disabled = (arrayReadOnlyKolonaGrides[3] == 'True') || !lejomod;
    if (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && !hfState.Get("RoliSR") && index == 1)
        disabled = false;

    return myJQGrid.myElemKodi(value, options, disabled, $(pageState.gridaSelector).getLastSel2(), arrayIdKolonaGrides[3], ButtonClickKodi, keyPressKodi, changeFunc, pageState.kushte.F, lostFocusKoloneFundit);
}

function SucceededCallbackComboNjesiArtikulli(result, idRreshti) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();

    var textzgjedhur = result[1];
    comboListNjesiArt = result[2];
    if (idRreshti == idRow && $('#' + 'cmbNjesia' + idRow).val() != undefined)
        $('#' + 'cmbNjesia' + idRreshti).replaceWith(myJQGrid.myElemCombo(textzgjedhur, 'cmbNjesia', idRreshti, updateCmimiRowKorrent, comboListNjesiArt, false).children()[0]);
    else {
        grida.setTekstQelize('cmbNjesia', idRreshti, textzgjedhur);
    }
}

function updateCmimiGrid() {
    var grida = $(pageState.gridaSelector);
    var rreshtaGride = grida.jqGrid('getRowData');
    var gridIds = grida.jqGrid('getDataIDs');
    if (pageState.kushte.ZT)
        return;

    async.map(gridIds, function (id, callbackMap) {
        var indexi = id;
        if (grida.getTekstQelize('cmbLloji', indexi) == "Llogari") return;
        var kodArt = grida.getTekstQelize('txtKodi', indexi);
        var idArtikulli = grida.getTekstQelize('txtIdKodi', indexi);
        var detajim = -1;
        if (grida.getTekstQelize('txtDetajimi', indexi) != "")
            detajim = grida.getTekstQelize('txtDetajimi', indexi);
        var njesiRreshti = grida.getTekstQelize('cmbNjesia', indexi);
        var sasia = grida.getTekstQelize('txtSasia', indexi);
        if (kodArt == undefined) { callbackMap(null); return; }
        var ajaxZbritja;
        if (kodArt != "" && ZbritjaKlientit != undefined && ZbritjaKlientit !== "")
            ajaxZbritja = $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheZbritjeAnalitikeArtikulliRow"),
                data: JSON.stringify({ kodArtikulli: kodArt, ZbritjaKlientit: ZbritjaKlientit, date: data_DateEdit.GetText(), idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
            });
        //callWebserviceZbritjeArtikulliRow(kodArt, ZbritjaKlientit, data_DateEdit.GetText(), njesiRreshti, indexi);
        var ajaxCmimi;
        var detajim = merrDetajim(grida, indexi);
        if (kodArt != "" && ndryshocmime) {
            var counter = 0;
            if ($("#txtCmimi" + indeksi).data('pending') && $("#txtCmimi" + indeksi).data('pending') != "")
                counter = parseInt($("#txtCmimi" + indeksi).data('pending'));
            $("#txtCmimi" + indeksi).data('pending', counter + 1);

            ajaxCmimi = $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheCmimArtikulliRow"),
                data: JSON.stringify({ idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, kodArtikulli: kodArt, nivelCmimi: NivelCmimi, date: data_DateEdit.GetText(), monedha: cmbMonedha.GetText(), njesia: njesiRreshti, kursi: txtKursi.GetText(), idRreshti: indexi, sasi: sasia, shitjeblerje: (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? 0 : 1, detajim: detajim })
            });
        }
        if (ajaxCmimi && ajaxZbritja)
            $.when(ajaxCmimi, ajaxZbritja).done(function (ajaxCmimi, ajaxZbritja) {
                myJQGrid.endWebserviceCmimArtikulliRow(indexi, "txtCmimi");
                myJQGrid.endWebserviceCmimArtikulliRow(indexi, "txtCmimiTvsh");
                SucceededCallbackCmimArtikulliRow(ajaxCmimi[0]);
                SucceededCallbackZbritjeArtikulliRow(ajaxZbritja[0], indexi);
            });
        else if (ajaxCmimi && !ajaxZbritja)
            callWebserviceCmimArtikulliRow(kodArt, NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), njesiRreshti, txtKursi.GetText(), indexi, sasia, detajim);
        else if (ajaxZbritja && !ajaxCmimi)
            callWebserviceZbritjeArtikulliRow(kodArt, ZbritjaKlientit, data_DateEdit.GetText(), njesiRreshti, indexi);
        else {
            //s'duhet thirr asnjera
        }

        if (gjendjeartminmax && grida.getTekstQelize('cmbLloji', indexi) != "Llogari") {
            if (idArtikulli != "") {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
                    data: JSON.stringify({ idartikulli: idArtikulli, mag: grida.getTekstQelize('txtMagazina', indexi), idNdermarrje: pageState.idNdermarrje })
                }).done(function (result, msg, res) {
                    SucceededCallbackGjendjeArtikulli(result, memoryArt.Get(idArtikulli));
                    callbackMap(null);
                });
            }
            else
                callbackMap(null);
        }
    },
        function (err, result) {
            //alert("done or no :P");
        });
}

/*Update-on cmimin, gjendje minmax dhe zbritjen ne grid ne rreshtin e selektuar*/
function updateCmimiRowKorrent() {//po //todo gerta
    var grida = $(pageState.gridaSelector);
    var idRreshti = grida.getLastSel2();
    if (grida.getTekstQelize('cmbLloji', idRreshti) == "Llogari") return;
    var kodi = "#txtKodi" + idRreshti;
    var editorCmimi = '#txtCmimi' + idRreshti;
    if (pageState.kushte.ZT) return;
    $(editorCmimi).autocomplete("close");
    var kodArt = grida.getTekstQelize('txtKodi', idRreshti);
    var idArtikulli = grida.getTekstQelize('txtIdKodi', idRreshti);
    var artikulli = memoryArt.Get(idArtikulli);
    if (!kontrolloRow(idRreshti, artikulli)) {
        return;
    }
    if (kodArt != "" && ndryshocmime) {
        var detajim = merrDetajim(grida, idRreshti);
        callWebserviceCmimArtikulliRow(kodArt, NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), $('#cmbNjesia' + idRreshti + ' option:selected').text(), txtKursi.GetText(), idRreshti, grida.getTekstQelize('txtSasia', idRreshti), detajim, false);
    }
    if (kodArt != "" && ZbritjaKlientit != undefined && ZbritjaKlientit !== "")
        callWebserviceZbritjeArtikulliRow(kodArt, ZbritjaKlientit, data_DateEdit.GetText(), $('#cmbNjesia' + idRreshti + ' option:selected').text(), idRreshti);
    var detajim = -1;
    if (grida.getTekstQelize('txtDetajimi', idRreshti) != "")
        detajim = grida.getTekstQelize('txtDetajimi', idRreshti);
    if (gjendjeartminmax) {
        if (artikulli.IdArtikulli != "") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
                data: JSON.stringify({ idartikulli: artikulli.IdArtikulli, mag: grida.getTekstQelize('txtMagazina', idRreshti), idNdermarrje: pageState.idNdermarrje })
            }).done(function (result) { SucceededCallbackGjendjeArtikulli(result, artikulli); });
        }
    }
    kontrolloGjendje(grida.getTekstQelize('txtIdKodi', idRreshti), grida.getTekstQelize('txtMagazina', idRreshti), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRreshti), grida.getTekstQelize('txtDetajimi2t', idRreshti), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRreshti);
}


function JoClick(s, e) {//nese nuk do 2 rreshta me artikull njesoj
    var grida = $(pageState.gridaSelector);
    var idRresht = identifikuesRreshti;
    switch (identifikuesPyetje) {
        case "artikulli":
            resetRreshtKorent(rreshtidyfish);
            break;
        case "krediti":
        case 'SubjektPasiv':
            click = false;
            break;
        case 'detajimi1':
        case 'detajimi1Lidhje':
            grida.SetTekst("txtDetajimi", idRresht, '');
            break;
        case 'detajimi2':
        case 'detajimi2Lidhje':
            grida.SetTekst("txtDetajimi2t", idRresht, '');
            break;
        case 'limiti':
            grida.setTekstQelize('txtSasia', grida.getLastSel2(), 0);
            break;
        case 'printKupon':
        case "Fshi":
        case 'ndryshoCmime':
            break;
        case "Rivleresimi":
            window.location = "RegjistrimDokumentash.aspx?shitje_blerje=" + Utils.getUrlVar("shitje_blerje");
            break;
        default:
            alert('Pyetje e panjohur');
            break;
    }
}

var queryString;
function PoClick(s, e) {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = identifikuesRreshti;
    switch (identifikuesPyetje) {
        case "artikulli":
            //asgje
            break;
        case "krediti":
            Utils.shfaqLoadingGif();
            merrTeDhena(ASPxMenu1, editorMenu);
            if (trupiBosh == true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
                editorMenu.processOnServer = false; click = false; Utils.hiqLoadingGif();
            }
            btn.DoClick();
            break;
        case 'detajimi1':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            queryString = 'vjenNga=Shto_RegjistrimDokumentash&kodArtikulli=' + grida.getTekstQelize('txtKodi', idRresht) + '&lloji=1&veprimi=' + memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRresht)).IdKategoriDetajimi + '&kodDet=' + grida.getTekstQelize('txtDetajimi', idRresht);
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + queryString, 900, 600);
            //nese zbritNrWsRruges nuk behet te lupa
            break;
        case 'detajimi2':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            queryString = 'vjenNga=Shto_RegjistrimDokumentash&kodArtikulli=' + grida.getTekstQelize('txtKodi', idRresht) + '&lloji=2&veprimi=' + memoryArt.Get($(pageState.gridaSelector).getTekstQelize('txtIdKodi', idRresht)).IdKategoriDetajimi2 + '&kodDet=' + grida.getTekstQelize('txtDetajimi2t', idRresht);
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + queryString, 900, 600);
            break;
        case 'detajimi1Lidhje':
            Utils.shfaqLoadingGif();
            Utils.RuajLidhjeDetajim(1, idRresht, grida, pageState.idNdermarrje, pageState.idPerdoruesi);
            break;
        case 'detajimi2Lidhje':
            Utils.shfaqLoadingGif();
            Utils.RuajLidhjeDetajim(2, idRresht, grida, pageState.idNdermarrje, pageState.idPerdoruesi);
            break;
        case 'printKupon':
        case "SubjektPasiv":
            Utils.shfaqLoadingGif();
            btn.DoClick();
            break;
        case 'limiti':
            //asgje
            break;
        case 'ndryshoCmime':
            updateCmimiGrid();
            break;
        case "Fshi":
            Utils.shfaqLoadingGif();
            FshiDokument();
            break;
        case "Rivleresimi":
            Utils.shfaqLoadingGif();
            Rivleresim();
            break;
        default:
            alert('Pyetje e panjohur');
            break;
    }
}

var editorMenu;

/*
Function: callWebserviceKodi

Sugjeron listen e artikujve, llogarive ose makrove kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackKodi>.
*/
function callWebserviceKodi(vlera, idRreshti) {
    var grida = $(pageState.gridaSelector);
    var vleraLlojit = grida.getTekstQelize("cmbLloji", idRreshti);
    var grup = "";
    switch (vleraLlojit) {
        case "Artikull":
            if (merrSipasGrupit)
                grup = cmbGrup1.GetText();
            if (merrDhurata)
                grup = "DhurateVFOne";
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeArtikujshKodPershkKodbarEShpejt"),
                data: JSON.stringify({ infixText: vlera, pershk: pershk, grup: grup, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, artikujTeShitshem: (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? true : false, merrVetemAfatgjate: false, merrSipasDetajimit: pageState.kushte["NAGDN"], klasa: "" })
            }).done(function (result) { SucceededCallbackKodi(result, idRreshti); });
            break;
        case "Makro":
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimMakro"),
                data: JSON.stringify({ prefixText: vlera, idNderViti: pageState.idNdermarrjeVit, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
            }).done(function (result) { SucceededCallbackKodi(result, idRreshti); });
            break;
        case "Llogari":
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish"),
                data: JSON.stringify({ infixText: vlera, pershk: pershk, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
            }).done(function (result) { SucceededCallbackKodi(result, idRreshti); });
            break;
        default:
            console.log("Gabim: lloj kodi i panjohur: " + vleraLlojit);
            break;
    }
}

function callWebserviceKategoriShpenzimi(vlera, idRreshti) {//po
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KtheListKategoriShpenzimesh"),
        data: JSON.stringify({ prefixText: vlera, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
    }).done(function (result) {
        myJQGrid.SucceededCallbackKodi(result, '#txtKategoriShpenzimi' + idRreshti);
    });
}

function callWebserviceMagazina(vlera, idRreshti) {//po
    var llojartikulli = kthellojArt(idRreshti);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KtheListMagazinat"),
        data: JSON.stringify({ prefixText: vlera, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, meAutorizim: true, llojArt: llojartikulli })
    }).done(function (result) {
        myJQGrid.SucceededCallbackKodi(result, '#txtMagazina' + idRreshti);
    });
}

function kthellojArt(idRreshti) {
    var grida = $('#rowed5');
    var lloji = grida.getTekstQelize('cmbLloji', idRreshti);
    var idArt = grida.getTekstQelize('txtIdKodi', idRreshti);
    var llojartikulli = -1;
    if (lloji === "Artikull" && memoryArt.Contains(idArt)) {
        var artikulli = memoryArt.Get(idArt);
        llojartikulli = artikulli.LlojiArt == false ? 0 : 1;
    }
    return llojartikulli;
}

/*
Function: mbushArrayMagazinat

*/
function mbushArrayMagazinat() {//po
    //var colMag = $('#hfTmpColMag').val();
    var colMag = hfState.Get("tmpColMag");
    if (colMag != '' && typeof (colMag) != 'undefined') {
        pageState.colMagazina = colMagazina = $.parseJSON(colMag);
        //magazinaPare = btnMagazina.GetSelectedItem() == null ? colMagazina[0] : { Kodi: btnMagazina.GetSelectedItem().GetColumnText('Kodi'), Pershkrimi: btnMagazina.GetSelectedItem().GetColumnText('Pershkrimi') }; //magazina e pare per ta vendosur ne rreshtat e grides
        hfState.Remove("tmpColMag");
        //$('#hfTmpColMag').val(''); //boshatisim HF-ne qe mos te harxhojme kot memorie
    }
}

function changeMag(id, idRresht) {
    ///<summary> metode per ndryshimin e vleres se magazines. kontrollon nese magazina eksiston tek comboja e magazines dhe therret webservicet e infos nqs ka info <summary>
    var grida = $(pageState.gridaSelector);
    if (typeof idRresht == "undefined")
        idRresht = grida.getLastSel2();

    if ($('#txtMagazina' + idRresht + ' option').length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKjoMagazineNukEkziston"));
        $('#txtMagazina' + idRresht).val('');
    }
    var idkodi = grida.getTekstQelize('txtIdKodi', idRresht);//heq serialet

    if (hfSeriale.Contains(idkodi + '_' + idRresht)) {
        hfSeriale.Remove(idkodi + '_' + idRresht);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines"));
        grida.setTekstQelize('txtSerial', idRresht, 'Pa serial');
    }
    if (hfSasiSeriale.Contains(idkodi + '_' + idRresht)) {
        hfSasiSeriale.Remove(idkodi + '_' + idRresht);
    }
    var mag = grida.getTekstQelize('txtMagazina', idRresht);
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimMagSipasKodit"),
        data: JSON.stringify({ kodi: mag, idNdermarrje: pageState.idNdermarrje })
    }).done(function (result) { SucceededCallbackChangeMag(result, idRresht); });
    // grida.setTekstQelize('txtPershkrimmag', idRresht, mag);
    callWebServiceInfoRow(idRresht);
    kontrolloGjendje(grida.getTekstQelize('txtIdKodi', idRresht), grida.getTekstQelize('txtMagazina', idRresht), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRresht), grida.getTekstQelize('txtDetajimi2t', idRresht), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRresht);;
}

/*
Function: SucceededCallbackKodi

Sugjeron listen e artikujve, llogarive ose makrove kur shkruajme te kodi.
*/
function SucceededCallbackKodi(result, idRreshti) {//po
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRreshti);
}

function SucceededCallbackChangeMag(result, idRreshti) {//po
    var grida = $(pageState.gridaSelector);
    if (!idRreshti)
        idRreshti = grida.getLastSel2();
    grida.setTekstQelize('txtPershkrimmag', idRreshti, result);
}

function vendosSasiLiter(result, idRreshti) {
    // vendosim sasine liter per artikullin perberes
    var grida = $(pageState.gridaSelector);
    var sasiLitra = grida.getTekstQelize('txtSasia', idRreshti) * result;
    if ((grida.getTekstQelize('txtSasia', idRreshti) === "") || (isNaN(grida.getTekstQelize('txtSasia', idRreshti))))
        sasiLitra = 0;
    grida.setTekstQelize('txtSasiLitra', idRreshti, sasiLitra);
}
function updateTotalSasiLitra() {
    var grida = $(pageState.gridaSelector);
    var totalSasiLitra = 0;
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtKodi', idTe[i]) != "") {
            var sasiLitraRresht = grida.getTekstQelize('txtSasiLitra', idTe[i]);
            if (sasiLitraRresht === "")
                sasiLitraRresht = 0;
            totalSasiLitra = Number(totalSasiLitra) + Number(sasiLitraRresht);
        }
    }
    txtTotalLitra.SetText(totalSasiLitra);
}
/*
Function: SucceededCallbackKodi

Sugjeron listen e artikujve, llogarive ose makrove kur shkruajme te kodi.
*/
function SucceededCallbackLlogariShpenzimi(result, idRow) {//po
    myJQGrid.SucceededCallbackKodi(result, '#txtIdLlogShpenzimi' + idRow);
}

/*
Function: callWebserviceDetajimi


*/
function callWebserviceDetajimi(vlera, lloji) {//po
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var kodArtikulli = grida.getTekstQelize('txtKodi', idRow);
    if (kodArtikulli == "")
        return;

    var mag = (pageState.kushte.GJDM || (!pageState.kushte.GJDM && lloji == 1 ? hfState.Get("SDGMZ1") : hfState.Get("SDGMZ2"))) ? grida.getTekstQelize('txtMagazina', idRow) : '';
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheListeDetajimeshArtikulliNew"), data: JSON.stringify({ infixText: vlera, art: kodArtikulli, lloji: lloji, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, merrPerberesit: pageState.kushte["NDPAP"], date: data_DateEdit.GetDate(), sipasGjendjes: true, detajimi1: grida.getTekstQelize('txtDetajimi', idRow), mag: mag })
    }).done(function (result) {
        SucceededCallbackDetajimiAutoComplete(result, idRow);
    });
}

/*
Function: SucceededCallbackDetajimiAutoComplete

Sugjeron listen e detajimeve kur shkruajme te detajimi.
*/
function SucceededCallbackDetajimiAutoComplete(result, idRow) {
    var emerEditor = result.lloji == 1 ? '#txtDetajimi' : '#txtDetajimi2t';
    myJQGrid.SucceededCallbackKodiDetajimi(result.autocomplete, emerEditor + idRow, result.kategoriDet, result.infixText);
}

/*
Function: lostFocusKoloneFundit

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride.
*/
function lostFocusKoloneFundit() {     //po
    var idRow = $(pageState.gridaSelector).lostFocusKoloneFundit();
    //$(pageState.gridaSelector).setLastSel2(idRow);
    //if (($('#' + 'cmbLloji' + idRow).attr("disabled") == 'disabled')) {
    //    $('#txtKodi' + idRow).focus();
    //    $('#txtKodi' + idRow).blur();
    //    $('#txtKodi' + idRow).focus();
    //}
}

function kontrolloKurs(s, e) {//po
    var diferenca = Math.abs(kursifundit - parseFloat(txtKursi.GetText()));
    if (diferenca / kursifundit > 0.2)
        myMesazh.ShtoMesazhInformues(hfState.Get("msgKursiRiNdryshonShumeMeKursinMePare"));
}

//shto rreshta ne gride - per tu ndertuar akoma
function shtoRreshtaGride(grida, grid2colMap, colTrup, params) {
    var defaultParams = {
        identifikues: "idRreshti",
        mbishkruaj: false
    };
    params = $.extend({}, defaultParams, params);
    var arrMap = [{ keyGrid: "txtKodi", keyArray: "KodArtikulli" }];
    var grid2colMap = {
        txtKodi: "kodArtikulli",
        txtPershkrimi: "Pershkrimi"
    };

    for (var prop in grid2colMap) {
        rreshti[prop] = colTrup[i][mapimi[prop]];
    }
}

var colTrup, colArt, colMakro, colLlog, colDetArt, colDetArt2, colNjesAdminis, colNjesiArt, coltaksa, colTvshArt, colTvshLlog, colArtPerb, colKategoriShpenzimi;

function percaktoButonFshiEnabled() {
    var hfLidhur = $("input[id$='hfLidhur']");
    var aprovim = false;
    var hf = document.getElementById("status1");
    var pageStateLloji = hf.value;
    if (pageStateLloji != 'klonim' && pageStateLloji != 'kthim' && pageStateLloji != 'kthimVod' && pageStateLloji != 'bli' && pageStateLloji != 'konvertim' && pageStateLloji != 'konvertimblerje' && pageStateLloji != 'rezervim' && (($('#hfTeDrejtaModSkema').val() == "False" && lblStatusAprovimi.GetText() == 'Per Aprovim') || (lblStatusAprovimi.GetText() == 'Aprovuar' || lblStatusAprovimi.GetText() == 'Refuzuar'))) {
        aprovim = true;
    }
    //lastsel2 = -1;
    var konvSipasUrdherShitje = (hfTeDrejta.Get("KonvertimSipasUSH") == true && hfState.Get("konvNgaUshNeFsh") == true);
    var isLidhur = (hfLidhur.val().toLowerCase() === 'true');
    if (aprovim || konvSipasUrdherShitje)
        isLidhur = true;


    return hfState.Get("MosModifikoTrup") || arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && $("input[id$='hfShtimModifikim']").val() == "konvertim") || (isLidhur && $("input[id$='hfShtimModifikim']").val() != "bli");
}

function krijoRreshtBosh(id, txtFshi, txtSerialUnik) {
    return {
        name: id, id: id,
        txtNrRendor: id, cmbLloji: '', txtIdKodi: '', txtKodi: '', txtKodbari: '', txtPershkrimi: '', txtDetajimi: '', txtDetajimi2t: '', cmbNjesia: '', txtMagazina: '', txtGjeresi: '',
        txtGjatesi: '', txtSasiPermase: '', txtSasia: '', txtCmimi: '', txtZbritja: '', txtVleftaTVSH: '', cbTVSH: '', txtVlefta: '', txtShenime: '',
        txtDtFillimi: '', txtDtMbarimi: '', txtIdTrupi: '', txtIdTrupiKonvertimi: '', txtSasiaRez: '', txtRezervuar: '', txtIdTrupiRezervimi: '',
        txtSasiMbetur: '', txtIdTrupiTransferimi: '', txtSerial: '', txtIdLlogShpenzimi: '', txtIdTrupiKthim: '', txtIdTrupiKonvertimBlerje: '', txtPershkrimmag: '', txtSasiLitra: '', txtShenime2: '', txtZbritjaVlere: '', txtLlojZbritje: 'Perqindje', txtKategoriShpenzimi: '', txtPershkrimiKatShpenzimi: '', txtCmimiTvsh: '', txtVleraKomisionit: '', txtSerialUnik: txtSerialUnik, txtFshi: txtFshi
    };
}

function mbushGrideNgaHiddenFieldet(isLidhur) {
    //console.time("mbushGrideNgaHiddenFieldet");
    var grida = $(pageState.gridaSelector);
    var gridaKomision = $(pageState.gridaKomision);
    var shtimModifikim = pageState.lloji;
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    if (shtimModifikim == "shtim") {
        return;
    }

    if (shtimModifikim == "modifikim" || shtimModifikim === "klonim" || shtimModifikim === "shtimraport" || shtimModifikim === "kthim" || shtimModifikim === "kthimVod" || shtimModifikim === "bli" || shtimModifikim === "konvertim" || shtimModifikim === "konvertimblerje" || shtimModifikim === "rezervim") {
        if ($('#HfColTrup').val() !== '') {
            colTrup = JSON.parse($('#HfColTrup').val());
            $('#HfColTrup').val('');
        }
        if ($('#HfColKodbare').val() !== '') {
            colKodbare = JSON.parse($('#HfColKodbare').val());
            $('#HfColKodbare').val('');
        }
        if ($('#HfColArt').val() !== '') {
            colArt = JSON.parse($('#HfColArt').val());
            $('#HfColArt').val('');
        }
        if ($('#HfColMakro').val() !== '') {
            colMakro = JSON.parse($('#HfColMakro').val());
            $('#HfColMakro').val('');
        }
        if ($('#HfColllogarite').val() !== '') {
            colLlog = JSON.parse($('#HfColllogarite').val());
            $('#HfColllogarite').val('');
        }
        if ($('#HfColDetArt').val() !== '') {
            colDetArt = JSON.parse($('#HfColDetArt').val());
            $('#HfColDetArt').val('');
        }
        if ($('#HfColDetArt2').val() !== '') {
            colDetArt2 = JSON.parse($('#HfColDetArt2').val());
            $('#HfColDetArt2').val('');
        }
        if ($('#HfColNjesAdminis').val() !== '') {
            colNjesAdminis = JSON.parse($('#HfColNjesAdminis').val());
            $('#HfColNjesAdminis').val('');
        }
        if ($('#HfColNjesiArt').val() !== '') {
            colNjesiArt = JSON.parse($('#HfColNjesiArt').val());
            $('#HfColNjesiArt').val('');
        }
        if ($('#HfColTaksa').val() !== '') {
            coltaksa = JSON.parse($('#HfColTaksa').val());
            $('#HfColTaksa').val('');
        }
        if ($('#hfKonverto').val() !== '') {
            resultkonvertime = JSON.parse($('#hfKonverto').val());
            $('#hfKonverto').val('');
        }
        if ($('#HfColTvshArt').val() !== '') {
            colTvshArt = JSON.parse($('#HfColTvshArt').val());
            $('#HfColTvshArt').val('');
        }
        if ($('#HfColTvshLlog').val() !== '') {
            colTvshLlog = JSON.parse($('#HfColTvshLlog').val());
            $('#HfColTvshLlog').val('');
        }

        if ($('#HfColArtPerb').val() !== '') {
            colArtPerb = JSON.parse($('#HfColArtPerb').val());
            $('#HfColArtPerb').val('');
        }
        if ($('#HfColKategoriShpenzimi').val() !== '') {
            colKategoriShpenzimi = JSON.parse($('#HfColKategoriShpenzimi').val());
            $('#HfColKategoriShpenzimi').val('');
        }
        countkonvertime = resultkonvertime.length;
        //lastsel2 = 1;
        var idRow = 1;
        var idRowKomision = 1;
        grida.setLastSel2(idRow);
        gridaKomision.setLastSel2(idRowKomision);
        grida.jqGrid('clearGridData');
        gridaKomision.jqGrid('clearGridData');

        var enabledButoniFshi = percaktoButonFshiEnabled();
        rreshtat = new Array();
        rreshtatKomision = new Array();
        var meKomision = false;

        for (var i = 0; i < colTrup.length; i++) {
            meKomision = colTrup[i].MeKomision;
            if (!meKomision)
                be = myJQGrid.myValueButtonFshi(enabledButoniFshi, idRow, "#rowed5");
            else
                be = "";

            var enabledSu = PercaktoButtonSerialeUnikeEnabled(colArt[i].Klasa);
            var su = myJQGrid.myElemButtonSerialetUnike(enabledSu, idRow);
            var rreshti = krijoRreshtBosh(i + 1, be, su);

            if (colTrup[i].VleftaMeTvsh !== null)
                rreshti.txtVlefta = colTrup[i].VleftaMeTvsh;
            if (colTrup[i].IdkategoriShpenzimi !== null)
                rreshti.txtKategoriShpenzimi = colKategoriShpenzimi[i].Kodi;
            if (colKategoriShpenzimi[i]) {
                rreshti.txtPershkrimiKatShpenzimi = colKategoriShpenzimi[i].Pershkrimi;
            }
            if (colTrup[i].VleftaPaTvsh !== null)
                rreshti.txtVleftaTVSH = colTrup[i].VleftaPaTvsh;

            if (colTrup[i].Zbritje !== null)
                rreshti.txtZbritja = colTrup[i].Zbritje;
            if (colTrup[i].ZbritjeVlere !== null)
                rreshti.txtZbritjaVlere = (shtimModifikim === "kthim" || shtimModifikim === "kthimVod") ? -colTrup[i].ZbritjeVlere : colTrup[i].ZbritjeVlere;
            if (colTrup[i].LlojZbritje !== null)
                rreshti.txtLlojZbritje = (colTrup[i].LlojZbritje == 1 ? 'Perqindje' : 'Vlere');
            //fushat Sasia, Cmimi, Gjatesia, Gjeresia, SasiaMbetur, SasiPermasa duhen plotesuar ne cdo rast, si ne rastin kur ka artikull tek trupi, po ashtu edhe ne rastin kur ka llogari
            //ne qofte se nuk plotesohen, dhe ngelet string bosh, merr vleren NaN.

            if (shtimModifikim === "konvertim" || shtimModifikim === "konvertimblerje" || shtimModifikim === "rezervim") {
                if (colTrup[i].Sasimbetur !== null)
                    rreshti.txtSasia = colTrup[i].Sasimbetur;
                if (colTrup[i].SasiRez !== null)
                    rreshti.txtSasiaRez = colTrup[i].SasiRez;
                if (colArtPerb[i]) {
                    if ((colTrup[i].Sasia) && (colArtPerb[i].Koeficienti))
                        rreshti.txtSasiLitra = colTrup[i].Sasia * colArtPerb[i].Koeficienti;
                }
            }
            else
                if (shtimModifikim === "kthim") {
                    if (colTrup[i].Sasia !== null)
                        rreshti.txtSasia = colTrup[i].Sasimbetur;
                    if (colTrup[i].SasiRez !== null)
                        rreshti.txtSasiaRez = -colTrup[i].SasiRez;
                    if (colArtPerb[i]) {
                        if ((colTrup[i].Sasia) && (colArtPerb[i].Koeficienti))
                            rreshti.txtSasiLitra = colTrup[i].Sasia * colArtPerb[i].Koeficienti;
                    }
                }
                else {
                    if (colTrup[i].Sasia !== null)
                        rreshti.txtSasia = colTrup[i].Sasia;
                    if (colTrup[i].SasiRez !== null)
                        rreshti.txtSasiaRez = colTrup[i].SasiRez;
                    if (colArtPerb[i]) {
                        if ((colTrup[i].Sasia) && (colArtPerb[i].Koeficienti))
                            rreshti.txtSasiLitra = colTrup[i].Sasia * colArtPerb[i].Koeficienti;
                    }
                }
            if (colTrup[i].Sasimbetur !== null)
                rreshti.txtSasiMbetur = colTrup[i].Sasimbetur;

            var eshteDokKthimi = hfState.Get("eshteDokKthimi");
            if (((shtimModifikim == "kthim" || eshteDokKthimi) && rreshti.txtSasiMbetur !== rreshti.txtSasia) ||
                $('#ASPxSplitter1_hyperlink1').text() == " I transferuar nga ndermarjen meme " || $('#ASPxSplitter1_hyperlink1').text() == " Transferim nga Magazina Vodafone ")
                cmbGrup1.SetEnabled(false);
            if (colTrup[i].SasiPermasa !== null)
                rreshti.txtSasiPermase = colTrup[i].SasiPermasa;
            if (colTrup[i].Gjeresi !== null)
                rreshti.txtGjeresi = colTrup[i].Gjeresi;
            if (colTrup[i].Gjatesi !== null)
                rreshti.txtGjatesi = colTrup[i].Gjatesi;
            if (colTrup[i].Cmimi !== null)
                rreshti.txtCmimi = colTrup[i].Cmimi;
            if (coltaksa[i].KodTaksa)
                rreshti.cbTVSH = coltaksa[i].KodTaksa;
            else
                rreshti.cbTVSH = "Pa TVSH";
            if (colTrup[i].Cmimi !== null) {
                rreshti.txtCmimiTvsh = colTrup[i].Cmimi * (1 + coltaksa[i].NormaPerqindje / 100);
            }
            if (colTrup[i].IdShitjeTrupi !== null)
                rreshti.txtIdTrupi = colTrup[i].IdShitjeTrupi;
            if (colTrup[i].IdTrupiKonvertimi !== null)
                rreshti.txtIdTrupiKonvertimi = colTrup[i].IdTrupiKonvertimi;
            if (colTrup[i].IdTrupiKonvertimBlerje !== null)
                rreshti.txtIdTrupiKonvertimBlerje = colTrup[i].IdTrupiKonvertimBlerje;
            if (shtimModifikim === "kthim" || shtimModifikim === "kthimVod") {
                if (colTrup[i].IdShitjeTrupi !== null)
                    rreshti.txtIdTrupiKthim = colTrup[i].IdShitjeTrupi;
            }
            else
                if (colTrup[i].IdTrupiKthim !== null) {
                    if (colTrup[i].IdTrupiKthim != 0)
                        lejomod = false;
                    rreshti.txtIdTrupiKthim = colTrup[i].IdTrupiKthim;
                }
            if (colTrup[i].IdTrupiTransferimi !== null)
                rreshti.txtIdTrupiTransferimi = colTrup[i].IdTrupiTransferimi;
            if (colTrup[i].IdTrupiRezervimi !== null)
                rreshti.txtIdTrupiRezervimi = colTrup[i].IdTrupiRezervimi;
            if (colTrup[i].Shenime !== null)
                rreshti.txtShenime = colTrup[i].Shenime;
            if (colTrup[i].Shenime2 !== null)
                rreshti.txtShenime2 = colTrup[i].Shenime2;
            if (colTrup[i].NrLlogShpenzimi !== null)
                rreshti.txtIdLlogShpenzimi = colTrup[i].NrLlogShpenzimi;
            if (hfSeriale.Contains(colTrup[i].IdKodi + '_' + idRow) && hfSeriale.Get(colTrup[i].IdKodi + '_' + idRow) != "[]") {
                rreshti.txtSerial = 'Me serial';
            }
            else
                rreshti.txtSerial = 'Pa serial';
            if (colTrup[i].DtFillimi !== undefined)
                rreshti.txtDtFillimi = new Date(colTrup[i].DtFillimi).format('dd/MM/yyyy');
            if (colTrup[i].DtMbarimi !== undefined)
                rreshti.txtDtMbarimi = new Date(colTrup[i].DtMbarimi).format('dd/MM/yyyy');

            if (colTrup[i].IdLlojVeprimi === 1) {
                rreshti.cmbLloji = "Artikull";
                rreshti.txtKodi = colArt[i].KodArtikulli;
                if (colKodbare.length > 0) {
                    var myKodbarElem = Utils.ktheElement(colKodbare, "IdShitjeTrupi", colTrup[i].IdShitjeTrupi);
                    rreshti.txtKodbari = myKodbarElem ? myKodbarElem.Pershkrimi : "";
                }
                else rreshti.txtKodbari = "";
                if (pershk === 1)
                    rreshti.txtPershkrimi = colArt[i].PershkrimArtikulli;
                else
                    if (colArt[i].PershkrimiAngArtikulli !== "")
                        rreshti.txtPershkrimi = colArt[i].PershkrimiAngArtikulli;
                    else
                        rreshti.txtPershkrimi = colArt[i].PershkrimArtikulli;
                rreshti.txtVleraKomisionit = colTrup[i].VleraKomisionit;
                rreshti.txtIdKodi = colArt[i].IdArtikulli;
                grida.vendosTeDhenaPerQelizen("txtKodi", idRow, "IdLlogari", colArt[i].IdLlogariKomisioni);
                grida.vendosTeDhenaPerQelizen("txtKodi", idRow, "MeKomision", colArt[i].LlogaritKomision);
                grida.vendosTeDhenaPerQelizen("txtKodi", idRow, "ZbritjaKlientit", (colTrup[i].VleraKomisionit / colTrup[i].VleftaPaTvsh) * 100);
                rreshti.txtRezervuar = colArt[i].IRezervueshem;
                if (colDetArt[i].KodDetajimArtikulli !== null)
                    rreshti.txtDetajimi = colDetArt[i].KodDetajimArtikulli;
                if (colDetArt2[i].KodDetajimArtikulli !== null)
                    rreshti.txtDetajimi2t = colDetArt2[i].KodDetajimArtikulli;


                if (btnMagazina.GetText() != "") {
                    rreshti.txtMagazina = btnMagazina.GetSelectedItem().GetColumnText('Kodi');
                    rreshti.txtPershkrimmag = btnMagazina.GetSelectedItem().GetColumnText('Pershkrimi');
                }
                else {
                    if (colNjesAdminis[i].Kodi) {
                        rreshti.txtMagazina = colNjesAdminis[i].Kodi;
                        rreshti.txtPershkrimmag = colNjesAdminis[i].Pershkrimi;
                    }
                    else
                        if (colArt[i].Magazina) {
                            rreshti.txtMagazina = colArt[i].Magazina;
                            rreshti.txtPershkrimmag = colArt[i].Magazina;
                        }
                        else
                            if (colMagazina[0] != undefined) {
                                rreshti.txtMagazina = colMagazina[0].Kodi;
                                rreshti.txtPershkrimmag = colMagazina[0].Kodi;
                            }
                }

                if (colNjesiArt[i].KodNjesia !== null)
                    rreshti.cmbNjesia = colNjesiArt[i].KodNjesia;

                var art = colArt[i];

                art.listeTvsh = colTvshArt[i];
                memoryArt.Set(art);
                if (shtimModifikim === "rezervim" && rreshti.txtKodi !== "" && !pageState.kushte.ZT && ndryshocmime && (rreshti.txtCmimi == "" || rreshti.txtCmimi == 0)) {
                    var detajim = "";
                    if (pageState.llojDetajimNdermarrje == 1)
                        detajim = colDetArt[i].KodDetajimArtikulli;
                    if (pageState.llojDetajimNdermarrje == 2)
                        detajim = colDetArt2[i].KodDetajimArtikulli;
                    callWebserviceCmimArtikulliRow(rreshti.txtKodi, hfState.Get("idNivelCmimiRez"), data_DateEdit.GetText(), cmbMonedha.GetText(), rreshti.cmbNjesia, txtKursi.GetText() == "" ? 1 : txtKursi.GetText(), parseInt(idRow), rreshti.txtSasia, detajim);
                    updateCmimiGridToDo = true;
                }
            }
            else
                if (colTrup[i].IdLlojVeprimi == 2) {
                    rreshti.cmbLloji = "Makro";
                    rreshti.txtKodi = colMakro[i].KodiKokaMakro;
                    rreshti.txtPershkrimi = colMakro[i].PershkrimiKokaMakro;
                    rreshti.txtIdKodi = colMakro[i].IdKokaMakro;
                }
                else {
                    rreshti.cmbLloji = "Llogari";
                    var llogari = colLlog[i];
                    llogari.listeTvsh = colTvshLlog[i];
                    //HfLlog.Set(llogari.IdLlogari, llogari);
                    memoryLlog.Set(llogari);
                    rreshti.txtKodi = colLlog[i].NrLlogari;
                    rreshti.txtKodbari = "";
                    rreshti.txtVleraKomisionit = colTrup[i].VleraKomisionit;
                    //memoryArt.Set(colLlog[i]);

                    if (colNjesAdminis[i].Kodi)
                        rreshti.txtMagazina = colNjesAdminis[i].Kodi;

                    if (pershk == 1)
                        rreshti.txtPershkrimi = colLlog[i].EmerLlogari1;
                    else {
                        if (colLlog[i].EmerLlogari2 !== "")
                            rreshti.txtPershkrimi = colLlog[i].EmerLlogari2;
                        else
                            rreshti.txtPershkrimi = colLlog[i].EmerLlogari1;
                    }
                    rreshti.txtIdKodi = colLlog[i].IdLlogari;
                }
            //var su = grida.jqGrid('addRowData', parseInt(lastsel2), rreshti);
            //grida.setTekstQelize('txtNrRendor', lastsel2, grida.getInd(lastsel2, false));

            if (!meKomision) {
                rreshtat.push(rreshti);
                idRow = idRow + 1;
                grida.setLastSel2(idRow);
            } else {
                idRowKomision = idRowKomision + 1;
                rreshtatKomision.push(rreshti);
                gridaKomision.setLastSel2(idRowKomision);
            }
            //lastsel2 = lastsel2 + 1;
        }
        grida[0].addJSONData(rreshtat);
        if (rreshtatKomision.length > 0)
            gridaKomision[0].addJSONData(rreshtatKomision);
    }
    updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
    if (hfState.Get("VleraMarreveshje") && hfState.Get("VleraMarreveshje") != "null" && pageState.kushte["VF_VM"] && (pageState.lloji == 'klonim' && Utils.getUrlVar("modMarreveshje") == "modifikim") && cmbStatusMarreveshje.GetValue() == 1 && DtMbarimi_DateEdit.GetDate() > Utils.ktheDateDefault(pageState.periudha))//marreveshje aktive- merret vetem vlera e buxhetit nga file i ngarkuar
    {
        var vlera = JSON.parse(hfState.Get("VleraMarreveshje"));
        if (txtIdMarreveshje.GetText().toLowerCase() == vlera.AgreementID.toLowerCase()) {
            grida.setTekstQelize('txtVlefta', grida.getLastSel2() - 1, vlera.VleraBuxhetit);//artkulli default Buxhet i vendoset vlefta me tvsh
            changedVleftaTVSH('txtVlefta', grida.getLastSel2() - 1);
        }
    }
}
var lidhur = false;
var widthLupaLlogaria = 750;
var heightLupaLlogaria = 560;
var widthLupaDokumenta = 950;
var heightLupaDokumenta = 560;
var widthLupaKF = 950;
var heightLupaKF = 560;
var widthLupaAutomjet = 800;
var heightLupaAutomjet = 600;
var identifikuesPerPopupAutomjet = "Shto_RegjistrimDokumentash";
var widthLupaArtikull = 1100;
var heightLupaArtikull = 560;
var widthLupaMenyraTransporti = 600;
var heightLupaMenyraTransporti = 560;
var widthLupaKushteDergimi = 600;
var heightLupaKushteDergimi = 560;
var widthLupaTransportues = 1070;
var heightLupaTransportues = 750;
var widthLupaAgjenti = 600;
var heightLupaAgjenti = 560;
var widthLupaMaturimi = 950;
var heightLupaMaturimi = 560;
var widthLupaKushtePagese = 600;
var heightLupaKushtePagese = 560;
var widthLupaPeriudha = 600;
var heightLupaPeriudha = 560;
var widthLupaDetajim = 600;
var heightLupaDetajim = 560;
var widthLupaMakro = 1100;
var heightLupaMakro = 560;
var widthLupaMagazina = 600;
var heightLupaMagazina = 560;
var widthLupaFazat = 950;
var heightLupaFazat = 560;
var arrDetajimetSelektuara = new Array(); //array me detajimet e zgjedhura per artikullin (ku behet modifikimi i fatures)
var identifikuesPerPopupDokumentat = "Shto_RegjistrimDokumentash.aspx";
var arrLloji = new Array();
var arrKodi = new Array();
var arrPershkrimi = new Array();
var arrDetajimi = new Array();
var arrNjesia = new Array();
var arrSasia = new Array();
var arrCmimi = new Array();
var arrZbritje = new Array();
var arrVlefteTVSH = new Array();
var arrTVSH = new Array();
var arrVlefte = new Array();
var arrMagazina = new Array();
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
var indeksi = -1;
var editorLloji;
var editorKodi;
var editorPershkrimi;
var editorDetajimi;
var editorNjesia;
var editorSasia;
var editorCmimi;
var editorZbritje;
var editorVlefteTVSH;
var editorTVSH;
var editorVlefte;
var btnKlienti;
var indexCounter;
var indeksPerPershkrim;
var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupMenyraTransporti;
var identikuesPerPopupKushteDergimi;
var identikuesPerPopupAgjenteShitje;
var identikuesPerPopupKushtePagese;
var identikuesPerPopupLlogari;
var identikuesPerPopupArtikulli;
var identifikuesPerPopupDetajime;
var identifikuesPerPopupMagazina;
var keyGlobal;
var indeksPerEmertim;
var ZbritjaKlientit = ""; //variabel qe mban vleren e zbritjes qe i eshte caktuar klientit qe kemi zgjedhur
var NivelCmimi; // variabel qe mban nivelin e cmimit qe i eshte caktuar klientit qe kemi zgjedhur
var Serialet;

function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    if (window.parent.lblFaqja) {
        pageState.ambienti = hfState.Get("pershkrimKomponente");
        window.parent.lblFaqja.SetText(pageState.ambienti);
    }
    myCookies.createCookie('adresa', window.location.href, 1);
}

/*
Function: ButtonClickKerko

Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {//po
    identifikuesPerPopupDokumentat = "Shto_RegjistrimDokumentash.aspx";
    popupUniversal.SetHeaderText('Zgjidh dokumentin');
    var pikeShitjeFurnizim = '';
    if (cmbPikeShitjeFurnizimi.GetSelectedItem() != null)
        pikeShitjeFurnizim = cmbPikeShitjeFurnizimi.GetSelectedItem().GetColumnText('Kodi');
    var degeAdmin = '';
    if (cmbDegeAdministrative.GetSelectedItem() != null)
        degeAdmin = cmbDegeAdministrative.GetSelectedItem().GetColumnText('Kodi');

    var queryString = {
        veprimi: 'RegjistrimDokumentash',
        shitje_blerje: pageState.veprimi,
        niveli: cmbNiveli.GetValue(),
        njesia: pikeShitjeFurnizim,
        degeAdmin: degeAdmin,
        listUrl: listUrl
    };
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetSize(widthLupaDokumenta, heightLupaDokumenta);
    popupUniversal.Show();

}

function kontrollofaturaTePaprintuara() {//po
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "KontrolloFaturaPaprintuara"),
        data: JSON.stringify({ idndermarje: pageState.idNdermarrje, idperdorues: pageState.idPerdoruesi, iddege: cmbDegeAdministrative.GetValue(), idnivel:cmbNiveli.GetValue() })
    }).done(SuccededCallbackPaPrintuar);
}

function SuccededCallbackPaPrintuar(result) {
    if (result)
        ButtonClickPaPrintuar();
    else if (hapLupeValidimi)
        ButtonClickValidim();
}

function ButtonClickPaPrintuar() {//po
    popupUniversal.SetHeaderText(hfState.Get("msgFaturatNukJanePrintuar"));
    identifikuesPerPopupDokumentat = "Shto_RegjistrimDokumentashPaPrint.aspx";
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?veprimi=RegjistrimDokumentashPaprintuar&niveli=' + cmbNiveli.GetValue() + '&idshop=' + cmbDegeAdministrative.GetValue());
    popupUniversal.SetSize(widthLupaDokumenta, heightLupaDokumenta);
    popupUniversal.Show();
}

function HapLupeValidimiPasLupesPaPrintuar() {
    if (hapLupeValidimi) {
        hapLupeValidimi = false; //se pastaj po shtype X tek lupa validim, sdo te mbyllet kurre
        ButtonClickValidim();
    }
}
function ButtonClickArkiva(idDok, lupeOptions) {//po  
    var teDrejtaNiveli = merrTeDrejtaNiveli(cmbNiveli.GetValue());
    if (!teDrejtaNiveli.DArkiva) {
        myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta per arkiven per nenkategorine: " + cmbNiveli.GetText());
        return;
    }
    lupeOptions = lupeOptions ? lupeOptions : {};
    var defaults = { emerPopUpi: popupUniversal, titull: hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"), baseUrl: "LupaArkiva.aspx", width: 738, height: 548, params: { vjenNga: "RegjistrimDokumentash", veprimi: pageState.veprimi, idDok: idDok, tmpfolder: hfArkiva.Get("rootFolder") } };
    lupeOptions = $.extend({}, defaults, lupeOptions);
    Utils.hapLupe(lupeOptions);

    //popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    //popupUniversal.SetSize(738, 548);
    //// popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=RegjistrimDokumentash&veprimi=' + pageState.veprimi + '&idDok=' + $('#hfArkivaDokId').val() + '&shtim_modifikim=' + pageState.lloji);
    //popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=RegjistrimDokumentash&veprimi=' + pageState.veprimi + '&idDok=' + idDok + "&tmpfolder=" + hfArkiva.Get("rootFolder"));
    //popupUniversal.Show();
}

function ButtonClickValidim() {//po
    hapLupeValidimi = false;
    popupUniversal.SetHeaderText(hfState.Get("msgValidimiKlientit"));

    popupUniversal.SetContentUrl('LupaValidim.aspx?mag=' + btnMagazina.GetValue() + '&status=' + hfState.Get('Status') + '&kodi=' + cmbModeli.GetText().substring(0, 5) + "&KGJVFONE=" + pageState.kushte["KGJVFONE"]);

    popupUniversal.SetSize(700, 600);
    popupUniversal.Show();
}

/*
Function: ButtonClickKonverto

Hap lupen e dokumentave.
*/
function ButtonClickKonverto() {//po
    var teDrejtaNiveli = merrTeDrejtaNiveli(cmbNiveli.GetValue());
    if (!teDrejtaNiveli.DKonverto) {
        myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta konvertimi per nenkategorine: " + cmbNiveli.GetText());
        return;
    }
    Utils.shfaqLoadingGif();
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKonvertuar"),
        data: JSON.stringify({ ids: [Utils.getUrlVar('id')], kodkonfig: "LDSH", idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, idGjuha: pageState.idGjuha, pageId: window['CurrentPageId'] })
    }).done(SuccededCallbackKonvertime);
}

/*
Function: callWebserviceKonfigurimi

*/
function callWebserviceKonfig(idKomp, kodKonf, pastroSeriale) { 

    var url = Utils.getServerApiUrl("Rregjistrime", "ktheKonfigDB");
    var dateDok;
    if (pageState.lloji === "shtim" || pageState.lloji === "shtimraport") {
        dateDok = Utils.ktheDateDefault(pageState.periudha);
        data_DateEdit.SetDate(dateDok);
        $.ajax({
            pritPergjigje: true,
            url: url,
            data: JSON.stringify({
                idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: pageState.idNdermarrje, kodKontrollKlienti: "btnKlienti", idKlienti: -1, shtim: true, merrFormatKursi: true, idGjuha: pageState.idGjuha,
                idPerdoruesi: pageState.idPerdoruesi, llojVeprimi: pageState.lloji, dateDok: dateDok
            })
        }).done(SucceededCallbackKonfig);
    }
    else {
        dateDok = data_DateEdit.GetDate();
        $.ajax({
            pritPergjigje: true,
            url: url,
            data: JSON.stringify({
                idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: pageState.idNdermarrje, kodKontrollKlienti: "btnKlienti", idKlienti: btnKlienti.GetValue() == null ? -1 : btnKlienti.GetValue(),
                shtim: false, merrFormatKursi: true, idGjuha: pageState.idGjuha, idPerdoruesi: pageState.idPerdoruesi, llojVeprimi: pageState.lloji, dateDok: dateDok, dateDokDefault: Utils.ktheDateDefault(pageState.periudha)
            })
        }).done(SucceededCallbackKonfig);
    }    
    if (pastroSeriale) {
        $.ajax({
            pritPergjigje: false,
            showLoading: true,
            data: JSON.stringify({
                guidString: pageState.guidString
            }),
            url: Utils.getServerApiUrl("SerialeUnike", "HiqSerialet")
        });
        kaSeriale = false;
    }
}

var colKushte;
function getInfoArtStructure(idInfoArt) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "getInfoArtStructure"),
            data: JSON.stringify({ idkoka: idInfoArt, idNdermarrje: pageState.idNdermarrje })
        }).done(SuccededCallbackInfoArtStructure);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

function SuccededCallbackInfoArtStructure(result) {
    vendosInfo(lbxZgjedhur, result, true);
}

function SuccededCallbackInfoLlogStructure(result) {
    vendosInfo(lbxLlogari, result, true);
}

function SuccededCallbackInfoKfStructure(result) {
    vendosInfo(lbxKF, result, true);
}

function krijoTable(rresht, kolone, emerTabele) {//po
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var state = {};
state.fokusi = 0;
//var fokusi = 0;
var infoArt = false;
var infoKf = false;
var infoLl = false;
var idInfoArt = 0;
var idInfoLlog = 0;
var idInfoKf = 0;
var upp = false; sasiautomatike = false;
var skema = 0;
var colGrida;
var totaliVjeter = undefined;
var kupon = false;
var serialShenuarNgaPerdoruesi = false;
var kuponKerkonKonfigurim = false;
var kuponKerkonKlienti = false;
var modifikimJoHapjePareKuponTatimor = false;
var kontrolloNrAutomatik = true;
var kasechrome = false;
var mesazhriprintimi = false;
var shfaqlupepaprintuar = false;
var kurskonvertimi = false;
var datekonvertimi = false;
var vendosartikullblerje = true;
var merrSipasGrupit = false;
var merrDhurata = false;

var formatNumriZgjedhur;
var formatKursi;
var rezultatit;
//var cmimesipassasise = false;
//var kushtDDFMK = false;//Date dokumenti midis Dt Fillimi dhe Dt Mbarimi nga konvertimi
var dtfillimikonvertimi = new Date();
var dtmbarimikonvertimi = new Date();
var ndryshocmime = false;
var cmimemetvsh = false;
//var ruajlocalstorage = false;
//var vlereperqindje = 'perqindje';
var percaktoCmimPerKF = "";
var hapLupeValidimi = false;

function initKushte() {
    hfState.Set("LAVK", false);
    hfState.Set("GJDM", false);
    hfState.Set("VCVKMD", false);
    hfState.Set("LKVK", false);
    pageState.kushte["ZT"] = false;
    pageState.kushte["ZADHG"] = true;
    pageState.kushte["AFI"] = false;
    pageState.kushte['RDDNR'] = true;
    pageState.kushte['NAGDN'] = false;
    pageState.kushte['NDPAP'] = false;
    pageState.kushte['NKAKNKA'];
}

function SucceededCallbackKonfig(result) {  //po
    var grida = $(pageState.gridaSelector);
    if (pageState.lloji == "shtim" || pageState.lloji == "shtimraport")
        pastroFushatKokes();
    formatNumriZgjedhur = result.formatNumri;
    Utils.hiqLoadingGif();
    rezultatit = result;
    var hfLidhur = $("input[id$='hfLidhur']");
    var colKontrollet, colAtrTrupi;
    pageState.colAtrTrupi = colAtrTrupi = result.colAtrTrupi;
    pageState.colKontrollet = colKontrollet = result.colKontroll;
    enablekursi = colAtrTrupi[colKontrollet.findIndex(function (kontrolli) { return kontrolli.KodKontrolli == "txtKursi"; })].Enabled;
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];


    if (pageState.lloji != 'klonim' && pageState.lloji != 'kthim' && pageState.lloji != 'konvertim' && pageState.lloji != 'konvertimblerje' && pageState.lloji != 'rezervim' && (($('#hfTeDrejtaModSkema').val() == "False" && lblStatusAprovimi.GetText() == 'Per Aprovim') || (lblStatusAprovimi.GetText() == 'Aprovuar' || lblStatusAprovimi.GetText() == 'Refuzuar'))) {
        pageState.aprovim = true;
    }
    if (pageState.lloji == 'kthim' && hfState.Get("likuiduar") == "true")
        cmbMenyrePagese.SetSelectedIndex(2);
    if (pageState.lloji != 'modifikim' && pageState.lloji != 'klonim' && pageState.lloji != 'bli' && pageState.lloji != 'kthim' && pageState.lloji != 'kthimVod' && pageState.lloji != 'konvertim' && pageState.lloji != 'konvertimblerje' && pageState.lloji != 'rezervim' && result.grupimeDokumentesh) {
        Utils.SucceededCallbackGrupimDokumentash(result.grupimeDokumentesh);
    }

    if (pageState.lloji == 'shtim' || pageState.lloji == 'shtimraport')
        cmbMenyrePagese.SetSelectedIndex(0);
    MenuTeDrejtaKonvertimi();
    var konvSipasUrdherShitje = (hfTeDrejta.Get("KonvertimSipasUSH") == true && hfState.Get("konvNgaUshNeFsh") == true);
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, $("#hfShtimModifikim"), '', arrTabela, undefined, undefined, hfLidhur, arrPrind, pageState.aprovim);
    kupon = cbKupon.GetChecked();
    kuponKerkonKonfigurim = kupon; //ruan vleren e konfigurimit te kuponit pergjate vendosjes se vlerave te konfigurimit
    myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
    if (pageState.lloji == 'shtim' || pageState.lloji == 'shtimraport') {
        btnKlienti.SetSelectedIndex(-1);
        setZbritjeTotal(Utils.llojZbritje.Perqindje, 0, true);
    }
    else {
        if ((pageState.lloji == 'konvertim' || pageState.lloji == 'konvertimblerje') && Utils.getUrlVar("klientKonv") != "-1")
            btnKlienti.SetValue(Utils.getUrlVar("klientKonv"));

        if (btnKlienti.GetValue() != null)
            callWebserviceKF({ idKf: btnKlienti.GetValue(), callBack: SucceededCallbackOKFDefault, mosPlotesoTeDhena: false, infoKf: true });

        if (btneKlientfurnitorVartes.GetSelectedItems() && btneKlientfurnitorVartes.GetSelectedItems().length == 1)
            callWebserviceKF({ idKf: btneKlientfurnitorVartes.GetValue()[0], callBack: SucceededCallbackOKFDefaultVartes, mosPlotesoTeDhena: false });

        if (pageState.lloji == 'kthim' || pageState.lloji == 'kthimVod')
            txtVlefte.SetText(-txtVlefte.GetText());
    }

    if ((pageState.lloji == 'konvertim' || pageState.lloji == 'konvertimblerje') && Utils.getUrlVar("klientKonv") == "-1") {
        btnKlienti.SetSelectedIndex(-1);
        btnKlienti.SetText('');
        btnKlienti.SetValue(null);
        btneKlientfurnitorVartes.ClearOptions();
    }
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFundi").show();

    NivelCmimi = 0;
    RenditjeCheck(false);
    $('#hfSkema').val('0');
    colGrida = result.colGrida;
    colKushte = result.colKushte;
    var kodniveli = result.kodniveli;
    var konfLlojRreshti = result.konfLlojRreshti;
    $("input[id$='HfGridCol']").val(JSON.stringify(colGrida));
    LostFocusSerial();
    TextChangeFile();
    var hfKlvartes = $("#hfLupaKlientFurnitorvartes");
    var hfAuto = $("#hfLupaAutomjet");
    var hfTransp = $("#hfLupaMenyreTransporti");
    var hfDerg = $("#hfLupaKushtDergimi");
    var hfAgj = $("#hfLupaAgjentShitje");
    var hfMat = $("#hfLupaAfatMaturimi");
    var hfPag = $("#hfLupaKushtPagese");
    var hfMag = $("#hfLupaMagazina");
    var hfTr = $("#hfLupaTransportues");
    var hfrivleresim = $('#hfKontrollRivleresim');
    //$('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    //$('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    LostFocusKursi();
    if (pageState.lloji != "shtim") {
        cmbNiveli.SetEnabled(false);
        cmbModeli.SetEnabled(false);
    }

    if (pageState.lloji == "shtim" || pageState.lloji === "klonim" || pageState.lloji === "shtimraport" || pageState.lloji === "kthim" || pageState.lloji === "kthimVod" || pageState.lloji === "bli" || pageState.lloji === "konvertim" || pageState.lloji === "konvertimblerje" || pageState.lloji === "rezervim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, data_DateEdit.GetDate(), result.objekteDefault);
        //kontrolloNrAutomatik = false;
        $("input[id$='hfPiket']").val('');
        cmbStatusMarreveshje.SetEnabled(false);
    }

    pershk = 1;
    konfirmimArt = 2;
    LlojiDefault = '0';
    kurskonvertimi = false;
    datekonvertimi = false;
    njesiDef = 1;
    merrSipasGrupit = false;
    merrDhurata = false;
    tvshkont = 3;
    regjistrimKF = 1;
    dogana = 2;
    limitKF = 2;
    ndryshocmime = false;
    cmimemetvsh = false;
    kodkodbar = 1;
    cmimzero = 0;
    kasechrome = false;
    sasiautomatike = false;
    mesazhriprintimi = false;
    upp = false;
    shfaqlupepaprintuar = false;
    dtfillimikonvertimi = new Date();
    dtmbarimikonvertimi = new Date();
    var hidField1 = $("#hfKontabilizimi");
    hidField1.val(0);
    var magazinagrides = $("select[id$='txtMagazina']");
    if (magazinagrides.val() != undefined) {
        magazinagrides.val('');
        magazinagrides.selectedIndex = 0;
        magazina1 = '';
    }
    if (((cmbModeli.GetText().length >= 5 && (cmbModeli.GetText().substring(0, 5) == "VFONE" && (Utils.getUrlVar("kthehu") != "kthehu" && !((!hfState.Get("RoliSR") && hfState.Get("Status") == 0))) || cmbModeli.GetText().substring(0, 5) == "USHDD")) || (cmbModeli.GetText().length >= 6 && cmbModeli.GetText().substring(0, 6) == "BAZAAR")) && pageState.lloji != 'konvertim' && (pageState.lloji != 'modifikim' || (hfState.Get('Status') == 0 && pageState.lloji == 'modifikim')))
        hapLupeValidimi = true;

    var hapLupeValidimiPaFaturaPaPrintuar = false;

    initKushte();

    for (var j = 0; j < colKushte.length; j++) {
        var kusht = colKushte[j];
        switch (kusht.Kodi) {
            case "LEJOARTPERB":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po" ? true : false;
                hfState.Set("LEJOARTPERB", kusht.Alternativa);
                break;
            case "GJDM":
            case "LAVK":
            case "VCVKMD":
            case "LKVK":
            case "IPSPRD":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po" ? true : false;
                hfState.Set(kusht.Kodi, pageState.kushte[kusht.Kodi]);
                break;
            case "ZT":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Jo" ? false : true;
                break;
            case "RSHTTK":
            case "RVF":
            case "VCVS":
            case "AFI":
            case "RDDNR":
            case "NAGDN":
            case "KGJVFONE":
            case "ZBVFONE":
            case "NDPAP":
            case "NKAKNKA":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po" ? true : false;
                break;
            case "PLLZD":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa;
                break;
            case "ZADHG":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Shto ne rresht te ri" ? true : false;
                break;
            case "DOKKONTRATE":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po" ? true : false;
                btnFazat.SetVisible(pageState.kushte[kusht.Kodi]);
                break;
            case "DDFMK":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po" ? true : false;
                if (pageState.kushte[kusht.Kodi]) {
                    dtfillimikonvertimi = DtFillimi_DateEdit.GetDate();
                    dtmbarimikonvertimi = DtMbarimi_DateEdit.GetDate();
                }
                break;
            case "F":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Sasia" ? 1 : kusht.Alternativa == "Rreshti tjeter" ? 2 : 0;
                break;
            case "NC":
                if (kusht.Alternativa == 'Jo') {
                    ndryshocmime = false;
                    cmimemetvsh = false;
                }
                else
                    if (kusht.Alternativa == 'Jo (me TVSH)') {
                        ndryshocmime = false;
                        cmimemetvsh = true;
                    }
                    else {
                        ndryshocmime = true; cmimemetvsh = false;
                    }
                break;
            case "MKS":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Sinkrone";
                break;
            case "MNSA":
            case "GJUSHNMD":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po";
                hfState.Set(kusht.Kodi, kusht.Alternativa == "Jo");
                break;
            case "KGJAPMR":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po";
                hfState.Set("KGJAPMR", kusht.Alternativa == "Po");
                break;
            case "LSPK":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po";
                hfState.Set("LSPK", kusht.Alternativa == "Po");
                break;
            case "VF_VM":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Po";
                hfState.Set("VF_VM", kusht.Alternativa == "Po");
                $('#ValidoMarreveshje').modal({ show: false, backdrop: 'static', keyboard: false });
                if (kusht.Alternativa == "Po") {
                    txtIdMarreveshje.SetEnabled(false);
                    if ((pageState.lloji == "shtim" || (pageState.lloji == "klonim" && Utils.getUrlVar("modMarreveshje") != "modifikim")))
                        $('#ValidoMarreveshje').modal('show');
                }
                break;
            case "SHRK":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "PO";
                if (kusht.Alternativa == 'JO')
                    //me ane te ketij kushti e bejme nese do shfaqet apo jo grida e komisionit
                    $("#trupKonfigurimiKomision").parent().hide();
                else {
                    $("#trupKonfigurimiKomision").parent().show();
                    $("#divgrideKomision").show();
                }
                break;
            case 'CMIMKF':
                percaktoCmimPerKF = kusht.Alternativa;
                break;
            case "P":
                if (kusht.Alternativa == 'Pershkrimi 1')
                    pershk = 1;
                else pershk = 2;
                break;
            case "LPNJAG":
                if (kusht.Alternativa == 'Po')
                    konfirmimArt = 1;
                else konfirmimArt = 2;
                break;
            case "SAR":
                if (kusht.Alternativa == 'Po')
                    sasiautomatike = true;
                else sasiautomatike = false;
                break;
            case "AASG":
                if (kusht.Alternativa == 'Po')
                    merrSipasGrupit = true;
                else merrSipasGrupit = false;
                break;
            case "AADH":
                if (kusht.Alternativa == 'Po')
                    merrDhurata = true;
                else merrDhurata = false;
                break;
            case "LLD":
                LlojiDefault = kusht.Vlera;
                arrayMeLloje = konfLlojRreshti;
                break;
            case "ZSP":
                $('#hfSkema').val(kusht.Vlera);
                lblStatusApr.SetVisible(false);
                lblStatusAprovimi.SetVisible(false);
                if (hfState.Get("menuteSipasSkemes") != "") {
                    SuccedcallbackSkemaMenu(JSON.parse(hfState.Get("menuteSipasSkemes")));
                    hfState.Set("menuteSipasSkemes", "");
                }
                else {
                    var isNotModifikim = (pageState.lloji == 'modifikim' ? false : true);
                    var idDok = (Utils.getUrlVar("id") === undefined || Utils.getUrlVar("id") === "undefined" || typeof (Utils.getUrlVar("id")) === "undefined") ? 0 : Utils.getUrlVar("id");
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "MerrMenuPerPerdoruesSipasSkemes"), data: JSON.stringify({
                            idskema: kusht.Vlera,
                            idperdoruesi: $('#hfPerdoruesi').val(), isNotModifikim: isNotModifikim, status: lblStatusAprovimi.GetText(), idkokashitje: idDok, kodkonf: cmbModeli.GetText(), idlloji: 1
                        })
                    }).done(SuccedcallbackSkemaMenu);
                }
                if (kusht.Vlera != 0) {
                    if (pageState.lloji == 'modifikim') {
                        lblStatusApr.SetVisible(true);
                        lblStatusAprovimi.SetVisible(true);
                    }
                }
                break;
            case "NJ":
                if (kusht.Alternativa == 'Njesia 1')
                    njesiDef = 1;
                else
                    njesiDef = 2;
                break;
            case "KR":
                if (kusht.Alternativa == 'Po')
                    hfrivleresim.val(true);
                else
                    hfrivleresim.val(false);
                break;
            case "UPP":
                if (kusht.Alternativa == 'Po')
                    upp = true;
                else
                    upp = false;
                break;
            case "RKMK":
                if (kusht.Alternativa == 'Po')
                    kurskonvertimi = true;
                else
                    kurskonvertimi = false;
                hfState.Set('kurskonvertimi', kurskonvertimi);
                break;
            case "PDET1ART":
                if (kusht.Alternativa == 'Po')
                    hfState.Set('plotesoDetajim1', true);
                else
                    hfState.Set('plotesoDetajim1', false);
                break;
            case "RDDK":
                if (kusht.Alternativa == 'Po')
                    datekonvertimi = true;
                else
                    datekonvertimi = false;
                break;
            case "ZIA":
                if (kusht.Vlera != "0") {
                    idInfoArt = kusht.Vlera;
                    infoArt = true;
                }
                else
                    infoArt = false;
                break;
            case "ZILL":
                if (kusht.Vlera != "0") {
                    idInfoLlog = kusht.Vlera;
                    infoLl = true;
                }
                else
                    infoLl = false;
                break;
            case "DK":
                if (kusht.Vlera == "78") {
                    kasechrome = false;
                }
                else
                    kasechrome = true;
                break;
            case "MMK":
                if (kusht.Vlera == "80") {
                    mesazhriprintimi = true;
                }
                else
                    mesazhriprintimi = false;
                break;
            case "PPK":
                if (kusht.Vlera == "82") {
                    shfaqlupepaprintuar = true;
                }
                else
                    shfaqlupepaprintuar = false;
                break;
            case "ZIKF":
                if (kusht.Vlera != "0") {
                    infoKf = true;
                    idInfoKf = kusht.Vlera;
                    //$('#hfHapurMbyllur').val('True');
                    hapMbyllInfo($('#hfHapurMbyllur').val() == 'True');
                    callWebServiceInfoKF();
                }
                else
                    infoKf = false;
                break;
            case "TVSH":
                if (kusht.Alternativa == 'Ndermarje')
                    tvshkont = 1;
                else
                    if (kusht.Alternativa == 'Sipas Artikullit') {
                        tvshkont = 2;
                    }
                    else
                        if (kusht.Alternativa == 'Sipas Klientit') {
                            tvshkont = 4;
                        }
                        else
                            tvshkont = 3; //pa tvsh
                if (cmbDogana.GetText() === 'Po')
                    dogana = 1;
                break;
            case "RPKF":
                if (kusht.Alternativa == 'Pa klient/furnitor') {
                    regjistrimKF = 1;
                    btnKlienti.SetSelectedIndex(-1);
                    btnKlienti.SetEnabled(false);
                }
                else
                    if (kusht.Alternativa == 'Nje klient/furnitor') {
                        regjistrimKF = 2;
                        //if (hfLidhur.val() != "True" && !aprovim)
                        //    btnKlienti.SetEnabled(true);
                    }
                    else {
                        regjistrimKF = 3;
                        //if (hfLidhur.val() != "True" && !aprovim)
                        //    btnKlienti.SetEnabled(true);
                    }
                break;
            case "KKLKF":
                if (kusht.Alternativa == 'Po')
                    limitKF = 1;
                else
                    limitKF = 2;
                break;
            case "KGJMM":
                if (kusht.Alternativa == 'Po')
                    gjendjeartminmax = 1;
                else
                    gjendjeartminmax = 0;
                break;
            case "CZ":
                if (kusht.Alternativa == 'Lajmerues')
                    cmimzero = 1;
                else if (kusht.Alternativa == 'Bllokues')
                    cmimzero = 2;
                break;
            case "GJK":
                if (kusht.Alternativa == 'Jo')
                    hidField1.val(0);
                else
                    if (kusht.Alternativa == "Direkt")
                        hidField1.val(1);
                    else
                        hidField1.val(2);
                break;
            case "IA":
                if (kusht.Alternativa == 'Kod')
                    kodkodbar = 1;
                else
                    kodkodbar = 2;
                break;
            case "LNZAP":
                if (kusht.Alternativa == 'Jo')
                    hfState.Set("LNZAP", false);
                else
                    hfState.Set("LNZAP", true);
                break;
            case "KGJAG":
                if (kusht.Alternativa == 'Jo')
                    hfState.Set("KGJAG", false);
                else
                    hfState.Set("KGJAG", true);
                break;
            case "LZAN":
                if (kusht.Alternativa == 'Jo')
                    hfState.Set("LZAN", false);
                else
                    hfState.Set("LZAN", true);
                break;
            case "AD":
                if ($('#hfShtimModifikim').val() == "shtim")
                    hfState.Set("AD", kusht.Vlera);
                else
                    hfState.Set("AD", 0);
                break;
            case "NTDKV":
                if (kusht.Alternativa == 'Jo')
                    hfState.Set("NTDKV", false);
                else
                    hfState.Set("NTDKV", true);
                break;
            default:
                break;
        }
    }
    
    cmbModeli.ShowDropDown();
    cmbModeli.AdjustDropDownWindow();
    cmbModeli.HideDropDown();
    var magazinaDefault = null;
    for (var i = 0; i < colKontrollet.length; i++) {
        //kur perdoruesi ka te drejta te percaktuara qe te beje fatura vetem duke konvertuar urdhrat e shitjes, duhet te lejohet qe te modifikohen data dhe pershkrimi i dokumentit
        if (konvSipasUrdherShitje && colKontrollet[i].IdTipiKontrollit != 0 && colKontrollet[i].IdTipiKontrollit !== 5 && colKontrollet[i].KodKontrolli !== 'data_DateEdit' && colKontrollet[i].KodKontrolli !== 'txtPershkrimi') {
            var k = Utils.ktheKontroll(colKontrollet[i].KodKontrolli);
            if ($.isEmptyObject(k))
                continue;
            k.SetEnabled(false);
        }
       
        if (colKontrollet[i].KodKontrolli == 'btnKlienti') {
            //shtuar lloji i kursit duke qene se btnKlienti ishte si kontroll para kursit dhe ne cdo rast marrja e kursit behej para vendosjes se llojit te kursit
            var vleraDefaultKursi = colAtrTrupi.find(function (element) { return element.IdKontroll == colKontrollet.find(function (elementi) { return elementi.KodKontrolli == "txtKursi"; }).IdKontrolli; }).VlereDefault;
            if (vleraDefaultKursi != "") {
                llojkursi = vleraDefaultKursi;
            }
            if (colAtrTrupi[i].VlereDefault != "" && (pageState.lloji == "shtim" || pageState.lloji == "shtimraport" || pageState.lloji == "konvertimblerje") && !((pageState.lloji == 'konvertim' || pageState.lloji == 'konvertimblerje') && Utils.getUrlVar("klientKonv") == "-1")) {
                callWebserviceKF({ idKf: colAtrTrupi[i].VlereDefault, callBack: SucceededCallbackOKFDefault, mosPlotesoTeDhena: false, infoKf: true, kf: result.objekteDefault[colKontrollet[i].KodKontrolli] });
                kontrolloNrAutomatik = false;
            }
            pageState.idLupaKf = colAtrTrupi[i].IdKonfigAmbjenteLupa;
            //hfKl.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen ne fjale
            continue;
        }

        if (colKontrollet[i].KodKontrolli == 'btneKlientfurnitorVartes') {
            if (colAtrTrupi[i].VlereDefault != "" && (pageState.lloji == "shtim" || pageState.lloji == "shtimraport")) {
                //callWebserviceOKFVartesDefaultbyID(colAtrTrupi[i].VlereDefault)
                callWebserviceKF({ idKf: colAtrTrupi[i].VlereDefault, callBack: SucceededCallbackOKFDefaultVartes, kf: result.objekteDefault[colKontrollet[i].KodKontrolli] });
                kontrolloNrAutomatik = false;
            }
            hfKlvartes.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen ne fjale
            continue;
        }

        if (colKontrollet[i].KodKontrolli == "btneAutomjeti") {
            hfAuto.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }

        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it perkates
        if (colKontrollet[i].KodKontrolli == "btnMenyreTransporti") {
            hfTransp.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btnTransportues") {
            hfTr.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btnKushtDergimi") {
            hfDerg.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btnAgjenti") {
            hfAgj.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btnMaturimi") {
            hfMat.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btnKushtPagese") {
            hfPag.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "txtMarresi") {
            if (colAtrTrupi[i].VlereDefault != "") {
                marresi = true;
            }
            continue;
        }
 
        if (colKontrollet[i].KodKontrolli == "btnMagazina") {
            if (colAtrTrupi[i].VlereDefault != "" && pageState.lloji != 'modifikim')
                magazinaDefault = result.objekteDefault[colKontrollet[i].KodKontrolli];
            hfMag.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "cmbDegeAdministrative" && pageState.lloji != 'modifikim' && pageState.lloji != 'klonim') {
            if (colAtrTrupi[i].VlereDefault != "" && colAtrTrupi[i].VlereDefault != "0") {
                kaVlereDefaultDegaAdmin = true;
                cmbDegeAdministrative.SetSelectedItem(cmbDegeAdministrative.FindItemByValue(colAtrTrupi[i].VlereDefault));
                TextChangedDega();
            }
            else
                hapLupeValidimiPaFaturaPaPrintuar = true;
        }

        //if (colKontrollet[i].KodKontrolli == "cmbOperatori" && pageState.lloji != 'modifikim' && pageState.lloji != 'klonim') {
        //    if (colAtrTrupi[i].VlereDefault != "" && colAtrTrupi[i].VlereDefault != "0")
        //        cmbOperatori.SetSelectedItem(cmbOperatori.FindItemByValue(colAtrTrupi[i].VlereDefault));
        //}

        if (colKontrollet[i].KodKontrolli == "cmbPikeShitjeFurnizimi") {
            if (cmbPikeShitjeFurnizimi.GetText() == "" && colAtrTrupi[i].VlereDefault != "") {
                cmbPikeShitjeFurnizimi.SetSelectedItem(cmbPikeShitjeFurnizimi.FindItemByValue(colAtrTrupi[i].VlereDefault));
            }
            if (konvSipasUrdherShitje)
                cmbPikeShitjeFurnizimi.SetEnabled(false);
            continue;
        }

        if (colKontrollet[i].KodKontrolli == "cmbKonfigurimKase") {
            var item = null;
            if (result.KasePerdoruesi && result.KasePerdoruesi != 0)
                item = cmbKonfigurimKase.FindItemByValue(result.KasePerdoruesi);
            else if (colAtrTrupi[i].VlereDefault !== "")
                item = cmbKonfigurimKase.FindItemByValue(colAtrTrupi[i].VlereDefault);

            if (item != null) {
                cmbKonfigurimKase.SetSelectedItem(item);
                localStorage.setItem("kase_key" + pageState.idNdermarrje + "_" + pageState.idPerdoruesi, item.text);
            }
        }

    }
    
    if (btnAgjenti && result && result.objekteDefault && result.objekteDefault["btnKlienti"] && (pageState.lloji != "konvertim" && pageState.lloji != "konvertimblerje")) {
        klienti = result.objekteDefault["btnKlienti"].kf;
        vendosAgjentinDefaultTeKlientit(klienti.IdPerfaqesuesShitje, btnAgjenti, klienti.PerqindjeAgjenti, txtPerqindjeAgjent, "1");
        vendosAgjentinDefaultTeKlientit(klienti.IdPerfaqesuesShitje2, btnAgjenti2, klienti.PerqindjeAgjenti2, txtPerqindjeAgjent2, "2");
        vendosAgjentinDefaultTeKlientit(klienti.IdPerfaqesuesShitje3, btnAgjenti3, klienti.PerqindjeAgjenti3, txtPerqindjeAgjent3, "3");
    }



    if (cmbMonedha.GetText() == '')
        cmbMonedha.SetSelectedIndex(0);
    if (cmbMonedhaPagese.GetText() == '')
        cmbMonedhaPagese.SetSelectedIndex(0);

    vendosDateDefault(pageState.lloji);
    if (pageState.lloji == "shtim" || pageState.lloji == "shtimraport" || ((pageState.lloji == "konvertim" || pageState.lloji == "konvertimblerje") && !kurskonvertimi)) {
        if (btnKlienti.GetValue() == null && (txtKursi.GetText() == '' || txtKursi.GetText() == '0'))
            callWebserviceKursi(cmbMonedha.GetValue());
        callWebserviceKursiSipasLlojitPagese(cmbMonedhaPagese.GetValue(), llojkursi);
        pastroFundin(); 
    }

    hapMbyllInfo($('#hfHapurMbyllur').val() == 'True');
    if (pageState.lloji == 'modifikim')
        cmbNiveli.SetEnabled(false);

    resultkonvertime = new Array();
    countkonvertime = 0;
    grida.jqGrid('GridUnload', "rowed5");
    var isLidhur;
    if (pageState.lloji == "klonim" && Utils.getUrlVar("modMarreveshje") == "modifikim")
        isLidhur = false;
    else if (pageState.aprovim || konvSipasUrdherShitje)
        isLidhur = true;
    else
        isLidhur = (hfLidhur.val().toLowerCase() === 'true');


    if (hfTeDrejta.Get("NdryshoZbritjeTotale") == false || isLidhur || konvSipasUrdherShitje || pageState.lloji == "kthim" || hfState.Get("eshteDokKthimi")) {
        txtPerqindje.SetEnabled(false);
        txtVlefte.SetEnabled(false);
    }
    else {
        txtPerqindje.SetEnabled(true);
        txtVlefte.SetEnabled(true);
    }
    if (magazinaDefault != null)
        vendosVleraDefaultTeKoka("btnMagazina", magazinaDefault);

    if (hfState.Get("AD") > 0)
        callWebserviceArtikulliIPlote({ idArt: hfState.Get("AD"), idRresht: 1, magazine: magazinaDefault == null ? grida.getTekstQelize('txtMagazina', 1) : magazinaDefault.Kodi, kodKodbarArt: "", peshoreArt: "", listeIMEIArtikull: new Array(), sasiaNeGride: new Object() });

    formGridColsArray(isLidhur);
    grida = inicializoGride(isLidhur, "#rowed5");
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    if ((pageState.lloji !== "shtim" && pageState.lloji !== "shtimraport" && Utils.findObjectByAttribute(JSON.parse($('#HfColTrup').val()), "MeKomision", true) != null)
        || ((pageState.lloji == "shtim" || pageState.lloji == "shtimraport") && pageState.kushte["SHRK"])) {
        $("#rowed6").jqGrid('GridUnload');
        var gridakomision = inicializoGride(true, "#rowed6");
    }
    mbushGrideNgaHiddenFieldet(isLidhur);
    
    $("input[id$='hfVodOne']").val('');

    $("input[id$='hfZbritja']").val('');
    if (pageState.lloji == 'modifikim' && cmbModeli.GetText().substring(0, 5) == "VFONE") {
        var idDok = (Utils.getUrlVar("id") === undefined || Utils.getUrlVar("id") === "undefined" || typeof (Utils.getUrlVar("id")) === "undefined") ? 0 : parseInt(Utils.getUrlVar("id"));
        MerrPiketNeModifikimTeVfoneMeWebservice(idDok);
    }

    if (Utils.getUrlVar("idartikulli") != undefined && Utils.getUrlVar("idartikulli") != "undefined" && vendosartikullblerje) {
        callWebserviceArtikulliIPlote({ idArt: Utils.getUrlVar("idartikulli"), idRresht: 1, magazine: "", kodKodbarArt: "", peshoreArt: "", listeIMEIArtikull: new Array(), sasiaNeGride: new Object() });
        vendosartikullblerje = false;
        if (Utils.getUrlVar("shitje_blerje") == 'shitje') {
            $("input[id$='hfVlera']").val(Utils.getUrlVar("zbritje"));
        }
        else if (Utils.getUrlVar("shitje_blerje") == 'shitjediscount') {
            // $("input[id$='hfZbritja']").val(100);
            cmbLlojZbritje.SetValue(Utils.llojZbritje.Perqindje);
            $("input[id$='hfVlera']").val(Utils.getUrlVar("zbritje"));
            //  $("input[id$='hfZbritja']").val(Utils.getUrlVar("zbritje"));
        }
        $("input[id$='hfPiket']").val(Utils.getUrlVar("pike"));
        txtPike.SetText(Utils.getUrlVar("pike"));
    }
    else $("input[id$='hfKodVFOne']").val('');

    if (Utils.getUrlVar("zbritje") != "undefined" && cmbModeli.GetText().substring(0, 5) == "VFONE") {
        $("input[id$='hfVlera']").val(Utils.getUrlVar("zbritje"));
    }

    if (hapLupeValidimi && hapLupeValidimiPaFaturaPaPrintuar) {
        ButtonClickValidim();
    }
    lblKonfigurimi.SetVisible(false);

    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

    if (pageState.kushte.RVF) {
        if (localStorage.degeadmshitje && cmbDegeAdministrative.GetText() == '') {
            cmbDegeAdministrative.SetText(localStorage.degeadmshitje);
            TextChangedDega();
        }
        if (localStorage.magazinashitje && btnMagazina.GetText() == '') {
            btnMagazina.SetText(localStorage.magazinashitje);
            previousMag = btnMagazina.GetSelectedItem();
            TextChangedMagazina();
        }
        if (localStorage.dateshitje && localStorage.dateshitje !== "" && data_DateEdit.GetDate().format('dd/MM/yyyy') === Utils.ktheDateDefault(pageState.periudha).format('dd/MM/yyyy')) {
            var dateShitje = new Date(JSON.parse(localStorage.dateshitje));
            data_DateEdit.SetDate(dateShitje);
            cmbMuajRaportimi.SetValue(dateShitje.getMonth() + 1);
            cmbVitRaportimi.SetText(dateShitje.getFullYear());
            dtDok_changed(data_DateEdit, undefined, true);
        }
    }

    if (btnKlienti.GetText() == "") {
        if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && cmbKarta.GetVisible())
            callwebserviceMbushKarta(0);
    }

    if (pageState.Lloji == 'konvertim' && $('#hfDokKontrate').val() == "PO") {
        lblFaza.SetVisible(true);
        cmbFaza.SetVisible(true);
        btnFazat.SetVisible(false);
    }

    kontrolloNrAutomatik = true;
    previousMag = btnMagazina.GetSelectedItem();
    pageState.EshteVisibletxtTotal2 = EshteVisible(colKontrollet, colAtrTrupi, "txtTotal2");
    pageState.EshteVisibletxtTotalMeZbritje2 = EshteVisible(colKontrollet, colAtrTrupi, "txtTotalMeZbritje2");
    pageState.EshteVisibletxtTVSH2 = EshteVisible(colKontrollet, colAtrTrupi, "txtTVSH2");
    pageState.EshteVisibletxtTotaliPaTVSH2 = EshteVisible(colKontrollet, colAtrTrupi, "txtTotaliPaTVSH2"); pageState.EshteVisibletxtTotaliMeZbritjePaTVSH2 = EshteVisible(colKontrollet, colAtrTrupi, "txtTotaliMeZbritjePaTVSH2");
    pageState.EshteVisiblelblMonedhaBaze = EshteVisible(colKontrollet, colAtrTrupi, "lblMonedhaBaze");
    btneKlientfurnitorVartes.SetMaxItems(!pageState.kushte["VF_VM"] ? 1 : null);
    btneKlientfurnitorVartes.Init();
    if (pageState.kushte["VF_VM"]) {
        if (pageState.lloji == "modifikim" && lblStatusAprovimi.GetText() == 'Aprovuar')
            cmbStatusMarreveshje.SetEnabled(true);

        if ((pageState.lloji == "modifikim" || pageState.lloji == "klonim") && cmbStatusMarreveshje.GetValue() == 1 && DtMbarimi_DateEdit.GetDate() < Utils.ktheDateServeriFromCookies())
            lblExpired.SetVisible(true);
        else
            lblExpired.SetVisible(false);
    }

    kuponKerkonKonfigurim = false;
    if (pageState.lloji == "modifikim" && txtNIVF.GetText() != "") {
        for (var i = 0; i < pageState.colKontrollet.length; i++) {

            var k = Utils.ktheKontroll(colKontrollet[i].KodKontrolli);

            if ($.isEmptyObject(k) || colKontrollet[i].KodKontrolli == 'rowed5')
                continue;
            console.log(colKontrollet[i].KodKontrolli);
            k.SetEnabled(false);
            if (txtEIC.GetText() == "") {
                cbEinvoice.SetEnabled(true);
                cmbProcesi.SetEnabled(true);
                cmbeInvoiceType.SetEnabled(true);
            }


        } cmbFormatiPrintimit.SetEnabled(true);
        lblFormatiPrintimit.SetEnabled(true);
        lejomod = false;
    }
}
function pastroFundin() {
    txtTotal1.SetText('0');
    txtTotalMeZbritje1.SetText('0');
    txtTVSH1.SetText('0');
    txtTotaliPaTVSH1.SetText('0'); 
}



function vendosVleraDefaultTeKoka(kodKontrolli, result) {
    if (!result)
        return;
    switch (kodKontrolli) {
        case "btnMagazina":
            Utils.SelectComboItem(btnMagazina, result.IdNjesiAdministrative, result.Kodi, result.Pershkrimi);
            TextChangedMagazina();
            break;
        default:
            break;
    }
}

function EshteVisible(colKontrollet, colAtrTrupi, kontrolliMB) {
    return (colKontrollet.find != undefined) ?
        EshteVisibleUsingFind(colKontrollet, colAtrTrupi, kontrolliMB)
        :
        EshteVisibleCompatibility(colKontrollet, colAtrTrupi, kontrolliMB);
    /*
        funksioni find() suportohet nga versioni 45 i chrome e me siper, dhe vetem nga versionet me te fundit
        te browserave te tjere
    */

}

function EshteVisibleUsingFind(colKontrollet, colAtrTrupi, kontrolliMB) {
    var attr = undefined;
    var kontroll = colKontrollet.find(function (kontrolli) { return kontrolli['KodKontrolli'] == kontrolliMB; });
    if (kontroll != undefined) {
        attr = colAtrTrupi.find(function (atributi) { return atributi['IdKontroll'] == kontroll.IdKontrolli; });
    }
    return attr != undefined && attr.Visible == true;
}

function EshteVisibleCompatibility(colKontrollet, colAtrTrupi, kontrolliMB) {
    var idKontrolli = Utils.findFieldValueByAttribute(colKontrollet, "KodKontrolli", "IdKontrolli", kontrolliMB);
    var visible = Utils.findFieldValueByAttribute(colAtrTrupi, "IdKontroll", "Visible", idKontrolli);
    return visible;
}

function spliterPaneResized(s, e) {
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth($(pageState.gridaSelector), $('#divgride2'));
    }
    if ($('#divgride2Komision').width() != null) {
        myJQGrid.fixGridWidth($(pageState.gridaKomision), $('#divgride2Komision'));
    }
}
//duhet shtuar edhe rasti tjeter


function SuccedcallbackSkemaMenu(result) {
    if (hfState.Get("Meme") && pageState.veprimi == "blerje") {
        vodafoneExeptionMenu(ASPxMenu1);
        return;
    }

    ASPxMenu1.GetItemByName('Ruaj').SetVisible(result[0]);
    if (pageState.lloji == 'kthim')
        ASPxMenu1.GetItemByName('Draft').SetVisible(false);
    else
        ASPxMenu1.GetItemByName('Draft').SetVisible(result[1]);

    if (cmbModeli.GetText().length >= 5 && (cmbModeli.GetText().substring(0, 5) == "VFONE")) {
        if (!hfState.Get("RoliSR")) {
            ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
            ASPxMenu1.GetItemByName('Draft').SetVisible(true);
            cmbLlojZbritje.SetValue(Utils.llojZbritje.Perqindje);
        }
        else
            ASPxMenu1.GetItemByName('Draft').SetVisible(false);
    }
    ASPxMenu1.GetItemByName('Aprovo').SetVisible(result[2]);
    ASPxMenu1.GetItemByName('Refuzo').SetVisible(result[3]);
    ASPxMenu1.GetItemByName('Delego').SetVisible(result[4]);
    ASPxMenu1.GetItemByName('Komento').SetVisible(result[5]);
    ASPxMenu1.GetItemByName('Modifiko').SetVisible(result[6]);
    try {
        ASPxMenu1.GetItemByName('Konverto').SetVisible(result[7]);
        ASPxMenu1.GetItemByName('Paguaj').SetVisible(result[9]);
    }
    catch (ex) {

    }
    ASPxMenu1.GetItemByName('Shto').SetVisible(result[8]);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(result[10]);
    if (cmbModeli.GetText().length >= 5 && (cmbModeli.GetText().substring(0, 5) == "VFONE") && Utils.getUrlVar("kthehu") == "kthehu") {
        ASPxMenu1.GetItemByName('Draft').SetVisible(true);
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        ASPxMenu1.GetItemByName('Validim').SetVisible(false);
        cmbLlojZbritje.SetValue(Utils.llojZbritje.Perqindje);
    }
    if (cmbModeli.GetText().length >= 5 && (cmbModeli.GetText().substring(0, 5) == "USHDD") && $("input[id$='hfShtimModifikim']").val() == "konvertim") {
        ASPxMenu1.GetItemByName('Validim').SetVisible(false);
    }
    if (cmbModeli.GetText().length >= 6 && (cmbModeli.GetText().substring(0, 6) == "BAZAAR") && $("input[id$='hfShtimModifikim']").val() == "konvertim") {
        ASPxMenu1.GetItemByName('Validim').SetVisible(false);
    }
    if (hfState.Get('Status') == 0 && $("input[id$='hfShtimModifikim']").val() == 'modifikim')
        try {
            ASPxMenu1.GetItemByName('Validim').SetVisible(true);
        }
        catch (exx) {

        }

    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar'))
        ASPxMenu1.GetItemByName('Validim').SetVisible(result[11]);


    if ($("input[id$='hfShtimModifikim']").val() == "modifikim" && hfState.Get("Status") == 4 && hfState.Get("Meme") == false)
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
}

function vodafoneExeptionMenu(container) {
    if (container.GetItemCount() > 0) {
        for (var i = 0; i < container.GetItemCount(); i++) {
            var item = container.GetItem(i);
            item.SetVisible(false);
        }
    }
    ASPxMenu1.GetItemByName('Anullo').SetVisible(true);
}


function callWebServiceDetyrimiKF(idKlientFurnitori) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheDetyrimi"),
        data: JSON.stringify({
            idKlientFurnitori: idKlientFurnitori,
            idKokaShitje: pageState.lloji == 'modifikim' ? Utils.getNumberOrDefaultFromUrl('id') : 0,
            date: data_DateEdit.GetDate(), idNdermarrje: pageState.idNdermarrje
        })
    }).done(SuccedeCallbackDetyrimi);
}

function dtDok_changed(s, e, ndryshoDate) {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    ndryshuarData = ndryshoDate;
    callWebserviceKursi(cmbMonedha.GetValue());
    LostFocusMaturimi();
    var selectedKF = btnKlienti.GetSelectedItem();
    if (selectedKF != null) {
        callWebServiceDetyrimiKF(selectedKF.value);
    }
    //var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    //var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    if (pageState.lloji != 'modifikim')
        myNrAuto.vendosNrAutomatik(pageState.colAtrTrupi, pageState.colKontrollet, data_DateEdit.GetDate());
    callWebServiceInfoRow(idRresht);
    callWebServiceInfoKF();
    dteAfatiKohor.SetDate(data_DateEdit.GetDate());
    DtFillimi_DateEdit.SetDate(data_DateEdit.GetDate());
    DtMbarimi_DateEdit.SetDate(data_DateEdit.GetDate());
    DtFature_DateEdit.SetDate(data_DateEdit.GetDate());
    cmbMuajRaportimi.SetValue(data_DateEdit.GetDate().getMonth() + 1);
    cmbVitRaportimi.SetText(data_DateEdit.GetDate().getFullYear());
    if (!dateMaturimi_DateEdit.GetVisible())
        dateMaturimi_DateEdit.SetDate(data_DateEdit.GetDate());

    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        var idRreshti = ids[i];
        if (grida.getTekstQelize('cmbLloji', idRreshti) !== 'Artikull')
            continue;
        kontrolloGjendje(grida.getTekstQelize('txtIdKodi', idRreshti), grida.getTekstQelize('txtMagazina', idRreshti), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRreshti), grida.getTekstQelize('txtDetajimi2t', idRreshti), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRreshti);
    }
}


function vendosDetyrimi(detyrimi, idKlientFurnitor) {
    txtDetyrimi.SetText(detyrimi);
    if (parseFloat(txtLimit.GetText()) === 0 && parseFloat(txtKrediti.GetText()) === 0)
        txtDetyrimi.inputElement.style.color = 'black';
    else if (parseFloat(detyrimi) < parseFloat(txtKrediti.GetText()))
        txtDetyrimi.inputElement.style.color = 'green';
    else if (parseFloat(detyrimi) >= parseFloat(txtKrediti.GetText()) && parseFloat(detyrimi) < parseFloat(txtLimit.GetText()))
        txtDetyrimi.inputElement.style.color = 'orange';
    else if (parseFloat(detyrimi) >= parseFloat(txtLimit.GetText()))
        txtDetyrimi.inputElement.style.color = 'red';
}

function SuccedeCallbackDetyrimi(result) {
    vendosDetyrimi(result.detyrimi, btnKlienti.GetSelectedItem().value);
    hfState.Set("detyrimiMeparshem", result.detyrimiMeparshem);
}

var konfirmimArt = 2;
var arrayLlojet;
var njesiDef = 1;
var tvshkont = 3;
var regjistrimKF = 1;
var limitKF = 2;
var gjendjeartminmax = 0;
var cmimzero = 0;

var dogana = 2;
/*
Function: ndryshoKonfigurimin

Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin(pastroSeriale) {//po
    var cmbModKontroll = cmbModeli.GetSelectedItem();
    var pershkKonfigAmb = cmbModKontroll.GetColumnText("PershkrimKonfigAmbjente");
    var pershKokeDok = hfState.Get("MenuKokeDokumenti");
    if (pershkKonfigAmb != undefined) {
        lblKonfigurimi.SetText(pershkKonfigAmb);
        lblKonfigurimi.SetVisible(false);
        $('#kokeKonfigurimi').text(pershKokeDok + ': ' + pershkKonfigAmb);
    }
    callWebserviceKonfig(506, cmbModKontroll.GetColumnText("KodKonfigAmbjente"), pastroSeriale);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

/*
Function: ButtonClickKlienti

Hap lupen e klienteve/furnitoreve.
*/
function ButtonClickKlienti() {//po
    var klientfurnitor;
    editorkf = "1";

    for (j = 0; j < colKushte.length; j++)
        if (colKushte[j].Kodi == 'KF')
            klientfurnitor = colKushte[j].Alternativa;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKF"));
    popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + pageState.idLupaKf + "&KlientapoFurnitor=" + klientfurnitor);
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
}

function ButtonClickKlientiVartes() {//po
    editorkf = "2";
    var hfKlvartes = $("#hfLupaKlientFurnitorvartes");
    var queryStr = hfKlvartes.val();

    var kfkryesor = "";
    if (btnKlienti.GetValue())
        kfkryesor = btnKlienti.GetValue();

    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKF"));
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar'))
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + queryStr + '&veprimi=' + 1 + '&kfkryesor=' + kfkryesor + '&idKfV=' + btneKlientfurnitorVartes.GetText());
    else
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + queryStr + '&veprimi=' + 2 + '&kfkryesor=' + kfkryesor + '&idKfV=' + btneKlientfurnitorVartes.GetText());
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
}

function ShtoNeKomboKfVartes(ids) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KtheKlientFurnitorSipasIdve"),
        data: JSON.stringify({
            ids: ids.join()
        })
    }).done(MbushKFVartes);
}

function MbushKFVartes(result) {
    var values = [];
    for (var i = 0; i < result.length; i++) {
        values[i] = result[i].IdKlientFurnitor;
        if (!btneKlientfurnitorVartes.Contains(result[i].IdKlientFurnitor)) {
            pageState.KFVartesNgaNgarkimiDokumentit = false;
            btneKlientfurnitorVartes.AddOption(result[i]);
        }
    }
    btneKlientfurnitorVartes.SetValue(values);
}


function ButtonClickKarta() {//po
    var idklient = 0;
    if (pageState.kushte.LKVK) idklient = btnKlienti.GetValue();
    popupUniversal.SetHeaderText("Zgjidh Karten e Klientit");
    //popupUniversal.SetContentUrl('LupaKartaKlienti.aspx?idKlienti=' + idklient);
    popupUniversal.SetContentUrl('Shto_KartaKlienti.aspx?celjeShpejte=po&idKlienti=' + idklient);
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
}

/*
Function: Auto_Click

Hap lupen e automjeteve.
*/
function Auto_Click() {//po
    var hfAuto = $("#hfLupaAutomjet");
    var queryStr = hfAuto.val();
    var klienti = '';
    if (pageState.kushte.LAVK === true) {
        if (btnKlienti.GetText() != '') {
            klienti = btnKlienti.GetValue();
            popupUniversal.SetContentUrl('LupaAutomjeti.aspx?idKonfigAmbjente=' + queryStr + '&klienti=' + klienti);
        }
        else
            popupUniversal.SetContentUrl('LupaAutomjeti.aspx?idKonfigAmbjente=' + queryStr);
    }
    else
        popupUniversal.SetContentUrl('LupaAutomjeti.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhAutomjetin"));
    popupUniversal.SetSize(widthLupaAutomjet, heightLupaAutomjet);
    popupUniversal.Show();
}

function textChangedAuto(s, e) {
    var idAuto = btneAutomjeti.GetValue();
    if (!idAuto || idAuto == -1)
        return;
    try {
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheTargeAutomjeti"), data: JSON.stringify({ idAutomjeti: idAuto }) }).done(SucceededCallbackTargeAutomjeti);

        if (btnKlienti.GetText() == '') {
            try {
                $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientAutomjeti"), data: JSON.stringify({ idAutomjeti: idAuto }) }).done(SucceededCallbackKlientAutomjeti);
            }
            catch (e) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiNjeGabimGjateMarrjesSeMonedhes"));
            }
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit"));
    }
}

//kur ndryshon automjeti ndryshohet dhe targa e tij
function SucceededCallbackTargeAutomjeti(result) {
    if (result != null)
        txtTarga.SetText(result);
}

// Kur ndryshohet automjeti vihet klienti i atij automjeti
function SucceededCallbackKlientAutomjeti(result) {
    if (result != null)
    Utils.SelectComboItem(btnKlienti, result.IdKlientFurnitor, result.KodKlientFurnitor, result.EmertimiKF);
}

/*
Function: ButtonClickMenyreTransporti

Hap lupen e menyrave te transportit.
*/
function ButtonClickMenyreTransporti() {//po
    var hfTransp = document.getElementById("hfLupaMenyreTransporti");
    var queryStr = hfTransp.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMenyrenTransportit"));
    popupUniversal.SetContentUrl('LupaMenyraTransporti.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMenyraTransporti, heightLupaMenyraTransporti);
    popupUniversal.Show();
}

/*
Function: ButtonClickTransportues

Hap lupen e transportuesit.
*/
function ButtonClickTransportues() {//po
    identikuesPerPopupTransportuesi = "RegjistrimDokumentash";
    var hfTr = document.getElementById("hfLupaTransportues");
    var queryStr = hfTr.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhTransportuesin"));
    popupUniversal.SetContentUrl('Shto_Transportues.aspx?lupe=true&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaTransportues, heightLupaTransportues);
    popupUniversal.Show();
}

/*
Function: ButtonClickAgjenti

Hap lupen e agjenteve te shitjes.
*/
function ButtonClickAgjenti(agjenti) {//po
    var hfAgj = document.getElementById("hfLupaAgjentShitje");
    var queryStr = hfAgj.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhAgjentShitje"));
    popupUniversal.SetContentUrl('LupaAgjenteShitje.aspx?idKonfigAmbjente=' + queryStr + '&agjenti=' + agjenti);
    popupUniversal.SetSize(widthLupaAgjenti, heightLupaAgjenti);
    popupUniversal.Show();
}


function ButtonClickFazat() {//po
    var hfid = $('#hfIdKontrata').val();
    var hfMerrNgaSesioni = $('#hfMerrFazaNgaSesioni').val();
    popupUniversal.SetHeaderText("Shto Fazat");
    popupUniversal.SetContentUrl('Shto_FazaKontrate.aspx?id=' + hfid + '&merrngasesioni=' + hfMerrNgaSesioni);
    popupUniversal.SetSize(widthLupaFazat, heightLupaFazat);
    popupUniversal.Show();
}

var arka = false;
/*
Function: ButtonClickArka

Hap lupen e arkes.
*/
function ButtonClickArka(agjenti) {//po
    if (cmbMenyrePagese.GetValue() == 9)
        arka = true;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhArkenBankenLupa"), 'LupaBanka.aspx?vjenNga=RegjDokumentash&arka=' + arka, 650, 600);
}

function TextChangedArka() {
    var menyrePagese = cmbMenyrePagese.GetValue();
    if (menyrePagese == 5 || menyrePagese == 9 || menyrePagese == 4) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "merrBankaSipasKodit"),
            data: JSON.stringify({ kodBanka: btneArka.GetText(), idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
        }).done(SucceededCallbackArka);
    }
    else {
        btneArka.SetText('');
    }
}

function SucceededCallbackMbushArka(result) {
    for (i = 0; i < result.length; i++) {
        btneArka.AddItem(result[i].KodiBanka, result[i].IdBanka);
    }

}

function SucceededCallbackArka(result) {
    // Utils.SelectComboItem(btneArka, result.IdBanka, result.EmerBanka, result.KodiBanka);
    btneArka.SetText(result.KodiBanka);
}

/*
Function: ButtonClickKushtDergimi

Hap lupen e kushteve te dergimit.
*/
function ButtonClickKushtDergimi() {//po
    var hfDerg = document.getElementById("hfLupaMenyreTransporti");
    var queryStr = hfDerg.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKushteDergimi"));
    popupUniversal.SetContentUrl('LupaKushteDergimi.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKushteDergimi, heightLupaKushteDergimi);
    popupUniversal.Show();
}

/*
Function: ButtonClickKushtPagese

Hap lupen e kushteve te pageses.
*/
function ButtonClickKushtPagese() {//po
    var hfPag = document.getElementById("hfLupaKushtPagese");
    var queryStr = hfPag.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKushtePagese"));
    popupUniversal.SetContentUrl('LupaKushtePagese.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKushtePagese, heightLupaKushtePagese);
    popupUniversal.Show();
}

/*
Function: ButtonClickAfateMaturimi

Hap lupen e afateve te maturimit.
*/
function ButtonClickAfateMaturimi() {//po
    var hfMat = document.getElementById("hfLupaAfatMaturimi");
    var queryStr = hfMat.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhAfateMaturimi"));
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar'))
        popupUniversal.SetContentUrl('LupaAfateMaturimi.aspx?veprimi=2&idKonfigAmbjente=' + queryStr);
    else popupUniversal.SetContentUrl('LupaAfateMaturimi.aspx?veprimi=1&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMaturimi, heightLupaMaturimi);
    popupUniversal.Show();
}

function IndexChangedKlienti(s, e) {//povendosArtPlote
    monklienti = "0";
    txtTarga.SetText('');
    txtPike.SetText('0');
    btnAgjenti2.SetValue(null);
    txtPerqindjeAgjent2.SetText('');
    btnAgjenti3.SetValue(null);
    txtPerqindjeAgjent3.SetText('');
    btnAgjenti.SetValue(null);
    txtPerqindjeAgjent.SetText('');
  
    if (isNaN(btnKlienti.GetValue()) || btnKlienti.GetValue() == null || s.GetSelectedIndex() == -1) {
        s.Focus();
        return;
    }
    if (s.GetValue() != "")
        callWebserviceKF({ idKf: s.GetValue(), callBack: SucceededCallbacOKF, mosPlotesoTeDhena: false, infoKf: true });
    if (txtMarresi.GetText() == '' || marresi == false) {
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "merrEmertimKlienti"), data: JSON.stringify({ idKlient: btnKlienti.GetValue() == null ? 0 : btnKlienti.GetValue() }) }).done(SucceededCallbackMarresi);
    }
    
}

function IndexChangedTransportues(s, e) {
    if (isNaN(btnTransportues.GetValue()) || btnTransportues.GetValue() == null || btnTransportues.GetSelectedIndex() == -1) {
        btnTransportues.SetText('');
        return;
    }
}

function SucceededCallbackFormatNumri(formatNumri) {
    var grida = $(pageState.gridaSelector);
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosKonfigFormatNumri();
}

//funksion per te dalluar nese thirrja nga lupa e kf vjen per te mbushur lupen e kf kryesor apo vartes, therrasim webservice perkates per secilen dhe vendosim vleren ne lupe

function callWebserviceKFDefault(idKf, mosPlotesoTeDhena, ngaLupa) {
    if (editorkf === "1")
        callWebserviceKF({ idKf: idKf, callBack: SucceededCallbacOKF, mosPlotesoTeDhena: mosPlotesoTeDhena, infoKf: true, ngaLupa: ngaLupa });
    else
        callWebserviceKF({ idKf: idKf, callBack: SucceededCallbackOKFDefaultVartes });
}
function vendosKf(params, result) {
    if (params.mosPlotesoTeDhena)
        result.mosPlotesoTeDhena = params.mosPlotesoTeDhena;
    params.callBack(result);
    if (params.ngaLupa)
        TextChangedKlienti();
}

function callWebserviceKF(params) { //{idKf:idKf,callBack:callBack, mosPlotesoTeDhena:mosPlotesoTeDhena, infoKf: true}
    /// <summary>Therret webservice per te marre objektin e klientfurnitorit ne server</summary>
    /// <param name="idKf" type="Number">Id-ja e klientFurnitorit</param>
    /// <param name="mosPlotesoTeDhena">duhet per rastin kur nuk duhen plotesuar disa te dhena - pyet Nestilen</param>
    if (!params)
        return;
    var idKonfig = cmbModeli.GetValue();
    if (!params)
        return;
    if (params.kf)
        vendosKf(params, params.kf);
    else
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheOKlientFurnitor"), data: JSON.stringify({ idKlientFurnitor: params.idKf, date: data_DateEdit.GetDate(), idKonfigurimi: idKonfig, idNdermarrje: pageState.idNdermarrje, idPerdorues: pageState.idPerdoruesi, idKomponente: 506, idGjuha: pageState.idGjuha, llojKursi: llojkursi })
        }).done(function (result) { vendosKf(params, result); });
    if (params.infoKf)
        callWebServiceInfoKF();
}

function callWebServiceKategorizbritje(kf, date) {//po
    var grida = $(pageState.gridaSelector);
    var totali = 0;
    var ids = grida.jqGrid('getDataIDs');
    for (var i = 0; i < ids.length; i++) {
        if (grida.getTekstQelize('txtKodi', ids[i]) == '')
            continue;
        var vlefta = grida.getTekstQelize('txtVlefta', ids[i]);
        if (vlefta == "")
            vlefta = 0;
        totali = totali + vlefta;
    }
    var kursi = 1;
    if (txtKursi.GetText() != "")
        kursi = txtKursi.GetText();
    if (isNaN(totali))
        totali = 0;

    if ($("input[id$='hfPolitike']").val() != "") {
        var politike = JSON.parse($("input[id$='hfPolitike']").val());
        var pikeTotale = parseFloat($('#hfTotalPikesh').val());

        if (politike.Lloji == 0 || politike.Lloji == 2) {//politike zbritje me % ose politike zbritje dhe pike
            var karta = JSON.parse($("input[id$='hfKarta']").val());
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKategoriZbritjeKarteKlienti"), data: JSON.stringify({ idKatZbritje: karta.IdKategoriZB, date: date, vlefte: totali, kursi: kursi, idmonedha: cmbMonedha.GetValue() })
            }).done(SucceededCallbacKategoriZbritjeKarta);
        }

        if (politike.Lloji == 1) {  //  || politike.Lloji == 2 vetem ne rastin e kartave me pike te vendoset zbritja nga klienti
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKategoriZbritje"), data: JSON.stringify({
                    kf: kf, date: date, vlefte: totali, kursi: kursi, idmonedha: cmbMonedha.GetValue(), idNdermarrje: pageState.idNdermarrje
                })
            }).done(SucceededCallbackKategori);
            txtPike.SetText(Math.floor(parseFloat(txtTotal2.GetText() / politike.VleraPikes)));
            txtTotalPike.SetText(pikeTotale + parseFloat(txtPike.GetText()));
        }
    }
    else {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKategoriZbritje"),
            data: JSON.stringify({ kf: kf, date: date, vlefte: totali, kursi: kursi, idmonedha: cmbMonedha.GetValue(), idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackKategori);
    }
}

function aplikoZbritjeKlienti() {
    return (pageState.Kf.alternativaKushtZbritje == "Te dyja" || ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && pageState.Kf.alternativaKushtZbritje == "Dokument shitje") || (pageState.veprimi == "blerje" && pageState.Kf.alternativaKushtZbritje == "Dokument blerje"));
}

function setZbritjeTotal(llojZbritje, zbritje, changeZbritje) {
    if (llojZbritje == Utils.llojZbritje.Perqindje) {
        txtPerqindje.SetText(zbritje);
        if (changeZbritje)
            cmbLlojZbritje.SetValue(Utils.llojZbritje.Perqindje);
        //txtVlefte.SetEnabled(false);
        //txtPerqindje.SetEnabled(true);
    }
    else {
        txtVlefte.SetText(zbritje);
        if (changeZbritje)
            cmbLlojZbritje.SetValue(Utils.llojZbritje.Vlere);
        //txtPerqindje.SetEnabled(false);
        //txtVlefte.SetEnabled(true);
    }
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    updateTotalet(txtPerqindje.GetText(), txtVlefte.GetText());
}

function SucceededCallbackKategori(result) {//po
    if (result == null) { //kevi bugu 7659
        result = { eshtePerqindje: true, zbritje: 0 };
    }
    if (!aplikoZbritjeKlienti())
        return;

    setZbritjeTotal(result.eshtePerqindje ? Utils.llojZbritje.Perqindje : Utils.llojZbritje.Vlere, result.zbritje, true);
}

function callWebserviceAdresatKF(name) {//po
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheAdresatKlientFurnitor"),
        data: JSON.stringify({ prefixText: name, idNdermarrje: pageState.idNdermarrje })
    }).done(SucceededCallbackAdresatKF);
}

var monklienti = '0';
var meTVSH = false;
var oColTrupatKategoriteZbritjes;
var updateCmimiGridToDo = false;
//var alternativaKushtZbritje = false;

function SucceededCallbackOKFDefaultVartes(result) {

    if (result.mosPlotesoTeDhena) {
        if (btneKlientfurnitorVartes.Options.value.length > 0 && btneKlientfurnitorVartes.GetValue().length == 0) {
            btneKlientfurnitorVartes.AddOption(result.kf);
            btneKlientfurnitorVartes.SetValue([result.kf.IdKlientFurnitor]);
        }

        if (hfState.Get("NTDKV")) {
            txtEmriKlienti.SetText(result.kf.EmertimFature);
            txtNipt.SetText(result.kf.NiptiKF);
            txtKontakti.SetText(result.kf.TelKF);
            txtAdresaFaturimit.SetText(result.oColAdresatKF[0] != undefined ? result.oColAdresatKF[0].Adresa : "");
            txtQytetiK.SetText(result.kf.EmriQytetitKF);
        }
    }
}
function SucceededCallbacOKF(result) {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    if (result == null || result.kf == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKFnukEkziston"));
        if (btnKlienti != "")
            btnKlienti.SetSelectedIndex(-1);
    }
    else {
        pageState.Klient = JSON.stringify(result);
        pageState.Kf.alternativaKushtZbritje = result.alternativaKushtZbritje;
        //alternativaKushtZbritje = result.alternativaKushtZbritje;
        formatNumriZgjedhur = result.formatZgjedhur;
        formatKursi = result.formatKursi;
        pageState.TaksaKF = result.taksa;
        //if (pageState.TaksaKF.IdTaksa>0)
        ChangeTVSHSipasKlientit();
        ndryshoKonfigFormatNumri(grida, formatNumriZgjedhur, formatKursi);
        vendosKonfigFormatNumri();
        txtNipt.SetText(result.kf.NiptiKF);
        txtQytetiK.SetText(result.kf.EmriQytetitKF);

        txtKontakti.SetText(result.kf.TelKF);
        Utils.SelectComboItem(btnKlienti, result.kf.IdKlientFurnitor, result.kf.KodKlientFurnitor, result.kf.EmertimiKF);
        if (txtMarresi.GetText() == '' || marresi == false) {
            txtMarresi.SetText(result.kf.EmertimiKF);
        }
        oColTrupatKategoriteZbritjes = result.oColTrupatKategoriteZbritjes;
        txtEmri.SetText(result.kf.EmertimiKF);
        if ((!result.mosPlotesoTeDhena || btnMenyreTransporti.GetText() === "") && (result.kf.MenyraTransportit != 0))
            btnMenyreTransporti.SetValue(result.kf.MenyraTransportit);
        if (!(result.mosPlotesoTeDhena === true && btnKushtDergimi.GetText() !== "") && (result.kf.KushteDergimi != 0))
            btnKushtDergimi.SetValue(result.kf.KushteDergimi);
        mbushVleraAgjenteshPerqindjeAgjentesh(result);

        if (!(result.mosPlotesoTeDhena === true && btnKushtPagese.GetText() !== "") && (result.kf.IdKushtePagese != 0))
            btnKushtPagese.SetValue(result.kf.IdKushtePagese);
        var aplikoZbritje = aplikoZbritjeKlienti();

        kuponKerkonKlienti = result.kf.Kupon;
        if (pageState.lloji != "modifikim" || result.kf.Kupon) {
            cbKupon.SetChecked(result.kf.Kupon);
            kuponclick(cbKupon);
        }
        if (pageState.lloji != "modifikim")
            totaliVjeter = undefined;
        else
            totaliVjeter = null;

        if (!(result.mosPlotesoTeDhena === true && txtVlefte.GetText() !== "") && aplikoZbritje
            //(result.alternativaKushtZbritje == "Te dyja" || (pageState.veprimi == "shitje" && result.alternativaKushtZbritje == "Dokument shitje") || (pageState.veprimi == "blerje" && result.alternativaKushtZbritje == "Dokument blerje"))
        ) {
            if (result.kf.ZbritjeTotal != "0.00") {
                setZbritjeTotal(Utils.llojZbritje.Perqindje, result.kf.ZbritjeTotal, true);
                //txtPerqindje.SetText(result.kf.ZbritjeTotal);
                //txtVlefte.SetEnabled(false);
                //cmbLlojZbritje.SetValue(Utils.llojZbritje.Perqindje);
            }
            else {
                var selectedKF = btnKlienti.GetSelectedItem();
                if (selectedKF != null) {
                    callWebServiceKategorizbritje(selectedKF.GetColumnText('KodKlientFurnitor'), data_DateEdit.GetDate());
                }
            }
        }
        if (result.kf.MaturimiKF != 0)
            btnMaturimi.SetValue(result.kf.MaturimiKF);
        txtKrediti.SetText(result.kf.LimitParalajmerues);
        txtLimit.SetText(result.kf.LimitBllokues);
        if (result.kf.IdMetoda != -1)
            cmbMenyrePagese.SetValue(result.kf.IdMetoda);
        //ZbritjaKlientit do marre vlere vetem ne rastet kur:
        //1. Kushti ZBKF te konfigurimi i klientit ka vleren Te dyja.
        //2. Eshte dokument shitje dhe kushti ZBKF ka vleren Dokument shitje.
        //3. Eshte dokument blerje dhe kushti ZBKF ka vleren Dokument blerje.
        //Ne rast te kundert ZbritjaKlientit nuk merr vlere.
        if (aplikoZbritje)
            ZbritjaKlientit = result.kf.ZbritjeAnalitike;
        else
            ZbritjaKlientit = "";
        if (ndryshocmime)
            NivelCmimi = result.kf.IdNivelCmimi;
        else NivelCmimi = -1;
        monklienti = result.idMonedha;
        if (!result.mosPlotesoTeDhena)
            if (cmbMonedha.GetValue() != monklienti) {
                cmbMonedha.SetValue(monklienti);
                updateCmimiGridToDo = true; //tregon qe me pas duhet te modifikohen cmimet patjeter.
                if (pageState.lloji == "modifikim" && hfState.Get("VCVKMD") === false)
                    updateCmimiGridToDo = false;
                SelectedIndexChangedMonedha();
            }
            else {
                if (!(pageState.lloji == "modifikim" && hfState.Get("VCVKMD") === false))
                    updateCmimiGrid();
            }
        else {
            if (cmbMonedha.GetValue() != monklienti) {
                cmbMonedha.SetValue(monklienti);
                updateCmimiGridToDo = false;
                SelectedIndexChangedMonedha();
            }
        }

        meTVSH = result.meTVSH;
        callWebServiceDetyrimiKF(result.kf.IdKlientFurnitor);

        if (cmbMonedhaPagese.GetValue() != monklienti) {
            cmbMonedhaPagese.SetValue(monklienti);
            SelectedIndexChangedMonedhaPagese();
        }

        if (!(result.mosPlotesoTeDhena === true && dateMaturimi_DateEdit.GetText() !== ""))
            LostFocusMaturimi();
        if (!(result.mosPlotesoTeDhena === true && (txtAdresaFaturimit.GetText() !== "" || txtAdresaDergimit.GetText() !== "")))
            vendosAdresaKF(result.oColAdresatKF);

        if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && cmbKarta.GetVisible())//&& cmbKarta.GetSelectedItem() == null //hequr te ngarkohen kartat e vetem klientit
            callwebserviceMbushKarta(result.kf.IdKlientFurnitor);

        var vleraDoganes = result.kf.MeDogane ? 'True' : 'False';
        if (cmbDogana.GetValue() != vleraDoganes) {
            cmbDogana.SetValue(vleraDoganes);
            SelectedIndexChangedDogana();
        }

        txtEmriKlienti.SetText(result.kf.EmertimFature);
    }
}



function SucceededCallbackOKFDefault(result) {//po
    if (pageState.lloji != "modifikim" && result.kf.Kupon) {
        kuponKerkonKlienti = true;
        cbKupon.SetChecked(true);
        kuponclick(cbKupon);
    }
    pageState.Klient = JSON.stringify(result);
    monklienti = result.idMonedha;
    if (cmbMonedha.GetValue() != monklienti || txtKursi.GetText() == "") {
        cmbMonedha.SetValue(monklienti);
        SelectedIndexChangedMonedha();
    }

    meTVSH = result.meTVSH;
    var detyrimi = result.detyrimi;

    Utils.SelectComboItem(btnKlienti, result.kf.IdKlientFurnitor, result.kf.KodKlientFurnitor, result.kf.EmertimiKF);
    var konvSipasUrdherShitje = (hfTeDrejta.Get("KonvertimSipasUSH") == true && hfState.Get("konvNgaUshNeFsh") == true);
    if (konvSipasUrdherShitje)
        btnKlienti.SetEnabled(false);
    txtEmri.SetText(result.kf.EmertimiKF);
    if (txtMarresi.GetText() == '') {
        txtMarresi.SetText(result.kf.EmertimiKF);
    }
    formatNumriZgjedhur = result.formatZgjedhur;
    formatKursi = result.formatKursi;
    var grida = $(pageState.gridaSelector);
    ndryshoKonfigFormatNumri(grida, formatNumriZgjedhur, formatKursi);
    vendosKonfigFormatNumri();
    pageState.Kf.alternativaKushtZbritje = result.alternativaKushtZbritje;
    pageState.TaksaKF = result.taksa;
    //if (pageState.TaksaKF.IdTaksa > 0)
    ChangeTVSHSipasKlientit();
    var aplikoZbritje = aplikoZbritjeKlienti();
    //alternativaKushtZbritje = result.alternativaKushtZbritje;
    if (pageState.lloji == "shtim" || pageState.lloji == "shtimraport") {
        if (result.kf.MenyraTransportit != 0 && btnMenyreTransporti.GetText() == '') {
            btnMenyreTransporti.SetValue(result.kf.MenyraTransportit);
            if (konvSipasUrdherShitje)
                btnMenyreTransporti.SetEnabled(false);
        }

        if (result.kf.KushteDergimi != 0 && btnKushtDergimi.GetText() == '') {
            btnKushtDergimi.SetValue(result.kf.KushteDergimi);
            if (konvSipasUrdherShitje)
                btnKushtDergimi.SetEnabled(false);
        }
        mbushVleraAgjenteshPerqindjeAgjentesh(result);
        if (result.kf.IdKushtePagese != 0 && btnKushtPagese.GetText() == '') {
            btnKushtPagese.SetValue(result.kf.IdKushtePagese);
            if (konvSipasUrdherShitje)
                btnKushtPagese.SetEnabled(false);
        }

        if (aplikoZbritje) {
            if (result.kf.ZbritjeTotal != "0.00") {
                setZbritjeTotal(Utils.llojZbritje.Perqindje, result.kf.ZbritjeTotal, true);
                //txtPerqindje.SetText(result.kf.ZbritjeTotal);
                //cmbLlojZbritje.SetValue(Utils.llojZbritje.Perqindje);
                //changedPerqindje();
            }
            else {
                var selectedKF = btnKlienti.GetSelectedItem();
                if (selectedKF != null)
                    callWebServiceKategorizbritje(selectedKF.GetColumnText('KodKlientFurnitor'), data_DateEdit.GetDate());
            }
        }
        if (result.kf.MaturimiKF != 0 && btnMaturimi.GetText() == '') {
            btnMaturimi.SetValue(result.kf.MaturimiKF);
            if (konvSipasUrdherShitje)
                btnMaturimi.SetEnabled(false);
        }
        txtKrediti.SetText(result.kf.LimitParalajmerues);
        txtLimit.SetText(result.kf.LimitBllokues);
        txtNipt.SetText(result.kf.NiptiKF);
        txtQytetiK.SetText(result.kf.EmriQytetitKF);
        if (result.kf.TelKF != "")
            txtKontakti.SetText(result.kf.TelKF);
        if (pageState.lloji != "modifikim") {
            kuponKerkonKlienti = result.kf.Kupon;
            cbKupon.SetChecked(result.kf.Kupon);
            kuponclick(cbKupon);
        }

        cmbDogana.SetValue((result.kf.MeDogane) ? 'True' : 'False');
        SelectedIndexChangedDogana();

        txtEmriKlienti.SetText(result.kf.EmertimFature);
        if (result.kf.IdMetoda != -1)
            cmbMenyrePagese.SetValue(result.kf.IdMetoda);
        else if (!cmbMenyrePagese.GetValue())
            cmbMenyrePagese.SetSelectedIndex(0);

    }

    callWebServiceDetyrimiKF(result.kf.IdKlientFurnitor);
    //ZbritjaKlientit = result.kf.ZbritjeAnalitike;
    //ZbritjaKlientit do marre vlere vetem ne rastet kur:
    //1. Kushti ZBKF te konfigurimi i klientit ka vleren Te dyja.
    //2. Eshte dokument shitje dhe kushti ZBKF ka vleren Dokument shitje.
    //3. Eshte dokument blerje dhe kushti ZBKF ka vleren Dokument blerje.
    //Ne rast te kundert ZbritjaKlientit nuk merr vlere.
    if (aplikoZbritje)
        ZbritjaKlientit = result.kf.ZbritjeAnalitike;
    else
        ZbritjaKlientit = "";
    if (ndryshocmime) {
        NivelCmimi = result.kf.IdNivelCmimi;
        //updateCmimiGrid();
    }
    else NivelCmimi = -1;
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && cmbKarta.GetVisible() && cmbKarta.GetText() == "")
        callwebserviceMbushKarta(result.kf.IdKlientFurnitor);
    if (pageState.lloji == "shtim" && !(result.mosPlotesoTeDhena === true && (txtAdresaFaturimit.GetText() !== "" || txtAdresaDergimit.GetText() !== "")))
        vendosAdresaKF(result.oColAdresatKF);
}

function vendosAdresaKF(result) {
    txtAdresaFaturimit.SetText("");
    txtAdresaDergimit.SetText("");
    for (i = 0; i < result.length; i++) {
        rreshti = result[i];
        if (rreshti.IdTipAdrese == 1)//adrese biznesi
            txtAdresaFaturimit.SetText(rreshti.Adresa);
        else
            if (rreshti.IdTipAdrese == 2)//adrese magazine
                txtAdresaDergimit.SetText(rreshti.Adresa);
    }
    LostFocusMaturimi();
}



/*
Function: ButtonClickMagazina
Hap lupen e magazinave.
*/

function ButtonClickMagazina() {//po
    var hfMag = $('#hfLupaMagazina').val();
    //var hfMag = document.getElementById("hfLupaMagazina");
    var queryStr = hfMag;  //hfMag.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
    identifikuesPerPopupMagazina = 'RegjistrimDokumentash';
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

function ButtonClickMagazinaTrupi() {//po
    identifikuesMagazina = "Mag1";
    var hfMag = $('#hfLupaMagazina').val();
    var idRreshti = $('#rowed5').getLastSel2();
    var aqt = kthellojArt(idRreshti) == 1 ? true : false;
    var queryStr = hfMag;  //hfMag.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
    identifikuesPerPopupMagazina = 'RegjistrimDokumentash_Trupi';
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr + '&aqt=' + aqt);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}


function MagazinaChanged() {
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    var item = btnMagazina.GetSelectedItem();
    if (item === null) {
        previousMag = undefined;
        return;
    }
    var kodi = item.GetColumnText('Kodi');
    var previousCode = previousMag == undefined ? "" : previousMag.GetColumnText('Kodi');
    if (kodi == previousCode || pageState.veprimi == "blerje" || !kaSeriale) {
        TextChangedMagazina();
        previousMag = item;
        return;
    }

    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: hfState.Get("msgSerialetDoTeFshihen"),
        modal: true,
        layout: "center",
        idGjuha: pageState.idGjuha,
        okClick: function (noty) {
            TextChangedMagazina();
            previousMag = item;
        },
        cancelClick: function (noty) {
            if (previousMag != undefined)
                Utils.SelectComboItem(btnMagazina, previousMag.value, previousMag.texts[0], previousMag.texts[1]);
            else
                btnMagazina.SetSelectedItem(undefined);
        }
    });
}


/*
Function: TextChangedMagazina

Vendosur ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina() {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    if (btnMagazina.GetSelectedItem() === null)
        return;
    magazina1 = btnMagazina.GetSelectedItem().GetColumnText('Kodi');
    var pershkrimmag = btnMagazina.GetSelectedItem().GetColumnText('Pershkrimi');
    txtPershkrimMagazine.SetText(pershkrimmag);
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        var idRreshti = rreshtaTeGrides[i];
        var idArt = grida.getTekstQelize('txtIdKodi', idRreshti);
        var art = memoryArt.Get(idArt);
        if (pageState.veprimi != "blerje" && art != undefined && (art.IdFormatSeriali != 0 || art.Klasa == 4) && kaSeriale && grida.getTekstQelize("txtMagazina", rreshtaTeGrides[i]) != magazina1)//nese eshte artikull qe ka nevoje per seriale ose eshte artikull set, qe perbehet nga art me seriale
            FshiSeriale(idArt, grida.getTekstQelize("txtMagazina", rreshtaTeGrides[i]));

        grida.setTekstQelize('txtPershkrimmag', rreshtaTeGrides[i], pershkrimmag);
        grida.setTekstQelize('txtMagazina', rreshtaTeGrides[i], magazina1);
        grida.vendosTeDhenaPerQelizen('txtMagazina', rreshtaTeGrides[i], "IshMagazina", magazina1);
        kontrolloGjendje(idArt, grida.getTekstQelize('txtMagazina', idRreshti), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRreshti), grida.getTekstQelize('txtDetajimi2t', idRreshti), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRreshti);
    }
    if ($('#txtMagazina').val() != undefined) {
        callWebServiceInfoRow(idRresht);
    }
    if (magazina1 != "") {
        try {
            $.ajax({
                pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheDegeMagazineSipasKodit"),
                data: JSON.stringify({ kodi: magazina1, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
            }).done(SucceededCallbackDega);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimMarrjeMagazine"));
        }
    }
}

function TextChangedDega() {
    if (cmbDegeAdministrative.GetSelectedItem() != null) {
        var dega = cmbDegeAdministrative.GetSelectedItem();
        if (dega != null) {
            txtPershkrimDege.SetText(dega.GetColumnText('Pershkrimi'));
            if (shfaqlupepaprintuar && $('#hfShtimModifikim').val() != 'modifikim')
                kontrollofaturaTePaprintuara();
            else if (hapLupeValidimi)
                ButtonClickValidim();
        }
    }
}

function SucceededCallbackDega(rezult) {//po
    if (rezult != 0) {
        cmbDegeAdministrative.SetValue(rezult);
        try {
            $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimDegeSipasID"), data: JSON.stringify({ id: rezult }) }).done(SucceededCallbackDegePershkrim);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        }
    }
}

function SucceededCallbackDegePershkrim(result) {
    if (result != 0)
        txtPershkrimDege.SetText(result);
}

function TextChangedPika() { //pershkrimi i Pike shitje
    if (cmbPikeShitjeFurnizimi.GetSelectedItem() != null) {
        var pika = cmbPikeShitjeFurnizimi.GetSelectedItem();
        if (pika != null) {
            txtPershkrimPike.SetText(pika.GetColumnText('Pershkrimi'));
            if (shfaqlupepaprintuar && $('#hfShtimModifikim').val() != 'modifikim')
                kontrollofaturaTePaprintuara();
            else if (hapLupeValidimi)
                ButtonClickValidim();
        }
    }
}


/*
Function: SucceededCallbackAdresatKF

Vendosur adresat e klientit/furnitorit te zgjedhur (adresen e faturimit, adresen e dergimit).
*/
function SucceededCallbackAdresatKF(result) {//po
    var vlerat = '';
    txtAdresaFaturimit.SetText("");
    txtAdresaDergimit.SetText("");
    vlerat = result.split(';');
    for (i = 0; i < vlerat.length - 1; i++) {
        rreshti = vlerat[i].split(',');
        if (rreshti[0] == "1")//adrese biznesi
            txtAdresaFaturimit.SetText(rreshti[1]);
        else if (rreshti[0] == "2")//adrese magazine
            txtAdresaDergimit.SetText(rreshti[1]);
    }
    LostFocusMaturimi();
}

/*
Function: TextChangedNiveli

Therret funksionin <callWebserviceNiveli> per te vendosur templaten sipas nivelit te zgjedhur.
*/
function TextChangedNiveli() {//po
    var hidField1 = document.getElementById("hfVeprimi");
    var mod;
    if (pageState.lloji != 'shtim')
        mod = true;
    else mod = false;
    if (cmbNiveli.GetText() != "") {
        callWebserviceNiveli(cmbNiveli.GetValue(), hidField1.value, mod);
        percaktoMenuSipasTeDrejtavePerNivelin(cmbNiveli.GetValue());
    }
    else callWebserviceNiveli(0, hidField1.value, mod);
}

function callWebserviceNiveli(idNiveli, veprimi, mod) {//po
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: idNiveli, veprimi: veprimi, mod: mod, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idGjuha: pageState.idGjuha })
        }).done(SucceededCallbackNiveli);
    }
    catch (e) {
        console.log(e.message);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

/*
Function: SucceededCallbackNiveli

Mbush combo-n modeli me vlerat sipas nivelit te zgjedhur
*/
var selektoKonfigurim = false;

function SucceededCallbackNiveli(colModelet) {
    if (pageState.lloji == "shtim") {
        cmbModeli.ClearItems();
        for (i = 0; i < colModelet.length; i++) {
            if ((Utils.getUrlVar('shitje_blerje') == "shitjediscount" && ((colModelet[i].KodKonfigAmbjente.length > 5 && colModelet[i].KodKonfigAmbjente.substring(0, 5) == "USHDD") || (colModelet[i].KodKonfigAmbjente.length > 8 && colModelet[i].KodKonfigAmbjente.substring(0, 8) == "POROSIDD"))) ||
                (Utils.getUrlVar('shitje_blerje') == "bazaar" && ((colModelet[i].KodKonfigAmbjente.length > 6 && colModelet[i].KodKonfigAmbjente.substring(0, 6) == "BAZAAR") || (colModelet[i].KodKonfigAmbjente.length > 12 && colModelet[i].KodKonfigAmbjente.substring(0, 12) == "POROSIBAZAAR")))
                || (Utils.getUrlVar('shitje_blerje') != "shitjediscount" && Utils.getUrlVar('shitje_blerje') != "bazaar" && !(((colModelet[i].KodKonfigAmbjente.length > 5 && colModelet[i].KodKonfigAmbjente.substring(0, 5) == "USHDD") || (colModelet[i].KodKonfigAmbjente.length > 8 && colModelet[i].KodKonfigAmbjente.substring(0, 8) == "POROSIDD") ||
                    (colModelet[i].KodKonfigAmbjente.length > 6 && colModelet[i].KodKonfigAmbjente.substring(0, 6) == "BAZAAR") || (colModelet[i].KodKonfigAmbjente.length > 12 && colModelet[i].KodKonfigAmbjente.substring(0, 12) == "POROSIBAZAAR")))))



                cmbModeli.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
        }
        if (!selektoKonfigurim)
            cmbModeli.SetSelectedIndex(0);
        else {
            cmbModeli.SetValue(Utils.getUrlVar('konfigurim'));
            selektoKonfigurim = false;
        }
    }
    ndryshoKonfigurimin(true);

}

function percaktoMenuSipasTeDrejtavePerNivelin(idNiveli) {
    var teDrejtaNiveli = merrTeDrejtaNiveli(idNiveli);

    var shitje_blerje = Utils.getUrlVar("shitje_blerje");
    if (shitje_blerje == 'shitje' || shitje_blerje == 'blerje') {
        hfTeDrejta.Set('Modifikim', teDrejtaNiveli.DMod);
        hfTeDrejta.Set('Shtim', teDrejtaNiveli.DShtim);
        hfTeDrejta.Set('ModifikimDraft', teDrejtaNiveli.DModifikimDraft);
        hfTeDrejta.Set('ShtimDraft', teDrejtaNiveli.DShtimDraft);
        hfTeDrejta.Set('Arkiva', teDrejtaNiveli.DArkiva);
        hfTeDrejta.Set('Konverto', teDrejtaNiveli.DKonverto);
        hfTeDrejta.Set('Fshi', teDrejtaNiveli.DFsh);
        myMenu.menuSipasTeDrejtaRegjistrimSipasNivelit($("#hfShtimModifikim"), hfTeDrejta);
    }
}

/*
Function: SelectedIndexChangedMonedha

Therret funksionin <callWebserviceKursi> per te marre kursin kur ndryshohet monedha.
*/
function SelectedIndexChangedMonedha() {//po
    if (cmbMonedha.GetText() != "")
        callWebserviceKursi(cmbMonedha.GetValue());
    else
        if (updateCmimiGridToDo)
            updateCmimiGrid();
}

function SelectedIndexChangedMonedhaPagese() {//po
    if (cmbMonedhaPagese.GetText() != "") {
        callWebserviceKursiSipasLlojitPagese(cmbMonedhaPagese.GetValue(), llojkursi);
    }
}

/*
Function: LostFocusKursi

Therret funksionin <vendosTotaleMonedheFature>.
*/
function LostFocusKursi() {//po
    if (parseFloat(txtKursi.GetText()) == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteZero"));
        txtKursi.SetText(1);
    }
    if (cmbMonedha.GetText() == cmbMonedhaPagese.GetText())
        txtKursiPagese.SetText(txtKursi.GetText());
    vendosTotaleMonedheFature();
}

function lostFocusTxtNumer() {
    vendosTotaleMonedheFature();
}

/*
Function: LostFocusKursi

Therret funksionin <vendosTotaleMonedheFature>.
*/
function LostFocusKursiPagese() {//po
    if (cmbMonedha.GetText() == cmbMonedhaPagese.GetText())
        txtKursi.SetText(txtKursiPagese.GetText());
    vendosTotaleMonedheFature();
}

/*
Function: callWebserviceKursi

Therret funksionin merrKursiSipasMonedhesDatesDheLlojit per te marre kursin e fundit te monedhes
Shiko funksionin <SucceededCallbackKursi>.
*/
function callWebserviceKursi(kodMonedhe) {//po
    if (kodMonedhe !== null && (!((pageState.lloji == "konvertim" || pageState.lloji == "konvertimblerje") && kurskonvertimi))) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursiSipasMonedhesDatesDheLlojit"), data: JSON.stringify({ idMonedha: kodMonedhe, date: data_DateEdit.GetDate(), lloji: llojkursi })
        }).done(SucceededCallbackKursi);
    }
}

function callWebserviceKursiSipasLlojitPagese(idMonedha, lloj) {//po
    if (pageState.idMonedheNdermarrje !== -1 && idMonedha == pageState.idMonedheNdermarrje) { //nese monedha eshte e barabarte me te ndermarrjes atehere kursi duhet 1
        vendosKursiPagese("1");
        return;
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKursinFunditSipasLlojitDB"), data: JSON.stringify({ idMonedha: idMonedha, lloji: lloj, idNdermarrje: pageState.idNdermarrje })
    }).done(SucceededCallbackKursiPagese);
}

var kursifundit = 1;

function SucceededCallbackKursi(result) {//po TODO: PATI formati i kursit
    if (result.kursi == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimKursiMonedha"));
        txtKursi.SetText("1");
        kursifundit = 1;
    }
    else {
        if (result == 1 && hfState.Get("idMonedheNderm") != cmbMonedha.GetValue())
            myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesKursiKembimitNje"));

        if (txtKursi.GetText() == result) {
            if (ndryshuarData == true && cmbModeli.GetText() != "FBT" && cmbModeli.GetText() != "FST" && pageState.lloji != 'kthim') {
                if (kaRreshtaPlotNeGride()) {
                    //popNdryshoCmime.Show();
                    myMesazh.ShtoPyetje(hfState.Get("msgDeshironiTendryshoniCmimet"), true);
                    identifikuesPyetje = "ndryshoCmime";
                }
                ndryshuarData = false;
            }
            if (!updateCmimiGridToDo) {
                return;
            }
        }
        txtKursi.SetText(result);
        kursifundit = result;
    }
    if (updateCmimiGridToDo)
        updateCmimiGrid();
    vendosTotaleMonedheFature();
    updateCmimiGridToDo = false; //kur

}

/// <summary>Kontrollon ne rreshtat e grides nese ka rreshta qe kane vlera (pra, a ka artikuj apo llogari ne gride), apo jo</summary>
/// <returns>true nese ne gride ka rreshta me vlera, false ne rast te kundert</returns>
function kaRreshtaPlotNeGride() {
    var grida = $(pageState.gridaSelector);
    var idTe = grida.jqGrid('getDataIDs');
    var kaRreshta = false;
    for (i = 0; i < idTe.length; i++) {
        var idRreshti = idTe[i];
        if (grida.getTekstQelize('txtKodi', idRreshti) !== "") {
            kaRreshta = true;
            break;
        }
    }
    return kaRreshta;
}

function SucceededCallbackKursiPagese(result) {//po
    vendosKursiPagese(result);
}

function vendosKursiPagese(result) {
    if (result == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimKursiMonedha"));
        txtKursiPagese.SetText("1");
    }
    else
        txtKursiPagese.SetText(result);
    vendosTotaleMonedheFature();
}

function LostFocusMaturimi() {//po
    if (btnMaturimi.GetValue() != 0 && btnMaturimi.GetValue() != null)
        callWebserviceMaturimi(btnMaturimi.GetValue(), data_DateEdit.GetDate());
}

/*
Function: callWebserviceMaturimi

Shiko funksionin <SucceededCallbackMaturimi>.
*/
function callWebserviceMaturimi(name, date) {//po
    try {

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheMaturim"), data: JSON.stringify({ idmat: name, dataFat: date })
        }).done(SucceededCallbackMaturimi);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimMarrjeMaturim"));
    }
}

/*
Function: SucceededCallbackMaturimi

Vendos daten e maturimit sipas maturimit te zgjedhur(ose maturimit te percaktuar te klienti)
*/
function SucceededCallbackMaturimi(result) {//po
    dateMaturimi_DateEdit.SetDate(new Date(result));
}
var idartikujsh = new Array();
/*
Function: merrTeDhena

Merr te dhenat qe ka grida dhe i vendos neper hidden field-e per ti perdorur ne server side
*/
function merrTeDhena(s, e) {//po
    //console.time('merrTeDhena');
    ShtoNeHfColKlienteFurnitoreVartes();
    var grida = $(pageState.gridaSelector);
    var gridaKomision = $(pageState.gridaKomision);
    hfIdGride.Clear();
    idartikujsh = new Array();
    grida.jqGrid('saveRow', grida.getLastSel2(), null, 'clientArray', {});
    grida.setLastSel2(0);
    //lastsel2 = 0;
    var kamag = true;
    trupiBosh = true;
    var total = 0;
    var shumaKomision = 0;
    var idTe = grida.jqGrid('getDataIDs');
    var idTeKomision = gridaKomision.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        hfIdGride.Add(i.toString(), idTe[i]);
        editorLloji = grida.getTekstQelize('cmbLloji', idTe[i]);
        editorKodi = grida.getTekstQelize('txtKodi', idTe[i]);
        editorCmimi = grida.getTekstQelize('txtCmimi', idTe[i]);
        editorZbritje = grida.getTekstQelize('txtZbritja', idTe[i]);
        editorVlefteTVSH = grida.getTekstQelize('txtVleftaTVSH', idTe[i]);
        editorVlefte = grida.getTekstQelize('txtVlefta', idTe[i]);
        editorVlereKomision = grida.getTekstQelize('txtVleraKomisionit', idTe[i]);
        editorMagazina = grida.getTekstQelize('txtMagazina', idTe[i]);
        idartikujsh[i] = (grida.getTekstQelize('txtIdKodi', idTe[i]) == "" ? 0 : grida.getTekstQelize('txtIdKodi', idTe[i]));

        if (editorKodi != "")
            if (editorVlefte != "")
                total = total + parseFloat(editorVlefte);

        shumaKomision += Number(editorVlereKomision);

        if (editorKodi != "")
            trupiBosh = false;
        if ((pageState.kushte.GJDM === true || upp) && editorMagazina == "" && editorKodi != '') {
            kamag = false;
            e.processOnServer = false; click = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniMagazinen"));
            return;
        }
        if ($('#hfRuajDraft').val() != "Draft") {
            if (cmimzero == 2) {
                if (editorLloji == "Artikull" && editorKodi != '' && parseFloat(editorCmimi) == 0.00) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukDuhetTeKeteArtikujMeCmimZero"));
                    e.processOnServer = false; click = false;
                    return;
                }
            }
            else if (cmimzero == 1) {
                if (editorLloji == "Artikull" && editorKodi != '' && parseFloat(editorCmimi) == 0.00) {
                    myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesKaCmimZeroNeRreshtin") + grida.getTekstQelize('txtNrRendor', idTe[i]) + '!');
                    Utils.shfaqLoadingGif();
                }
            }
        }
        if (editorLloji == "Artikull" && editorKodi != '') {
            if (hfState.Get('plotesoDetajim1') == true) {
                var kaDetajimArtikulli = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idTe[i])).DetajimArtikulli;
                if (kaDetajimArtikulli == true) {
                    var detajimi1 = grida.getTekstQelize('txtDetajimi', idTe[i]);
                    if (detajimi1 == '') {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniDetajiminEArtikullit") + grida.getTekstQelize('txtNrRendor', idTe[i]) + '!');
                        e.processOnServer = false;
                        click = false;
                        return;
                    }
                }
            }
        }
        if (hfState.Get("LZAN") && (editorZbritje < -100 || editorZbritje > 100)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZbritjaNegativeVlerat"));
            e.processOnServer = false; click = false;
            return;
        }
        else if (!hfState.Get("LZAN") && (editorZbritje < 0 || editorZbritje > 100)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZbritjaVlerat"));
            e.processOnServer = false; click = false;
            return;
        }
        if ($('#hfRuajDraft').val() != "Draft") {
            if (editorLloji == "Llogari" && editorKodi != '' && parseFloat(editorVlefteTVSH) == 0.00) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlogariCmimZero"));
                e.processOnServer = false; click = false;
                return;
            }
        }
    }
    //total = (total - shumaKomision).toFixed(formatNumriZgjedhur.ShifraPasPresjesSasia);
        //total = (total - shumaKomision).toFixed(formatNumriZgjedhur.ShifraPasPresjesSasia);
    //if ((total - parseFloat(txtVlefte.GetText())).toFixed(2) != parseFloat(txtTotal1.GetText()).toFixed(2)) {
    if ((total - parseFloat(txtVlefte.GetText())).toFixed(2) != (Math.round(parseFloat(txtTotal1.GetText()) * 100) / 100).toFixed(2)) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgTeDhenaTePasakta"));
/*        e.processOnServer = false; click = false;*/
        return;
    }
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    var rreshtaKomision = gridaKomision.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    $('#gridObjectKomision').val(JSON.stringify(rreshtaKomision));
    unformatoFushaDevi();
    //console.timeEnd('merrTeDhena');
}

function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    myJQGrid.ruajKolonatEGrides(grida, idGride, pageState.idGjuha, pageState.idNdermarrje, pageState.idViti, pageState.idPerdoruesi);
}

var trupiBosh = false;
/*
Function: pastro

Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {//po
    arrLloji = new Array();
    arrKodi = new Array();
    arrPershkrimi = new Array();
    arrDetajimi = new Array();
    arrNjesia = new Array();
    arrSasia = new Array();
    arrCmimi = new Array();
    arrZbritje = new Array();
    arrVlefteTVSH = new Array();
    arrTVSH = new Array();
    arrVlefte = new Array();
    arrMagazina = new Array();
    $('#hfKonv').val('');
    resetCountera();
    ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
    try {
        ASPxMenu1.GetItemByName('Paguaj').SetVisible(false);
    }
    catch (ex) { }
    var hidField1 = document.getElementById("hfLloji");
    var hidfield2 = document.getElementById("hfKodi");
    var hidField3 = document.getElementById("hfPershkrimi");
    var hidField4 = document.getElementById("hfDetajimi");
    var hidField5 = document.getElementById("hfNjesia");
    var hidField6 = document.getElementById("hfSasia");
    var hidField7 = document.getElementById("hfCmimi");
    var hidfield8 = document.getElementById("hfZbritje");
    var hidfield9 = document.getElementById("hfVlefteTVSH");
    var hidfield10 = document.getElementById("hfTVSH");
    var hidfield11 = document.getElementById("hfVlefte");
    var hidField12 = document.getElementById("hfMagazina");
    hidField1.value = '';
    hidfield2.value = '';
    hidField3.value = '';
    hidField4.value = '';
    hidField5.value = '';
    hidField6.value = '';
    hidField7.value = '';
    hidfield8.value = '';
    hidfield9.value = '';
    hidfield10.value = '';
    hidfield11.value = '';
    hidField12.value = '';
    hfArkiva.Clear();
    pastroFushatKokes();
    $('#hfMerrFazaNgaSesioni').val('false');
    $('#hfIdKontrata').val('');
    cmbFaza.ClearItems();
}

/*
Function: resetCountera

Vendos vleren 0 tek te gjithe counter-at.
*/
function resetCountera() {//po
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
}

/*
Function: OnContactSelected

Thirret kur zgjidhet nje element nga autosuggesti qe te vendoset edhe pershkrimi i llogarise
*/
function OnContactSelected(source, eventArgs) {
    var gjatesiaParashteses = constanteParashtese.length;
    var tempStr = source.get_element().id.substring(gjatesiaParashteses, source.get_element().id.length);
    var poziocionVize = tempStr.indexOf("_");
    var indexRreshti = tempStr.substring(0, poziocionVize);
    editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + indexRreshti);
    editorLlogEmer.SetText(eventArgs.get_value());
}

/*
Function: ButtonClickKodi

Hap lupen e artikujve, llogarive apo makrove sipas zgjedhjes qe eshte bere te lloji.
*/
function ButtonClickKodi() {//po
    var grida = $(pageState.gridaSelector);
    var vleraLlojit = grida.getTekstQelize('cmbLloji', grida.getLastSel2());
    var hfKod = $("#hfGridaKodi");
    var queryStr = hfKod.val();
    switch (vleraLlojit) {
        case "":
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojinEVeprimit"));
            break;
        case "Artikull":
            popupUniversal.SetHeaderText(hfState.Get("headerPopUpZgjidhArtikullin"));
            var grup = '';
            if (merrSipasGrupit)
                grup = cmbGrup1.GetText();
            if (merrDhurata)
                grup = "DhurateVFOne";

            var idMagazina = -1;
            if (btnMagazina.GetText() !== '')
                idMagazina = btnMagazina.GetValue();
            if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar'))
                popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup + '&idMag=' + idMagazina + '&shitshem=true&idUserPerTheme=' + pageState.idPerdoruesi);
            else
                popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup + '&idMag=' + idMagazina + '&shitshem=false&idUserPerTheme=' + pageState.idPerdoruesi);
            popupUniversal.SetSize(widthLupaArtikull, heightLupaArtikull);
            popupUniversal.Show();
            countHapLupeArtikujsh++;
            break;
        case "Makro":
            popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMakro"));
            popupUniversal.SetContentUrl("LupaMakro.aspx?idKonfigAmbjente=" + queryStr);
            popupUniversal.SetSize(widthLupaMakro, heightLupaMakro);
            popupUniversal.Show();
            break;
        case "Llogari":
            identikuesPerPopupLlogari = "RegjistrimDokumentashKodi";
            popupUniversal.SetHeaderText(hfState.Get("headerPopUpText"));
            popupUniversal.SetContentUrl('LupaLlogaria.aspx?idKonfigAmbjente=' + queryStr);
            popupUniversal.SetSize(widthLupaLlogaria, heightLupaLlogaria);
            popupUniversal.Show();
            break;
        default:
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlojiVeprimitIPanjohur"));
            break;
    }
}

function ButtonClickKategoriShpenzimi() {
    var grida = $(pageState.gridaSelector);
    var hfKod = $("#hfGridaKodi");
    var queryStr = hfKod.val();
    identikuesPerPopupLlogari = "RegjistrimDokumentashKategoriShpenzimi";
    popupUniversal.SetHeaderText(hfState.Get("popupKategoriShpenzimi"));
    popupUniversal.SetContentUrl('LupaKategoriShpenzimi.aspx?lloji=JoPrind&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaLlogaria, heightLupaLlogaria);
    popupUniversal.Show();
}

function ButtonClickCmimi(idFushe) {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    if (grida.getTekstQelize('txtKodi', idRresht) !== '') {
        var detajim = merrDetajim(grida, idRresht);
        callWebserviceComboCmimArtikulliRow(grida.getTekstQelize('txtKodi', idRresht), NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), grida.getTekstQelize('cmbNjesia', idRresht), txtKursi.GetText() == "" ? 1 : txtKursi.GetText(), parseInt(idRresht), grida.getTekstQelize('txtSasia', idRresht), idFushe, detajim);
    }
}

//function beginWebserviceCmimArtikulliRow(idRreshti) {
//    var counterCmimi = 0;
//    if ($("#txtCmimi" + idRreshti).data('pending') && $("#txtCmimi" + idRreshti).data('pending') != "")
//        counterCmimi = parseInt($("#txtCmimi" + idRreshti).data('pending'));
//    $("#txtCmimi" + idRreshti).data('pending', counterCmimi + 1);

//    var counterCmimiTvsh = 0;   
//    if ($("#txtCmimiTvsh" + idRreshti).data('pending') && $("#txtCmimiTvsh" + idRreshti).data('pending') != "")
//        counterCmimiTvsh = parseInt($("#txtCmimiTvsh" + idRreshti).data('pending'));   
//    $("#txtCmimiTvsh" + idRreshti).data('pending', counterCmimiTvsh + 1);
//}

//function endWebserviceCmimArtikulliRow(idRreshti) {
//    if ($("#txtCmimi" + idRreshti).data('pending') && $("#txtCmimi" + idRreshti).data('pending') != "") {
//        var counterCmimi = $("#txtCmimi" + idRreshti).data('pending');
//        if (counterCmimi == 1)
//            $("#txtCmimi" + idRreshti).removeData('pending');
//        else
//            $("#txtCmimi" + idRreshti).data('pending', counterCmimi - 1);
//    }

//    if ($("#txtCmimiTvsh" + idRreshti).data('pending') && $("#txtCmimiTvsh" + idRreshti).data('pending') != "") {
//        var counterCmimiTvsh = $("#txtCmimiTvsh" + idRreshti).data('pending');
//        if (counterCmimiTvsh == 1)
//            $("#txtCmimiTvsh" + idRreshti).removeData('pending');
//        else
//            $("#txtCmimiTvsh" + idRreshti).data('pending', counterCmimiTvsh - 1);
//    }
//}

function callWebserviceCmimArtikulliRow(kodartikulli, nivel, date, monedhe, njesi, kursi, idRreshti, sasi, detajim, mosVendosCmimZero) {//po
    myJQGrid.beginWebserviceCmimArtikulliRow(idRreshti, "txtCmimi");
    myJQGrid.beginWebserviceCmimArtikulliRow(idRreshti, "txtCmimiTvsh");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheCmimArtikulliRow"),
        data: JSON.stringify({ idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, kodArtikulli: kodartikulli, nivelCmimi: nivel, date: date, monedha: monedhe, njesia: njesi, kursi: kursi, idRreshti: idRreshti, sasi: sasi, shitjeblerje: (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? 0 : 1, detajim: detajim })
    }).done(function (res) {
        myJQGrid.endWebserviceCmimArtikulliRow(idRreshti, "txtCmimi");
        myJQGrid.endWebserviceCmimArtikulliRow(idRreshti, "txtCmimiTvsh");
        if (mosVendosCmimZero && res[1] == 0)
            return;
        SucceededCallbackCmimArtikulliRow(res);
    });
}

function callWebserviceComboCmimArtikulliRow(kodartikulli, nivel, date, monedhe, njesi, kursi, idRreshti, sasi, idFushe, det) {//po
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheCmimetSipasNiveleveArtComboMeKodRow"),
        data: JSON.stringify({ kodi: kodartikulli, idNdermarrje: pageState.idNdermarrje, idPerdorues: pageState.idPerdoruesi, data: date, monedha: monedhe, njesia: njesi, kursi: kursi, index: idRreshti, sasi: sasi, nivel: nivel, shitjeblerje: (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? 0 : 1, detajim: det })
    }).done(function (result) {
        SucceededCallbackComboCmimeArtikulli(result, idRreshti, idFushe);
    });
}

function callWebserviceZbritjeArtikulliRow(KodArtikulli, ZbritjaKlientit, data, njesi, idRreshti) {//po

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheZbritjeAnalitikeArtikulliRow"), data: JSON.stringify({ kodArtikulli: KodArtikulli, ZbritjaKlientit: ZbritjaKlientit, date: data, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
    }).done(function (zbritje) {
        SucceededCallbackZbritjeArtikulliRow(zbritje, idRreshti);
    });
}

/*
Function: SucceededCallbackCmimArtikulli
    SucceededCallbackCmimArtikulliRow
Vendos cmimin qe i eshte caktuar artikullit(nese ka).
*/
function SucceededCallbackCmimArtikulliRow(result, idFushe) {//po

    var grida = $(pageState.gridaSelector);
    var idRreshti = result[0];
    var cm = result[1];
    if (grida.getTekstQelize('txtKodi', idRreshti) == '') {
        return;
    }

    if (cm == 0 && cmimzero == 2) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetCmimZeroNeGride"));
        if (pageState.kushte.F != 1)
            $("#txtCmimi" + idRreshti).focus();
    }
    if (cm == 0 && cmimzero == 1)
        myMesazh.ShtoMesazhInformues('Kujdes ka cmim zero ne gride');

    if (cm != '' && cm != '0') {
        hfState.Set('CmimiArt', cm);
        grida.setTekstQelize('txtCmimi', idRreshti, cm);
    }
    else
        grida.setTekstQelize('txtCmimi', idRreshti);

    vendosVlereKomisioniSipasArtikullitAndKlientit(pageState.Klient, idRreshti, grida);

    hfState.Set('CmimiArtPerKontroll', cm);
    if (grida.getTekstQelize('txtSasia', idRreshti) == '' || grida.getTekstQelize('txtSasia', idRreshti) == '0')
        grida.setTekstQelize('txtSasia', idRreshti);

    changedCmimireshti(null, null, 'txtCmimi', idRreshti);
        //$.ajax({
    //    /*async: false,*/
    //    url: "https://europe-west1-delta-327121.cloudfunctions.net/median_mad_input_check",
    //    data: JSON.stringify({ KODI: grida.getTekstQelize('txtKodi', idRreshti), CMIMI: cm, idndermarje: 11107, organizata: "cinitest" }),
    //    complete: function (res) {
    //        if (res.status == 'Cmimi mund te jete gabim.') {
    //            $("#txtCmimi" + idRreshti).css('background-color', '#FFA500');
    //        } else if (res.status == 'Cmimi duket ok.') {
    //            $("#txtCmimi" + idRreshti).css('background-color', '#42c947');
    //        }
    //    },
    //    success: function (res) {
    //        if (res.status == 'Cmimi mund te jete gabim.') {
    //            $("#txtCmimi" + idRreshti).css('background-color', '#FFA500');
    //        } else if (res.status == 'Cmimi duket ok.') {
    //            $("#txtCmimi" + idRreshti).css('background-color', '#42c947');
    //        }
    //    }
    //})
}

function SucceededCallbackComboCmimeArtikulli(result, idRresht, idFushe) {
    var grida = $(pageState.gridaSelector);
    var cmimet = new Array();
    cmimet = result.cmimet;
    var arrayOptions = new Array();
    var j = 0;
    var formati = JSON.parse(hfState.Get("formatMonedhe"));
    var shifraPasPresjesCmimi = parseInt(formati.ShifraPasPresjesCmimi);
    if (cmimet.length > 0) {
        var normaTvsh = grida.getNorma('cbTVSH', idRresht, 'cmbLloji', 'txtIdKodi', memoryArt, memoryLlog);
        for (var i = 0; i < cmimet.length; i++) {
            if (cmimet[i].cmimi === 0 || cmimet[i].cmimi === '0')
                continue;
            var objCmimi = new Object();
            var cmimi;
            if (idFushe == "txtCmimi")
                cmimi = cmimet[i].cmimi;
            else
                var cmimi = (cmimet[i].cmimi * (1 + normaTvsh / 100)).toFixed(7).replace(/\.?0+$/, '');

            objCmimi.value = cmimi;
            objCmimi.desc = cmimet[i].pershkrimiNivelCmimi;
            arrayOptions[j] = objCmimi;
            j++;
        }
    }
    myJQGrid.SucceededCallbackCmimi(arrayOptions, "#" + idFushe + idRresht, '#txtKodi' + idRresht);
}

function SucceededCallbackZbritjeArtikulliRow(zbritje, idRreshti) {//po
    var grida = $(pageState.gridaSelector);
    var klienti = JSON.parse(pageState.Klient);

    if (!zbritje) {
        grida.setTekstQelize("txtZbritjaVlere", idRreshti, grida.getVlereDefault("txtZbritjaVlere"));
        grida.setTekstQelize("txtZbritja", idRreshti, grida.getVlereDefault("txtZbritja"));
        changedZbritjaReshti(null, idRreshti);
        changell(null, idRreshti);
        return;
    }
    var njesia = grida.getTekstQelize("cmbNjesia", idRreshti);
    var artikulli = memoryArt.Get(grida.getTekstQelize("txtIdKodi", idRreshti));
    var valueZbritje = artikulli.KodNjesia1 == njesia ? zbritje.Zbritja : zbritje.Zbritja2;
    switch (zbritje.LlojZbritje) {
        case Utils.llojZbritje.Perqindje:
            grida.setTekstQelize("txtZbritja", idRreshti, klienti.kf.LlogaritKomision && grida.merrTeDhenaPerQelizen("txtKodi", idRreshti, "MeKomision") ? 0 : valueZbritje);
            grida.vendosTeDhenaPerQelizen("txtKodi", idRreshti, "ZbritjaKlientit", valueZbritje);
            grida.setTekstQelize("txtZbritjaVlere", idRreshti, grida.getVlereDefault("txtZbritjaVlere"));
            grida.setTekstQelize("txtLlojZbritje", idRreshti, "Perqindje", null, null, myelemComboZbritja);
            changell(null, idRreshti);
            changedZbritjaReshti("txtZbritja", idRreshti);
            break;
        case Utils.llojZbritje.Vlere:
            grida.setTekstQelize("txtZbritjaVlere", idRreshti, valueZbritje);
            grida.setTekstQelize("txtZbritja", idRreshti, klienti.kf.LlogaritKomision && grida.merrTeDhenaPerQelizen("txtKodi", idRreshti, "MeKomision") ? 0 : grida.getVlereDefault("txtZbritja"));
            grida.setTekstQelize("txtLlojZbritje", idRreshti, "Vlere", null, null, myelemComboZbritja);
            changell(null, idRreshti);
            changedZbritjaReshti("txtZbritjaVlere", idRreshti);
            break;
        default:
            myMesazh.ShtoMesazhGabimi("Lloj zbritje e panjohur: " + zbritje.LlojZbritje);
            break;
    }
}

function ButtonClickDetajimet2() {
    ButtonClickDetajim(2);
}

function ButtonClickDetajimet() {
    ButtonClickDetajim(1);
}

/*
Function: ButtonClickDetajimet

Hap lupen e detajimeve te artikullit.
*/
function ButtonClickDetajim(lloji) {//po    
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    identifikuesRreshti = idRresht;    
    if (grida.getTekstQelize('cmbLloji', idRresht) !== "Artikull")
        return;

    //var magazine = grida.getTekstQelize('txtMagazina', idRresht);
    var magazine = (pageState.kushte.GJDM || (!pageState.kushte.GJDM && lloji == 1 ? hfState.Get("SDGMZ1") : hfState.Get("SDGMZ2"))) ? grida.getTekstQelize('txtMagazina', idRresht) : '';
    //if (pageState.kushte.GJDM)
    //    magazine = grida.getTekstQelize('txtMagazina', idRresht);
    //marr vleren e IdKonfigurim te luper se detajimeve
    var hfDetajim = document.getElementById("hfGridaDetajimi");
    var vleraKodit = grida.getTekstQelize('txtIdKodi', idRresht);
    var queryString = 'idArtikulli=' + vleraKodit + '&mag=' + magazine + '&vjenNga=' + pageState.veprimi + '&lloji=' + lloji + '&idKonfigAmbjente=' + hfDetajim.value + '&detajimi1=' + grida.getTekstQelize('txtDetajimi', idRresht) + '&dateDok=' + data_DateEdit.GetText();

    myButtonClickLupa.ButtonClickGjeneral('LupaDetajimArtikulliRegjistrim.aspx?' + queryString, hfState.Get("msgZgjidhDetajimArtikulli"), widthLupaDetajim, heightLupaDetajim);
}

function ButtonClickLlogShpenzimi() {
    //marr vleren e IdKonfigurim te luper se detajimeve
    var hfLupaLlogShpenz = document.getElementById("hfLupaLlogShpenz");
    identikuesPerPopupLlogari = "RegjistrimDokumentashLlogShpenzimi";
    popupUniversal.SetHeaderText(hfState.Get("headerPopUpText"));
    popupUniversal.SetContentUrl('LupaLlogaria.aspx?idKonfigAmbjente=' + hfLupaLlogShpenz.value);
    popupUniversal.SetSize(widthLupaLlogaria, heightLupaLlogaria);
    popupUniversal.Show();
}

/*
Function: keyPressDetajimi

Shton nje rresht te ri ne gride nese nuk ka deh therret funksionin <callWebserviceDetajimi>
*/
function keyPressDet(event) {
    keyPressDetajimi(event, 1);
}

function keyPressDet2(event) {
    keyPressDetajimi(event, 2);
}

function keyPressDetajimi(event, lloji) {//po
    var idRresht = $(pageState.gridaSelector).getLastSel2();
    var vlera = event.target.value;
    if (vlera == "" && lloji == 1)
        $('#' + arrayIdKolonaGrides[6] + idRresht).autocomplete("close");
    else if (vlera == "" && lloji == 2)
        $('#' + arrayIdKolonaGrides[7] + idRresht).autocomplete("close");
    else
        callWebserviceDetajimi(vlera, lloji);
}

function isValidZbritjePerqindje(grida, zbritjePerqindje, idRreshti) {
    if (zbritjePerqindje == ".") {
        grida.setTekstQelize("txtZbritja", idRreshti, "0.");
        return { kthehu: true, zbritjePerqindje: zbritjePerqindje };
    }
    if ((hfState.Get("LZAN") && zbritjePerqindje.toString().indexOf("-") == 0 && isNaN(zbritjePerqindje.toString().substr(1)))
        || (!hfState.Get("LZAN") && (zbritjePerqindje === "" || isNaN(zbritjePerqindje)))) {
        if (idRreshti == grida.getLastSel2())
            $("#" + "txtZbritja" + idRreshti).focus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimZbritjaNumer"));
        zbritjePerqindje = grida.getVlereDefault("txtZbritja");
    }
    if (hfState.Get("LZAN")) {
        if ((zbritjePerqindje.toString().indexOf("-") == 0 && zbritjePerqindje.toString().substr(1) > 100) || (zbritjePerqindje.toString().indexOf("-") != 0 && zbritjePerqindje > 100)) {
            if (idRreshti == grida.getLastSel2())
                $("#" + "txtZbritja" + idRreshti).focus();
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZbritjaNegativeVlerat"));
            zbritjePerqindje = grida.getVlereDefault("txtZbritja");
            grida.setTekstQelize("txtZbritja", idRreshti, zbritjePerqindje);
        }
    }
    else if (zbritjePerqindje < 0 || zbritjePerqindje > 100) {
        if (idRreshti == grida.getLastSel2())
            $("#" + "txtZbritja" + idRreshti).focus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZbritjaVlerat"));
        zbritjePerqindje = grida.getVlereDefault("txtZbritja");
        grida.setTekstQelize("txtZbritja", idRreshti, zbritjePerqindje);
    }
    return { kthehu: false, zbritjePerqindje: zbritjePerqindje };
}

function isValidZbritjeVlere(grida, zbritjeVlere, idRreshti) {
    if (zbritjeVlere == ".") {
        grida.setTekstQelize("txtZbritjaVlere", idRreshti, "0.");
        return { kthehu: true, zbritjeVlere: zbritjeVlere };
    }
    if ((hfState.Get("LZAN") && zbritjeVlere.toString().indexOf("-") == 0 && isNaN(zbritjeVlere.toString().substr(1)))
        || (!hfState.Get("LZAN") && (zbritjeVlere === "" || isNaN(zbritjeVlere)))) {
        $("#" + "txtZbritjaVlere" + idRreshti).focus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimZbritjaNumer"));
        zbritjeVlere = grida.getVlereDefault("txtZbritjaVlere");
    }
    return { kthehu: false, zbritjeVlere: zbritjeVlere };
}

function changedZbritjaReshti(kontrollzbritja, idRreshti) { //po
    var grida = $(pageState.gridaSelector);
    var kodi = grida.getTekstQelize("txtKodi", idRreshti);
    if (kodi == "")
        return;
    var zbritjePerqindje = grida.getTekstQelize("txtZbritja", idRreshti);
    var zbritjeVlere = grida.getTekstQelize("txtZbritjaVlere", idRreshti);
    var cmimi = grida.getTekstQelize("txtCmimi", idRreshti);
    var cmimiTvsh = grida.getTekstQelize("txtCmimiTvsh", idRreshti);
    if (cmimi == 0) {
        grida.setTekstQelize("txtVlefta", idRreshti, grida.getVlereDefault("txtVlefta"));
        grida.setTekstQelize("txtVleftaTVSH", idRreshti, grida.getVlereDefault("txtVleftaTVSH"));
        ShtoRreshtaKomision();
        updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
        return;
    }
    var resultValidZbritjePerqindje = isValidZbritjePerqindje(grida, zbritjePerqindje, idRreshti);
    zbritjePerqindje = resultValidZbritjePerqindje.zbritjePerqindje;
    var resultValidZbritjeVlere = isValidZbritjeVlere(grida, zbritjeVlere, idRreshti);
    zbritjeVlere = resultValidZbritjeVlere.zbritjeVlere;
    switch (kontrollzbritja) {
        case "txtZbritja":
            if (resultValidZbritjePerqindje.kthehu)
                return;
            break;
        case "txtZbritjaVlere":
            if (resultValidZbritjeVlere.kthehu)
                return;
            break;
        default:
            //myMesazh.ShtoMesazhGabimi("Lloj kontroll zbritje i panjohur: " + kontrollzbritja);
            break;
    }

    var sasia = grida.getTekstQelize("txtSasia", idRreshti);
    var llojZbritje = grida.getTekstQelize("txtLlojZbritje", idRreshti);
    switch (llojZbritje) {
        case "Perqindje":
            zbritjeVlere = sasia * cmimi * zbritjePerqindje / 100;
            grida.setTekstQelize("txtZbritjaVlere", idRreshti, zbritjeVlere);
            break;
        case "Vlere":
            zbritjePerqindje = zbritjeVlere * 100 / sasia / cmimi;
            grida.setTekstQelize("txtZbritja", idRreshti, zbritjePerqindje);
            break;
        default:
            myMesazh.ShtoMesazhGabimi("Lloj zbritje e panjohur: " + llojZbritje);
            break;
    }

    var normaTvsh = grida.getNorma("cbTVSH", idRreshti, "cmbLloji", "txtIdKodi", memoryArt, memoryLlog);
    var vleftaTvsh, vlefta;
    vlefta = Math.round(parseFloat(sasia) * cmimi * 10000000) / 10000000 - zbritjeVlere;
    grida.setTekstQelize("txtVleftaTVSH", idRreshti, vlefta);
    vleftaTvsh = vlefta * (1 + normaTvsh / 100);
    grida.setTekstQelize("txtVlefta", idRreshti, vleftaTvsh);
    vendosVlereKomisioniSipasArtikullitAndKlientit(pageState.Klient, idRreshti, grida);
    ShtoRreshtaKomision();
    updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
}

function changedVleftaTVSH(kontrollzbritja, idRreshti) {
    var grida = $(pageState.gridaSelector);
    var vleftaTVSH = grida.getTekstQelize('txtVlefta', idRreshti);
    if (vleftaTVSH == "" || isNaN(vleftaTVSH)) {
        vleftaTVSH = grida.getVlereDefault('txtVlefta');
    }
    var normaTvsh = grida.getNorma('cbTVSH', idRreshti, 'cmbLloji', 'txtIdKodi', memoryArt, memoryLlog);
    normaTvsh = normaTvsh == undefined ? "0.00" : normaTvsh;

    grida.setTekstQelize('txtVleftaTVSH', idRreshti, (1 + parseFloat(normaTvsh) / 100) == 0 ? vleftaTVSH : (vleftaTVSH / (1 + parseFloat(normaTvsh) / 100)));

    vlefta = grida.getTekstQelize('txtVleftaTVSH', idRreshti);

    sasia = grida.getTekstQelize('txtSasia', idRreshti);
    if (sasia == 0)
        sasia = 1;

    var zbritja;
    if (grida.getTekstQelize('txtLlojZbritje', idRreshti) == 'Perqindje') {
        if (1 - parseFloat(grida.getTekstQelize('txtZbritja', idRreshti)) / 100 == 0)
            zbritja = 1;
        else
            zbritja = 1 - (grida.getTekstQelize('txtZbritja', idRreshti) / 100);
        if (!(zbritja == 1 && vlefta == 0 && grida.getTekstQelize('cmbLloji', idRreshti) != "Llogari")) {
            grida.setTekstQelize('txtCmimi', idRreshti, parseFloat(vlefta / sasia / zbritja));
            grida.setTekstQelize('txtCmimiTvsh', idRreshti, grida.getTekstQelize('txtCmimi', idRreshti) * (1 + normaTvsh / 100));
            grida.setTekstQelize('txtZbritjaVlere', idRreshti, (grida.getTekstQelize('txtSasia', idRreshti) * grida.getTekstQelize('txtCmimi', idRreshti) * grida.getTekstQelize('txtZbritja', idRreshti) / 100));
        }
    }
    else {
        var zbr = grida.getTekstQelize('txtZbritjaVlere', idRreshti);
        if (zbr == 0 || isNaN(zbr))
            zbr = grida.getVlereDefault('txtZbritjaVlere');
        grida.setTekstQelize('txtCmimi', idRreshti, parseFloat((vlefta + zbr) / sasia));
        grida.setTekstQelize('txtCmimiTvsh', idRreshti, grida.getTekstQelize('txtCmimi', idRreshti) * (1 + normaTvsh / 100));
        grida.setTekstQelize('txtZbritja', idRreshti, ((grida.getTekstQelize('txtZbritjaVlere', idRreshti) * 100) / (grida.getTekstQelize('txtSasia', idRreshti) * (grida.getTekstQelize('txtCmimi', idRreshti) == 0 ? 1 : grida.getTekstQelize('txtCmimi', idRreshti)))));

    }
    vendosVlereKomisioniSipasArtikullitAndKlientit(pageState.Klient, idRreshti, grida);
    ShtoRreshtaKomision();
    updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
}

function changedVlefta(kontrollzbritja, idRreshti) { //po
    var grida = $(pageState.gridaSelector);
    var formati = JSON.parse(hfState.Get("formatMonedhe"));
    var shifraPasPresjesVlefta = parseInt(formati.ShifraPasPresjesVlefta);
    var vlefta = grida.getTekstQelize('txtVleftaTVSH', idRreshti);
    if (vlefta == "" || isNaN(vlefta)) {
        vlefta = grida.getVlereDefault('txtVleftaTVSH');
    }
    var normaTvsh = grida.getNorma('cbTVSH', idRreshti, 'cmbLloji', 'txtIdKodi', memoryArt, memoryLlog);
    normaTvsh = normaTvsh == undefined ? "0.00" : normaTvsh;
    grida.setTekstQelize('txtVlefta', idRreshti, vlefta * (1 + normaTvsh / 100));
    var sasia = grida.getTekstQelize('txtSasia', idRreshti);
    if (sasia == 0)
        sasia = 1;
    if (grida.getTekstQelize('txtLlojZbritje', idRreshti) == 'Perqindje') {
        var zbr = grida.getTekstQelize('txtZbritja', idRreshti);
        if (zbr == 0 || isNaN(zbr))
            zbr = grida.getVlereDefault('txtZbritja');
        if (1 - parseFloat(zbr) / 100 == 0)
            zbritja = 1;
        else
            zbritja = 1 - parseFloat(zbr) / 100;
        if (!(zbritja == 1 && vlefta == 0 && grida.getTekstQelize('cmbLloji', idRreshti) != "Llogari")) {
            grida.setTekstQelize('txtCmimi', idRreshti, parseFloat(vlefta / sasia / zbritja));
            grida.setTekstQelize('txtCmimiTvsh', idRreshti, grida.getTekstQelize('txtCmimi', idRreshti) * (1 + normaTvsh / 100));
            grida.setTekstQelize('txtZbritjaVlere', idRreshti, (grida.getTekstQelize('txtSasia', idRreshti) * grida.getTekstQelize('txtCmimi', idRreshti) * grida.getTekstQelize('txtZbritja', idRreshti) / 100));
        }
    } else {

        var zbr = grida.getTekstQelize('txtZbritjaVlere', idRreshti);
        if (zbr == 0 || isNaN(zbr))
            zbr = grida.getVlereDefault('txtZbritjaVlere');
        grida.setTekstQelize('txtCmimi', idRreshti, parseFloat((vlefta + zbr) / sasia));
        grida.setTekstQelize('txtCmimiTvsh', idRreshti, grida.getTekstQelize('txtCmimi', idRreshti) * (1 + normaTvsh / 100));
        grida.setTekstQelize('txtZbritja', idRreshti, ((grida.getTekstQelize('txtZbritjaVlere', idRreshti) * 100) / (grida.getTekstQelize('txtSasia', idRreshti) * (grida.getTekstQelize('txtCmimi', idRreshti) == 0 ? 1 : grida.getTekstQelize('txtCmimi', idRreshti)))));
    }
    vendosVlereKomisioniSipasArtikullitAndKlientit(pageState.Klient, idRreshti, grida);
    ShtoRreshtaKomision();
    updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
}

function kontrolloCmiminArtSipasKushtit(cmimiVendosur) {
    var cmimiArtIPandryshuar = hfState.Get('CmimiArtPerKontroll');
    if (cmimiArtIPandryshuar != "" && cmimiArtIPandryshuar != undefined) {
        switch (percaktoCmimPerKF) {
            case 'Me i madh':
                {
                    if (cmimiVendosur < cmimiArtIPandryshuar) {
                        myMesazh.ShtoMesazhGabimi('Cmimi i artikullit nuk mund te jete me i vogel se cmimi i percaktuar ne nivelin me te cilin eshte i lidhur ky klient/furnitor!');
                        return false;
                    }
                }
                return true;
            case 'I barabarte':
                {
                    if (cmimiVendosur != cmimiArtIPandryshuar) {
                        myMesazh.ShtoMesazhGabimi('Cmimi i artikullit duhet te jete i barabarte me cmimin e percaktuar ne nivelin me te cilin eshte i lidhur ky klient/furnitor!');
                        return false;
                    }
                }
                return true;
            case 'Me i vogel':
                {
                    if (cmimiVendosur > cmimiArtIPandryshuar) {
                        myMesazh.ShtoMesazhGabimi('Cmimi i artikullit nuk mund te jete me i madh se cmimi i percaktuar ne nivelin me te cilin eshte i lidhur ky klient/furnitor!');
                        return false;
                    }
                }
                return true;
            default:
                return true;
        }
    }
    return true;
}

function changedCmimireshti(event, ui, emerKodi, idRreshti) {
    var grida = $(pageState.gridaSelector);
    var kodi = grida.getTekstQelize('txtKodi', idRreshti);
    if (kodi == '' || kodi == undefined)
        return;
    var cmimi = grida.getTekstQelize(emerKodi, idRreshti);

    if (cmimi === "" || isNaN(cmimi)) {
        grida.setTekstQelize(emerKodi, idRreshti);
    }
    if (cmimi === "" || isNaN(cmimi) || parseFloat(cmimi) == NaN) {
        cmimi = grida.getVlereDefault(emerKodi);
        grida.setTekstQelize(emerKodi, idRreshti, cmimi);
    }
    var normaTvsh = grida.getNorma('cbTVSH', idRreshti, 'cmbLloji', 'txtIdKodi', memoryArt, memoryLlog);
    grida.setTekstQelize('txtCmimiTvsh', idRreshti, cmimi * (1 + normaTvsh / 100));
    changedZbritjaReshti("txtZbritja", idRreshti);
    kontrolloSasiLimit(idRreshti, grida.getTekstQelize('txtIdKodi', idRreshti), false);
    var timeoutId = 0;
    clearTimeout(timeoutId);
    timeoutId = setTimeout(function() {

        $.ajax({
            /*async: false,*/
            url: urlMedianInputCheck,
            data: JSON.stringify({ KODI: kodi, CMIMI: cmimi, idndermarje: pageState.idNdermarrje, organizata: organizataMIC }),
            complete: function (res) {
                if (res.status == 'Cmimi mund te jete gabim.') {
                    $("#txtCmimi" + idRreshti).css('background-color', '#FFA500');
                    $("#txtCmimi" + idRreshti).attr("title", "Cmimi mund te jete gabim.");
                } else if (res.status == 'Cmimi duket ok.') {
                    $("#txtCmimi" + idRreshti).css('background-color', '#42c947');
                    $("#txtCmimi" + idRreshti).attr("title", "Cmimi duket ok.");
                }
            },
            error: function (res) {

            },
            success: function (res) {
                if (res.status == 'Cmimi mund te jete gabim.') {
                    $("#txtCmimi" + idRreshti).css('background-color', '#FFA500');
                    $("#txtCmimi" + idRreshti).attr("title", "Cmimi mund te jete gabim.");
                } else if (res.status == 'Cmimi duket ok.') {
                    $("#txtCmimi" + idRreshti).css('background-color', '#42c947');
                    $("#txtCmimi" + idRreshti).attr("title", "Cmimi duket ok.");
                }
            },
            failure: function (res) {
                console.log();
            },
            timeout: 4000,
        })

    }, 800);
    return;
}

function changedCmimireshtiTvsh(event, ui, emerKodi, idRreshti) {
    var grida = $(pageState.gridaSelector);
    var kodi = grida.getTekstQelize('txtKodi', idRreshti);
    if (kodi == '' || kodi == undefined)
        return;
    var cmimi = grida.getTekstQelize(emerKodi, idRreshti);
    // kontrolloCmiminArtSipasKushtit(cmimi);
    if (cmimi == "" || isNaN(cmimi)) {
        grida.setTekstQelize(emerKodi, idRreshti);
    }
    if (cmimi == "" || isNaN(cmimi) || parseFloat(cmimi) == NaN) {
        cmimi = grida.getVlereDefault(emerKodi);
        grida.setTekstQelize(emerKodi, idRreshti, cmimi);
    }
    var normaTvsh = grida.getNorma('cbTVSH', idRreshti, 'cmbLloji', 'txtIdKodi', memoryArt, memoryLlog);
    grida.setTekstQelize('txtCmimi', idRreshti, cmimi / (1 + normaTvsh / 100));
    changedZbritjaReshti("txtZbritja", idRreshti);
    kontrolloSasiLimit(idRreshti, grida.getTekstQelize('txtIdKodi', idRreshti), false);
    return;
}

function focusoutSasia() {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    editorKodi = $('#txtKodi' + idRresht);
    var kodi = grida.getTekstQelize('txtKodi', idRresht);
    var sasia = grida.getTekstQelize('txtSasia', idRresht);
    var gjeresia = grida.getTekstQelize('txtGjeresi', idRresht);
    var gjatesia = grida.getTekstQelize('txtGjatesi', idRresht);
    //eshteDokKthimi merr vleren true, vetem kur eshte modifikim i nje dokumenti te kthyer. Nuk eshte i barazvlefshem me kushtin pageState.lloji == 'kthim'
    var eshteDokKthimi = hfState.Get("eshteDokKthimi");

    if ((sasia === "" || isNaN(sasia)) && pageState.lloji != 'kthim' && !hfState.Get("eshteDokKthimi"))
        grida.setTekstQelize('txtSasia', idRresht);
    if ((pageState.lloji == 'kthim' || eshteDokKthimi) && sasia == '-')
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumer"));
    var sasiambetur = grida.getTekstQelize('txtSasiMbetur', idRresht);
    if (pageState.lloji == 'modifikim')
        sasiambetur += colTrup[idRresht - 1].Sasia;
    

    if (pageState.lloji == 'kthim' || eshteDokKthimi) {
        var art = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRresht));
        if (sasia === "" || isNaN(sasia))
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumer"));
        else if (sasia == 0)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
        else if (art.Klasa != '3' && !pageState.kushte["LSPK"]) {
            if (sasia > 0)
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiPozitiveKthimShitje"));
            else
                if (sasia < sasiambetur)
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiMbeturKthimShitje").replace('{0}', grida.getTekstQelize('txtSasiMbetur', idRresht)).replace('{1}', kodi));
            //grida.setTekstQelize('txtSasia', idRresht, sasiambetur);
            ndryshonSasia(idRresht);
            return;
        }
    }


    if (gjeresia === "" || isNaN(gjeresia))
        grida.setTekstQelize('txtGjeresi', idRresht);

    if (gjatesia === "" || isNaN(gjatesia))
        grida.setTekstQelize('txtGjatesi', idRresht);

    if (sasia == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
        grida.setFocus('txtSasia', idRresht);
        sasia = grida.getVlereDefault('txtSasia');
    }

    //if (eshteDokKthimi && sasia > 0) {
    //    myMesazh.ShtoMesazhGabimi('Nuk mund te ktheni sasi pozitive');
    //    grida.setTekstQelize('txtSasia', idRresht, sasiambetur);
    //    ndryshonSasia(idRresht);
    //    return;
    //}
    var detajim = -1;
    var idkodi = grida.getTekstQelize('txtIdKodi', idRresht);//heq serialet
    if (grida.getTekstQelize('txtDetajimi', idRresht) != "")
        detajim = grida.getTekstQelize('txtDetajimi', idRresht);
    if (gjendjeartminmax && kodi != '' && grida.getTekstQelize('cmbLloji', idRresht) != "Llogari") {
        if (idkodi != "") {
            var artikulli = memoryArt.Get(idkodi);
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
                data: JSON.stringify({ idartikulli: idkodi, mag: grida.getTekstQelize('txtMagazina', idRresht), idNdermarrje: pageState.idNdermarrje })
            }).done(function (result) { SucceededCallbackGjendjeArtikulli(result, artikulli); });
        }
    }


    if (memoryArt.Contains(idkodi)) {
        var artikulli = memoryArt.Get(idkodi);
        if (!artikulli.MeSeriale && hfSeriale.Contains(idkodi + '_' + idRresht)) {
            if (hfSasiSeriale.Contains(idkodi + '_' + idRresht)) {
                hfSasiSeriale.Set(idkodi + '_' + idRresht, grida.getTekstQelize('txtSasia', idRresht));
            }
        }
    }
}

function focusoutVlefta() {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var vlefta = grida.getTekstQelize('txtVleftaTVSH', idRresht);
    if (vlefta === "" || isNaN(vlefta))
        grida.setTekstQelize('txtVleftaTVSH', idRresht);
}

function focusoutVleftaTvsh() {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var vlefta = grida.getTekstQelize('txtVlefta', idRresht);
    if (vlefta === "" || isNaN(vlefta))
        grida.setTekstQelize('txtVlefta', idRresht);
}

function focusoutCmimiTvsh() {
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var cmimiMeTvsh = grida.getTekstQelize('txtCmimiTvsh', idRresht);
    if (cmimiMeTvsh === "" || isNaN(cmimiMeTvsh))
        grida.setTekstQelize('txtCmimiTvsh', idRresht);
}

function changedGjeresiGjatesiSasiPermase(s) {
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    editorGjeresia = $("#" + 'txtGjeresi' + idRresht);
    editorGjatesia = $("#" + 'txtGjatesi' + idRresht);
    editorSasiPermasa = $("#" + 'txtSasiPermase' + idRresht);

    var gjeresi = grida.getTekstQelize('txtGjeresi', idRresht);
    if (gjeresi === "" || isNaN(gjeresi)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjeresiaNumer"));
        editorGjeresia.focus();
        grida.setTekstQelize('txtGjeresi', idRresht, grida.getVlereDefault('txtGjeresi'));
    }
    else if (gjeresi == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjeresiaJoZero"));
        editorGjeresia.focus();
        //   grida.setTekstQelize('txtGjeresi', idRresht, grida.getVlereDefault('txtGjeresi'));
    }
    var gjatesi = grida.getTekstQelize('txtGjatesi', idRresht);
    if (gjatesi === "" || isNaN(gjatesi)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjatesiNumer"));
        editorGjatesia.focus();
        grida.setTekstQelize('txtGjatesi', idRresht, grida.getVlereDefault('txtGjatesi'));
    }
    else if (gjatesi == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjatesiaJoZero"));
        editorGjatesia.focus();
        //grida.setTekstQelize('txtGjatesi', idRresht, grida.getVlereDefault('txtGjatesi'));
    }
    var sasipermasa = grida.getTekstQelize('txtSasiPermase', idRresht);
    if (sasipermasa === "" || isNaN(sasipermasa)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiPermaseNumer"));
        editorSasiPermasa.focus();
        sasipermasa = grida.getVlereDefault('txtSasiPermase');
        //  grida.setTekstQelize('txtSasiPermase', idRresht);
        grida.setTekstQelize('txtSasiPermase', idRresht, grida.getVlereDefault('txtSasiPermase'));
    }
    else if (sasipermasa == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiPermaseJoZero"));
        editorSasiPermasa.focus();
        //     grida.setTekstQelize('txtSasiPermase', idRresht, grida.getVlereDefault('txtSasiPermase'));
    }
    var sasia = gjatesi * gjeresi * sasipermasa;
    grida.setTekstQelize('txtSasia', idRresht, sasia);
    changedSasiaPaFocusTeSasia(null, idRresht);// to check per ndonje menyre tjeter jepte problem vendosja e fokusit te sasia kur ishe duke modifikuar gjeresine ose gjatesine
}

function changedSasiaPaFocusTeSasia(event, idRresht) {
    var grida = $(pageState.gridaSelector);
    if (typeof idRresht == 'undefined')
        idRresht = grida.getLastSel2();

    var sasia;
    var sasia = grida.getTekstQelize('txtSasia', idRresht);
    if (sasia == '.') {
        grida.setTekstQelize('txtSasia', idRresht, '0.');
        return;
    }
    if (sasia === "" || isNaN(sasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumer"));
        //  grida.setFocus('txtSasia', idRresht);
        sasia = grida.getVlereDefault('txtSasia');
    }
    else
        if (sasia == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
            //   grida.setFocus('txtSasia', idRresht);
            sasia = grida.getVlereDefault('txtSasia');
            //  grida.setTekstQelize('txtSasia', idRresht, sasia);
        }
    var art = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRresht));
    //fusha sasi litra per tollonat

    if (art && art.Klasa === 4) {
        //ktheArtikullPerberes - todo nestila duhet ktheKoefArtPerberes
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPerberes"), data: JSON.stringify({ id: art.IdArtikulli }) }).done(function (koefArtPerbere) { vendosSasiLiter(koefArtPerbere, idRresht); updateTotalSasiLitra(); });
    }
    if (grida.getTekstQelize('txtRezervuar', idRresht) == 'true' && sasiautomatike)
        grida.setTekstQelize('txtSasiaRez', idRresht, sasia);

    changedTVSHReshti(idRresht);

    kontrolloGjendje(grida.getTekstQelize('txtIdKodi', idRresht), grida.getTekstQelize('txtMagazina', idRresht), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRresht), grida.getTekstQelize('txtDetajimi2t', idRresht), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRresht);
    kontrolloSasiLimit(idRresht, grida.getTekstQelize('txtIdKodi', idRresht), true);

    myJQGrid.cancelQuickChangeSasi('txtSasia' + idRresht, grida.getTekstQelize('txtKodi', idRresht), sasia, grida.getTekstQelize('cmbNjesia', idRresht), changeCmimSipasSasi);
}

function changedSasia(event, idRresht) {
    ndryshonSasia(idRresht);
}
function ndryshonSasia(idRresht) {
    var grida = $(pageState.gridaSelector);
    if (typeof idRresht == 'undefined')
        idRresht = grida.getLastSel2();

    var sasia;
    var sasia = grida.getTekstQelize('txtSasia', idRresht);
    if (sasia == '.') {
        grida.setTekstQelize('txtSasia', idRresht, '0.');
        return;
    }



    if ((sasia === "" || isNaN(sasia)) && pageState.lloji != 'kthim' && !hfState.Get("eshteDokKthimi")) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumer"));
        grida.setFocus('txtSasia', idRresht);
        sasia = grida.getVlereDefault('txtSasia');
    }

    var art = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRresht));
    //fusha sasi litra per tollonat

    if (art && art.Klasa === 4) {
        //ktheArtikullPerberes - todo nestila duhet ktheKoefArtPerberes
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPerberes"), data: JSON.stringify({ id: art.IdArtikulli }) }).done(function (koefArtPerbere) { vendosSasiLiter(koefArtPerbere, idRresht); updateTotalSasiLitra(); });
    }
    if (grida.getTekstQelize('txtRezervuar', idRresht) == 'true' && sasiautomatike)
        grida.setTekstQelize('txtSasiaRez', idRresht, sasia);

    changedTVSHReshti(idRresht);

    kontrolloGjendje(grida.getTekstQelize('txtIdKodi', idRresht), grida.getTekstQelize('txtMagazina', idRresht), data_DateEdit.GetDate(), grida.getTekstQelize('txtDetajimi', idRresht), grida.getTekstQelize('txtDetajimi2t', idRresht), pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0, pageState.idNdermarrje, cmbModeli.GetValue(), idRresht);
    kontrolloSasiLimit(idRresht, grida.getTekstQelize('txtIdKodi', idRresht), true);

    myJQGrid.cancelQuickChangeSasi('txtSasia' + idRresht, grida.getTekstQelize('txtKodi', idRresht), sasia, grida.getTekstQelize('cmbNjesia', idRresht), changeCmimSipasSasi);
}

function changeCmimSipasSasi(kod, sasia, last, njesia) {
    if (kod != "" && pageState.kushte.VCVS && !pageState.kushte.ZT && ndryshocmime && pageState.lloji != 'kthim' && pageState.lloji != 'modifikim') {//&&pageState.veprimi != 'blerje'
        callWebserviceCmimArtikulliRow(kod, NivelCmimi, data_DateEdit.GetText(), cmbMonedha.GetText(), njesia, txtKursi.GetText(), last, sasia, merrDetajim($(pageState.gridaSelector), last));
    }
}

function changedTVSH(event, idRreshti) {//po
    if (!idRreshti)
        idRreshti = $(pageState.gridaSelector).getLastSel2();
    changedTVSHReshti(idRreshti);
}

function changedTVSHReshti(idRresht) {//po
    var grida = $(pageState.gridaSelector);
    var cmimi = grida.getTekstQelize('txtCmimi', idRresht);
    var normaTvsh = grida.getNorma('cbTVSH', idRresht, 'cmbLloji', 'txtIdKodi', memoryArt, memoryLlog);
    if (!normaTvsh)
        normaTvsh = 0;
    // var netoNivelCmimi = grida.getNetoNivelCmimi(memoryArt, idRresht, 'cmbLloji');
    //if (netoNivelCmimi == 1)
    //    cmimi = cmimi / (1 + normaTvsh / 100);

    if (grida.getTekstQelize('txtLlojZbritje', idRresht) == 'Perqindje') {
        grida.setTekstQelize('txtVleftaTVSH', idRresht, (grida.getTekstQelize('txtSasia', idRresht) * cmimi * (1 - grida.getTekstQelize('txtZbritja', idRresht) / 100)));
        grida.setTekstQelize('txtZbritjaVlere', idRresht, (grida.getTekstQelize('txtSasia', idRresht) * grida.getTekstQelize('txtCmimi', idRresht) * grida.getTekstQelize('txtZbritja', idRresht) / 100));
    }
    else {
        var emeruesi = grida.getTekstQelize('txtSasia', idRresht) * grida.getTekstQelize('txtCmimi', idRresht);
        grida.setTekstQelize('txtZbritja', idRresht, emeruesi == 0 ? 0 : (grida.getTekstQelize('txtZbritjaVlere', idRresht) * 100 / emeruesi));
        grida.setTekstQelize('txtVleftaTVSH', idRresht, (grida.getTekstQelize('txtSasia', idRresht) * grida.getTekstQelize('txtCmimi', idRresht) - grida.getTekstQelize('txtZbritjaVlere', idRresht)));
    }
    changedCmimireshti(null, null, 'txtCmimi', idRresht);
    var txtVleftaTvsh = grida.getTekstQelize('txtVleftaTVSH', idRresht);
    grida.setTekstQelize('txtVlefta', idRresht, txtVleftaTvsh * (1 + parseFloat(normaTvsh) / 100));
    vendosVlereKomisioniSipasArtikullitAndKlientit(pageState.Klient, idRresht, grida);
    ShtoRreshtaKomision();
    updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
}

function VendosTekstinSipasVleres(textBox) {
    var vlere = textBox.GetValue();
    var text = textBox.GetText();
    if (Utils.IsNullOrEmpty(text) && Utils.IsNullOrEmpty(vlere))
        return;
    //if (vlere != text)
    //    textBox.SetText(textBox.GetValue());
}

function changedPerqindje() {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var zbritTotalPerqindje;
    VendosTekstinSipasVleres(txtPerqindje);
    //var formati = JSON.parse(hfState.Get("formatMonedhe"));
    if (txtPerqindje.GetText() === "" || isNaN(parseFloat(txtPerqindje.GetText())) || parseFloat(txtPerqindje.GetText()) < 0 || parseFloat(txtPerqindje.GetText()) > 100) {
        if (txtPerqindje.GetText() === "")
            zbritTotalPerqindje = 0;

        if (isNaN(parseFloat(txtPerqindje.GetText()))) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerqindjaDuhetJeteNumer"));
            setZbritjeTotal(Utils.llojZbritje.Perqindje, 0, false);
            return;
        }
        if (parseFloat(txtPerqindje.GetText()) < 0 || parseFloat(txtPerqindje.GetText()) > 100) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerqindjaVleraNdermjet"));
            setZbritjeTotal(Utils.llojZbritje.Perqindje, 0, false);
            return;
        }
    }
    else
        zbritTotalPerqindje = parseFloat(txtPerqindje.GetText());
    var totali = calcTotale(grida).totali;
    zbritTotalVlere = parseFloat((totali * parseFloat(zbritTotalPerqindje) / 100));
    txtVlefte.SetText(zbritTotalVlere);
    updateTotalet(zbritTotalPerqindje, zbritTotalVlere);
}

function changedVlefteZbritje() {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    var zbritTotalVlere;
    VendosTekstinSipasVleres(txtVlefte);
    if (isNaN(parseFloat(txtVlefte.GetText())) || txtVlefte.GetText() === '') {//|| txtVlefte.GetText() < 0
        if (isNaN(parseFloat(txtVlefte.GetText())) || txtVlefte.GetText() === '') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraDuhetJeteNumer"));
            setZbritjeTotal(Utils.llojZbritje.Vlere, 0, false);
            return;
        }
    }
    else
        zbritTotalVlere = parseFloat(txtVlefte.GetText());
    var totali = calcTotale(grida).totali;
    if (totali == 0)
        zbritTotalPerqindje = 0;
    else
        zbritTotalPerqindje = parseFloat(zbritTotalVlere) * 100 / totali;

    if (zbritTotalPerqindje < 0 || zbritTotalPerqindje > 100) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZbritjaJoMeMadheTotal"));
        setZbritjeTotal(Utils.llojZbritje.Vlere, 0, false);
        return;
    }
    else
        txtPerqindje.SetText(zbritTotalPerqindje);

    updateTotalet(zbritTotalPerqindje, zbritTotalVlere);
}

function calcTotale(grida) {
    var idTe = grida.jqGrid('getDataIDs');
    var nenTotali = 0, totali = 0, totalSasiLitra = 0;
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtKodi', idTe[i]) != "") {
            var vleftaMeTvsh = grida.getTekstQelize('txtVlefta', idTe[i]);
            if (vleftaMeTvsh === "")
                vleftaMeTvsh = grida.getVlereDefault('txtVlefta');
            var sasiLitra = grida.getTekstQelize('txtSasiLitra', idTe[i]);
            if (sasiLitra === "")
                sasiLitra = 0;
            var vleftaPaTvsh = grida.getTekstQelize('txtVleftaTVSH', idTe[i]);
            if (vleftaPaTvsh === "")
                vleftaPaTvsh = grida.getVlereDefault('txtVleftaTVSH');
            nenTotali = nenTotali + parseFloat(vleftaPaTvsh);
            totali = totali + parseFloat(vleftaMeTvsh);
            totalSasiLitra = totalSasiLitra + parseFloat(sasiLitra);
        }
    }
    return { nenTotali: nenTotali, totali: totali, totalSasiLitra: totalSasiLitra };
}

function updateTotalet(zbritTotalPerqindje, zbritTotalVlere) {//po
    var grida = $(pageState.gridaSelector);
    var gridaKomision = $(pageState.gridaKomision);
    var idRresht = grida.getLastSel2();
    if (zbritTotalPerqindje === "" || ((pageState.veprimi == "blerje" && pageState.Kf.alternativaKushtZbritje == "Dokument shitje") || ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && pageState.Kf.alternativaKushtZbritje == "Dokument blerje"))) //todo Pati - ca sensi ka kjo???
        zbritTotalPerqindje = 0;
    if (zbritTotalVlere === "")
        zbritTotalVlere = 0;

    editorKodi = $("#txtKodi" + idRresht); 
    var totale = calcTotale(grida);

    if (cmbModeli.GetText().substring(0, 5) == "VFONE" && !pageState.kushte.ZBVFONE) {
        txtVlefte.SetText(0);
        zbritTotalVlere = 0;
        txtPerqindje.SetText(0);
        zbritTotalPerqindje = 0;
    }
    else {
        if (cmbLlojZbritje.GetValue() == Utils.llojZbritje.Perqindje) {
            zbritTotalVlere = parseFloat((totale.totali * parseFloat(zbritTotalPerqindje) / 100));
            txtVlefte.SetText(zbritTotalVlere);
        }
        else {
            if (totale.totali == 0)
                zbritTotalPerqindje = parseFloat(zbritTotalVlere) * 100 / 1;
            else
                zbritTotalPerqindje = parseFloat(zbritTotalVlere) * 100 / totale.totali;
            txtPerqindje.SetText(zbritTotalPerqindje);
        }
        if ($('#hfVlera').val() === "0") {
            txtPerqindje.SetText(100);
            zbritTotalPerqindje = 100;
            txtVlefte.SetText((totale.totali));
            zbritTotalVlere = totale.totali;
        }
        else if ($('#hfVlera').val() != '' && $('#hfVlera').val() != 0) {
            if (Utils.getUrlVar("shitje_blerje") == 'shitjediscount') {
                txtVlefte.SetText((totale.totali >= $('#hfVlera').val()) ? $('#hfVlera').val() : 0);
                zbritTotalVlere = (totale.totali >= $('#hfVlera').val()) ? $('#hfVlera').val() : 0;
            }
            else {
                txtVlefte.SetText((totale.totali - $('#hfVlera').val()) >= 0 ? (totale.totali - $('#hfVlera').val()) : 0);
                zbritTotalVlere = (totale.totali - $('#hfVlera').val()) >= 0 ? (totale.totali - $('#hfVlera').val()) : 0;

            }
            txtPerqindje.SetText(zbritTotalVlere * 100 / totale.totali);
            zbritTotalPerqindje = zbritTotalVlere * 100 / totale.totali;
        }

        if ($('#hfZbritja').val() != '' && $('#hfZbritja').val() != 0) {
            txtVlefte.SetText((totale.totali - $('#hfZbritja').val()) >= 0 ? ($('#hfZbritja').val()) : 0);
            zbritTotalVlere = (totale.totali - $('#hfZbritja').val()) >= 0 ? ($('#hfZbritja').val()) : 0;
            txtPerqindje.SetText(zbritTotalVlere * 100 / totale.totali);
            zbritTotalPerqindje = zbritTotalVlere * 100 / totale.totali;
        }
    }
    var rreshtaTeGridesId = grida.jqGrid('getDataIDs');
    var shumaKomision = 0;
    for (var i = 0; i < rreshtaTeGridesId.length; i++) {
        shumaKomision += Number(grida.getTekstQelize('txtVleraKomisionit', rreshtaTeGridesId[i]));
    }
    var zbritTotalVlerePaTvsh = totale.nenTotali * parseFloat(zbritTotalPerqindje) / 100;
    txtTotalMeZbritje1.SetText(parseFloat(totale.nenTotali).toFixed(pageState.formatVleftaDB) - parseFloat(shumaKomision).toFixed(pageState.formatVleftaDB));
    txtTotaliPaTVSH1.SetText(parseFloat(zbritTotalVlerePaTvsh).toFixed(pageState.formatVleftaDB));
    txtTotaliMeZbritjePaTVSH1.SetText(parseFloat(totale.nenTotali).toFixed(pageState.formatVleftaDB) - parseFloat(zbritTotalVlerePaTvsh).toFixed(pageState.formatVleftaDB));
    txtTVSH1.SetText((parseFloat((totale.totali - totale.nenTotali)).toFixed(pageState.formatVleftaDB) * (1 - parseFloat(zbritTotalPerqindje) / 100)));
    txtTotal1.SetText((parseFloat(totale.totali).toFixed(pageState.formatVleftaDB) - parseFloat(zbritTotalVlere).toFixed(pageState.formatVleftaDB)) - parseFloat(shumaKomision).toFixed(pageState.formatVleftaDB));
    txtTotalLitra.SetText(totale.totalSasiLitra);
    vendosTotaleMonedheFature();

    //gerta
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar')) {
        if ($("input[id$='hfPolitike']").val() != "" && $("input[id$='hfPolitike']").val() != undefined) {
            var politike = JSON.parse($("input[id$='hfPolitike']").val());
            if (politike.Lloji == 0) {
                //me zbritje do nothing here
            }
            else {
                // me pike
                txtPike.SetText(Math.floor(parseFloat(txtTotal2.GetText()) / politike.VleraPikes));
                txtTotalPike.SetText(parseFloat($('#hfTotalPikesh').val()) + parseFloat(txtPike.GetText()));
            }
        }
    }
    vendosKuponTatimor();
}

function changedPerqindjeAgjent(agjenti) {//po
    if (isNaN(txtPerqindjeAgjent.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerqindjaDuhetJeteNumer"));
        if (agjenti == '1')
            txtPerqindjeAgjent.SetText('0.00');
        else if (agjenti == '2')
            txtPerqindjeAgjent2.SetText('0.00');
        else txtPerqindjeAgjent3.SetText('0.00');
    }
    if (parseFloat(txtPerqindjeAgjent.GetText()) < 0 || parseFloat(txtPerqindjeAgjent.GetText()) > 100) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerqindjaVleraNdermjet"));
        if (agjenti == '1')
            txtPerqindjeAgjent.SetText('0.00');
        else if (agjenti == '2')
            txtPerqindjeAgjent2.SetText('0.00');
        else
            txtPerqindjeAgjent3.SetText('0.00');
    }
}
function vendosKuponTatimor() {

    //Ne hapjen e pare totaliVjeter == undefined ndaj merret parasysh ketu
    if (totaliVjeter === undefined && pageState.lloji == 'modifikim') {
        totaliVjeter = parseFloat(txtTotal2.GetText());
        if (hfState.Get('Status') != 0)
            return;
        else {
            if (cbKupon.GetChecked() && !serialShenuarNgaPerdoruesi && kontrolloNrAutomatik)
                myNrAuto.vendosNrAutomatikNrSerial(pageState.colAtrTrupi, pageState.colKontrollet, data_DateEdit.GetDate());
            return;
        }
    }
    else if (parseFloat(txtTotal2.GetText()) == totaliVjeter)
        return;

    totaliVjeter = parseFloat(txtTotal2.GetText());
    var limitishitjes = hfState.Get('limitishitjes');

    if (parseFloat(txtTotal2.GetText()) > limitishitjes) {
        if (pageState.lloji == 'modifikim' && txtNumerSerial.GetText() !== "")
            return;
        else {
            cbKupon.SetChecked(true);
            kupon = true;
        }
        if (!serialShenuarNgaPerdoruesi && kontrolloNrAutomatik)
            myNrAuto.vendosNrAutomatikNrSerial(pageState.colAtrTrupi, pageState.colKontrollet, data_DateEdit.GetDate());
    }
    else {
        if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && !serialShenuarNgaPerdoruesi && kupon) {
            if ((kuponKerkonKlienti || kuponKerkonKonfigurim) && kontrolloNrAutomatik)
                myNrAuto.vendosNrAutomatikNrSerial(pageState.colAtrTrupi, pageState.colKontrollet, data_DateEdit.GetDate());
            else {
                kupon = false;
                cbKupon.SetChecked(false);
                txtNumerSerial.SetText('');
            }
        }
        if (pageState.lloji == "konvertim" && txtNumerSerial.GetText() != "" && (limitishitjes == 0 || limitishitjes == ""))
            kupon = true;
        if (!kupon)
            cbKupon.SetChecked(false);
    }
}

function NdryshoiSeriali(s, e) {
    if (s.GetText() != "")
        serialShenuarNgaPerdoruesi = true;
}

function kuponclick(s, e) {
    kupon = s.GetChecked();

    //nese eshte plotesuar njehere fusha e serialit nuk duhet te humbase si vlere.
    if (!serialShenuarNgaPerdoruesi && kontrolloNrAutomatik) {
        myNrAuto.vendosNrAutomatikNrSerial(pageState.colAtrTrupi, pageState.colKontrollet, data_DateEdit.GetDate());

        //if (kupon)
        //    myNrAuto.vendosNrAutomatikNrSerial(pageState.colAtrTrupi, pageState.colKontrollet, data_DateEdit.GetDate());
        //else {
        //    var limitishitjes = hfState.Get('limitishitjes');
        //    if (parseFloat(txtTotal2.GetText()) > limitishitjes)
        //        myNrAuto.vendosNrAutomatikNrSerial(pageState.colAtrTrupi, pageState.colKontrollet, data_DateEdit.GetDate());
        //    else if (pageState.veprimi != 'blerje')
        //        txtNumerSerial.SetText('');
        //}
    }
}

/*
Function: vendosTotaletMeZbritje

Vendos totalet duke i hequr zbritjen
*/
function vendosTotaletMeZbritje() {
    var totali = txtTotal1.GetText();
    var tvshh = txtTVSH1.GetText();
    var vl;

    vl = totali - ((txtPerqindje.GetText() / 100) * totali);
    txtTotal1.SetText(parseFloat(vl));
    vl = txtTotal2.GetText() - ((txtPerqindje.GetText() / 100) * txtTotal2.GetText());
    txtTotal2.SetText(parseFloat(vl));
    vl = tvshh - ((txtPerqindje.GetText() / 100) * tvshh);
    txtTVSH1.SetText(parseFloat(vl));
    vl = txtTotalMeZbritje1.GetText() * ((txtPerqindje.GetText() / 100));
    txtTotaliPaTVSH1.SetText(parseFloat(vl));
    txtTotaliMeZbritjePaTVSH1.SetText(totali - parseFloat(vl));
    vl = 0;
}

/*
Function: vendosTotaleMonedheFature

Llogarit dhe vendos vlerat e totaleve ne varesi te monedhes dhe kursit te percaktuar ne fature
*/
function vendosTotaleMonedheFature() {//po
    var hfLidhur = $("input[id$='hfLidhur']");

    if (cmbMonedha.GetValue()) {
        var kursi = txtKursi.GetText();
        if (monklienti != "0" && (monklienti != cmbMonedha.GetValue())) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgVeprimeMonedheNdryshmekf"));
            cmbMonedha.SetValue(monklienti);
            callWebserviceKursi(monklienti);
        }
        if (cmbMonedha.GetValue() != pageState.idMonedheNdermarrje) {
            kursi = txtKursi.GetText();
            var konvSipasUrdherShitje = (hfTeDrejta.Get("KonvertimSipasUSH") == true && hfState.Get("konvNgaUshNeFsh") == true);
            if (konvSipasUrdherShitje)
                txtKursi.SetEnabled(false);
            else {
                if (pageState.lloji === 'modifikim') {
                    if (hfLidhur.val() === 'True')
                        txtKursi.SetEnabled(false);
                    else if (enablekursi != false)
                        txtKursi.SetEnabled(true);
                }
                else if (enablekursi != false)
                    txtKursi.SetEnabled(true);
            }
            if (isNaN(kursi)) {
                txtKursi.SetFocus();
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiDuhetNumer"));
                txtKursi.SetText("1");
            }
            else {
                txtTotal2.SetText(parseFloat(txtTotal1.GetText()).toFixed(pageState.formatVleftaDB) * kursi);
                txtTotalMeZbritje2.SetText(parseFloat(txtTotalMeZbritje1.GetText()).toFixed(pageState.formatVleftaDB) * kursi);
                txtTVSH2.SetText(parseFloat(txtTVSH1.GetText()).toFixed(pageState.formatVleftaDB) * kursi);
                txtTotaliPaTVSH2.SetText(parseFloat(txtTotaliPaTVSH1.GetText()).toFixed(pageState.formatVleftaDB) * kursi);
                txtTotaliMeZbritjePaTVSH2.SetText(parseFloat(txtTotaliMeZbritjePaTVSH1.GetText()).toFixed(pageState.formatVleftaDB) * kursi);
            }
            if (pageState.EshteVisibletxtTotal2)
                txtTotal2.SetVisible(true);
            if (pageState.EshteVisibletxtTotalMeZbritje2)
                txtTotalMeZbritje2.SetVisible(true);
            if (pageState.EshteVisibletxtTVSH2)
                txtTVSH2.SetVisible(true);
            if (pageState.EshteVisibletxtTotaliPaTVSH2)
                txtTotaliPaTVSH2.SetVisible(true);
            if (pageState.EshteVisibletxtTotaliMeZbritjePaTVSH2)
                txtTotaliMeZbritjePaTVSH2.SetVisible(true);
            if (pageState.EshteVisiblelblMonedhaBaze)
                lblMonedhaBaze.SetVisible(true);
        }
        else {
            txtKursi.SetEnabled(false);
            txtTotal2.SetText(txtTotal1.GetText());
            txtTotalMeZbritje2.SetText(parseFloat(txtTotalMeZbritje1.GetText()).toFixed(pageState.formatVleftaDB));
            txtTVSH2.SetText(parseFloat(txtTVSH1.GetText()).toFixed(pageState.formatVleftaDB));
            txtTotaliPaTVSH2.SetText(parseFloat(txtTotaliPaTVSH1.GetText()).toFixed(pageState.formatVleftaDB));
            txtTotaliMeZbritjePaTVSH2.SetText(parseFloat(txtTotaliMeZbritjePaTVSH1.GetText()).toFixed(pageState.formatVleftaDB));
            txtTotal2.SetVisible(false);
            txtTotalMeZbritje2.SetVisible(false);
            txtTVSH2.SetVisible(false);
            txtTotaliPaTVSH2.SetVisible(false);
            txtTotaliMeZbritjePaTVSH2.SetVisible(false);
            lblMonedhaBaze.SetVisible(false);
        }
        if (cmbMonedhaPagese.GetValue() != pageState.idMonedheNdermarrje) {
            kursi = txtKursiPagese.GetText();
            txtKursiPagese.SetEnabled(true);
            if (isNaN(kursi)) {
                txtKursiPagese.SetFocus();
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiDuhetNumer"));
                txtKursiPagese.SetText("1");
            }
        }
        else {
            txtKursiPagese.SetText("1");
            txtKursiPagese.SetEnabled(false);
        }
    }
    vendosKuponTatimor();
    LlogaritResto();
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, data_DateEdit.GetDate());
}

//function vendosNrAutomatikNrSerial(colAtrTrupi, colKontrollet) {
//    myNrAuto.vendosNrAutomatikNrSerial(colAtrTrupi, colKontrollet, data_DateEdit.GetDate());
//}

//function merrNrSerial() {
//    //var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
//    //var atributet = JSON.parse($('#hfAtributeNrAutom').val());
//    vendosNrAutomatikNrSerial(pageState.colAtrTrupi, pageState.colKontrollet);
//}

/*
Function: pastroFushatKokes

Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim
*/
function pastroFushatKokes() {//po
    serialShenuarNgaPerdoruesi = false;
    btnKlienti.SetSelectedIndex(-1);
    btnKlienti.SetValue(null);
    pageState.TaksaKF = new Object();
    var grida = $(pageState.gridaSelector);
    monklienti = "0";
    txtEmri.SetText('');
    $('#hfIdMag').val(0);
    txtAdresaFaturimit.SetText('');
    txtEmriKlienti.SetText('');
    ucEmerSkedari.ClearText('');
    if (Utils.getUrlVar('nrklienti') == undefined || (cmbModeli.GetText().substring(0, 5) != 'VFONE' && cmbModeli.GetText().substring(0, 8) != 'POROSIBA' && cmbModeli.GetText().substring(0, 8) != 'POROSIDD')) {
        txtKontakti.SetText('');
    }
    hfState.Set('Status', 1);
    cbKasa.SetChecked(false);
    hfSeriale.Clear(); hfSasiSeriale.Clear();
    cbKupon.SetChecked(false);
    dteAfatiKohor.SetText('');
    txtCash.SetText('0.00');
    txtAdresaDergimit.SetText('');
    btnMenyreTransporti.SetText('');
    txtNumer.SetText('');
    lblStatusAprovimi.SetText('');
    txtNumerSerial.SetText('');
    btnKushtDergimi.SetValue(null);
    txtNumerProjekti.SetText('');
    btnAgjenti.SetValue(null);
    btneArka.SetValue(null);
    txtPerqindjeAgjent.SetText('');
    //btneKlientfurnitorVartes.SetValue(null);
    btneKlientfurnitorVartes.ClearOptions();
    btneKlientfurnitorVartes.SetValue([]);
    cmbDegeAdministrative.SetValue(null);
    cmbGrup1.SetValue(null);
    cmbGrup2.SetValue(null);
    cmbGrup3.SetValue(null);
    cmbPikeShitjeFurnizimi.SetValue(null);
    txtPershkrimi.SetText('');    
    cmbMonedha.SetSelectedItem(cmbMonedha.FindItemByValue(hfState.Get("idMonedheNderm")));
    cmbMonedhaPagese.SetSelectedItem(cmbMonedhaPagese.FindItemByValue(hfState.Get("idMonedheNderm")));
    //ne kete moment vendoset monedha e ndermarrjes, kursi i se ciles eshte 1. Per kete arsye nuk ka perse ta vendosim si string bosh dhe as ta marrim me webservice
    txtKursi.SetText("1");
    txtKursiPagese.SetText("1");
    //cmbMonedha.SetSelectedIndex(0);
    //cmbMonedhaPagese.SetSelectedIndex(0);
    //txtKursi.SetText('');
    //txtKursiPagese.SetText('');
    txtTotaliPaTVSH1.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesZbritja));
    txtTotaliPaTVSH2.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesZbritja));
    txtTotaliMeZbritjePaTVSH1.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesZbritja));
    txtTotaliMeZbritjePaTVSH2.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesZbritja));
    txtTotalLitra.SetText('0');
    txtDetyrimi.SetText('');
    hfState.Set("detyrimiMeparshem", 0);
    btnMaturimi.SetValue(null);
    btnKushtPagese.SetValue(null);
    txtPerqindje.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesZbritja));
    txtVlefte.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesZbritja));
    txtTotal1.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesVlefta));
    txtTotalMeZbritje1.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesVlefta));
    txtTVSH1.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesVlefta));
    txtTotal2.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesVlefta));
    txtTotalMeZbritje2.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesVlefta));
    txtTVSH2.SetText('0'); //parseFloat('0').toFixed(shifraPasPresjesVlefta));
    txtKrediti.SetText('0.00');
    txtLimit.SetText('0.00');
    btnMagazina.SetValue(null);
    previousMag = btnMagazina.GetSelectedItem();
    txtPershkrimMagazine.SetText('');
    txtPershkrimDege.SetText('');
    txtPershkrimPike.SetText('');
    txtKilometra.SetText('');
    btneAutomjeti.SetValue(null);
    btneAutomjeti.SetSelectedIndex(-1);
    txtTarga.SetText('');
    btnAgjenti2.SetValue(null);
    txtPerqindjeAgjent2.SetText('');
    btnAgjenti3.SetValue(null);
    txtPerqindjeAgjent3.SetText('');
    txtMarresi.SetText('');
    btnTransportues.SetValue(null);
    cbShpenzimeJoTeZbritshme.SetChecked(false);
    ZbritjaKlientit = "";
    NivelCmimi = 0;
    kaVlereDefaultDegaAdmin = false;
    hfState.Set('VCVKMD', false);
    hfState.Set("LNZAP", false);
    hfState.Set("KGJAG", false);

    hfState.Set("MosModifikoTrup", false);
    lblKrijuesi.SetText($("input[id$='hfPerdoruesAktual']").val());
    var hf = document.getElementById("status1");
    if (hf.value != "pagese")
        hf.value = "false";
    //vendosDateDefault();
    $('#ASPxSplitter1_hl').empty();
    $('#ASPxSplitter1_tblKonfigurimi>tbody>tr:eq(0)>td:eq(0)').empty();
    pastroInfoArt();
    pastroInfoKf();
    pastroInfoLlog();
    cmbDogana.SetValue('False');
    cmbKarta.SetValue(null);
    txtPike.SetText('0');
    txtTotalPike.SetText('0');
    $('#hfTotalPikesh').val('0');
    $('#hfKarta').val("");
    $('#hfPolitike').val("");
    hfArkiva.Clear();
    $('#hfArkivaDokId').val("");
    txtTarga2.SetText("");
    txtShoferi.SetText("");
    btneCaktoNeHarte.SetValue(null);
    txtNipt.SetText("");
    txtQytetiK.SetText("");
    cmbKategoriSeriali.SetSelectedIndex(-1);
    txtShenime2.SetText("");
    cbKartaPaPagese.SetChecked(false);
    txtIdMarreveshje.SetText("");
    cmbLlojMarreveshje.SetSelectedIndex(-1);
    cmbStatusMarreveshje.SetSelectedIndex(0);
    txtKerkuarNga.SetText("");
    txtNrDokMagazine.SetText('');
    txtNIVF.SetText('');
    txtIIC.SetText('');
    txtEIC.SetText('');
    txtNivfKthim.SetText('');
    cbFiskalizo.SetChecked(false);
    cmbOperatori.SetValue(null);
    cmbTipiIVetefaturimit.SetValue(null);
    UcDocumentEinvoice.SetEnabled(false);
    upload.SetEnabled(false);
}
function DokumentEincoiceUpload(s, e) {
    if (s.GetChecked() == false) {
        UcDocumentEinvoice.SetEnabled(false);
        upload.SetEnabled(false);
    } else {
        UcDocumentEinvoice.SetEnabled(true);
        upload.SetEnabled(true);
    }
}
function ButtonClickKategoriSeriali() {
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni kategorine e serialit", 'LupaKategoriSeriali.aspx?vjenNga=Shto_Artikull', widthLupaKF, heightLupaKF);
}
function MenyrePageseChanged(s, e) {
    switch (s.GetValue()) {
        case 6:
            arka = false;
            break;
        case 9:
            btneArka.ClearItems();
            arka = true;
            break;
        default:
            if (arka == true)
                btneArka.ClearItems();
            arka = false;
            break;
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "merrBankaSipasLLojit"),
        data: JSON.stringify({ idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, idLlojiArka: arka })
    }).done(SucceededCallbackMbushArka);
}

function vendosDateDefault(shtimMod) {
    var dataDefault = Utils.ktheDateDefault(pageState.periudha);
    var dataSot = Utils.zeroOren(new Date());

    if (!pageState.kushte.RDDNR) {
        vendosDateDefaultNgaPeriudha(shtimMod, dataDefault, dataSot);
        if (shtimMod != "konvertim" && shtimMod != "konvertimblerje" && shtimMod != "klonim")
            return;
    }

    switch (shtimMod) {
        case "kthim":
        case "kthimVod":
        case "bli":
            dateRegjistrimi_DateEdit.SetDate(dataSot);
            cmbMuajRaportimi.SetValue(dataSot.getMonth() + 1);
            cmbVitRaportimi.SetText(dataSot.getFullYear());
            break;
        case "klonim":
            if (pageState.kushte["VF_VM"])
                vendosDateDefaultNgaPeriudha(shtimMod, dataDefault, dataSot);
            else
                dateRegjistrimi_DateEdit.SetDate(dataSot);
            break;
        case "shtim":
        case "rezervim":
        case "shtimraport":
        case "konvertim":
        case "konvertimblerje":
            if ((pageState.lloji == "konvertim" || pageState.lloji == "konvertimblerje") && datekonvertimi)
                break;
            vendosDateDefaultNgaPeriudha(shtimMod, dataDefault, dataSot);
            break;
        default:
            break;
    }
}

function vendosDateDefaultNgaPeriudha(shtimMod, dataDefault, dataSot){
    data_DateEdit.SetDate(dataDefault);
    cmbMuajRaportimi.SetValue(dataDefault.getMonth() + 1);
    cmbVitRaportimi.SetText(dataDefault.getFullYear());
    dteAfatiKohor.SetDate(dataDefault);
    dateRegjistrimi_DateEdit.SetDate(dataSot);
    if (dateMaturimi_DateEdit.GetVisible())
        dateMaturimi_DateEdit.SetDate(dataSot);
    else
        dateMaturimi_DateEdit.SetDate(data_DateEdit.GetDate());

    if (shtimMod == "klonim" && pageState.kushte["VF_VM"])
        return;

    DtFillimi_DateEdit.SetDate(dataDefault);
    DtMbarimi_DateEdit.SetDate(dataDefault);
    DtFature_DateEdit.SetDate(dataDefault);
    DtKerkese_DateEdit.SetDate(dataDefault);
}

/*
Function: isValidKoka

Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit
*/
function isValidKoka() {//po
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    if (!pageState.kastratiTollona.isValid(grida))
        return false;
    editorCmimi = jQuery("#txtCmimi" + idRresht)[0];
    editorKodi = jQuery("#txtKodi" + idRresht)[0];
    editorVlefte = jQuery("#txtVleftaTVSH" + idRresht)[0];
    editorSasia = jQuery("#txtSasia" + idRresht)[0];
    if (cmbNiveli.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNivelin"));
        return false;
    }
    if (data_DateEdit.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniDtDok"));
        return false;
    }
    if (txtNumer.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    if (txtKursi.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniKursin"));
        return false;
    }
    if (regjistrimKF != 1 && btnKlienti.GetValue() == null && btnKlienti.GetVisible() == true) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniKf"));
        return false;
    }
    var totale = calcTotale(grida);
    if (Math.abs(parseFloat(txtVlefte.GetText())) > Math.abs(totale.totali).toFixed(10)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZbritjaJoMeMadheTotal"));
        return false;
    }
    if (cbKupon.GetChecked() && cbKasa.GetChecked() && txtNumerSerial.GetText() === "" && cmbNiveli.GetText() === "Fature shitje") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosniNjeSerialFaturePerKuponatMeFatureTatimore"));
        return false;
    }
    if (pageState.kushte.DDFMK && (data_DateEdit.GetDate() < dtfillimikonvertimi || data_DateEdit.GetDate() > dtmbarimikonvertimi)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDataDokJashtePeriudhes"));
        return false;
    }
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
    if ($('#hfRuajDraft').val() != "Draft") {
        if (editorCmimi != undefined && cmimzero == 2 && grida.getTekstQelize('txtCmimi', idRresht) == 0.00 && grida.getTekstQelize('txtKodi', idRresht) != '' && vleraLlojit == "Artikull") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetCmimZeroNeGride"));
            editorCmimi.focus();
            return false;
        }

        if (editorVlefte != undefined && cmimzero == 2 && grida.getTekstQelize('txtVleftaTVSH', idRresht) == 0.00 && grida.getTekstQelize('txtKodi', idRresht) != '' && vleraLlojit == "Llogari") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetCmimZeroNeGride"));
            editorVlefte.focus();
            return false;
        }
        if (editorSasia != undefined && grida.getTekstQelize('txtKodi', idRresht) != '' && vleraLlojit == "Artikull") {
            var sasia = grida.getTekstQelize('txtSasia', idRresht);
            if (sasia == 0) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
                editorSasia.focus();
                return false;
            }
            if (sasia === "" || isNaN(sasia)) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumer"));
                editorSasia.focus();
                return false;
            }
        }
        editorLloji = jQuery("#cmbLloji" + idRresht)[0];
        if (btnKlienti.GetValue() != null && editorCmimi != undefined && editorLloji != undefined && editorKodi.value != '' && editorLloji[editorLloji.selectedIndex].value == "1") {
            if (!kontrolloCmiminArtSipasKushtit(editorCmimi.value)) {
                editorCmimi.focus();
                return false;
            }
        }
    }
    if (monklienti != "0" && (monklienti != cmbMonedha.GetValue())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVeprimeMonedheNdryshmekf"));
        cmbMonedha.SetValue(monklienti);
        vendosTotaleMonedheFature();
        return false;
    }
    return true;
}

var fillimprint;

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro> dhe <pastroFushatKokes>.
*/
function EndRequestHandler(sender, args) {//po
    formatoFushaDevi();
    var hf = document.getElementById("status1");
    var pageStateLloji = hf.value;
    if (pageStateLloji == "konvertuar") {
        popKonvertuar.Show();
        return;
    }
    hapPopMesazhQK();

    if (pageStateLloji == "true" || pageStateLloji == "pagese" || pageStateLloji == "kthehu") {
        if ($('#hfKasaNew').val() != "") //shiko nese eshte kase e re
            dergoNeKase($('#hfKasaNew'));
        else //nese jo shiko per kase te vjeter
            if ($('#hfKasa').val() != "")
                shkruajFile($('#hfKasa').val());
        if (pageStateLloji == "kthehu" && ($('#hfUrl').val() == "" && $('#hfUrlmag').val() == "" && $('#hfUrlbanka').val() == "" && $('#hfUrlVDK').val() == ''))
            myFaqeCelje.kontrolloTeDrejta($('#hfId').val());
        else
            if (pageStateLloji == "pagese" && ($('#hfUrl').val() == "" && $('#hfUrlmag').val() == "" && $('#hfUrlbanka').val() == "" && $('#hfUrlVDK').val() == '')) {
                //fillimprint = new Date();
                if (cbPrinto.GetChecked())
                    CheckWindowState();
                else if ($('#hfqkmesazhi').val() == 'jo')
                    myFaqeCelje.kontrolloTeDrejta($('#hfId').val());
                else myFaqeCelje.kontrolloTeDrejta($('#hfId').val(), true);
            }
            else {
                myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + '&shtim_modifikim=shtim', true);
            }
    }
    else
        click = false; //kur nuk ruhet per ndonje arsye i japim mundesi te riruaj
    var idDok = (Utils.getUrlVar("id") === undefined || Utils.getUrlVar("id") === "undefined" || typeof (Utils.getUrlVar("id")) === "undefined") ? 0 : Utils.getUrlVar("id");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "MerrMenuPerPerdoruesSipasSkemes"),
        data: JSON.stringify({
            idskema: $('#hfSkema').val(), idperdoruesi: $('#hfPerdoruesi').val(), isNotModifikim: pageState.lloji == 'modifikim' ? false : true,
            status: lblStatusAprovimi.GetText(), idkokashitje: idDok, kodkonf: cmbModeli.GetText(), idlloji: 1
        })
    }).done(SuccedcallbackSkemaMenu);

   // if (pageState.lloji == "shtim" && ($('#hfRuajteobjkti').val() != ""))

    webhook();
}
function webhook() {
    var event;
    for (var i = 0; i < pageState.webhook.length; i++) {
        if (pageState.webhook[i].Kategoria == 1)
            kategoria = "shitje"
        else if (pageState.webhook[i].Kategoria == 0)
            kategoria = "blerje"
        else kategoria = "";
        var a = pageState.webhook[i].Eventi
        if ((pageState.webhook[i].Eventi == 1 || pageState.webhook[i].Eventi == 2 || pageState.webhook[i].Eventi == 0) && kategoria == pageState.veprimi && pageState.webhook[i].Aktive == true) {

            $.ajax({
                type: "POST",
                url: pageState.webhook[i].Urlpritese,

                beforeSend: function (request) {
                    request.setRequestHeader("referer", document.referrer.split('?')[0])
                },

                data: $('#hfObjektRuajtur').val(),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).done(function (response) {
                if (response != null) {
                    console.log("success");
                    console.log(response);

                } else {
                    console.log("Something went wrong");
                }

            }).fail(function (response) {
                console.log(response.responseText);
            });
            
        }

    }
}
function CheckWindowState() {
    setTimeout(function () {
        $('body').on('mousemove click keypress', function () {
            myFaqeCelje.kontrolloTeDrejta($('#hfId').val());
            $('body').off('mousemove click keypress');
        });
    }, 1200);
}
function hapPopMesazhQK() {

    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        $('#hfqkmesazhi').val('jo');
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndarjeQendraKosto"), cancelClick: JopopupClick, okClick: hapPopUp });
        return;
    }
    else if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), pageState.widthLupaQendraKosto, 600);
        $('#hfqkmesazhi').val('jo');
        $('#hfUrl').val('');
    }
    else if ($('#hfqkmesazhimag').val() == 'shfaqmesazh') {
        $('#hfqkmesazhimag').val('jo');
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndQendraKostoMag"), cancelClick: JopopupClick, okClick: hapPopUp });
        return;
    }
    else if ($('#hfqkmesazhimag').val() == 'shfaqlupe') {
        if ($('#hfUrlmag').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlmag').val(), pageState.widthLupaQendraKosto, 600);
            $('#hfUrlmag').val('');
            $('#hfqkmesazhimag').val('');
        }
    }
    else if ($('#hfqkmesazhibanka').val() == 'shfaqmesazh') {
        $('#hfqkmesazhibanka').val('jo');
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiTeBeniShperndarjenNeQendraKosto"), cancelClick: JopopupClick, okClick: hapPopUp });
        return;
    }
    else if ($('#hfqkmesazhibanka').val() == 'shfaqlupe') {
        if ($('#hfUrlbanka').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlbanka').val(), pageState.widthLupaQendraKosto, 600);
            $('#hfUrlbanka').val('');
            $('#hfqkmesazhibanka').val('');
        }
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {
        $('#hfqkmesazhiVDK').val('jo');
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"), cancelClick: JopopupClick, okClick: hapPopUp });
        return;
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        if ($('#hfUrlVDK').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], pageState.widthLupaQendraKosto, 600);
            $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));
            if ($('#hfUrlVDK').val() == '')
                $('#hfqkmesazhiVDK').val('jo');
        }
    }
}


function JopopupClick(s, e) {
    if ($('#hfUrl').val() != '')
        $('#hfUrl').val('');
    else if ($('#hfUrlmag').val() != '')
        $('#hfUrlmag').val('');
    else if ($('#hfUrlbanka').val() != '')
        $('#hfUrlbanka').val('');
    else if ($('#hfUrlVDK').val() != '')
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));
    hapPopMesazhQK();
    var hf = document.getElementById("status1");
    var pageStateLloji = hf.value;
    if (pageStateLloji == "kthehu" && ($('#hfUrl').val() == "" && $('#hfUrlmag').val() == "" && $('#hfUrlbanka').val() == "" && $('#hfUrlVDK').val() == ""))
        myFaqeCelje.kontrolloTeDrejta($('#hfId').val());
    if (pageStateLloji == "pagese" && ($('#hfUrl').val() == "" && $('#hfUrlmag').val() == "" && $('#hfUrlbanka').val() == "" && $('#hfUrlVDK').val() == "")) {
        myFaqeCelje.kontrolloTeDrejta($('#hfId').val());
    }
}

function closePopup(s, e) {
    var popUpUrl = popupUniversal.GetContentUrl();
    if (popUpUrl.search('LupaValidim.aspx') != -1) {
        if ($('#hfVodOne').val() == "false" && cmbModeli.GetText().substring(0, 5) != "USHma") {

            cmbModeli.SetSelectedIndex(0);
            ndryshoKonfigurimin(true);
        }
    }
    popupUniversal.SetContentUrl('');
    hapPopMesazhQK();
    var hf = document.getElementById("status1");
    var pageStateLloji = hf.value;
    if (pageStateLloji == "kthehu" && ($('#hfUrl').val() == "" && $('#hfUrlmag').val() == "" && $('#hfUrlbanka').val() == "" && $('#hfUrlVDK').val() == ""))
        myFaqeCelje.kontrolloTeDrejta($('#hfId').val());
    if (pageStateLloji == "pagese" && ($('#hfUrl').val() == "" && $('#hfUrlmag').val() == "" && $('#hfUrlbanka').val() == "" && $('#hfUrlVDK').val() == ""))
        myFaqeCelje.kontrolloTeDrejta($('#hfId').val());
    HapLupeValidimiPasLupesPaPrintuar();
}

function hapPopUp(s, e) {
    if ($('#hfUrl').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), pageState.widthLupaQendraKosto, 600);
        $('#hfUrl').val('');
    }
    else if ($('#hfUrlmag').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlmag').val(), pageState.widthLupaQendraKosto, 600);
        $('#hfUrlmag').val('');
    }
    else if ($('#hfUrlbanka').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlbanka').val(), pageState.widthLupaQendraKosto, 600);
        $('#hfUrlbanka').val('');
    }
    else if ($('#hfUrlVDK').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], pageState.widthLupaQendraKosto, 600);
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", "")); if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');
    }
}
function kasaError(e, x, settings, exception) {

    var message = undefined;
    if (e.status) {
        if (e.state == 404) {//gabim url
            console.error("Not Found!/");
        }
        else {
            message = e.responseJSON != undefined ? e.responseJSON.Message : e.responseText;
        }
    }
    else if (exception == 'parsererror') {
        message = "Error.\nNuk behet ne rregull parsimi (JSON)";
    } else if (exception == 'timeout') {
        message = "Kerkesa ka tejkaluar kohen qe i eshte lejuar per te pritur! ";
    } else if (exception == 'abort') {
        message = "Kerkesa eshte nderprere nga serveri!";
    } else {
        message = "Gabim i panjohur! \n";
    }
    if (message) console.error(message);
}
function dergoNeKase(hfKasa) {
    //var myKasaJson = JSON.parse(hfKasa.val());
    var urlKase = cmbKonfigurimKase.FindItemByText(cmbKonfigurimKase.GetText()).GetColumnText('Url');
    $.ajax({
        crossDomain: true,
        data: hfKasa.val(),
        url: urlKase
    }).done(function (res) {
        if (res)
            myMesazh.ShtoMesazhSuksesi(hfState.Get("msgFaturaDerguaKaseFiskaleSukses"));
    }).fail(function (res) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDownloadPrograminEKasesTeMenu"));
        if (console && console.error) {
            console.error("Server Kasa responded:", res);
        }
    });

    // Denisi
    hfKasa.val(""); //pastrojme hf-ne
}
function shkruajFile(hfKasaVal) {//po

    var file = hfKasaVal.split('&&');
    if (typeof ActiveXObject != "undefined") {
        var fso = new ActiveXObject("Scripting.FileSystemObject");
        var index = file[0].lastIndexOf('\\');
        var direktoria = file[0].substring(0, index);
        if (fso.FolderExists(direktoria)) {
            if (fso.FileExists(file[0])) {
                s = fso.OpenTextFile(file[0], 2, false);
            }
            else {
                var s = fso.CreateTextFile(file[0], false);
            }
            var text = file[1].split('||');
            for (var i = 0; i < text.length; i++)
                s.WriteLine(text[i]);
            s.Close();
            if (file.length > 2) {
                fso.CopyFile(file[0], file[2]);
                if (file[3] == "False") {
                    fso.DeleteFile(file[0]);
                }
            }
            myMesazh.ShtoMesazhSuksesi(hfState.Get("msgFaturaDerguaKaseFiskaleSukses"));
        }
        else
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDirektoriaNukEkziston"));
    }
    else {
        var fileKase;
        var urlKase = cmbKonfigurimKase.FindItemByText(cmbKonfigurimKase.GetText()).GetColumnText('Url');

        if (file.length > 2 && file.length < 5)
            fileKase = { pathUnike: file[0], fileStream: file[1], pathShkurter: file[2], mosFshiOrigjine: file[3] };
        else {
            if (file.length == 6)
                fileKase = { shitje: file[0], port: file[1], boundrate: file[2], meTvsh: file[3], perqindjeZbritje: file[4], paguar: file[5] };
            if (file.length > 6)
                fileKase = { shitje: file[0], port: file[1], boundrate: file[2], meTvsh: file[3], perqindjeZbritje: file[4], meShifraDhjetore: file[5], printoSerial: file[6] };
            if (file.length == 2)
                fileKase = { pathUnike: file[0], fileStream: file[1], pathShkurter: "", mosFshiOrigjine: "" };
        }




        $.ajax({
            crossDomain: true,
            dataType: "json",
            url: urlKase,
            type: 'POST',
            contentType: 'text/plain',
            data: JSON.stringify(fileKase),
            cache: false,
            success: function (res) {
                if (res)
                    myMesazh.ShtoMesazhSuksesi(hfState.Get("msgFaturaDerguaKaseFiskaleSukses"));

            },
            error: function (res) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgDownloadPrograminEKasesTeMenu"));
                if (console && console.error) {
                    console.error("Server Kasa responded:", res);
                }
            }
        });

        //$.ajax({
        //    crossDomain: true,
        //    url: urlKase, type: 'POST', contentType: 'application/json', data: JSON.stringify(fileKase),
        //    error: kasaError
        //}).done(function (resp) {
        //    if (console && console.log) {
        //        console.log("Server Kasa responded:", resp);
        //        if (resp)
        //            myMesazh.ShtoMesazhSuksesi(hfState.Get("msgFaturaDerguaKaseFiskaleSukses"));
        //    }
        //}).fail(function (err) {
        //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgDownloadPrograminEKasesTeMenu"));
        //    if (console && console.log) {
        //        console.log("Server Kasa responded:", err);
        //    }
        //});

    }
}


var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e); }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + '&shtim_modifikim=shtim', true);
    }
}

function menu_click(s, e) {
    if (!myJQGrid.checkIsPageReadyToSave(e.item.name)) {
        Utils.shfaqLoadingGif();
        Utils.shtoFunksionNeRadheMeParametra(menuClick, [s, e, true], pageState.idGjuha, this);
        e.processOnServer = false;
        return;
    }
    menuClick(s, e, false);
}

function menuClick(s, e, doPostback) {//po
    var id = Utils.getNumberOrDefaultFromUrl('id');
    editorMenu = e;
    e.processOnServer = true;
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
        return;
    }
    click = true;
    if (e.item.name == 'Ruaj' || e.item.name == 'Draft' || e.item.name == 'Pezullo' || e.item.name == "Aprovo" || e.item.name == "Refuzo" || e.item.name == "Modifiko" || e.item.name == "Delego")
        $('#hfRuajDraft')[0].value = e.item.name;
    if (e.item.name == 'Ruaj' || e.item.name == 'Draft' || e.item.name == 'Pezullo' || e.item.name == "PrintPreview" || e.item.name == "Aprovo" || e.item.name == "Refuzo" || e.item.name == "Modifiko" || e.item.name == "Delego") {
        myMesazh.vendosServer();
        if (!myFaqeCelje.validim(s, e)) {
            Utils.hiqLoadingGif();
            click = false;
            return;
        }
        if (e.item.name == "PrintPreview") {
            Utils.hiqLoadingGif();
            click = false;
            Utils.doPostback(s, e, doPostback);
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            isSubjektAktiv(e);            
            if ((e.item.name == 'Ruaj' || e.item.name == 'Draft') && $('#hfShtimModifikim').val() == 'shtim') {
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
                ASPxMenu1.GetItemByName('Draft').SetEnabled(false);
            }
            RuajClick(1, s, e);
            Utils.doPostback(s == 'tastiera' ? { name: "ASPxMenu1" } : s, e, s == 'tastiera' ? true : doPostback);
            return;
        }
        else {
            e.processOnServer = false;
            click = false;
            Utils.hiqLoadingGif();
        }
        return;
    }
    else if (e.item.name == 'Klono') {
        var katDokAndKomponentObj = [{ idKatDok: 1, komponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje' }, { idKatDok: 2, komponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje' }, { idKatDok: 6, komponente: '' }];
        popUpKlonimiFunctions.showPopUp(cmbNiveli.GetValue(), cmbModeli.GetValue(), Utils.getUrlVar('id'), katDokAndKomponentObj);
        click = false;
        e.processOnServer = false;
        return;
        //Utils.redirectKlono(id, pageState.veprimi, "Shto_RegjistrimDokumentash.aspx", '');
    }

    if (e.item.name === 'KthimB' || e.item.name === 'KthimSh') {

        $.ajax({
            pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "merrUrlKthim"),
            data: JSON.stringify({ id: id, idNdermarrje: pageState.idNdermarrje, pageId: window['CurrentPageId'] })
        }).done(SuccededCallbackKthim);
        e.processOnServer = false;
        click = false;
    }
    if (e.item.name == "Paguaj") {

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrUrlPaguaj"),
            data: JSON.stringify({ id: id, veprimi: pageState.veprimi, vjenNga: 'Shto_RegjistrimDokumentash', idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idVitNdermarrje: pageState.idViti })
        }).done(SuccededCallbackPaguaj);

        e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == "Komento") {
        if (Utils.getUrlVar('vjenNga') == 'aprovim' || Utils.getUrlVar('vjenNga') == 'kerkese')
            myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + Utils.getUrlVar('idetapa') + '&nrprocesi=' + Utils.getUrlVar('nrprocesi') + '&veprimi=Gjitha&idkategoria=1', 600, 500);
        else {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "gjejEtapeDokumenti"),
                data: JSON.stringify({ idperdoruesi: $('#hfPerdoruesi').val(), idDok: id, lloji: "shitje" })
            }).done(SucededCallbackLupaKomente);
        }
        e.processOnServer = false;
        click = false;
    }
    if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi, null, true);
        e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == 'Shto') {
        if (pageState.EshtedokBije) {
            window.location = 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + '&shtim_modifikim=shtim';
        } else {
            myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + '&shtim_modifikim=shtim', true);
        }
        return;
    }
    if (e.item.name == 'Serialet') {
        HapLupeSerialeshUnike();

        e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == 'Konverto') {
        ButtonClickKonverto();
        click = false;
        e.processOnServer = false;
        return;
    }
    if (e.item.name == 'QendraKosto') {
        e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == 'Fshi') {
        var teDrejtaNiveli = merrTeDrejtaNiveli(cmbNiveli.GetValue());
        if (!teDrejtaNiveli.DFsh) {
            myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta fshirje per nenkategorine: " + cmbNiveli.GetText());
            Utils.hiqLoadingGif();
            e.processOnServer = false;
            click = false;
            return;
        }
        identifikuesPyetje = "Fshi";
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeFshireDok"));
            Utils.hiqLoadingGif();
            e.processOnServer = false;
            click = false;
            return;
        }
        if (mesazhriprintimi && pageState.lloji == 'modifikim' && hfState.Get('printuarKase'))
            myMesazh.ShtoPyetje(hfState.Get("regjisDokMsgFaturaEshtePrintNeKase"));
        else if (pageState.lloji == 'modifikim' && hfState.Get('mesazhKonvertuar') != "")
            myMesazh.ShtoPyetje(hfState.Get("mesazhKonvertuar"));
        else
            myMesazh.ShtoPyetje(hfState.Get("labelAdministrimiMsgJeniSigurt"));
        Utils.hiqLoadingGif();
        e.processOnServer = false;
        click = false;
    }
    if (e.item.name == 'Anullo') {
        if (Utils.getUrlVar('vjenNga') == 'aprovim')
            myFaqeCelje.kontrolloTeDrejta('ListeAprovimi.aspx?status=aprovim');
        else if (Utils.getUrlVar('vjenNga') == 'kerkese')
            myFaqeCelje.kontrolloTeDrejta('ListeAprovimi.aspx?status=kerkese');
        else myFaqeCelje.kontrolloTeDrejta('RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi); e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == 'Arkiva') {
        ButtonClickArkiva($('#hfArkivaDokId').val());
        e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == 'Validim') {
        ButtonClickValidim();
        e.processOnServer = false;
        click = false;
        return;
    }
    Utils.doPostback(s, e, doPostback);
}

function HapLupeSerialeshUnike(idSeti) {
    var hyrje_dalje = (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? "dalje" : "hyrje";
    var idMag = btnMagazina.GetValue();
    myButtonClickLupa.ButtonClickLupaSerialeUnike(popupSerialet, 'Serialet', {
        "guidString": pageState.guidString,
        "hyrje_dalje": hyrje_dalje,
        "lidhur": $("input[id$='hfLidhur']").val().toLowerCase() === 'true',
        "MerrArtikujSet": hyrje_dalje == "dalje",
        "MenyreKontrollSeriali": pageState.kushte["MKS"],
        "MNSA": pageState.kushte["MNSA"],
        "IdMag": idMag != undefined ? idMag : 0,
        "Shitje": true,
        "KGJAPMR": pageState.kushte["KGJAPMR"],
        "Kthim": hfState.Get("eshteDokKthimi"),
        "Id": pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0,
        "IdSeti": idSeti ? idSeti : 0
    });
}

function SucededCallbackLupaKomente(result) {
    myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + result.IdEtapa + '&nrprocesi=' + result.NrProcesi + '&veprimi=Gjitha&idkategoria=1', 600, 500);
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + "&ruaj=po");
}

function SuccededCallbackPaguaj(result) {
    if (result === "ska te drejta")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniTeDrejtaNeKeteAmbjent"));
    else if (result === "draft")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeLikuidoniNjeFatureDraft"));
    else if (result === "joFature")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKonvertohenDokumentatFatOferteKerkese"));
    else if (result === "likuiduar")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokILikuiduar"));
    else myFaqeCelje.kontrolloTeDrejta(result);
}

function SuccededCallbackKthim(result) {
    if (result[1] === "ska te drejta")
        alert(hfState.Get("msgNukKeniTeDrejtaNeKeteAmbjent"));
    else if (result[1] === "draft")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJoKthimFatureDraft"));
    else if (result[1] === "joFature")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJoKthimDokFatOferteKerkese"));
    else if (result[1] === "likuiduar")
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + '&id=' + Utils.getUrlVar('id') + '&shtim_modifikim=kthim' + '&pageCacheId=' + window['CurrentPageId'] + '&likuiduar=true');
    else if (result[1] === "negative")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJoKthimFatureVlereNegative"));
    else
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + '&id=' + Utils.getUrlVar('id') + '&shtim_modifikim=kthim' + '&pageCacheId=' + window['CurrentPageId']);
}
function isSubjektAktiv(e) {
    if (!Utils.IsNullOrEmpty(pageState.Klient))
        if (pageState.kushte.IPSPRD && JSON.parse(pageState.Klient) != undefined && JSON.parse(pageState.Klient).kf.NiptiKF != "" && hfState.Get('URL_SUBJEKTEPASIV') != null && hfState.Get('URL_SUBJEKTEPASIV') != "") {
            $.ajax({
                pritPergjigje: true,
                type: "GET",
                async: false,
                timeout: 3000,
                url: hfState.Get('URL_SUBJEKTEPASIV') + JSON.parse(pageState.Klient).kf.NiptiKF,
                success: function (result) {
                    if (result) {
                        if (console)
                            console.log("URL_SUBJEKTEPASIV: PASSIV - NIPTI: " + JSON.parse(pageState.Klient).kf.NiptiKF);
                        myMesazh.ShtoPyetje(hfState.Get("msgSubjektiMeNiptEshtePasivSipasTativemeDoniTeVazhdoni").replace('@NIPTI', JSON.parse(pageState.Klient).kf.NiptiKF), true);
                        identifikuesPyetje = "SubjektPasiv";
                        e.processOnServer = false;
                        click = false;
                        Utils.hiqLoadingGif();
                    }
                    else {
                        if (console)
                            console.log("URL_SUBJEKTEPASIV: AKTIV - NIPTI: " + JSON.parse(pageState.Klient).kf.NiptiKF);
                    }
                },
                error: function () {
                    if (console)
                        console.log("URL_SUBJEKTEPASIV: DESHTOI KOMUNIKIMI - NIPTI: " + JSON.parse(pageState.Klient).kf.NiptiKF);
                }
            });
        }
}

/*
Function: RuajClick

validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/
var click = false;
var identifikuesPyetje;
function RuajClick(status, s, e) {//po
    editorMenu = e;
    $('#hfKonverto').val(JSON.stringify(resultkonvertime));

    if (isValidKoka()) {
        if (hfState.Get("certificateExpire") && cbFiskalizo.GetChecked()) {
            myMesazh.ShtoMesazhGabimi("Certifikata juaj ka skaduar");
            e.processOnServer = false;
            click = false;
        }
        kontrolloPerqindjeAgjentiPaAgjent(btnAgjenti, txtPerqindjeAgjent);
        kontrolloPerqindjeAgjentiPaAgjent(btnAgjenti2, txtPerqindjeAgjent2);
        kontrolloPerqindjeAgjentiPaAgjent(btnAgjenti3, txtPerqindjeAgjent3);

        merrTeDhena(s, e);
        if (click == false) {
            e.processOnServer = false;
            return;
        }
        if (limitKF == 1 && $('#hfRuajDraft').val() != "Draft") {
            if (cmbMenyrePagese.GetSelectedIndex() !== 2) {
                if ((pageState.lloji == 'shtim' || pageState.lloji == 'konvertim' || pageState.lloji == 'shtimraport' || pageState.lloji == 'klonim' || pageState.lloji == 'kthim' || pageState.lloji == 'modifikim') && parseFloat(txtTotal1.GetText()) + parseFloat(hfState.Get("detyrimiMeparshem")) > parseFloat(txtLimit.GetText()) && parseFloat(txtLimit.GetText()) != 0.00) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgTotalFatureKaluarLimitBllokues"));
                    e.processOnServer = false;
                    click = false;
                }
                else
                    if ((pageState.lloji == 'shtim' || pageState.lloji == 'konvertim' || pageState.lloji == 'shtimraport' || pageState.lloji == 'klonim' || pageState.lloji == 'kthim' || pageState.lloji == 'modifikim') && parseFloat(txtTotal1.GetText()) + parseFloat(hfState.Get("detyrimiMeparshem")) > parseFloat(txtKrediti.GetText()) && parseFloat(txtKrediti.GetText()) != 0.00) {
                        myMesazh.vendosClient();
                        myMesazh.ShtoPyetje(hfState.Get("msgTotalfatureKaluarLimitkf"), false);
                        identifikuesPyetje = "krediti";
                        e.processOnServer = false;
                        click = false;
                    }
                    else {
                        if (trupiBosh == true) {
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
                            e.processOnServer = false;
                            click = false;
                        }
                        if (cbKasa.GetChecked() && mesazhriprintimi && pageState.lloji == 'modifikim' && hfState.Get('printuarKase')) {
                            myMesazh.ShtoPyetje(hfState.Get("msgFaturaEkzistueseEshtePrintuarPrintoKuponTeRi"), true);

                            identifikuesPyetje = "printKupon";
                            //popKupon.Show();
                            e.processOnServer = false;
                            click = false;
                            Utils.hiqLoadingGif();
                        }
                    }
            }
        }
        else {
            merrTeDhena(s, e);
            if (trupiBosh == true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
                e.processOnServer = false; click = false;
            }
            if (cbKasa.GetChecked() && mesazhriprintimi && pageState.lloji == 'modifikim' && hfState.Get('printuarKase')) {
                myMesazh.ShtoPyetje("Fatura ekzistuese eshte printuar ne kase. Deshironi te printoni kupon te ri?", true);
                identifikuesPyetje = "printKupon";
                //popKupon.Show();
                e.processOnServer = false;
                click = false;
                Utils.hiqLoadingGif();
            }
        }
        if ($('#hfShtimModifikim').val() == 'shtim' && ((Utils.getUrlVar('shitje_blerje') == "shitjediscount" && ((cmbModeli.GetText().length > 5 && cmbModeli.GetText().substring(0, 5) == "USHDD") || (cmbModeli.GetText().length > 8 && cmbModeli.GetText().substring(0, 8) == "POROSIDD"))) ||
            (Utils.getUrlVar('shitje_blerje') == "bazaar" && ((cmbModeli.GetText().length > 6 && cmbModeli.GetText().substring(0, 6) == "BAZAAR") || (cmbModeli.GetText().length > 12 && cmbModeli.GetText().substring(0, 12) == "POROSIBAZAAR"))))) {
            e.processOnServer = false;
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "kontrollobundle"),
                data: JSON.stringify({ idartikujsh: idartikujsh })
            }).done(SucceededCallbackBundle);
        }
    }
    else {
        e.processOnServer = false;
        click = false;
        Utils.hiqLoadingGif();
    }
}
function SucceededCallbackBundle(result) {
    if (result == "" || result == null)
        btn.DoClick();
    else {
        $('#hfKodBundle').val(result);
        ButtonClickValidim();
        Utils.hiqLoadingGif();
    }
}
$(window).unload(function () {
    localStorage.degeadmshitje = '';
    localStorage.magazinashitje = '';
    localStorage.dateshitje = '';
});
/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro> dhe <pastroFushatKokes>.
*/
function PastroClick() {//po
    setHfShtimModifikim();
    pageState.aprovim = false;
    $("input[id$='hfLidhur']").val(false);
    $("input[id$='hfAutorizimi']").val(true);
    $("input[id$='hfPiket']").val('');
    $("input[id$='hfKodVFOne']").val('');
    $("input[id$='hfVodOne']").val('');
    hfState.Set("konvNgaUshNeFsh", false);
    hfState.Set("eshteDokKthimi", false);
    if (pageState.kushte.RVF) {
        localStorage.setItem("degeadmshitje", cmbDegeAdministrative.GetText());
        localStorage.setItem("magazinashitje", btnMagazina.GetText());
        localStorage.setItem("dateshitje", JSON.stringify(data_DateEdit.GetDate()));
    }

    pastro();
    ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('Paguaj').SetVisible(false);
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Konverto').SetVisible(false);
    ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    if (ASPxMenu1.GetItemByName('KthimB') != null)
        ASPxMenu1.GetItemByName('KthimB').SetVisible(false);
    if (ASPxMenu1.GetItemByName('KthimSh') != null)
        ASPxMenu1.GetItemByName('KthimSh').SetVisible(false);
    var hf = $("#hfShtimModifikim");
    myMenu.menuSipasTeDrejtaRegjistrimSipasNivelit(hf, hfTeDrejta);
    MenuTeDrejtaKonvertimi();
    ndryshoKonfigurimin(true);
    $("#ASPxSplitter1_hl").empty();
    click = false; // jap mundesi per te klikuar ruaj;
}

function setHfShtimModifikim() {
    var hf = $("#hfShtimModifikim");
    pageState.lloji = 'shtim';
    hf.val(pageState.lloji);
}

function MenuTeDrejtaKonvertimi() {
    var pageStateLloji = pageState.lloji;
    if (cmbNiveli.GetText() === "Fature shitje") {
        var vetemKonvertimFSH = hfTeDrejta.Get("VetemKonvertimFSH");
        if (vetemKonvertimFSH && (pageStateLloji == 'shtim' || pageStateLloji == 'klonim' || pageStateLloji == 'kthim')) {
            ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
            ASPxMenu1.GetItemByName('Draft').SetEnabled(false);
        }
        else {
            if ((pageStateLloji == 'modifikim' && hfTeDrejta.Get("Modifikim")) || ((pageStateLloji == 'shtim' || pageStateLloji == 'klonim' || pageStateLloji == 'shtimraport' || pageStateLloji == 'kthim') && hfTeDrejta.Get("Shtim")))
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
            if ((pageStateLloji == 'modifikim' && hfTeDrejta.Get("ModifikimDraft")) || ((pageStateLloji == 'shtim' || pageStateLloji == 'klonim' || pageStateLloji == 'shtimraport' || pageStateLloji == 'kthim') && hfTeDrejta.Get("ShtimDraft")))
                ASPxMenu1.GetItemByName('Draft').SetEnabled(true);
        }
    }
    else {
        if ((pageStateLloji == 'modifikim' && hfTeDrejta.Get("Modifikim")) || ((pageStateLloji == 'shtim' || pageStateLloji == 'klonim' || pageStateLloji == 'shtimraport' || pageStateLloji == 'kthim') && hfTeDrejta.Get("Shtim")))
            ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
        if ((pageStateLloji == 'modifikim' && hfTeDrejta.Get("ModifikimDraft")) || ((pageStateLloji == 'shtim' || pageStateLloji == 'klonim' || pageStateLloji == 'shtimraport' || pageStateLloji == 'kthim') && hfTeDrejta.Get("ShtimDraft")))
            ASPxMenu1.GetItemByName('Draft').SetEnabled(true);
    }
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

Hap lupen e periudhave.
*/
function ShfaqPeriudhen() {
    popupUniversal.SetHeaderText('Zgjidh periudhen');
    popupUniversal.SetContentUrl('LupaPeriudhaKontabel.aspx');
    popupUniversal.SetSize(widthLupaPeriudha, heightLupaPeriudha);
    popupUniversal.Show();
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
        data_DateEdit.SetText(periudha[0]);
    else {
        if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
            data_DateEdit.SetText(periudha[0]);
        else
            data_DateEdit.SetText(dataDok);
    }
}

function SelectedIndexChangedDogana() {
    var grida = $(pageState.gridaSelector);
    var rreshtaTeGridesId = grida.jqGrid('getDataIDs');
    if (cmbDogana.GetText() === 'Po')
        dogana = 1;
    else
        dogana = 2;
    for (var i = 0; i < rreshtaTeGridesId.length; i++) {
        if (grida.getTekstQelize('txtKodi', rreshtaTeGridesId[i])) {
            grida.setTekstQelize('cbTVSH', rreshtaTeGridesId[i], '', null, undefined, myelemComboTVSHSup);
            changedTVSHReshti(rreshtaTeGridesId[i]);
        }
    }
}
function ChangeTVSHSipasKlientit() {
    if (tvshkont == 4) {
        var grida = $(pageState.gridaSelector);
        var rreshtaTeGridesId = grida.jqGrid('getDataIDs');
        var arrartikull = new Array();
        var arrlloji = new Array();
        for (var i = 0; i < rreshtaTeGridesId.length; i++) {
            if (grida.getTekstQelize('txtKodi', rreshtaTeGridesId[i])) {
                if (pageState.TaksaKF.IdTaksa > 0) {
                    grida.setTekstQelize('cbTVSH', rreshtaTeGridesId[i], pageState.TaksaKF.KodTaksa, null, undefined, myelemComboTVSHSup);
                    changedTVSHReshti(rreshtaTeGridesId[i]);
                }
                else {
                    arrartikull[i] = grida.getTekstQelize('txtIdKodi', rreshtaTeGridesId[i]);
                    arrlloji[i] = grida.getTekstQelize('cmbLloji', rreshtaTeGridesId[i]);
                }
            }
        }
        if (pageState.TaksaKF.IdTaksa == 0) {
            merrTVshSipasARtikullit(arrartikull, arrlloji);
        }
    }
}
function SucceededCallbackVendosTVSH(result) {
    var grida = $(pageState.gridaSelector);
    var rreshtaTeGridesId = grida.jqGrid('getDataIDs');
    for (var i = 0; i < rreshtaTeGridesId.length; i++) {
        if (grida.getTekstQelize('txtKodi', rreshtaTeGridesId[i]) && result.length > i) {
            grida.setTekstQelize('cbTVSH', rreshtaTeGridesId[i], result[i], null, undefined, myelemComboTVSHSup);
            changedTVSHReshti(rreshtaTeGridesId[i]);
        }
    }
}
function LlogaritResto() {
    if (txtKursiPagese.GetText() != "" && txtKursiPagese.GetText() != "0")
        txtVleftePagese.SetText(txtTotal2.GetText() / txtKursiPagese.GetText());
    txtResto.SetText(txtPaguar.GetText() - txtVleftePagese.GetText());
}

var resultkonvertime = new Array();
var countkonvertime = 0;
function Konverto(result, merrtedhena) {
    if (result.length > 0) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "mbushFushaPerKonvertim"),
            data: JSON.stringify({ result: JSON.stringify(result), merrtedhena: merrtedhena, idNdermarrje: pageState.idNdermarrje, idPerdorues: pageState.idPerdoruesi })
        }).done(SuccededCallbackKonvertim);
    }
}

function SuccededCallbackKonvertim(result) {
    var grida = $(pageState.gridaSelector);
    var gridaKomision = $(pageState.gridaKomision);
    if (result[9] === null && result[10] === null) {
        if (result[0] != 0 && btnKlienti.GetText() == '')
            callWebserviceKF({ idKf: result[0], callBack: SucceededCallbacOKF, mosPlotesoTeDhena: false, infoKf: true });
    }
    else if (result[9] !== null)
        mbushTeDhenaKokeNgaShitja(result[9]);
    else if (result[10] !== null)
        mbushTeDhenaKokeNgaMagazina(result[10]);
    var colTrup = result[1];
    var colArt = result[2];
    var colMakro = result[3];
    var colLlog = result[4];
    var colDetArt = result[5];
    var colDetArt2 = result[11];
    var colNjesAdminis = result[6];
    var colNjesiArt = result[7];
    var coltaksa = result[8];
    grida.jqGrid('saveRow', grida.getLastSel2(), null, 'clientArray', {});
    var lloji, kodi, kodbari, pershkrimi, detajimet, detajimet2, njesia, sasia, cmimi, vlefta, zbritja, TVSH, vlefteTVSH, magazina, idkodi, gjeresi, gjatesi, sasipermase, shenime, dtfillimi, dtmbarimi, sasiarez, rezervim, serial, zbritjevlere,
        llojzbritje, cmimitvsh;

    var arrid = grida.jqGrid('getDataIDs');
    for (g = 0; g < arrid.length; g++) {
        dataRow = grida.jqGrid('getRowData', arrid[g]);
        if (dataRow.txtKodi == "") {
            grida.jqGrid('delRowData', arrid[g]);
        }
    }
    //lastsel2 = parseFloat(arrid[arrid.length - 1]) + 1;
    grida.setLastSel2(parseFloat(arrid[arrid.length - 1]) + 1);
    for (var i = 0; i < colArt.length; i++) {
        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || (cmbModeli.GetText().length >= 5 && cmbModeli.GetText().substring(0, 5) == "VFONE" && $("input[id$='hfShtimModifikim']").val() == "konvertim") || (lidhur && $("input[id$='hfShtimModifikim']").val() != "bli"))

            be = myJQGrid.myValueButtonFshi(true, grida.getLastSel2(), "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, grida.getLastSel2(), "#rowed5");
        if (colTrup[i].IdLlojVeprimi == 1) {
            lloji = "Artikull";
            kodi = colArt[i].KodArtikulli;
            if (colArt[i].OColKodbare.length > 0)
                kodbari = colArt[i].OColKodbare[0].Pershkrimi;
            else kodbari = "";
            if (pershk == 1)
                pershkrimi = colArt[i].PershkrimArtikulli;
            else {
                if (colArt[i].PershkrimiAngArtikulli !== "")
                    pershkrimi = colArt[i].PershkrimiAngArtikulli;
                else pershkrimi = colArt[i].PershkrimArtikulli;
            }
            idkodi = colArt[i].IdArtikulli;
            if (colArt[i].IRezervueshem) {
                rezervim = 'true';
            }
            else {
                rezervim = 'false';
            }
        }
        else if (colTrup[i].IdLlojVeprimi == 2) {
            lloji = "Makro";
            kodi = colMakro[i].KodiKokaMakro;
            pershkrimi = colMakro[i].PershkrimiKokaMakro;
            idkodi = colMakro[i].IdKokaMakro;
        }
        else {
            lloji = "Llogari";
            kodi = colLlog[i].NrLlogari;
            if (pershk == 1)
                pershkrimi = colLlog[i].EmerLlogari1;
            else {
                if (colLlog[i].EmerLlogari2 !== "")
                    pershkrimi = colLlog[i].EmerLlogari2;
                else pershkrimi = colLlog[i].EmerLlogari1;
            }
            idkodi = colLlog[i].IdLlogari;
        }
        detajimet = (colDetArt[i].KodDetajimArtikulli == null) ? "" : colDetArt[i].KodDetajimArtikulli;
        detajimet2 = (colDetArt2[i].KodDetajimArtikulli == null) ? "" : colDetArt2[i].KodDetajimArtikulli;

        if (colNjesAdminis[i].Kodi != '' && colNjesAdminis[i].Kod != null)
            magazina = colNjesAdminis[i].Kodi;
        else
            if (colArt[i].Magazina != '' && colArt[i].Magazina != null)
                magazina = colArt[i].Magazina;
            else
                magazina = colMagazina[0].Kodi;
        njesia = colNjesiArt[i].KodNjesia;
        TVSH = coltaksa[i].KodTaksa;
        sasia = colTrup[i].Sasia;
        sasiarez = colTrup[i];
        gjeresi = colTrup[i].Gjeresi;
        gjatesi = colTrup[i].Gjatesi;
        sasipermase = colTrup[i].SasiPermasa;
        cmimi = colTrup[i].Cmimi;
        cmimitvsh = colTrup[i].Cmimi * (1 + coltaksa[i].NormaPerqindje / 100);
        vlefta = colTrup[i].VleftaMeTvsh;
        zbritja = colTrup[i].Zbritje;
        zbritjavlere = colTrup[i].ZbritjaVlere;
        llojzbritje = colTrup[i].LlojZbritje;
        vlefteTVSH = colTrup[i].VleftaPaTvsh;
        shenime = colTrup[i].Shenime;
        var idRow = grida.getLastSel2();
        var idRowKomision = grida.getLastSel2();
        if (hfSeriale.Contains(colTrup[i].IdKodi + '_' + idRow) && hfSeriale.Get(colTrup[i].IdKodi + '_' + idRow) != "[]") {
            serial = 'Me serial';
        }
        else serial = 'Pa serial';
        dtfillimi = (colTrup[i].DtFillimi == undefined) ? "" : new Date(colTrup[i].DtFillimi).format('dd/MM/yyyy');
        dtmbarimi = (colTrup[i].DtMbarimi == undefined) ? "" : new Date(colTrup[i].DtMbarimi).format('dd/MM/yyyy');
        var datarow = {
            cmbLloji: lloji, txtIdKodi: idkodi, txtKodi: kodi, txtKodbari: kodbari, txtPershkrimi: pershkrimi, txtDetajimi: detajimet, txtDetajimi2t: detajimet2,
            cmbNjesia: njesia, txtMagazina: magazina, txtGjeresi: gjeresi, txtGjatesi: gjatesi, txtSasiPermase: sasipermase, txtSasia: sasia, txtCmimi: cmimi, txtZbritja: zbritja, txtVleftaTVSH: vlefteTVSH, cbTVSH: TVSH, txtVlefta: vlefta, txtShenime: shenime,
            txtDtFillimi: dtfillimi, txtDtMbarimi: dtmbarimi, txtIdTrupi: '0', txtIdTrupiKonvertimi: colTrup[i].IdShitjeKoka, txtSasiaRez: sasiarez, txtRezervuar: rezervim, txtIdTrupiRezervimi: 0, txtSasiMbetur: 0, txtIdTrupiTransferimi: 0, txtSerial: serial, txtIdTrupiKthim: 0, txtIdTrupiKonvertimBlerje: 0, txtZbritjaVlere: zbritjavlere, txtLlojZbritje: llojzbritje, txtCmimiTvsh: cmimitvsh, txtFshi: be
        };

        var su = grida.jqGrid('addRowData', parseInt(idRow), datarow);
        idRow = idRow + 1;
        grida.setLastSel2(idRow);

    }
    updateTotalet(parseFloat(txtPerqindje.GetText()), parseFloat(txtVlefte.GetText()));
}

function mbushTeDhenaKokeNgaShitja(shitje) {
    txtNumerProjekti.SetText(shitje.NrProjekt);
    if (shitje.IdPikeShitjeFurnizimi != 0)
        cmbPikeShitjeFurnizimi.SetValue(shitje.IdPikeShitjeFurnizimi);
    if (shitje.IdDegeAdministrative != 0)
        cmbDegeAdministrative.SetValue(shitje.IdDegeAdministrative);
    txtPershkrimi.SetText(shitje.Pershkrimi);
    if (shitje.Dogana)
        cmbDogana.SetValue('True');
    else cmbDogana.SetValue('False');
    txtAdresaFaturimit.SetText(shitje.AdresaFaturimit);
    txtEmriKlienti.SetText(shitje.EmerKlienti);
    txtKontakti.SetText(shitje.Kontakti);
    txtAdresaDergimit.SetText(shitje.AdresaDergimit);
    if (shitje.IdMenyreTransporti != 0)
        btnMenyreTransporti.SetValue(shitje.IdMenyreTransporti);
    dateTransportimi_DateEdit.SetDate(shitje.DtTransportimi);
    DtFillimi_DateEdit.SetDate(shitje.DtFillimi);
    DtMbarimi_DateEdit.SetDate(shitje.DtMbarimi);
    DtFature_DateEdit.SetDate(shitje.DtFature);
    if (shitje.IdKushtDergimi != 0)
        btnKushtDergimi.SetValue(shitje.IdKushtDergimi);
    if (shitje.IdAgjent != 0)
        btnAgjenti.SetValue(shitje.IdAgjent);
    if (shitje.IdArka != 0)
        btneArka.SetValue(shitje.IdArka);
    dateMaturimi_DateEdit.SetDate(shitje.DtMaturimi);
    cmbMenyrePagese.SetValue(shitje.IdMenyrePagese);
    cmbLlojMarreveshje.SetValue(shitje.IdLlojMarreveshje);
    if (shitje.IdKushtPagese != 0)
        btnKushtPagese.SetValue(shitje.IdKushtPagese);
    txtVlefte.SetText(shitje.Zbritje);
    cbShpenzimeJoTeZbritshme.SetChecked(shitje.ShpenzimeJoTeZbritshme);
    txtKerkuarNga.SetText(shitje.KerkuarNga);
    txtNrDokMagazine.SetText(shitje.NrDokMagazine);
    DtKerkese_DateEdit.SetDate(shitje.Datekerkese);
    if (shitje.IdKlientFurnitor != 0)
        callWebserviceKF({ idKf: shitje.IdKlientFurnitor, callBack: SucceededCallbacOKF, mosPlotesoTeDhena: true, infoKf: true });
}

function mbushTeDhenaKokeNgaMagazina(mag) {
    if (mag.IdKlientFurnitor != 0) {
        callWebserviceKF({ idKf: mag.IdKlientFurnitor, callBack: SucceededCallbacOKF, mosPlotesoTeDhena: false, infoKf: true });
    }
    txtNumerProjekti.SetText(mag.NrProjekt);
    txtPershkrimi.SetText(mag.Shenime);
    if (mag.IdMagazina != 0) {
        btnMagazina.SetValue(mag.IdMagazina);
        previousMag = btnMagazina.GetSelectedItem();
    }
    if (mag.IdDegeAdministrative != 0)
        cmbDegeAdministrative.SetValue(mag.IdDegeAdministrative);
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

function SuccedcallbackBosh() {
}

function Succedcallback(result) {
    lbxZgjedhur.ClearItems();
    for (var i = 0; i < result.length; i++) {
        lbxZgjedhur.AddItem([result[i].PershkrimKolone, ''], result[i].EmerKolone);
    }
    lbxZgjedhur.SetHeight(result.length * 22 + 28);
}

function SuccedcallbackLlog(result) {
    lbxLlogari.ClearItems();
    for (var i = 0; i < result.length; i++) {
        lbxLlogari.AddItem([result[i].PershkrimKolone, ''], result[i].EmerKolone);
    }
    lbxLlogari.SetHeight(result.length * 22 + 28);
}

function SuccedcallbackKF(result) {
    lbxKF.ClearItems();
    for (var i = 0; i < result.length; i++) {
        lbxKF.AddItem([result[i].PershkrimKolone, ''], result[i].EmerKolone);
    }
    lbxKF.SetHeight(result.length * 22 + 28);
}

function Expanded() {
    lbxZgjedhur.SetHeight(lbxZgjedhur.GetItemCount() * 22 + 28);
    lbxLlogari.SetHeight(lbxLlogari.GetItemCount() * 22 + 28);
    lbxKF.SetHeight(lbxKF.GetItemCount() * 22 + 28);
}

function KontrolloTeDrejta(s) {
    var alti = $(s.GetValue()).attr('alt');
    //    if (s.GetText().split('alt="')[1].split('"')[0] == "Artikulli" && $('#hfTeDrejtaInfoArt').val() == 'False')
    if (alti == "Artikulli" && $('#hfTeDrejtaInfoArt').val() == 'False')
        s.SetEnabled(false);
    if (alti == "KF" && $('#hfTeDrejtaInfoKF').val() == 'False')
        s.SetEnabled(false);
}

function hapPopUpRi(eshteAqt) {
    var qs = '?vjenNga=Shto_RegjistrimDokumentash&veprimi=shtim';
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
    var grida = $(pageState.gridaSelector);
    var kodArtikulli = grida.getTekstQelize('txtKodi', grida.getLastSel2());
    if (!kodArtikulli) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShkruaniKodArtikulli"));
        return;
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullSipasKodit"),
        data: JSON.stringify({ kodi: kodArtikulli, merrfurnitor: false, idNdermarrje: pageState.idNdermarrje, merrDetajime: false })
    }).done(HapLupeModifikimArtikulli);
}

function hapPopUpInfoArtPerberes() {
    var grida = $(pageState.gridaSelector);
    var kodArtikulli = grida.getTekstQelize('txtKodi', grida.getLastSel2());
    if (!kodArtikulli) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShkruaniKodArtikulliPerberes"));
        return;
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullSipasKodit"),
        data: JSON.stringify({ kodi: kodArtikulli, merrfurnitor: false, idNdermarrje: pageState.idNdermarrje, merrDetajime: false })
    }).done(HapLupeArtikujPerberes);


}

function hapPopUpinfoGrupimPerberes() {
    var grida = $(pageState.gridaSelector);
    var kodArtikulli = grida.getTekstQelize('txtKodi', grida.getLastSel2());
    if (!kodArtikulli) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShkruaniKodArtikulliPerberes"));
        return;
    }

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullSipasKodit"),
        data: JSON.stringify({ kodi: kodArtikulli, merrfurnitor: false, idNdermarrje: pageState.idNdermarrje, merrDetajime: false })
    }).done(HapLupeGrupimPerberes);


}
function onHapLupeArkive() {
    var grida = $(pageState.gridaSelector);
    var idArtikull = grida.getTekstQelize('txtIdKodi', grida.getLastSel2());
    if (idArtikull) {
        ButtonClickArkiva(idArtikull, { params: { veprimi: "artikull", idDok: idArtikull } });
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniArtikullin"));
    }
}
function onHapPopUpImazheArkive() {
    var grida = $(pageState.gridaSelector);
    var idArtikull = grida.getTekstQelize('txtIdKodi', grida.getLastSel2());
    if (idArtikull) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheImazheArkive"),
            data: JSON.stringify({ idEntitet: idArtikull, idKategoria: 13 }) //13 - kategoria per artikullin
        }).done(hapLupeImazheArkive).fail(function (obj) { console.error(obj.responseText); });
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniArtikullin"));
    }
}
function hapLupeImazheArkive(result) {
    //{ statusPergjigje = mesazh.Status, mesazh = mesazh.PershkrimMesazhi, listaUrl = myArkiva }
    if (!result.statusPergjigje) {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        return;
    }
    Arkiva.HapLupeImazheArkive(result.listaUrl);
}

function HapLupeModifikimArtikulli(result) { //TODO per tu hequr thirrja e webservices-it
    if (result == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliNukEkzistonOseJoAutorizim"));
        return;
    }
    var artikulli = result.artikulli;
    if (artikulli.LlojiArt) //nqs eshte artikull afatgjate
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikullZgjedhurAfatgjate"));
    else {
        kodArtikulli = encodeURIComponent(artikulli.KodArtikulli);
        myButtonClickLupa.LupaUniversal_Click('Modifiko Artikull', 'LupaArtikullShpejte.aspx?vjenNga=Shto_RegjistrimDokumentash&llojiart=afatshkurter&veprimi=modifikim&kodArt=' + kodArtikulli + '&idartikulli=' + artikulli.IdArtikulli, 1100, 600);
    }
}

function HapLupeArtikujPerberes(result) {
    var artikulli = result.artikulli;

    if (artikulli == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliNukEkzistonOseJoAutorizim"));
        return;
    }
    var queryString = {
        idartikull: artikulli.IdArtikulli,
        data: data_DateEdit.GetText(),
        artikullIPerbere: artikulli.Klasa == 4 || artikulli.Klasa == 5 || artikulli.Klasa == 6 //Perbere,Prodhim, Prodhim me proces
    };

    var headerTitle = queryString.artikullIPerbere ? hfState.Get("lblArtikujtPerberes") : hfState.Get("lblArtikujTePerbere");
    myButtonClickLupa.LupaUniversal_Click(headerTitle, 'LupaShfaqArtikujPerberes.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 400);
}

function HapLupeGrupimPerberes(result) {
    var grida = $(pageState.gridaSelector);
    var ids = grida.getDataIDs();
    var idMePresje = "";
    var idArtikujshKryesor = "";

    for (var i = 0; i < ids.length; i++) {
        if (i < ids.length - 1) {
            idMePresje += grida.getTekstQelize('txtIdKodi', ids[i]) + "," + grida.getTekstQelize('txtSasia', ids[i]) + ";";
            idArtikujshKryesor += grida.getTekstQelize('txtIdKodi', ids[i]) + ",";
        }
        else if (grida.getTekstQelize('txtIdKodi', ids[i]) != "" && grida.getTekstQelize('txtIdKodi', ids[i]) != null) {
            idMePresje += grida.getTekstQelize('txtIdKodi', ids[i]) + "," + grida.getTekstQelize('txtSasia', ids[i]);
            idArtikujshKryesor += grida.getTekstQelize('txtIdKodi', ids[i]);
        }
    }
    if (idMePresje.charAt(idMePresje.length - 1) == ';') {
        idMePresje = idMePresje.substr(0, idMePresje.length - 1);
    }
    if (idArtikujshKryesor.charAt(idArtikujshKryesor.length - 1) == ',') {
        idArtikujshKryesor = idArtikujshKryesor.substr(0, idArtikujshKryesor.length - 1);
    }
    if (result.artikulli != null) {
        if (result.artikulli.Klasa == 4)
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("lblGrupimPerberesish"), 'LupaShfaqGrupimPerberes.aspx?idartikullSasi=' + idMePresje + '&idartikull=' + idArtikujshKryesor + '&data=' + data_DateEdit.GetText(), 950, 400);
        else myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliJoPerbere"));
    }
    else //nqs artikulli nuk ekziston
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliNukEkzistonOseJoAutorizim"));
}

var mbyll = true;
function RuajHapurMbyllur(hapur) {
    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else $('#hfHapurMbyllur').val('True');
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllur"),
            data: JSON.stringify({ hapur: hapur, idperdorues: pageState.idPerdoruesi, idperdoruesveprimi: pageState.idPerdoruesi })
        }).done(SuccededCallbackHapurMbyllur);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimRuajtje"));
    }
}

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

function RuajHapurMbyllurminus(hapur) {
    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else
        $('#hfHapurMbyllur').val('True');
    $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurminus"), data: JSON.stringify({ hapur: hapur, idperdorues: pageState.idPerdoruesi, idperdoruesveprimi: pageState.idPerdoruesi }) }).done(SuccededCallbackHapurMbyllur);
}

function SuccededCallbackHapurMbyllur(result) {
    myMesazh.ShtoMesazhSuksesi(result);
}

function HeaderClick(s, e) {
    if (!mbyll) { e.cancel = true; mbyll = true; } //per rastet kur shtyp butonat + dhe -
}

function kontrollagjent(agjenti) {
    var vleraagjentit;
    if (agjenti == '1') {
        vleraagjentit = btnAgjenti.GetValue();
        if ((vleraagjentit == btnAgjenti2.GetValue() || vleraagjentit == btnAgjenti3.GetValue()) && vleraagjentit != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAgjentZgjedhurNjeHere"));
            btnAgjenti.SetValue("");
            txtPerqindjeAgjent.SetValue("");
            return;
        }
    }
    else if (agjenti == '2') {
         vleraagjentit = btnAgjenti2.GetValue();
         if ((vleraagjentit == btnAgjenti.GetValue() || vleraagjentit == btnAgjenti3.GetValue()) && vleraagjentit != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAgjentZgjedhurNjeHere"));
            btnAgjenti2.SetValue("");
            txtPerqindjeAgjent2.SetValue("");
            return;
         }
    }
    else {
        vleraagjentit = btnAgjenti3.GetValue();
         if ((vleraagjentit == btnAgjenti.GetValue() || vleraagjentit == btnAgjenti2.GetValue()) && vleraagjentit != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAgjentZgjedhurNjeHere"));
            btnAgjenti3.SetValue("");
            txtPerqindjeAgjent3.SetValue("");
            return;
         }
    }
}

function changeRadio(agjenti) {

    if (agjenti == '1')
        kthePerqindjeAgjentit('btnAgjenti', 'txtPerqindjeAgjent', agjenti);

    else if (agjenti == '2')
        kthePerqindjeAgjentit('btnAgjenti2', 'txtPerqindjeAgjent2', agjenti);

    else if (agjenti == '3')
        kthePerqindjeAgjentit('btnAgjenti3', 'txtPerqindjeAgjent3', agjenti);
}

function kthePerqindjeAgjentit(butonAgjent, txtPerqindjeAgjenti, agjenti) {

    if (Utils.ktheKontroll(butonAgjent).GetValue() == null) {
        Utils.ktheKontroll(txtPerqindjeAgjenti).SetText('');
        return;
    }

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrPerqindjeAgjenti"),
        data: JSON.stringify({ idAgj: Utils.ktheKontroll(butonAgjent).GetValue(), llojAgj: agjenti, idKlient: btnKlienti.GetValue() })
    }).done(SucceededCallbackData);

}

function SucceededCallbackData(result) {
    if (result.perqindjeAgjenti && result.perqindjeAgjenti > 0) {
        if (result.llojAgenti == '1')
            txtPerqindjeAgjent.SetText(result.perqindjeAgjenti);

        else if (result.llojAgenti == '2')
            txtPerqindjeAgjent2.SetText(result.perqindjeAgjenti);

        else if (result.llojAgenti == '3')
            txtPerqindjeAgjent3.SetText(result.perqindjeAgjenti);

    }

}

function SucceededCallbackMarresi(result) {
    txtMarresi.SetText(result);
}

var konfigurimet, idte;
function SuccededCallbackKonvertime(result) {
    Utils.hiqLoadingGif();
    var nivelet = result.nivelet;
    var colKonfig = result.colKonfig;
    if (result.mesazh !== "Nuk jane konvertuar") {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        return;
    }
    if (nivelet.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukMundTeKonvertohet"));
        return;
    }
    cmbKonverto.BeginUpdate();
    cmbKonverto.ClearItems();
    var teDrejtaNiveli;
    for (i = 0; i < nivelet.length; i++) {
        teDrejtaNiveli = merrTeDrejtaNiveli(nivelet[i].IdNivel);
        if (teDrejtaNiveli && !teDrejtaNiveli.DAmb && !teDrejtaNiveli.DShtim && !teDrejtaNiveli.DShtimDraft)
            continue;
        cmbKonverto.AddItem(nivelet[i].Kodi, nivelet[i].IdNivel); //AddItem(teksti, vlera);
    }

    cmbKonverto.EndUpdate();
    cmbKonverto.SelectIndex(0);
    cmbKonf.BeginUpdate();
    cmbKonf.ClearItems();
    for (i = 0; i < colKonfig.length; i++)
        if (colKonfig[i].IdNivel == cmbKonverto.GetValue())
            cmbKonf.AddItem(colKonfig[i].KodKonfigAmbjente, colKonfig[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.EndUpdate();
    cmbKonf.SelectIndex(0);
    konfigurimet = colKonfig;
    idte = result.ids;
    popKonvertim.Show();
}

function ndryshoNiveli(s, e) {
    cmbKonf.ClearItems();
    for (i = 0; i < konfigurimet.length; i++)
        if (konfigurimet[i].IdNivel == s.GetValue())
            cmbKonf.AddItem(konfigurimet[i].KodKonfigAmbjente, konfigurimet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.SelectIndex(0);
}

function konverto() {
    Utils.konverto(idte, "", "", SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen);
}

function SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen() {

    if (cmbKonverto.GetText() == 'FD' || cmbKonverto.GetText() == 'UD')
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=dalje' + '&id=' + idte[0] + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&fsh=po' + '&pageCacheId=' + window['CurrentPageId']);

    else if (cmbKonverto.GetText() == 'FH' || cmbKonverto.GetText() == 'UH')
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=hyrje' + '&id=' + idte[0] + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&fsh=po' + '&pageCacheId=' + window['CurrentPageId']);

    else if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') && (cmbKonverto.GetText() == 'FB' || cmbKonverto.GetText() == 'UB' || cmbKonverto.GetText() == 'OB' || cmbKonverto.GetText() == 'KB'))
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&id=' + idte[0] + '&shtim_modifikim=konvertimblerje&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&klientKonv=' + btnKlienti.GetValue() + '&pageCacheId=' + window['CurrentPageId']);
    else
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + pageState.veprimi + '&id=' + idte[0] + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&klientKonv=' + btnKlienti.GetValue() + '&pageCacheId=' + window['CurrentPageId']);

}

function ButtonClickKursi(s, e) {
    if (cmbMonedha.GetValue() != null && cmbMonedha.GetValue() != 0) {
        popupUniversal.SetHeaderText('Zgjidh Kursin');
        popupUniversal.SetContentUrl('LupaKursiShpejte.aspx?idMonedha=' + cmbMonedha.GetValue() + '&llojKursi=' + llojkursi);
        popupUniversal.SetSize(widthLupaKF, heightLupaKF);
        popupUniversal.Show();
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


function cmbMuajiSelectedChanged(s, e) {
    var muajiDtDok = data_DateEdit.GetDate().getMonth() + 1;
    var vitiDtDok = data_DateEdit.GetDate().getFullYear();
    var muajiRap = cmbMuajRaportimi.GetValue();
    var vitiRap = cmbVitRaportimi.GetText();
    if ((muajiDtDok > muajiRap && ((vitiDtDok > vitiRap) || vitiDtDok == vitiRap)) || (vitiDtDok > vitiRap))
        myMesazh.ShtoMesazhGabimi("Periudha e raportimit duhet te jete me e madhe ose e barabarte me periudhen e dokumentit!");
}

function cmbVitiSelectedChanged(s, e) {
    var muajiDtDok = data_DateEdit.GetDate().getMonth() + 1;
    var vitiDtDok = data_DateEdit.GetDate().getFullYear();
    var muajiRap = cmbMuajRaportimi.GetValue();
    var vitiRap = cmbVitRaportimi.GetText();
    if ((muajiDtDok > muajiRap && ((vitiDtDok > vitiRap) || vitiDtDok == vitiRap)) || (vitiDtDok > vitiRap))
        myMesazh.ShtoMesazhGabimi("Periudha e raportimit duhet te jete me e madhe ose e barabarte me periudhen e dokumentit!");
}

function RenditjeCheck(reload) {
    var pageStateLloji = pageState.lloji;
    if (cbRenditje.GetChecked() && (pageStateLloji == "modifikim")) {
        lejomod = false;
    }
    else {
        if (pageStateLloji === "kthim" || hfState.Get("MosModifikoTrup"))
            lejomod = false;
        else
            lejomod = true;
        cbRenditje.SetEnabled(false);
        if (reload) {
            var hfLidhur = $("input[id$='hfLidhur']");
            var aprovim = false;

            if (pageStateLloji != 'klonim' && pageStateLloji != 'kthim' && pageStateLloji != 'kthimVod' && pageStateLloji != 'bli' && pageStateLloji != 'konvertim' && pageStateLloji != 'konvertimblerje' && pageStateLloji != 'rezervim' && (($('#hfTeDrejtaModSkema').val() == "False" && lblStatusAprovimi.GetText() == 'Per Aprovim') || (lblStatusAprovimi.GetText() == 'Aprovuar' || lblStatusAprovimi.GetText() == 'Refuzuar'))) {
                aprovim = true;
            }
            //lastsel2 = -1;
            var grida = $(pageState.gridaSelector);
            grida.setLastSel2(-1);
            grida.jqGrid('GridUnload', "rowed5");
            var konvSipasUrdherShitje = (hfTeDrejta.Get("KonvertimSipasUSH") == true && hfState.Get("konvNgaUshNeFsh") == true);
            var isLidhur = (hfLidhur.val().toLowerCase() === 'true');
            if (aprovim || konvSipasUrdherShitje)
                isLidhur = true;
            inicializoGride(isLidhur);
            mbushGrideNgaHiddenFieldet(isLidhur);
        }
    }
}

//====================kartat e klientit =========================


function TextChangedKarta(s, e) {

    pastroFushaKartaKlienti();
    if (cmbKarta.GetSelectedItem() != null) {
        var karta = cmbKarta.GetSelectedItem();
        if (karta != null)
            callWebserviceKarta(parseInt(karta.value));
    }
    else {
        var selectedKF = btnKlienti.GetSelectedItem();
        //nqs ka zbritje mos merr ate te klientit perseri
        if (selectedKF != null && parseFloat(txtPerqindje.GetText()) == 0) {
            callWebServiceKategorizbritje(selectedKF.GetColumnText('KodKlientFurnitor'), data_DateEdit.GetDate());
        }
    }
}

function callWebserviceKarta(idKarta) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKarteKlienti"),
            data: JSON.stringify({ idKarte: idKarta })
        }).done(SucceededCallbacKarta);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

function SucceededCallbacKarta(rezult) {
    if (rezult != null) {
        $("input[id$='hfKarta']").val(JSON.stringify(rezult));
        txtShoferi.SetText(rezult.Shoferi);
        txtTarga2.SetText(rezult.Targa);
        var idKlienti = Number(btnKlienti.GetValue());
        if (rezult.OColKlientFurnitor && rezult.OColKlientFurnitor.length > 0) {

            //nuk ka klient te selektuar dhe karta eshte e  lidhur me vetem nje klient
            if (rezult.OColKlientFurnitor.length == 1) {
                var kf = rezult.OColKlientFurnitor[0];
                if (kf.IdKlientFurnitor !== 0 && kf.IdKlientFurnitor != idKlienti) {
                    Utils.SelectComboItem(btnKlienti, kf.IdKlientFurnitor, kf.KodKlientFurnitor, kf.EmertimiKF);
                    callWebserviceKF({ idKf: kf.IdKlientFurnitor, callBack: SucceededCallbacOKF, mosPlotesoTeDhena: false, infoKf: true });
                }
            }
            else if (rezult.OColKlientFurnitor.length > 1) {
                var kfEkzistues = rezult.OColKlientFurnitor.find(function (k) {
                    return (k.IdKlientFurnitor == idKlienti);
                });
                //nese asnje nga klientet e lidhur me karten nuk ai i zgjedhuri hiq selektimin
                if (kfEkzistues == undefined) btnKlienti.SetSelectedIndex(-1);
            }

        }

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "kthePolitikeKarte"), data: JSON.stringify({ idPolitike: rezult.IdPolitike })
        }).done(SucceededCallbackKartaPolitike);

    }
}

function SucceededCallbackKartaPolitike(result) {
    if (result != null) {
        $("input[id$='hfPolitike']").val(JSON.stringify(result));
        var karta = JSON.parse($("input[id$='hfKarta']").val());
        if (result.Lloji == 0 || result.Lloji == 2) {  //politike zbritje me % ose zbritje dhe pike

            callWebserviceKategoriZbritjeKarta(karta.IdKategoriZB);
        }
        else {//politike me pike
            //vendosim dhe njehere zbritjen sipas karteles se klientit
            var selectedKF = btnKlienti.GetSelectedItem();
            if (selectedKF != null) {
                callWebServiceKategorizbritje(selectedKF.GetColumnText('KodKlientFurnitor'), data_DateEdit.GetDate());
            }
        }
        if (result.Lloji == 1 || result.Lloji == 2)  //1)politike me pike 2) politike zbritje dhe pike
            callWebservicePikeTotaleKarta(karta.IdKarta);
    }
}

function callWebserviceKategoriZbritjeKarta(idKategoria) {
    var grida = $(pageState.gridaSelector);
    var totali = 0;
    var ids = grida.jqGrid('getDataIDs');
    var kursi = 1;

    for (var i = 0; i < ids.length; i++) {
        if (grida.getTekstQelize('txtKodi', ids[i]) == '')
            continue;
        var vlefta = grida.getTekstQelize('txtVlefta', ids[i]);
        if (vlefta == "")
            vlefta = 0;
        totali = totali + vlefta;
    }
    try {
        if (isNaN(totali)) totali = 0;
        if (txtKursi.GetText() != "")
            kursi = txtKursi.GetText();

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKategoriZbritjeKarteKlienti"),
            data: JSON.stringify({ idKatZbritje: idKategoria, date: data_DateEdit.GetDate(), vlefte: totali, kursi: kursi, idmonedha: cmbMonedha.GetValue() })
        }).done(SucceededCallbacKategoriZbritjeKarta);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

function SucceededCallbacKategoriZbritjeKarta(result) {

    if (result == null) {
        result = { eshtePerqindje: true, zbritje: 0 };
    }
    setZbritjeTotal(result.eshtePerqindje ? Utils.llojZbritje.Perqindje : Utils.llojZbritje.Vlere, result.zbritje, true);
}

function callwebserviceMbushKarta(idKlient) {
    var lkvk = hfState.Get('LKVK');
    if (idKlient != 0 && lkvk)
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKartaKlientiDT"), data: JSON.stringify({ idKlient: idKlient, idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackMbushKarta);
    else
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKartaKlientiAll"), data: JSON.stringify({ idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackMbushKarta);

}

function SucceededCallbackMbushKarta(result) {
    if (result == null)
        return;
    var lkvk = hfState.Get('LKVK');
    var karta;
    if ($('#hfKarta').val() == "") karta = null;
    else {
        karta = JSON.parse($('#hfKarta').val());
        var iNjejte = false;
        for (var i = 0; i < karta.OColKlientFurnitor.length; i++) {
            if (btnKlienti.GetValue() == karta.OColKlientFurnitor[i].IdKlientFurnitor) {
                iNjejte = true;
                break;
            }
        }
    }
    txtPike.SetText('0');
    
    if (karta != null) {
        if ((karta.IdKlient != 0 && iNjejte) || karta.IdKlient == 0) {
            var itemFoundByText = cmbKarta.FindItemByValue(karta.IdKarta); // ka raste kur cmbKarta.GetSelectedItem(); kthen null edhe pse ka karte
            if (itemFoundByText !== null) {
                cmbKarta.SetSelectedItem(itemFoundByText);
                //te behen update piket kur ringarkohen kartat
                var selectedKF = btnKlienti.GetSelectedItem();
                if (selectedKF != null) {
                    callWebServiceKategorizbritje(selectedKF.GetColumnText('KodKlientFurnitor'), data_DateEdit.GetDate());
                }
            }
        }
        else {
            cmbKarta.SetText('');
            pastroFushaKartaKlienti();
        }
    }
    else
        pastroFushaKartaKlienti();
}

function pastroFushaKartaKlienti() {
    txtPike.SetText('0');
    txtTotalPike.SetText('0');
    $('#hfKarta').val("");
    $('#hfPolitike').val("");
    $('#hfTotalPikesh').val('0');
}

function SucceededCallbackAutomjetILidhur(result) {
    if (result != null && result.KodKlientFurnitor != null) {
        btneAutomjeti.SetText('');
        btneAutomjeti.SetSelectedIndex(-1);
        btneAutomjeti.SetValue(null);
        txtTarga.SetText('');
    }
}

function SucceededCallbackIDAutomjet(result) {
    if (result != null) {
        var idAuto = result;
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientAutomjeti"), data: JSON.stringify({ idAutomjeti: idAuto }) }).done(SucceededCallbackAutomjetILidhur);
    }
}

function TextChangedKlienti(s, e) {
    if (cmbKarta.GetVisible()) {
        if (btnKlienti.GetText() == "") {
            callwebserviceMbushKarta(0);
        }
        else {
            callwebserviceMbushKarta(btnKlienti.GetValue());
            var karta;
            if ($('#hfKarta').val() == "")
                karta = null;
            else {
                karta = JSON.parse($('#hfKarta').val());
                var iNjejte = false;
                for (var i = 0; i < karta.OColKlientFurnitor.length; i++) {
                    if (btnKlienti.GetValue() == karta.OColKlientFurnitor[i].IdKlientFurnitor) {
                        iNjejte = true;
                        break;
                    }
                }
            }

            if (karta != null && karta.IdKlient != 0 && karta.OColKlientFurnitor && !iNjejte) {
                cmbKarta.SetSelectedIndex(-1);
                pastroFushaKartaKlienti();
                cmbKarta.ClearItems();
            }
        }
    }
    if (btneAutomjeti.GetValue() != null)
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheIDAutomjeti"), data: JSON.stringify({ Automjeti: btneAutomjeti.GetText(), idNdermarrje: pageState.idNdermarrje }) }).done(SucceededCallbackIDAutomjet);

    else return;
}


function kontrolloSasiLimit(idRow, idartikulli, kontrollSasi) {
    if ((pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar'))
        return;
    var totali = 0;
    var totalivlere = 0;
    var idkarta = 0;
    if (cmbKarta.GetValue())
        idkarta = cmbKarta.GetValue();
    else return;
    if (idartikulli == "")
        return;
    var grida = $(pageState.gridaSelector);
    var ids = grida.getDataIDs();
    var artikulli = memoryArt.Get(idartikulli);
    ///gjejme totalet e artikullit,
    for (var i = 0; i < ids.length; i++) {
        if (grida.getTekstQelize('txtIdKodi', ids[i]) == idartikulli) {

            totalivlere = totalivlere + parseFloat(grida.getTekstQelize('txtVlefta', ids[i]));

            if (artikulli.KodNjesia1 === grida.getTekstQelize('cmbNjesia', ids[i]))
                totali = totali + parseFloat(grida.getTekstQelize('txtSasia', ids[i]));
            else
                totali = totali + (parseFloat(grida.getTekstQelize('txtSasia', ids[i])) * artikulli.KoeficientArtikulli);
        }
    }
    callWebserviceLimitArtikulli(idartikulli, idkarta, totali, totalivlere, data_DateEdit.GetDate(), kontrollSasi);
}


function callWebserviceLimitArtikulli(idartikulli, idkarta, totali, totalivlere, dtdok, kontrollSasi) {
    if (totali >= 0 && kontrollSasi)
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "kontrolloLimitSasiKarte"),
            data: JSON.stringify({ idartikulli: idartikulli, idkarta: idkarta, dtDok: dtdok, totali: totali })
        }).done(SucceededCallbacLimitiKarta).fail(function () {

            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        });
    if (totalivlere >= 0)
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "kontrolloLimitVlereKarte"),
            data: JSON.stringify({ idartikulli: idartikulli, idkarta: idkarta, dtDok: dtdok, totali: totalivlere })
        }).done(SucceededCallbacLimitiKartaVlere).fail(function () {

            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        });

}

function SucceededCallbacLimitiKarta(result) {
    if (!result) {
        myMesazh.vendosClient();
        identifikuesPyetje = "limiti";
        myMesazh.ShtoPyetje("Eshte kaluar limiti per sasine e artikullit! Doni te vazhdoni?", true);
        return true;
    }
}

function SucceededCallbacLimitiKartaVlere(result) {
    if (!result) {
        myMesazh.vendosClient();
        identifikuesPyetje = "limiti";
        myMesazh.ShtoPyetje("Eshte kaluar limiti per vleren e artikullit! Doni te vazhdoni?", true);
        return true;

    }
}

function callWebservicePikeTotaleKarta(idkarta) {

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "kthePikeKarte"),
        data: JSON.stringify({ idkarta: idkarta, idNdermarrje: pageState.idNdermarrje })
    }).done(SucceededCallbacPikeTotaleKarta).fail(function () {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    });
}

function SucceededCallbacPikeTotaleKarta(result) {
    txtTotalPike.SetText(result);
    $('#hfTotalPikesh').val(result);

}


//=========================== End Kartat ===============================

function llogaritSasiGjeresiGjatesiArtSipasFormulesKodbarit(artikulli, kodbari, idRreshti, grida) {
    if (artikulli.MeBarkodLogjik) {
        if (artikulli.SkemaBarkodit !== '') {
            var sasiGjeresiGjatesiSipasFormulesKodbarit = Utils.llogaritSasiGjeresiGjatesiArtSipasFormulesKodbarit(kodbari, artikulli.SkemaBarkodit, grida.getTekstQelize('txtSasiPermase', idRreshti));
            if (sasiGjeresiGjatesiSipasFormulesKodbarit) {
                grida.setTekstQelize('txtSasia', idRreshti, sasiGjeresiGjatesiSipasFormulesKodbarit.Sasia);
                grida.setTekstQelize('txtGjeresi', idRreshti, sasiGjeresiGjatesiSipasFormulesKodbarit.Gjeresia);
                grida.setTekstQelize('txtGjatesi', idRreshti, sasiGjeresiGjatesiSipasFormulesKodbarit.Gjatesia);
            }
            else
                myMesazh.ShtoMesazhGabimi("Nuk mund te aplikohet formula e kodbarit sepse gjatesia e formules se kodbarit dhe kodbarit te artikullit nuk perkojne!");
        }
    }
}

function hapLupeHarte(s, e) {
    popupUniversal.SetHeaderText('Cakto ne harte');
    popupUniversal.SetContentUrl('LupaHarta.aspx?idNjesia=' + 0);
    popupUniversal.SetSize(700, 800);
    popupUniversal.Show();
}

function vendosGeomNeHfState(s, e) {
    if (btneCaktoNeHarte.GetText() == '')
        hfState.Set("geom", '');
    else {
        var koordinata = btneCaktoNeHarte.GetText();
        koordinata = koordinata.split(', ');
        if (koordinata.length != 2) {
            alert('Formati i kordinatave duhet te jete: "x.x, y.yy" - pra te ndara me ", "');
        }
        else {
            koordinata_x = koordinata[0];
            koordinata_y = koordinata[1];
            if ((!kontrollokoordinate(koordinata_x)) || (!kontrollokoordinate(koordinata_y)))
                alert('Formati i kordinatave duhet te jete: "x.x, y.yy" - pra te ndara me ", "');
        }
        var k = 'POINT (' + koordinata[1] + ' ' + koordinata[0] + ')';
        hfState.Set("geom", JSON.stringify(([k])));
    }
}

function kontrollokoordinate(koordinata) {
    koordinata = koordinata.split('.');
    var kontroll = true;
    if (koordinata.length != 2) {
        return false;
    }
    else
        for (var i = 0; i < koordinata.length; i++) {
            if (isNaN(koordinata[i])) {
                kontroll = false;
                break;
            }
        }
    return kontroll;
}
function Ngarko() {
    hfState.Set("kategoriSeriali", cmbKategoriSeriali.GetText());
    // ucEmerSkedari.Upload();
}



function hapFineUpload() {
    $('#fine-uploader-validation').fineUploader({
        template: 'qq-template-validation',
        request: {
            endpoint: Utils.getServerApiUrl("Rregjistrime", "NgarkoFile"),
            params: {
                scopeID: Utils.getUrlVar("scopeID"),
                lloji: "SkedareSerial"
            }
        },
        thumbnails: {
            placeholders: {
                waitingPath: '/fine-uploader/placeholders/waiting-generic.png',
                notAvailablePath: '/fine-uploader/placeholders/not_available-generic.png'
            }
        },
        paramsInBody: true,
        deleteFile: {
            enabled: true,
            forceConfirm: true,
            endpoint: Utils.getServerApiUrl("Rregjistrime", "FshiFile"),
            params: {
                scopeID: Utils.getUrlVar("scopeID"),
                lloji: "SkedareSerial"
            }
        },
        validation: {
            allowedExtensions: ['csv', 'xls', 'xlsx', 'txt']
        },
        onComplete: function (id) {
            qq(this.getItemByFileId(id)).remove();
        },
        callbacks: {
            onError: function (id, name, errorReason, xhrOrXdr) {
                myMesazh.ShtoMesazhGabimi(qq.format("Error ne ngarkimin e skedarit me numer {} - {}.  Gabimi: {}", id, name, errorReason));
            }
        },
        messages: Utils.getGlobalization(pageState.idGjuha)
    });
}

function ngarkoKategoriSerialesh() {
    var kategoriSerialesh = JSON.parse(hfState.Get("kategoriSerialesh"));
    var kategorite = [];
    kategorite.push('<option></option>');
    $.each(kategoriSerialesh, function (key, value) {
        kategorite.push('<option value="' + value.Kategori + '">' + value.Kategori + '</option>');
    });
    $('#selKategoriSeriali').html(kategorite.join(''));
}

function MerrArtikujDheSasitePerkatese() {
    var artikujMeSasi = new Array();
    var rreshta = $(pageState.gridaSelector).getTeDhenaRreshti().filter(function (s) { return s.txtIdKodi != ''; });
    for (var i = 0; i < rreshta.length; i++)
        artikujMeSasi.push({ IdArtikulli: rreshta[i].txtIdKodi, Sasi: rreshta[i].txtSasia, Mag: rreshta[i].txtMagazina, IdSeti: 0 });
    return artikujMeSasi;
}

function hapNgarkimSerialesh() {
    $('#myModal').modal('toggle');

    $('#fine-uploader-validation').unbind().empty();
    $("#selKategoriSeriali").val($("#selKategoriSeriali option:first").val());
    $('#selKategoriSeriali').on('change', function () {
        hapFineUpload();
        var kategoria = $("#selKategoriSeriali option:selected").text();
        if (kategoria == '')
            $('#fine-uploader-validation').hide();
        else
            $('#fine-uploader-validation').show();
    });

    $('#buttonNgarko').unbind().click(function () {
        hfState.Set("kategoriSeriali", $("#selKategoriSeriali option:selected").text());
        serialetCallbackPanel.PerformCallback(JSON.stringify(MerrArtikujDheSasitePerkatese()));
    });
}

function BeginCallback(s, e) {
    Utils.shfaqLoadingGif();
}

function SkedariUploaded(s, e) {
    var mesazhgabimi = s.cpSerialMesazhGabimi;
    if (mesazhgabimi) {
        myMesazh.ShtoMesazhGabimi(mesazhgabimi);
    }
    if (s.cpArtikujSeriale && s.cpArtikujSeriale != "" && pageState.kushte["MNSA"])
        ShtoArtikujPerSerialet(JSON.parse(s.cpArtikujSeriale));

    Utils.hiqLoadingGif();
    delete s.cpArtikujSeriale;
    delete s.cpSerialMesazhGabimi;
}


function SetKaSeriale(arrSerialet, nrSerialesh) {
    if (arrSerialet.length > 0) {
        kaSeriale = true;
    } else if (nrSerialesh != undefined) {
        kaSeriale = nrSerialesh == 0;
    }
}


function ShtoArtikujPerSerialet(arrSerialet, idArtikujPertuHequr, nrSerialesh) {

    var grida = $(pageState.gridaSelector);

    if (idArtikujPertuHequr != undefined) {
        for (var i = 0; i < idArtikujPertuHequr.length; i++) {
            var id = grida.gjejIdRreshtiSipasFunksionit(function (rreshti) {
                return rreshti.txtIdKodi == idArtikujPertuHequr[i].IdArtikulli.toString() && rreshti.txtMagazina == idArtikujPertuHequr[i].KodMag;
            });
            if (id)
                fshiClicked(id);

        }
    }

    SetKaSeriale(arrSerialet, nrSerialesh);

    var idRow = parseInt(grida.gjeRreshtBosh("txtIdKodi"));
    grida.selektoRreshtin(idRow, true);
    var magazine = grida.getTekstQelize('txtMagazina', grida.getLastSel2());

    var idRreshti = -1;
    var buttonFshiEnable = percaktoButonFshiEnabled();
    for (var i = 0; i < arrSerialet.length; i++) {
        var idRreshtiEkzistues = grida.gjejIdRreshtiSipasFunksionit(function (rreshti) {
            return rreshti.txtIdKodi == arrSerialet[i].IdArtikulli.toString() && (arrSerialet[i].KodMag == undefined || rreshti.txtMagazina == arrSerialet[i].KodMag);
        });

        if (idRreshtiEkzistues) {
            if (grida.getTekstQelize("txtSasia", idRreshtiEkzistues) != arrSerialet[i].Sasia) {
                grida.setTekstQelize("txtSasia", idRreshtiEkzistues, arrSerialet[i].Sasia);
                ndryshonSasia(idRreshtiEkzistues);
            }

            continue;
        }
        var newIdRreshti = grida.gjeRreshtBoshPasKetijRreshti("txtIdKodi", undefined, undefined, idRreshti);
        // var be = myJQGrid.myValueButtonFshi(buttonFshiEnable, idRreshti, "#rowed5");
        if (newIdRreshti == -1) {
            idRreshti = grida.shtoRresht(buttonFshiEnable, pageState.gridaSelector);
        } else {
            idRreshti = newIdRreshti;
        }
        $("#txtCmimiTvsh" + idRreshti).data('pending', 1);
        grida.selektoRreshtin(idRreshti, true);
        $("#txtCmimiTvsh" + idRreshti).removeData('pending');
        grida.setTekstQelize('cmbLloji', idRreshti, 'Artikull');
        grida.setTekstQelize('txtKodi', idRreshti, ' ');
        grida.setTekstQelize("txtSasia", idRreshti, arrSerialet[i].Sasia);
        grida.setTekstQelize('txtMagazina', idRreshti, arrSerialet[i].KodMag);
        grida.vendosTeDhenaPerQelizen('txtMagazina', idRreshti, "IshMagazina", arrSerialet[i].KodMag);

        callWebserviceArtikulliIPlote({ idArt: arrSerialet[i].IdArtikulli, idRresht: idRreshti, magazine: magazine, kodKodbarArt: "", listeIMEIArtikull: [], sasiaNeGride: { shtimModifikim: "shtim", idDok: 0, dokShitje: true, detSasite: [{ Detajim: "", Sasi: 2 }] }, KodMagSerialesh: arrSerialet[i].KodMag });
        // grida.jqGrid('setSelection', idRreshti + 1);
    }

    grida.rregulloNrRendor("txtNrRendor");
}


function onSelectedIndexChangeserial() {

}

function LostFocusSerial() { }
function TextChangeFile() { }

function FshiDokument() {
    var guidString = hfState.Get("guidString");
    var komponente = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=" + Utils.getUrlVar("shitje_blerje");
    var komponShitje_blerje = Utils.getUrlVar("shitje_blerje");
    var arrayId = new Array(1);
    arrayId[0] = Utils.getUrlVar("id");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "FshiDokument"),
        data: JSON.stringify({ ids: arrayId, komponente: komponente, guidString: guidString, komponShitje_blerje: komponShitje_blerje, komponPerTedrejtat: komponente })
    }).done(SuccededCallbackDelete);
}

function SuccededCallbackDelete(result) {
    Utils.hiqLoadingGif();
    if (result.mesazhSukses != "") {
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);

        for (var i = 0; i < pageState.webhook.length; i++) {
            if (pageState.webhook[i].Kategoria == 1)
                kategoria = "shitje"
            else if (pageState.webhook[i].Kategoria == 0)
                kategoria = "blerje"
            else kategoria=""
            if ((pageState.webhook[i].Eventi == 3 || pageState.webhook[i].Eventi == 0)&& kategoria == Utils.getUrlVar('shitje_blerje') && pageState.webhook[i].Aktive == true) {
                if (result.objektifshire.length != 0) {

                    $.ajax({
                        type: "POST",
                        url: pageState.webhook[i].Urlpritese,
                        data: result.objektiWebhook,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response != null) {
                                alert("Eventi : " + response.Event + ", Kategri : " + response.Kategori + ", Mesazh :" + response.Mesazh);
                            } else {
                                console.log("Something went wrong");
                            }
                        },
                        failure: function (response) {
                            console.log(response.responseText);
                        },
                        error: function (response) {
                            console.log(response.responseText);
                        }
                    });
                }
            }

        }
        if (result.mesazhRivleresim != "") {
            identifikuesPyetje = "Rivleresimi";
            myMesazh.ShtoPyetje(result.mesazhRivleresim);
        }
        else
            window.location = "RegjistrimDokumentash.aspx?shitje_blerje=" + Utils.getUrlVar("shitje_blerje");
    }
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
   
   
}

function Rivleresim() {
    var guidString = hfState.Get("guidString");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "BejRivleresim"),
        data: JSON.stringify({ guidString: guidString })
    }).done(function (result) {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhSesioni(result);
        window.location = "RegjistrimDokumentash.aspx?shitje_blerje=" + Utils.getUrlVar("shitje_blerje");
    });
}

function merrTeDrejtaNiveli(idNiveli) {
    var teDrejtaNivele = JSON.parse(hfState.Get("teDrejtaNivele"));
    return teDrejtaNivele.filter(function (eDrejta) { return eDrejta.IdNivelRegjistrimi == idNiveli; })[0];
}

function VlereDefaultDokument(kontrolli) {
    for (var i = 0; i < pageState.colKontrollet.length; i++)
        if (pageState.colKontrollet[i].KodKontrolli == kontrolli)
            return pageState.colAtrTrupi[i].VlereDefault;
    return undefined;
}

function vendosVlereDefaultDokumentOseBoshAgjent(kodAgjent, btnAgjenti, kodPerqindjeAgjent, txtPerqindjeAgjent, lloji) {
    var agjent = VlereDefaultDokument(kodAgjent);
    if (agjent != undefined) {
        btnAgjenti.SetValue(agjent);
        var perqindje = VlereDefaultDokument(kodPerqindjeAgjent);
        if (perqindje != undefined)
            txtPerqindjeAgjent.SetText(perqindje);
        changeRadio(lloji);
        kontrollagjent(lloji);
    }
    else {
        btnAgjenti.SetText('');
        btnAgjenti.SetSelectedIndex(-1);
        txtPerqindjeAgjent.SetText('');
    }

}

function kontrolloPerqindjeAgjentiPaAgjent(btnAgjenti, txtPerqindjeAgjent) {
    if (btnAgjenti && txtPerqindjeAgjent) {
        var agjent = btnAgjenti.GetValue();
        var perqindje = txtPerqindjeAgjent.GetText();
        if ((!agjent || agjent == "") && perqindje && perqindje != "")
            txtPerqindjeAgjent.SetText('');
    }
}

function vendosAgjentDhePerqindjeKlientiOseAgjenti(btnAgjenti, idAgjenti, perqindjeNgaKlienti, perqindjeNgaAgjent, txtPerqindjeAgjent, lloji) {
    btnAgjenti.SetValue(idAgjenti);
    if (perqindjeNgaKlienti && perqindjeNgaKlienti > 0)
        txtPerqindjeAgjent.SetText(perqindjeNgaKlienti);
    else if (perqindjeNgaAgjent && perqindjeNgaAgjent > 0)
        txtPerqindjeAgjent.SetText(perqindjeNgaAgjent);
    changeRadio(lloji);
    kontrollagjent(lloji);
}

function vendosAgjentinDefaultTeKlientit(IdAgjent, btnAgjenti, perqindje, txtPerqindjeAgjent, lloji) {
    if (IdAgjent != 0) {
        btnAgjenti.SetValue(IdAgjent);
        if (perqindje != null && perqindje > 0)
            txtPerqindjeAgjent.SetText(perqindje);
        kontrollagjent(lloji);
        changeRadio(lloji);
    }
}

function vendosVlerePerqindjeDefaultDokumenti(btnAgjenti, kodPerqindjeAgjent, txtPerqindjeAgjent) {
    if ((btnAgjenti && btnAgjenti.GetValue() != '' && btnAgjenti.GetValue() != 0) && (!txtPerqindjeAgjent.GetText() || txtPerqindjeAgjent.GetText() == "")) {
        var perqindje = VlereDefaultDokument(kodPerqindjeAgjent);
        if (perqindje && perqindje > 0)
            txtPerqindjeAgjent.SetText(perqindje);
    }
}

function mbushVleraAgjenteshPerqindjeAgjentesh(result) {
    mbushVlereAgjentiPerqindjeAgjenti(result.mosPlotesoTeDhena, btnAgjenti, result.kf.IdPerfaqesuesShitje, result.kf.PerqindjeAgjenti, result.perqindjeagjent, txtPerqindjeAgjent, '1', "txtPerqindjeAgjent", "btnAgjenti");
    mbushVlereAgjentiPerqindjeAgjenti(result.mosPlotesoTeDhena, btnAgjenti2, result.kf.IdPerfaqesuesShitje2, result.kf.PerqindjeAgjenti2, result.perqindjeagjent2, txtPerqindjeAgjent2, '2', "txtPerqindjeAgjent2", "btnAgjenti2");
    mbushVlereAgjentiPerqindjeAgjenti(result.mosPlotesoTeDhena, btnAgjenti3, result.kf.IdPerfaqesuesShitje3, result.kf.PerqindjeAgjenti3, result.perqindjeagjent3, txtPerqindjeAgjent3, '3', "txtPerqindjeAgjent3", "btnAgjenti3");
}

function mbushVlereAgjentiPerqindjeAgjenti(mosPlotesoTeDhena, btnAgjenti, agjenti, perqindjeNgaKlienti, perqindjeNgaAgjenti, txtPerqindjeAgjent, lloji, kodPerqindjeAgjent, kodAgjent) {
    if (!(mosPlotesoTeDhena === true && btnAgjenti.GetText() !== "")) {
        if (agjenti != 0) {
            vendosAgjentDhePerqindjeKlientiOseAgjenti(btnAgjenti, agjenti, perqindjeNgaKlienti, perqindjeNgaAgjenti, txtPerqindjeAgjent, lloji);
            vendosVlerePerqindjeDefaultDokumenti(btnAgjenti, kodPerqindjeAgjent, txtPerqindjeAgjent);
        }
        else
            vendosVlereDefaultDokumentOseBoshAgjent(kodAgjent, btnAgjenti, kodPerqindjeAgjent, txtPerqindjeAgjent, "1");
    }
}


function ktheVlereKomision(perqindje, vleftaPaTvsh) {
    if (perqindje == 0 || perqindje == null || vleftaPaTvsh == 0 || vleftaPaTvsh == null)
        return '';
    else
        return vleftaPaTvsh * (perqindje / 100);
}


//funksion qe do llogarise sa rreshta me komision jane 
function ShtoRreshtaKomision() {
    var grida = $(pageState.gridaSelector);
    var rreshtaTeGridesId = grida.jqGrid('getDataIDs');
    var idLlogarite = [];
    var llogariGurpuar = [];
    var llogariteWs = {};
    var llogariteGrupuarWs = [];

    for (var i = 0; i < rreshtaTeGridesId.length; i++) {
        idLlogarite[i] = { id: grida.merrTeDhenaPerQelizen("txtKodi", rreshtaTeGridesId[i], "IdLlogari"), sasia: grida.getTekstQelize('txtVleraKomisionit', rreshtaTeGridesId[i]) };
    }
    var k = 0;
    var indeksi;
    for (var j in idLlogarite) {
        if (idLlogarite[j].id != "" && idLlogarite[j].id != null && idLlogarite[j].id != undefined) {
            indeksi = llogariGurpuar.findIndex(function (x) { return x.idja == idLlogarite[j].id; });
            if (indeksi != -1)
                llogariGurpuar[indeksi] = { idja: llogariGurpuar[indeksi].idja, sasia: Number(llogariGurpuar[indeksi].sasia) + Number(idLlogarite[j].sasia), index: indeksi + 1, llojTvsh: 2, idPerdoruesi: pageState.idPerdoruesi, TaksaKF: pageState.TaksaKF };
            else {
                llogariGurpuar[k] = { idja: idLlogarite[j].id, sasia: Number(idLlogarite[j].sasia), index: k + 1, llojTvsh: 2, idPerdoruesi: pageState.idPerdoruesi, TaksaKF: pageState.TaksaKF };
                k++;
            }
        }
    }

    var l = 0;
    for (var i = 0, len = llogariGurpuar.length; i < len; i++) {
        if (llogariGurpuar[i].sasia != 0) {
            llogariteGrupuarWs[l] = llogariGurpuar[i];
            llogariteWs[l] = { "idja": llogariGurpuar[i].idja, "TaksaKF": llogariGurpuar[i].TaksaKF, "index": llogariGurpuar[l].index, "llojTvsh": llogariGurpuar[i].llojTvsh, "idPerdoruesi": llogariGurpuar[i].idPerdoruesi };
            l++;
        }
    }
    var gridaKomision = $(pageState.gridaKomision);

    if (Object.keys(llogariteWs).length != 0)
        callWebServiceLlogariPlote(llogariteWs, true, llogariteGrupuarWs);    //duhen shtuar tek grida e re tani 
    else
        gridaKomision.jqGrid('clearGridData');
}

function callWebServiceLlogariPlote(object, mbushGrideKomision, llogariGurpuar) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogIDTvsh"),
        data: JSON.stringify(object)
    }).done(function (result) { SucceededCallbackLlogPlote(result, mbushGrideKomision, llogariGurpuar); });
}

function vendosVlereKomisioniSipasArtikullitAndKlientit(klient, idRreshti, grida) {
    if (klient != null && klient != undefined && klient != "") {
        var klienti = JSON.parse(pageState.Klient);
        if (klienti.kf.LlogaritKomision && grida.merrTeDhenaPerQelizen("txtKodi", idRreshti, "MeKomision"))
            grida.setTekstQelize('txtVleraKomisionit', idRreshti, ktheVlereKomision(grida.merrTeDhenaPerQelizen("txtKodi", idRreshti, "ZbritjaKlientit"), grida.getTekstQelize('txtVleftaTVSH', idRreshti)));//shtuar markel
        else
            grida.setTekstQelize('txtVleraKomisionit', idRreshti, 0);
    }
}

function DtMbarimi_changed(s, e) {
    if (pageState.kushte["VF_VM"]) {
        if (cmbStatusMarreveshje.GetValue() == 1 && DtMbarimi_DateEdit.GetDate() < Utils.ktheDateServeriFromCookies())
            lblExpired.SetVisible(true);
        else
            lblExpired.SetVisible(false);
    }
}

function SucceededCallbackMarreveshje(result) {//therritet ne mbarim te webservice NgarkoMarreveshje tek ucPopUpValidimMarreveshje.js
    if (!result.Mesazh.Status) {
        myMesazh.ShtoMesazhGabimi(result.Mesazh.PershkrimMesazhi);
        return;
    }
    else if (result.Mesazh.Tipi == 2 && result.Mesazh.PershkrimMesazhi != "")
        myMesazh.ShtoMesazhInformues(result.Mesazh.PershkrimMesazhi);

    $('#ValidoMarreveshje').modal('toggle');
    var grida = $(pageState.gridaSelector);
    if (result.Kokashitje.IdShitjeKoka > 0)
        hapDokMarreveshje(result.Kokashitje);

    else {
        txtIdMarreveshje.SetText(result.AgreementID);
        grida.setTekstQelize('txtVlefta', grida.getLastSel2(), result.VleraBuxhetit);//artkulli default Buxhet i vendoset vlefta me tvsh
        changedVleftaTVSH('txtVlefta', grida.getLastSel2());
        MbushKFVartes(result.Klientet);
    }
}
function SucceededCallbackIDMarreveshje(result) {//therritet ne mbarim te webservice KerkoMarreveshje tek ucPopUpValidimMarreveshje.js
    if (result.Kokashitje.IdShitjeKoka > 0) {
        if (pageState.lloji == "klonim") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgExistIDmarreveshjeNukKlonohet"));
            return;
        }
        else {
            if (!result.Mesazh.Status) {
                myMesazh.ShtoMesazhGabimi(result.Mesazh.PershkrimMesazhi);
                return;
            }
            else if (result.Mesazh.Tipi == 2 && result.Mesazh.PershkrimMesazhi != "")
                myMesazh.ShtoMesazhInformues(result.Mesazh.PershkrimMesazhi);
            hapDokMarreveshje(result.Kokashitje);
        }
    }
    else {
        txtIdMarreveshje.SetText(result.idMarreveshje);
        $('#ValidoMarreveshje').modal('toggle');
    }
}

function hapDokMarreveshje(kokashitje) {
    var dataMbarimi = new Date(kokashitje.DtMbarimi);
    if ((kokashitje.StatusMarreveshje == 1 && dataMbarimi <= Utils.ktheDateDefault(pageState.periudha)) || kokashitje.StatusMarreveshje == 2)
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + Utils.getUrlVar('shitje_blerje') + '&id=' + kokashitje.IdShitjeKoka + '&shtim_modifikim=modifikim&pageCacheId=' + window['CurrentPageId']);
    else
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + Utils.getUrlVar('shitje_blerje') + '&id=' + kokashitje.IdShitjeKoka + '&shtim_modifikim=klonim&pageCacheId=' + window['CurrentPageId'] + '&modMarreveshje=modifikim');
}

function TextChangedNrDok(s, e) {
    if (pageState.lloji == 'modifikim')
        txtNrDokMagazine.SetText(txtNumer.GetText());
}
function MerrPiketNeModifikimTeVfoneMeWebservice(idDok) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "kthePiketNeModifikimTeVFONE"), data: JSON.stringify({ idDok: idDok })
    }).done(SuccededCallbackKthePikeNeModifikimVFONE);

}

function SuccededCallbackKthePikeNeModifikimVFONE(result) {
    txtPike.SetText(result.Piket);
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