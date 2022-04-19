/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvModelAutomjeti, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvModelAutomjeti, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvModelAutomjeti, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvModelAutomjeti, indexSel);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function Init() {
    try {
        myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
        editmode = false;
        indexEdit = -1;
        myFaqeCelje.shtoHandlerSession();
        gvModelAutomjeti.SetFocusedRowIndex(0);
    }
    catch (err) {
    }
}

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvModelAutomjeti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).on('unload',function () {
});

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (e.item.name == 'OK') {
        e.processOnServer = false;
        OnGridSelectionChanged();        
    }
    else {
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvModelAutomjeti, hfTeDrejta);       
    }
}

function OnGridSelectionChanged() {
    var indexi = gvModelAutomjeti.GetFocusedRowIndex();
    gvModelAutomjeti.GetRowValues(indexi, 'IdModeli;KodModeli;PershkrimModeli;IdStatusDok;IdNdermarrje;IdKrijuesi;IdPerdoruesi;DtKrijimi;DtModifikimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    //    var s = new String();
    if (values.length == 0 || values == null) {
        alert("Ju lutemi, zgjidhni një rresht!");
        return;
    }
    var id = values[0];
    var modeli = values[2];
    switch (window.parent.identikuesPerPopupModelAutomjeti) {        
        case 'ShtoAutomjet':
        case 'LupaAutomjetShpejte':
            Utils.SelectComboItem(window.parent.cmbModelAuto, values[0], values[2], values[1]);
            window.parent.cmbModelAuto.SetFocus(true);
            break;
    }
    window.parent.popupUniversal.Hide();
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj')[0].value = 'Filtra';
    myMenu.aplikoFiltra(s, e, gvModelAutomjeti, "", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko') {
        KodModeli.SetEnabled(false);        
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        KodModeli.SetEnabled(true);        
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

function RowDblClickGrida(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
}

function callWebservice() {
    var emer = 'LupaModelAutomjeti.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true") {
        indexModifiko = gvModelAutomjeti.GetFocusedRowIndex();
        switchEditMode(indexModifiko);
    }
    else {
        alert("Nuk keni të drejta për të kryer këtë veprim!");
    }
}

function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    gvModelAutomjeti.StartEditRow(index);
    indexEdit = index;
}