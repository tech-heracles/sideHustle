; $(document).ready(function (e) {
    changeName();
});


var focusedColumn;


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    gvPlanifikim.PerformCallback(3040 + ";" + cmbKonfigurimi.GetText());
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
}
    myFaqeCelje.changeNameRegjistrime(hfState.Get('komponente'), 0);
}


function SucceededCallbackMesazhi(result) {
    Utils.hiqLoadingGif();;
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
    if (hfState.Get("raportuese") || e.focusedColumn.fieldName != 'SasiaMiratuar')
        e.cancel = true;
    focusedColumn = e.focusedColumn.fieldName;
}

function EndEditing(s, e) {
    
    
    var sasiaMiratuar = e.rowValues[(s.GetColumnByField("SasiaMiratuar").index)].value;
    var sasiaTerhequr = e.rowValues[(s.GetColumnByField("SasiaTerhequr").index)].value;
    gvSetCellValue = s.batchEditApi.SetCellValue(e.visibleIndex, "SasiaMbetur", sasiaMiratuar - sasiaTerhequr);
}

function menu_click(s, e) {

    if (e.item.name == "Ruaj") {
        gvPlanifikim.UpdateEdit();


    }
    else if (e.item.name == "Pastro") {
        gvPlanifikim.CancelEdit();
        gvPlanifikim.Refresh();
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