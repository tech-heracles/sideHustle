; var editmode = false;
var indexEdit = -1;

function Init() {
    editmode = false; menuSipasTeDrejta(modifiko, hfTeDrejta);
    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    indexEdit = -1;
    try {
        myFaqeCelje.shtoHandlerSession();
        
        gvLupaGrKont.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
function menuSipasTeDrejta(modifiko, hfTeDrejta) {
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
document.onkeydown = ProcessKeyPress;
var modifiko = false;
//kur kthehet nje veprim postback nga serveri
function EndRequestHandler(sender, args) {
     menuSipasTeDrejta(modifiko, hfTeDrejta);
}
//kthimi i fokusit ne faqe te pare
function ProcessKeyPress() {
    var currentIndex = gvLupaGrKont.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaGrKont.GetVisibleRowsOnPage() - 1) {
            gvLupaGrKont.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrKont.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaGrKont.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    //gvLupaGrKont.GetSelectedFieldValues('NrGrupKontabilizimi', OnGridSelectionComplete);
    gvLupaGrKont.GetRowValues(gvLupaGrKont.GetFocusedRowIndex(), 'NrGrupKontabilizimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    window.parent.txtNrGrupKontabilizimi.SetText(values);window.parent.popupUniversal.Hide();
    if (window.parent.indentifikuesPerPopup == "FleteKontabel")
     window.parent.btnGrupo.DoClick();    



}

function switchEditMode(index) {//kalon ne edit grida
    menuSipasTeDrejta(modifiko, hfTeDrejta);
    if (editmode) {
        if (index == indexEdit) {
            gvLupaGrKont.CancelEdit();
            editmode = !editmode;
        }
        else {
            gvLupaGrKont.StartEditRow(index);
            indexEdit = index;
        }
    }
    else {
        gvLupaGrKont.StartEditRow(index);
        indexEdit = index;
        editmode = !editmode;
    }
}

function EndCallbackGrida(s, e) {
    Utils.hiqLoadingGif();;
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
    {  popFshi.Show();     e.processOnServer = false;} 
    if(e.item.name=='Ruaj')
    { gvLupaGrKont.UpdateEdit();   e.processOnServer=false; modifiko=false;
    }   else     if(e.item.name=='Modifiko')   
    {modifiko=true;} else modifiko=false;      menuSipasTeDrejta(modifiko, hfTeDrejta);
}