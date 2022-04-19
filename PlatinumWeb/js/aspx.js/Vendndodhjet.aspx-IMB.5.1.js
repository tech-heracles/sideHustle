; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvVendndodhjet";
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
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
    $(window).on('load', function () {
        Init();
    });
});

var btnFiltrat;
///aplikon filtrat ne gride
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val('Filtra');
         myMenu.aplikoFiltra(s, e, gvVendndodhjet, "", '');
   

}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvVendndodhjet';
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvVendndodhjet, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvVendndodhjet, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvVendndodhjet, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvVendndodhjet, indexSel);
}




function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
/// kontrollon nqs ke te drejta per te kryer nje veprim
function callWebservice() {
    var emer = 'Vendndodhjet.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko, grida);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}
// kalon griden ne edit mode
function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KaVeprimeVendndodhje"),
        data: JSON.stringify({ id: gvVendndodhjet.GetRowKey(gvVendndodhjet.GetFocusedRowIndex()) })
    }).done(SucceededCallback1);

   
   

    indexEdit = index;
}
function SucceededCallback1(result) {
    if (result == false) {
            gvVendndodhjet.StartEditRow(indexEdit);
       
    }
    else {
        myMesazh.ShtoMesazhGabimi("Ka veprime me kete profesion/pozion!");
    }
}
function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
///shfaq emrin e faqes
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Vendndodhjet.aspx', 0, hf);

}
function EndRequestHandler() {

}

var editorKodi;

/// ben enable disable kodin 
function enable() {//kur humb fokusin kolona Autorizimeve      
    if (gvVendndodhjet.IsEditing()) {
        editorKodi = Utils.ktheKontroll('Kodi');
        if (editorKodi == false) return;
        if ($("#hfRuaj").val() == "Modifiko") {

            editorKodi.SetEnabled(false);
        }
        else {


            editorKodi.SetEnabled(true);
            hfNrAuto.Clear(); hfNrAutoKF.Clear();
            myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto, new Date());


            editorAktiv = Utils.ktheKontroll('Aktiv');
            editorAktiv.SetChecked(true);
        }
    }
}

///verprimet e menu clickut

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (e.item.name == 'Modifiko') {
        hfRuaj.val('Modifiko');
        s.GetItemByName('Ruaj').SetVisible(true);

        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KaVeprimeVendndodhje"),
            data: JSON.stringify({ id: gvVendndodhjet.GetRowKey(gvVendndodhjet.GetFocusedRowIndex()) })
        }).done(SucceededCallback1);
        indexEdit = gvVendndodhjet.GetFocusedRowIndex();
      
        e.processOnServer = false;
    } else {
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvVendndodhjet, hfTeDrejta);
    }
}

function EndCallbackGrida(s, e) {
    enable();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    //            cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(720, cmbKonfigurimi.GetText());
    //var grida = $('#rowed5');
    //ndryshoKonfigFormatNumri(grida);

}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimi("720", cmbKonfigurimi.GetText());
}
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    gvVendndodhjet.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdorues = hfState.Get('idPerdoruesi');
    $.ajax({
       pritPergjigje: true,
       url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
       data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: " ", idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdorues, idNdermarrje: idNdermarrje })
   }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
var nrauto = 0;
function SucceededCallbackKonfig(result) {
    nrauto = 0;
    if (result != "" && result != null) {
        colKushte = result.colKushte;  
        for (j = 0; j < colKushte.length; j++) {
            if (colKushte[j].Kodi == 'NrAutoKodi') {
                nrauto = colKushte[j].Vlera;

            }
            

        }
    }
}