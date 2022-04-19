;
var focusedColumn;

$(document).ready(function (e) {
    changeName();
});

function ndryshoAmbjentin() {
    //lblAmbjenti.SetText(cmbAmbjenti.GetText());
    ////cmbAmbjenti.SetText(cmbAmbjenti.GetText().split(';')[0]);

    //gvKonfiguroShpenzimeOperative.PerformCallback(3001 + ";" + cmbAmbjenti.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    if (hf !== null) {
     
    //    cmbAmbjenti.SetText(hf.value.split(';')[0]);
        //cmbAmbjenti.SetValue(hfKonffillestar.value);
        //lblAmbjenti.SetText(cmbAmbjenti.GetText());
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
    myFaqeCelje.changeName("ABKonfiguroShpenzimeOperative.aspx", 0, hf);
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
}

function gvEndCallback(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function StartEditing(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    focusedColumn = e.focusedColumn.fieldName;
    if (focusedColumn == 'Niveli')
        e.cancel = true;
}

function EndEditing(s, e) {
    //gvKonfiguroShpenzimeOperative = new ASPxClientGridView();

    if (focusedColumn == "IdPrindi") {

        //  var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, focusedColumn);
        var idPrindi = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
        $.ajax({

            url: Utils.getServerApiUrl("AnalizeBuxheti", "MerrNivelinEShpenzimitOperativ"),
            data: JSON.stringify({ idPrindi: idPrindi })

        }).done(function (result) {

            s.batchEditApi.SetCellValue(e.visibleIndex, "Niveli", result);

        });
    }

}

function rowValidation(s, e) {
    if (e.validationInfo[2].value == undefined || e.validationInfo[2].value == null) {
        e.validationInfo[2].isValid = false;
        e.validationInfo[2].errorText = "Kodi nuk mund te jete bosh!";
    }
    
}

function menu_click(s, e) {
    if (e.item.name == "Shto") {
        gvKonfiguroShpenzimeOperative.AddNewRow();
    }
    else if (e.item.name == "Fshi") {
        var keys = new Array().map
        keys = gvKonfiguroShpenzimeOperative.GetSelectedKeysOnPage();
        $.map(keys, function (key) {
            KontrolloRreshtin(key);
        });
       // gvKonfiguroShpenzimeOperative.UpdateEdit();
    }

    else if (e.item.name == "Ruaj") {
       // Utils.shfaqLoadingGif();;
        gvKonfiguroShpenzimeOperative.UpdateEdit();
    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvKonfiguroShpenzimeOperative.CancelEdit();
    }
    else if (e.item.name == "Riruaj") {
        Utils.shfaqLoadingGif();;
        gvKonfiguroShpenzimeOperative.CancelEdit();
        gvKonfiguroShpenzimeOperative.PerformCallback("Riruaj");
    }
    e.processOnServer = false;
}

function KontrolloRreshtin(shokID) {
    $.ajax({
        url: Utils.getServerApiUrl("AnalizeBuxheti", "KontrolloNeseMundTeFshihetShpenzimiKonfig"),
        data: JSON.stringify({ shokID: shokID })
    }).done(function (result) {
        if (result && !result.Status) {
            myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
        }
        else {
            gvKonfiguroShpenzimeOperative.DeleteRowByKey(shokID);
        }

    });
}
function ItemClickMenu(s, e) {
    menu_click(s,e);
}
function gvInit(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}