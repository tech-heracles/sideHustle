; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;

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

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), 0, hf);

    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}
function OnGetRowValues(values) {

    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=modifikim&numer=' + values[1] + '&indexrow=' + indexModifiko + '&id=' + gvAmortizimi.GetRowKey(gvAmortizimi.GetFocusedRowIndex()));
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvAmortizimi, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvAmortizimi, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvAmortizimi, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvAmortizimi, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvAmortizimi, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {

    myMenu.menu_click_regjistrime(s, e, "Shto_RivleresimeAmortizimi.aspx?lloj=" + Utils.getUrlVar('lloj') + "&shtim_modifikim=shtim&id=0", 'Shto_RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvAmortizimi.GetRowKey(gvAmortizimi.GetFocusedRowIndex()));
    if (e.item.name === 'Klono') {
        if (id === null)
            myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        else
            myFaqeCelje.kontrolloTeDrejta('Shto_RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + gvAmortizimi.GetRowKey(gvAmortizimi.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=klonim');
        e.processOnServer = false;
    }
    if (e.item.name == "Eksporto") {
        e.processOnServer = false;
        gvAmortizimi.GetSelectedFieldValues('IdAmortizimi', OnGridSelectionCompleteEksport);
    }
}

function OnGridSelectionCompleteEksport(values) {

    myButtonClickLupa.ButtonClickLupaEksporto(values, 'amortizimi', 90, 'Rivlersime Amortizimi', 'Format Standart FAFPermbledhese');
}

var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {

        myMenu.ShikoClick(editor, 'Shto_RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvAmortizimi.GetRowKey(gvAmortizimi.GetFocusedRowIndex()));
    } else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvAmortizimi.GetFocusedRowIndex();

    gvAmortizimi.GetRowValues(indexModifiko, 'IdAmortizimi;NrDok', OnGetRowValues);
    focuschange = true;
}
function BeginCallback(s, e) {

    if (e.command === 'APPLYFILTER' && btnFiltrat !== undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    if(Utils.getUrlVar('lloj')=='amortizim')
        callWebserviceKonfigurimi("1005", cmbKonfigurimi.GetText());
    else  callWebserviceKonfigurimi("1006", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvAmortizimi.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}