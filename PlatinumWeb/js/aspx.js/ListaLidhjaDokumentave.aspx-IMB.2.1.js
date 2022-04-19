; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;
function OnGetRowValues(values) {
    //        var hf = $("#hfLloji")[0];
    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'LidhjaDokumentave.aspx?shtim_modifikim=modifikim&numer=' + values[1] + '&indexrow=' + indexModifiko + '&id=' + gvDokumenta.GetRowKey(gvDokumenta.GetFocusedRowIndex()));
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
    myMenu.aplikoFiltra(s, e, gvDokumenta, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvDokumenta, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvDokumenta, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvDokumenta, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvDokumenta, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    //        var hf = $("#hfLloji")[0];
    myMenu.menu_click_regjistrime(s, e, "LidhjaDokumentave.aspx?shtim_modifikim=shtim", 'LidhjaDokumentave.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvDokumenta.GetRowKey(gvDokumenta.GetFocusedRowIndex()));

}
var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        //            var hf = $("#hfLloji")[0];
        myMenu.ShikoClick(editor, 'LidhjaDokumentave.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvDokumenta.GetRowKey(gvDokumenta.GetFocusedRowIndex()));
    } else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvDokumenta.GetFocusedRowIndex();
    gvDokumenta.GetRowValues(indexModifiko, 'IdKoka;NrLidhje', OnGetRowValues);
    focuschange = true;
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('ListaLidhjaDokumentave.aspx',0, hf);
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
    callWebserviceKonfigurimi("229", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvDokumenta.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}