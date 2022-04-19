; //variabla per te kaluar nga nje reshti i grides tek tjetri

var pageState = {
    indexSel: 0,
    identifikuesPyetje: null,
    msgJuKeniZgjedhur: null,
    msgRreshta: null,
    labelAdministrimiMsgJeniSigurt: null,
    msgZgjdhniNjeNgaElementetEListes: null,
    msgDokNukMundTeKonvertohet: null,
    regjisDokZgjidhniTePakten1DokPerKonvertim: null,
    regjisDokZgjidhDokPerTeBashkengjitur: null,
    regjMagMesazhZgjidhniNje: null,
    regjisDokNukKeniAsnjeDokTeZgjedhur: null,
    msgDokTeJeneTeSeNjejtesNenkategori: null
};

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
                break;
            default:
                break;
        }
    });
    changeName();
});

$(window).on("load", function () {
    pageState.msgJuKeniZgjedhur = hfState.Get("msgJuKeniZgjedhur"); hfState.Remove("msgJuKeniZgjedhur");
    pageState.msgRreshta = hfState.Get("msgRreshta"); hfState.Remove("msgRreshta");
    pageState.labelAdministrimiMsgJeniSigurt = hfState.Get("labelAdministrimiMsgJeniSigurt"); hfState.Remove("labelAdministrimiMsgJeniSigurt");
    pageState.msgZgjdhniNjeNgaElementetEListes = hfState.Get("msgZgjdhniNjeNgaElementetEListes"); hfState.Remove("msgZgjdhniNjeNgaElementetEListes");
    pageState.msgDokNukMundTeKonvertohet = hfState.Get("msgDokNukMundTeKonvertohet"); hfState.Remove("msgDokNukMundTeKonvertohet");
    pageState.regjisDokZgjidhniTePakten1DokPerKonvertim = hfState.Get("regjisDokZgjidhniTePakten1DokPerKonvertim"); hfState.Remove("regjisDokZgjidhniTePakten1DokPerKonvertim");
    pageState.regjisDokZgjidhDokPerTeBashkengjitur = hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"); hfState.Remove("regjisDokZgjidhDokPerTeBashkengjitur");
    pageState.regjMagMesazhZgjidhniNje = hfState.Get("regjMagMesazhZgjidhniNje"); hfState.Remove("regjMagMesazhZgjidhniNje");
    pageState.regjisDokNukKeniAsnjeDokTeZgjedhur = hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"); hfState.Remove("regjisDokNukKeniAsnjeDokTeZgjedhur");
    pageState.msgDokTeJeneTeSeNjejtesNenkategori = hfState.Get("msgDokTeJeneTeSeNjejtesNenkategori"); hfState.Remove("msgDokTeJeneTeSeNjejtesNenkategori");
});
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_RegMag, "", "");
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
    pageState.indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_RegMag, pageState.indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    pageState.indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_RegMag, pageState.indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    pageState.indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_RegMag, pageState.indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    pageState.indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_RegMag, pageState.indexSel);
}

function klonoDokument() {
    var indexModifiko = grid_RegMag.GetFocusedRowIndex();
    grid_RegMag.GetRowValues(indexModifiko, 'IdNivel;IdKonfigAmbjente;IdKokaMagazina;IdGjenerues', function (values) {
        if (values.length < 1 || !values[0]) {
            myMesazh.ShtoMesazhGabimi(pageState.regjisDokNukKeniAsnjeDokTeZgjedhur);
            return;
        }

        if (values[3] != null) {
            myMesazh.ShtoMesazhGabimi('Nuk mund te klononi nje dokument te gjeneruar nga nje ambjent tjeter!');
            return;
        }

        var katDokAndKomponentObj = [{ idKatDok: 6, komponente: '' }];
        popUpKlonimiFunctions.showPopUp(values[0], values[1], values[2], katDokAndKomponentObj);
    });     
}

function SuccededCallbackSelect(selectedValues) {
    nrreshtash = selectedValues.length;
    if (nrreshtash == 0) {
        myMesazh.ShtoMesazhGabimi(pageState.regjMagMesazhZgjidhniNje);
        return;
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "kontrolloKonvertuarDheKase"),
        data: JSON.stringify({ id: selectedValues, lloji: "mag", kodkonfig: cmbKonfigurimi.GetText(), idNdermarrje: hfState.Get("idNdermarrje"), idPerdoruesi: hfState.Get("idPerdoruesi"), idGjuha: hfState.Get("idGjuha") })
    }).done(function (result) {
        SuccededCallbackFshi(result, selectedValues.length);
    });

}

function SuccededCallbackFshi(result, nrreshtash) {
    pageState.identifikuesPyetje = "FshiDokMag";
    if (result != "")
        myMesazh.ShtoPyetje(result);
    else
        myMesazh.ShtoPyetje(pageState.msgJuKeniZgjedhur + nrreshtash + pageState.msgRreshta + pageState.labelAdministrimiMsgJeniSigurt);
}

function PoClick(s, e) {
    switch (pageState.identifikuesPyetje) {
        case "FshiDokMag":
            FshiDokumentMagazine();
            break;
        case "Rivleresimi":
            Rivleresim();
            break;
    }
}

function JoClick(s, e) {
    return;
}

function konvertoDokument(grid_RegMag) {
    grid_RegMag.GetSelectedFieldValues('IdKokaMagazina;NrDok;IdNivel', function (values) {
        if (values.length === 0) {
            myMesazh.ShtoMesazhGabimi(pageState.regjisDokZgjidhniTePakten1DokPerKonvertim)
            return;
        }
        Utils.shfaqLoadingGif();
        var idnivel = values[0][2];
        var ids = new Array();
        var status = new Array();
        for (i = 0; i < values.length; i++) {
            if (values[i][2] != idnivel) {
                Utils.hiqLoadingGif();
                myMesazh.ShtoMesazhGabimi(pageState.msgDokTeJeneTeSeNjejtesNenkategori);
                return;
            }

            ids[i] = values[i][0];
        }

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKonvertuar"),
            data: JSON.stringify({ ids: ids, kodkonfig: cmbKonfigurimi.GetText(), idNdermarrje: hfState.Get("idNdermarrje"), idPerdoruesi: hfState.Get("idPerdoruesi"), idGjuha: hfState.Get("idGjuha"), pageId: window['CurrentPageId'] })
        }).done(SuccededCallbackKonvertime);
    });
}

function ButtonClickArkiva(id) {//id e rreshtit te selektuar
    if (id === null) {
        myMesazh.ShtoMesazhGabimi(pageState.regjisDokNukKeniAsnjeDokTeZgjedhur);
        return;
    }
    popupUniversal.SetHeaderText(pageState.regjisDokZgjidhDokPerTeBashkengjitur);
    popupUniversal.SetSize(738, 548);
   
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=magazina' + '&idDok=' + grid_RegMag.GetRowKey(grid_RegMag.GetFocusedRowIndex()));
    popupUniversal.Show();
}

function eksportoDokumentat(grid_RegMag, lloj) {
    grid_RegMag.GetSelectedFieldValues('IdKokaMagazina', function (values) {
        if (lloj == 'hyrje')
            myButtonClickLupa.ButtonClickLupaEksporto(values, lloj, 6, 'Magazina', 'Format Standart Flete Hyrje ne Magazine');
        else if (lloj == 'dalje')
            myButtonClickLupa.ButtonClickLupaEksporto(values, lloj, 6, 'Magazina', 'Format Standart Flete Dalje ne Magazine');
    });
}
/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var id = grid_RegMag.GetRowKey(grid_RegMag.GetFocusedRowIndex());
    switch (e.item.name) {
        case "Fshi":
            e.processOnServer = false;
            grid_RegMag.GetSelectedFieldValues('IdKokaMagazina;NrDok;IdGjenerues', SuccededCallbackSelect)
            break;
        case 'Klono':
            klonoDokument();
            e.processOnServer = false;
            break;
        case "Konverto":
            e.processOnServer = false;
            konvertoDokument(grid_RegMag);
            break;
        case "Arkiva":
            e.processOnServer = false;
            ButtonClickArkiva(id);
            break;
        case "Eksporto":
            e.processOnServer = false;
            eksportoDokumentat(grid_RegMag, Utils.getUrlVar('lloj'));
            break;
        default:
            myMenu.menu_click_regjistrime(s, e, "Shto_RegjistrimMagazine.aspx?lloj=" + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', 'Shto_RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + id + '&shtim_modifikim=modifikim', grid_RegMag.GetSelectedRowCount());
    }
}


var konfigurimet, idte;
function SuccededCallbackKonvertime(result) {
    Utils.hiqLoadingGif();
    var nivelet = result.nivelet;
    var colKonfig = result.colKonfig;
    if (result.mesazh !== "Nuk jane konvertuar") {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        return;
    }

    if (nivelet.length == 0) {
        myMesazh.ShtoMesazhGabimi(pageState.msgDokNukMundTeKonvertohet);
        return;
    }
    cmbKonverto.BeginUpdate();
    cmbKonverto.ClearItems();
    for (i = 0; i < nivelet.length; i++) {
        var kakonf = false;
        for (m = 0; m < colKonfig.length; m++)
            if (colKonfig[m].IdNivel == nivelet[i].IdNivel) {
                kakonf = true;
                break;
            }
        if (kakonf)
            cmbKonverto.AddItem(nivelet[i].Kodi, nivelet[i].IdNivel); //AddItem(teksti, vlera);
    }
    cmbKonverto.EndUpdate();
    if (cmbKonverto.GetItemCount() == 0) {
        myMesazh.ShtoMesazhGabimi(pageState.msgDokNukMundTeKonvertohet); return;
    }
    cmbKonverto.SelectIndex(0);
    cmbKonf.BeginUpdate();
    cmbKonf.ClearItems();
    for (i = 0; i < colKonfig.length; i++)
        if (colKonfig[i].IdNivel == cmbKonverto.GetValue())
            cmbKonf.AddItem(colKonfig[i].KodKonfigAmbjente, colKonfig[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.EndUpdate();
    cmbKonf.SelectIndex(0);
    konfigurimet = colKonfig;
    idte = result.ids;
    popKonvertim.Show();
}

function ndryshoNiveli(s, e) {
    cmbKonf.ClearItems();
    for (i = 0; i < konfigurimet.length; i++)
        if (konfigurimet[i].IdNivel == s.GetValue())
            cmbKonf.AddItem(konfigurimet[i].KodKonfigAmbjente, konfigurimet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.SelectIndex(0);

}
function konverto() {
    Utils.konverto("", idte, "", SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen);
}

function SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen() {
    
    var konvertoTekst = cmbKonverto.GetText()
       , konvertoVlere = cmbKonverto.GetValue()
       , konfVlere = cmbKonf.GetValue();
    if (konvertoTekst == 'FD' || konvertoTekst == 'UD' || konvertoTekst == 'FH')
        myFaqeCelje.kontrolloTeDrejta(Utils.buildUrl('Shto_RegjistrimMagazine.aspx', {
            'lloj': (konvertoTekst == 'FH' ? 'hyrje' : 'dalje'),
            'id': idte[0], 'shtim_modifikim': 'konvertim', 'niveli': konvertoVlere, 'konfigurim': konfVlere, 'fsh': 'jo',
            'pageCacheId': window['CurrentPageId']
        }));
    else
        myFaqeCelje.kontrolloTeDrejta(Utils.buildUrl('Shto_RegjistrimDokumentash.aspx', { 'shitje_blerje': Utils.getUrlVar('shitje_blerje'), 'id': idte[0], 'shtim_modifikim': 'konvertim', 'niveli': cmbKonverto.GetValue(), 'konfigurim': konfVlere, 'pageCacheId': window['CurrentPageId'] }));
}

function OnGridDoubleClick(e, index) {
        myMenu.ShikoClick(e, 'Shto_RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + grid_RegMag.GetRowKey(grid_RegMag.GetFocusedRowIndex())+ '&shtim_modifikim=modifikim');
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj'), 0, hf);

    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    $('#dvMenu').show();//[0].style.visibility = 'visible';
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

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("513", cmbKonfigurimi.GetText());
    //TODO getson me kismet te behet edhe kjo funksionale
    //   PlatinumWeb.wsfunc.ktheIndexSelectedFilterPeriudhaKusht(cmbKonfigurimi.GetValue(), SuccededCallbacPeriudhaKusht, myWS.webServiceFail);
}

function SuccededCallbacPeriudhaKusht(result) {
    hfState.Set("PeriudhaSelektuar", result);
    radDtDok.SetSelectedIndex(result);
}

function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    grid_RegMag.PerformCallback(idKomp + ";" + kodKonf);
}
function EndRequestHandler(sender, args) {
    PrintPreview("cpHapFaqe");
}

function PrintPreview(propertyName) {
    var faqe = grid_RegMag[propertyName];
    if (faqe) {
        delete grid_RegMag[propertyName];
        window.open(faqe + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
    }
}

function clickExport(e) {
    if (grid_RegMag.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function FshiDokumentMagazine() {
    var guidString = hfState.Get("guidString");
    var komponente = "RegjistrimMagazine.aspx?lloj=" + Utils.getUrlVar("lloj");
    var periudha = JSON.parse(hfState.Get("periudhatHfStateKey"));
    grid_RegMag.GetSelectedFieldValues('IdKokaMagazina', function (result) {
        $.ajax({
            pritPergjigje: true,
            showLoading: true,
            url: Utils.getServerApiUrl("Rregjistrime", "FshiDokumentMagazine"),
            data: JSON.stringify({ ids: result, komponente: komponente, guidString: guidString, periudhaIdViti: periudha.IdViti, periudhaDok: periudha.PeriudhaDok })
        }).done(SuccededCallbackDelete);
    });
}

function SuccededCallbackDelete(result) {
    result.deletedKeys.forEach(function (key, index) {
        grid_RegMag.DeleteRowByKey(key);
    });
    if (result.mesazhSukses != "")
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
    if (result.mesazhRivleresim != "") {
        pageState.identifikuesPyetje = "Rivleresimi";
        myMesazh.ShtoPyetje(result.mesazhRivleresim);
    }    
    for (var i = 0; i < result.faf.length; i++)
        myMesazh.ShtoMesazhGabimi(result.faf[i]);
    if (result.fafErr)
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo&scopeID="+Utils.getUrlVar("scopeID"), '_blank');
}

function Rivleresim() {
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("Rregjistrime", "BejRivleresim"),
        data: JSON.stringify({ guidString: hfState.Get("guidString") })
    }).done(function (result) {
        myMesazh.ShtoMesazhSesioni(result);
    });
}