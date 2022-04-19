;
var identifikuesPyetje;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var pageState = {
    webhook: {}
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_RegDok, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_RegDok, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_RegDok, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_RegDok, indexSel);
}

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_RegDok, "", "");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

var nrreshtash = 1;
function SuccededCallbackFshi(result) {
    identifikuesPyetje = "FshiDok";
    var mesazh = result != ""? result:hfState.Get("msgJuKeniZgjedhur") + nrreshtash + hfState.Get("msgRreshta") + hfState.Get("labelAdministrimiMsgJeniSigurt");       
    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: mesazh,
        modal: true,
        idGjuha: hfState.Get("idGjuha"),
        okClick: PoClick,
        cancelClick: JoClick
    });
}

function SuccededCallbackSelect(result) {
    nrreshtash = result.length;
    if (nrreshtash == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjMagMesazhZgjidhniNje"));
        return;
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "kontrolloKonvertuarDheKase"),
        data: JSON.stringify({ id: result, lloji: "shitje", kodkonfig: cmbKonfigurimi.GetText(), idNdermarrje: hfState.Get("idNdermarrje"), idGjuha: hfState.Get("idGjuha") })
    }).done(SuccededCallbackFshi);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function shto_dok(s, e) {
    var shitjeBlerje = Utils.getUrlVar('shitje_blerje'),
        emerFaqe = "Shto_RegjistrimDokumentash.aspx";

    myFaqeCelje.kontrolloTeDrejta(Utils.buildUrl(emerFaqe, { "shitje_blerje": shitjeBlerje, "shtim_modifikim": "shtim" }));
        //myMenu.menu_click_regjistrime(s, e, Utils.buildUrl(emerFaqe, { "shitje_blerje": shitjeBlerje, "shtim_modifikim": "shtim" }), Utils.buildUrl(emerFaqe, {
        //    "shitje_blerje": shitjeBlerje,
        //    "shtim_modifikim": "shtim",
        //}));
}

function menu_click(s, e) {
    var shitjeBlerje = Utils.getUrlVar('shitje_blerje'),
        gridaDokumentave = grid_RegDok,
        gridIndexModifiko = gridaDokumentave.GetFocusedRowIndex(),
        idDok = gridaDokumentave.GetRowKey(gridIndexModifiko),
        emerFaqe = "Shto_RegjistrimDokumentash.aspx",
        gridSelectedRowCount = gridaDokumentave.GetSelectedRowCount();

    if (e.item.name == "Shiko") {
        e.processOnServer = false;
        if (gridSelectedRowCount == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
            e.processOnServer = false;
            return;
        }

        grid_RegDok.GetRowValues(grid_RegDok.GetFocusedRowIndex(), 'TotaliMeZbritjeMeTVSH', function (result) {
            myMenu.menu_click_regjistrime(s, e, Utils.buildUrl(emerFaqe, { "shitje_blerje": shitjeBlerje, "shtim_modifikim": "shtim" }), Utils.buildUrl(emerFaqe, {
                "shitje_blerje": shitjeBlerje, 'id': idDok,
                "shtim_modifikim": "modifikim",
                "zbritje": result
            }), gridSelectedRowCount);
        });
    }
    else {
        myMenu.menu_click_regjistrime(s, e, Utils.buildUrl(emerFaqe, { "shitje_blerje": shitjeBlerje, "shtim_modifikim": "shtim" }), Utils.buildUrl(emerFaqe, {
            "shitje_blerje": shitjeBlerje, 'id': idDok, "shtim_modifikim": "modifikim"}), gridSelectedRowCount);
    }
    switch (e.item.name) {
        case "Paguaj":
            $.ajax({
                pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "merrUrlPaguaj"),
                data: JSON.stringify({ id: idDok, veprimi: shitjeBlerje, vjenNga: 'RegjistrimDokumentash', idPerdoruesi: hfState.Get("idPerdoruesi"), idNdermarrje: hfState.Get("idNdermarrje"), idVitNdermarrje: hfState.Get("idViti")})
            }).done(SuccededCallbackPaguaj);
            e.processOnServer = false;
            break;
        case "Fshi":
            e.processOnServer = false;
            if (shitjeBlerje == 'shitje' || shitjeBlerje == 'shitjediscount' || shitjeBlerje == 'bazaar' || shitjeBlerje == 'blerje') {            
                gridaDokumentave.GetSelectedFieldValues('IdShitjeKoka;IdKonfigAmbjente;Kase', SuccededCallbackSelect);
            }
            else {
                identifikuesPyetje = "FshiDok";
                myMesazh.ShtoMesazh({
                    type: "confirm",
                    UseCancelButton: true,
                    text: hfState.Get("msgJuKeniZgjedhur") + nrreshtash + hfState.Get("msgRreshta") + hfState.Get("labelAdministrimiMsgJeniSigurt"),
                    modal: true,
                    idGjuha: hfState.Get("idGjuha"),
                    okClick: PoClick,
                    cancelClick: JoClick
                });
            }
            break;
        case "Klono":
            gridaDokumentave.GetRowValues(gridIndexModifiko, 'IdNivel;IdKonfigAmbjente;IdShitjeKoka', function (values) {
                if (values.length < 1 || !values[0]) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokZgjidhniNjeDokument"));
                    return;
                }
                var katDokAndKomponentObj = [{ idKatDok: 1, komponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje' }, { idKatDok: 2, komponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje' }, { idKatDok: 6, komponente: '' }];
                popUpKlonimiFunctions.showPopUp(values[0], values[1], values[2], katDokAndKomponentObj);
            });            
            e.processOnServer = false;
            break;
        case "Fiskalizo":
            myMesazh.ShtoMesazh({ type: "alert", text: 'Ju lutem prisni pak sekonda...', timeout: false });
            break;
        case "E-invoice":
            Utils.shfaqLoadingGif();
            gridaDokumentave.GetSelectedFieldValues('IdNivel;IdKonfigAmbjente;IdShitjeKoka;EIC', function (values) {
                if (values.length < 1 || !values[0]) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokZgjidhniNjeDokument"));
                    return;
                }
                var eics = [];
                for (var i = 0; i < values.length; i++) {
                    eics.push(values[i][3]);
                }
                var katDokAndKomponentObj = [{ idKatDok: 1, komponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje' }, { idKatDok: 2, komponente: 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje' }, { idKatDok: 6, komponente: '' }];
                popUpEinvoiceFunctions.showPopUp(values[0], values[1], values[2], katDokAndKomponentObj, eics);
            });
            e.processOnServer = false;
            break;
        case "KthimB":
        case "KthimSh":
            kthim(idDok);
            e.processOnServer = false;
            break;
        case 'KthimVod':
            kthimVod(idDok, gridaDokumentave);
            e.processOnServer = false;
            break;
        case 'Bli':
            bli(idDok);
            e.processOnServer = false;
            break;
        case "Konverto":
            var callBackFunction = function () {
                gridaDokumentave.GetSelectedFieldValues('IdShitjeKoka;NrDok;IdNivel;IdKlientFurnitor;Status Porosie;IdMonedha', OnGridSelectionComplete);
            };
            gridaDokumentave.GetSelectedFieldValues('IdNivel', function (values) {
                kontrolloTeDrejtaPerNivelRegjistrimiDokumenti(values, "Konverto", callBackFunction);
            });
            e.processOnServer = false;
            break;
        case "AutoKonverto":
            e.processOnServer = false;
            AutoKonverto(gridaDokumentave);
            break;
        case "Trasfero":
            if (gridSelectedRowCount == 0) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokZgjidhniNjeDokument"));
                e.processOnServer = false;
                return;
            }
            Utils.shfaqLoadingGif();
            break;
        case "Arkiva":
            var callBackFunction = function () {
                ButtonClickArkiva(idDok);
            };
            gridaDokumentave.GetSelectedFieldValues('IdNivel', function (values) {
                kontrolloTeDrejtaPerNivelRegjistrimiDokumenti(values, "Arkiva", callBackFunction);
            });
            e.processOnServer = false;
            break;
        case "Eksporto":
            e.processOnServer = false;
            gridaDokumentave.GetSelectedFieldValues('IdShitjeKoka', OnGridSelectionCompleteEksport);
            break;
        case "LidhArketim":
            e.processOnServer = false;
            Utils.shfaqLoadingGif();
            gridaDokumentave.GetSelectedFieldValues('IdShitjeKoka;NrDok;IdKonfigAmbjente;IdKlientFurnitor;Status Porosie;IdMonedha', OnGridSelectionCompleteArketim);
            break;
        case "PrintPreview":
            if (grid_RegDok.GetSelectedRowCount() <= 1) {
                e.processOnServer = false;
                Print();
            }
            break;
    }
}
function Print() {
    var index = grid_RegDok.GetFocusedRowIndex();
    if (index == -1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjMagMesazhZgjidhniFaturePerPrintim"));
        return;
    }
    grid_RegDok.GetRowValues(index, "IdShitjeKoka;NrDok;PershkrimKonfigDokumenti;IdStatusDok;IdRaportDesing", function (result) {
        var idkoka = result[0];
        var nrDok = result[1];
        var pershkrim = result[2].toLowerCase();
        var idstatusdok = result[3];
        var idDesign = result[4];
        if (idkoka == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("regjMagMesazhZgjidhniFaturePerPrintim"));
            return;
        }
        if ((pershkrim.indexOf("vodafone one") !== -1 && idstatusdok == "0") || pershkrim.indexOf("porosi bazaar") !== -1 || pershkrim.indexOf("porosi summer promo") !== -1) {
            myMesazh.ShtoMesazhGabimi("Nuk mund te printoni porosi!");
            return;
        }
        if (idDesign == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("regjShitjeMesazhSkaFormatPerPrintim"));
            return;
        }

        window.open("RaportiShpejte.aspx?Sesioni=false&idraporti=0&idDokumenti=" + idkoka + "&printo=0&raportdyte=jo&iddesign=" + idDesign + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KaGaranciShitja"),
            data: JSON.stringify({ idShitjeKoka: idkoka })
        }).done(function (result) {
            if (result.KaGaranci) {
                window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=formatFleteGaranci&idDokumenti=" + idkoka + "&printo=0&raportdyte=po" + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
            }
        });
    });
}



function kontrolloTeDrejtaPerNivelRegjistrimiDokumenti(values, veprimi, callBackFunction) {
    var shitjeBlerje = Utils.getUrlVar('shitje_blerje');
    if (shitjeBlerje == 'shitjediscount' || shitjeBlerje == 'bazaar' || values.length === 0) {
        callBackFunction();
        return;
    }
    var idNivelRegjistrimi = values[0];
    var teDrejtaNivele = JSON.parse(hfState.Get("teDrejtaNivele"));
    var teDrejtaNiveli = teDrejtaNivele.filter(function (eDrejta) { return eDrejta.IdNivelRegjistrimi == idNivelRegjistrimi; })[0];
    var kaTeDrejta = false;
    var eDrejta = '';
    switch (veprimi) {
        case 'Klono':
            kaTeDrejta = teDrejtaNiveli.DShtim || teDrejtaNiveli.DShtimDraft;
            eDrejta = "Shtimi";
            break;
        case 'Konverto':
            kaTeDrejta = teDrejtaNiveli.DKonverto;
            eDrejta = "Konvertimi";
            break;
        case 'Arkiva':
            kaTeDrejta = teDrejtaNiveli.DArkiva;
            eDrejta = " per Arkiven";
            break;
        default:
            break;
    }
        
    if (kaTeDrejta)
        callBackFunction();
    else
        myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta " + eDrejta + " per nenkategorine: " + teDrejtaNiveli.PershkrimKomponente);
}

function kthimVod(idDok) {
    if (idDok === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        return;
    }
    Utils.shfaqLoadingGif();
    $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKthim"), data: JSON.stringify({ ids: idDok, kthyer: true, pageId: window['CurrentPageId'] }) })
        .done(SuccededCallbackKthimVod);
}

function kthim(idDok) {
    if (idDok === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        return;
    }
    Utils.shfaqLoadingGif();
    $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "merrUrlKthim"), data: JSON.stringify({ id: idDok, idNdermarrje: hfState.Get("idNdermarrje"), pageId: window['CurrentPageId'] }) }).done(SuccededCallbackKthim);
}

function bli(idDok) {
    if (idDok === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        return;
    }
    Utils.shfaqLoadingGif();
    $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKthim"), data: JSON.stringify({ ids: idDok, kthyer: false, pageId: window['CurrentPageId'] }) }).done(SuccededCallbackBli);
}

function SuccededCallbackBli(result) {
    if (result[1] != "") {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhGabimi(result[1]);        
    }
    else
        myFaqeCelje.kontrolloTeDrejta(Utils.buildUrl('Shto_RegjistrimDokumentash.aspx', { 'shitje_blerje': Utils.getUrlVar('shitje_blerje'), 'id': result[0], 'shtim_modifikim': 'bli', 'pageCacheId': window['CurrentPageId'] }));
}

function SuccededCallbackKthimVod(result) {    
    if (result[1] != "") {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhGabimi(result[1]);        
    }
    else
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + Utils.getUrlVar('shitje_blerje') + '&id=' + result[0] + '&shtim_modifikim=kthimVod&pageCacheId=' + window['CurrentPageId']);
}

function OnGridSelectionCompleteArketim(values) {
    if (values.length === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokZgjidhniTePakten1DokPerLidhje"))
        return;
    }

    var ids = new Array();
    for (i = 0; i < values.length; i++) {

        ids[i] = values[i][0];
        status[i] = values[i][4];
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "LidhArketime"),
        data: JSON.stringify({ ids: ids, idNdermarrje: hfState.Get("idNdermarrje"), idPerdoruesi:hfState.Get("idPerdoruesi"), idGjuha : hfState.Get("idGjuha")})
    }).done(SuccededCallbackArketime);  
}

function ButtonClickArkiva(idDok) {//po
    if (idDok === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        return;
    }
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=' + Utils.getUrlVar('shitje_blerje') + '&idDok=' + grid_RegDok.GetRowKey(grid_RegDok.GetFocusedRowIndex())
        //+ '&shtim_modifikim=modifikim'
        );
    popupUniversal.Show();
}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
}

var klientiDokKonv = -1;
var idMonedhaDokKonv = -1;
function OnGridSelectionComplete(values) {
    if (values.length === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokZgjidhniTePakten1DokPerKonvertim"));
        return;
    }
    Utils.shfaqLoadingGif();
    var idnivel = values[0][2];    
    klientiDokKonv = values[0][3];
    idMonedhaDokKonv = values[0][5];
    var ids = new Array();
    var status = new Array();
    for (i = 0; i < values.length; i++) {
        if (values[i][2] != idnivel) {
            Utils.hiqLoadingGif();
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeJeneTeSeNjejtesNenkategori"));
            return;
        }
        else if (idMonedhaDokKonv == 0 && values[i][5] != 0)
            idMonedhaDokKonv = values[i][3];
        else if (values[i][5] != 0 && idMonedhaDokKonv != values[i][5]) {
            Utils.hiqLoadingGif();
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeKeneTeNjejtenMonedhe"));
            return;
        }
        if (klientiDokKonv != values[i][3]) {
            if (!grid_RegDok.cpLKDKN) {
                Utils.hiqLoadingGif();
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeKeneTeNjejtinKlient"));
                return;
            }
            klientiDokKonv = -1;
        }
        ids[i] = values[i][0];
        status[i] = values[i][4];
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKonvertuar"),
        data: JSON.stringify({
            ids: ids, kodkonfig: cmbKonfigurimi.GetText(), idNdermarrje: hfState.Get("idNdermarrje"), idPerdoruesi: hfState.Get("idPerdoruesi"),
            idGjuha: hfState.Get("idGjuha"), pageId: window['CurrentPageId']
        })
    }).done(SuccededCallbackKonvertime);
}

function AutoKonverto(gridaDokumentave) {
    gridaDokumentave.GetSelectedFieldValues('IdShitjeKoka;NrDok;IdNivel;IdKlientFurnitor;Status Porosie;IdMonedha;IdKonfigAmbjente;DtDok', function (values) {
        if (values.length === 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokZgjidhniTePakten1DokPerKonvertim"));
            return;
        }
        var ids = new Array();
        for (i = 0; i < values.length; i++) {
            ids[i] = values[i][0];
        }
        $.ajax({
            pritPergjigje: true,
            showLoading: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KonvertoAuto"),
            data: JSON.stringify({ ids: ids, pageId: window['CurrentPageId'] })
        }).done(SuccededCallbackKonvertimeAuto);
    });
}

function SuccededCallbackKonvertimeAuto(result) {
    if (result.kaGabime) {
        myMesazh.ShtoMesazh({
            type: "confirm",
            text: hfState.Get("msgUKonvertua") + result.nrReshtaKonvertuar + " " + hfState.Get("msgRreshtatDeshtuan") + " " + result.nrRreshtaMeGabime + hfState.Get("msgRreshta") + " " + hfState.Get("hapListenPerMeShumeInfo"),
            idGjuha: hfState.Get("idGjuha"),
            okClick: function (noty) {
                window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo&scopeID=" + Utils.getUrlVar("scopeID"));
            },
            cancelClick: function (noty) {

            }
        });
    }
    else
        myMesazh.ShtoMesazhSuksesi(hfState.Get("msgAllConverted"));
}

function SuccededCallbackArketime(result) {
    grid_RegDok.PerformCallback("rilodo");
    if (result == "U riruajten te gjitha rreshtat!")
        myMesazh.ShtoMesazhSuksesi(result);
    else {
        myMesazh.ShtoMesazhGabimi(result);
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo&scopeID=" + Utils.getUrlVar("scopeID"));
    }
}

function OnGridSelectionCompleteEksport(values) {
    var shitjeBlerje = Utils.getUrlVar('shitje_blerje');
    if (shitjeBlerje == 'shitje')
        myButtonClickLupa.ButtonClickLupaEksporto(values, shitjeBlerje, 1, 'Shitje', 'Format Standart Shitje me Artikuj');
    else
        myButtonClickLupa.ButtonClickLupaEksporto(values, shitjeBlerje, 2, 'Blerje', 'Format Standart Blerje me Artikuj');



}

var konfigurimet, idte;
function SuccededCallbackKonvertime(result) {
    Utils.hiqLoadingGif();
    var nivelet = result.nivelet;
    var colKonfig = result.colKonfig;
    var teDrejtaNivele = JSON.parse(hfState.Get("teDrejtaNivele"));

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
    var teDrejtaNiveli;

    for (i = 0; i < nivelet.length; i++) {
        teDrejtaNiveli = teDrejtaNivele.filter(function (eDrejta) { return eDrejta.IdNivelRegjistrimi == nivelet[i].IdNivel; })[0];
        if (teDrejtaNiveli && !teDrejtaNiveli.DAmb && !teDrejtaNiveli.DShtim && !teDrejtaNiveli.DShtimDraft)
            continue;

        var kakonf = false;
        for (m = 0; m < colKonfig.length; m++)
            if (colKonfig[m].IdNivel == nivelet[i].IdNivel) {
                kakonf = true;
                break;
            }
        if (kakonf)
            cmbKonverto.AddItem(nivelet[i].Kodi, nivelet[i].IdNivel);
    }
    cmbKonverto.EndUpdate();
    if (cmbKonverto.GetItemCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukMundTeKonvertohet"));
        return;
    }
    cmbKonverto.SelectIndex(0);
    cmbKonf.BeginUpdate();
    cmbKonf.ClearItems();
    for (i = 0; i < colKonfig.length; i++)
        if (colKonfig[i].IdNivel == cmbKonverto.GetValue())
            cmbKonf.AddItem(colKonfig[i].KodKonfigAmbjente, colKonfig[i].IdKonfigAmbjente);
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
    Utils.konverto(idte, "", "", SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen);
}

function SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen() {
    
    var konvertoTekst = cmbKonverto.GetText()
        , konvertoVlere = cmbKonverto.GetValue()
        , konfVlere = cmbKonf.GetValue();
    if (konvertoTekst == 'FD' || konvertoTekst == 'UD' || konvertoTekst == 'FH' || konvertoTekst == 'UH') //nqs eshte dok magazine
        myFaqeCelje.kontrolloTeDrejta(Utils.buildUrl('Shto_RegjistrimMagazine.aspx', {
            'lloj': (konvertoTekst == 'FH' || konvertoTekst == 'UH' ? 'hyrje' : 'dalje'),
            'id': idte[0],
            'shtim_modifikim': 'konvertim',
            'niveli': konvertoVlere,
            'konfigurim': konfVlere,
            'fsh': 'po',
            'klientKonv': klientiDokKonv,
            'pageCacheId' : window['CurrentPageId']
        }));
    else {
        var shitjeBlerje = Utils.getUrlVar('shitje_blerje');
        var shtim_modifikim = 'konvertim';
        if ((shitjeBlerje == 'shitje' || shitjeBlerje == 'shitjediscount' || shitjeBlerje == 'bazaar') && (konvertoTekst == 'FB' || konvertoTekst == 'UB' || konvertoTekst == 'OB' || konvertoTekst == 'KB')) {
            shitjeBlerje = 'blerje';
            shtim_modifikim = 'konvertimblerje';
        }
        myFaqeCelje.kontrolloTeDrejta(Utils.buildUrl('Shto_RegjistrimDokumentash.aspx', {
            'shitje_blerje': shitjeBlerje,
            'id': idte[0],
            'shtim_modifikim': shtim_modifikim,
            'niveli': konvertoVlere,
            'konfigurim': konfVlere,
            'klientKonv': klientiDokKonv,
            'pageCacheId': window['CurrentPageId']
        }));
    }
}

function SuccededCallbackKthim(result) {    
    if (result[1] === "ska te drejta") {
        Utils.hiqLoadingGif();
        alert(hfState.Get("msgNukKeniTeDrejtaNeKeteAmbjent"));        
    }
    else if (result[1] === "draft") {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJoKthimFatureDraft"));        
    }
    else if (result[1] === "joFature") {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJoKthimDokFatOferteKerkese"));        
    }
    else if (result[1] === "likuiduar") {
        //Utils.hiqLoadingGif();
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + Utils.getUrlVar('shitje_blerje') + '&id=' + grid_RegDok.GetRowKey(grid_RegDok.GetFocusedRowIndex()) + '&shtim_modifikim=kthim&pageCacheId=' + window['CurrentPageId'] + '&likuiduar=true');
    }
    else if (result[1] === "negative") {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJoKthimFatureVlereNegative"));        
    }
    else myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + Utils.getUrlVar('shitje_blerje') + '&id=' + grid_RegDok.GetRowKey(grid_RegDok.GetFocusedRowIndex()) + '&shtim_modifikim=kthim&pageCacheId=' + window['CurrentPageId']);
}

function SuccededCallbackPaguaj(result) {
    if (result === "ska te drejta")
        alert(hfState.Get("msgNukKeniTeDrejtaNeKeteAmbjent"));
    else if (result === "draft")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeLikuidoniNjeFatureDraft"));
    else if (result === "joFature")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKonvertohenDokumentatFatOferteKerkese"));
    else if (result === "likuiduar")
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokILikuiduar"));
    else if (result === 'totali0')
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLikuidohetTotali0"));
    else myFaqeCelje.kontrolloTeDrejta(result);
}

//var mbush = false;

function OnGridDoubleClick(s, e, index) {
    grid_RegDok.GetRowValues(grid_RegDok.GetFocusedRowIndex(), 'TotaliMeZbritjeMeTVSH', function (result) {
        myMenu.ShikoClick(e, Utils.buildUrl('Shto_RegjistrimDokumentash.aspx', { 'shitje_blerje': Utils.getUrlVar('shitje_blerje'), 'id': s.GetRowKey(s.GetFocusedRowIndex()), 'shtim_modifikim': 'modifikim','zbritje': result }));
    });
}
    
    

function clickExport(e) {
    if (grid_RegDok.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

$(document).ready(function () {
    $(document).keydown(function (e) {//po
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 120:
                e.preventDefault();
                e.item = {};
                e.item.name = 'Ruaj';
                e.processOnServer = true;
                var sender = 'tastiera';
                menu_click(sender, e);
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
    merrKonfigurimeWebhook(hfState.Get("idNdermarrje"))
});

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RegjistrimDokumentash.aspx?shitje_blerje=' + Utils.getUrlVar("shitje_blerje"), 0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    $('#dvMenu').show();
}

function OnError(message, context) {
    if (message == "Session TimeOut")
        ndryshoUrlFrame(Paths.defaultLoginPath);
}

function selection(index) {
    hf = $("#hfReshtaTeSelektuar")[0];
    hf.value = grid_RegDok.GetSelectedKeysOnPage();
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
    callWebserviceKonfigurimi("505", cmbKonfigurimi.GetText());
}

function SuccededCallbacPeriudhaKusht(result) {
    hfState.Set("PeriudhaSelektuar", result);
    radDtDok.SetSelectedIndex(result);
}

function onSelectionChanged(s, e) {
    hfState.Set('PeriudhaSelektuar', radDtDok.GetSelectedIndex());
    grid_RegDok.PerformCallback();
}

function ndryshoKonfiguriminInit() {

}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    grid_RegDok.PerformCallback(idKomp + ";" + kodKonf);
}

function EndRequestHandler(sender, args) {
    PrintPreview("cpHapFaqe");
    PrintPreview("cpHapFaqe1");


}
function PrintPreview(propertyName) {
    var faqe = grid_RegDok[propertyName];
    if (faqe) {
        delete grid_RegDok[propertyName];
        window.open(faqe + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
    }
}
function kontrolloNivel(result) {
    var ids = new Array();
    var idNivele = new Array();
    var niveli = result[0][1]; //ruajme id e nivelit te dokumentit te pare te selektuar
    for (i = 0; i < result.length; i++) {
        if (result[i][1] != niveli) { //nqs id e nivelit nuk eshte e njejte me ate te dokumentit te pare, atehere nuk hapet raporti
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeJeneTeSeNjejtesNenkategori"));
            return;
        }
        ids[i] = result[i][0];
        idNivele[i] = result[i][1];
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "janeUrdherShitje"),
        data: JSON.stringify({ ids: ids, idNivele: idNivele })
    }).done(hapRaportTeFiltruar);
}

function kontrolloNivelPorosiDealer(result) {
    var ids = new Array();
    var idNivele = new Array();
    var niveli = result[0][1]; //ruajme id e nivelit te dokumentit te pare te selektuar
    for (i = 0; i < result.length; i++) {
        if (result[i][1] != niveli) { //nqs id e nivelit nuk eshte e njejte me ate te dokumentit te pare, atehere nuk hapet raporti
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeJeneTeSeNjejtesNenkategori"));
            return;
        }
        ids[i] = result[i][0];
        idNivele[i] = result[i][1];
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "janeOferteShitje"),
        data: JSON.stringify({ ids: ids, idNivele: idNivele })
    }).done(hapRaportTeFiltruarPorosiDealer);
}
function ktheVleratEShitjesPerFiskalizimin(result) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheVleratEShitjesPerFiskalizimin"),
        data: JSON.stringify({ idKokaShitje: result[0] })
    }).done(hapFaturenFiskalizimi);
}
function kontrolloNivelOferteBlerje(result) {
    var ids = new Array();
    var idNivele = new Array();
    var niveli = result[0][1]; //ruajme id e nivelit te dokumentit te pare te selektuar
    for (i = 0; i < result.length; i++) {
        if (result[i][1] != niveli) { //nqs id e nivelit nuk eshte e njejte me ate te dokumentit te pare, atehere nuk hapet raporti
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeJeneTeSeNjejtesNenkategori"));
            return;
        }
        ids[i] = result[i][0];
        idNivele[i] = result[i][1];
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "janeOferteBlerje"),
        data: JSON.stringify({ ids: ids, idNivele: idNivele })
    }).done(hapRaportTeFiltruarOferteBlerje);
}

function hapRaportTeFiltruar(result) {
    if (result.janeUSH == false) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukJaneUrdherShitje"));
    }
    else {
        var width = $(window).width();
        var idte = '';//ruajme id e dokumentave te selektuara te ndara me '-' qe t'ia kalojme raportit ne url
        var idDok = result.idte;
        for (var i = 0; i < idDok.length; i++) {
            if (idte == '')
                idte = idDok[i];
            else
                idte = idte + '-' + idDok[i];
        }
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=procedimProdhimi&printo=0&kodkonf=&dtdok=&artGjendjeZero=Po&idDokKonv=" + idte, "_blank");
    }
}

function hapRaportTeFiltruarPorosiDealer(result) {
    if (result.janeOSH == false) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukJaneOferteShitje"));
    }
    else {
        var width = $(window).width();
        var idte = '';//ruajme id e dokumentave te selektuara te ndara me '-' qe t'ia kalojme raportit ne url
        var idDok = result.idte;
        for (var i = 0; i < idDok.length; i++) {
            if (idte == '')
                idte = idDok[i];
            else
                idte = idte + '-' + idDok[i];
        }
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=porosiDealerVodafone&printo=0&kodkonf=&dtdok=&artGjendjeZero=Po&idDokKonv=" + idte, "_blank");
    }
}

function hapRaportTeFiltruarOferteBlerje(result) {
    if (result.janeOB == false) {
        myMesazh.ShtoMesazhGabimi("Dokumentet nuk jane oferte blerje!");
    }
    else {
        var width = $(window).width();
        var idte = '';//ruajme id e dokumentave te selektuara te ndara me '-' qe t'ia kalojme raportit ne url
        var idDok = result.idte;
        for (var i = 0; i < idDok.length; i++) {
            if (idte == '')
                idte = idDok[i];
            else
                idte = idte + '-' + idDok[i];
        }
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=ofertBlerje&printo=0&idDokumenti=" + idte, "_blank");
    }
}

function PoClick() {
    Utils.shfaqLoadingGif();
    switch (identifikuesPyetje) {
        case "FshiDok":            
            FshiDokument();
            break;
        case "Rivleresimi":
            Rivleresim();
            break;
        default:
            return;
    }
}

function JoClick() {
    return;
}

function FshiDokument() {
    var guidString = hfState.Get("guidString");
    var komponente = "RegjistrimDokumentash.aspx?shitje_blerje=" + Utils.getUrlVar("shitje_blerje") + "_" + cmbKonfigurimi.GetText() + "_" + hfState.Get("idViti") + "_" + radDtDok.GetSelectedItem().value;
    var komponShitje_blerje = Utils.getUrlVar("shitje_blerje");
    var komponPerTedrejtat = "RegjistrimDokumentash.aspx?shitje_blerje=" + Utils.getUrlVar("shitje_blerje");
    grid_RegDok.GetSelectedFieldValues('IdShitjeKoka', function (result) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "FshiDokument"),
            data: JSON.stringify({ ids: result, komponente: komponente, guidString: guidString, komponShitje_blerje: komponShitje_blerje, komponPerTedrejtat: komponPerTedrejtat })
        }).done(SuccededCallbackDelete)
    });
}

function SuccededCallbackDelete(result) {
    Utils.hiqLoadingGif();
    result.deletedKeys.forEach(function (key, index) {
        grid_RegDok.DeleteRowByKey(key);
    });
    if (result.mesazhSukses != "")
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
    if (result.mesazhRivleresim != "") {
        identifikuesPyetje = "Rivleresimi";
        myMesazh.ShtoPyetje(result.mesazhRivleresim);
    }
    for (var i = 0; i < pageState.webhook.length; i++) {
        var kategoria;
        if (pageState.webhook[i].Kategoria == 1)
            kategoria = "shitje"
        else if (pageState.webhook[i].Kategoria == 1)
            kategoria = "blerje"
        else kategoria = ""
        if ((pageState.webhook[i].Eventi == 3 || pageState.webhook[i].Eventi == 0) && kategoria == Utils.getUrlVar('shitje_blerje') && pageState.webhook[i].Aktive == true) {
         
             if (result.objektifshire.length!=0) {

                $.ajax({
                    type: "POST",
                    url: pageState.webhook[i].Urlpritese,
                    data: result.objektiWebhook,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != null) {
                            alert("Eventi : " + response.Event + ", Kategri : " + response.Kategori + ", Mesazh :" + response.Mesazh);
                        } else {
                            Console.log("Something went wrong");
                        }
                    },
                    failure: function (response) {
                        Console.log(response.responseText);
                    },
                    error: function (response) {
                        Console.log(response.responseText);
                    }
                });
            }
        }
       
    }
}

function Rivleresim() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "BejRivleresim"),
        data: JSON.stringify({ guidString: hfState.Get("guidString") })
    }).done(function (result) {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhSesioni(result);
    });
}
function merrKonfigurimeWebhook(idnderrmarje) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKonfigurimeWebhook"), data: JSON.stringify({ idndermarje: idnderrmarje })
    }).done(SucceededCallbackWebhook);
}

function SucceededCallbackWebhook(result) {


    pageState.webhook = result;


}
function hapFaturenFiskalizimi(result) {
    var a = result.dtKrijimi.replace("+", "%2B");
    window.open(result.linkFiskalizim + result.iic + "&tin=" + result.niptNdermarrje + "&crtd=" + a + "&prc=" + result.totali + "", "_blank");
}