; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvShtesaPage";
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}

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

var btnFiltrat;
///aplikon filtrat ne gride
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val( 'Filtra');
    if (grida == "gvShtesaPage")
        myMenu.aplikoFiltra(s, e, gvShtesaPage, "", '');
    else if (grida == "gvShtesaPage2")
        myMenu.aplikoFiltra(s, e, gvShtesaPage2, "", '');
    else if (grida == "gvShtesaPage3")
        myMenu.aplikoFiltra(s, e, gvShtesaPage3, "", '');
    else if (grida == "gvShtesaPage4")
        myMenu.aplikoFiltra(s, e, gvShtesaPage4, "", '');
    else if (grida == "gvShtesaPage5")
        myMenu.aplikoFiltra(s, e, gvShtesaPage5, "", '');
    else if (grida == "gvShtesaPage6")
        myMenu.aplikoFiltra(s, e, gvShtesaPage6, "", '');

}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
///kur ben double click
function RowDblClickGrida2(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvShtesaPage2';
}
function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvShtesaPage';
}
function RowDblClickGrida3(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvShtesaPage3';
}
function RowDblClickGrida4(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvShtesaPage4';
}
function RowDblClickGrida5(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvShtesaPage5';
}
function RowDblClickGrida6(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvShtesaPage6';
}
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvShtesaPage, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvShtesaPage, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvShtesaPage, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvShtesaPage, indexSel);
}

//kur ndryshon tabin del nga editimi
function ndryshimTabi(tab) {

    if (tab.GetText() == "Funksioni %") {
        grida = "gvShtesaPage";
        if (gvShtesaPage2.IsEditing() == true)
            gvShtesaPage2.CancelEdit();
        else if (gvShtesaPage3.IsEditing() == true)
            gvShtesaPage3.CancelEdit();
        else if (gvShtesaPage4.IsEditing() == true)
            gvShtesaPage4.CancelEdit();
        else if (gvShtesaPage5.IsEditing() == true)
            gvShtesaPage5.CancelEdit();
        else if (gvShtesaPage6.IsEditing() == true)
            gvShtesaPage6.CancelEdit();

    }
    else if (tab.GetText() == "Funksioni vlere") {
        grida = "gvShtesaPage2";
        if (gvShtesaPage.IsEditing() == true)
            gvShtesaPage.CancelEdit();
        else if (gvShtesaPage3.IsEditing() == true)
            gvShtesaPage3.CancelEdit();
        else if (gvShtesaPage4.IsEditing() == true)
            gvShtesaPage4.CancelEdit();
        else if (gvShtesaPage5.IsEditing() == true)
            gvShtesaPage5.CancelEdit();
        else if (gvShtesaPage6.IsEditing() == true)
            gvShtesaPage6.CancelEdit();
    }
    else if (tab.GetText() == "Pozicioni") {
        grida = "gvShtesaPage3";
        if (gvShtesaPage.IsEditing() == true)
            gvShtesaPage.CancelEdit();
        else if (gvShtesaPage2.IsEditing() == true)
            gvShtesaPage2.CancelEdit();
        else if (gvShtesaPage4.IsEditing() == true)
            gvShtesaPage4.CancelEdit();
        else if (gvShtesaPage5.IsEditing() == true)
            gvShtesaPage5.CancelEdit();
        else if (gvShtesaPage6.IsEditing() == true)
            gvShtesaPage6.CancelEdit();
    }
    else if (tab.GetText() == "Kualifikimi") {
        grida = "gvShtesaPage4";
        if (gvShtesaPage.IsEditing() == true)
            gvShtesaPage.CancelEdit();
        else if (gvShtesaPage2.IsEditing() == true)
            gvShtesaPage2.CancelEdit();
        else if (gvShtesaPage3.IsEditing() == true)
            gvShtesaPage3.CancelEdit();
        else if (gvShtesaPage5.IsEditing() == true)
            gvShtesaPage5.CancelEdit();
        else if (gvShtesaPage6.IsEditing() == true)
            gvShtesaPage6.CancelEdit();
    }
    else if (tab.GetText() == "Veshtiresia") {
        grida = "gvShtesaPage5";
        if (gvShtesaPage.IsEditing() == true)
            gvShtesaPage.CancelEdit();
        else if (gvShtesaPage2.IsEditing() == true)
            gvShtesaPage2.CancelEdit();
        else if (gvShtesaPage4.IsEditing() == true)
            gvShtesaPage4.CancelEdit();
        else if (gvShtesaPage3.IsEditing() == true)
            gvShtesaPage3.CancelEdit();
        else if (gvShtesaPage6.IsEditing() == true)
            gvShtesaPage6.CancelEdit();
    }
    else if (tab.GetText() == "Vjetersia") {
        grida = "gvShtesaPage6";
        if (gvShtesaPage.IsEditing() == true)
            gvShtesaPage.CancelEdit();
        else if (gvShtesaPage2.IsEditing() == true)
            gvShtesaPage2.CancelEdit();
        else if (gvShtesaPage4.IsEditing() == true)
            gvShtesaPage4.CancelEdit();
        else if (gvShtesaPage5.IsEditing() == true)
            gvShtesaPage5.CancelEdit();
        else if (gvShtesaPage3.IsEditing() == true)
            gvShtesaPage3.CancelEdit();
    }
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
/// kontrollon nqs ke te drejta per te kryer nje veprim
function callWebservice() {
    var emer = 'ShtesaPage.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko, grida);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}
// kalon griden ne edit mode
function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    if (grida == "gvShtesaPage")
        gvShtesaPage.StartEditRow(index);
    else if (grida == "gvShtesaPage2")
        gvShtesaPage2.StartEditRow(index);
    else if (grida == "gvShtesaPage3")
        gvShtesaPage3.StartEditRow(index);
    else if (grida == "gvShtesaPage4")
        gvShtesaPage4.StartEditRow(index);
    else if (grida == "gvShtesaPage5")
        gvShtesaPage5.StartEditRow(index);
    else if (grida == "gvShtesaPage6")
        gvShtesaPage6.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
///shfaq emrin e faqes
function changeName() {
    myFaqeCelje.changeNameRegjistrime('ShtesaPage.aspx', 0);
}


var editorKodi;

/// ben enable disable kodin 
function enable() {//kur humb fokusin kolona Autorizimeve      
    if ($("#hfRuaj").val() == "Modifiko") {
        editorKodi = Utils.ktheKontroll('Kodi');
        editorKodi.SetEnabled(false);
    }
    else {

        editorKodi = Utils.ktheKontroll('Kodi');
        editorKodi.SetEnabled(true);
    }
}

///verprimet e menu clickut

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj')
    if (grida == "gvShtesaPage")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvShtesaPage, hfTeDrejta);
    else if (grida == "gvShtesaPage2")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvShtesaPage2, hfTeDrejta);
    else if (grida == "gvShtesaPage3")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvShtesaPage3, hfTeDrejta);
    else if (grida == "gvShtesaPage4")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvShtesaPage4, hfTeDrejta);
    else if (grida == "gvShtesaPage5")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvShtesaPage5, hfTeDrejta);
    else if (grida == "gvShtesaPage6")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvShtesaPage6, hfTeDrejta);

//    if (e.item.name == 'Ruaj' && grida == "gvShtesaPage") {
//        gvShtesaPage.UpdateEdit();
//        e.processOnServer = false;

//    }

//    if (e.item.name == 'Ruaj' && grida == "gvShtesaPage2") {
//        gvShtesaPage2.UpdateEdit();
//        e.processOnServer = false;
//    }

//    if (e.item.name == 'Ruaj' && grida == "gvShtesaPage3") {
//        gvShtesaPage3.UpdateEdit();
//        e.processOnServer = false;
//    }
//    if (e.item.name == 'Ruaj' && grida == "gvShtesaPage4") {
//        gvShtesaPage4.UpdateEdit();
//        e.processOnServer = false;
//    }
//    if (e.item.name == 'Ruaj' && grida == "gvShtesaPage5") {
//        gvShtesaPage5.UpdateEdit();
//        e.processOnServer = false;
//    }
//    if (e.item.name == 'Ruaj' && grida == "gvShtesaPage6") {
//        gvShtesaPage6.UpdateEdit();
//        e.processOnServer = false;
//    }

}

function EndCallbackGrida(s, e) {
    enable();    
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
function End_Callback(s, e) {
    enable()
    if($('#hfRuaj')[0].value=='Modifiko') 
    { btn.DoClick(); }
    else if($('#hfRuaj')[0].value=='Ruaj') 
    { btn.DoClick(); }           
}