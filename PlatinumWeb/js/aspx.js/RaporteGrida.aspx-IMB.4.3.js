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
    myFaqeCelje.changeNameRegjistrime('RaporteGrida.aspx?lloji=' + Utils.getUrlVar('lloji'), 0);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    switch (Utils.getUrlVar('lloji')) {
        case "kastrat":
            $('#filtra').show();
            $('#filtra1').show();
            break;
        case "GjendjaEArtikujveMeSeriale":
            $('#filtra').show();
            lblPeriudha2.SetText("Periudha");
            break;
        case "GjendjaEMagazines":
        case "gjendjaArtikujveIMEI":
        case "gjendjaArtikujveIMEIEkspozitor":
            $('#filtra').show();
            lblPeriudha2.SetText("Datë dokumenti");
            break;
    }
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvRaporti, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvRaporti, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvRaporti, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvRaporti, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvRaporti, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    if (e.item.name == "Shiko" && (Utils.getUrlVar('lloji') == "GjendjaEArtikujveMeSeriale" || Utils.getUrlVar('lloji') == "gjendjaArtikujveIMEI" || Utils.getUrlVar('lloji') == "gjendjaArtikujveIMEIEkspozitor" || Utils.getUrlVar('lloji') == "GjendjaEMagazines")) {
        e.processOnServer = false;
        Utils.RaiseCustomCallbackFiltrimi(gvRaporti);
    }
}

function BeginCallback(s, e) {

    if (e.command === 'APPLYFILTER' && btnFiltrat !== undefined)
        btnFiltrat.SetText('');
}
function endCallback(s, e) {
    if (Utils.getUrlVar('lloji') != "tollon")
        $('#filtra').show();

}
function enter() {
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}


function ndryshoKonfigurimin() {

    callWebserviceKonfigurimi("1004", "");
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvRaporti.PerformCallback(idKomp + ";" + kodKonf);
}
function EndRequestHandler(sender, args) {
    if (Utils.getUrlVar('lloji') != "tollon")
        $('#filtra').show();

}

function clickExport(e) {
    if (gvRaporti.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje element nga lista");
        e.processOnServer = false;
    }
}
function ngaDokDateChanged(s, e) {
    if (txtDeriDok.GetDate() < txtNgaDok.GetDate())
        txtDeriDok.SetDate(txtNgaDok.GetDate());
    gvRaporti.PerformCallback('filtro');

}
function deriDokDateChanged(s, e) {
    gvRaporti.PerformCallback('filtro');
}

function AllowMoving(s, e) {
    if (Utils.getUrlVar('lloji') == "GjendjaEArtikujveMeSeriale") {
        e.allow = false;
    }
}
