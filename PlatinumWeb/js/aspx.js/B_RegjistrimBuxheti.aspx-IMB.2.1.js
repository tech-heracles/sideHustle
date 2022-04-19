;

var _idKomponente;
var _idNdermarrje;
var _idPerdoruesi;
var _idViti;
var idKonvertimi;
var llojKonvertimiNga;
var konfigurimet;
var btnFiltrat;
var idSkemaWF;

$(window).on("load", function () {
    myMesazh.shtoHandler();
    changeName();
});

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    _idKomponente = hfState.Get("_idKomponente");
    _idNdermarrje = hfState.Get("_idNdermarrje");
    _idPerdoruesi = hfState.Get("_idPerdoruesi");
    _idViti = hfState.Get("_idViti");
    myFaqeCelje.changeName('B_RegjistrimBuxheti.aspx?lloji=' + Utils.getUrlVar("lloji"), 0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    $('#dvMenu').show();
    //vendosVisiblitetPerMenuItems();
}

//function vendosVisiblitetPerMenuItems() {
//    var lloji = Utils.getUrlVar('lloji')
//    switch (lloji) {
//        case 'planifikim':
//            bejMenuItemsVisible(false, true); // (visiblePosto, visibleKonverto)
//            break;
//        case 'miratim':
//            bejMenuItemsVisible(true, true);
//            break;
//        //default:
//        //    bejMenuItemsVisible(false, false);
//        //    break;
//    }
//}

//function bejMenuItemsVisible(visiblePosto, visibleKonverto) {
//    ASPxMenu1.GetItemByName('Posto').SetVisible(visiblePosto);
//    ASPxMenu1.GetItemByName('Konverto').SetVisible(visibleKonverto);
//}


var nrreshtash = 1
function menu_click(s, e) {
    var lloji = Utils.getUrlVar('lloji'),
        gridaDokumentave = gv_RegjistrimBuxheti,
        gridIndexModifiko = gridaDokumentave.GetFocusedRowIndex(),
        idDok = gridaDokumentave.GetRowKey(gridIndexModifiko),
        gridSelectedRowCount = gridaDokumentave.GetSelectedRowCount();
    var emerFaqe = (lloji == "miratim" || lloji == "planifikim") ? "B_Shto_RegjistrimBuxheti.aspx" : (lloji == "alokim") ? "B_Shto_RegjistrimAlokimBuxheti.aspx" : (lloji == "rialokim") ? "B_Shto_RegjistrimRialokimBuxheti.aspx" : (lloji == "planifikimEkzekutimi" || lloji == "ekzekutim") ? "B_Shto_RegjistrimDokumentBuxheti.aspx" : "B_Shto_RegjistrimDokumentBuxheti.aspx";
    switch (e.item.name) {
        case "Fshi":
            if (gridaDokumentave.GetSelectedRowCount() == 0) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjMagMesazhZgjidhniNje"));
                return;
            }
            identifikuesPyetje = "FshiDok";
            myMesazh.ShtoPyetje(hfState.Get("msgJuKeniZgjedhur") + gridaDokumentave.GetSelectedRowCount() + hfState.Get("msgReshtaNjeOseDisa") + hfState.Get("labelAdministrimiMsgJeniSigurt"));
            break;
        case "Shto":
            if (lloji == "perfitim" || lloji == "planifikimEkzekutimi" || lloji == "ekzekutim")
                myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'lloji': lloji, 'id': 0, 'shtim_modifikim': 'shtim' }));
            else if (!(lloji == "miratim" || lloji == "planifikim"))
                myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'id': 0, 'shtim_modifikim': 'shtim' }));
            else
                myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'planifikim_miratim': lloji, 'id': 0, 'shtim_modifikim': 'shtim' }));
            break;
        case "Shiko":
            if (gridaDokumentave.GetFocusedRowIndex() == -1) {
                myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje dokument!");
                e.processOnServer = false;
                return;
            }

            if (lloji == "miratim" || lloji == "planifikim")
                myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'planifikim_miratim': lloji, 'id': gridaDokumentave.GetRowKey(gridaDokumentave.GetFocusedRowIndex()), 'shtim_modifikim': 'modifikim' }));
            else {
                var callBackFunction;
                if (lloji == "planifikimEkzekutimi" || lloji == "ekzekutim" || lloji == "perfitim")
                    callBackFunction = function () {
                        myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'lloji': lloji, 'id': gridaDokumentave.GetRowKey(gridaDokumentave.GetFocusedRowIndex()), 'shtim_modifikim': 'modifikim' }));
                    };
                else
                    callBackFunction = function () {
                        myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'id': gridaDokumentave.GetRowKey(gridaDokumentave.GetFocusedRowIndex()), 'shtim_modifikim': 'modifikim' }));
                };
                if (lloji == "rialokim" || lloji == "planifikimEkzekutimi" || lloji == "ekzekutim" || lloji == "perfitim")
                    gridaDokumentave.GetSelectedFieldValues('IdNivel;IdStatusDok', function (values) {
                        kontrolloTeDrejtaPerNivelRegjistrimiDokumenti(values, "Shiko", callBackFunction)
                    });
                else
                    callBackFunction();
            }
            e.processOnServer = false;
            break;
        case "Posto":
            if (gridaDokumentave.GetSelectedRowCount() != 1)
                myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje dokument!");
            else
                gridaDokumentave.GetSelectedFieldValues('IdBuxhetiKoka', SuccededCallbackSelectPosto);
            e.processOnServer = false;
            break;
        case "Konverto":
            if (gridaDokumentave.GetSelectedRowCount() != 1)
                myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje dokument!");
            else
                gridaDokumentave.GetSelectedFieldValues('IdBuxhetiKoka;IdKonfigAmbjente', ButtonClickKonverto);
            e.processOnServer = false;
            break;
        case "Ruaj":
            if (gridaDokumentave.GetFocusedRowIndex() == -1)
                myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje dokument!");
            else {
                identifikuesPyetje = "RuajDok";
                myMesazh.ShtoPyetje(hfState.Get("msgJuKeniZgjedhur") + gridaDokumentave.GetSelectedRowCount() + hfState.Get("msgReshtaNjeOseDisa") + hfState.Get("labelAdministrimiMsgJeniSigurt"));
            }
            e.processOnServer = false;
            break;
        case "Pezullo":
            if (gridaDokumentave.GetSelectedRowCount() < 1)
                myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje dokument!");
            else
                NdryshoStatusDokumenti(8);
            e.processOnServer = false;
            break;
        default:
            myMenu.menu_click_regjistrime(s, e, Utils.buildUrl(emerFaqe, { "lloji": lloji, "shtim_modifikim": "shtim" }), Utils.buildUrl(emerFaqe, { "lloji": lloji, 'id': idDok, "shtim_modifikim": "modifikim" }), gridSelectedRowCount);
            break;
    }
}

function NdryshoStatusDokumenti(idStatusi) {
    var komponente = "B_RegjistrimBuxheti.aspx?lloji=" + Utils.getUrlVar("lloji");
    gv_RegjistrimBuxheti.GetSelectedFieldValues('IdBuxhetiKoka', function (result) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Buxheti", "NdryshoStatusDokumenti"),
            data: JSON.stringify({ idBuxhetiKoka: result, idStatusDok: idStatusi, idPerdoruesi: _idPerdoruesi, idNdermarrje: _idNdermarrje, idViti: _idViti, komponente: komponente, shenime: "" })
        }).done(SucceededCallbackNdryshoStatusDokumenti)
    });
}

function SucceededCallbackNdryshoStatusDokumenti(result) {
    if (result.mesazhSukses != "") {
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
        gv_RegjistrimBuxheti.PerformCallback(true);
    }
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
    Utils.hiqLoadingGif();
}

function SuccededCallbackSelectPosto(result) {
    var idKokaBuxheti = result[0];
    var lloji = Utils.getUrlVar("lloji");
    var komponente = 'B_RegjistrimBuxheti.aspx?lloji=' + lloji;
    Utils.shfaqLoadingGif();
    switch (lloji) {
        case "miratim":
        case "planifikim":
            $.ajax({
                url: Utils.getServerApiUrl("Buxheti", "PostoDokumentMiratimPlanifikimBuxheti"),
                data: JSON.stringify({ idKokaBuxheti: idKokaBuxheti, komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti })
            }).done(SucceededCallbackPostoDokument);
            break;
        case "alokim":
        case "rialokim":
            $.ajax({
                url: Utils.getServerApiUrl("Buxheti", "PostoDokumentAlokimBuxheti"),
                data: JSON.stringify({ idKokaAlokimi: idKokaBuxheti, komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, idSkemaWF: 0, statusAprovimi: 0 })
            }).done(SucceededCallbackPostoDokument);
            break;
        case "planifikimEkzekutimi":
            $.ajax({
                url: Utils.getServerApiUrl("Buxheti", "PostoDokumentAlokimBuxheti"),
                data: JSON.stringify({ idKokaAlokimi: idKokaBuxheti, komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, idSkemaWF: 0, statusAprovimi: 1 })
            }).done(SucceededCallbackPostoDokument);
            break;
    } 
}

function SucceededCallbackPostoDokument(result) {
    var mesazh = result.mesazh;
    if (!mesazh.Status)
        myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
    else {
        myMesazh.ShtoMesazhSuksesi(mesazh.PershkrimMesazhi);
        gv_RegjistrimBuxheti.PerformCallback(true);
    }
    Utils.hiqLoadingGif();
}

function clickExport(e) {
    if (gv_RegjistrimBuxheti.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function OnGridDoubleClick(s, e, index) {
    var lloji = Utils.getUrlVar('lloji');
    var emerFaqe = (lloji == "miratim" || lloji == "planifikim") ? "B_Shto_RegjistrimBuxheti.aspx" : (lloji == "alokim") ? "B_Shto_RegjistrimAlokimBuxheti.aspx" : (lloji == "rialokim") ? "B_Shto_RegjistrimRialokimBuxheti.aspx" : (lloji == "planifikimEkzekutimi" || lloji == "ekzekutim") ? "B_Shto_RegjistrimDokumentBuxheti.aspx" : "B_Shto_RegjistrimDokumentBuxheti.aspx";
    if (lloji == "miratim" || lloji == "planifikim") {
        gv_RegjistrimBuxheti.GetRowValues(s.GetFocusedRowIndex(), 'IdBuxhetiKoka;IdNivel', function (result) {
            myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'planifikim_miratim': Utils.getUrlVar('lloji'), 'id': result[0], 'shtim_modifikim': 'modifikim', 'idNivel': result[1] }));
        });
    }
    else {

        if (lloji == "planifikimEkzekutimi" || lloji == "ekzekutim" || lloji == "perfitim") {
            var callBackFunction = function () {
                gv_RegjistrimBuxheti.GetRowValues(s.GetFocusedRowIndex(), 'IdBuxhetiKoka;IdNivel', function (result) {
                    myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'lloji': lloji, 'id': result[0], 'shtim_modifikim': 'modifikim', 'idNivel': result[1] }));
                });
            };
        }
        else
            callBackFunction = function () {
                gv_RegjistrimBuxheti.GetRowValues(s.GetFocusedRowIndex(), 'IdBuxhetiKoka;IdNivel', function (result) {
                    myMenu.ShikoClick(e, Utils.buildUrl(emerFaqe, { 'id': result[0], 'shtim_modifikim': 'modifikim', 'idNivel': result[1] }));
                });
            };
        if (lloji == "rialokim" || lloji == "planifikimEkzekutimi" || lloji == "ekzekutim" || lloji == "perfitim")
            gv_RegjistrimBuxheti.GetSelectedFieldValues('IdNivel;IdStatusDok', function (values) {
                kontrolloTeDrejtaPerNivelRegjistrimiDokumenti(values, "Shiko", callBackFunction)
            });
        else
            callBackFunction();
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText('PershkrimKonfigAmbjente'));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText());
    gv_RegjistrimBuxheti.PerformCallback("" + _idKomponente + "; cmbKonfigurimi.GetText()");
}

function ndryshoKonfiguriminInit() {
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gv_RegjistrimBuxheti, _idKomponente, cmbKonfigurimi.GetText());
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function textChanged(s, e) {
    hfState.Set("ruajtjeFiltri", true);
    myMenu.textChanged(s, e);
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function PoClick(s, e) {
    Utils.shfaqLoadingGif();
    switch (identifikuesPyetje) {
        case "FshiDok":
            Utils.shfaqLoadingGif();
            FshiDokument();
            break;
        case "RuajDok":
            Utils.shfaqLoadingGif();
            NdryshoStatusDokumenti(1);
            break;
        default:
            return;
    }
}

function JoClick(s, e) {
    return;
}

function FshiDokument() {
    var komponente = "B_RegjistrimBuxheti.aspx?lloji=" + Utils.getUrlVar("lloji");
    gv_RegjistrimBuxheti.GetSelectedFieldValues('IdBuxhetiKoka', function (result) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Buxheti", "FshiDokumentBuxheti"),
            data: JSON.stringify({ ids: result, komponente: komponente })
        }).done(SuccededCallbackDelete)
    });
}

function SuccededCallbackDelete(result) {
    Utils.hiqLoadingGif();
    result.deletedKeys.forEach(function (key, index) {
        gv_RegjistrimBuxheti.DeleteRowByKey(key);
    });
    if (result.keysCount > 1)
    {
        window.open("RaportiShpejte.aspx?emriReal=gabimeImporti&printo=0&Sesioni=false&db=jo&scopeID=" + Utils.getUrlVar("scopeID"));
        return;
    }
    if (result.mesazhSukses != "")
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
}

function EndRequestHandler(sender, args) {
    if (hfState.Get("ruajtjeFiltri") && btnFiltrat) {
        btnFiltrat.PerformCallback();
    }
    hfState.Set("ruajtjeFiltri", false);
}

function konverto() {
    if (cmbKonverto.GetText() == 'MB')
        myFaqeCelje.kontrolloTeDrejta('B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=miratim' + '&id=' + idKonvertimi + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&llojKonvertimiNga=' + llojKonvertimiNga);
    else if (cmbKonverto.GetText() == 'AB')
        myFaqeCelje.kontrolloTeDrejta('B_Shto_RegjistrimAlokimBuxheti.aspx?id=' + idKonvertimi + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&llojKonvertimiNga=' + llojKonvertimiNga);
    else if (cmbKonverto.GetText() == 'EB' || cmbKonverto.GetText() == 'IB')
        myFaqeCelje.kontrolloTeDrejta('B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=ekzekutim' + '&id=' + idKonvertimi + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&llojKonvertimiNga=' + llojKonvertimiNga);
}

function ndryshoNiveli(s, e) {
    cmbKonf.ClearItems();
    for (i = 0; i < konfigurimet.length; i++)
        if (konfigurimet[i].IdNivel == s.GetValue())
            cmbKonf.AddItem(konfigurimet[i].KodKonfigAmbjente, konfigurimet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.SelectIndex(0);
}

function SuccededCallbackKonvertime(result, konvertuarNga) {
    Utils.hiqLoadingGif();
    var nivelet = result.nivelet;
    var colKonfig = result.colKonfig;
    if (result.mesazh !== "Nuk jane konvertuar") {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        return;
    }
    if (nivelet.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukMundTeKonvertohet"));
        return;
    }
    cmbKonverto.BeginUpdate();
    cmbKonverto.ClearItems();

    for (i = 0; i < nivelet.length; i++) {
        cmbKonverto.AddItem(nivelet[i].Kodi, nivelet[i].IdNivel); //AddItem(teksti, vlera);
    }

    cmbKonverto.EndUpdate();
    cmbKonverto.SelectIndex(0);

    cmbKonf.BeginUpdate();
    cmbKonf.ClearItems();
    for (i = 0; i < colKonfig.length; i++)
        if (colKonfig[i].IdNivel == cmbKonverto.GetValue())
            cmbKonf.AddItem(colKonfig[i].KodKonfigAmbjente, colKonfig[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.EndUpdate();
    cmbKonf.SelectIndex(0);
    konfigurimet = colKonfig;
    idKonvertimi = result.id;
    llojKonvertimiNga = result.llojKonvertimiNga;
    popKonvertim.Show();
}

function ButtonClickKonverto(result) {
    Utils.shfaqLoadingGif();
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Buxheti", "KontrolloKonvertuar"),
        data: JSON.stringify({ id: result[0][0], idKonfigKonvertimiNga: result[0][1], idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idGjuha: hfState.Get("_idGjuha") })
    }).done(SuccededCallbackKonvertime);
}

function kontrolloTeDrejtaPerNivelRegjistrimiDokumenti(values, veprimi, callBackFunction) {
    var idNivelRegjistrimi = values[0][0];
    var idStatusDok = values[0][1];
    var teDrejtaNivele = JSON.parse(hfState.Get("teDrejtaNivele"));
    var teDrejtaNiveli = teDrejtaNivele.filter(function (eDrejta) { return eDrejta.IdNivelRegjistrimi == idNivelRegjistrimi; })[0];
    var kaTeDrejta = false;
    var eDrejta = '';
    switch (veprimi) {
        case 'Shiko':
            kaTeDrejta = idStatusDok == 1 ? teDrejtaNiveli.DMod : teDrejtaNiveli.DModifikimDraft;
            eDrejta = "te modifikoni dokumenta me status "+ (idStatusDok == 1 ? "ruajtur" : "draft");
            break;
        default:
            break;
    }

    if (kaTeDrejta)
        callBackFunction();
    else
        myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta " + eDrejta + " te nenkategorise: " + teDrejtaNiveli.PershkrimKomponente);
}

function SetBtnFiltraValue(s, e) {
    cmbfiltra.SetValue(s.GetValue());
}