; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvKategoriPage";
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
    if (grida == "gvKategoriPage")
        myMenu.aplikoFiltra(s, e, gvKategoriPage, "", '');
    else if (grida == "gvKategoriPage2")
        myMenu.aplikoFiltra(s, e, gvKategoriPage2, "", '');
    else if (grida == "gvKategoriPage3")
        myMenu.aplikoFiltra(s, e, gvKategoriPage3, "", '');

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
    grida = 'gvKategoriPage2';
}
function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvKategoriPage';
}
function RowDblClickGrida3(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvKategoriPage3';
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKategoriPage, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKategoriPage, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKategoriPage, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKategoriPage, indexSel);
}

//kur ndryshon tabin del nga editimi
function ndryshimTabi(tab) {

    if (tab.GetText() == "Mujore") {
        grida = "gvKategoriPage";
        if (gvKategoriPage2.IsEditing() == true)
            gvKategoriPage2.CancelEdit();
        else if (gvKategoriPage3.IsEditing() == true)
            gvKategoriPage3.CancelEdit();
    }
    else if (tab.GetText() == "Ditore") {
        grida = "gvKategoriPage2";
        if (gvKategoriPage.IsEditing() == true)
            gvKategoriPage.CancelEdit();
        else if (gvKategoriPage3.IsEditing() == true)
            gvKategoriPage3.CancelEdit();
    }
    else if (tab.GetText() == "Orare") {
        grida = "gvKategoriPage3";
        if (gvKategoriPage.IsEditing() == true)
            gvKategoriPage.CancelEdit();
        else if (gvKategoriPage2.IsEditing() == true)
            gvKategoriPage2.CancelEdit();
    }
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
/// kontrollon nqs ke te drejta per te kryer nje veprim
function callWebservice() {
    var emer = 'KategoriPage.aspx';
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
    if (grida == "gvKategoriPage")
        gvKategoriPage.StartEditRow(index);
    else if (grida == "gvKategoriPage2")
        gvKategoriPage2.StartEditRow(index);
    else if (grida == "gvKategoriPage3")
        gvKategoriPage3.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
///shfaq emrin e faqes
function changeName() {
    myFaqeCelje.changeNameRegjistrime('KategoriPage.aspx',0);
}


var editorKodi;

/// ben enable disable kodin 
function enable() {//kur humb fokusin kolona Autorizimeve     
    editorKodi = Utils.ktheKontroll('Kodi');
    if (!editorKodi) return;
    if ($("#hfRuaj").val() == "Modifiko") {
      
        editorKodi.SetEnabled(false);
    }
    else {

        editorKodi.SetEnabled(true);
    }
}

///verprimet e menu clickut

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (grida == "gvKategoriPage")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKategoriPage, hfTeDrejta);
    else if (grida == "gvKategoriPage2")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKategoriPage2, hfTeDrejta);
    else if (grida == "gvKategoriPage3")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKategoriPage3, hfTeDrejta);

//    if (e.item.name == 'Ruaj' && grida == "gvKategoriPage") {
//        gvKategoriPage.UpdateEdit();
//        e.processOnServer = false;

//    }

//    if (e.item.name == 'Ruaj' && grida == "gvKategoriPage2") {
//        gvKategoriPage2.UpdateEdit();
//        e.processOnServer = false;
//    }

//    if (e.item.name == 'Ruaj' && grida == "gvKategoriPage3") {
//        gvKategoriPage3.UpdateEdit();
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
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}