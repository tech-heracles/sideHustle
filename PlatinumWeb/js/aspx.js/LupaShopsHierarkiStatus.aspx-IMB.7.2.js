; var editmode = false;
var indexEdit = -1;
var indexModifiko;

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    $('#hfRuaj').val('Filtra'); btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaShopsHierarkiStatus, "", '');
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvLupaShopsHierarkiStatus, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvLupaShopsHierarkiStatus, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvLupaShopsHierarkiStatus, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvLupaShopsHierarkiStatus, indexSel);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

function callWebservice() {
    var emer = 'LupaShopsHierarkiStatus.aspx';
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
    gvLupaShopsHierarkiStatus.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;

}
function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvLupaShopsHierarkiStatus, hfTeDrejta);
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    if (e.item.name == "Anullo") {
        window.parent.popupUniversal.Hide();
    }
}


function OnGridSelectionChanged() {
    gvLupaShopsHierarkiStatus.GetRowValues(gvLupaShopsHierarkiStatus.GetFocusedRowIndex(), 'IdStatus;PershkrimStatusi;Aktiv', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {

    if (values[0] != undefined) {
        if (Utils.getUrlVar('vjenNga') == 'Perdoruesi') {
            if (window.parent.cmbStatus.GetText() != values[1]) {
                if (window.parent.cmbStatus.FindItemByText(values[1]) == null)
                    window.parent.cmbStatus.AddItem(values[1], values[0]);
                window.parent.cmbStatus.SetValue(values[0]);
                window.parent.cmbStatus.SetText(values[1]);
                if (values[2] == false) {
                    myMesazh.ShtoMesazhGabimi("Nuk mund te zgjidhni nje status jo aktiv");
                    window.parent.cmbStatus.SetValue('');
                    return;
                }
            }
            window.parent.cmbStatus.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Importi') {
            if (window.parent.editorGlobal.GetText() != values[1]) {
                window.parent.editorGlobal.SetValue(values[0]);
                window.parent.editorGlobal.SetText(values[1]);
            }
            window.parent.editorGlobal.SetFocus();
        }
        if (window.parent.identikuesPerPopupPerdoruesi == "raporti") {
            if (window.parent.editorGlobal.GetText() != values[1]) {
                window.parent.editorGlobal.SetValue(values[0]);
                window.parent.editorGlobal.SetText(values[1]);
            }
            window.parent.editorGlobal.SetFocus();
        }

    }
    window.parent.popupUniversal.Hide();
}


function changeName() {
    myFaqeCelje.changeNameRegjistrime('LupaShopsHierarkiStatus.aspx', 0);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}


function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko') {
        PershkrimStatusi.SetEnabled(false);
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        PershkrimStatusi.SetEnabled(true);
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