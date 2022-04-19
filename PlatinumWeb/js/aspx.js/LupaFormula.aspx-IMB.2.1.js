;var editmode = false;
var indexEdit = -1;
var indexModifiko;
var btnFiltrat;

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvFormula, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvFormula, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvFormula, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvFormula, indexSel);
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
        gvFormula.SetFocusedRowIndex(0);
        //gvFormula.SelectRowOnPage(0, true);
    }
    catch (err) {
    }
}

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvFormula.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).on('unload',function () {
});

//document.onkeydown = ProcessKeyPress;

//function ProcessKeyPress() {
//    var currentIndex = gvFormula.GetFocusedRowIndex();
//    if (event.keyCode == 40) {
//        if (currentIndex == gvFormula.GetVisibleRowsOnPage() - 1) {
//            gvFormula.SetFocusedRowIndex(0);
//        }
//        else {
//            gvFormula.SetFocusedRowIndex(currentIndex + 1);
//        }
//    }
//    if (event.keyCode == 38) {
//        if (currentIndex == 0) {
//            return;
//        }
//        else {
//            gvFormula.SetFocusedRowIndex(currentIndex - 1);
//        }
//    }
//    if (event.keyCode == 13) {
//        OnGridSelectionChanged();
//    }
//}

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (e.item.name == 'OK') {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    else {
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvFormula, hfTeDrejta);
    }
}

function OnGridSelectionChanged() {
    var indexi = gvFormula.GetFocusedRowIndex();
    gvFormula.GetRowValues(indexi, 'IdFormula;KodFormula;PershkrimFormula;IdNdermarje;IdPerdorues;DtKrijimi;DtModifikimi;IdStatusDok', OnGridSelectionComplete);
    //gvFormula.GetSelectedFieldValues('IdFormula;KodFormula;PershkrimFormula;IdNdermarje;IdPerdorues;DtKrijimi;DtModifikimi;IdStatusDok', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
//    var s = new String();
    if (values.length == 0 || values == null) {
        alert("Ju lutemi, zgjidhni një rresht!");
        return;
    }
//    s = s + values[0];
//    var vl = s.split(",");
    var id = values[0];
    var formula = values[2];
    switch (window.parent.identikuesPerPopupFormula) {
        case 'Artikull':
            window.parent.editorFormula.SetText(formula);
            //window.parent.editorFormula.SetValue(id);
            window.parent.editorFormula.SetFocus(true);
            break;
        case 'KonfigurimDokumentash':
            window.parent.editorFormula.SetText(formula);
            window.parent.editorFormula.SetValue(id);
            window.parent.editorFormula.SetFocus(true);
            break;
        case 'LupaArtShpejt':
            window.parent.editorFormula.SetText(formula);
            //window.parent.editorFormula.SetValue(id);
            window.parent.editorFormula.SetFocus(true);
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
    myMenu.aplikoFiltra(s, e, gvFormula, "", '');
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
        KodFormula.SetEnabled(false);
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        KodFormula.SetEnabled(true);
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
    var emer = 'LupaFormula.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true") {
        indexModifiko = gvFormula.GetFocusedRowIndex();
        switchEditMode(indexModifiko);
    }
    else {
        alert("Nuk keni të drejta për të kryer këtë veprim!");
    }
}

function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    gvFormula.StartEditRow(index);
    indexEdit = index;
}

function ProcessTextChanged(fieldName, value) {

    if (fieldName == "PershkrimFormula") {
        var formula = PershkrimFormula.GetText();
        if (formula.match(/[^0-9\.*-+\/]/)) {
//            myMesazh.ShtoMesazhGabimi('Formula që keni shkruar nuk është e vlefshme!');
//            var kodi = KodFormula.GetText();
//            gvFormula.CancelEdit();
//            if ($("#hfRuaj").val() == 'Ruaj') {
//                gvFormula.AddNewRow();
//                KodFormula.SetText(kodi);
//            }
//            else {
//                gvFormula.StartEditRow(gvFormula.GetFocusedRowIndex());
//            }
        }
    }
}