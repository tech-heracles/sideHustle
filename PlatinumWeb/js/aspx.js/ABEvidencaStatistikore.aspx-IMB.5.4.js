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
    myMenu.aplikoFiltra(s, e, gvListaEvidencat, hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
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
    myMenu.menu_click_batchEdit(s, e, $('#hfShtimModifikim'), $('#hfId'), PageControl, gvEvidenca, false, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi, $("#hfStatusDokumenti"));
    e.processOnServer = false;
}
function valido(s, e) {

    return CustomValidation(s, e) && myFaqeCelje.validoKontrolleDheGriden(s, e, PageControl, gvEvidenca, hfTeDrejta, $('#hfShtimModifikim'));
}

function CustomValidation(s, e) {
    //  gvEvidenca = new ASPxClientGridView();
    var kolona = gvEvidenca.GetColumnById("IdProfesioni");
    //for (var i = 0, rreshtat = gvEvidenca.GetVisibleRowsOnPage() ; i < rreshtat;i++)
    //{
    //    gvEvidenca.batchEditApi.ValidateRow(i);
    //    gvEvidenca.batchEditApi.StartEdit(i, kolona.index);
    //}

    if (gvEvidenca.GetVisibleRowsOnPage() == 0) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te ruhet dokumenti me trup bosh!");
        return false;
    }
    if (txtGjyqtarPlan.GetValue() < 0) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te vendosen vlera negative per numrin e gjyqtareve plan! ");
        return false;
    }
    if (!gvEvidenca.batchEditApi.ValidateRows()) {
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
    indexModifiko = gvListaEvidencat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje pasqyre!');
    else
        gvListaEvidencat.GetRowValues(indexModifiko, 'IdKokaDok;TreMujori;GjyqtarPlan;IdStatusDok', OnGetRowValuesMod);
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
    txtGjyqtarPlan.SetValue(values[2]);
    IdStatusDok = values[3];
    ///te futen ne callbackpanel qe te mos perdorim dy callback
    gvEvidenca.PerformCallback(values[0]);
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
    cmbPeriudha.SetValue("");
    txtGjyqtarPlan.SetValue(0);
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    gvEvidenca.PerformCallback(-1);
    IdStatusDok = undefined;
    // AktivizoDraft();
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({      
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    gvListaEvidencat.PerformCallback(idKomp + ";" + kodKonf);
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
    if (!result.d && !result)
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

        $("#divFillim").hide();
        // $("#divGrida").width($("#tblPasqyra").width() + "%");
        $("#divGrida").show();
        $("#divFundi").show();

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
        cmbPeriudha.SetEnabled(true);
        cmbPeriudha.SetValue(null);
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvListaEvidencat, hfState.Get("idKomponente"), pastrofusha, hfTeDrejta)
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
    indexModifiko = gvListaEvidencat.GetFocusedRowIndex();
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
    if (result == undefined)
        return;
    if (result && result.d)
        result = result.d;
    if (result.length == undefined)
        return;

    
    var arr = result.split(':');
    if (arr[1] == "Green") {
        myMesazh.ShtoMesazhSuksesi(arr[0]);
        gvListaEvidencat.Refresh();
        PageControl.SetActiveTabIndex(0);
    }
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
    
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
//function rowValidation(s, e) {
//    if (e.visibleIndex == (s.GetVisibleRowsOnPage() - 1))//rreshti i fundit
//        return;
//    for (var col in e.validationInfo) {
//        if (col.value == null) {
//            col.isvalid = false;
//            col.errorText = "Vlera nuk mund te jete bosh!";
//        }
//    }
//}

function rowValidation(s, e) {


    var vleraFakt = s.GetColumnById("NumriGjithsej");
    if (vleraFakt != undefined) {
        if (e.validationInfo[vleraFakt.index].value < 0) {
            e.validationInfo[vleraFakt.index].isValid = false;
            e.validationInfo[vleraFakt.index].errorText = "Numri i ceshtjeve gjithsej nuk mund te jete negativ!";
        }
    }

    var vleraPlan = s.GetColumnById("NumriPerfunduar");
    if (vleraPlan != undefined) {
        if (e.validationInfo[vleraPlan.index].value < 0) {
            e.validationInfo[vleraPlan.index].isValid = false;
            e.validationInfo[vleraPlan.index].errorText = "Numri i ceshtjeve te perfunduara nuk mund te jete negativ!";
        }
    }
}

function startEditing(s, e) {
    editingIndex = e.visibleIndex;//rreshti qe po editohet
}

function StartEditing(s, e) {
    focusedColumn = e.focusedColumn.fieldName;
    if (focusedColumn != 'NumriGjithsej' && focusedColumn != 'NumriPerfunduar')
        e.cancel = true;
}

function EndEditing(s, e) {

    //if (focusedColumn == 'NumriGjithsej' || focusedColumn == 'NumriPerfunduar') {
        var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
        var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
        var dif = newValue - originalValue;

        var summaryTotal = Utils.ktheKontroll("footer_" + focusedColumn);
        if (!summaryTotal) return;

        var oldSummary = Utils.HiqPresjet(summaryTotal.GetValue());

        summaryTotal.SetValue(Utils.FormatoNumberMePresje(oldSummary + dif));
    //}
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
function gvListaEvidencatRowDblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex);
    kaloTab=true; 
}
function gvListaEvidencatFocusedRowChanged(s, e) {
    mbush=true;
}
function gvListaEvidencatBeginCallback(s, e) {
    BeginCallback(s,e);
}

