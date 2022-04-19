; var editmode = false;
var indexEdit = -1;
// kur faqja lodohet
function Init() {
    editmode = false; menuSipasTeDrejta(modifiko, hfTeDrejta);
    indexEdit = -1;
    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaKomente.SetFocusedRowIndex(0);
    
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;
var modifiko = false;
//kur kthehet nje veprim postback nga serveri
function EndRequestHandler(sender, args) {

}
//kthimi i fokusit ne faqe te pare
function ProcessKeyPress() {
    var currentIndex = gvLupaKomente.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKomente.GetVisibleRowsOnPage() - 1) {
            gvLupaKomente.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKomente.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKomente.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
// kur ndryshon select 
function OnGridSelectionChanged() {
    //gvLupaKomente.GetSelectedFieldValues('NrGrupBanke', OnGridSelectionComplete);
    gvLupaKomente.GetRowValues(gvLupaKomente.GetFocusedRowIndex(), 'NrGrupBanke', OnGridSelectionComplete);
}


function menuSipasTeDrejta(modifiko, hfTeDrejta) {
    if (hfTeDrejta.Get('Shtim') == true && modifiko==false) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);

    }
    else if (hfTeDrejta.Get('Modifikim') == true && modifiko==true) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }
    else {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);

    }
}
function switchEditMode(index) {//kalon ne edit grida

    menuSipasTeDrejta(modifiko, hfTeDrejta);
    if (editmode) {
        if (index == indexEdit) {
            gvLupaKomente.CancelEdit();
            editmode = !editmode;
        }
        else {
            gvLupaKomente.StartEditRow(index);
            indexEdit = index;
        }
    }
    else {
        gvLupaKomente.StartEditRow(index);
        indexEdit = index;
        editmode = !editmode;
    }
}

function EndCallbackGrida(s, e) {
    $.ajax({    
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green") {
        myMesazh.ShtoMesazhSuksesi(arr[0]);
        try {
            window.parent.gvAprovimet.Refresh();
        }
        catch (e) {
        }
    }
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}
function Item_Click(s, e) {
	     
    if(e.item.name=='Ruaj')
    { gvLupaKomente.UpdateEdit();    e.processOnServer=false;
           
    }    if(e.item.name=='Anullo') window.parent.popupUniversal.Hide();
}
