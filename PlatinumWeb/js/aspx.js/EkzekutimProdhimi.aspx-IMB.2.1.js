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
    myFaqeCelje.changeName('EkzekutimProdhimi.aspx',0, hf);
     myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}
function OnGetRowValues(values) {

    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_Ekzekutim.aspx?shtim_modifikim=modifikim&numer=' + values[1] + '&indexrow=' + indexModifiko + '&id=' + gvEkzekutimi.GetRowKey(gvEkzekutimi.GetFocusedRowIndex()));
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvEkzekutimi, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvEkzekutimi, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvEkzekutimi, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvEkzekutimi, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvEkzekutimi, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    myMenu.menu_click_regjistrime(s, e, "Shto_Ekzekutim.aspx?shtim_modifikim=shtim&id=0", 'Shto_Ekzekutim.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvEkzekutimi.GetRowKey(gvEkzekutimi.GetFocusedRowIndex()));
    if (e.item.name == "Eksporto") {
        e.processOnServer = false;
        gvEkzekutimi.GetSelectedFieldValues('IdKokaEkzekutim', OnGridSelectionCompleteEksport);
    }
}

function OnGridSelectionCompleteEksport(values) {
    myButtonClickLupa.ButtonClickLupaEksporto(values, 'ekzekutimProdhimi', 45, 'Ekzekutim Prodhimi', 'Format Standart Ekzekutim Prodhimi');
}

var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {

        myMenu.ShikoClick(editor, 'Shto_Ekzekutim.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvEkzekutimi.GetRowKey(gvEkzekutimi.GetFocusedRowIndex()));
    } else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvEkzekutimi.GetFocusedRowIndex();

    gvEkzekutimi.GetRowValues(indexModifiko, 'IdKokaEkzekutim;NrDok', OnGetRowValues);
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
    callWebserviceKonfigurimi("805", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvEkzekutimi.PerformCallback(idKomp + ";" + kodKonf);
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