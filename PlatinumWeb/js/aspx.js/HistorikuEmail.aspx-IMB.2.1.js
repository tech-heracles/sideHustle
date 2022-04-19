
jQuery(document).ready(function () {
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
    changeName();
});

function menu_click(s, e) {
    e.processOnServer = false;
    if (grid_HistorikuEmail.GetSelectedRowCount() == 0 || grid_HistorikuEmail.GetFocusedRowIndex() == -1)
    {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        return;
    }
    grid_HistorikuEmail.GetSelectedFieldValues("Statusi", OnGetRowValuesCallback);
        
}
function OnGetRowValuesCallback(result)
{
    var statusDelivery = result;
    if (statusDelivery == "Derguar") {
        myMesazh.ShtoMesazhGabimi("Ky email eshte derguar me sukses. Nuk mund te ridergohet!");
        return;
    }
    grid_HistorikuEmail.PerformCallback(grid_HistorikuEmail.GetRowKey(grid_HistorikuEmail.GetFocusedRowIndex()));

}


function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('HistorikuEmail.aspx', 0);
    window.parent.createCookie('adresa', 'HistorikuEmail.aspx', 1);
}

function EndCallbackGrida(s, e) {
    if(hfState.Get("merrMesazh") == "Po")
        $.ajax({            
            url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
            data: JSON.stringify({})
        }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    if (result != null && result !== ":" && result != undefined) {
        var arr = result.split(':');
        if (arr[1] == "Green")
            myMesazh.ShtoMesazhSuksesi(arr[0]);
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    }
}