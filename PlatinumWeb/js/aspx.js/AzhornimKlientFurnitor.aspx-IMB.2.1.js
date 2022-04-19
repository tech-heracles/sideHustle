//variabla per te kaluar nga nje reshti i grides tek tjetri
; var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;
function OnGetRowValues(values) {
    var hidField1 = $("#hfVeprimi")[0];
    id = grid_AzhornimKF.GetRowKey(indexModifiko);
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep') + '&shtim_modifikim=modifikim&numer=' + values[1] + '&indexrow=' + indexModifiko + '&id=' + grid_AzhornimKF.GetRowKey(grid_AzhornimKF.GetFocusedRowIndex()) + '&shitje_blerje=' + hidField1.value);
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_AzhornimKF, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_AzhornimKF, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_AzhornimKF, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_AzhornimKF, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_AzhornimKF, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hidField1 = $("#hfVeprimi")[0];
    if (e.item.name == "Shiko") {
        indexModifiko = grid_AzhornimKF.GetFocusedRowIndex();
        e.processOnServer = false;
        if (Utils.getUrlVar('vep') == 'azhornim')
            grid_AzhornimKF.GetRowValues(indexModifiko, 'IdAzhornimKFKoka;NrDok', OnGetRowValues);
        else grid_AzhornimKF.GetRowValues(indexModifiko, 'IdKoka;NrDok', OnGetRowValues);
        mbush = true;
    }
    else
        myMenu.menu_click_regjistrime(s, e, "Shto_AzhornimKlientFurnitor.aspx?vep=" + Utils.getUrlVar('vep') + "&shtim_modifikim=shtim", 'Shto_AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep') + '&shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + grid_AzhornimKF.GetRowKey(grid_AzhornimKF.GetFocusedRowIndex()) + '&shitje_blerje=' + hidField1.value);

}
var mbush = false;
function OnGridDoubleClick(e, index) {
//    if (!focuschange) {
//        var hidField1 = $("#hfVeprimi")[0];
//        myMenu.ShikoClick(editor, 'Shto_AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep') + '&shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + id + '&shitje_blerje=' + hidField1.value);
//    } else {
//        indexModifiko = index;
//        mbush = true;
    //    }
    indexModifiko = index;
    if (Utils.getUrlVar('vep') == 'azhornim')
        grid_AzhornimKF.GetRowValues(indexModifiko, 'IdAzhornimKFKoka;NrDok', OnGetRowValues);
    else grid_AzhornimKF.GetRowValues(indexModifiko, 'IdKoka;NrDok', OnGetRowValues);
    mbush = true;
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = grid_AzhornimKF.GetFocusedRowIndex();
    if (Utils.getUrlVar('vep') == 'azhornim')
        grid_AzhornimKF.GetRowValues(indexModifiko, 'IdAzhornimKFKoka;NrDok', OnGetRowValues);
    else grid_AzhornimKF.GetRowValues(indexModifiko, 'IdKoka;NrDok', OnGetRowValues);
    focuschange = true;
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

function changeName() {

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep'),0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}
function selection(index) {
    hf = document.getElementById("hfReshtaTeSelektuar");
    hf.value = grid_AzhornimKF.GetSelectedKeysOnPage();
}
function OnGridSelectionCompleteMultiSelect(values) {
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

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    if (Utils.getUrlVar('vep') == 'azhornim') callWebserviceKonfigurimi("651", cmbKonfigurimi.GetText()); else callWebserviceKonfigurimi("678", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    grid_AzhornimKF.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}
function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}