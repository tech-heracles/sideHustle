; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var indexSel = 0;

//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var grida = "gvGrupime";
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
    if (grida == "gvGrupime")
        myMenu.aplikoFiltra(s, e, gvGrupime, "", '');
    else if (grida == "gvGlobale")
        myMenu.aplikoFiltra(s, e, gvGlobale, "", '');

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
    grida = 'gvGlobale';
}
function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvGrupime';
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvGrupime, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvGrupime, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvGrupime, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvGrupime, indexSel);
}




function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
/// kontrollon nqs ke te drejta per te kryer nje veprim
function callWebservice() {
    var emer = 'GrupimeLocaleGlobale.aspx';
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
// kalon griden ne edit mode
function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    if (grida == "gvGrupime")
        gvGrupime.StartEditRow(index);
    else if (grida == "gvGlobale")
        gvGlobale.StartEditRow(index);

    indexEdit = index;
}

function Init() {
    changeName(); myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
///shfaq emrin e faqes
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('GrupimeLocaleGlobale.aspx', 0, hf);

}


var editorKodi;

/// ben enable disable kodin 
function enable() {//kur humb fokusin kolona Autorizimeve      
    if ($("#hfRuaj").val() == "Modifiko") {
        editorKodi = Utils.ktheKontroll('Kodi');
        if (!editorKodi) return;
        editorKodi.SetEnabled(false);
    }
    else {

        editorKodi = Utils.ktheKontroll('Kodi');
        if (!editorKodi) return;
        editorKodi.SetEnabled(true);
        hfNrAuto.Clear(); hfNrAutoKF.Clear();
        if (grida == "gvGrupime") myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto, new Date());
        else if (grida == "gvGlobale") myNrAuto.vendosNrAutomatikKodi('Kodi', nrauto2, new Date());

        editorAktiv = Utils.ktheKontroll('Aktiv');
        if (!editorAktiv) return;
        editorAktiv.SetChecked(true);
    }
}

///verprimet e menu clickut

function menu_click(s, e) {
    //var hfRuaj = $('#hfRuaj');
    //if (grida == "gvGrupime")
    //    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvGrupime, hfTeDrejta);
    //else if (grida == "gvGlobale")
    //    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvGlobale, hfTeDrejta);

    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //        myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);


}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvGrupime.GetFocusedNodeKey();
    if (indexModifiko == '' || indexModifiko == 0)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgQKDuhetTeZgjdhni1Qender"));
    else
        gvGrupime.GetNodeValues(indexModifiko, 'Id;Kodi;Pershkrimi;Aktiv;IdPrindi;Prindi', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }
    txtEmertimi.SetText(values[2]);
    cbAktiv.SetChecked(values[3]);
  
    if (values[4] !== "" && values[4] !== null)
      
      cmbPrindi.SetSelectedIndex(cmbPrindi.AddItem(values[5], values[4]));
    else cmbPrindi.SetText('');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "kaVeprimeGrupimi"),
        data: JSON.stringify({ id: values[0] })
    }).done(SucceededCallbackLidhur);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result) {
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    //        aktivizoFusha(hf.value);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result));
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
   
    cbAktiv.SetChecked(true);
    cmbPrindi.SetText('');
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}
function EndCallbackGrida(s, e) {
    enable();
    $.ajax({        
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    gvGrupime.SelectNode(gvGrupime.GetFocusedNodeKey());
    indexModifiko = index;
    lista = true;
    mbushfusha();
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
    callWebserviceKonfigurimi(721, cmbKonfigurimi.GetText());
    //var grida = $('#rowed5');
    //ndryshoKonfigFormatNumri(grida);

}
function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}

function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimi("721", cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    gvGrupime.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdorues = hfState.Get('idPerdoruesi');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: " ", idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi:idPerdorues, idNdermarrje: idNdermarrje})
    }).done(SucceededCallbackKonfig);    
}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}
var resultkonf;
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
var nrauto = 0;
var nrauto2 = 0;
function SucceededCallbackKonfig(result) {
    nrauto = 0;
    nrauto2 = 0;
    if (result != "" && result != null) {
        colKushte = result.colKushte; 
        colKontrollet = result.colKontroll; 
        colAtrTrupi = result.colAtrTrupi; 
        $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
        $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
        var hfMod = $('#hfShtimModifikim');
        if (hfMod.val() == "shtim" ) {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
        //for (j = 0; j < colKushte.length; j++) {
        //    if (colKushte[j].Kodi == 'NrAutoKodi') {
        //        nrauto = colKushte[j].Vlera;

        //    }
        //    if (colKushte[j].Kodi == 'NrAutoKodi2') {
        //        nrauto2 = colKushte[j].Vlera;

        //    }

        //}
           

            resultkonf = result;
            LupaKontrollet(colKontrollet, colAtrTrupi);
            var hf = $('#hfKontrollet');
            var hfLidhur = $("#hfLidhur");
       
            var arrTabela = ['tblQendraKosto'];
            myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
        
    }
}
function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}
function LupaKontrollet(kontrollet, colAtrTrupi) {

}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
} var cmbGlobal = new Object;
function Prindi_Click() {
    cmbGlobal = cmbPrindi;

    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPrindin"), 'LupaGrupimeLocaleGlobale.aspx?vjenNga=PunonjesGlobale', 600, 560);
}
/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvGrupime, "721", pastrofusha, hfTeDrejta, undefined, undefined,false);
}
function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function Node_DblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex); 
    kaloTab=true;
}
function Active_TabChanged(s, e) {
    indexModifiko = gvGrupime.GetFocusedNodeKey();    
    if(mbush)
    { 
        if(indexModifiko !=''&&indexModifiko!==0)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else 
        {   
            mbush = false;
            $('#hfShtimModifikim').val( 'shtim'); 
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab=false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta,$('#hfShtimModifikim'));
                  
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}