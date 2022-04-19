; var editmode = false;
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKurset, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKurset, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKurset, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKurset, indexSel);
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
        gvKurset.SetFocusedRowIndex(0);        
    }
    catch (err) {
    }
}

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvKurset.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).on('unload', function () {
});

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
        default:
            myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKurset, hfTeDrejta);
            break;
        
    }
}

function OnGridSelectionChanged() {
    var indexi = gvKurset.GetFocusedRowIndex();
    gvKurset.GetRowValues(indexi, 'IdKursi;LlojKursi;VleraKursi;DataKursit;NjesiaKursit;IdMonedha;PershkrimLlojKursi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {    
    var grida = window.parent.$('#rowed5');
    var idRow = grida.getLastSel2();

    if (values.length == 0 || values == null) {
        alert("Ju lutemi, zgjidhni një rresht!");
        return;
    }
    var id = values[0];
    var vleraKursi = values[2];
    switch (window.parent.identikuesPerPopupKursi) {
        case 'ShtoRegjistrimDokumentash':
        case 'ShtoFleteDoganore':
        case 'ShtoListPagesa':
            window.parent.txtKursi.SetText(vleraKursi);
            window.parent.txtKursi.SetFocus(true);
            break;
        case 'ShtoVeprimBanka':
            window.parent.kursi_TextBox.SetText(vleraKursi);
            window.parent.kursi_TextBox.SetFocus(true);
            break;
        case 'ShtoFleteKontabel':
            grida.setTekstQelize('txtKursi', idRow, vleraKursi);
            window.parent.$("#txtKursi" + idRow).focus();
            window.parent.textChangedKursi();            
            break;
        case 'ShtoVeprimeKf':        
            grida.setTekstQelize('txtKursi', idRow, vleraKursi);
            window.parent.$("#txtKursi" + idRow).focus();
            window.parent.changedKursi();            
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
    myMenu.aplikoFiltra(s, e, gvKurset, "", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
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

function RowDblClickGrida(s, e) {
    e.processOnServer = false;
    OnGridSelectionChanged();
}