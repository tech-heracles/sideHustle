; var editmode = false;
var indexEdit = -1;

$(document).ready(function () {
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
    $(window).on('load', function () {
        Init();
    });

});

// kur faqja lodohet
function Init() {
    editmode = false; menuSipasTeDrejta(modifiko, hfTeDrejta);
    indexEdit = -1;
    changeName();
    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaGrNdermarje.SetFocusedRowIndex(0);
        btnOk.Focus();
       if( $(window.parent.location).attr('href').search('FaqeKryesore')!=-1)
       btnOk.SetVisible(false) ;
    }
    catch (err) {
        ///alert('gabim');    
    }
}

//shfaq emrin e komponentes tek faqja
function changeName() {
    myFaqeCelje.changeNameRegjistrime('LupaGrupNdermarrje.aspx', 0);

}


document.onkeydown = ProcessKeyPress;
var modifiko = false;
//kur kthehet nje veprim postback nga serveri
function EndRequestHandler(sender, args) {
    if (modifiko) Kodi.SetEnabled(false); menuSipasTeDrejta(modifiko, hfTeDrejta);
}
//kthimi i fokusit ne faqe te pare
function ProcessKeyPress() {
    var currentIndex = gvLupaGrNdermarje.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaGrNdermarje.GetVisibleRowsOnPage() - 1) {
            gvLupaGrNdermarje.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrNdermarje.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaGrNdermarje.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
// kur ndryshon select 
function OnGridSelectionChanged() {
   gvLupaGrNdermarje.GetRowValues(gvLupaGrNdermarje.GetFocusedRowIndex(), 'Kodi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    try{
        window.parent.txtGrupi.SetText(values);

        window.parent.popupUniversal.Hide();
    }
    catch (e) {
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
function switchEditMode(index) {//kalon ne edit grida

    menuSipasTeDrejta(modifiko, hfTeDrejta);
    if (editmode) {
        if (index == indexEdit) {
            gvLupaGrNdermarje.CancelEdit();
            editmode = !editmode;
        }
        else {
            gvLupaGrNdermarje.StartEditRow(index);
            indexEdit = index;
        }
    }
    else {
        gvLupaGrNdermarje.StartEditRow(index);
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
    { gvLupaGrNdermarje.UpdateEdit();    e.processOnServer=false;
        modifiko=false;
    }   
    else     if(e.item.name=='Modifiko')   
    {modifiko=true;} else modifiko=false;                   
    menuSipasTeDrejta(modifiko, hfTeDrejta);            }
