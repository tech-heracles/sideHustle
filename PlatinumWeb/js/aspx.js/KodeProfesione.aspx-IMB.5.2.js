; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvKodeProfesione";
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
    $('#hfRuaj').val('Filtra');
    myMenu.aplikoFiltra(s, e, gvKodeProfesione, "", '');


}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvKodeProfesione';
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKodeProfesione, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKodeProfesione, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKodeProfesione, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKodeProfesione, indexSel);
}




function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
/// kontrollon nqs ke te drejta per te kryer nje veprim
function callWebservice() {
    var emer = 'KodeProfesione.aspx';
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
    gvKodeProfesione.StartEditRow(index);


    indexEdit = index;
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
///shfaq emrin e faqes
function changeName() {

    myFaqeCelje.changeNameRegjistrime('KodeProfesione.aspx', 0);
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
       
        editorAktiv = Utils.ktheKontroll('Aktiv');
        if (!editorAktiv) return;
        editorAktiv.SetChecked(true);
    }
}

///verprimet e menu clickut

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');

    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKodeProfesione, hfTeDrejta);

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
