
var focusedColumn;
//var merrNgaSessioni = false;
; $(document).ready(function (e) {
    changeName();
    myMesazh.eshteLupe = true;
});
$(document).keydown(function (e) {//po
    switch (e.which) {
        case 13:
            e.preventDefault();
            break;
    }
});


function ndryshoAmbjentin() {

    gvFazat.PerformCallback(3001 + ";" + 0);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    if (hf !== null) {


    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(myMesazh.EndRequestTimer);
}

function SucceededCallbackMesazhi(result) {
    Utils.hiqLoadingGif();;
    if ((result == null) || (result == undefined) || (result.length == 0))
        return;
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
    }).done(SucceededCallbackMesazhi);
}

function StartEditing(s, e) {
    //ndalon kolonat qe nuk duhet te editohen
    //if (e.focusedColumn.fieldName == 'Dite') 
    //       e.cancel = true;

    focusedColumn = e.focusedColumn.fieldName;
  //  if (e.focusedColumn.fieldName == 'Data') {
   //     var nowdate = new Date();
   //     if (e.rowValues[(s.GetColumnByField(focusedColumn).index)].value == null || e.rowValues[(s.GetColumnByField(focusedColumn).index)].value == undefined)
    //        e.rowValues[(s.GetColumnByField(focusedColumn).index)].value = nowdate;
   // }

}
function rowValidation(s, e) {

    var vleraFakt = s.GetColumnById("Pershkrimi");
    if (vleraFakt != undefined) {
        if (e.validationInfo[vleraFakt.index].value == undefined || e.validationInfo[vleraFakt.index].value == null) {
            e.validationInfo[vleraFakt.index].isValid = false;
            e.validationInfo[vleraFakt.index].errorText = "Pershkrimi nuk mund te jete bosh!";
        }
    }
    vleraFakt = s.GetColumnById("Vlera");
    if (vleraFakt != undefined) {
        if (e.validationInfo[vleraFakt.index].value == undefined || e.validationInfo[vleraFakt.index].value == null) {
            e.validationInfo[vleraFakt.index].isValid = false;
            e.validationInfo[vleraFakt.index].errorText = "Vlera nuk mund te jete bosh!";
        }
    }
    vleraFakt = s.GetColumnById("Dite");
    if (e.validationInfo[vleraFakt.index].value != undefined || e.validationInfo[vleraFakt.index].value != null)
        return;
    else

    vleraFakt = s.GetColumnById("Data");
    if (vleraFakt != undefined) {
        if (e.validationInfo[vleraFakt.index].value == undefined || e.validationInfo[vleraFakt.index].value == null) {
            e.validationInfo[vleraFakt.index].isValid = false;
            e.validationInfo[vleraFakt.index].errorText = "Data nuk mund te jete bosh!";
        }
    }



}
function CustomButtonsClick(s, e) {

    if (e.buttonID == 'btnDelete') {
        var key = s.GetRowKey(e.visibleIndex);
        if (key == undefined)
            s.DeleteRow(e.visibleIndex);
        else {
            Utils.shfaqLoadingGif();;
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "EkzistonFazaKontrates"),
                data: JSON.stringify({ idNdermarrje: hfState.Get("idNdermarrje"), idKontrata: $("#hfId").val(), idFaza: key })
            }).done(function (data) {
                if (!data && data == false)
                    s.DeleteRowByKey(key);
                else {
                    myMesazh.ShtoMesazhGabimi("Kjo faze nuk mund te fshihet sepse eshte me statusin ruajtur!");
                }
            }).always(function () {
                Utils.hiqLoadingGif();;
            });
            //kontrollo nese ka veprime me rreshtin
        }
    }
}
function EndEditing(s, e) {
  
    
    setTimeout(function () {
        if (focusedColumn == 'Data') {
            var ditet;
            var datafazes = e.rowValues[(s.GetColumnByField(focusedColumn).index)].value;
            d1 = new Date(datafazes);
            d2 = window.parent.data_DateEdit.GetDate();
            ditet = DateDiff.inDays(d2, d1);
            s.batchEditApi.SetCellValue(e.visibleIndex, "Dite", ditet);
        }
        else
            if (focusedColumn == 'Dite') {
                var dite = parseInt(e.rowValues[(s.GetColumnByField(focusedColumn).index)].value);
                if (dite == undefined || dite == null || isNaN(dite))
                    return;
                d2 = window.parent.data_DateEdit.GetDate();
                d1 = new Date();
                d1.setDate(d2.getDate() + dite);

            s.batchEditApi.SetCellValue(e.visibleIndex, "Data", d1);
        }
    }, 10);


}
function menu_click(s, e) {
    switch (e.item.name) {
        case "Shto":
            e.processOnServer = false;
            gvFazat.AddNewRow();
            window.parent.$('#hfMerrFazaNgaSesioni').val('true');
            break;


        case "Ruaj":
            e.processOnServer = false;
            gvFazat.UpdateEdit();
            window.parent.$('#hfMerrFazaNgaSesioni').val('true');

            break;

        case "Anullo":
            if (gvFazat.batchEditApi.HasChanges()) {
                e.processOnServer = false;
                myMesazh.ShtoPyetje("A jeni te sigurt qe doni te mbyllni dritaren pa ruajtur ndryshimet?");
            }
            else {
                e.processOnServer = false;
                window.parent.popupUniversal.Hide();
            }
            break;

        default:
            e.processOnServer = false;
            break;
    }
}

function PoClick(s, e) {
    window.parent.popupUniversal.Hide();
}

function JoClick(s, e) {
    return;
}

var grida = "gvFazat";
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val('Filtra');
    if (grida == "gvFazat")
        myMenu.aplikoFiltra(s, e, gvFazat, "3031", '');
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
var DateDiff = {

    inDays: function (d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000));
    },

    inWeeks: function (d1, d2) {
        var t2 = d2.getTime();
        var t1 = d1.getTime();

        return parseInt((t2 - t1) / (24 * 3600 * 1000 * 7));
    },

    inMonths: function (d1, d2) {
        var d1Y = d1.getFullYear();
        var d2Y = d2.getFullYear();
        var d1M = d1.getMonth();
        var d2M = d2.getMonth();

        return (d2M + 12 * d2Y) - (d1M + 12 * d1Y);
    },

    inYears: function (d1, d2) {
        return d2.getFullYear() - d1.getFullYear();
    }
}