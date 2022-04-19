; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;
var kaTeDrejteMod;

var indexModifiko;
var focuschange = false;
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('KonfigurimFormatImporti.aspx',0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}
function OnGetRowValues(values) {
    ASPxMenu1.GetItemByName("Klono").SetEnabled(values[2]);
    ASPxMenu1.GetItemByName("Shiko").SetEnabled(values[3]);
    ASPxMenu1.GetItemByName("Fshi").SetEnabled(values[4]);
   

    kaTeDrejteMod = values[3];
    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush && kaTeDrejteMod) myMenu.ShikoClick(editor, 'Shto_KonfigurimFormatImporti.aspx?shtim_modifikim=modifikim&numer=' + values[1] + '&indexrow=' + indexModifiko + '&id=' + values[0]);
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKonfigurim, "", "");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
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
                break;
            default:
                break;
        }
    });
    changeName();
});

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKonfigurim, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKonfigurim, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKonfigurim, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKonfigurim, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {

    
    myMenu.menu_click_regjistrime(s, e, "Shto_KonfigurimFormatImporti.aspx?shtim_modifikim=shtim&id=0", 'Shto_KonfigurimFormatImporti.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + id);

    if (e.item.name === 'Klono') {
        if (id === null)
            myMesazh.ShtoMesazhGabimi("Nuk keni asnje dokument te zgjedhur!");
        else
            myFaqeCelje.kontrolloTeDrejta('Shto_KonfigurimFormatImporti.aspx?shtim_modifikim=klonim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + id);
        e.processOnServer = false;
    }
    if (e.item.name === "Eksporto") {
        e.processOnServer = false;
        myButtonClickLupa.LupaUniversal_Click('Eksportimi', 'LupaEksportim.aspx?id=' + id + '&komponente=KonfigurimFormatImporti.aspx', 600, 400);
    }

}
var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange && !kaTeDrejteMod) {
        myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta per kete veprim!");
        return; //nese bej dblClick ne te njejtin rresht ku skam te drejta
    }

    if (!focuschange) {

        myMenu.ShikoClick(editor, 'Shto_KonfigurimFormatImporti.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + id);
    } else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvKonfigurim.GetFocusedRowIndex();

    gvKonfigurim.GetRowValues(indexModifiko, 'IdKoka;Kodi;D_SHTIM;D_MOD;D_FSH', OnGetRowValues);
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
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("174", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvKonfigurim.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}