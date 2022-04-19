; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvProfesione";
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
    if (grida == "gvProfesione")
        myMenu.aplikoFiltra(s, e, gvProfesione, "", '');
    else if (grida == "gvtituj")
        myMenu.aplikoFiltra(s, e, gvtituj, "", '');

}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
///kur ben double click
function RowDblClickGrida2(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvtituj';
}
function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvProfesione';
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvProfesione, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvProfesione, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvProfesione, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvProfesione, indexSel);
}

//kur ndryshon tabin del nga editimi
function ndryshimTabi(tab) {

    if (tab.index == 0) {
        grida = "gvProfesione";
        if (gvtituj.IsEditing() == true)
            gvtituj.CancelEdit();
       
    }
    else {
        grida = "gvtituj";
        if (gvProfesione.IsEditing() == true)
            gvProfesione.CancelEdit();
       
    }
    
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
/// kontrollon nqs ke te drejta per te kryer nje veprim
function callWebservice() {

    var emer = 'ProfesioneTitujPune.aspx';
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
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniDrejtaPerVeprim"));
    }
}
function SucceededCallback1(result) {
    if (result == false) {
         if (grida == "gvProfesione")
             gvProfesione.StartEditRow(indexEdit);
    else if (grida == "gvtituj")
        gvtituj.StartEditRow(indexEdit);
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonProfesionPozicion"));
    }
}
// kalon griden ne edit mode
function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    if (grida == "gvProfesione") {
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "KaVeprimeProfesioni"),
            data: JSON.stringify({ id: gvProfesione.GetRowKey(gvProfesione.GetFocusedRowIndex()) })
        }).done(SucceededCallback1);
    }
    else {
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "KaVeprimeProfesioni"),
            data: JSON.stringify({ id: gvtituj.GetRowKey(gvtituj.GetFocusedRowIndex()) })
        }).done(SucceededCallback1);
    }

    indexEdit = index;
   
 
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
    myMesazh.shtoHandler();
}
///shfaq emrin e faqes
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeNameRegjistrime('ProfesioneTitujPune.aspx', 0);

}

var editorKodi;

/// ben enable disable kodin 
function enable() {//kur humb fokusin kolona Autorizimeve      
    if ($("#hfRuaj").val() == "Modifiko") {
        editorKodi = Utils.ktheKontroll('Kodi');
        editorKodi.SetEnabled(false);
    }
    else {

        editorKodi = Utils.ktheKontroll('Kodi');
        editorKodi.SetEnabled(true);
        hfNrAuto.Clear(); hfNrAutoKF.Clear();
        if (grida == "gvProfesione") myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto, new Date());
        else if (grida == "gvtituj") myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto2, new Date());

        editorAktiv = Utils.ktheKontroll('Aktiv');
        editorAktiv.SetChecked(true);
    }
}

///verprimet e menu clickut

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (e.item.name == 'Modifiko') {
        hfRuaj.val('Modifiko');
        s.GetItemByName('Ruaj').SetVisible(true);
   
        if (grida == "gvProfesione") {
            $.ajax({               
                url: Utils.getServerApiUrl("Konfigurime", "KaVeprimeProfesioni"),
                data: JSON.stringify({ id: gvProfesione.GetRowKey(gvProfesione.GetFocusedRowIndex()) })
            }).done(SucceededCallback1);
            indexEdit = gvProfesione.GetFocusedRowIndex();
        }
        else {
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "KaVeprimeProfesioni"),
                data: JSON.stringify({ id: gvtituj.GetRowKey(gvtituj.GetFocusedRowIndex()) })
            }).done(SucceededCallback1);
            indexEdit = gvtituj.GetFocusedRowIndex();
        }
    
        e.processOnServer = false;
    } else {
        if (grida == "gvProfesione")
            myMenu.menu_click_celjevogel(s, e, hfRuaj, gvProfesione, hfTeDrejta);
        else if (grida == "gvtituj")
            myMenu.menu_click_celjevogel(s, e, hfRuaj, gvtituj, hfTeDrejta);
    }
   

    

}

function EndCallbackGrida(s, e) {
    myMesazh.ShtoMesazhNgaGrida(s);
    enable();
    //$.ajax({
    //    url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
    //    data: JSON.stringify({})
    //}).done(SucceededCallbackMesazhi);
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
    callWebserviceKonfigurimi(718, cmbKonfigurimi.GetText());
    //var grida = $('#rowed5');
    //ndryshoKonfigFormatNumri(grida);

}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimi("718", cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    gvProfesione.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdorues = hfState.Get('idPerdoruesi');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: " ", idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdorues, idNdermarrje: idNdermarrje })
    }).done(SucceededCallbackKonfig);
}
function EndRequestHandler(s, e) { }
var resultkonf;
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
var nrauto = 0;
var nrauto2 = 0;
function SucceededCallbackKonfig(result) {
    nrauto = 0;
    nrauto2 = 0;
    if (result != "" && result != null) {
        colKushte = result.colKushte; 
        for (j = 0; j < colKushte.length; j++) {
            if (colKushte[j].Kodi == 'NrAutoKodi') {
                nrauto = colKushte[j].Vlera;

            }
            if (colKushte[j].Kodi == 'NrAutoKodi2') {
                nrauto2 = colKushte[j].Vlera;

            }

        }
    }
}
function End_Callback(s, e) {
    myMesazh.ShtoMesazhNgaGrida(s);
    enable()
    if($('#hfRuaj')[0].value=='Modifiko') 
    { btn.DoClick(); }
    else if($('#hfRuaj')[0].value=='Ruaj') 
    { btn.DoClick(); }
} 