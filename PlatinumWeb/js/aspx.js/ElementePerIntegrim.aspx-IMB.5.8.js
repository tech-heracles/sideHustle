; $(document).ready(function (e) {
    changeName();
});
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
var editingIndex = undefined;
var focusedColumn;
var resultkonf;
var colKontrollet, colAtrTrupi;

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvElementePerIntegrim, hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    mbushfusha();
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function menu_click(s, e) {
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //            myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
 

    
   
   
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvElementePerIntegrim.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje pasqyre!');
    else
        gvElementePerIntegrim.GetRowValues(indexModifiko, 'IdElementi;Kodi;Emertimi;Lloji;Aktiv;Shenime;DateRegjistrimi', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
        cbAktiv.SetEnabled(true);
        deDateRegjistrimi.SetEnabled(false);
        cmbLloji.SetEnabled(false);
    } else {
        txtKodi.SetEnabled(true);
        cbAktiv.SetEnabled(true);
        deDateRegjistrimi.SetEnabled(true);
        cmbLloji.SetEnabled(true);
    }

    txtKodi.SetValue(values[1]);
    txtEmertimi.SetValue(values[2]);
    cmbLloji.SetValue(values[3]);
    cbAktiv.SetValue(values[4]);
    memoShenime.SetValue(values[5]);
    deDateRegjistrimi.SetValue(values[6]);
    ///te futen ne callbackpanel qe te mos perdorim dy callback
    gvElementePerIntegrim.PerformCallback(values[0]);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        ASPxMenu1.AdjustControl();
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
        // AktivizoDraft();
    }

    Utils.hiqLoadingGif();;
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetValue("");
    txtEmertimi.SetValue("");
    memoShenime.SetValue("");
    //cmbLloji.SetValue(0);
    cbAktiv.SetValue(true);
    deDateRegjistrimi.SetValue(new Date());
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
   
  
    
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
   
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    gvElementePerIntegrim.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvDetyra").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}




function SucceededCallbackKonfig(result) {
    if (!result.d && !result)
        return;
    if (result && result.d)
        result = result.d;
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        //  LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblPasqyra'];
        var arrPrind = ['divFillim'];

        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));

        $("#divFillim").show();
        // $("#divGrida").width($("#tblPasqyra").width() + "%");
        $("#divGrida").show();
        //$("#divFundi").show();

        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoDet.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
            
        }
        //AktivizoDraft();
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    if (hfMod.val() != "klonim") {
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar");
    if (hf !== null) {
        lblKonfigurimi.SetText(hf.val().split(';')[1]);
        cmbKonfigurimi.SetText(hf.val().split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
    myFaqeCelje.changeName(hfState.Get('komponente'), 0, null);
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusVeprimi");
    var colKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvElementePerIntegrim, hfState.Get("idKomponente"), pastrofusha, hfTeDrejta)
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
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

function PageControlTabChanging(s, e) {
    indexModifiko = gvElementePerIntegrim.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);

            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));

    ASPxMenu1.AdjustControl();
}

function gvEndCallback(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}
function SucceededCallbackMesazhi(result) {
    if (result && result.d)
        result = result.d;
    if (result.length == undefined)
        return;

    //if (result != null)
    {
        var arr = result.split(':');
        if (arr[1] == "Green") {
            myMesazh.ShtoMesazhSuksesi(arr[0]);
            gvElementePerIntegrim.Refresh();
            PageControl.SetActiveTabIndex(0);
        }
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    }
    Utils.hiqLoadingGif();;
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function Row_DblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex);  
    kaloTab=true;
}
function Init_deDateRegjistrimi(s, e) {
    s.SetDate(new Date());
}