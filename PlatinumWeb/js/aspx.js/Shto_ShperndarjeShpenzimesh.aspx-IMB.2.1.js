;

var pageState = {
    widthLupaLlogari: 600,
    heightLupaLlogari: 600,
    widthLupaKF: 600,
    heightLupaKF: 600,
    widthLupaKerko: 600,
    heightLupaKerko: 600,
    eshteShtuarFatureERe: false,
    identifikuesPyetje: "",
    gridFaturat: null,
    gridTrupiShpenzimi: null,
    gridLlogaria: null,
    meKontabilizim: false,
    menyreKontabilizimi: 0,
    meRivleresim: false,
    pergjigje: "",
    kokaHapurDokumentShpenzimi: null,
    iLidhur: false
};


//anullon veprimin e enterit
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

jQuery(document).ready(function () {
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }

    }).trigger('resize');
    var colTrupiGrida = null;

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
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });
});

function validim(s,e)
{
    myFaqeCelje.validim(s, e);
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}


function formatoFushaDevi(formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri))
    Utils.setFormatNumri(txtTotaliVleftaPaTVSH, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotaliVlefteShpenzimi, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtVlera, formatNumri.ShifraPasPresjesVlefta);
    Utils.formatoTextBox(txtVlera);
    Utils.formatoTextBox(txtTotaliVleftaPaTVSH);
    Utils.formatoTextBox(txtTotaliVlefteShpenzimi);

}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtVlera);
    Utils.unFormatoTextBox(txtTotaliVleftaPaTVSH);
    Utils.unFormatoTextBox(txtTotaliVlefteShpenzimi);
}

/*
Function: mbushGrideNgaHiddenFieldi

Merr te dhena nga hidden field-et dhe me to ploteson griden. Hidden field-et plotesohen ne server side kur behet modifikim dokumenti.
*/
function mbushGrideNgaHiddenFieldi() {
  
}


var arrNrLlogarie = new Array();
var arrEmerLlogarie = new Array();
var arrVlefteLlogarie = new Array();
var editorNrLlogarie;
var editorEmerLlogarie;
var editorVlefteLlogarie;
var indexCounter;
var indeksPerEmerLlogarie;
var counterNrLlogarie = 0;
var counterVlefteLlogarie = 0;


function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_ShperndarjeShpenzimesh.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_ShperndarjeShpenzimesh.aspx', Utils.getUrlVar('id'));
    }
    catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

function Init() {
    if (typeof (isPostBack) == "undefined") {
        identikuesPerPopupKlientFurnitori = "ShperndarjeShpenzimesh";
        identikuesPerPopupArtikulli = "ShperndarjeShpenzimesh";
        identifikuesPerPopupDetajime = "ShperndarjeShpenzimesh";
        identifikuesPerPopupMagazina = "ShperndarjeShpenzimesh";
        identikuesPerPopupLlogari = "ShperndarjeShpenzimesh";
        identifikuesPerPopupDokumentat = "ShperndarjeShpenzimeshKerkoMenu";
        changeName();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        myMesazh.shtoHandler();
        var hf = $("#hfKonffillestar");
        cmbKonfigurimi.SetText(hf.val());
        ndryshoKonfigurimin();
    }
}

/*
Function: ndryshoKonfigurimin

Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    if (cmbKonfigurimi.GetSelectedIndex() < 0)
        return;
    if (cmbKonfigurimi.GetSelectedItem().texts != null && cmbKonfigurimi.GetSelectedItem().texts.length > 1)
        lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[0]);
    callWebserviceKonfigurimi(518, cmbKonfigurimi.GetText());
}

/*
Function: TextChangedLloji

Ben ndryshime ne gride ne varesi te llojit te veprimit te zgjedhur (hyrje, Dalje apo Transferim)
*/
function TextChangedLloji() {
    var mod;
    if ($("#hfShtimModifikim").val() == 'modifikim')
        mod = true;
    else mod = false;
    if (cmbLloji.GetText() != "")
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'shsh', mod);
    else callWebserviceNiveliNew('', 'shsh', mod);
}

/*
Function: callWebserviceNiveli

Therret funksionin <ktheTemplatetNivelit> per te marre temlaten e nivelit.
Shiko funksionin <SucceededCallbackNiveli>.
*/
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
    cmbKonfigurimi.ClearItems();
    for (i = 0; i < colModelet.length; i++) {
        cmbKonfigurimi.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    }
    cmbKonfigurimi.SelectIndex(0);
    ndryshoKonfigurimin();
}

/*
Function: SucceededCallbackNiveli

Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function SucceededCallbackNiveli(result) {
    var vlerat = '';
    vlerat = result.split('|');
    cmbKonfigurimi.ClearItems();
    for (i = 0; i < vlerat.length - 1; i++) {
        var arr = vlerat[i].split(',')[1].split(';')
        cmbKonfigurimi.AddItem(arr, vlerat[i].split(',')[0]); //AddItem(teksti, vlera);
    }
    cmbKonfigurimi.SelectIndex(0);
    ndryshoKonfigurimin();
}

/*
Function: callWebserviceKonfigurimi

Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per shperndarjen e shpenzimeve.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    try {
        var eshteShtim = $("#hfShtimModifikim").val() == 'shtim';
        var idGjuha = hfState.Get('_idGjuha');
        var idNdermarrje = hfState.Get('_idNdermarrje');
        var idPerdoruesi = hfState.Get('_idPerdoruesi');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: "", idObjekti: -1, shtim: eshteShtim, merrFormatKursi: false, merrGjitheKonf: true, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje })
        }).done(SucceededCallbackKonfig);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

/*
Function: SucceededCallbackKonfigurimi

U vendos atributeve te kontrolleve vlerat e konfigurimit perkates.
*/
var colGrida;
var formatNumriZgjedhur;
function SucceededCallbackKonfig(result) {
    DevExpress.localization.locale(hfState.Get("_idGjuha") == 0 ? "al" : "en");

    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ["tblFillim", "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];

    var colKontrollet = result.colKontroll;
    var colAtrTrupi = result.colAtrTrupi;

    formatNumriZgjedhur = result.formatNumri.KonfigTrupi[0];

    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    pageState.iLidhur = hfLidhur.val() == "True";

    $("#dvgvLlogarite").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    $("#dvbutonShpernda").show();

    pastro();
    formatoFushaDevi(formatNumriZgjedhur);

    colGrida = result.colGrida;
    var colKushte = result.colKushte;
    var colAlterKusht = result.colAlterKusht;
    var kodniveli = result.kodniveli;

    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShpernd.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }

    var hfLlog = $("#hfLlogaria")[0];
    
    for (j = 0; j < colKushte.length; j++) {
        if (colKushte[j].Kodi == 'GJK') {
            if (colAlterKusht[j].Alternativa == 'Jo') {
                pageState.meKontabilizim = false;
                pageState.menyreKontabilizimi = 0;
            }
            else {
                pageState.meKontabilizim = true;
                if (colAlterKusht[j].Alternativa == "Direkt")
                    pageState.menyreKontabilizimi = 1;
                else
                    pageState.menyreKontabilizimi = 2;
            }
        } if (colKushte[j].Kodi == 'KR') {
            if (colAlterKusht[j].Alternativa == 'Po')
                pageState.meRivleresim = true;
            else
                pageState.meRivleresim = false;
        }

    }

    var columnsKonfigKoka = colGrida.filter(function (item) { return item.GridKokaEmri == 'gvTrupiShperndarjeShpenzimesh' });
    var columnsKonfigTrupi = colGrida.filter(function (item) { return item.GridKokaEmri == 'gvSubGridShperndarjeShpenzimesh' });
    bejGatiGrideTrupiShpenzimi(columnsKonfigKoka, columnsKonfigTrupi);
    bejGatiGrideFaturat(colGrida.filter(function (item) { return item.GridKokaEmri == 'grid_faturat' }));
    if (pageState.meKontabilizim)
        bejGatiGrideLlogarite(colGrida.filter(function (item) { return item.GridKokaEmri == 'gvLlogaria' }));
    else
        fshiGrideLlogaria();

    if (hf.val() == 'modifikim') {
        callWebserviceDokumentShperndarjeShpenzimi();
    }
}

function fshiGrideLlogaria() {
    pageState.gridLlogaria = null;
    $("#dxDataGrid_llogaria").remove();
    $("<div id='dxDataGrid_llogaria' class ='noUndoGrida'>").appendTo($("#data-grid-llogaria"));
}

/*  Function: ButtonClickKerkoFatura

Hap lupen e dokumentave (Kur zgjedh fature per te marre nje total vlerash per shperndarje)
*/
function ButtonClickKerkoFatura() {
    identifikuesPerPopupDokumentat = "ShperndarjeShpenzimeshKerko";
    var queryString = {
        veprimi: 'ShperndarjeShpenzimeshKerko',
        idKonfigAmbjente:$("#hfLupaFatura").val()
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

/*
Function: isValidKoka

Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit.
Kontrollon totalet.
*/
function isValidKoka() {
    var totali = 0;
    var llogarite = new Array();
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    if (pageState.meKontabilizim) {
        pageState.gridLlogaria.SaveCurrentValues();
        totali = pageState.gridLlogaria.GetSummaryOfColumns(["Vlefta"], "sum");
        llogarite = pageState.gridLlogaria.GetData();
    }
    if (txtNrDok.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    else if (parseFloat(txtVlera.GetText()) == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosniVlShperndarje"));
        return false;
    }
    else if (cmbLloji.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojin"));
        return false;
    }

    else if (dateDtRegjistrimi.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    else {
        if (pageState.meKontabilizim && (llogarite.length < 1 || llogarite.filter(function (row) { return row.IdLlogaria > 0 }).length == 0)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoLlogariZgjidhniTePaktenNjeLlogari"));
            return false;
        }

        else if (parseFloat(txtTotaliVlefteShpenzimi.GetText()).toFixed(2) != parseFloat(txtVlera.GetText()).toFixed(2)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgTotaliVleftaNdryshemVlShpernd"));
            return false;
        }
        else if (pageState.meKontabilizim && pageState.menyreKontabilizimi != 0 && parseFloat(txtTotaliVlefteShpenzimi.GetText()).toFixed(2) != parseFloat(totali).toFixed(2)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKujdesTotalNjejteTotVlLlogari"));
            return false;
        }
        else if (pageState.meKontabilizim && pageState.eshteShtuarFatureERe){
          myMesazh.ShtoMesazhGabimi(hfState.Get("msgKujdesTotalNjejteTotVlLlogari"));
          return false;
        }
        else return true;
    }
}

/*
Function: pastro

Pastrin array-t e perdorura dhe textbox-et ku vendosen totalet.
*/
function pastro() {
    txtNrDok.SetText('');
    var dataSot = Utils.zeroOren(new Date());
    dateDtRegjistrimi.SetDate(dataSot);
    txtShenime.SetText('');
    var hf = $("#status1")
    hf.val("false");
    txtVlera.SetText("0.00");
    txtTotaliSasia.SetText("0.00");
    txtTotaliVleftaPaTVSH.SetText("0.00");
    txtTotaliVlefteShpenzimi.SetText("0.00");
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    switch (e.item.name) {
        case "Ruaj":
            e.processOnServer = false;
            if (isValidKoka())
                ruajDokumentShperndarjeShpenzimi(1);
            break;
        case "Draft":
            e.processOnServer = false;
            if (isValidKoka())
                ruajDokumentShperndarjeShpenzimi(0);
            break;
        case "Pastro":
        case "Shto":
            e.processOnServer = false;
            PastroClick();
            break;
        case "Kerko":
            ButtonClickKerko("ShperndarjeShpenzimesh.aspx");
            e.processOnServer = false;
            break;
        case "Fshi":
            e.processOnServer = false;
            pageState.identifikuesPyetje = "Fshi";
            myMesazh.ShtoPyetje(hfState.Get("labelAdministrimiMsgJeniSigurt"));
            break;
        case "Anullo":
            myFaqeCelje.kontrolloTeDrejta("ShperndarjeShpenzimesh.aspx");
            e.processOnServer = false;
            break;
    }
}

function PoClick(s, e) {//po
    switch (pageState.identifikuesPyetje) {
        case "Fshi":
            Utils.shfaqLoadingGif();
            FshiShperndarjeShpenzimi();
            break;
        case "shperndarjeQendraKostoMag":
            hapPopUp();
            break;
        case "rivleresim":
            bejRivleresimNgaShperndarjeShpenzimi();
            if (pageState.pergjigje == "ruaj")
                popUpQendraKostoMagazina();
            break;
    }
}

function JoClick(s, e) {//po
    switch (pageState.identifikuesPyetje) {
        case "shperndarjeQendraKostoMag":
            if ($('#hfUrl').val() != '')
                $('#hfUrl').val($('#hfUrl').val().replace($('#hfUrl').val().split(';')[0] + ";", ""));
            if ($('#hfqkmesazhi').val() == 'shfaqmesazh' && $('#hfUrl').val() != '') {
                myMesazh.ShtoPyetje(hfState.Get("msgDeshironiShperndQendraKostoMag"));
            }
            else if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
                hapPopUp();
            }
            break;
        case "rivleresim":
            if (pageState.pergjigje == "fshi") {
                myFaqeCelje.kontrolloTeDrejta("ShperndarjeShpenzimesh.aspx?fshi=po");
            }
            else if (pageState.pergjigje == "ruaj") {
                myMesazh.ShtoMesazhSuksesi(hfState.Get("msgRuajtjeMeSukses"));
                PastroClick();
                popUpQendraKostoMagazina();
            }
            break;
        default:
            return false;
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("ShperndarjeShpenzimesh.aspx?ruaj=po");
}

/*
Function: PastroClick

Pastron fushat e kokes se dokumentit dhe inicializon serish griden.
*/
function PastroClick() {
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    hf.val("shtim");
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    pastro();
    ndryshoKonfigurimin();
    nvFatura.CollapseAll();
}

/*
Function: ButtonClickKerko

Hap lupen e dokumentave (kur klikon kerko tek menuja)
*/
function ButtonClickKerko(listUrl) {
    identifikuesPerPopupDokumentat = "ShperndarjeShpenzimeshKerkoMenu";

    var queryString = {
        veprimi: 'ShperndarjeShpenzimeshKerkoMenu',
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}

/////////////////////////////////////////NEW STUFF GOES HERE//////////////////////////////////

///Faturat per tu shperndare
function bejGatiGrideFaturat(columnsKonfig) {
    pageState.gridFaturat = new myDxDataGrid("dxDataGrid_faturat", {
        keyExpr: "IdDokumenti",
        editing: {
            mode: "none",
            allowUpdating: false
        },
        columnResizingMode: "nextColumn",
        showRowLines: true,
        selection: {
            mode: "multiple"
        },
        filterPanel: { visible: true },
        headerFilter: { visible: true },
        filterRow: {
            visible: true,
        },
        scrolling: {
            mode: "none"
        },
        paging: {
            pageSize: 10
        },
        groupPanel: {
            visible: true
        },
        onSelectionChanged: function (selectedItems) {
            Utils.nrWsRrugesManager.rritNrWsRruges();
            var lenObjekte = Math.max(selectedItems.currentSelectedRowKeys.length, selectedItems.currentDeselectedRowKeys.length);
            var eshteSel = selectedItems.currentSelectedRowKeys.length >= selectedItems.currentDeselectedRowKeys.length ? true : false;
            for (var i = 0; i < lenObjekte; i++) {
                if (eshteSel) {
                    CallWebServiceFaturaZgjedhur(selectedItems.currentSelectedRowKeys[i]);
                }
                else {
                    HiqRreshtNgaTrupiShperndarjeShpenzimi(selectedItems.currentDeselectedRowKeys[i]);
                }
                //if (selectedItems.currentSelectedRowKeys[i])
                //    CallWebServiceFaturaZgjedhur(selectedItems.currentSelectedRowKeys[i]);
                //else
                //    if (selectedItems.currentDeselectedRowKeys[i])
                //        HiqRreshtNgaTrupiShperndarjeShpenzimi(selectedItems.currentDeselectedRowKeys[i]);
            }
            Utils.nrWsRrugesManager.zbritNrWsRruges();
        }
    });

    pageState.gridFaturat.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides
    pageState.gridFaturat.AddCustomOptionToColumns(["DtDokumenti"], "dataType", "date");
    pageState.gridFaturat.AddCustomOptionToColumns(["DtDokumenti"], "format", "dd/MM/yyyy");
}

function nvFaturaExpanded(s, e) {
    if (!e.group.GetExpanded())
        return;

    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("ShperndarjeShpenzimi", "KtheFaturaPerShperndarje")
    }).done(function (result) {
        pageState.gridFaturat.SetDataSource(result);
        });
}

function CallWebServiceFaturaZgjedhur(value) {
        $.ajax({
            pritPergjigje: true,
            showLoading: true,
            url: Utils.getServerApiUrl("ShperndarjeShpenzimi", "ktheFaturatFiltruaraShpenzimiObj"),
            data: JSON.stringify({ id: value })
        }).done(SucceededCallbackFaturatFiltruara);
}

function HiqRreshtNgaTrupiShperndarjeShpenzimi(value) {
    console.log("U hoq selekti nga rreshti me idDok:" + value);
}

/*Faturat per tu shperndare*/
function bejGatiGrideTrupiShpenzimi(columnsKonfigKoka, columnsKonfigTrupi) {
    pageState.gridTrupiShpenzimi = new myDxDataGrid("dxDataGrid_trupiShpenzimi", {
        keyExpr: "IdDokumenti",
        showRowLines: true,
        scrolling: { mode: "none" },
        paging: { pageSize: 10 },
        addDeleteRowCommand: true,
        columnResizingMode: "nextColumn",
        editing: {
            mode: "cell",
            allowUpdating: true,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        onRowRemoved: function () { vendosTotaleVlera(); }
    });

    pageState.gridTrupiShpenzimi.SetColumnsFromConfig(columnsKonfigKoka); //percakton kolonat e grides
    pageState.gridTrupiShpenzimi.AddCustomOptionToColumns(["DtFature"], "dataType", "date");
    pageState.gridTrupiShpenzimi.AddCustomOptionToColumns(["DtFature"], "format", "dd/MM/yyyy");

    shtoMasterDetailKoka(columnsKonfigTrupi);
}

//Shtoj si master detail te grides kryesore te importit, griden e trupit
function shtoMasterDetailKoka(columnsKonfig) {
    var trupiMasterDetail = {
        enabled: true,
        template: function (container, options) { krijoTemplatePerMasterDetailTrupi(container, options, columnsKonfig); }
    };
    pageState.gridTrupiShpenzimi.Grida.option('masterDetail', trupiMasterDetail);
}

//Krijoj nje template per griden e trupit dhe ia bashkangjis rreshtave te grides kryesore te importit
function krijoTemplatePerMasterDetailTrupi(container, options, columnsKonfig) {
    var gridaMasterDetail = krijoTemplateGrideMasterDetail(container, options.data.Trupi, columnsKonfig);
    gridaMasterDetail.GridWidget.appendTo(container);
}

function krijoTemplateGrideMasterDetail(container, dataSource, columnsKonfig) {
    $("<div>")
        .addClass("master-detail-caption")
        .appendTo(container);

    var gridContainer = $("<div>");
    var masterDetailGrid = new myDxDataGrid(gridContainer, {
        dataSource: dataSource,
        keyExpr: "IdTrupiMagazina",
        searchPanel: { visible: false },
        showRowLines: true,
        scrolling: { useNative: true },
        onRowExpanded: function (e) { e.element.find(".dx-virtual-row").hide(); },
        onRowUpdated: function () { vendosTotaleVlera(); },
        onEditorPreparing: function (e) {
            if (e.dataField == "VlereShpenzimi") {
                e.editorOptions.disabled = !(e.row.data.Shperndaj);
            }
        },
    }, true);

    masterDetailGrid.SetColumnsFromConfig(columnsKonfig);
    masterDetailGrid.AddCustomOptionToColumns(["Shperndaj"], "dataType", "boolean");
    masterDetailGrid.AddCustomOptionToColumns(["Shperndaj"], "setCellValue", function (newData, value, currentRowData) {
        newData.Shperndaj = value;
        newData.VlereShpenzimi = (value) ? currentRowData.VlereShpenzimi : 0;
    });
    return masterDetailGrid;
}

/*
Function: SucceededCallbackFaturatFiltruara

Vendos neper hidden field-e vlerat e faturave te zgjedhura.
*/
function SucceededCallbackFaturatFiltruara(result) {
    if (result.dokumenti == null)
        return;

    eshteZhdoganuarFatura(result.mesazhZhdoganuar);

    if (!eshteShtuarFatura(result.dokumenti.IdDokumenti)) {
        var data = pageState.gridTrupiShpenzimi.GetData();
        data.push(result.dokumenti);
        pageState.gridTrupiShpenzimi.Refresh();
        pageState.eshteShtuarFatureERe = true;
        vendosTotaleVlera();
    }

}


function eshteZhdoganuarFatura(mesazh) {
    if (mesazh.Status) {
        if (mesazh.PershkrimMesazhi == "")
            return;
        myMesazh.ShtoMesazhInformues(mesazh.PershkrimMesazhi);
        return;
    }
    myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
}

/*
Function: eshteShtuarFatura

Kontrollon nese fatura (qe kalohet si parameter) eshte shtuar me pare te grida e trupit apo jo.
*/
function eshteShtuarFatura(id) {
    var ds = pageState.gridTrupiShpenzimi.GetData();
    if (ds.filter(function (item) { return item.IdDokumenti == id }).length > 0)
        return true;
    return false;
}

/*
Function: ButtonClickShpernda

Shperndan vleren e caktuar neper fatura.
Shiko dhe funksionin <ruajVlereShpenzimi>.
*/
function ButtonClickShpernda() {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJuLutemPrisniDisaSekonda"));
        return;
    }
    var vlPerShperdarje = 0;
    var totaliSasi = 0;
    var totaliVlPaTvsh = 0;
    pageState.gridLlogaria.SaveCurrentValues();
    vendosTotalinLlogarive();
    pageState.eshteShtuarFatureERe = false;
    if (radioShperndaSipas.GetSelectedItem() != null) {//nese eshte zgjedhur njera nga menyrat e shperndarjes
        var dataSource = pageState.gridTrupiShpenzimi.GetData();
        var objTotale = llogaritTotaleGride(dataSource);
        totaliSasi = objTotale.totaliSasi;
        totaliVlPaTvsh = objTotale.totaliVlPaTvsh;
        
        for (var i = 0; i < dataSource.length; i++)
            for (var j = 0; j < dataSource[i].Trupi.length; j++) {
                if (dataSource[i].Trupi[j].Shperndaj == false)
                    continue;
                if (txtVlera.GetText() != "") {
                    var vlpjesetimit;
                    if (radioShperndaSipas.GetSelectedItem().value == "1")//nese eshte shperndarje sipas vleres
                        vlpjesetimit = pjesetim(dataSource[i].Trupi[j].VleftaPaTvsh, totaliVlPaTvsh);
                    else
                        vlpjesetimit = pjesetim(dataSource[i].Trupi[j].Sasia, totaliSasi);

                    dataSource[i].Trupi[j].VlereShpenzimi = vlpjesetimit * parseFloat(txtVlera.GetText());
                }
            }
        pageState.gridTrupiShpenzimi.Refresh();
        pageState.gridTrupiShpenzimi.Grida.option("masterDetail.autoExpandAll", true);
        vendosTotaleVlera();
    }
    else
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniMetodeShperndarje"));
}

function vendosTotaleVlera() {
    var dataSource = pageState.gridTrupiShpenzimi.GetData();
    var objTotale = llogaritTotaleGride(dataSource);

    txtTotaliVlefteShpenzimi.SetText(objTotale.totaliVlShpenzuar);
    txtTotaliSasia.SetText(objTotale.totaliSasi);
    txtTotaliVleftaPaTVSH.SetText(objTotale.totaliVlPaTvsh);
}

function llogaritTotaleGride(dataSource) {
    var objTotale = {
        totaliSasi: 0,
        totaliVlPaTvsh: 0,
        totaliVlShpenzuar: 0
    };

    for (var i = 0; i < dataSource.length; i++)
        for (var j = 0; j < dataSource[i].Trupi.length; j++) {
            if (dataSource[i].Trupi[j].Shperndaj == false)
                continue;
            objTotale.totaliSasi += dataSource[i].Trupi[j].Sasia;
            objTotale.totaliVlPaTvsh += dataSource[i].Trupi[j].VleftaPaTvsh;
            objTotale.totaliVlShpenzuar += dataSource[i].Trupi[j].VlereShpenzimi;
        }

    return objTotale;
}

function bejGatiGrideLlogarite(columnsKonfig) {
    defaultObject = {IdLlogaria: 0, Llogaria: null, Pershkrimi: null, Vlefta: 0 };
    var ds = [Utils.CloneObject(defaultObject)];
    pageState.gridLlogaria = new myDxDataGrid("dxDataGrid_llogaria", {
        dataSource: ds,
        showRowLines: true,
        addDeleteRowCommand: true,
        columnResizingMode: "nextColumn",
        editing: {
            mode: "cell",
            allowUpdating: true,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        sorting: { mode: "none" },
        onContentReady: function (e) {
            pageState.gridLlogaria.ShtoRreshtBosh(defaultObject, "Llogaria");
        },
        onRowUpdated: function (data) {
        vendosTotalinLlogarive();
        },
        onRowRemoved: function (data) {
            vendosTotalinLlogarive();
        }
    });
    pageState.gridLlogaria.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides
    //width sherben per klasat col-md-<width> te bootstrap
    var llogariaAutoCompleteColumns = [{ dataField: "label", capField: "Llogaria", width: 2 }, { dataField: "desc", capField: "Pershkrimi", width: 8 }, { dataField: "monedha", capField: "Monedha", width: 2 }];
    pageState.gridLlogaria.AddAutocompleteToColumn("Llogaria", "IdLlogaria", "Pershkrimi", merrLlogari, "Zgjidhni llogarine...", "value", "label", "desc", HapLupeLlogarie, onValueChangedZeri, llogariaAutoCompleteColumns);
}

function vendosTotalinLlogarive() {
    var dsLlogarite = pageState.gridLlogaria.GetData();
    var totalLlogari = 0;
    for (var i = 0; i < dsLlogarite.length; i++) {
        if (dsLlogarite[i].IdLlogaria == 0 || dsLlogarite[i].IdLlogaria == null)
            continue;
        totalLlogari += dsLlogarite[i].Vlefta;
    }
    txtVlera.SetText(totalLlogari);
}

function HapLupeLlogarie(rowIndex) {
    pageState.rreshtIndexLlogaria = rowIndex;
    var idNdermarrjeLlogarie = hfState.Get("_idNdermarrje");
    var idPerdoruesLlogarie = hfState.Get("_idPerdoruesi");
    popupUniversal.SetHeaderText("Zgjidh Llogarine");
    popupUniversal.SetContentUrl('LupaLlogaria.aspx?idNdermarrje=' + idNdermarrjeLlogarie + '&idPerdoruesi=' + idPerdoruesLlogarie);
    popupUniversal.SetSize(750, 600);
    popupUniversal.Show();
}

function merrLlogari(value, rowIndex, perAcList) {
    var deferred = new jQuery.Deferred();
    var wsData, wsURL;
    
    wsData = { infixText: value ? value : "", pershk: 1, klasa:"6"};
    wsURL = Utils.getServerApiUrl("ShperndarjeShpenzimi", "ktheACListeLlogarishSipasKlases");
        
    $.ajax({
        pritPergjigje: true,
        url: wsURL,
        data: JSON.stringify(wsData),
        success: function (result) {
            if (!perAcList) {
                var objekti = result.filter(function (item) { return item.label.toLowerCase() == value.toLowerCase() || item.desc.toLowerCase() == value.toLowerCase() })[0];
                if (!objekti) return;
                pageState.gridLlogaria.VendosVleraNeDataSource(rowIndex, "Llogaria", "IdLlogaria", "Pershkrimi", result[0], "label", "value", "desc", onValueChangedZeri);
                pageState.gridLlogaria.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}

function VendosLlogariNeGride(kodi, id, pershkrimi) {
    if (kodi.substr(0, 1) != "6") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlogariKlasaGjashte"));
        return;
    }

    var dataSource = pageState.gridLlogaria.GetData();
    dataSource[pageState.rreshtIndexLlogaria].IdLlogaria = id;
    dataSource[pageState.rreshtIndexLlogaria].Llogaria = kodi;
    dataSource[pageState.rreshtIndexLlogaria].Pershkrimi = pershkrimi;
    pageState.gridLlogaria.Refresh();
    pageState.gridLlogaria.Grida.closeEditCell();
    pageState.rreshtIndexLlogaria = null;
}

function onValueChangedZeri(rowIndex, objekti) {
    if (objekti && objekti.label && objekti.label.substr(0, 1) != "6") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlogariKlasaGjashte"));
        pageState.gridLlogaria.DeleteRow(rowIndex);
        return;
    }
}

function ruajDokumentShperndarjeShpenzimi(idStatusDok) {
    unformatoFushaDevi();
    var koka = krijoKokeDokumentShpenzimi(idStatusDok);
    var trupi = krijoTrupDokumentShpenzimi();
    var llogarite = new Array();
    if (pageState.meKontabilizim)
        llogarite = krijoLlogarite();

    var data = {
        komponente: "Shto_ShperndarjeShpenzimesh.aspx",
        kokaDokumentit: koka,
        trupiDokumentit: trupi,
        idKonfigAmbjente: cmbKonfigurimi.GetValue(),
        statusDokumenti: idStatusDok,
        kontrolloRivleresim: pageState.meRivleresim,
        menyreKontabilizimi: pageState.menyreKontabilizimi,
        llogarite: llogarite,
        iLidhur: pageState.iLidhur
    }

    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("ShperndarjeShpenzimi", "RuajShperndarjeShpenzimesh"),
        data: JSON.stringify(data)
    }).done(function (result) { SucceededRuajDokumentShperndarjeShpenzimi(result);});
}

function krijoKokeDokumentShpenzimi(idStatusDok) {
    var veprimi = $('#hfShtimModifikim').val();
    var idKokaShpenz = (pageState.kokaHapurDokumentShpenzimi && veprimi == "modifikim") ? pageState.kokaHapurDokumentShpenzimi.IdKokaShperndarjeShpenz : 0;

    var objKoka = {};
    objKoka.IdKokaShperndarjeShpenz = idKokaShpenz;
    objKoka.NrDok = txtNrDok.GetText();
    objKoka.DtDok = dateDtRegjistrimi.GetDate();
    objKoka.DtRegjistrimi = dateDtRegjistrimi.GetDate();
    objKoka.Shenime = txtShenime.GetText();
    objKoka.VleraTotale = txtVlera.GetText();
    objKoka.IdStatusDok = idStatusDok;
    objKoka.IdNdermarrje = pageState.kokaHapurDokumentShpenzimi ? pageState.kokaHapurDokumentShpenzimi.IdNdermarrje : hfState.Get("_idNdermarrje");
    objKoka.IdNdermarrjeVit = pageState.kokaHapurDokumentShpenzimi ? pageState.kokaHapurDokumentShpenzimi.IdNdermarrjeVit : hfState.Get("_idNdermarrjeVit");
    objKoka.IdPerdoruesi = pageState.kokaHapurDokumentShpenzimi ? pageState.kokaHapurDokumentShpenzimi.IdPerdoruesi : hfState.Get("_idPerdoruesi");
    objKoka.IdNivel = cmbLloji.GetValue();
    objKoka.IdKonfigAmbjente = cmbKonfigurimi.GetValue();

    return objKoka;
}

function krijoTrupDokumentShpenzimi() {
    pageState.gridTrupiShpenzimi.SaveCurrentValues();
    var dsShperndarjeShpenzimeTrupi = pageState.gridTrupiShpenzimi.GetData();
    var colShperndarjeShpenzimeTrupi = new Array();
    var clsShperndarjeShpenzimeTrupi = {};

    for (var i = 0; i < dsShperndarjeShpenzimeTrupi.length; i++) {
        clsShperndarjeShpenzimeTrupi = {};
        clsShperndarjeShpenzimeTrupi.DtDok = dsShperndarjeShpenzimeTrupi[i].DtFature;
        clsShperndarjeShpenzimeTrupi.IdTrupi = dsShperndarjeShpenzimeTrupi[i].IdTrupi;
        clsShperndarjeShpenzimeTrupi.IdKoka = dsShperndarjeShpenzimeTrupi[i].IdKoka;
        clsShperndarjeShpenzimeTrupi.IdFatura = dsShperndarjeShpenzimeTrupi[i].IdDokumenti;
        clsShperndarjeShpenzimeTrupi.LlojDok = 39;
        clsShperndarjeShpenzimeTrupi.NrDok = dsShperndarjeShpenzimeTrupi[i].NrFature;
        clsShperndarjeShpenzimeTrupi.OColTrupiFaturat = merrShperndarjeShpenzimiTrupiFaturat(dsShperndarjeShpenzimeTrupi[i]);
        colShperndarjeShpenzimeTrupi.push(clsShperndarjeShpenzimeTrupi);
    }
    return colShperndarjeShpenzimeTrupi;
}

function merrShperndarjeShpenzimiTrupiFaturat(fatura) {
    var rreshtat = fatura.Trupi;
    var colShperndarjeShpenzimeTrupiFaturat = new Array();
    var clsShperndarjeShpenzimeTrupiFaturat = {};

    for (var i = 0; i < rreshtat.length; i++) {
        clsShperndarjeShpenzimeTrupiFaturat = {};
        clsShperndarjeShpenzimeTrupiFaturat.IdArtikull = rreshtat[i].IdArtikulli;
        clsShperndarjeShpenzimeTrupiFaturat.IdTrupiShitje = rreshtat[i].IdTrupiMagazina;
        clsShperndarjeShpenzimeTrupiFaturat.Vlera = rreshtat[i].VlereShpenzimi;
        clsShperndarjeShpenzimeTrupiFaturat.Shperndaj = rreshtat[i].Shperndaj;
        colShperndarjeShpenzimeTrupiFaturat.push(clsShperndarjeShpenzimeTrupiFaturat);
    }
    return colShperndarjeShpenzimeTrupiFaturat;
}

function krijoLlogarite() {
    var dsLlogarite = pageState.gridLlogaria.GetData();
    var colShperndarjeShpenzimeLlogarite = new Array();
    var clsShperndarjeShpenzimeLlogarite = {};

    for (var i = 0; i < dsLlogarite.length; i++) {
        if (dsLlogarite[i].IdLlogaria == 0 || dsLlogarite[i].IdLlogaria == null )
            continue;
        clsShperndarjeShpenzimeLlogarite = {};
        clsShperndarjeShpenzimeLlogarite.IdLlogari = dsLlogarite[i].IdLlogaria;
        clsShperndarjeShpenzimeLlogarite.Vlefta = dsLlogarite[i].Vlefta;
        clsShperndarjeShpenzimeLlogarite.NrLlogari = dsLlogarite[i].Llogaria;
        colShperndarjeShpenzimeLlogarite.push(clsShperndarjeShpenzimeLlogarite);
    }
    return colShperndarjeShpenzimeLlogarite;
}

function callWebserviceDokumentShperndarjeShpenzimi() {
    var idDokumenti = $("#hfId").val();
    if (idDokumenti && idDokumenti > 0) {
        $.ajax({
            pritPergjigje: true,
            showLoading: true,
            url: Utils.getServerApiUrl("ShperndarjeShpenzimi", "MerrDokumentShperndarjeShpenzimi"),
            data: JSON.stringify({ idDokumenti: idDokumenti })
        }).done(function (result) { VendosDokumentShperndarjeShpenzimi(result); });
    }
}

function VendosDokumentShperndarjeShpenzimi(result) {
    var koka = result.koka;
    var trupi = result.trupi;
    var llogarite = result.llogarite;
    VendosVleraKoka(koka);
    pageState.gridTrupiShpenzimi.SetDataSource(trupi);
    if (llogarite.length > 0) {
        pageState.meKontabilizim = true;
        bejGatiGrideLlogarite(colGrida.filter(function (item) { return item.GridKokaEmri == 'gvLlogaria' }));
        pageState.gridLlogaria.SetDataSource(llogarite);
    }
    pageState.kokaHapurDokumentShpenzimi = koka;
    vendosTotaleVlera();
}

function VendosVleraKoka(koka) {
    cmbKonfigurimi.SetValue(koka.IdKonfigAmbjente);
    cmbLloji.SetValue(koka.IdNivel);
    txtNrDok.SetText(koka.NrDok);
    txtShenime.SetText(koka.Shenime);
    txtVlera.SetText(koka.VleraTotale);
    dateDtRegjistrimi.SetValue(new Date(koka.DtRegjistrimi));
    dateDtDok.SetValue(new Date(koka.DtDok));
}

function SucceededRuajDokumentShperndarjeShpenzimi(result) {
    formatoFushaDevi();
    pageState.pergjigje = result.pergjigje;

    var hf = $("#status1");
    hf.val() == result.statusi1;
    if (result.statusi1) {
        if (result.rivleresim) {
            pageState.identifikuesPyetje = "rivleresim";
            myMesazh.ShtoPyetje(result.pyetjeRivleresimi);
        }
        else {
            myMesazh.ShtoMesazhSuksesi(result.mesazh.PershkrimMesazhi);
            PastroClick();
        }
        $('#hfqkmesazhi').val(result.shfaqmesazhapolupemagazina);
        $('#hfUrl').val(result.url);
    }
    else {
        myMesazh.ShtoMesazhGabimi(result.mesazh.PershkrimMesazhi);
    }
}
/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe inicializon gridat.
*/
function popUpQendraKostoMagazina() {
    Utils.bllokoFaqe();
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        pageState.identifikuesPyetje = "shperndarjeQendraKostoMag";
        myMesazh.ShtoPyetje(hfState.Get("msgDeshironiShperndQendraKostoMag"));
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        hapPopUp();
    }
}

function hapPopUp(s, e) {
    if ($('#hfUrl').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val().split(';')[0], 900, 600);
        $('#hfUrl').val($('#hfUrl').val().replace($('#hfUrl').val().split(';')[0] + ";", ""));
        if ($('#hfUrl').val() == '')
            $('#hfqkmesazhi').val('jo');
    }
}

function bejRivleresimNgaShperndarjeShpenzimi() {
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("ShperndarjeShpenzimi", "ShperndarjeShpenzimiKryejRivleresim")
    }).done(function (result) {
        if (result.mesazh.Status)
            myFaqeCelje.kontrolloTeDrejta('ShperndarjeShpenzimesh.aspx?fshi=rivleresimpo');
        else
            myFaqeCelje.kontrolloTeDrejta('ShperndarjeShpenzimesh.aspx?fshi=rivleresimjo');
        });
}

function FshiShperndarjeShpenzimi() {
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("ShperndarjeShpenzimi", "FshiDokumentShperndarjeShpenzimi"),
        data: JSON.stringify({ idDokumenti: pageState.kokaHapurDokumentShpenzimi.IdKokaShperndarjeShpenz, kontrolloRivleresim: pageState.meRivleresim })
    }).done(function (result) {SucceededFshiShperndarjeShpenzimi(result)});
}

function SucceededFshiShperndarjeShpenzimi(result) {
    pageState.pergjigje = result.pergjigje;

    var hf = $("#status1");
    hf.val() == result.statusi1;
    if (result.statusi1) {
        if (result.rivleresim) {
            pageState.identifikuesPyetje = "rivleresim";
            myMesazh.ShtoPyetje(result.pyetjeRivleresimi);
        }
        else {
            myFaqeCelje.kontrolloTeDrejta('ShperndarjeShpenzimesh.aspx?fshi=po');
        }
    }
    else {
        myMesazh.ShtoMesazhGabimi(result.mesazh.PershkrimMesazhi);
    }
}