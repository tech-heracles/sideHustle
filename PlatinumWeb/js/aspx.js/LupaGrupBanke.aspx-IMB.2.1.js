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
        gvLupaGrBanka.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;
var modifiko = false;
//kur kthehet nje veprim postback nga serveri
function EndRequestHandler(sender, args) {
    if (modifiko) NrGrupBanke.SetEnabled(false);menuSipasTeDrejta(modifiko, hfTeDrejta);
}
//kthimi i fokusit ne faqe te pare
function ProcessKeyPress() {
    var currentIndex = gvLupaGrBanka.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaGrBanka.GetVisibleRowsOnPage() - 1) {
            gvLupaGrBanka.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrBanka.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaGrBanka.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
// kur ndryshon select 
function OnGridSelectionChanged() {
    //gvLupaGrBanka.GetSelectedFieldValues('NrGrupBanke', OnGridSelectionComplete);
    gvLupaGrBanka.GetRowValues(gvLupaGrBanka.GetFocusedRowIndex(), 'NrGrupBanke', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
 
    if (window.parent.identifikuesPerPopupGrupBanke == 'raportGrupBanke') {
        window.parent.editorGlobal.SetText(values);
        window.parent.editorGlobal.SetFocus(true);
    }

    else window.parent.txtGrupi.SetText(values);
    window.parent.popupUniversal.Hide();
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

    } if (hfTeDrejta.Get('Modifikim') == true) ASPxMenu1.GetItemByName('Modifiko').SetEnabled(true);
    else ASPxMenu1.GetItemByName('Modifiko').SetEnabled(false);
}
function switchEditMode(index) {//kalon ne edit grida

    menuSipasTeDrejta(modifiko, hfTeDrejta);
    if (editmode) {
        if (index == indexEdit) {
            gvLupaGrBanka.CancelEdit();
            editmode = !editmode;
        }
        else {
            gvLupaGrBanka.StartEditRow(index);
            indexEdit = index;
        }
    }
    else {
        gvLupaGrBanka.StartEditRow(index);
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
if(e.item.name=='Ruaj')
{ gvLupaGrBanka.UpdateEdit();    e.processOnServer=false;
    modifiko=false;
}   
else     if(e.item.name=='Modifiko')   
{modifiko=true;} else modifiko=false;                   
menuSipasTeDrejta(modifiko, hfTeDrejta);            }