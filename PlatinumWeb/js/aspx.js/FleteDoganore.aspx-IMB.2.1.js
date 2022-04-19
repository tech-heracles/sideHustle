//variabla per te kaluar nga nje reshti i grides tek tjetri
; var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji'), 0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
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
    changeName();
});

function OnGetRowValues(values) {
    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + grid_FleteDoganore.GetRowKey(grid_FleteDoganore.GetFocusedRowIndex()) + '&numer=' + values[1] + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
}

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_FleteDoganore, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_FleteDoganore, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_FleteDoganore, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_FleteDoganore, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_FleteDoganore, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    myMenu.menu_click_regjistrime(s, e, "Shto_FleteDoganore.aspx?lloji=" + Utils.getUrlVar('lloji') + '&shtim_modifikim=shtim&id=0', 'Shto_FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + grid_FleteDoganore.GetRowKey(grid_FleteDoganore.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
}

var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        myMenu.ShikoClick(editor, 'Shto_FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + grid_FleteDoganore.GetRowKey(grid_FleteDoganore.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
    }
    else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = grid_FleteDoganore.GetFocusedRowIndex();
    grid_FleteDoganore.GetRowValues(indexModifiko, 'IdFleteDoganoreKoka;NrDok', OnGetRowValues);
    focuschange = true;
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
        lblKonfigurimi.SetText(pershkKonfigAmb);
    callWebserviceKonfigurimi("523", cmbKonfigurimi.GetText());
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
    grid_FleteDoganore.PerformCallback();
}

function ndryshoKonfiguriminInit() {

}

function callWebserviceKonfigurimi(idKomp, kodKonf) {

    grid_FleteDoganore.PerformCallback(idKomp + ";" + kodKonf);
}

function EndRequestHandler(sender, args) {
}
function Click_ButtonOk(s, e) {
    Utils.shfaqLoadingGif();;
    popFshi.Hide();
}