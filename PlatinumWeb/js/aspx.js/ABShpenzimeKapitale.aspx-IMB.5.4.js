; $(document).ready(function (e) {
    changeName();
});


var focusedColumn;


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    gvShpenzimeKapitale.PerformCallback(3003 + ";" + cmbKonfigurimi.GetText());
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
    if (e.focusedColumn.fieldName == 'Emertimi'
        || e.focusedColumn.fieldName == 'TotaliArdhme'
        || e.focusedColumn.fieldName == 'ShKId'
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

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;






    var summaryTotal = Utils.ktheKontroll("footer_" + focusedColumn);
    if (!summaryTotal) return;
    var oldSummary = Utils.HiqPresjet(summaryTotal.GetValue());
    
    
    summaryTotal.SetValue(Utils.FormatoNumberMePresje(oldSummary + dif));

    if (focusedColumn == "ShpenzKapitalePatrupezuarArdhme" || focusedColumn == "ShpenzKapitaleTrupezuarArdhme" || focusedColumn == "TransferimKapitalArdhme") 
        LlogaritTotalinPerTeArdhmen(s, e);
}

function LlogaritTotalinPerTeArdhmen(s, e) {

    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
    var newValue = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
    var dif = newValue - originalValue;

    var oldValue = s.batchEditApi.GetCellValue(e.visibleIndex, 'TotaliArdhme');

    s.batchEditApi.SetCellValue(e.visibleIndex,'TotaliArdhme', oldValue + dif);


}

function menu_click(s, e) {

    if (e.item.name == "Ruaj") {
        gvShpenzimeKapitale.UpdateEdit();


    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvShpenzimeKapitale.CancelEdit();
        gvShpenzimeKapitale.Refresh();
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