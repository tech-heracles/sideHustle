;
var focusedColumn;
var ambjentiIZgjedhur = 1;

$(document).ready(function (e) {
    changeName();
    ambjentiIZgjedhur = 1;
});

function PageControlTabChanging(s, e) {
    var activeIndex = Tabs.GetActiveTabIndex();
    if (activeIndex == 0) {
        
        cmbAmbjenti.SetValue(ambjentiIZgjedhur);
        lblAmbjenti.SetText(cmbAmbjenti.GetText());
        gvKonfiguroFusha.PerformCallback(3001 + ";" + cmbAmbjenti.GetText());
    }
    else if (activeIndex == 1 || activeIndex == 2) {
        cmbAmbjenti.SetValue(activeIndex + 2);
        lblAmbjenti.SetText(cmbAmbjenti.GetText());
    }
    else {
        cmbAmbjenti.SetValue(12);
        lblAmbjenti.SetText(cmbAmbjenti.GetText());
    }
 
}

function gvSelectedIndexChanged(s, e) {
    ndryshoAmbjentin()
}

function ndryshoAmbjentin() {
    lblAmbjenti.SetText(cmbAmbjenti.GetText());
    //cmbAmbjenti.SetText(cmbAmbjenti.GetText().split(';')[0]);
    var amb = cmbAmbjenti.GetValue();
    if (amb == 4 || amb == 3) {
        Tabs.SetActiveTabIndex(amb - 2);
    } else if (amb == 12) {
        Tabs.SetActiveTabIndex(3);
    } else {
        ambjentiIZgjedhur = amb;
        Tabs.SetActiveTabIndex(0);
    }
    gvKonfiguroFusha.PerformCallback(3001 + ";" + cmbAmbjenti.GetText());
}

function GetSelectedCode(cmb) {
    return cmb.GetSelectedItem(cmb.GetSelectedIndex()).texts[0];
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    if (hf !== null) {
        cmbAmbjenti.SetValue(hfKonffillestar.value);
        lblAmbjenti.SetText(cmbAmbjenti.GetText());
    }
    myFaqeCelje.changeName(hfState.Get('komponente'), 0);
}

function EndRequestHandler(sender, args) { }

function gvBeginCallback(s, e) {
    Utils.shfaqLoadingGif();;
}
function SucceededCallbackMesazhi(result) {
    Utils.hiqLoadingGif();;
    if (result == null)
        return;
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
    //if (e.focusedColumn.fieldName == 'Emertimi' || e.focusedColumn.fieldName == 'Total')
    //	e.cancel = true;
}
function rowValidation(s, e) {
    if (e.validationInfo[2].value == undefined || e.validationInfo[2].value == null) {
        e.validationInfo[2].isValid = false;
        e.validationInfo[2].errorText = "Kodi nuk mund te jete bosh!";
    }
    if (e.validationInfo[3].value == undefined || e.validationInfo[3].value == null) {
        e.validationInfo[3].isValid = false;
        e.validationInfo[3].errorText = "Pershkrimi nuk mund te jete bosh!";
    }
}
function EndEditing(s, e) {
}

function menu_click(s, e) {
    var activeIndex = Tabs.GetActiveTabIndex();
    switch (activeIndex)
    {
        case 0:
            menu_click_konfiguroFusha(s, e);
            break;
        case 1:
            menu_click_konfiguroParashikimShpenzimesh(s, e);
            break;
        case 2:
            menu_click_konfiguroShpenzimmeOperative(s, e);
            break;
        case 3:
            menu_click_konfiguroZeraProkurimesh(s, e);
            break;
    }
}

function menu_click_konfiguroFusha(s, e) {
    
    if (e.item.name == "Shto") {
        gvKonfiguroFusha.AddNewRow();
    }
    else if (e.item.name == "Fshi") {
        var keys = new Array().map
        keys = gvKonfiguroFusha.GetSelectedKeysOnPage()
        var idAmb = cmbAmbjenti.GetValue();

        $.map(keys, function (key) {
            KontrolloRreshtin(key,idAmb);
        });
       // gvKonfiguroFusha.UpdateEdit();
    }

    else if (e.item.name == "Ruaj") {
      //  Utils.shfaqLoadingGif();;

        gvKonfiguroFusha.UpdateEdit();
    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvKonfiguroFusha.CancelEdit();
    }
    else if (e.item.name == "Riruaj") {
        Utils.shfaqLoadingGif();;
        gvKonfiguroFusha.CancelEdit();
        gvKonfiguroFusha.PerformCallback("Riruaj");
    }
    e.processOnServer = false;
}

function menu_click_konfiguroShpenzimmeOperative(s, e) {
    if (e.item.name == "Shto") {
        gvKonfiguroShpenzimeOperative.AddNewRow();
    }
    else if (e.item.name == "Fshi") {
        var keys = new Array().map
        keys = gvKonfiguroShpenzimeOperative.GetSelectedKeysOnPage();
        $.map(keys, function (key) {
            KontrolloRreshtinShpenzimeOperative(key);
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


function menu_click_konfiguroParashikimShpenzimesh(s, e) {
    if (e.item.name == "Shto") {
        gvKonfigParashikimShpenzimesh.AddNewRow();
    }
    else if (e.item.name == "Fshi") {
        var keys = new Array().map
        keys = gvKonfigParashikimShpenzimesh.GetSelectedKeysOnPage();
        $.map(keys, function (key) {
            KontrolloRreshtinParashikimShpenzimesh(key);
        });
        //gvKonfigParashikimShpenzimesh.UpdateEdit();
    }

    else if (e.item.name == "Ruaj") {
        //Utils.shfaqLoadingGif();;
        //gvKonfigParashikimShpenz.AddNewRow();
        gvKonfigParashikimShpenzimesh.UpdateEdit();
        //gvKonfigParashikimShpenz.CancelEdit();
    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvKonfigParashikimShpenzimesh.CancelEdit();
    }
    else if (e.item.name == "Riruaj") {
        Utils.shfaqLoadingGif();;
        gvKonfigParashikimShpenzimesh.CancelEdit();
        gvKonfigParashikimShpenzimesh.PerformCallback("Riruaj");
    }

    e.processOnServer = false;
}

function menu_click_konfiguroZeraProkurimesh(s, e) {
    if (e.item.name == "Shto") {
        gvKonfiguroZeraProkurimesh.AddNewRow();
    }
    else if (e.item.name == "Fshi") {
        var keys = new Array().map
        keys = gvKonfiguroZeraProkurimesh.GetSelectedKeysOnPage();
        $.map(keys, function (key) {
            KontrolloRreshtinZeraProkurimesh(key);
        });
        
    }

    else if (e.item.name == "Ruaj") {
        // Utils.shfaqLoadingGif();;
        gvKonfiguroZeraProkurimesh.UpdateEdit();
    }
    else if (e.item.name == "AnulloNdryshimet") {
        gvKonfiguroZeraProkurimesh.CancelEdit();
    }
    else if (e.item.name == "Riruaj") {
        Utils.shfaqLoadingGif();;
        gvKonfiguroZeraProkurimesh.CancelEdit();
        gvKonfiguroZeraProkurimesh.PerformCallback("Riruaj");
    }
    e.processOnServer = false;
}


function KontrolloRreshtin(idRreshti,idAmbjenti) {
    $.ajax({
        url: Utils.getServerApiUrl("AnalizeBuxheti", "KaVeprimeMeKeteFushe"),
        data: JSON.stringify({ idAmbjenti: idAmbjenti, idRreshti: idRreshti })
    }).done(function (result) {
        if (result && result.KaVeprime) {
            myMesazh.ShtoMesazhGabimi("Me kete rresht ka veprime!");
        }
        else {
            gvKonfiguroFusha.DeleteRowByKey(idRreshti);
        }

    });
}


function StartEditingShpenzimeOperative(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    focusedColumn = e.focusedColumn.fieldName;
    if (focusedColumn == 'Niveli')
        e.cancel = true;
}

function EndEditingShpenzimeOperative(s, e) {
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

function rowValidationShpenzimeOperative(s, e) {
    if (e.validationInfo[2].value == undefined || e.validationInfo[2].value == null) {
        e.validationInfo[2].isValid = false;
        e.validationInfo[2].errorText = "Kodi nuk mund te jete bosh!";
    }

}


function KontrolloRreshtinShpenzimeOperative(shokID) {
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

//parashikim shpenzimesh

function StartEditingParashikimShpenzimesh(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    if (   e.focusedColumn.fieldName == 'IdAuto'
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

function KontrolloRreshtinParashikimShpenzimesh(idRreshti) {

    $.ajax({
        url: Utils.getServerApiUrl("AnalizeBuxheti", "KaVeprimeMeKeteParashikimShpenzimi"),
        data: JSON.stringify({ idRreshti: idRreshti })
    }).done(function (result) {
        if (result && result.KaVeprime) {
            myMesazh.ShtoMesazhGabimi("Me kete rresht ka veprime!");
        }
        else {
            gvKonfigParashikimShpenzimesh.DeleteRowByKey(idRreshti);
        }

    });
    $.ajax({
        url: Utils.getServerApiUrl("AnalizeBuxheti", "EshtePerdorurPrindiParashikimShpenzimesh"),
        data: JSON.stringify({ idRreshti: idRreshti })
    }).done(function (result) {
        if (result && result.KaFemije) {
            myMesazh.ShtoMesazhGabimi("Me kete rresht ka zera te lidhur!");
        }
        else {
            gvKonfigParashikimShpenzimesh.DeleteRowByKey(idRreshti);
        }

    });
}

function KontrolloRreshtinZeraProkurimesh(rpkId) {
    $.ajax({
        url: Utils.getServerApiUrl("AnalizeBuxheti", "KontrolloNeseMundTeFshihetZeriIProkurimit"),
        data: JSON.stringify({ rpkId: rpkId })
    }).done(function (result) {
        if (result && !result.Status) {
            myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
        }
        else {

            gvKonfiguroZeraProkurimesh.DeleteRowByKey(rpkId);
        }

    });
}

function StartEditingZeraProkurimesh(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    focusedColumn = e.focusedColumn.fieldName;
    if (focusedColumn == 'Niveli')
        e.cancel = true;
}

function EndEditingZeraProkurimesh(s, e) {

    if (focusedColumn == "IdPrindi") {

        var idPrindi = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
        $.ajax({

            url: Utils.getServerApiUrl("AnalizeBuxheti", "MerrNivelinEZeritTeProkurimeve"),
            data: JSON.stringify({ idPrindi: idPrindi })

        }).done(function (result) {

            s.batchEditApi.SetCellValue(e.visibleIndex, "Niveli", result);

        });
    }

}

function rowValidationZeraProkurimesh(s, e) {
    if (e.validationInfo[2].value == undefined || e.validationInfo[2].value == null) {
        e.validationInfo[2].isValid = false;
        e.validationInfo[2].errorText = "Kodi nuk mund te jete bosh!";
    }

}



function ItemClickMenu(s, e) {
    menu_click(s,e);
}
function gvInit(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
