; $(document).ready(function (e) {
    changeName();
});


var focusedColumn;


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    gvinventariPerdorues.PerformCallback(3018 + ";" + cmbKonfigurimi.GetText());
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
    myFaqeCelje.changeName(hfState.Get('komponente'), 0, hf);
}


function SucceededCallbackMesazhi(result) {
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
    Utils.hiqLoadingGif();;

}

function gvEndCallback(s, e) {

    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);


}
function StartEditing(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    if (e.focusedColumn.fieldName == 'Artikulli'
        || e.focusedColumn.fieldName == 'InventariId'
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
    s.batchEditApi.SetCellValue(e.visibleIndex, "Total", parseFloat(s.batchEditApi.GetCellValue(e.visibleIndex, "Total")) + dif);


    var oldSummary = Utils.HiqPresjet(summaryTotal.GetValue());

    summaryTotal.SetValue(Utils.FormatoNumberMePresje(oldSummary + dif));

   
}

function menu_click(s, e) {

    if (e.item.name == "Ruaj") {
        gvinventariPerdorues.UpdateEdit();


    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvinventariPerdorues.CancelEdit();
        gvinventariPerdorues.Refresh();
    }

    e.processOnServer = false;
}
function ItemClickMenu(s, e) {
    menu_click(s,e);
}
function gvInit(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function gvSelectedIndexChanged(s, e) {
    ndryshoKonfigurimin()
}