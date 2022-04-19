



$(document).ready(function () {//po
    $("#dvArtikulli").hide();
    $(window).load(function () {

        gvLupaArtikull.SetWidth(document.documentElement.clientWidth - 20);
        Init();

    });
    $(window).on('unload', function () {
        //PlatinumWeb.wsfunc.fshiGrideNgaSessioniLupa(window.parent.myWS.fshiSessionFailCheck, window.parent.myWS.webServiceFail);
    });
    $(window).bind('resize', function () {//po
        try {
            //document.getElementById("menu").style.width = document.documentElement.clientWidth - 50;
            gvLupaArtikull.SetWidth(document.documentElement.clientWidth - 20);
        }
        catch (e) {
        }
    }).trigger('resize');
    document.onkeydown = ProcessKeyPress;
});


function Init() {

    $('#hfIdMag').val(window.parent.btnMagazina.GetValue());
    $('#hfData').val(window.parent.data_DateEdit.GetText());
    window.parent.myFaqeCelje.shtoHandlerSession();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    if (Utils.getUrlVar('kodi') == 'VFONE') {
        $("#discount").hide();
        $("#kontaktiBazaar").hide();
        $("#pinBazaar").hide();
        $("#bundle").hide();
    }
    else if (Utils.getUrlVar('kodi') == 'USHDD') {
        var statusDD = $('#hfStatusDD').val();

        $("#kontakti").hide();
        $("#pin").hide();
        //$("#kontaktiBazaar").hide();
        //$("#pinBazaar").hide();
        $("#bundle").hide();
        btnValido.SetEnabled(statusDD);
        txtKodi.SetEnabled(statusDD);
        txtKodi.validationGroup = statusDD ? "entries1" : "";
    }
    else if (Utils.getUrlVar('kodi') == 'BAZAA') {
        $("#kontakti").hide();
        $("#pin").hide();
        $("#discount").hide();
        $("#bundle").hide();
    }
    else if (Utils.getUrlVar('kodi') == 'USHma') {
        try { myMesazh.ShtoMesazhSuksesi("Klienti fiton oferten me kod " + window.parent.$('#hfKodBundle').val()); }
        catch (ee) { }

        $("#kontakti").hide();
        $("#pin").hide();
        $("#discount").hide();
        $("#kontaktiBazaar").hide();
        $("#pinBazaar").hide();
    }



    //gvLupaArtikull.SetFocusedRowIndex(0);
    //gvLupaArtikull.SelectRowOnPage(0, true);
    //var idNdermarrje = hfState.Get('idNdermarrje');
    try {
        window.parent.window.parent.SessionTimeout.sendKeepAlive();
    }
    catch (err) {
    }
}

function EndRequestHandler(sender, args) {//po
    window.parent.$('#hfVodOne').val($('#hfStatus').val());
    if ($('#hfStatus').val() == "true" && Utils.getUrlVar('status') != 0)
        $("#dvArtikulli").show();
    if (Utils.getUrlVar('status') == 0) {
        $("#dvArtikulli").hide();
        timeout = setTimeout(function () {
            window.parent.popupUniversal.Hide(); //  gvQendra.PerformCallback();
        }, 3000);
    
    }

    if ($('#hfStatusBundle').val() == "true") {
        window.parent.btn.DoClick();
        window.parent.popupUniversal.Hide();
        window.parent.Utils.shfaqLoadingGif();;

    }

    if (Utils.getUrlVar('kodi') == 'VFONE') {
        $("#discount").hide();
        $("#kontaktiBazaar").hide();
        $("#pinBazaar").hide();
        $("#bundle").hide();
        Utils.hiqLoadingGif();
    }
    else if (Utils.getUrlVar('kodi') == 'USHDD') {
        var statusDD = $('#hfStatusDD').val();

        $("#kontakti").hide();
        $("#pin").hide();
        //$("#kontaktiBazaar").hide();
        //$("#pinBazaar").hide();
        $("#piket").hide();
        $("#bundle").hide();
        btnValido.SetEnabled(statusDD);
        txtKodi.SetEnabled(statusDD);
        txtKodi.validationGroup = statusDD ? "entries1" : "";
    }
    else if (Utils.getUrlVar('kodi') == 'BAZAA') {
        $("#kontakti").hide();
        $("#pin").hide();
        $("#discount").hide();
        $("#piket").hide();
        $("#bundle").hide();
    }
    else if (Utils.getUrlVar('kodi') == 'USHma') {
        $("#kontakti").hide();
        $("#pin").hide();
        $("#discount").hide();
        $("#kontaktiBazaar").hide();
        $("#pinBazaar").hide();
        $("#dvArtikulli").hide();
    }
}


function ProcessKeyPress() {
    var currentIndex = gvLupaArtikull.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaArtikull.GetVisibleRowsOnPage() - 1) {
            gvLupaArtikull.SetFocusedRowIndex(0);
        }
        else {
            gvLupaArtikull.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaArtikull.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function ShitjeClicked(key, e) {
    e.processOnServer = false;

    if (Utils.getUrlVar('kodi') == 'VFONE') {
        window.parent.txtKontakti.SetText("3556" + txtNrKontakti.GetText());
        gvLupaArtikull.GetRowValues(key, 'KodArtikulli;IdArtikulli;Pike;Vlere;KodVFOne', OnGridSelectionCompleteShitje);
    }
    else if (Utils.getUrlVar('kodi') == 'USHDD') {
        window.parent.txtKontakti.SetText("3556" + txtKontaktiBazaar.GetText());
        gvLupaArtikull.GetRowValues(key, 'KodArtikulli;IdArtikulli;Vlere', OnGridSelectionCompleteShitjeDiscount);
    } else if (Utils.getUrlVar('kodi') == 'BAZAA') {
        window.parent.txtKontakti.SetText("3556" + txtKontaktiBazaar.GetText());
        gvLupaArtikull.GetRowValues(key, 'KodArtikulli;IdArtikulli;Vlere', OnGridSelectionCompleteShitjeBazaar);
    }
}
function PorositClicked(key, e) {
    e.processOnServer = false;
    if (Utils.getUrlVar('kodi') == 'VFONE') gvLupaArtikull.GetRowValues(key, 'KodArtikulli;PershkrimArtikulli;PershkrimiAngArtikulli;Kodbari;IdArtikulli;DetajimArtikulli;PershkrimNjesia1;PershkrimNjesia2;Njesi1Artikulli;Njesi2Artikulli;LlojiArt;Klasa;KodVFOne;Vlere;Pike', OnGridSelectionCompletePorosit);
    else if (Utils.getUrlVar('kodi') == 'USHDD') {
        window.parent.txtKontakti.SetText("3556" + txtKontaktiBazaar.GetText());
        gvLupaArtikull.GetRowValues(key, 'KodArtikulli;PershkrimArtikulli;PershkrimiAngArtikulli;Kodbari;IdArtikulli;DetajimArtikulli;PershkrimNjesia1;PershkrimNjesia2;Njesi1Artikulli;Njesi2Artikulli;LlojiArt;Klasa;KodVFOne;Vlere;Pike', OnGridSelectionCompletePorositDD);

    }
    else if (Utils.getUrlVar('kodi') == 'BAZAA') {
        gvLupaArtikull.GetRowValues(key, 'KodArtikulli;PershkrimArtikulli;PershkrimiAngArtikulli;Kodbari;IdArtikulli;DetajimArtikulli;PershkrimNjesia1;PershkrimNjesia2;Njesi1Artikulli;Njesi2Artikulli;LlojiArt;Klasa;KodVFOne;Vlere;Pike', OnGridSelectionCompletePorositBazaar);

    }
}
function OnGridSelectionCompleteShitjeBazaar(values) {

    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodi = vl[0];
    //if (window.parent.identikuesPerPopupArtikulli == 'artikull') {
    //    window.parent.editorKod.SetText(vl[0]);
    //    window.parent.editorEmertimiA.SetText(vl[1]);
    //    window.parent.editorKod.SetFocus(true);
    //} 
    {
        var grida = window.parent.$("#rowed5");
        var index = grida.getLastSel2();
        var idKontrolli = "#txtKodi" + index;
        var kodi;
        kodi = values[0];
        var idArt = values[1];

        // window.parent.$('#hfZbritja').val(values[2]);
        window.parent.$("#hfMsisdnBazaari").val("3556" + txtKontaktiBazaar.GetText());
        //window.parent.selectFunc(null, null, idKontrolli, idArt, kodi);
        if (window.parent.btnMagazina.GetText() == "" && window.parent.colMagazina.length > 0)
            mag = window.parent.colMagazina[0].Kodi;
        else
            mag = window.parent.btnMagazina.GetText();
        //window.parent.$(idKontrolli).focus();
        var rreshtaTeGrides = window.parent.$("#rowed5").jqGrid('getRowData');
        var rreshtILire = 0;
        for (i = 0; i < rreshtaTeGrides.length; i++) {
            if (!(rreshtaTeGrides[i].undefined !== ""))
                continue;
            if (grida.getTekstQelize('txtKodi', index) == "" && grida.getTekstQelize('cmbLloji', index)=="Artikull")
            {
           
                    rreshtILire = index;
                    break;
               
            }
       
            if (grida.getTekstQelize('txtKodi', index) == "" && (grida.getTekstQelize('cmbLloji', index) == "")) {
                rreshtILire = grida.jqGrid('getDataIDs')[i];
                var rreshti = window.parent.$('#rowed5').getRowData(rreshtILire);
                grida.setTekstQelize('cmbLloji', rreshtILire, 'Artikull');
                grida.setTekstQelize('txtSasia', rreshtILire, '1');
                grida.setTekstQelize('txtCmimi', rreshtILire, '1');
                grida.setTekstQelize('txtZbritja', rreshtILire, '0');
                grida.setTekstQelize('txtVleftaTVSH', rreshtILire, '1');
                
             

                grida.setTekstQelize('txtMagazina', rreshtILire, mag);
                grida.setTekstQelize('txtVlefta', rreshtILire, '1');
                grida.setTekstQelize('txtDtFillimi', rreshtILire, new Date().format('dd/MM/yyyy'));
                grida.setTekstQelize('txtDtMbarimi', rreshtILire, new Date().format('dd/MM/yyyy'));
                grida.setTekstQelize('txtVleftaTVSH', rreshtILire, '1');


                var rreshtitjeter = parseFloat(rreshtILire) + 1;
                if (window.parent.arrayReadOnlyKolonaGrides[window.parent.arrayReadOnlyKolonaGrides.length - 1] == 'True')
                    be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png'  onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                else
                    be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                var datarow = { txtFshi: be };
                var su = grida.jqGrid('addRowData', parseInt(rreshtitjeter), datarow);
                break;
            }
     
             
              
        }
        if (rreshtILire != 0) {
            var idPerdoruesi = window.parent.hfState.Get('idPerdoruesi');
            var idNdermarrje = window.parent.hfState.Get('idNdermarrje');
            listeIMEIArtikull = window.parent.merrImeiDheArtikull(index);
            window.parent.callWebserviceArtikulliIPlote({ idArt: -1, idRresht: rreshtILire, detajim: -1, magazine: mag, kodKodbarArt: kodi, peshoreArt: false, listeIMEIArtikull: listeIMEIArtikull, sasiaNeGride: new Object() });
            //   window.parent.myWS.ktheRowVleraKodArtTvsh(kodi, rreshtILire, window.parent.data_DateEdit.GetDate(), -1, idPerdoruesi, window.parent.tvshkont, idNdermarrje, window.parent.SucceededCallbackArtPlote);
        }
        
    }

    //PlatinumWeb.wsfunc.fshiGrideNgaSessioniLupa(window.parent.myWS.fshiSessionFailCheck, window.parent.myWS.webServiceFail);
    window.parent.popupUniversal.Hide();

}
function OnGridSelectionCompleteShitjeDiscount(values) {

    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodi = vl[0];
    {
        var grida = window.parent.$("#rowed5");
        var index = grida.getLastSel2();
        var idKontrolli = "#txtKodi" + index;
        var kodi;
        kodi = values[0];
        var idArt = values[1];

        window.parent.$('#hfZbritja').val(values[2]);
        window.parent.$("#hfKodKuponiDD").val($("#hfKodKuponiDD").val());
        //window.parent.selectFunc(null, null, idKontrolli, idArt, kodi);

        if (window.parent.btnMagazina.GetText() == "" && window.parent.colMagazina.length > 0)
            mag = window.parent.colMagazina[0].Kodi;
        else
            mag = window.parent.btnMagazina.GetText();
        //window.parent.$(idKontrolli).focus();
        var rreshtaTeGrides = window.parent.$("#rowed5").jqGrid('getRowData');
        var rreshtILire = 0;
        for (i = 0; i < rreshtaTeGrides.length; i++) {
            if (!(rreshtaTeGrides[i].undefined !== ""))
                continue;
            if (grida.getTekstQelize('txtKodi', index) == "" && grida.getTekstQelize('cmbLloji', index) == "Artikull") {

                rreshtILire = index;
                break;

            }

            if (grida.getTekstQelize('txtKodi', index) == "" && (grida.getTekstQelize('cmbLloji', index) == "")) {
                rreshtILire = grida.jqGrid('getDataIDs')[i];
                var rreshti = window.parent.$('#rowed5').getRowData(rreshtILire);
                grida.setTekstQelize('cmbLloji', rreshtILire, 'Artikull');
                grida.setTekstQelize('txtSasia', rreshtILire, '1');
                grida.setTekstQelize('txtCmimi', rreshtILire, '1');
                grida.setTekstQelize('txtZbritja', rreshtILire, '0');
                grida.setTekstQelize('txtVleftaTVSH', rreshtILire, '1');



                grida.setTekstQelize('txtMagazina', rreshtILire, mag);
                grida.setTekstQelize('txtVlefta', rreshtILire, '1');
                grida.setTekstQelize('txtDtFillimi', rreshtILire, new Date().format('dd/MM/yyyy'));
                grida.setTekstQelize('txtDtMbarimi', rreshtILire, new Date().format('dd/MM/yyyy'));
                grida.setTekstQelize('txtVleftaTVSH', rreshtILire, '1');


                var rreshtitjeter = parseFloat(rreshtILire) + 1;
                if (window.parent.arrayReadOnlyKolonaGrides[window.parent.arrayReadOnlyKolonaGrides.length - 1] == 'True')
                    be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png'  onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                else
                    be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                var datarow = { txtFshi: be };
                var su = grida.jqGrid('addRowData', parseInt(rreshtitjeter), datarow);
                break;
            }



        }
        if (rreshtILire != 0) {
            var idPerdoruesi = window.parent.hfState.Get('idPerdoruesi');
            var idNdermarrje = window.parent.hfState.Get('idNdermarrje');
            listeIMEIArtikull = window.parent.merrImeiDheArtikull(index);
            window.parent.callWebserviceArtikulliIPlote({ idArt: -1, idRresht: rreshtILire, detajim: -1, magazine: mag, kodKodbarArt: kodi, peshoreArt: false, listeIMEIArtikull: listeIMEIArtikull, sasiaNeGride: new Object() });
            //   window.parent.myWS.ktheRowVleraKodArtTvsh(kodi, rreshtILire, window.parent.data_DateEdit.GetDate(), -1, idPerdoruesi, window.parent.tvshkont, idNdermarrje, window.parent.SucceededCallbackArtPlote);
        }
      
    }

    //PlatinumWeb.wsfunc.fshiGrideNgaSessioniLupa(window.parent.myWS.fshiSessionFailCheck, window.parent.myWS.webServiceFail);
    window.parent.popupUniversal.Hide();

}
var vleratVfone = new Array();
var isPorosi = false;
function OnGridSelectionCompleteShitje(values) {

    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodi = vl[0];
    //if (window.parent.identikuesPerPopupArtikulli == 'artikull') {
    //    window.parent.editorKod.SetText(vl[0]);
    //    window.parent.editorEmertimiA.SetText(vl[1]);
    //    window.parent.editorKod.SetFocus(true);
    //} 
    {
        isPorosi = false;
        vleratVfone = values;
        if (values[3] != 0)
            popMesazh.Show();
        else kaloArtikullNeGride();

    }
}
function kaloArtikullNeGride() {
    var grida = window.parent.$("#rowed5");
    var index = grida.getLastSel2();
    var idKontrolli = "#txtKodi" + index;
    var kodi;
    kodi = vleratVfone[0];
    var idArt = vleratVfone[1];
    window.parent.$('#hfPiket').val(vleratVfone[2]);
    window.parent.txtPike.SetText(vleratVfone[2]);
    window.parent.$('#hfKodVFOne').val(vleratVfone[4]);
    window.parent.$('#hfVlera').val(vleratVfone[3]);
    //window.parent.selectFunc(null, null, idKontrolli, idArt, kodi);
    var mag;
    if (window.parent.btnMagazina.GetText() == "" && window.parent.colMagazina.length > 0)
        mag = window.parent.colMagazina[0].Kodi;
    else
        mag = window.parent.btnMagazina.GetText();
    //window.parent.$(idKontrolli).focus();
    var rreshtaTeGrides = window.parent.$("#rowed5").jqGrid('getRowData');
    var rreshtILire = 0;
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (!(rreshtaTeGrides[i].undefined !== ""))
            continue;
        if (grida.getTekstQelize('txtKodi', index) == "" && grida.getTekstQelize('cmbLloji', index)=="Artikull")
        {
            rreshtILire = grida.jqGrid('getDataIDs')[i];
            break;
        }
       
      
        if (grida.getTekstQelize('txtKodi', index) == "" && (grida.getTekstQelize('cmbLloji', index) == "")) {
            rreshtILire = grida.jqGrid('getDataIDs')[i];
            var rreshti = window.parent.$('#rowed5').getRowData(rreshtILire);
            grida.setTekstQelize('cmbLloji', rreshtILire, 'Artikull');
            grida.setTekstQelize('txtSasia', rreshtILire, '1');
            grida.setTekstQelize('txtCmimi', rreshtILire, '1');
            grida.setTekstQelize('txtZbritja', rreshtILire, '0');
            grida.setTekstQelize('txtVleftaTVSH', rreshtILire, '1');
           
           
            grida.setTekstQelize('txtMagazina', rreshtILire, mag);
            grida.setTekstQelize('txtVlefta', rreshtILire, '1');
            grida.setTekstQelize('txtDtFillimi', rreshtILire, new Date().format('dd/MM/yyyy'));
            grida.setTekstQelize('txtDtMbarimi', rreshtILire, new Date().format('dd/MM/yyyy'));
            grida.setTekstQelize('txtVleftaTVSH', rreshtILire, '1');

           
            var rreshtitjeter = parseFloat(rreshtILire) + 1;
            if (window.parent.arrayReadOnlyKolonaGrides[window.parent.arrayReadOnlyKolonaGrides.length - 1] == 'True')
                be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png'  onclick='fshiClicked(" + rreshtitjeter + ")'/>";
            else
                be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtitjeter + ")'/>";
            var datarow = { txtFshi: be };
            var su =grida.jqGrid('addRowData', parseInt(rreshtitjeter), datarow);
            break;
        }
    }
    if (rreshtILire != 0) {
        var idPerdoruesi = window.parent.hfState.Get('idPerdoruesi');
        var idNdermarrje = window.parent.hfState.Get('idNdermarrje');
        listeIMEIArtikull = window.parent.merrImeiDheArtikull(index);
        window.parent.callWebserviceArtikulliIPlote({ idArt: -1, idRresht: rreshtILire, detajim: -1, magazine: mag, kodKodbarArt: kodi, peshoreArt: false, listeIMEIArtikull: listeIMEIArtikull, sasiaNeGride: new Object() });
     //   window.parent.myWS.ktheRowVleraKodArtTvsh(kodi, rreshtILire, window.parent.data_DateEdit.GetDate(), -1, idPerdoruesi, window.parent.tvshkont, idNdermarrje, window.parent.SucceededCallbackArtPlote);
    }

    //PlatinumWeb.wsfunc.fshiGrideNgaSessioniLupa(window.parent.myWS.fshiSessionFailCheck, window.parent.myWS.webServiceFail);
    window.parent.popupUniversal.Hide();
}

function OnGridSelectionCompletePorosit(values) {

    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodi = vl[0];
    vleratVfone = values;
    isPorosi = true;
    if (values[13] != 0)
        popMesazh.Show();
    else porositOk();

}
function porositOk()
{
    window.parent.myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim&niveli=' + hfState.Get('IdNivel') + '&konfigurim=' + hfState.Get('IdKonfigurimi') + '&idartikulli=' + vleratVfone[4] + '&nrklienti=' + "3556" + txtNrKontakti.GetText() + '&kthehu=kthehu&kodvodone=' + vleratVfone[12] + '&zbritje=' + vleratVfone[13] + '&pike=' + vleratVfone[14]);


    //PlatinumWeb.wsfunc.fshiGrideNgaSessioniLupa(window.parent.myWS.fshiSessionFailCheck, window.parent.myWS.webServiceFail);
    window.parent.popupUniversal.Hide();
}
function OnGridSelectionCompletePorositDD(values) {

    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodi = vl[0];

    window.parent.myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitjediscount&shtim_modifikim=shtim&niveli=' + hfState.Get('IdNivel') + '&konfigurim=' + hfState.Get('IdKonfigurimi') + '&idartikulli=' + values[4] + '&nrklienti=' + "3556" + txtKontaktiBazaar.GetText() + '&kthehu=kthehu' + '&zbritje=' + values[13] + '&kodkupon=' + $("#hfKodKuponiDD").val());


    //PlatinumWeb.wsfunc.fshiGrideNgaSessioniLupa(window.parent.myWS.fshiSessionFailCheck, window.parent.myWS.webServiceFail);
    window.parent.popupUniversal.Hide();

}
function OnGridSelectionCompletePorositBazaar(values) {

    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodi = vl[0];

    window.parent.myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=bazaar&shtim_modifikim=shtim&niveli=' + hfState.Get('IdNivel') + '&konfigurim=' + hfState.Get('IdKonfigurimi') + '&idartikulli=' + values[4] + '&nrklienti=' + "3556" + txtKontaktiBazaar.GetText() + '&kthehu=kthehu');


    //PlatinumWeb.wsfunc.fshiGrideNgaSessioniLupa(window.parent.myWS.fshiSessionFailCheck, window.parent.myWS.webServiceFail);
    window.parent.popupUniversal.Hide();

}
function menu_click(s, e) {

}

function btnGjeneroPinClick(s, e) {
    if (!Utils.IsNullOrWhiteSpace(txtNrKontakti.GetText()))
        Utils.shfaqLoadingGif();
}

function btnVerifikoPINClick(s, e) {
    if (!Utils.IsNullOrWhiteSpace(txtPIN.GetText()))
        Utils.shfaqLoadingGif();
}


