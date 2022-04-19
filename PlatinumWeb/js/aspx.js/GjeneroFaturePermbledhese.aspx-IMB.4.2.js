; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;
var pageState;
var interval;


//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_RegDok, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_RegDok, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_RegDok, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_RegDok, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_RegDok, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/

function Intervali() {
    window.parent.SessionTimeout.sendKeepAliveImmediately()
}

function menu_click(s, e) {
    Utils.shfaqLoadingGif();
    if (e.item.name == "Gjenero")
        interval = setInterval(Intervali, 600000);
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();;
        return;
    }
    if (grid_RegDok.GetVisibleRowsOnPage() == 0) {
        myMesazh.ShtoMesazhGabimi('Nuk ka fature per te gjeneruar fature permbledhese!');
        e.processOnServer = false;
    }

}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
}



var mbush = false;

function clickExport(e) {
    if (grid_RegDok.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje nga elementet e listes!');
        e.processOnServer = false;
    }
}
var editor;
var focuschange = false;


$(document).ready(function () {
    $(document).keydown(function (e) {//po
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 120:
                e.preventDefault();
                e.item = {};
                e.item.name = 'Ruaj';
                e.processOnServer = true;
                var sender = 'tastiera';
                menu_click(sender, e);
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
    myFaqeCelje.changeName('GjeneroFaturePermbledhese.aspx', 0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    $('#dvMenu').show();
}

var indexModifiko;

function OnError(message, context) {
    if (message == "Session TimeOut")
        ndryshoUrlFrame(Paths.defaultLoginPath);
}



function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function EndCallback(s, e) {
    var mesazhi = Utils.MerrMesazhNgaGrida(s);
    if (mesazhi.Kodi !== 1000) {
        if (mesazhi.Status) {
            myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
        } else {
            myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
        }
    }
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("543", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {

}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    grid_RegDok.PerformCallback(idKomp + ";" + kodKonf);
}

var click = false;
function EndRequestHandler(sender, args) {
    var hf = document.getElementById("status1");
    var status = hf.value;
    if (status == "true" ) {
        click = false;
    }
    else
        click = false; //kur nuk ruhet per ndonje arsye i japim mundesi te riruaj
    clearInterval(interval);
    PrintPreview("cpHapFaqe");
}

function PrintPreview(propertyName) {
    var faqe = grid_RegDok[propertyName];
    if (faqe) {
        delete grid_RegDok[propertyName];
        window.open(faqe + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
    }
}


function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Date_Changed(s, e) {
    if (txtDeriDok.GetDate() < txtNgaDok.GetDate())
        txtDeriDok.SetDate(txtNgaDok.GetDate());
}



