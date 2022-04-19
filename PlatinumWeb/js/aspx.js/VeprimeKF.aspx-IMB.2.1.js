;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;
function OnGetRowValues(values) {
    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_VeprimeKF.aspx?shtim_modifikim=modifikim&numer=' + values[1] + '&indexrow=' + indexModifiko + '&id=' + grid_VeprimeKF.GetRowKey(grid_VeprimeKF.GetFocusedRowIndex()));
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_VeprimeKF, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_VeprimeKF, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_VeprimeKF, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_VeprimeKF, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_VeprimeKF, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    if (e.item.name == "Shiko") {
        indexModifiko = grid_VeprimeKF.GetFocusedRowIndex();
        e.processOnServer = false;
        grid_VeprimeKF.GetRowValues(indexModifiko, 'IdVeprimeKFKoka;NrDok', OnGetRowValues);
        mbush = true;
    }
    else if (e.item.name == "Eksporto") {
        e.processOnServer = false;
        grid_VeprimeKF.GetSelectedFieldValues('IdVeprimeKFKoka', OnGridSelectionCompleteEksport);
    }
    else
        myMenu.menu_click_regjistrime(s, e, "Shto_VeprimeKF.aspx?shtim_modifikim=shtim", 'Shto_VeprimeKF.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + grid_VeprimeKF.GetRowKey(grid_VeprimeKF.GetFocusedRowIndex()), grid_VeprimeKF.GetSelectedRowCount());
}

function OnGridSelectionCompleteEksport(values) {    
    myButtonClickLupa.ButtonClickLupaEksporto(values, 'veprimekf', 20, 'Veprime Klient Furnitor', 'Format Standart VKF');
}

var mbush = false;
function OnGridDoubleClick(e, index) {
//        if (!focuschange) {
//            myMenu.ShikoClick(editor, 'Shto_VeprimeKF.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + id );
//        } else {
//            indexModifiko = index;
//            mbush = true;
    //        } 
        indexModifiko = index;
    grid_VeprimeKF.GetRowValues(indexModifiko, 'IdVeprimeKFKoka;NrDok', OnGetRowValues); 
        mbush = true;
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = grid_VeprimeKF.GetFocusedRowIndex();
    grid_VeprimeKF.GetRowValues(indexModifiko, 'IdVeprimeKFKoka;NrDok', OnGetRowValues);
    focuschange = true;
}
function changeName() {

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('VeprimeKF.aspx',0, hf);
  
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}

function selection(index) {
    hf = document.getElementById("hfReshtaTeSelektuar");
    hf.value = grid_VeprimeKF.GetSelectedKeysOnPage();
} function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function EndRequestHandler(sender, args) {
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("652", cmbKonfigurimi.GetText());
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheIndexSelectedFilterPeriudhaKusht"),
        data: JSON.stringify({ idKonfigurimi: cmbKonfigurimi.GetValue() })
    }).done(SuccededCallbacPeriudhaKusht);
}

function SuccededCallbacPeriudhaKusht(result) {
    hfState.Set("PeriudhaSelektuar", result);
    radDtDok.SetSelectedIndex(result);
}

function onSelectionChanged(s, e) {
    hfState.Set('PeriudhaSelektuar', radDtDok.GetSelectedIndex());
    grid_VeprimeKF.PerformCallback();
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    grid_VeprimeKF.PerformCallback(idKomp + ";" + kodKonf);
}
function EndRequestHandler(sender, args) {
}