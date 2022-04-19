; var editmode = false;
var indexEdit = -1;
var resultkonf;
var colKontrollet, colAtrTrupi;
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;

$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                try { window.parent.rifresko = true; } catch (e) { }
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    try { window.parent.rifresko = true; } catch (e) { }
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
    });
});

// kur faqja lodohet
function Init() {
    changeName();

    editmode = false; menuSipasTeDrejta(modifiko, hfTeDrejta);
    indexEdit = -1;
    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    try {
        if (Utils.getUrlVar('vjenNga') !== 'GIS')
        myFaqeCelje.shtoHandlerSession();
        gvSeriale.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("445", cmbKonfigurimi.GetText());
    ndryshoKonfigurimFushaShtese(cmbKonfigurimi.GetValue());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("445", cmbKonfigurimi.GetText());
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Seriale.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
    menuSipasTeDrejta(modifiko, hfTeDrejta);
}

//document.onkeydown = ProcessKeyPress;
var modifiko = false;
//kur kthehet nje veprim postback nga serveri
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar    
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvSeriale, "445", pastrofusha, hfTeDrejta);
    menuSipasTeDrejta(modifiko, hfTeDrejta);
}

//kthimi i fokusit ne faqe te pare
//function ProcessKeyPress() {
//    var currentIndex = gvSeriale.GetFocusedRowIndex();
//    if (event.keyCode == 40) {
//        if (currentIndex == gvSeriale.GetVisibleRowsOnPage() - 1) {
//            gvSeriale.SetFocusedRowIndex(0);
//        }
//        else {
//            gvSeriale.SetFocusedRowIndex(currentIndex + 1);
//        }
//    }
//    if (event.keyCode == 38) {
//        if (currentIndex == 0) {
//            return;
//        }
//        else {
//            gvSeriale.SetFocusedRowIndex(currentIndex - 1);
//        }
//    }
//    if (event.keyCode == 13) {
//        OnGridSelectionChanged();
//    }
//}

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    }
    catch (e) { }
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

// kur ndryshon select 
function OnGridSelectionChanged() {
    //gvSeriale.GetSelectedFieldValues('NrGrupBanke', OnGridSelectionComplete);
  if(Utils.getUrlVar('idartikulli')==0)
      gvSeriale.GetSelectedFieldValues('AqtSerialKod;AqtSerialPershkrim', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var kodi = '';
    for (var i = 0; i < values.length; i++) {
        if (kodi == '')
            kodi = values[i][0];
        else
            kodi = kodi + ',' + values[i][0];
    }
    window.parent.editorGlobal.SetText(kodi);
    window.parent.editorGlobal.Focus();
    window.parent.popupUniversal.Hide();
}

function menuSipasTeDrejta(modifiko, hfTeDrejta) {
    var idartikulli = Utils.getUrlVar("idartikulli");
    if (idartikulli != 0) {
        ASPxMenu1.GetItemByName('OK').SetVisible(false);
        ASPxMenu1.GetItemByName('Anullo').SetVisible(false);
        ASPxMenu1.GetItemByName('TemplatedItemFilter').SetVisible(false);
    }
    else {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        ASPxMenu1.GetItemByName('Shto').SetVisible(false);
        ASPxMenu1.GetItemByName('Modifiko').SetVisible(false);
        ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
        ASPxMenu1.GetItemByName('Pastro').SetVisible(false);
    }
    if (hfTeDrejta.Get('Shtim') == true && modifiko == false) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);

    }
    else if (hfTeDrejta.Get('Modifikim') == true && modifiko == true) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }
    else {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);

    } if (hfTeDrejta.Get('Modifikim') == true)
        ASPxMenu1.GetItemByName('Modifiko').SetEnabled(true);
    else ASPxMenu1.GetItemByName('Modifiko').SetEnabled(false);
}

function switchEditMode(index) {//kalon ne edit grida

    //menuSipasTeDrejta(modifiko, hfTeDrejta);
    //if (editmode) {
    //    if (index == indexEdit) {
    //        gvSeriale.CancelEdit();
    //        editmode = !editmode;
    //    }
    //    else {
    //        gvSeriale.StartEditRow(index);
    //        indexEdit = index;
    //    }
    //}
    //else {
    //    gvSeriale.StartEditRow(index);
    //    indexEdit = index;
    //    editmode = !editmode;
    //}
}

function EndCallbackGrida(s, e) {
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvSeriale, "445", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

//function menu_click(s, e) {
//    if (e.item.name == 'Fshi') {
//        popFshi.Show();
//        e.processOnServer = false;
//    }
//    if (e.item.name == 'OK') {
//        e.processOnServer = false;
//        OnGridSelectionChanged();
//    }
//    else if (e.item.name == 'Anullo') {
//        window.parent.popupUniversal.Hide();
//    }
//    if (e.item.name == 'Ruaj') {
//        gvSeriale.UpdateEdit();
//        e.processOnServer = false;
//        modifiko = false;
//    }
//    else if (e.item.name == 'Modifiko') {
//        modifiko = true;
//    }
//    else
//        modifiko = false;
//    menuSipasTeDrejta(modifiko, hfTeDrejta);
//}

function menu_click(s, e) {

    //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar   
    if (e.item.name == "Shto")
        $('#hfShtimModifikim').val("shtim");
    hfShtimModifikim = $('#hfShtimModifikim');
    if (e.item.name == 'OK') {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    else if (e.item.name === 'Arkiva') {
        if ($('#hfShtimModifikim')[0].value == "modifikim" && gvSeriale.GetRowKey(gvSeriale.GetFocusedRowIndex()) == undefined) {

            myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        } else
            ButtonClickArkiva();
        // myFaqeCelje.kontrolloTeDrejta('LupaArkiva.aspx?veprimi=' + Utils.getUrlVar('shitje_blerje') + '&idDok=' + grid_RegDok.GetRowKey(grid_RegDok.GetFocusedRowIndex()) + '&shtim_modifikim=modifikim');
        e.processOnServer = false;
    }
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    ruajFushaShtese();
}
function ButtonClickArkiva() {//po
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);
    //popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=seriale&idDok=' + gvSeriale.GetRowKey(gvSeriale.GetFocusedRowIndex()) + '&shtim_modifikim=modifikim');
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=seriale&idDok=' + gvSeriale.GetRowKey(gvSeriale.GetFocusedRowIndex())
        //+ '&shtim_modifikim=modifikim'
        + "&tmpfolder=" + hfArkiva.Get("rootFolder"));
    popupUniversal.Show();
}
function onActiveTabChanged(s, e) {
    indexModifiko = gvSeriale.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim';
            $('#hfId')[0].value = 0;
            pastrofusha();

        }
    }
    kaloTab = false; //ishte false    
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
    menuSipasTeDrejta(modifiko, hfTeDrejta);
}

function OnGridDoubleClick(index) {
    indexModifiko = gvSeriale.GetFocusedRowIndex();
    lista = true;
    mbushfusha();
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvSeriale.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje serial");
    else
        gvSeriale.GetRowValues(indexModifiko, 'IdAQTSerial;AqtSerialKod;AqtSerialPershkrim', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi.SetText(values[1]);
    txtPershkrimi.SetText(values[2]);
    if ($('#hfShtimModifikim').val() == "modifikim")
        txtKodi.SetEnabled(false);
    RefreshFushatShtese($('#hfId').val(), 'mod');
    Utils.hiqLoadingGif();;
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
        menuSipasTeDrejta(modifiko, hfTeDrejta);
    }
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblSeriale', 'tblFushatShtese'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshemSeriale(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
        if (Utils.getUrlVar('vjenNga') == 'GIS' && Utils.getUrlVar('idSeriali') !== '' && typeof hfState.Get("idNjesiPerSelektim") !== "undefined") {
            gvSeriale.SelectRowOnPage(hfState.Get("idNjesiPerSelektim"));
            gvSeriale.SetFocusedRowIndex(hfState.Get("idNjesiPerSelektim"));
            kaloTab = true;
            lista = true;
            mbush = false;
            $('#hfShtimModifikim')[0].value = "modifikim";
            gvSeriale.GetRowValues(hfState.Get("idNjesiPerSelektim"), 'IdAQTSerial;AqtSerialKod;AqtSerialPershkrim', OnGetRowValuesMod);
            Utils.shfaqLoadingGif();;
            if ($('#hfShtimModifikim').val() == "modifikim")
                
            RefreshFushatShtese($('#hfId').val(), 'mod');
        }
    }
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    gvSeriale.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvSeriale").show()
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({        
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function pastrofusha() {
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    menuSipasTeDrejta(modifiko, hfTeDrejta);
    PastroFushatShtese();
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}