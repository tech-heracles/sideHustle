function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvProfesione, "719", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function EndCallbackGrida(s, e) {
    enable();
    $.ajax({        
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}
function enable() {//kur humb fokusin kolona Autorizimeve      
    
        
        hfNrAuto.Clear(); hfNrAutoKF.Clear();
        if (Utils.getUrlVar('vjenNga') == 'PunonjesProfesione') myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto, new Date());
        else if (Utils.getUrlVar('vjenNga') == 'PunonjesTitujPune') myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto2, new Date());

        var editorAktiv = Utils.ktheKontroll('Aktiv');
        editorAktiv.SetChecked(true);
   
}

function callWebserviceKonfigurimi() {
  
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdorues = hfState.Get('idPerdoruesi');
    $.ajax({
       pritPergjigje: true,
       url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
       data: JSON.stringify({ idKomp: 718, kodKonf: "PTP", kodKontrolli: " ", idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdorues, idNdermarrje: idNdermarrje })
    }).done(SucceededCallbackKonfig);
}
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
function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}
$(window).on('unload', function () {
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvProfesione.SelectRowOnPage(0, true);
        gvProfesione.SetFocusedRowIndex(0);
     
        callWebserviceKonfigurimi();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvProfesione.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvProfesione.GetVisibleRowsOnPage() - 1) {
            gvProfesione.SetFocusedRowIndex(0);
        }
        else {
            gvProfesione.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvProfesione.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvProfesione.GetRowValues(index, 'Kodi;Pershkrimi;PershkrimiAng;Id', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var emri; emri = values[1];
        var kodi; kodi = values[0];
        var kosto; kosto = values[2];
        if (Utils.getUrlVar('vjenNga') == 'PunonjesProfesione') {
            
            window.parent.cmbProfesioni.SetText(emri);
                    
                window.parent.cmbProfesioni.SetFocus();
        }
       else if (Utils.getUrlVar('vjenNga') == 'PunonjesTitujPune') {
           if (hfState.Get('idGjuha') == 1)
               window.parent.cmbTitull.SetText(kosto);
         else  window.parent.cmbTitull.SetText(emri);

           window.parent.cmbTitull.SetFocus();
       }
       else if (Utils.getUrlVar('vjenNga') == 'Raporti') {
           window.parent.editorGlobal.SetText(kodi + ";" + emri);
           window.parent.editorGlobal.SetFocus(true);
       }
        else if (window.parent.identikuesPerPopupBurimi == 'Raporti') {
            window.parent.editorGlobal.SetText(kodi);
            window.parent.editorGlobal.SetFocus(true);
        }
        
    }
    window.parent.popupUniversal.Hide();
}
function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvProfesione.GetFocusedRowIndex());
    }
    else if (e.item.name == 'Shto') {

        gvProfesione.AddNewRow();
        e.processOnServer = false;
        
    }  else if (e.item.name == 'Anullo') {

        window.parent.popupUniversal.Hide();
    }
}


function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvProfesione.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvProfesione.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');