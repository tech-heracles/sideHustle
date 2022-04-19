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
    myMenu.aplikoFiltra(s, e, gvLupaShopsHierarkiUniform, "", '');
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvLupaShopsHierarkiUniform, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvLupaShopsHierarkiUniform, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvLupaShopsHierarkiUniform, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvLupaShopsHierarkiUniform, indexSel);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

function callWebservice() {
    var emer = 'LupaShopsHierarkiUniform.aspx';
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
        myMesazh.ShtoMesazhGabimi("Nuk keni te drejta per te kryer kete veprim");
    }
}

function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    gvLupaShopsHierarkiUniform.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;

}
function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvLupaShopsHierarkiUniform, hfTeDrejta);
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            window.parent.popupUniversal.Hide();
            break;
    }
}

function OnGridSelectionChanged() {
    gvLupaShopsHierarkiUniform.GetRowValues(gvLupaShopsHierarkiUniform.GetFocusedRowIndex(), 'IdUniform;PershkrimUniform;Aktiv', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {

    if (values[0] != undefined) {
        if (Utils.getUrlVar('vjenNga') == 'Perdoruesi') {
            if (window.parent.cmbUniform.GetText() != values[1]) {
                if (window.parent.cmbUniform.FindItemByText(values[1]) == null)
                    window.parent.cmbUniform.AddItem(values[1], values[0]);
                window.parent.cmbUniform.SetValue(values[0]);
                window.parent.cmbUniform.SetText(values[1]);
                if (values[2] == false) {
                    myMesazh.ShtoMesazhGabimi("Nuk mund te zgjidhni nje uniforme jo aktive");
                    window.parent.cmbUniform.SetValue('');
                    return;
                }
            }
            window.parent.cmbUniform.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Importi') {
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
    myFaqeCelje.changeNameRegjistrime('LupaShopsHierarkiUniform.aspx', 0);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko') {
        PershkrimUniform.SetEnabled(false);
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        PershkrimUniform.SetEnabled(true);
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