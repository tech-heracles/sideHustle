; $(document).ready(function (e) {
    changeName();
});


var focusedColumn;


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    gvParashikimShpenzPersoneli.PerformCallback(3008 + ";" + cmbKonfigurimi.GetText());
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

function gvBeginCallback(s, e) {
    Utils.shfaqLoadingGif();;
}

function SucceededCallbackMesazhi(result) {
    Utils.hiqLoadingGif();;
    if (result && result.d)
        result = result.d;
    if (result.length == undefined)
        return;

    
    var arr = result.split(':');
    if (arr[1] == "Green") {

        myMesazh.ShtoMesazhSuksesi(arr[0]);
    }
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
    

}

function gvEndCallback(s, e) {

    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);


}
function StartEditing(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    if (e.focusedColumn.fieldName == 'PShPId'
        || e.focusedColumn.fieldName == 'FunksioniId'
        || e.focusedColumn.fieldName == 'RreshtiId'
        || e.focusedColumn.fieldName == 'Funksioni'
        || e.focusedColumn.fieldName == 'IdStatusDok'
        || e.focusedColumn.fieldName == 'IdNdermarrje'
        || e.focusedColumn.fieldName == 'IdKrijuesi'
        || e.focusedColumn.fieldName == 'IdModifikuesi'
        || e.focusedColumn.fieldName == 'DtKrijimi'
        || e.focusedColumn.fieldName == 'DtModifikimi'
        || e.focusedColumn.fieldName == 'Prindi'
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
    setTimeout(function () {
        Utils.RillogaritTotaletPerGrup(s, 2, focusedColumn);
    }, 10);
}

function menu_click(s, e) {

    if (e.item.name == "Ruaj") {
        gvParashikimShpenzPersoneli.UpdateEdit();


    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvParashikimShpenzPersoneli.CancelEdit();
        gvParashikimShpenzPersoneli.Refresh();
    }

    e.processOnServer = false;
}
function ItemClickMenu(s, e) {
    menu_click(s,e);
}
function Init(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function cmbKonfigurimiSelectedIndexChanged(s, e) {
    ndryshoKonfigurimin()
}
