; var editmode = false;
var indexEdit = -1;
var indexModifiko;

jQuery(document).ready(function () {
    $(window).on('load', function () {
        Init();
    });
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
});
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    $('#hfRuaj').val('Filtra'); btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLlojDifekti, "", '');
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvLlojDifekti, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvLlojDifekti, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvLlojDifekti, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvLlojDifekti, indexSel);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

function callWebservice() {
    var emer = 'LlojDifekti.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}

function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    gvLlojDifekti.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;

}
function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvLlojDifekti, hfTeDrejta);
    if (e.item.name == "Sinkronizo") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidh ndermarrjet', 'LupaNdermarjeBij.aspx?vjenNga=LlojDifekti', 500, 500);

    }
}

function changeName() {
    myFaqeCelje.changeNameRegjistrime('LlojDifekti.aspx', 0);
}
function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko') {
        Kodi.SetEnabled(false);
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        Kodi.SetEnabled(true);
    }
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