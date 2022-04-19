; var editmode = false;
var indexEdit = -1;
//kur lodohet faqja
function Init() {
    editmode = false; menuSipasTeDrejta(modifiko, hfTeDrejta);
    indexEdit = -1;
    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaGrPunonjesish.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
} function menuSipasTeDrejta(modifiko, hfTeDrejta) {
    if (hfTeDrejta.Get('Shtim') == true && modifiko == false) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);

    }
    else if (hfTeDrejta.Get('Modifikim') == true && modifiko == true) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }
    else {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);

    } if (hfTeDrejta.Get('Modifikim') == true) ASPxMenu1.GetItemByName('Modifiko').SetEnabled(true);
    else ASPxMenu1.GetItemByName('Modifiko').SetEnabled(false);
}
var modifiko = false;
// kur kthehet nje veprim postback nga serveri
function EndRequestHandler(sender, args) {
    if (modifiko) Nr.SetEnabled(false); menuSipasTeDrejta(modifiko, hfTeDrejta);
}
document.onkeydown = ProcessKeyPress;

//kthimi i fokusit ne faqe te pare
function ProcessKeyPress() {
    var currentIndex = gvLupaGrPunonjesish.GetFocusedRowIndex();
    if (event.keyCode === 40) {
        if (currentIndex === gvLupaGrPunonjesish.GetVisibleRowsOnPage() - 1) {
            gvLupaGrPunonjesish.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrPunonjesish.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode === 38) {
        if (currentIndex === 0) {
            return;
        }
        else {
            gvLupaGrPunonjesish.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode === 13) {
        OnGridSelectionChanged();

    }
}
// kur ndryshon selektimi
function OnGridSelectionChanged() {
    gvLupaGrPunonjesish.GetRowValues(gvLupaGrPunonjesish.GetFocusedRowIndex(), 'IdGrupPunonjesish;Nr', OnGridSelectionComplete);
}
// kur bejme double klick qe kalojme te dhenat tek faqja kryesore
function OnGridSelectionComplete(values) {
    window.parent.cmbGrupi.SetSelectedIndex(window.parent.cmbGrupi.AddItem(values[1], values[0]));
    window.parent.cmbGrupi.SetFocus(true);
    window.parent.popupUniversal.Hide();
}
// kalimi nga edit mode ne gjendje jo edito mode dhe e kunderta
function switchEditMode(index) {//kalon ne edit grida
    menuSipasTeDrejta(modifiko, hfTeDrejta);
    if (editmode) {
        if (index === indexEdit) {
            gvLupaGrPunonjesish.CancelEdit();
            editmode = !editmode;
        }
        else {
            gvLupaGrPunonjesish.StartEditRow(index);
            indexEdit = index;
        }
    }
    else {
        gvLupaGrPunonjesish.StartEditRow(index);
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
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}

function Item_Click(s, e) {
    if(e.item.name=='Fshi')
    {  popFshi.Show(); e.processOnServer=false;
    }
    else if(e.item.name=='Ruaj')
    { gvLupaGrPunonjesish.UpdateEdit();    e.processOnServer=false;
        modifiko=false;
    }   
    else     if(e.item.name=='Modifiko')   
    {modifiko=true;} else modifiko=false;           menuSipasTeDrejta(modifiko, hfTeDrejta);  
}