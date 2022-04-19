; $(document).ready(function (e) {
    changeName();
});


var focusedColumn;


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    gvKonfigParashikimShpenz.PerformCallback(3006 + ";" + cmbKonfigurimi.GetText());
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
    if (e.focusedColumn.fieldName == 'Emertimi'
        || e.focusedColumn.fieldName == 'IdAuto' 
        || e.focusedColumn.fieldName == 'PShPConfigId'
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


function menu_click(s, e) {
    
    if (e.item.name == "Ruaj") {
        //Utils.shfaqLoadingGif();;
        //gvKonfigParashikimShpenz.AddNewRow();
        gvKonfigParashikimShpenz.UpdateEdit();
        //gvKonfigParashikimShpenz.CancelEdit();
    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvKonfigParashikimShpenz.CancelEdit();
    }
    else if (e.item.name == "Riruaj")
    {
        Utils.shfaqLoadingGif();;
        gvKonfigParashikimShpenz.CancelEdit();
        gvKonfigParashikimShpenz.PerformCallback("Riruaj");
    }

    e.processOnServer = false;
}

function ItemClickMenu(s, e)
{
    menu_click(s,e);
}
function Init(s, e)
{
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function cmbKonfigurimiSelectedIndexChanged(s, e)
{
    ndryshoKonfigurimin()
}
