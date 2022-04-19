; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;
function OnGetRowValues(values) {
    var hf = $("#hfLloji")[0];
    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=modifikim&id=' + grid_RegMag.GetRowKey(grid_RegMag.GetFocusedRowIndex()) + '&numer=' + values[1] + '&indexrow=' + indexModifiko + '');
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

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_RegMag, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_RegMag, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_RegMag, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_RegMag, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_RegMag, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hf = $("#hfLloji")[0];
    if (e.item.name === 'Fshi') {
        e.processOnServer = false;
        popFshi.Hide();
        grid_RegMag.GetSelectedFieldValues('IdKoka;NrDok', SuccededCallbackFshi);
    }
    else
        myMenu.menu_click_regjistrime(s, e, "Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=shtim", 'Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=modifikim&id=' + grid_RegMag.GetRowKey(grid_RegMag.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '', grid_RegMag.GetSelectedRowCount());

}

function SuccededCallbackFshi(selectedValues) {
    lblMsgbox.SetText(hfState.Get("msgJuKeniZgjedhur") + selectedValues.length + hfState.Get("msgRreshta") + hfState.Get("labelAdministrimiMsgJeniSigurt"));
    popFshi.Show();
}
var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        var hf = $("#hfLloji")[0];
        myMenu.ShikoClick(editor, 'Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=modifikim&id=' + grid_RegMag.GetRowKey(grid_RegMag.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '');
    } else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = grid_RegMag.GetFocusedRowIndex();

    grid_RegMag.GetRowValues(indexModifiko, 'IdKoka;NrDok', OnGetRowValues);
    focuschange = true;
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RegjistrimNdryshimCmimSasi.aspx', 0, hf);

    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    $('#dvMenu').show();//[0].style.visibility = 'visible';
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
    callWebserviceKonfigurimi("544", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    grid_RegMag.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}

function clickExport(e) {
    if (grid_RegMag.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}