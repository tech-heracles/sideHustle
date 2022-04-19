; $(document).ready(function (e) {
    changeName();
});


var focusedColumn;


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    gvShpenzimeOperative.PerformCallback(3009 + ";" + cmbKonfigurimi.GetText());
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
        // cmbKonfigurimi.SetText(hfKonffillestar.value);
        
        //window.parent.callWebServiceKtheInfoLart(hfState.Get('komponente'), 0);
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
    myFaqeCelje.changeName(hfState.Get('komponente'), 0);
}


function SucceededCallbackMesazhi(result) {
    Utils.hiqLoadingGif();;
    if (result && result.d)
        result = result.d;
    if (result.length == undefined)
        return;

    //if (result != null)
    {
        var arr = result.split(':');
        if (arr[1] == "Green") {

            myMesazh.ShtoMesazhSuksesi(arr[0]);
        }
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    }

}
function gvBeginCallback(s, e) {
    Utils.shfaqLoadingGif();;
}


function gvEndCallback(s, e) {

    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);


}
function StartEditing(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    if (e.focusedColumn.fieldName == 'IdPrindi'
        || e.focusedColumn.fieldName == 'TotalKerkesaTeArdhuraArdhme'
        || e.focusedColumn.fieldName == 'TotalAktuale'
        || e.focusedColumn.fieldName == 'TotalParaardhes'
        || e.focusedColumn.fieldName == 'DiferencaKerkeseLimitArdhme'
        || e.focusedColumn.fieldName == 'DiferencaKerkeseLimitArdhmePlus1'
        || e.focusedColumn.fieldName == 'DiferencaKerkeseLimitArdhmePlus2'
        || e.focusedColumn.fieldName == 'Niveli'
        || e.focusedColumn.fieldName == 'Pershkrimi'
        || e.focusedColumn.fieldName == 'ShoId'
        || e.focusedColumn.fieldName == 'ShokId'
        || e.focusedColumn.fieldName == 'RreshtiId'
        || e.focusedColumn.fieldName == 'IdStatusDok'
        || e.focusedColumn.fieldName == 'IdNdermarrje'
        || e.focusedColumn.fieldName == 'IdKrijuesi'
        || e.focusedColumn.fieldName == 'IdModifikuesi'
        || e.focusedColumn.fieldName == 'DtKrijimi'
        || e.focusedColumn.fieldName == 'DtModifikimi'
        )

        e.cancel = true;
    focusedColumn = e.focusedColumn.fieldName;
}


function EndEditing(s, e) {

    if (focusedColumn == "NjesiaAktuale" || focusedColumn == "NjesiaArdhme")
        return;
    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var summaryTotal = Utils.ktheKontroll("footer_" + focusedColumn);
    if (!summaryTotal) return;
    s.batchEditApi.SetCellValue(e.visibleIndex, "Total", parseFloat(s.batchEditApi.GetCellValue(e.visibleIndex, "Total")) + dif);


    var oldSummary = Utils.HiqPresjet(summaryTotal.GetValue());

    summaryTotal.SetValue(Utils.FormatoNumberMePresje(oldSummary + dif));



    if (focusedColumn == "NgaBuxhetiParaardhes" || focusedColumn == "NgaTeArdhuratParaardhes")
        LlogaritTotalParaardhes(s, e);

    if (focusedColumn == "NgaBuxhetiAktuale" || focusedColumn == "NgaTeArdhuratAktuale")
        LlogaritTotalAktuale(s, e);

    if (focusedColumn == "ShpenzimeTePlanifikuarTeArdhuraArdhme")
        LlogaritTotalKerkesaTeArdhuraArdhmes(s, e);

    if (focusedColumn == "KerkesaGjykatesArdhme" || focusedColumn == "LimitiArdhme") {
        LlogaritDiference(s, e);
        LlogaritTotalKerkesaTeArdhuraArdhmes(s, e);
    }
    if (focusedColumn == "KerkesaGjykatesArdhmePlus1" || focusedColumn == "LimitiArdhmePlus1")
        LlogaritDiferencePlus1(s, e);

    if (focusedColumn == "KerkesaGjykatesArdhmePlus2" || focusedColumn == "LimitiArdhmePlus2")
        LlogaritDiferencePlus2(s, e);
    setTimeout(function () {
        Utils.RillogaritTotaletPerGrup(s, 2, focusedColumn);
    }, 10);
}
function LlogaritTotalParaardhes(s, e) {

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var oldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'TotalParaardhes');

    s.batchEditApi.SetCellValue(e.visibleIndex, 'TotalParaardhes', oldValue + dif);


}

function LlogaritTotalAktuale(s, e) {

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var oldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'TotalAktuale');

    s.batchEditApi.SetCellValue(e.visibleIndex, 'TotalAktuale', oldValue + dif);

}

function LlogaritTotalKerkesaTeArdhuraArdhmes(s, e) {

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var oldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'TotalKerkesaTeArdhuraArdhme');

    s.batchEditApi.SetCellValue(e.visibleIndex, 'TotalKerkesaTeArdhuraArdhme', focusedColumn == "LimitiArdhme" ? oldValue - dif : oldValue + dif);

}

function LlogaritDiference(s, e) {

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var oldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'DiferencaKerkeseLimitArdhme');

    s.batchEditApi.SetCellValue(e.visibleIndex, 'DiferencaKerkeseLimitArdhme', focusedColumn == "LimitiArdhme" ? oldValue - dif : oldValue + dif);

}

function LlogaritDiferencePlus1(s, e) {

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var oldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'DiferencaKerkeseLimitArdhmePlus1');

    s.batchEditApi.SetCellValue(e.visibleIndex, 'DiferencaKerkeseLimitArdhmePlus1', focusedColumn == "LimitiArdhmePlus1" ? oldValue - dif : oldValue + dif);

}

function LlogaritDiferencePlus2(s, e) {

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var oldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'DiferencaKerkeseLimitArdhmePlus2');

    s.batchEditApi.SetCellValue(e.visibleIndex, 'DiferencaKerkeseLimitArdhmePlus2', focusedColumn == "LimitiArdhmePlus2" ? oldValue - dif : oldValue + dif);

}

function menu_click(s, e) {

    if (e.item.name == "Ruaj") {
        gvShpenzimeOperative.UpdateEdit();
        if (!hfState.Get('modifikuarNrCeshtjesh')) 
            gvShpenzimeOperative.PerformCallback("ceshtjet");
        else 
            hfState.Get('modifikuarNrCeshtjesh', false);

    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvShpenzimeOperative.CancelEdit();
        gvShpenzimeOperative.Refresh();
    }

    e.processOnServer = false;
}
function ItemClickMenu(s, e) {
    menu_click(s,e);
}
function MenuInfoInit(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function cmbKonfigurimiSelectedIndexChanged(s, e) {
    ndryshoKonfigurimin()
}