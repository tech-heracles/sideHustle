; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;

var indexModifiko;

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
    var hidField1 = $("#hfVeprimi")[0];
    id = grid_RegRip.GetRowKey(grid_RegRip.GetFocusedRowIndex());

    numur = values[1];
    focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_RegjistrimRiparimi.aspx?shtim_modifikim=riparim&id=' + grid_RegRip.GetRowKey(grid_RegRip.GetFocusedRowIndex()) + '&numer=' + values[1] + '&indexrow=' + indexModifiko );
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_RegRip, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_RegRip, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_RegRip, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_RegRip, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_RegRip, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hidField1 = $("#hfVeprimi")[0];
    myMenu.menu_click_regjistrime(s, e, "Shto_RegjistrimRiparimi.aspx?shtim_modifikim=shtim", 'Shto_RegjistrimRiparimi.aspx?shtim_modifikim=riparim&id=' + grid_RegRip.GetRowKey(grid_RegRip.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko );
   

}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
}
var mbush = false;

function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        var hidField1 = $("#hfVeprimi")[0];
        myMenu.ShikoClick(editor, 'Shto_RegjistrimRiparimi.aspx?shtim_modifikim=riparim&id=' + grid_RegRip.GetRowKey(grid_RegRip.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko );
    }
    else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
var focuschange = false;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = grid_RegRip.GetFocusedRowIndex();
    //         if (indexModifiko == -1)
    //            myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje fature shitje/blerje!');
    //        else
    grid_RegRip.GetRowValues(indexModifiko, 'IdKoka;NrKontakti', OnGetRowValues);
    focuschange = true;

}
function changeName() {

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RegjistrimRiparimi.aspx', 0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler(); $('#dvMenu').show();//[0].style.visibility = 'visible';
}

var indexModifiko;

function OnError(message, context) {
    if (message == "Session TimeOut")
        ndryshoUrlFrame(Paths.defaultLoginPath);
}

function selection(index) {
    hf = $("#hfReshtaTeSelektuar")[0];
    hf.value = grid_RegRip.GetSelectedKeysOnPage();
}
function OnGridSelectionCompleteMultiSelect(values) {


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
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("541", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {

}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    grid_RegRip.PerformCallback(idKomp + ";" + kodKonf);
}


function EndRequestHandler(sender, args) {
}
