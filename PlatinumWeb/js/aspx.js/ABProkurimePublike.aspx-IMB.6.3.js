; $(document).ready(function (e) {
    changeName();
});
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
var IdStatusDok = undefined;
var editingIndex = undefined;
var focusedColumn;
var resultkonf;
var colKontrollet, colAtrTrupi;

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvListaProkurimet, hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    mbushfusha();
}

function menu_click(s, e) {
    myMenu.menu_click_batchEdit(s, e, $('#hfShtimModifikim'), $('#hfId'), PageControl, gvProkurimet, false, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi, $("#hfStatusDokumenti"));
    e.processOnServer = false;
}
function valido(s, e) {

    return CustomValidation(s, e) && myFaqeCelje.validoKontrolleDheGriden(s, e, PageControl, gvProkurimet, hfTeDrejta, $('#hfShtimModifikim'));
}

function CustomValidation(s, e) {
   
    if (gvProkurimet.GetVisibleRowsOnPage() == 0) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te ruhet dokumenti me trup bosh!");
        return false;
    }
    if (txtNrDok.Text == "") {
        myMesazh.ShtoMesazhGabimi("Nuk mund te ruhet numri i dokumentit bosh! ");
        return false;
    }
    if (!gvProkurimet.batchEditApi.ValidateRows()) {
        myMesazh.ShtoMesazhGabimi("Trupi i dokumentit nuk eshte i rregullt!");
        return false;
    }
    return true;
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvListaProkurimet.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje pasqyre!');
    else
        gvListaProkurimet.GetRowValues(indexModifiko, 'IdKokaRp;KaterMujori;NrDok;IdStatusDok', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        cmbPeriudha.SetEnabled(false);
    } else {
        cmbPeriudha.SetEnabled(true);
    }

    cmbPeriudha.SetValue(values[1]);
    txtNrDok.SetValue(values[2]);
    IdStatusDok = values[3];
    gvProkurimet.PerformCallback(values[0]);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        ASPxMenu1.AdjustControl();
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
        // AktivizoDraft();
    }

    Utils.hiqLoadingGif();;
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    cmbPeriudha.SetValue(Math.floor((new Date()).getMonth() / 4) + 1);
    txtNrDok.SetValue("");
    
    gvProkurimet.PerformCallback(-1);
    IdStatusDok = undefined;
    // AktivizoDraft();
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({      
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    gvListaProkurimet.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvDetyra").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    
    if (result == null || (!result.d && !result))
        return;
    if (result && result.d)
        result = result.d;
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        //  LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblPasqyra'];
        var arrPrind = ['divFillim'];

        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));

        $("#divFillim").show();
        // $("#divGrida").width($("#tblPasqyra").width() + "%");
        $("#divGrida").show();
        //$("#divFundi").show();

        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoDet.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
            IdStatusDok = 0;
        }
        //AktivizoDraft();
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    if (hfMod.val() != "klonim") {
      //  cmbPeriudha.SetEnabled(true);
      //  cmbPeriudha.SetValue(null);
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
    myFaqeCelje.changeName(hfState.Get('komponente'), 0, hf);
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusVeprimi");
    var colKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvListaProkurimet, hfState.Get("idKomponente"), pastrofusha, hfTeDrejta)
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function PageControlTabChanging(s, e) {
    indexModifiko = gvListaProkurimet.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);

            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));

    ASPxMenu1.AdjustControl();
}

function gvEndCallback(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}
function SucceededCallbackMesazhi(result) {
    if (result == null)
        return;
    if (result && result.d)
        result = result.d;
    if (result.length == undefined)
        return;

    //if (result != null)
    {
        var arr = result.split(':');
        if (arr[1] == "Green") {
            myMesazh.ShtoMesazhSuksesi(arr[0]);
            gvListaProkurimet.Refresh();
            PageControl.SetActiveTabIndex(0);
        }
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    }
    Utils.hiqLoadingGif();;
}
function AktivizoDraft() {
    if (PageControl.GetActiveTabIndex() == 1) {
        if (IdStatusDok == 1)
            ASPxMenu1.GetItemByName("Draft").SetEnabled(false)
        else
            ASPxMenu1.GetItemByName("Draft").SetEnabled(true)
    }
}


function rowValidation(s, e) {


    var vleraLimit = s.GetColumnById("FondiLimit");
    if (vleraLimit != undefined) {
        if (e.validationInfo[vleraLimit.index].value < 0) {
            e.validationInfo[vleraLimit.index].isValid = false;
            e.validationInfo[vleraLimit.index].errorText = "Fondi limit nuk mund te jete negativ!";
        }
    }

    var vleraKontrates = s.GetColumnById("VleraKontrates");
    if (vleraKontrates != undefined) {
        if (e.validationInfo[vleraKontrates.index].value < 0) {
            e.validationInfo[vleraKontrates.index].isValid = false;
            e.validationInfo[vleraKontrates.index].errorText = "Vlera e kontrates nuk mund te jete negative!";
        }
    }
}

function startEditing(s, e) {
    editingIndex = e.visibleIndex;//rreshti qe po editohet
}

function StartEditing(s, e) {
    focusedColumn = e.focusedColumn.fieldName;
    if (focusedColumn != 'VleraKontrates'
         && focusedColumn != 'FondiLimit'
         && focusedColumn != 'LlojProcedure'
         && focusedColumn != 'KoheTenderi'
         && focusedColumn != 'OperatoriEkonomik')
        e.cancel = true;
}

function EndEditing(s, e) {

   
}
function gvItemClick(s, e) {
    menu_click(s,e);
}
function ClickOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function ClickCancel(s, e) {
    popFshi.Hide();
}
function gvSelectedIndexChanged(s, e) {
    ndryshoKonfigurimin()
}
function gvListaProkurimetRowDblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex);
    kaloTab=true; 
}
function gvListaProkurimetFocusedRowChanged(s, e) {
    mbush=true;
}
function gvListaProkurimetBeginCallback(s, e) {
    BeginCallback(s,e);
}

