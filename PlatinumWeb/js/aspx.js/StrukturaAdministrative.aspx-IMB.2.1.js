;
var editmode = false;
var indexEdit = -1;
var indexModifiko;
//per filtrat
///perdoret per te aktivizuar butonat ruaj dhe fshi tek filtrat
function checkText(s, e) {//po
    myMenu.checkText(s, e);
}

var btnFiltrat;
///aplikon filtrat
function aplikoFiltra(s, e) {//po
    btnFiltrat = s;
    $('#hfRuaj').val('Filtra');
    myMenu.aplikoFiltra(s, e, gvKodifikimArtikulli, "", '');
}
///perdoret per te aktivizuar butonat ruaj dhe fshi tek filtrat
function textChanged(s, e) {//po
    myMenu.textChanged(s, e);
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKodifikimArtikulli, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKodifikimArtikulli, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKodifikimArtikulli, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKodifikimArtikulli, indexSel);
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
//kontrollon nese perdoruesi ka te drejta per kete veprim
function callWebservice() {
    var emer = 'StrukturaAdministrative.aspx';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result === "true") {
        indexModifiko = trlStruktura.GetFocusedNodeKey();
        switchEditMode(indexModifiko);
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get('msgNukKeniDrejtaPerVeprim'));
    }
}
//editon reshtin kur ben double click
function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    trlStruktura.StartEdit(index);
    indexEdit = index;
}

function Qendra_Click() {
    if (LlojQendre.GetValue() == 1 || LlojQendre.GetValue() == hfState.Get('cmbVleraQendraKosto'))
        myButtonClickLupa.LupaUniversal_Click(hfState.Get('headerTextPopUpStrukturaAdministrative'), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Struktura', 900, 600);
    else myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerTextPopSkemaQKStrukturaAdministrative"), 'LupaSkemaKosto.aspx?vjenNga=Struktura', 600, 600);
}
function Personi_Click() {
     myButtonClickLupa.LupaUniversal_Click("Zgjidhni punonjesin", 'LupaPunonjes.aspx?vjenNga=Struktura', 600, 600);
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

//thirret ne loading te faqes
function Init() {
    changeName();
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
}
//thirret kur ndodh nje event ne server postback
function EndRequestHandler(sender, args) {
    enable();
}
//shfaq emrin e komponentes tek faqja
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('StrukturaAdministrative.aspx', 0, hf);

   // ndryshoKonfigurimin();
}
//ben enabled disabled kodin nese jemi ne shtim apo modifikim
function enable() {
    try {
        editorKodi = Utils.ktheKontroll('Kodi');
        if ($("#hfRuaj").val() === "Modifiko") {
            editorKodi.SetEnabled(false);
           
        }
        else {
            hfNrAuto.Clear(); hfNrAutoKF.Clear();
            editorKodi.SetEnabled(true); myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto, new Date());
        }
    } catch (ex) {
    }
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    //            cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(701, cmbKonfigurimi.GetText());
    //var grida = $('#rowed5');
    //ndryshoKonfigFormatNumri(grida);

}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimi("701", cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    trlStruktura.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
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
                nrauto=  colKushte[j].Vlera;
                
            }

            
        }
    }
}
//veprimet e menuse
function menu_click(s, e) {//po
    var hfRuaj = $('#hfRuaj')
    var arr = trlStruktura.GetVisibleSelectedNodeKeys();
    if (e.item.name == 'Arkiva') {
        ButtonClickArkiva();
        e.processOnServer = false;
    }
    lblMsgbox.SetText("Jeni i sigurt?");
    //for (var i = 0; i < arr.length; i++)
    //    if (trlStruktura.GetNodeState(arr[i]) != 'Child') {
    //        lblMsgbox.SetText("Duke fshire prindin, fshihen edhe te gjithe femijet e tij. Jeni i sigurt?");
    //        break;
    //    }
    myMenu.menu_click_celjevogeltree(s, e, hfRuaj, trlStruktura, hfTeDrejta);

}

function ButtonClickArkiva() {
    var idObjekti = ($('#hfRuaj').val() == "Modifiko" || Utils.IsNullOrEmpty($('#hfRuaj').val())) ? parseInt(trlStruktura.GetFocusedNodeKey()) : 0;
    if (idObjekti == -1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgStrukturaAdministrativeZgjidhniNje"));
        return;
    }

    Utils.hapLupe({ emerPopUpi: popupUniversal, titull: hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"), baseUrl: "LupaArkiva.aspx", width: 738, height: 548, params: { vjenNga: "Lista", veprimi: "hierarki", idDok: idObjekti, tmpfolder: hfArkiva.Get("rootFolder") } });
}

//ne fund te callbackut te grides per te shfaqur mesazhet
function endcallback(s, e) {
    enable();
    if (s.name == 'trlStruktura' && !trlStruktura.IsEditing()) {
        $('#hfRuaj').val('');
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    }
    if (s.GetErrorCell() === null || s.GetErrorCell().innerHTML === "")
    //              if ($('#hfRuaj').val() === 'Modifiko') {
    //                  btn.DoClick();
    //              }
    //              else if ($('#hfRuaj').val() === 'Ruaj') {
    //                  btn.DoClick();
    //              }
        $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green") {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    }
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}

function SucceededCallbackQendra(result) {
    Qendra.ClearItems();
    if (result.length != 0) {
        for (i = 0; i < result.length; i++) {
            if (LlojQendre.GetValue() == "1")
                Qendra.AddItem(result[i].Kodi, result[i].Id); //AddItem(kodi, id);
            else Qendra.AddItem(result[i].Kodi, result[i].IdKoka); //AddItem(kodi, id);
        }
    }
}

function Qendra_Changed() {
    $('#hfQendra').val(Qendra.GetText());
}
function Personi_Changed() {
    $('#hfPersoni').val(Personi.GetText());
}
function SelectedIndexChangedHtmlRowPrepared(s, e) {
    Qendra.SetText('');
    $('#hfLloji').val(LlojQendre.GetValue()); 
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrQendraKosto"),
        data: JSON.stringify({ lloji: LlojQendre.GetValue() })
    }).done(SucceededCallbackQendra);
}

function Aktiv_Changed(s, e) {
    $('#hfAktiv').val(s.GetValue());
}