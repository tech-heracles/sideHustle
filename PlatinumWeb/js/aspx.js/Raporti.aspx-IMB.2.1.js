
;
var constanteParashtese = 'ASPxRoundPanel1_ASPxCallbackPanel1_gvFleteKontabelTrupi_cell';
var constantePrapashtese = '_4_';
var editorGlobal;
var editorPershkLlojDok;
var identikuesPerPopupLlogari;
var arkabanka = 0;
var idGjuha;
var eshtememe;
var identifikuesPerPopupKartaKlient;
var RaportiEmerReal;
var DesignRaport;
var eshteHapurRaporti;
var uEkzekutuaFshiRaportNgaSesioni = false;
var identifikuesPerPopup;

function Init() {
    changeName();

    if (hfState.Get("RaportIPaImplementuar")) {
        myMesazh.ShtoMesazh({
            type: "confirm", text: "Ky raport nuk eshte ndertuar, deshironi ta krijoni?", okClick: function (myNotyTextButton) {
                var urlHost = Utils.getServerUrlHost();
                var queryString = {
                    key: hfState.Get("guidString"),
                    subRaport: false,
                    raportdyte: false,
                    idRaportDesign: hfState.Get("IdRaportDesign"),
                    urlReferuesi: location.href,
                    orientimi: hfState.Get("Orientimi"),
                    raportIPaImplementuar: true,
                    SpEmri: hfState.Get("SpEmri"),
                    scopeID: Utils.getUrlVar("scopeID")
                };
                window.open(urlHost + "/Designer.aspx?" + Utils.KonvertoObjectQueryString(queryString))
            }, cancelClick: function () {
                kthehuNeFaqenEMeparshme();
            },
            idGjuha: 0

        });
    }

    eshteHapurRaporti = false;
    RaportiEmerReal = hfState.Get("RaportiEmerReal");
    RaportiDesign=hfState.Get("RaportiDesign");
    identikuesPerPopupLlojDokumenti = "Raporti";
    identikuesPerPopupLlogari = "Raporti";
    identikuesPerPopupBurimi = "Raporti";
    identikuesPerPopupAktiviteti = "Raporti";
    if (RaportiEmerReal === "ditari" || RaportiEmerReal === "RptKartelaLlogarive" || RaportiEmerReal === "KartelaLlogariveMeKunderparti" || RaportiEmerReal === "Rap_BuxhetiDheFondesh" || RaportiEmerReal === "buxhetimiSipasShpenzimeve" || RaportiEmerReal === "buxhetimeSipasUrdherPagesave") 
        identikuesPerPopupKategoriShpenzimi = "RaportiMultiSelect";
    else
        identikuesPerPopupKategoriShpenzimi = "Raporti";
}
function checkText(s, e) {
    myMenu.checkText(s, e);
}
function ktheFiltra(value) {
    if (value == 0)
        pastrofiltrat();
    else if (cmbfiltra.FindItemByText(cmbfiltra.GetInputElement().value) != null)
        window.parent.$.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheVleraFiltri"),
            data: JSON.stringify({ id: value })
        }).done(SuccedktheFiltra);
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function SuccedktheFiltra(result) {

    if (result != "" || result != "undefined") {
        pastrofiltrat();
        var rezultati, filertrupikontrolle, filtertrupivlera, i, val, n;
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(true);
        filertrupikontrolle = result[0];
        filtertrupivlera = result[1];
        for (i = 0; i < filertrupikontrolle.length; i++) {
            val = filertrupikontrolle[i];
            if (val == "") {
                continue;
            }
            if (val.toString() == "radDtDok") {
                try {
                    n = radDtDok.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDok.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDok.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDok, txtNgaDok, txtDeriDok);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }
            if (val.toString() == "radDtSkadence") {
                try {
                    n = radDtSkadence.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtSkadence.GetItem(j).value == filtertrupivlera[i]) {
                            radDtSkadence.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtSkadence, txtNgaSkadence, txtDeriSkadence);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }
            if (val.toString() == "radDtTakimi") {
                try {
                    n = radDtTakimi.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtTakimi.GetItem(j).value == filtertrupivlera[i]) {
                            radDtTakimi.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtTakimi, txtNgaDttakimi, txtDeriDttakimi);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }
            if (val.toString() == "radDtDokKrahasues") {
                try {
                    n = radDtDokKrahasues.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokKrahasues.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokKrahasues.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokKrahasues, txtNgaDok, txtDeriDok);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radDtDokKryesor") {
                try {
                    n = radDtDokKryesor.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokKryesor.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokKryesor.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokKryesor, txtNgaDok, txtDeriDok);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }


            if (val.toString() == "radDtDokAmortizimi") {
                try {
                    n = radDtDokAmortizimi.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokAmortizimi.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokAmortizimi.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokAmortizimi, txtNgaDok, txtDeriDok);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }
            if (val.toString() == "radDtDokKonvertuar") {
                try {
                    n = radDtDokKonvertuar.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokKonvertuar.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokKonvertuar.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokKonvertuar, txtNgaDok, txtDeriDok);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radDtDokLidhes") {
                try {
                    n = radDtDokLidhes.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokLidhes.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokLidhes.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokLidhes, txtNgaDok, txtDeriDok);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radDateLidhes") {
                try {
                    n = radDateLidhes.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDateLidhes.GetItem(j).value == filtertrupivlera[i]) {
                            radDateLidhes.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDateLidhes, txtNgaDokDateLidhes, txtDeriDokDateLidhes);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radDtDokUrdherPagese") {
                try {
                    n = radDtDokUrdherPagese.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokUrdherPagese.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokUrdherPagese.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokUrdherPagese, txtNgaDokUrdherPagese, txtDeriDokUrdherPagese);
                            break;
                        }
                    }
                }
                catch (ex) {
                    
                }
                continue;
            }

            if (val.toString() == "radDtDokAprovimit") {
                try {
                    n = radDtDokAprovimit.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokAprovimit.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokAprovimit.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokAprovimit, txtNgaDokAprovimit, txtDeriDokAprovimit);
                            break;
                        }
                    }
                }
                catch (ex) {

                }
                continue;
            }

            if (val.toString() == "radDtRegj") {
                try {
                    n = radDtRegj.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtRegj.GetItem(j).value == filtertrupivlera[i]) {
                            radDtRegj.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtRegj, txtNgaRegj, txtDeriRegj);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radDtDtFillimi") {
                try {
                    n = radDtDtFillimi.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDtFillimi.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDtFillimi.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDtFillimi, txtNgaDtFillimi, txtDeriDtFillimi);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radDtDokAmortizimi") {
                try {
                    n = radDtRegj.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokAmortizimi.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokAmortizimi.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokAmortizimi, txtNgaDokAmortizim, txtDeriDokAmortizim);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radDtDokAfatKohor") {
                try {
                    n = radDtDokAfatKohor.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokAfatKohor.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokAfatKohor.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokAfatKohor, txtNgaDokAfatKohor, txtDeriDokAfatKohor);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            if (val.toString() == "radPeriudheMaturimi") {
                try {
                    n = radPeriudheMaturimi.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radPeriudheMaturimi.GetItem(j).value == filtertrupivlera[i]) {
                            radPeriudheMaturimi.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radPeriudheMaturimi, txtNgaPeriudheMaturimi, txtDeriPeriudheMaturimi);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }
            if (val.toString() == "radPeriudheFillimi") {
                try {
                    n = radPeriudheFillimi.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radPeriudheFillimi.GetItem(j).value == filtertrupivlera[i]) {
                            radPeriudheFillimi.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radPeriudheFillimi, txtNgaPeriudheFillimi, txtDeriPeriudheFillimi);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }
            if (val.toString() == "radPeriudheMbarimi") {
                try {
                    n = radPeriudheMbarimi.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radPeriudheMbarimi.GetItem(j).value == filtertrupivlera[i]) {
                            radPeriudheMbarimi.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radPeriudheMbarimi, txtNgaPeriudheMbarimi, txtDeriPeriudheMbarimi);
                            break;
                        }
                    }
                }
                catch (ex) {
                }
                continue;
            }

            try {
                var kontrolli = Utils.ktheKontroll(val.toString());
                if (kontrolli != undefined) {
                    if (val.indexOf('cb') == 0)
                        kontrolli.SetChecked(JSON.parse(filtertrupivlera[i]));
                    else
                        kontrolli.SetText(filtertrupivlera[i]);
                    if (!$(kontrolli.GetInputElement()).closest('div').hasClass('radioDate'))
                        kontrolli.SetEnabled(true);
                }
            }
            catch (ex) {
            }
            continue;
        }
    }
    else {
        alert(hfState.Get("msgNukKeniDrejtaPerVeprim"));
    }
    myMesazh.ShtoMesazhSuksesi(hfState.Get("msgRaportiFiltratUNgarkuanMeSukses"));
}

function ButtonClickedLlojDok(editor) {
    var headerText = hfState.Get("msgRaportiZgjidhLlojinEDokumentit");
    var contentUrl;

    switch (RaportiEmerReal) {
        case "KartaSipasAirtimeGrupuar":
            contentUrl = "LupaKonfigurime.aspx?vjenNgaRaporti=true&veprimi=1-6";
            break;
        case "rdlbankaditariklasik":
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31885&vjenNgaRaporti=true';
            break;
        case "regjistriPermbledhesBlerje":
        case "regjistriAnalitikBlerje":
        case "liber_blerje":
        case "fletaKontabel":
        case "blerjeMbi500Euro":
        case "regjisterAnalitikBlerjeFormat2":
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31886&vjenNgaRaporti=true';
            break;
        case "shitjepermbledhese":
        case "regjistriAnalitikShitje":
        case "artikujTeShitur":
        case "liber_shitje":
        case "kerkeseOfertePorosiFaturaShitje":
        case "artikujTeShiturAutorizime":
        case "regjistriPermbledhesShitjeAutorizime":
        case "regjistriAnalitikShitjeAutorizime":
        case "kontratatIMB":
        case "marzhiShitjeveSipasKlienteve":
        case "MarzhiShitjeveSipasArtikujveDheKlienteve":
        case "evidencaShitjeve":
        case "artikujTeShiturGrupe":
        case "kartelaKartaMePike":
        case "regjistriAnalitikShitjeDetajime":
        case "ShitjetSipasSasiveKrahasuese":
        case "marzhiShitjeveDetajimArtikujsh":
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31887&vjenNgaRaporti=true';
            break;
        case "regjistriPermbledhesMagazine":
        case "regjistriPermbledhesMagazine_Format2":
        case "gjendjaMagazine":
        case "gjendjaMagazineSipasAutorizimeve":
        case "regjistriPermbledhesMagazineAutorizime":
        case "regjistriAnalitikMagazineAutorizime":
        case "hyrjeVodafone":
        case "hyrjeVodafoneNdermarrjeBije":
        case "gjendjaMagazinesVlefte":
        case "gjendjaMagazineCmimeShitje":
            contentUrl = 'LupaKonfigurime.aspx?veprimi=6&vjenNgaRaporti=true';
            break;
        case "analizeQendraKostosh":
            contentUrl = 'LupaKonfigurime.aspx?veprimi=75&vjenNgaRaporti=true';
            break;
        case "ekzekutimRezervimesh":
            contentUrl = 'LupaKonfigurime.aspx?veprimi=78&vjenNgaRaporti=true';
            break;
        case "tabelaAmortizimit":
            contentUrl = 'LupaKonfigurime.aspx?veprimi=90&vjenNgaRaporti=true';
            break;
        default:
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=30933&vjenNgaRaporti=true';
            break;
    }
    editorGlobal = editor;
    identikuesPerPopupLlojDokumenti = "raportllojdokumenti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, '900', '700');
}

function ButtonClickedLlojDokLidhes(editor) {
    var headerText = hfState.Get("msgRaportiZgjidhLlojinEDokumentit");
    var contentUrl;
    switch (RaportiEmerReal) {
        case "rdlbankaditariklasik":
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31885&vjenNgaRaporti=true';
            break;
        case "regjistriPermbledhesBlerje":
        case "regjistriAnalitikBlerje":
        case "liber_blerje":
        case "fletaKontabel":
        case "blerjeMbi500Euro":
        case "regjisterAnalitikBlerjeFormat2":
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31886&vjenNgaRaporti=true';
            break;
        case "shitjepermbledhese":
        case "regjistriAnalitikShitje":
        case "artikujTeShitur":
        case "liber_shitje":
        case "kerkeseOfertePorosiFaturaShitje":
        case "artikujTeShiturAutorizime":
        case "regjistriPermbledhesShitjeAutorizime":
        case "regjistriAnalitikShitjeAutorizime":
        case "kontratatIMB":
        case "marzhiShitjeveSipasKlienteve":
        case "MarzhiShitjeveSipasArtikujveDheKlienteve":
        case "evidencaShitjeve":
        case "artikujTeShiturGrupe":
        case "kartelaKartaMePike":
        case "regjistriAnalitikShitjeDetajime":
        case "ShitjetSipasSasiveKrahasuese":
        case "marzhiShitjeveDetajimArtikujsh":
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31887&vjenNgaRaporti=true';
            break;
        case "regjistriPermbledhesMagazine":
        case "regjistriPermbledhesMagazine_Format2":
        case "gjendjaMagazine":
        case "gjendjaMagazineSipasAutorizimeve":
        case "regjistriPermbledhesMagazineAutorizime":
        case "regjistriAnalitikMagazineAutorizime":
        case "hyrjeVodafone":
        case "hyrjeVodafoneNdermarrjeBije":
        case "gjendjaMagazinesVlefte":
        case "gjendjaMagazineCmimeShitje":
            contentUrl = 'LupaKonfigurime.aspx?veprimi=6&vjenNgaRaporti=true';
            break;
        case "ekzekutimRezervimesh":
            contentUrl = 'LupaKonfigurime.aspx?veprimi=78&vjenNgaRaporti=true';
            break;
        case "tabelaAmortizimit":
            contentUrl = 'LupaKonfigurime.aspx?veprimi=90&vjenNgaRaporti=true';
            break;
        default:
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=30933&vjenNgaRaporti=true';
            break;
    }
    editorGlobal = editor;
    identikuesPerPopupLlojDokumenti = "raportllojdokumenti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, '900', '700');
}

function alertme(s) {
    alert(s);
}
function ButtonClickedNjesiVartese() {
var hfNjVartese = $("#hfLupaNjesiVartese")[0];
    var queryStr = hfNjVartese.value;
if (queryStr == undefined)
        queryStr = 1;
    identifikuesPerPopupMagazina = "raportmagazina";
    var headerText = hfState.Get("msgZgjidhNjesiVartese");
    var contentUrl = 'LupaNjesiVartese.aspx?idKonfigAmbjente=' + queryStr;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, '750', '600');
popupUniversal.Show();
}



function ButtonClickedLlogaria(editor) {
    var headerText = hfState.Get("popupAdministrimiUniversal");
    var contentUrl = RaportiEmerReal != "historikRecepturash" ? 'LupaLlogaria.aspx?vjenNgaRaporti=true' : 'LupaLlogaria.aspx?idKonfigAmbjente=6821&vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    if (RaportiEmerReal == "bilanciVertetuesNdermarrje")
        contentUrl += "&Raportuese=true"
    identifikuesPerPopupLlogari = "raportllogariafillim";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

var editorGlobalValue = 0;
///hap lupen e departamentit nendepartamentit
function ButtonClickedDepartamenti(vjenNga, s) {
    if (vjenNga !== 'Nendepartamenti') {
        var idprindi = 0;
        var title = hfState.Get("msgZgjidhniDepartamentin");
    }
    else {
        idprindi = editorGlobalValue;
        title = hfState.Get("msgZgjidhniNenDepartamentin");
    }
    editorGlobal = s;
    myButtonClickLupa.LupaUniversal_Click(title, 'LupaStrukturaAdministrative.aspx?vjenNga=' + vjenNga + '&IdPrindi=' + idprindi + '&raporti=raporti' + '&RaportiEmerReal=' + RaportiEmerReal, 600, 600);
}

function ButtonClickedTitulli(editor) {
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni pozicionin', 'LupaProfesioneTitujPune.aspx?vjenNga=Raporti', '600', '600');
}

function ButtonClickedPolitike(editor) {
    editorGlobal = editor;
    identifikuesPerPopupKartaKlient = "Politike";
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni politiken', 'Shto_PolitikeKartaKlienti.aspx?lupe=true', '800', '600');
}

function ButtonClickedKategoriBuxhetimi(editor) {
    editorGlobal = editor;
    identifikuesPerPopup = "RaportiRialokimidheEkzekutimiBuxhetitQeveritar";
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni Kategorine', 'B_KategoriBuxhetimi.aspx?lupe=true', '800', '600');
}

function ButtonClickedPunonjes(editor) {
    var headerText = hfState.Get("msgZgjidhPunonjesin");
    var contentUrl = 'LupaPunonjes.aspx?vjenNga=raporti';
    var widthLupa = '800';
    var heightLupa = '600';
    editorGlobal = editor;
    if (RaportiEmerReal == "pasqyraSigurimeveRaportuese")
        contentUrl += "&Raportuese=true";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedSeriali(editor) {
    var headerText = hfState.Get("msgZgjidhSerialin");
    var contentUrl = 'Seriale.aspx?idartikulli=0';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}
function ButtonClickedArkaBanka(editor) {
    var headerText = hfState.Get("msgZgjidhArkaBanka");
    var contentUrl;
    var idmod = Utils.getUrlVar('idmod');
    switch (idmod) {
        case "6"://BANKA
            contentUrl = 'LupaBanka.aspx?arkabanka=4&arka=true&vjenNgaRaporti=true';
            if (RaportiEmerReal == 'ditariPermbledhesVeprimeArkeBanke' || RaportiEmerReal == "gjendjaPermbledheseArkeBankeMonedheKerkuar" || RaportiEmerReal == "RegjistriAnalitikIVeprimeveArkeBanke")

                contentUrl += "&PermbledhesArkeBanke=true";
            break;
        case "2"://ARKA
            contentUrl = 'LupaBanka.aspx?arkabanka=3&arka=false&vjenNgaRaporti=true';
            if (RaportiEmerReal == "ArketimetOrare" || RaportiEmerReal == "pagesaDealer" || RaportiEmerReal == "listeArketimeAnullime" ||
                RaportiEmerReal == "postPaidPayments" || RaportiEmerReal == "billPaymentsPerDay" || RaportiEmerReal == "dailyGuaranteePayment" ||
                RaportiEmerReal == "arketimeDitore" || RaportiEmerReal == "listeArketimeAnullimePostpaid")
                contentUrl += "&MerrNdemarrjeBija=true";
            if (RaportiEmerReal == "levizjetelikujditeteve")
                contentUrl += "&PermbledhesArkeBanke=true";

            break;
        default:
            contentUrl = 'LupaBanka.aspx?&vjenNgaRaporti=true';
            break;
    }
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupBanka = "raportbanka";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedLupaKategoriSeriali(editor) {
    var headerText = "Zgjidhni Kategorine e Serialit";
    var contentUrl = 'LupaKategoriSeriali.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}


function ButtonClickedMagazina(editor) {
    var headerText = hfState.Get("msgZgjidhMagazinen");
    switch (RaportiEmerReal) {
        case "tabelaAmortizimit":
        case "kartelaPermbledheseArtikullit":
        case "permbledheseSipasGrupeve":
        case "Depreciation":
        case "gjendjaAseteve":
        case "regjistriAktiveve":
        case "regjisterAsetesh":
            contentUrl = 'LupaMagazina.aspx?aqt=true&vjenNgaRaporti=true';
            break;
        default:
            contentUrl = 'LupaMagazina.aspx?vjenNgaRaporti=true';
            break;
    }
    var widthLupa = '800';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupMagazina = "raportmagazina";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}


function ButtonClickedMagazinaSkadenca(editor) {
    var headerText = hfState.Get("msgZgjidhMagazinen");
    var widthLupa = '800';
    var heightLupa = '600';
    var contentUrl = 'LupaMagazina.aspx?vjenNgaRaporti=true';
    editorGlobal = editor;
    identifikuesPerPopupMagazina = "raportmagazina";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}


function ButtonClickedMagazinaPershkrimi(editor) {
    var headerText = hfState.Get("msgZgjidhMagazinen");
    var widthLupa = '800';
    var heightLupa = '600';
    var contentUrl = 'LupaMagazina.aspx?vjenNgaRaporti=true';
    editorGlobal = editor;
    identifikuesPerPopupMagazina = "raportPershMag";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedKartaKlient(editor) {
    var headerText = hfState.Get("msgZgjidhKartenKlientit");
    var widthLupa = '800';
    var heightLupa = '600';
    contentUrl = 'LupaKartaKlienti.aspx?vjenNgaRaporti=true';


    editorGlobal = editor;
    identifikuesPerPopupKartaKlient = "raportkartaklient";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}
function selectBenPjeseNe(s, e, veprimi) {
    if (s.GetText().indexOf(',') !== -1 || (e.htmlEvent && e.htmlEvent.which === 188)) { //nuk ka presje dhe nuk eshte presje karakteri i fundit i shtypur
        var itemToSelect = veprimi.FindItemByValue("7");
        if (itemToSelect !== null)
            veprimi.SetSelectedItem(itemToSelect);
    }
}

function kontrolloPresje(s, e, kodi) {
    if (kodi.GetText().indexOf(',') === -1)
        return;
    myMesazh.ShtoMesazhInformues(hfState.Get("msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit"));
    var itemToSelect = s.FindItemByValue("7");
    if (itemToSelect !== null)
        s.SetSelectedItem(itemToSelect);
}

function ButtonClickedKlientFurnitor(editor) {
    var headerText = hfState.Get("msgZgjidhKF");
    var contentUrl;
    switch (RaportiEmerReal) {
        case "regjistriPermbledhesBlerje":
        case "regjistriAnalitikBlerje":
        case "liber_blerje":
        case "karteleFurnitori":
        case "situacionFurnitori":
        case "situacionFurnitoriMeGrupime":
        case "fletaKontabel":
        case "blerjeMbi500Euro":
        case "liber_blerjeBizneseTeVogla":
        case "liber_shitjeKosove":
        case "artikujTeBlere":
        case "situacioniMonedheFurnitori":
        case "gjendjeKerkeseArtikujshPerProdhim":
        case "gjendjeKerkeseTotaleRecepturashProdhim":
        case "regjisterAnalitikBlerjeFormat2":
        case "liber_blerje2015":
        case "liber_blerje2015Kosove":
        case "LibriBlerjes2019":
        case "situacionPermbledhesFurnitorNivelRaportues":
        case "konvertimiKontrataBlerje":
        case "kartelaFurnitorit":
        case "blerjeAnalitike":
        case "MaturimiFaturaveTeFurnitoreve":
        case "kartelaArtikullitFormat2Blerje":
            contentUrl = 'LupaKlientFurnitor.aspx?KlientApoFurnitor=Furnitor&vjenNgaRaporti=true';
            break;
        case "shitjepermbledhese":
        case "regjistriAnalitikShitje":
        case "artikujTeShitur":
        case "liber_shitje":
        case "karteleKlienti":
        case "MaturimiFaturaveTeKlienteve":
        case "marzhiShitjeve":
        case "situacionKlienti":
        case "liber_shitjeBizneseTeVogla":
        case "liber_blerjeKosove":
        case "grafikMarzhiShitje":
        case "shlyrjeKlienteshSipasPeriudhes":
        case "produktetSipasPorosiveUnivers":
        case "kerkeseOfertePorosiFaturaShitje":
        case "artikujTeShiturAutorizime":
        case "regjistriPermbledhesShitjeAutorizime":
        case "regjistriAnalitikShitjeAutorizime":
        case "kontratatIMB":
        case "shitjeKlienteSipasMakinave":
        case "situacionKlienteshSipasMakinave":
        case "karteleAnalitikeKlienti":
        case "marzhiShitjeveSipasKlienteve":
        case "MarzhiShitjeveSipasArtikujveDheKlienteve":
        case "shlyerjeKlienteveSipasPeriudhesUniversReklama":
        case "shlyerjeKlienteveSipasMonedheKlient":
        case "marzhiShitjeveMagazina":
        case "evidencaShitjeve":
        case "shitjeGjendjeKlienteshAgjente":
        case "marzhiShitjeveSipasMagazinaveFormat2":
        case "situacionKlienteshMaturime":
        case "artikujTePashitur":
        case "vjetersiDetyrimeshKliente":
        case "konvertimiKontrataShitje":
        case "situacionKlientiAfateMaturimi":
        case "artikujShiturKlienteVartes":
        case "situacioniMonedheKlienti":
        case "artikujTeShiturGrupe":
        case "kartelaKartaMePike":
        case "permbledhesSipasKlientevePajisjeve":
        case "analitikSipasKartave":
        case "regjistriAnalitikShitjeDetajime":
        case "gjendjePermbledhurKartaKlienti":
        case "marzhiShitjesNjesiPerberese":
        case "marzhiShitjeveMagazineNjesiPerberese":
        case "marzhiShitjevePerberesit":
        case "ShitjetSipasSasiveKrahasuese":
        case "marzhiShitjeveTollona":
        case "marzhiShitjeveDetajimArtikujsh":
        case "veprimtariaDitore":
        case "klienteMeAfateMaturimi":
            contentUrl = 'LupaKlientFurnitor.aspx?KlientApoFurnitor=Klient&vjenNgaRaporti=true';
            break;
        default:
            contentUrl = 'LupaKlientFurnitor.aspx?vjenNgaRaporti=true';
            break;
    }
    var widthLupa = '800';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupKlientFurnitori = "raportklientfurnitor";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}


function ButtonClickedKonfigUrdherPagesa(editor, llojiKonfig) {
    var headerText = hfState.Get("msgZgjidhni" + llojiKonfig + "n");
    var contentUrl = 'LupaKonfigUrdherPagesa.aspx?vjenNga=' + llojiKonfig + '&vjenNgaRaporti=true';

    var widthLupa = '800';
    var heightLupa = '600';
    editorGlobal = editor;

    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}
function ButtonClickedKategoriMaturimi(editor) {
    var headerText = hfState.Get("msgZgjidhKategorineEMaturimit");
    var contentUrl = 'LupaAfateMaturimi.aspx?veprimi=1';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupAfateMaturimi = "raportklientfurnitor";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedBtnePerdoruesi(editor) {
    var headerText = hfState.Get("msgZgjidhPerdoruesin");
    var contentUrl = 'LupaPerdorues.aspx?vjenNgaRaporti=true';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedBtnStatusPerdoruesi(editor) {
    var headerText = 'Zgjidh Statusin';
    var contentUrl = 'LupaShopsHierarkiStatus.aspx?vjenNga=Raporti';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedbtnAgjentShitje(editor) {
    var headerText = hfState.Get("msgZgjidhAgjentinEShitjes");
    var contentUrl = 'LupaAgjenteShitje.aspx';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedbtnNivelCmimi(editor) {
    var headerText = hfState.Get("msgZgjidhniNivelinECmimit");
    var contentUrl = 'LupaNivelCmimi.aspx';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedbtnNivelZbritje(editor) {
    var headerText = hfState.Get("msgZgjidhniNivelinECmimit");
    var contentUrl = 'LupaNivelZbritje.aspx';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}


function ButtonClickedKrijuesi(editor) {
    var headerText = hfState.Get("msgZgjidhKrijuesin");
    var contentUrl = 'LupaPerdorues.aspx';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedQendra1() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni qendra', 'Shto_QendraKosto.aspx?lupe=true&llojLupe=prind&vjenNga=Raporti', '900', '600');
}

function ButtonClickedQendra2() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni qendra', 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Raporti1&idPrindi=' + (btnQenderKosto2.GetValue() == null ? 0 : btnQenderKosto2.GetValue()), '900', '600');
}

function merrPrindQK() {
    window.parent.$.ajax({
        pritPergjigje: false,
        url: Utils.getServerApiUrl("ListPagesa", "MerrPrindQK"),
        data: JSON.stringify({ idqk: btnQenderKosto2.GetValue() })
    }).done(SuccedeedPrindQK);
}
function SuccedeedPrindQK(result) {

    btnQenderKosto1.SetSelectedIndex(btnQenderKosto1.SetText(result.Kodi, result.Id));

}

function ButtonClickedGrupimin() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni grupimin', 'LupaGrupimeLocaleGlobale.aspx?vjenNga=RaportiGlobal', '900', '600');
}
var editorGlobalValueGrupimGlobal = 0;
function ButtonClickedGrupimLocal() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni grupimin', 'LupaGrupimeLocaleGlobale.aspx?vjenNga=RaportiLocale&idPrindi=' + editorGlobalValueGrupimGlobal, '900', '600');
}
function merrPrind() {
    window.parent.$.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("ListPagesa", "MerrPrindGrupimi"),
        data: JSON.stringify({ idgrupilocal: btnLocalBand.GetValue() })
    }).done(SuccedeedPrind);
}
function SuccedeedPrind(result) {
    btnGlobalBand.SetText(result.Kodi);
    btnGlobalBand.SetValue(result.Id);

}

function ButtonClickedQenderKosto(editor) {
    var headerText = hfState.Get("msgZgjidhQendrenEKostos");
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    var contentUrl = 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=raporti';
    if (RaportiEmerReal == "analizeQendraKostosh" || RaportiEmerReal == "bilanciQendraKosto"
        || RaportiEmerReal == "cashFlowQendraKosto" || RaportiEmerReal == "teArdhuraShpenzimeQendraKosto"
        || RaportiEmerReal == "RezultatiQendraveTeKostosMeZeraDheNenzera" || RaportiEmerReal == "RaportiQendraveTeKostosSipasZeraveDheNenzeraveTeDetajuar" || RaportiEmerReal == 'RezultatiQendraveTeKostosMeZera')

        contentUrl = 'Shto_QendraKosto.aspx?lupe=true&llojLupe=plote&vjenNga=raporti';
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedFurnitorArt(editor) {
    var headerText = hfState.Get("headerPopUpZgjidhFurnitorin");
    var contentUrl = 'LupaKlientFurnitor.aspx?veprimi=2&vjenNgaRaporti=true';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupKlientFurnitori = "raportklientfurnitor";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedGrupimiDok(editor, grupimi) {
    var contentUrl = 'LupaGrupimDokumenti.aspx?grupi=' + grupimi + '&vjenNgaRaporti=true';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni grupimin e dokumentave", contentUrl, widthLupa, heightLupa);
}

function ButtonClickedKartela(editor, eshteKod) {
    var headerText = hfState.Get("roundPanelZgjidhArtikullin");
    var contentUrl;
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    if (eshteKod)
        identikuesPerPopupArtikulli = "raportArtikull";
    else identikuesPerPopupArtikulli = "raportPershArt";
    var idModuli = Utils.getUrlVar('idmod');
    if (RaportiEmerReal == "historikRecepturash")
        contentUrl = 'LupaArtikull.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value
    else if (idModuli == '21')
        contentUrl = 'LupaArtikull.aspx?aqt=aqt&vjenNgaRaporti=true';
    else
        contentUrl = 'LupaArtikull.aspx?vjenNgaRaporti=true';

    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);

}

function ButtonClickedBurimi(editor) {
    var contentUrl = 'LupaBurime.aspx?vjenNgaRaporti=true';
    var widthLupa = '600';
    var heightLupa = '600';
    editorGlobal = editor;
    //identikuesPerPopupArtikulli = "raportBurim";
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhBurimin"), contentUrl, widthLupa, heightLupa);
}
function ButtonClickedAktiviteti(editor) {
    editorGlobal = editor;
    //identikuesPerPopupArtikulli = "raportBurim";
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhAktivitetin"), 'LupaAktivitete.aspx?vjenNgaRaporti=true', 600, 500);
}

function ButtonClickedKategoriShpenzimi(editor) {
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupKategoriShpenzimi"), 'LupaKategoriShpenzimi.aspx', 680, 600);
}


function ButtonClickedArtikullBurim(editor) {
    var headerText = hfState.Get("roundPanelZgjidhArtikullin");
    var contentUrl = RaportiEmerReal != "historikRecepturash" ? 'LupaArtikull.aspx?vjenNgaRaporti=true' :
        'LupaArtikull.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value;
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupArtikulli = "raportArtikullBurim";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}


function ButtonClickedNjesiProdhimi(editor) {
    var headerText = hfState.Get("roundPanelZgjidhNjesiProdhimi");
    var contentUrl = 'LupaNjesiProdhimi.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupNjesiProdhimi = "raportNjesiProdhimi";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedKategPunonjesish(editor) {
    var widthlupa = 650;
    var heightlupa = 600;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgLupaLlogariShpejteZgjidhniGrupin"), 'LupaGrupPunonjesish.aspx', widthlupa, heightlupa);
}
function ButtonClickedKodbari(editor) {
    var headerText = hfState.Get("msgZgjidhKodbar");
    var contentUrl = 'LupaKodbari.aspx';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupKodbari = "raportKodbari";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}
function ButtonClickedDetajime(editor) {
    var headerText = hfState.Get("msgZgjidhDetajimin");
    var contentUrl;
    if (RaportiEmerReal == "artikujTeBlereSipasSkadences" || RaportiEmerReal == "gjendjaMagazinesSipasSkadences" || RaportiEmerReal == "artikujTeShiturSipasSkadences")
        contentUrl = 'LupaDetajime.aspx?veprimet=raporti';
    else contentUrl = 'LupaDetajime.aspx';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupDetajime = "raportDetajime";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}
function ButtonClickedDetajimePershkrim(editor) {
    var headerText = hfState.Get("msgZgjidhDetajimin");
    var contentUrl;
    contentUrl = 'LupaDetajime.aspx';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupDetajime = "raportDetajimePershkrim";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedPikeShitjeFurnizim(editor) {
    var headerText = hfState.Get("msgZgjidhPikeShitjeFurnizimi");
    var contentUrl;
    switch (RaportiEmerReal) {
        case "shitjepermbledhese":
        case "regjistriAnalitikShitje":
        case "artikujTeShitur":
        case "liber_shitje":
        case "marzhiShitjeve":
        case "grafikMarzhiShitje":
        case "kerkeseOfertePorosiFaturaShitje":
        case "artikujTeShiturAutorizime":
        case "regjistriPermbledhesShitjeAutorizime":
        case "regjistriAnalitikShitjeAutorizime":
        case "shitjeKlienteSipasMakinave":
        case "marzhiShitjeveSipasKlienteve":
        case "MarzhiShitjeveSipasArtikujveDheKlienteve":
        case "marzhiShitjeveMagazina":
        case "evidencaShitjeve":
        case "marzhiShitjeveSipasMagazinaveFormat2":
        case "artikujTePashitur":
        case "artikujTeShiturGrupe":
        case "regjistriAnalitikShitjeDetajime":
        case "marzhiShitjesNjesiPerberese":
        case "marzhiShitjeveMagazineNjesiPerberese":
        case "marzhiShitjevePerberesit":
        case "ShitjetSipasSasiveKrahasuese":
        case "marzhiShitjeveTollona":
        case "marzhiShitjeveDetajimArtikujsh":
            contentUrl = 'LupaPikeShitjeFurnizimi.aspx?shitjeblerje=Shitje&vjenNgaRaporti=true';
            break;
        case "regjistriPermbledhesBlerje":
        case "regjistriAnalitikBlerje":
        case "liber_blerje":
        case "kartelaArtikullitBlerje":
        case "fletaKontabel":
        case "blerjeMbi500Euro":
        case "kontratatIMB":
        case "regjisterAnalitikBlerjeFormat2":
        case "liber_blerje2015":
        case "LibriBlerjes2019":
        case "liber_blerje2015Kosove":
        case "konvertimiKontrataBlerje":
        case "kartelaArtikullitFormat2Blerje":
            contentUrl = 'LupaPikeShitjeFurnizimi.aspx?shitjeblerje=Blerje&vjenNgaRaporti=true';
            break;
        default:
            contentUrl = 'LupaPikeShitjeFurnizimi.aspx?vjenNgaRaporti=true';
            break;
    }
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupKodbari = "raportPikeShitje";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedDegaAdministrative(editor) {
    var headerText = hfState.Get("msgZgjidhDegeAdministrative");
    var contentUrl = 'LupaDegaAdministrative.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupDegaAdministrative = "raportDegaAdministrative";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedLayerElement(editor) {
    var headerText = hfState.Get("zgjidhLayerElem");
    var contentUrl = 'LupaLayerElementi.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupLayerElem = "raportLayerElement";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedGrupim1(editor) {
    var headerText = hfState.Get("msgZgjidhGrupin1TeArtikullit");
    var contentUrl = 'LupaKodifikimArtikulli.aspx?llojkodifikimi=1';
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedGrupimArt1(editor) {
    var headerText = hfState.Get("msgZgjidhGrupin1TeArtikullit");
    var contentUrl;
    switch (RaportiEmerReal) {
        case "permbledheseSipasGrupeve":
            contentUrl = 'LupaKodifikimArtikulli.aspx?llojartikulli=true&llojkodifikimi=1&nivelKodifikimi=1';
            break;
        case "tabelaAmortizimit":
        case "kartelaPermbledheseArtikullit":
        case "Depreciation":
        case "gjendjaAseteve":
        case "aseteJashtePerdorimi":
        case "aseteJashtePerdorimiRezervaRivleresimi":
        case "parashikimAmortizim":
        case "gjendjaAseteveSipasNdermarrjeve":
        case "kartelaPermbledheseArtikullitSipasNdermarrjeve":
        case "regjistriAktiveve":
        case "regjisterAsetesh":
            contentUrl = 'LupaKodifikimArtikulli.aspx?llojartikulli=true&llojkodifikimi=1';
            break;
        default:
            contentUrl = 'LupaKodifikimArtikulli.aspx?llojkodifikimi=1';
            break;
    }
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedGrupim2(editor) {
    var headerText = hfState.Get("msgZgjidhGrupin2TeArtikullit");
    contentUrl = 'LupaKodifikimArtikulli.aspx?llojkodifikimi=2';
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedGrupBanke(editor) {
    var headerText = hfState.Get("msgzgjidhGrupinArkaOseBanka");
    var arka = (Utils.getUrlVar('idmod') != 'undefined' && Utils.getUrlVar('idmod') == '2');
    contentUrl = 'LupaGrupBanke.aspx?veprimi=' + (arka ? 'arka' : 'banka');

    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupGrupBanke = "raportGrupBanke";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedGrupimArt2(editor) {
    var headerText = hfState.Get("msgZgjidhGrupin2TeArtikullit");
    var contentUrl;
    if (Utils.getUrlVar('idmod') != 'undefined' && Utils.getUrlVar('idmod') == '21') {
        contentUrl = 'LupaKodifikimArtikulli.aspx?llojartikulli=true&llojkodifikimi=2';
    } else {
        contentUrl = 'LupaKodifikimArtikulli.aspx?llojkodifikimi=2';
    }
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedGrupimArt3(editor) {
    var headerText = hfState.Get("msgZgjidhGrupin3TeArtikullit");
    var contentUrl;
    if (Utils.getUrlVar('idmod') != 'undefined' && Utils.getUrlVar('idmod') == '21') {
        contentUrl = 'LupaKodifikimArtikulli.aspx?llojartikulli=true&llojkodifikimi=3';
    } else {
        contentUrl = 'LupaKodifikimArtikulli.aspx?llojkodifikimi=3';
    }
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}



function ButtonClickedbtneAuto(editor) {
    var headerText = hfState.Get("msgZgjidhAutomjetin");
    contentUrl = 'LupaAutomjeti.aspx';
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupAutomjet = "raportAuto";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedbtneKompania(editor) {
    editorGlobal = editor;
    var queryStr = "?vjenNgaRaporti=true";
    switch (RaportiEmerReal) {
        case "gjendjaPermbledhurArkesNivelRaportues":
        case "gjendjaPermbledhurBankesNivelRaportues":
        case "permbledheseGrupetNivelRaportues":
        case "gjendjaAseteveSipasNdermarrjeve":
        case "kartelaPermbledheseArtikullitSipasNdermarrjeve":
        case "gjendjaMagazinesNivelRaportues":
        case "situacionPermbledhesKlienteNivelRaportues":
        case "situacionPermbledhesFurnitorNivelRaportues":
        case "permbledhesAseteNivelRaportues":
        case "bilanciEnergjitikPermbledhes":
        case "pasqyraSigurimeveRaportuese":
        case "ditariTotalBankaRaportues":
        case "RapEvidenceBuxheti":
            queryStr += "&Raportuese=true";
            break;
        case "RaportiRialokimidheEkzekutimiBuxhetitQeveritar":
        case "RaportiPlanifikimitMiratimitRialokimitTeBuxhetit":
        case "Rap_HistorikuVeprimeveBuxhetimi":
        case "StrukturaOrganizative":
            queryStr += "&Raportuese=true&KodeBashke=true";
            break;
        case "RapBuxhetiTeArdhuratSipasBurimeve":
        case "RapBuxhetiIKontraktuarPerVitin":
        case "RapPerfitimiBuxhetitSipasArtikujveSherbim":
            queryStr += "&Raportuese=true&NivelStrukture=3";
            break;
        default:
            break;
    }
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpTextZgjidhNdermarrjet"), 'LupaNdermarjeBij.aspx' + queryStr, 750, 600);
}

//function ButtonClickedGrupim1KF(editor) {
//    if (lblGrupPKF.GetText() == "Grup Klienti 1") {
//        var headerText = hfState.Get("msgZgjidhGrupin1TeKlientit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=1&kf=klient&vjenNgaRaporti=true';
//    } else if (lblGrupPKF.GetText() == "Grup Furnitori 1") {
//        headerText = hfState.Get("msgZgjidhGrupin1TeFurnitorit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=1&kf=furnitor&vjenNgaRaporti=true';
//    } else {
//        headerText = hfState.Get("msgZgjidhGrupin1TeKlientFurnitorit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=1&vjenNgaRaporti=true';
//    }
//    var widthLupa = '750';
//    var heightLupa = '600';
//    identifikuesPerPopupKodifikimin = "raportGrupim";
//    editorGlobal = editor;
//    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
//}

//function ButtonClickedGrup1KF(editor, emerLabelGrupimi, llojKodifikimi) {
//    var labelGrupimi = Utils.ktheKontroll(emerLabelGrupimi).GetText();
//    var llojKf;
//    var headerText;
//    switch (labelGrupimi) {
//        case "Grup Klienti 1":
//            headerText = hfState.Get("msgZgjidhGrupin1TeKlientit");
//            llojKf = "klient";
//            break;
//        case "Grup Furnitori 1":
//            headerText = hfState.Get("msgZgjidhGrupin1TeFurnitorit");
//            llojKf = "furnitor";
//            break;
//        default:
//            headerText = hfState.Get("msgZgjidhGrupin1TeKlientFurnitorit");
//            llojKf = "";
//            break;
//    }
//    var contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=' + llojKodifikimi + '&vjenNgaRaporti=true';
//    if (!Utils.IsNullOrEmpty(llojKf))
//        contentUrl += '&kf=' + llojKf;
//    var widthLupa = '750';
//    var heightLupa = '600';
//    identifikuesPerPopupKodifikimin = "raportGrupim";
//    editorGlobal = editor;
//    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
//}

//function ButtonClickedGrupim2KF(editor) {
//    if (lblGrupDKF.GetText() == "Grup Klienti 2") {
//        var headerText = hfState.Get("msgZgjidhGrupin2TeKlientit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=2&kf=klient&vjenNgaRaporti=true';
//    } else if (lblGrupDKF.GetText() == "Grup Furnitori 2") {
//        headerText = hfState.Get("msgZgjidhGrupin2TeFurnitorit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=2&kf=furnitor&vjenNgaRaporti=true';
//    } else {
//        headerText = hfState.Get("msgZgjidhGrupin2TeKlientFurnitorit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=2&vjenNgaRaporti=true';
//    }
//    var widthLupa = '750';
//    var heightLupa = '600';
//    identifikuesPerPopupKodifikimin = "raportGrupim";
//    editorGlobal = editor;
//    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
//}

//function ButtonClickedGrup2KF(editor, emerLabelGrupimi, llojKodifikimi) {
//    var labelGrupimi = Utils.ktheKontroll(emerLabelGrupimi).GetText();
//    var llojKf;
//    var headerText;
//    switch (labelGrupimi) {
//        case "Grup Klienti 2":
//            headerText = hfState.Get("msgZgjidhGrupin2TeKlientit");
//            llojKf = "klient";
//            break;
//        case "Grup Furnitori 2":
//            headerText = hfState.Get("msgZgjidhGrupin2TeFurnitorit");
//            llojKf = "furnitor";
//            break;
//        default:
//            headerText = hfState.Get("msgZgjidhGrupin2TeKlientFurnitorit");
//            llojKf = "";
//            break;
//    }
//    var contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=' + llojKodifikimi + '&vjenNgaRaporti=true';
//    if (!Utils.IsNullOrEmpty(llojKf))
//        contentUrl += '&kf=' + llojKf;
//    var widthLupa = '750';
//    var heightLupa = '600';
//    identifikuesPerPopupKodifikimin = "raportGrupim";
//    editorGlobal = editor;
//    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
//}

//function ButtonClickedGrupim3KF(editor) {
//    if (lblGrupTKF.GetText() == "Grup Klienti 3") {
//        var headerText = hfState.Get("msgZgjidhGrupin3TeKlientit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=3&kf=klient&vjenNgaRaporti=true';
//    } else if (lblGrupTKF.GetText() == "Grup Furnitori 3") {
//        headerText = hfState.Get("msgZgjidhGrupin3TeFurnitorit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=3&kf=furnitor&vjenNgaRaporti=true';
//    } else {
//        headerText = hfState.Get("msgZgjidhGrupin3TeKlientFurnitorit");
//        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=3&vjenNgaRaporti=true';
//    }
//    var widthLupa = '750';
//    var heightLupa = '600';
//    identifikuesPerPopupKodifikimin = "raportGrupim";
//    editorGlobal = editor;
//    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
//}

//function ButtonClickedGrup3KF(editor, emerLabelGrupimi, llojKodifikimi) {
//    var labelGrupimi = Utils.ktheKontroll(emerLabelGrupimi).GetText();
//    var llojKf;
//    var headerText;
//    switch (labelGrupimi) {
//        case "Grup Klienti 3":
//            headerText = hfState.Get("msgZgjidhGrupin3TeKlientit");
//            llojKf = "klient";
//            break;
//        case "Grup Furnitori 3":
//            headerText = hfState.Get("msgZgjidhGrupin3TeFurnitorit");
//            llojKf = "furnitor";
//            break;
//        default:
//            headerText = hfState.Get("msgZgjidhGrupin3TeKlientFurnitorit");
//            llojKf = "";
//            break;
//    }
//    var contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=' + llojKodifikimi + '&vjenNgaRaporti=true';
//    if (!Utils.IsNullOrEmpty(llojKf))
//        contentUrl += '&kf=' + llojKf;
//    var widthLupa = '750';
//    var heightLupa = '600';
//    identifikuesPerPopupKodifikimin = "raportGrupim";
//    editorGlobal = editor;
//    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
//}

function ButtonClickedGrupeKF(editor, emerLabelGrupimi, llojKodifikimi) {
    var labelGrupimi = Utils.ktheKontroll(emerLabelGrupimi).GetText();
    var llojKf;
    var headerText;
    switch (labelGrupimi) {
        case "Grup Klienti 1":
            headerText = hfState.Get("msgZgjidhGrupin1TeKlientit");
            llojKf = "klient";
            break;
        case "Grup Klienti 2":
            headerText = hfState.Get("msgZgjidhGrupin2TeKlientit");
            llojKf = "klient";
            break;
        case "Grup Klienti 3":
            headerText = hfState.Get("msgZgjidhGrupin3TeKlientit");
            llojKf = "klient";
            break;
        case "Grup Furnitori 1":
            headerText = hfState.Get("msgZgjidhGrupin1TeFurnitorit");
            llojKf = "furnitor";
            break;
        case "Grup Furnitori 2":
            headerText = hfState.Get("msgZgjidhGrupin2TeFurnitorit");
            llojKf = "furnitor";
            break;
        case "Grup Furnitori 3":
            headerText = hfState.Get("msgZgjidhGrupin3TeFurnitorit");
            llojKf = "furnitor";
            break;
        default:
            headerText = llojKodifikimi == 1 ? hfState.Get("msgZgjidhGrupin1TeKlientFurnitorit") : llojKodifikimi == 2 ? hfState.Get("msgZgjidhGrupin2TeKlientFurnitorit") : hfState.Get("msgZgjidhGrupin3TeKlientFurnitorit");
            llojKf = "";
            break;
    }
    var contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=' + llojKodifikimi + '&vjenNgaRaporti=true';
    if (!Utils.IsNullOrEmpty(llojKf))
        contentUrl += '&kf=' + llojKf;
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function klickselectedvalueregj(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaRegj.SetEnabled(true);
            txtDeriRegj.SetEnabled(true);
        }
        else {
            txtNgaRegj.SetEnabled(false);
            txtDeriRegj.SetEnabled(false);
        }
    }
}

function klickselectedvalueFillimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaDtFillimi.SetEnabled(true);
            txtDeriDtFillimi.SetEnabled(true);
        }
        else {
            txtNgaDtFillimi.SetEnabled(false);
            txtDeriDtFillimi.SetEnabled(false);
        }
    }
}

function klickselectedvalueMbarimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaDtMbarimi.SetEnabled(true);
            txtDeriDtMbarimi.SetEnabled(true);
        }
        else {
            txtNgaDtMbarimi.SetEnabled(false);
            txtDeriDtMbarimi.SetEnabled(false);
        }
    }
}


function klickselectedvaluedtkrijimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtDtNgaKrijimiAqtSerial.SetEnabled(true);
            txtDtDeriKrijimiAqtSerial.SetEnabled(true);
        }
        else {
            txtDtNgaKrijimiAqtSerial.SetEnabled(false);
            txtDtDeriKrijimiAqtSerial.SetEnabled(false);
        }
    }
}

function klickselectedvalueamort(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaDokAmortizim.SetEnabled(true);
            txtDeriDokAmortizim.SetEnabled(true);
        }
        else {
            txtNgaDokAmortizim.SetEnabled(false);
            txtDeriDokAmortizim.SetEnabled(false);
        }
    }
}

function klickselectedvalueReg(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtDeriDokReg.SetEnabled(true);
            txtNgaDokReg.SetEnabled(true);
            //txtDeriDokReg.SetEnabled(true);
        }
        else {
            txtDeriDokReg.SetEnabled(false);
            txtNgaDokReg.SetEnabled(false);
            // txtNgaDokReg.SetEnabled(false);
        }
    }
}

function klickselectedvaluePeriudheMaturimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaPeriudheMaturimi.SetEnabled(true);
            txtDeriPeriudheMaturimi.SetEnabled(true);
        }
        else {
            txtNgaPeriudheMaturimi.SetEnabled(false);
            txtDeriPeriudheMaturimi.SetEnabled(false);
        }
    }
}

function klickselectedvaluePeriudheFillimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaPeriudheFillimi.SetEnabled(true);
            txtDeriPeriudheFillimi.SetEnabled(true);
        }
        else {
            txtNgaPeriudheFillimi.SetEnabled(false);
            txtDeriPeriudheFillimi.SetEnabled(false);
        }
    }
}

function klickselectedvaluePeriudheMbarimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {

            txtNgaPeriudheMbarimi.SetEnabled(true);
            txtDeriPeriudheMbarimi.SetEnabled(true);

        }
        else {
            txtNgaPeriudheMbarimi.SetEnabled(false);
            txtDeriPeriudheMbarimi.SetEnabled(false);
        }
    }
}

function klickselectedvalueDtDokAprovimit(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaDokAprovimit.SetEnabled(true);
            txtDeriDokAprovimit.SetEnabled(true);
        }
        else {
            txtNgaDokAprovimit.SetEnabled(false);
            txtDeriDokAprovimit.SetEnabled(false);
        }
    }
}

function klickselectedvalueDtDokUrdherPagese(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaDokUrdherPagese.SetEnabled(true);
            txtDeriDokUrdherPagese.SetEnabled(true);
        }
        else {
            txtNgaDokUrdherPagese.SetEnabled(false);
            txtDeriDokUrdherPagese.SetEnabled(false);
        }
    }
}
function klickselectedvalueDtStatus(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaDtStatus.SetEnabled(true);
            txtDeriDtStatus.SetEnabled(true);
        }
        else {
            txtNgaDtStatus.SetEnabled(false);
            txtDeriDtStatus.SetEnabled(false);
        }
    }
}

function klickselectedvaluedok(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 'Periudha') {
            txtNgaDok.SetEnabled(true);
            //  btnPeriudha
            txtDeriDok.SetEnabled(true);
        }
        else {
            txtNgaDok.SetEnabled(false);
            txtDeriDok.SetEnabled(false);
        }
    }
    if (RaportiEmerReal == "shitjeDitore" || RaportiEmerReal == "shitjeDetyrimeshAutorizime" || RaportiEmerReal == "shitjeArketimeDitoreAutorizime") {
        txtDeriDok.SetEnabled(false);
    }
}

function klickselectedvaluedokQenderKosto(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 'Periudha') {
            txtNgaDokQenderKosto.SetEnabled(true);
            //  btnPeriudha
            txtDeriDokQenderKosto.SetEnabled(true);
        }
        else {
            txtNgaDokQenderKosto.SetEnabled(false);
            txtDeriDokQenderKosto.SetEnabled(false);
        }
    }

}

function klickselectedvaluedokFshirje(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 'Periudha') {
            txtNgaDokFshirje.SetEnabled(true);
            //  btnPeriudha
            txtDeriDokFshirje.SetEnabled(true);
        }
        else {
            txtNgaDokFshirje.SetEnabled(false);
            txtDeriDokFshirje.SetEnabled(false);
        }
    }

}

function klickselectedvaluedokAfatKohor(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 'Periudha') {
            txtNgaDokAfatKohor.SetEnabled(true);
            //  btnPeriudha
            txtDeriDokAfatKohor.SetEnabled(true);
        }
        else {
            txtNgaDokAfatKohor.SetEnabled(false);
            txtDeriDokAfatKohor.SetEnabled(false);
        }
    }

}

function klickselectedvaluedokKonvertuar(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 'Periudha') {
            txtNgaDokKonvertuar.SetEnabled(true);
            txtDeriDokKonvertuar.SetEnabled(true);
        }
        else {
            txtNgaDokKonvertuar.SetEnabled(false);
            txtDeriDokKonvertuar.SetEnabled(false);
        }
    }
}

function klickselectedvaluedokKryesor(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 'Periudha') {
            txtNgaDokKryesor.SetEnabled(true);
            //  btnPeriudha
            txtDeriDokKryesor.SetEnabled(true);
        }
        else {
            txtNgaDokKryesor.SetEnabled(false);
            txtDeriDokKryesor.SetEnabled(false);
        }
    }
}


function klickselectedvaluedokLidhes(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 'Periudha') {
            txtNgaDokLidhes.SetEnabled(true);
            //  btnPeriudha
            txtDeriDokLidhes.SetEnabled(true);
        }
        else {
            txtNgaDokLidhes.SetEnabled(false);
            txtDeriDokLidhes.SetEnabled(false);
        }
    }
}




function klickselectedvaluedokPlanifikimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDokKrahasues = theRadio.GetSelectedItem().value;
        if (rblCaseControlDokKrahasues == 'Periudha') {
            txtNgaDtPlanifikimi.SetEnabled(true);
            txtDeriDtPlanifikimi.SetEnabled(true);
        }
        else {
            txtNgaDtPlanifikimi.SetEnabled(false);
            txtDeriDtPlanifikimi.SetEnabled(false);
        }
    }
}

function klickselectedvaluedokProdhimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDokKrahasues = theRadio.GetSelectedItem().value;
        if (rblCaseControlDokKrahasues == 'Periudha') {
            txtNgaDtProdhimi.SetEnabled(true);
            txtDeriDtProdhimi.SetEnabled(true);
        }
        else {
            txtNgaDtProdhimi.SetEnabled(false);
            txtDeriDtProdhimi.SetEnabled(false);
        }
    }
}

function klickselectedvalueDtProdhimi(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            txtNgaDtPorosie.SetEnabled(true);
            txtDeriDtPorosie.SetEnabled(true);
        }
        else {
            txtNgaDtPorosie.SetEnabled(false);
            txtDeriDtPorosie.SetEnabled(false);
        }
    }
}

function merrArkaBanka(s, e) {
    if (s.GetSelectedItem().value == 0) {
        arkabanka = 3;
    }
    else if (s.GetSelectedItem().value == 1) {
        arkabanka = 4;
    }
    else
        arkabanka = 0;
}

function pastro() {
    identikuesPerPopupLlogari = "";
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    if (Utils.getUrlVar('idraporti') == undefined)
        window.parent.callWebServiceKtheInfoLart('Raporti.aspx', 0);

    window.parent.createCookie('adresa', 'Raporti.aspx', 1);
}

function changeButtonsType() {
    //document.querySelectorAll("input[title=Ndihme]")[0].type = "button";
    //document.querySelectorAll("input[title=Minimizo]")[0].type = "button";
    //document.querySelectorAll("input[title=Maksimizo]")[0].type = "button";
}

function OnGridSelectionChanged() {
    grid_Filtrat.GetSelectedFieldValues('IdkokaFilter,KokaFilterKodi;KokaFilterPershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    txtEmerFiltri.SetText(vl[1]);

}

function changeRadioButtonDateValue() {

    if (radNgaDeri1.checked == true) {
        txtNgaRegj.Enabled = true;
        txtDeriRegj.Enabled = true;
    }
    else {
        txtNgaRegj.Enabled = false;
        txtDeriRegj.Enabled = false;
    }
}
function initNgaTakim(s, e) {
    //$(s.GetInputElement()).css('zIndex', 3000);
}



function initNgaDok(s, e) {
    //$(s.GetInputElement()).css('zIndex', 3000);
}
function ngaDokDateChanged(s, e) {
    if (txtDeriDok.GetDate() < txtNgaDok.GetDate())
        txtDeriDok.SetDate(txtNgaDok.GetDate());

    if (RaportiEmerReal == "shitjeDitore" || RaportiEmerReal == "shitjeDetyrimeshAutorizime" || RaportiEmerReal == "shitjeArketimeDitoreAutorizime") {
        txtDeriDok.SetDate(txtNgaDok.GetDate());
    }
}

function ngaDokDateSkadenceChanged(s, e) {
    if (txtDeriSkadence.GetDate() < txtNgaSkadence.GetDate())
        txtDeriSkadence.SetDate(txtNgaSkadence.GetDate());

}

function deriDokDateChanged(s, e) {
    if (RaportiEmerReal == "shitjeDitore" || RaportiEmerReal == "shitjeDetyrimeshAutorizime" || RaportiEmerReal == "shitjeArketimeDitoreAutorizime") {
        txtNgaDok.SetDate(txtDeriDok.GetDate());
    }
}
function ngaDokDateChangedQendraKosto(s, e) {
    if (txtDeriDokQenderKosto.GetDate() < txtNgaDokQenderKosto.GetDate())
        txtDeriDokQenderKosto.SetDate(txtNgaDokQenderKosto.GetDate());
}
function ngaDokDateChangedFshirje(s, e) {
    if (txtDeriDokFshirje.GetDate() < txtNgaDokFshirje.GetDate())
        txtDeriDokFshirje.SetDate(txtNgaDokFshirje.GetDate());
}
function ngaDokDateChangedAfatKohor(s, e) {
    if (txtDeriDokAfatKohor.GetDate() < txtNgaDokAfatKohor.GetDate())
        txtDeriDokAfatKohor.SetDate(txtNgaDokAfatKohor.GetDate());
}

function ngaDokKonvertuarDateChanged(s, e) {
    if (txtDeriDokKonvertuar.GetDate() < txtNgaDokKonvertuar.GetDate())
        txtDeriDokKonvertuar.SetDate(txtNgaDokKonvertuar.GetDate());
}

function ngaDtDokUrdherPageseDateChanged(s, e) {
    if (txtDeriDokUrdherPagese.GetDate() < txtNgaDokUrdherPagese.GetDate())
        txtDeriDokUrdherPagese.SetDate(txtNgaDokUrdherPagese.GetDate());
}

function ngaDtAprovimiDateChanged(s, e) {
    if (txtDeriDokAprovimit.GetDate() < txtNgaDokAprovimit.GetDate())
        txtDeriDokAprovimit.SetDate(txtNgaDokAprovimit.GetDate());
}

function ngaDokDateKrahasuesChanged(s, e) {
    if (txtDeriDtKrahasues.GetDate() < txtNgaDtKrahasues.GetDate())
        txtDeriDtKrahasues.SetDate(txtNgaDtKrahasues.GetDate());
}

function ngaDokDateKrahasuesQKChanged(s, e) {
    if (txtDeriDtKrahasuesQK.GetDate() < txtNgaDtKrahasuesQK.GetDate())
        txtDeriDtKrahasuesQK.SetDate(txtNgaDtKrahasuesQK.GetDate());
}

function NgaDtStatusChanged(s, e) {
    if (txtDeriDtStatus.GetDate() < txtNgaDtStatus.GetDate())
        txtDeriDtStatus.SetDate(txtNgaDtStatus.GetDate());
}

function ngaDokDateChangedKryesor(s, e) {
    if (txtDeriDokKryesor.GetDate() < txtNgaDokKryesor.GetDate())
        txtDeriDokKryesor.SetDate(txtNgaDokKryesor.GetDate());
}
function ngaDtTakimiChanged(s, e) {
    if (txtDeriDttakimi.GetDate() < txtNgaDttakimi.GetDate())
        txtDeriDttakimi.SetDate(txtNgaDttakimi.GetDate());
}
function ngaDokDateAmortizimiChanged(s, e) {

    if (txtDeriDokAmortizim.GetDate() < txtNgaDokAmortizim.GetDate())
        txtDeriDokAmortizim.SetDate(txtNgaDokAmortizim.GetDate());
}

function ngaDokDateRegChanged(s, e) {

    if (txtDeriDokReg.GetDate() < txtNgaDokReg.GetDate())
        txtDeriDokReg.SetDate(txtNgaDokReg.GetDate());
}
function ngaDokDateChangedLidhes(s, e) {
    if (txtDeriDokLidhes.GetDate() < txtNgaDokLidhes.GetDate())
        txtDeriDokLidhes.SetDate(txtNgaDokLidhes.GetDate());
}
function ngaDokDateChangedLidhes1(s, e) {
    if (txtDeriDokDateLidhes.GetDate() < txtNgaDokDateLidhes.GetDate())
        txtDeriDokDateLidhes.SetDate(txtNgaDokDateLidhes.GetDate());
}


function ngaRegjDateChanged(s, e) {
    if (txtDeriRegj.GetDate() < txtNgaRegj.GetDate())
        txtDeriRegj.SetDate(txtNgaRegj.GetDate());
}

function ngaDateFillimiVodDateChanged(s, e) {
    if (txtDeriDokFillimiVod.GetDate() < txtNgaDokFillimiVod.GetDate())
        txtDeriDokFillimiVod.SetDate(txtNgaDokFillimiVod.GetDate());
}
function ngaDateLargimiVodDateChanged(s, e) {
    if (txtDeriDokLargimiVod.GetDate() < txtNgaDokLargimiVod.GetDate())
        txtDeriDokLargimiVod.SetDate(txtNgaDokLargimiVod.GetDate());
}

function ngaFillimiDateChanged(s, e) {
    if (txtDeriDtFillimi.GetDate() < txtNgaDtFillimi.GetDate())
        txtDeriDtFillimi.SetDate(txtNgaDtFillimi.GetDate());
}

function ngaMbarimiDateChanged(s, e) {
    if (txtDeriDtMbarimi.GetDate() < txtNgaDtMbarimi.GetDate())
        txtDeriDtMbarimi.SetDate(txtNgaDtMbarimi.GetDate());
}

function ngaDtKrijimiDateChanged(s, e) {
    if (txtDtDeriKrijimiAqtSerial.GetDate() < txtDtNgaKrijimiAqtSerial.GetDate())
        txtDtDeriKrijimiAqtSerial.SetDate(txtDtNgaKrijimiAqtSerial.GetDate());
}

function ngaDtPeriudheMaturimiChanged(s, e) {
    if (txtDeriPeriudheMaturimi.GetDate() < txtNgaPeriudheMaturimi.GetDate())
        txtDeriPeriudheMaturimi.SetDate(txtNgaPeriudheMaturimi.GetDate());
}

function ngaDtPeriudheFillimiChanged(s, e) {
    if (txtDeriPeriudheFillimi.GetDate() < txtNgaPeriudheFillimi.GetDate())
        txtDeriPeriudheFillimi.SetDate(txtNgaPeriudheFillimi.GetDate());
}

function ngaDtPeriudheMbarimiChanged(s, e) {
    if (txtDeriPeriudheMbarimi.GetDate() < txtNgaPeriudheMbarimi.GetDate())
        txtDeriPeriudheMbarimi.SetDate(txtNgaPeriudheMbarimi.GetDate());
}
function ngaDokDateChangedPlanifikimi(s, e) {
    if (txtDeriDtPlanifikimi.GetDate() < txtNgaDtPlanifikimi.GetDate())
        txtDeriDtPlanifikimi.SetDate(txtNgaDtPlanifikimi.GetDate());
}

function ngaDokDateChangedProdhimi(s, e) {
    if (txtDeriDtProdhimi.GetDate() < txtNgaDtProdhimi.GetDate())
        txtDeriDtProdhimi.SetDate(txtNgaDtProdhimi.GetDate());
}

function getClientID(key) {

    return 'navBarFiltrat_GCTC1_' + key;
}

function FshiClicked(key) {
    popFshi.Show();
}

//Funksioni qe perdoret per fshirjen e filtareve te personalizuar
function FshiFilter_Clicked(s, e, kodFilter) {

    if (!isNaN(kodFilter)) {
        popFshiFilter.Show();
        pastrofiltrat();
        e.processOnServer = false;
    }
}
function OnGridSelectionChangedRaportDefault() {
    grid_rapDefault.GetSelectedFieldValues('IdRaporti', OnGridSelectionCompleteRaporti);
}

function OnGridSelectionChangedRaportCustom() {
    grid_rapCustom.GetSelectedFieldValues('IdRaporti', OnGridSelectionCompleteRaporti);
}

function OnGridSelectionCompleteRaporti(values) {
    var s = new String();
    s = s + values[0];
    var vl = s.split(",");
    txtIdRap.SetText(vl[0]);
}

function dbClickGrida() {
    document.parentWindow.parent.document.getElementById('Container').src = 'Raporti.aspx';
}
function hapraportin(sender, args) {
    if (args.get_error() == undefined) {
        if (sender._postBackSettings.panelID == "pnlKryesor|menuKryesore" && document.getElementById('hdf1').value == '1') {
            document.getElementById('hdf1').value = '';
            window.open("Raporti.aspx" + "&scopeID=" + Utils.getUrlVar("scopeID"));
        }
    }
    else
        alert("There was an error" + args.get_error().message);
}


function RuajFilter_Click(s, e, combo) {

    if (combo.GetValue() != "") {
        popruajfiltrin.Show();
        txtkodifiltrit.SetText(combo.GetValue());
        txtpershkrimifiltrit.SetText('');
        localRaport.SetChecked(false);
        localPerdorues.SetChecked(false);
        localNdermarrje.SetChecked(false);

    } e.processOnServer = false;
}

$(document).ready(function () {
    RaportiEmerReal = hfState.Get("RaportiEmerReal");
    identikuesPerPopupLlojDokumenti = "Raporti";
    identikuesPerPopupLlogari = "Raporti";
    identikuesPerPopupBurimi = "Raporti";
    identikuesPerPopupAktiviteti = "Raporti";
    if (RaportiEmerReal === "ditari" || RaportiEmerReal === "RptKartelaLlogarive" || RaportiEmerReal === "KartelaLlogariveMeKunderparti" || RaportiEmerReal === "Rap_BuxhetiDheFondesh" || RaportiEmerReal === "buxhetimiSipasShpenzimeve" || RaportiEmerReal === "buxhetimeSipasUrdherPagesave")
        identikuesPerPopupKategoriShpenzimi = "RaportiMultiSelect";
    else
        identikuesPerPopupKategoriShpenzimi = "Raporti";

    $(document).keydown(function (event) {
        if ((event.which == 13 || event.keyCode == 13) && $(event.target).closest("#reportContainer").length == 0) {
            hapRaportin();
        }
    });
    $(window).on('load', function () {
        Init();
        renditFiltra();
        changeButtonsType();
    });
    $(window).bind("beforeunload", function () {
        if (!uEkzekutuaFshiRaportNgaSesioni)
            fshiFleteKontabel();
    });
});

function renditFiltra() {
    myMesazh.shtoHandler();
    idGjuha = hfgjuha.Get("idGjuha");

    if (RaportiEmerReal == "logePerKartelePunonjesi") {
        radDtRegj.SetSelectedIndex(0);
        txtNgaRegj.SetDate(new Date());
        txtDeriRegj.SetDate(new Date());
        txtNgaRegj.SetEnabled(false);
        txtDeriRegj.SetEnabled(false);

        if (hfPerdorues.Get("Roli") === "RA")
            btnePerdorues.SetEnabled(true);
        else {
            lblPerdorues.SetVisible(false);
            btnePerdorues.SetVisible(false);
            btnePerdorues.SetText(hfPerdorues.Get("Perdoruesi"));
        }

    }

    if (RaportiEmerReal == "Rap_BuxhetiHarxhuar" || RaportiEmerReal == "Rap_StatusiMarreveshjeve") {
        radDtDok.SetValue('Periudha');
        txtNgaDok.SetDate(new Date());
        txtDeriDok.SetDate(new Date());
    }

    switch (RaportiEmerReal) {
        case "regjistriPermbledhesBlerje":
        case "RapBlerjeSipasSasiveKrahasuese":
        case "regjistriAnalitikBlerje":
        case "liber_blerje":
        case "situacionFurnitori":
        case "situacionFurnitoriMeGrupime":
        case "kartelaArtikullitBlerje":
        case "fletaKontabel":
        case "blerjeMbi500Euro":
        case "liber_blerjeBizneseTeVogla":
        case "liber_shitjeKosove":
        case "artikujTeBlereDetajime":
        case "artikujTeBlereSipasSkadences":
        case "liber_blerjeProCredit":
        case "artikujTeBlere":
        case "regjisterAnalitikBlerjeFormat2":
        case "liber_blerje2015":
        case "LibriBlerjes2019":
        case "liber_blerje2015Kosove":
        case "situacionPermbledhesFurnitorNivelRaportues":
        case "kartelaFurnitorit":
        case "kartelaArtikullitFormat2Blerje":
        case "situacioniMonedheFurnitori":
            lblGrupTKF.SetText(hfgjuha.Get("labelGrupFurnitori3"));
            lblGrupDKF.SetText(hfgjuha.Get("labelGrupFurnitori2"));
            lblGrupPKF.SetText(hfgjuha.Get("labelGrupFurnitori1"));
            break;

        case "karteleFurnitori":
            lblGrupimiKF1.SetText(hfgjuha.Get("labelGrupFurnitori1"));
            lblGrupimiKF2.SetText(hfgjuha.Get("labelGrupFurnitori2"));
            lblGrupimiKF3.SetText(hfgjuha.Get("labelGrupFurnitori3"));

            break;

        case "liber_shitje":
        case "marzhiShitjeve":
        case "liber_shitjeBizneseTeVogla":
        case "liber_blerjeKosove":
        case "grafikMarzhiShitje":
        case "kerkeseOfertePorosiFaturaShitje":
        case "artikujTeShiturAutorizime":
        case "regjistriPermbledhesShitjeAutorizime":
        case "regjistriAnalitikShitjeAutorizime":
        case "kontratatIMB":
        case "shitjeKlienteSipasMakinave":
        case "situacionKlienteshSipasMakinave":
        case "karteleAnalitikeKlienti":
        case "marzhiShitjeveSipasKlienteve":
        case "MarzhiShitjeveSipasArtikujveDheKlienteve":
        case "marzhiShitjeveMagazina":
        case "evidencaShitjeve":
        case "shitjeGjendjeKlienteshAgjente":
        case "marzhiShitjeveSipasMagazinaveFormat2":
        case "shitjeSipasMuajveKrahasues":
        case "artikujTePashitur":
        case "vjetersiDetyrimeshKliente":
        case "situacionKlientiAfateMaturimi":
        case "artikujShiturKlienteVartes":
        case "regjistriAnalitikShitjeDetajime":
        case "marzhiShitjeveMagazineNjesiPerberese":
        case "marzhiShitjesNjesiPerberese":
        case "marzhiShitjevePerberesit":
        case "marzhiShitjeveTollona":
        case "marzhiShitjeveDetajimArtikujsh":
        case "ShitjetSipasSasiveKrahasuese":
            lblGrupTKF.SetText(hfgjuha.Get("labelGrupKlienti3"));
            lblGrupDKF.SetText(hfgjuha.Get("labelGrupKlenti2"));
            lblGrupPKF.SetText(hfgjuha.Get("labelGrupKlienti1"));
            break;

        case "situacionKlienti":
        case "karteleKlienti":
        case "MaturimiFaturaveTeKlienteve":
        case "MaturimiFaturaveTeFurnitoreve":
        case "artikujTeShitur":
        case "kartelaEKlientit":
        case "regjistriAnalitikShitje":
        case "shitjepermbledhese":
        case "klienteMeAfateMaturimi":
            lblGrupimiKF1.SetText(hfgjuha.Get("labelGrupKlienti1"));
            lblGrupimiKF2.SetText(hfgjuha.Get("labelGrupKlenti2"));
            lblGrupimiKF3.SetText(hfgjuha.Get("labelGrupKlienti3"));

            break;
        case "maturimiAnalitikKF":
        case "maturimiPermbledhesKF":
        case "permbledhesFaturimePagesa":
            lblGrupTKF.SetText(hfgjuha.Get("labelGrupKlientFurnitor3"));
            lblGrupDKF.SetText(hfgjuha.Get("labelGrupKlientFurnitor2"));
            lblGrupPKF.SetText(hfgjuha.Get("labelGrupKlientFurnitor1"));
            break;
    }

    if (RaportiEmerReal == "maturimStokuArtikujAfatgjate" || RaportiEmerReal == "maturimStokuArtikujAfatgjate" || RaportiEmerReal == "MaturimiStokutPerArtikujtMeSeriale") {
        txtInterval6.SetVisible(false);
        txtInterval7.SetVisible(false);
    }
    if (RaportiEmerReal == "MaturimiFaturaveTeKlienteve") {
        txtInterval5.SetVisible(false);
        txtInterval6.SetVisible(false);
        txtInterval7.SetVisible(false);
    }

    if (RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve") {
        txtInterval5.SetVisible(false);
        txtInterval6.SetVisible(false);
        txtInterval7.SetVisible(false);
    }

    if (RaportiEmerReal == "niveleCmimesh") {
        var filtraKryesor = navBarFiltrat.GetGroupByName("filtraKryesor");
        filtraKryesor.SetVisible(false);
    }
    var filtroQueryString = Utils.getUrlVar("Filtro");
    if (filtroQueryString == "true") {
        //$('.ImbReportToolBar').hide();
        var filtraAvancuar = navBarFiltrat.GetGroupByName("filtraAvancuar");
        filtraAvancuar.SetExpanded(true);
    }
    else {
        //$('.ImbReportToolBar').show();
    }
    var idDivKrye = JSON.parse($('#hfIdKrye').val());
    var topDivKrye = JSON.parse($('#hfTopKrye').val());
    var idDivAvanc = JSON.parse($('#hfIdAvanc').val());
    var topDivAvanc = JSON.parse($('#hfTopAvanc').val());
    $('#filtraKrye').children().each(function () { //fshehim gjithe filtrat kryesore
        $(this).hide();
    });
    $('#filtraAvanc').children().each(function () { //fshehim gjithe filtrat e avancuar
        $(this).hide();
    });
    for (i in idDivKrye) {
        $('#' + idDivKrye[i]).show();
        $('#' + idDivKrye[i]).css({ left: '1%' });
    }
    for (i in idDivAvanc) {
        $('#' + idDivAvanc[i]).show();
        $('#' + idDivAvanc[i]).css({ left: '1%' });
    }


    if (RaportiEmerReal == "ndjekjaECiklitTeKonvertimeve") {
        cmbLidhesaDok.SetEnabled(false);
        cmbVeprimDok2.SetEnabled(false);
        txtNrDok2.SetEnabled(false);
        cmbLidhesaKF.SetEnabled(false);
        cmbLidhesaLlojDok.SetEnabled(false);
        cmbLidhesaPikeShF.SetEnabled(false);
        cmbLidhesaDegeAdmin.SetEnabled(false);
        cmbLidhesaGrup3.SetEnabled(false);
        cmbLidhesaGrup2.SetEnabled(false);
        cmbLidhesaGrup1.SetEnabled(false);
    }
    if (RaportiEmerReal == "shitjeDitore" || RaportiEmerReal == "shitjeDetyrimeshAutorizime" || RaportiEmerReal == "shitjeArketimeDitoreAutorizime") {
        radDtDok.SetSelectedIndex(1);
        txtNgaDok.SetDate(new Date());
        txtDeriDok.SetDate(new Date());
        radDtDok.SetEnabled(false);
        txtDeriDok.SetEnabled(false);
    }
    if (RaportiEmerReal == "historikuVeprimeve") {
        radDtRegj.SetSelectedIndex(1);
        txtNgaRegj.SetDate(new Date());
        txtDeriRegj.SetDate(new Date());
    }

    if (RaportiEmerReal == "gjendjaLlogarive" || RaportiEmerReal == "gjendjaPermbledheseArkeBankeMonedheKerkuar" || RaportiEmerReal == "Rap_GjendjaBuxhetit") {
        radDtDok.SetSelectedIndex(1);
        radDtDok.SetEnabled(false);
        txtDeriDok.SetEnabled(true);
    }

    if (RaportiEmerReal == "pasqyreEKonsoliduarTeArdhuraShpenzime" || RaportiEmerReal == "pasqyreKonsoliduarGjendjeFinanciare") {
        radDtDok.SetSelectedIndex(2);
    }
    if (RaportiEmerReal == "analizeAktiveQarkullues" || RaportiEmerReal == "analizaDetyrime") {
        radDtDokKrahasues.SetSelectedIndex(2);
        radDtDok.SetSelectedIndex(2);
    }

    if (RaportiEmerReal == "artikujTeBlere")
        cbMonedheKF.SetEnabled(false);
}

function DisableCombo() {
    if (RaportiEmerReal == "shitjepermbledhese"
        || RaportiEmerReal == "maturimiAnalitikKF"
        || RaportiEmerReal == "regjistriAnalitikShitje"
        || RaportiEmerReal == "artikujTeShitur"
        || RaportiEmerReal == "karteleKlienti"
        || RaportiEmerReal == "MaturimiFaturaveTeKlienteve"
        || RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve"
        || RaportiEmerReal == "kartelaArtikullitShitje"
        || RaportiEmerReal == "artikujTeShiturIThjeshte"
        || RaportiEmerReal == "shlyrjeKlienteshSipasPeriudhes"
        || RaportiEmerReal == "shitjeDitore"
        || RaportiEmerReal == "komisioneAnalitikeSipasAgjenteve"
        || RaportiEmerReal == "karteleAnalitikeKlienti"
        || RaportiEmerReal == "shitjeArketimeDitoreAutorizime"
        || RaportiEmerReal == "shlyerjeKlienteveSipasPeriudhesUniversReklama"
        || RaportiEmerReal == "shlyerjeKlienteveSipasMonedheKlient"
        || RaportiEmerReal == "shitjeLikujdime"
        || RaportiEmerReal == "shitjeGjendjeKlienteshAgjente"
        || RaportiEmerReal == "artikujShiturKlienteVartes"
        || RaportiEmerReal == "regjistriAnalitikShitjeDetajime"
        || RaportiEmerReal == "ShitjetSipasSasiveKrahasuese") {
        cmbVeprimi1GrupPKF.SetEnabled(false);
        cmbVeprimi1GrupDKF.SetEnabled(false);
        cmbVeprimi1GrupTKF.SetEnabled(false);
    }
}

function lidhes_valueChanged(s, e, filterVeprim, filter) {
    if (s.GetText() == ' ') {
        filterVeprim.SetEnabled(false);
        filter.SetEnabled(false);
        filterVeprim.SetText('');
        filter.SetText('');
    }
    else {
        filterVeprim.SetSelectedIndex(0);
        filter.SetEnabled(true);
        filterVeprim.SetEnabled(true);
        if ((filterVeprim.name.indexOf('cmbVeprimi1GrupPKF') != -1 || filterVeprim.name.indexOf('cmbVeprimi2GrupPKF') != -1 || filterVeprim.name.indexOf('cmbVeprimi3GrupPKF') != -1) &&
            (RaportiEmerReal == "shitjepermbledhese" || RaportiEmerReal == "maturimiAnalitikKF" || RaportiEmerReal == "klienteMeAfateMaturimi" || RaportiEmerReal == "regjistriAnalitikShitje"
            || RaportiEmerReal == "artikujTeShitur" || RaportiEmerReal == "karteleKlienti" || RaportiEmerReal == "MaturimiFaturaveTeKlienteve" || RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve" || RaportiEmerReal == "kartelaArtikullitShitje" || RaportiEmerReal == "artikujShiturKlienteVartes"
                || RaportiEmerReal == "artikujTeShiturIThjeshte" || RaportiEmerReal == "shlyrjeKlienteshSipasPeriudhes" || RaportiEmerReal == "shitjeDitore"
                || RaportiEmerReal == "karteleAnalitikeKlienti" || RaportiEmerReal == "shitjeArketimeDitoreAutorizime" || RaportiEmerReal == "shlyerjeKlienteveSipasPeriudhesUniversReklama"
                || RaportiEmerReal == "shlyerjeKlienteveSipasMonedheKlient" || RaportiEmerReal == "shitjeLikujdime" || RaportiEmerReal == "shitjeGjendjeKlienteshAgjente"
                || RaportiEmerReal == "artikujShiturKlienteVartes" || RaportiEmerReal == "artikujTeShiturGrupe" || RaportiEmerReal == "regjistriAnalitikShitjeDetajime"))
            filterVeprim.SetEnabled(false);

    }
}

function lidhes_valueChanged1(s, e, filterVeprim, filter) {
    if (s.GetSelectedIndex == 0) {
        filterVeprim.SetEnabled(false);
        filter.SetEnabled(false);
        filterVeprim.SetText('');
        filter.SetText('');
    }
    else {
        filterVeprim.SetSelectedIndex(0);
        filterVeprim.SetEnabled(true);
        filter.SetEnabled(true);
    }
}

function cmbCikli_Changed(s, e) {
    if (RaportiEmerReal == "grafikTeArdhurash" || RaportiEmerReal == "grafikKrahasueshTeArdhurash" || RaportiEmerReal == "grafikMarzhiShitje") {
        if (s.GetSelectedIndex() == 0)
            radDtDok.SetSelectedIndex(0);
        if (RaportiEmerReal == "grafikKrahasueshTeArdhurash" && s.GetSelectedIndex() == 0) {
            radDtDokKrahasues.SetSelectedIndex(0);
            Utils.toggleKontrolletPeriudha(radDtDokKrahasues, txtNgaDtKrahasues, txtDeriDtKrahasues);
        }
        if (s.GetSelectedIndex() == 1)
            radDtDok.SetSelectedIndex(2);
        if (RaportiEmerReal == "grafikKrahasueshTeArdhurash" && s.GetSelectedIndex() == 1) {
            radDtDokKrahasues.SetSelectedIndex(2);
            Utils.toggleKontrolletPeriudha(radDtDokKrahasues, txtNgaDtKrahasues, txtDeriDtKrahasues);
        }

        Utils.toggleKontrolletPeriudha(radDtDok, txtNgaDok, txtDeriDok);

    }
}

function filter2_Init(sender, event, lidhes) {
    if (typeof lidhes === 'undefined' || lidhes.GetText() == ' ') {
        sender.SetEnabled(false);
        var name = sender.name;
        if (name.indexOf('cmbVepri') > 0)
            sender.SetSelectedIndex(-1);
        else
            sender.SetText("");
    }
    if (RaportiEmerReal && RaportiEmerReal.toLowerCase().indexOf('marzhiShitje') != -1)
        vendosVleraDefaultKlasaArtikullit();
}
function Pageload(s, e) {
    RaporteUtils.OldViewer = s;
    var reportWindow = s.getContentFrameElement().contentWindow;
    reportWindow.myFaqeCelje = window.myFaqeCelje;
    reportWindow.ASPxCallbackPanel1 = window.ASPxCallbackPanel1;    
}

function reportViwerEndCallBack(s, e) {
    if (hfState.Get("reportPageCount") != reportViewer2.pageCount && reportViewer2.pageCount > 0) {
        hfState.Set("reportPageCount", reportViewer2.pageCount); 
        RaporteUtils.Paginator.option('dataSource', RaporteUtils.Pages());
        RaporteUtils.Paginator._refresh();
    }
    RaporteUtils.Paginator.option('value', reportViewer2.getCurrentPageIndex());
}
$(document).mouseup(function (e) {
    var container = $("#delta");

    // if the target of the click isn't the container nor a descendant of the container
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        container.css("display","none");
    }
});
function menu_click(s, e) {
    switch (e.item.name) {
        case "Shiko":
            hapRaportin();
            break;
        case "Vizualizo ne Delta":
            window.document.getElementById("delta").style.display = "block";
            break;
    }

    if (e.item.name == "Gjenero") {

        if (!eshteHapurRaporti)
            myMesazh.ShtoMesazhGabimi("Ju lutem shtypni shiko perpara se te shtyni gjenero")
        else
            popKonvertim.Show();
        e.processOnServer = false;
        return;

    }
    if (e.item.name == 'Pastro') {
        pastrofiltrat();
        cmbfiltra.SetValue(0);
    }
    else if (e.item.name == "Anullo") {
        kthehuNeFaqenEMeparshme();
    } else if (e.item.name == "HapPopup") {
        if (eshteHapurRaporti) {
            if (hfState.Get("oldViewer") != true)
                reportViewer.ExportTo("pdf", true);
            else
                reportViewer2.SaveToWindow("pdf");
        }
        else
            myMesazh.ShtoMesazhGabimi("Ju lutem shtypni shiko perpara se te hapet ne faqe te re!");
    }
    e.processOnServer = false;
}
function hapRaportin() {
    var filtra = navBarFiltrat.GetGroupByName("filtraKryesor");
    if ((Date.now() / 1000) - hfState.Get("lastOpenDate") < 60 && hfState.Get("filter") == radDtDok.GetValue()) {
        const data = new Date(hfState.Get("lastOpenDate") * 1000);
        let date_now = data.toISOString().split("T")[0];
        date_now = date_now + " " + data.getHours().toString() + ":"+data.getMinutes().toString()+":" + data.getSeconds().toString();
        toastMessage("Raporti i perditesuar qe prej " + date_now + "!");
        return;
    }
    if (!KontrolloIntervalet())
        return;
    var filtraAvancuar = navBarFiltrat.GetGroupByName("filtraAvancuar");
    filtraAvancuar.SetExpanded(false);
    filtra.SetExpanded(false);
    $('#hfRilodo').val(true);
    eshteHapurRaporti = true;
    RaporteUtils.Parameter = "Shiko";
    ASPxCallbackPanel1.PerformCallback("Shiko");
}
function showFilters() {
    var filtra = navBarFiltrat.GetGroupByName("filtraKryesor");
    filtra.SetExpanded(true);
}
function toast() {
    const toast = document.getElementById('toast');

    toast.querySelector('.toast-body').innerHTML = "Filloj vizualizimi ne Delta, mos e hiqni kete faqe deri ne perfundim!";
    toast.style.opacity = 1;
    toast.style.zIndex = 999999;
    //toast.style.display = "block";
    setTimeout(function(){
        toast.style.opacity = 0;
        toast.style.zIndex = -10;
        //toast.style.display= "none";
    }, 2000);
}
function toastMessage(message) {
    const toast = document.getElementById('toast');

    toast.querySelector('.toast-body').innerHTML = message;
    toast.style.opacity = 1;
    toast.style.zIndex = 999999;
    //toast.style.display = "block";
    setTimeout(function () {
        toast.style.opacity = 0;
        toast.style.zIndex = -10;
        //toast.style.display= "none";
    }, 4000);
}
function KontrolloIntervalet() {
    var idKontrollet = JSON.parse($('#hfKontrolle').val());

    if (IntervalIPavlefshem(Utils.ktheKontroll('txtInterval1')) || IntervalIPavlefshem(Utils.ktheKontroll('txtInterval2')) || IntervalIPavlefshem(Utils.ktheKontroll('txtInterval3')) || IntervalIPavlefshem(Utils.ktheKontroll('txtInterval4')) || IntervalIPavlefshem(Utils.ktheKontroll('txtInterval5')) || IntervalIPavlefshem(Utils.ktheKontroll('txtInterval6')) || IntervalIPavlefshem(Utils.ktheKontroll('txtInterval7'))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("MsgVleraNeIntervaleGabim"));
        return false;
    }

    return true;
}

function IntervalIPavlefshem(txtInterval) {
    return Object.keys(txtInterval).length > 0 && txtInterval.GetVisible() && (txtInterval.GetText() == "" || isNaN(txtInterval.GetText()));
}

function kthehuNeFaqenEMeparshme() {
    var idmod = Utils.getUrlVar("idmod");
    var vjenNga = Utils.getUrlVar("vjenNga");
    var previousPage = Utils.getUrlVar("previousPage");
    if (vjenNga == "CRM")
        window.location = "RaportetAllNew.aspx?vjenNga=CRM";
    else if (vjenNga == "Aprovim")
        window.location = "ListeAprovimi.aspx?status=aprovim";
    else if (previousPage == "RaportetAllNew")
        window.location = "RaportetAllNew.aspx";
    else if (previousPage == "RaportetAll")
        window.location = "RaportetAll.aspx";
    else if (idmod != "undefined")
        window.location = "Raportet.aspx?idmod=" + idmod;
    else if (vjenNga == Utils.getUrlVar("VjenNgaSubraporti"))       
        window.location = "Raportet.aspx?idmod=" + hfState.Get("idModulSubRaporti");

    else
        window.location = window.parent.document.location;
}
function konverto() {
    var id = 0;
    if (RaportiEmerReal == "KontrolliSkadencesArtikujve")
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=dalje&shtim_modifikim=shtimraport&niveli=' + cmbKonverto.GetValue() + '&id=' + id + '&konfigurim=' + cmbKonf.GetValue() + '&filtermagdestinacion=' + txtBtnMagazineSkadence.GetText()+ '&PageId=' + window["CurrentPageId"]);

        else 
    myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtimraport&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&filtersasipakonvert=' + cbSasiaPakonvertuar.GetChecked() + '&PageId=' + window["CurrentPageId"]);

}
function pastrofiltrat() {
    var idKontrollet = JSON.parse($('#hfKontrolle').val());
    var d = new Date();
    for (i in idKontrollet) {

        switch (idKontrollet[i].IdTipiKontrollit) {
            case 1:
                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('');
                break;
            case 2:
                if (idKontrollet[i].KodKontrolli.search('cmbLidhesaKlasaArtikullit') != -1 && (RaportiEmerReal == "porosiPromotions"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbLlojArtikulli') != -1 && ((RaportiEmerReal == "shitjeSipasMuajveGrafik") || (RaportiEmerReal == "analizeCmimeShitje") || (RaportiEmerReal == "analizaPorosive")))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);

                else if (idKontrollet[i].KodKontrolli.search('cmbVeprimAdrKlient1') != -1 && ((RaportiEmerReal == "analitikShitje") || (RaportiEmerReal == "maturimiAnalitikKF") || (RaportiEmerReal == "klienteMeAfateMaturimi")))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(6);
                else if (idKontrollet[i].KodKontrolli.search('cmbVeprimKlasaArtikullit1') != -1 && (RaportiEmerReal == "grafikMarzhiShitje"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(3);
                else if (idKontrollet[i].KodKontrolli.search('cmbGjendjeArt') != -1 && (RaportiEmerReal == "levizjetelikujditeteve"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(1);
                else if (idKontrollet[i].KodKontrolli.search('cmbKlasaArtikullit1') != -1 && (RaportiEmerReal == "grafikMarzhiShitje"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(2);
                else if (idKontrollet[i].KodKontrolli == "cmbStatusRezervimi")
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbStatus') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(1);
                else if (idKontrollet[i].KodKontrolli.search('cmbTipKontrate') != -1 && (RaportiEmerReal == "regjistriVjetorPunonjes"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);

                else if (idKontrollet[i].KodKontrolli.search('cmbMenyrePagese') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbCmimMePaTVSH') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbkrahasimKosto') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbGrupoKlientSipas') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbGrupoArtikullSipas') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbKlientAktiv') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbMenu1') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbLlojVeprimi1') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                else if (idKontrollet[i].KodKontrolli.search('1') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(0);
                else if (idKontrollet[i].KodKontrolli.search('cmbLikuiduar') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(2);
                else if (idKontrollet[i].KodKontrolli.search('cmbPaguar') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(3);

                else if (idKontrollet[i].KodKontrolli == 'cmbViti' && ((RaportiEmerReal == "deklarimArdhuraVjetore") || (RaportiEmerReal == "memoAnnualBonus")))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(hfState.Get("_idViti"));
                else if (idKontrollet[i].KodKontrolli == 'cmbFormatNumri' && RaportiEmerReal != "bilancikontabelformat2" && RaportiEmerReal != "liber_shitje2015" && RaportiEmerReal != "LibriBlerjes2019" && RaportiEmerReal != "LibriShitjeveVodafone" && RaportiEmerReal != "LibriShitjes2019")
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(2);
                else if (idKontrollet[i].KodKontrolli.search('cmbShfaqVlerat') != -1 && ((RaportiEmerReal == "grafikTeArdhurash") || (RaportiEmerReal == "shitjeSipasMuajveGrafik") || (RaportiEmerReal == "grafikMarzhiShitje") || (RaportiEmerReal == "grafikKrahasueshTeArdhurash")))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(1);
                else if (idKontrollet[i].KodKontrolli.search('cmbCikli') != -1 && ((RaportiEmerReal == "grafikTeArdhurash") || (RaportiEmerReal == "grafikMarzhiShitje") || (RaportiEmerReal == "grafikKrahasueshTeArdhurash")))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(1);
                else if (idKontrollet[i].KodKontrolli.search('cmbLlojArtikulli') != -1 && ((RaportiEmerReal == "grafikTeArdhurash") || (RaportiEmerReal == "grafikMarzhiShitje") || (RaportiEmerReal == "grafikKrahasueshTeArdhurash")))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(1);

                else if (idKontrollet[i].KodKontrolli.search('cmbVeprimKlasaArtikullit2') != -1 && (RaportiEmerReal == "grafikMarzhiShitje")) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(3);
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(3).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli.search('cmbKlasaArtikullit2') != -1 && (RaportiEmerReal == "grafikMarzhiShitje"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(3);
                else if (idKontrollet[i].KodKontrolli.search('cmbVeprimePeriudhe') != -1 && (RaportiEmerReal == "shitjeSipasMuajveKrahasues"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(2);
                else if (idKontrollet[i].KodKontrolli.search('cmbLidhesaKlasaArtikullit') != -1 && (RaportiEmerReal != "shitjeSipasMuajveGrafik"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue("and");
                else if (idKontrollet[i].KodKontrolli.search('cmbViti') != -1 && (RaportiEmerReal == "RapBuxhetiIKontraktuarPerVitin"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(parseInt(hfState.Get("cmbVitiSelectedItem").toString()));

                else if (idKontrollet[i].KodKontrolli.search('cmbPasqyra') != -1 && (RaportiDesign == "Bilanci per Buxhetore"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText("BB2018");
                else if (idKontrollet[i].KodKontrolli.search('cmbPasqyra') != -1 && (RaportiDesign == "Pash buxhetor"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText("PB2018");
                else if (idKontrollet[i].KodKontrolli.search('cmbPasqyra') != -1 && (RaportiDesign == "Cash Flow buxhetor"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText("Cash_Flow_PB2018");

                else if (idKontrollet[i].KodKontrolli.search('cmbPasqyra') != -1 && (RaportiEmerReal == "burimeShpenzimeInvestime"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText("BSHI2018");
                else if (idKontrollet[i].KodKontrolli.search('cmbPasqyra') != -1 && (RaportiEmerReal == "gjendjeNdryshimeAktiveTeQendrueshem"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText("GJAQ2018");
                else if (idKontrollet[i].KodKontrolli.search('cmbKontabilizuar') != -1 && (RaportiEmerReal == "karteleKlienti" || RaportiEmerReal == "situacionKlienti"))
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(1);
                else
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                break;
            case 3:
                if (idKontrollet[i].KodKontrolli == 'txtNgaRegj' && RaportiEmerReal == "logePerKartelePunonjesi") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), d.getDate()));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriRegj' && RaportiEmerReal == "logePerKartelePunonjesi") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), d.getDate()));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokAfatKohor' && ((RaportiEmerReal == "porosiPromotions") || (RaportiEmerReal == "gjendjeKerkeseArtikujshPerProdhim"))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth() + 1, 0));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if ((idKontrollet[i].KodKontrolli == 'txtNgaDokAfatKohor') && ((RaportiEmerReal == "porosiPromotions") || (RaportiEmerReal == "gjendjeKerkeseArtikujshPerProdhim"))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), 1));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaPeriudheMaturimi' && (RaportiEmerReal == "maturimiPorosive")) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), d.getDate()));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriPeriudheMaturimi' && (RaportiEmerReal == "maturimiPorosive")) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), d.getDate()));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaPeriudheMaturimi' && ((RaportiEmerReal == "maturimiAnalitikKF") || (RaportiEmerReal == "klienteMeAfateMaturimi") || (RaportiEmerReal == "maturimiFurnitoreveShaga") || (RaportiEmerReal == "situacionKlienteshMaturime"))) {

                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriPeriudheMaturimi' && ((RaportiEmerReal == "maturimiAnalitikKF") || (RaportiEmerReal == "klienteMeAfateMaturimi") || (RaportiEmerReal == "maturimiFurnitoreveShaga") || (RaportiEmerReal == "situacionKlienteshMaturime"))) {

                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaRegj' && (RaportiEmerReal != "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriRegj' && (RaportiEmerReal != "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if ((idKontrollet[i].KodKontrolli == 'txtDtNgaKrijimiAqtSerial' || idKontrollet[i].KodKontrolli == 'txtDtDeriKrijimiAqtSerial') && RaportiEmerReal == "printimSerialesh") {

                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaDtFillimi' && RaportiEmerReal == "regjistriVjetorPunonjes") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDtFillimi' && RaportiEmerReal == "regjistriVjetorPunonjes") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaDtFillimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                }
                
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDtFillimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');

                }
                else if ((idKontrollet[i].KodKontrolli == 'txtNgaDtPorosie') && ((RaportiEmerReal == "gjendjeKerkeseArtikujshPerProdhim") || (RaportiEmerReal == "gjendjeKerkeseTotaleRecepturashProdhim"))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');


                } else if ((idKontrollet[i].KodKontrolli == 'txtDeriDtPorosie') && ((RaportiEmerReal == "gjendjeKerkeseArtikujshPerProdhim") || (RaportiEmerReal == "gjendjeKerkeseTotaleRecepturashProdhim"))) {

                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);

                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');

                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaDokAmortizim') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokAmortizim') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaDokReg') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokReg') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('31/12/9999');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaDokAfatKohor') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokAfatKohor') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaPeriudheFillimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtDeriPeriudheFillimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaPeriudheMbarimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriPeriudheMbarimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaDtMbarimi' && RaportiEmerReal == "regjistriVjetorPunonjes") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if ((idKontrollet[i].KodKontrolli == 'txtDeriDtMbarimi' || idKontrollet[i].KodKontrolli == 'txtDeriDtFillimi') && RaportiEmerReal == "regjistriVjetorPunonjes") {

                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaRegj' && (RaportiEmerReal == "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")) {
                    eval(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), 1));
                    eval(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtDeriRegj' && (RaportiEmerReal == "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")) {
                    eval(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth() + 1, 0));
                    eval(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtDeriRegj' && RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaPeriudheMaturimi' && (RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve")) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtDeriPeriudheMaturimi' && RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaOreKrijimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('00:00:00');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriOreKrijimi') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('23:59:59');
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaDok' && RaportiEmerReal == "Kartolina_ditelindjes_klientit") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), d.getDate()));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDok' && RaportiEmerReal == "Kartolina_ditelindjes_klientit") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth() + 1, 0));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaDok' && RaportiEmerReal == "ShitjetSipasDegeveAdministrative") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(1900, 1, 1));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDok' && RaportiEmerReal == "ShitjetSipasDegeveAdministrative") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date());
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaDok') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), 1));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDok') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth() + 1, 0));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if (idKontrollet[i].KodKontrolli == 'dataSkadence') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date());
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaDokDateLidhes') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), 1));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokDateLidhes') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth() + 1, 0));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaSkadence') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth(), d.getDate()));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriSkadence') {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(new Date(d.getFullYear(), d.getMonth() + 3, d.getDate()));
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                }
                else {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetDate(d);
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
                }
                break;
            case 4:
                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('');
                break;
            case 8:
                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetChecked(false);
                break;
            case 10:
                if ((idKontrollet[i].KodKontrolli == 'radDtDokShitje') && ((RaportiEmerReal == 'analizeStokuKrahasimShitje') || (RaportiEmerReal == 'analizaStokutEkspozitorKrahasimShitje'))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('VitiUshtrimor');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtDokAfatKohor') && ((RaportiEmerReal == 'porosiPromotions') || (RaportiEmerReal == 'gjendjeKerkeseArtikujshPerProdhim'))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Aktuale');
                }
                else if (((idKontrollet[i].KodKontrolli == 'radDtKrijimi') || (idKontrollet[i].KodKontrolli == 'radDtRegj')) && ((RaportiEmerReal == 'analitikShitje') || (RaportiEmerReal == 'logePerKartelePunonjesi'))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Aktuale');

                } else if (((idKontrollet[i].KodKontrolli == 'radDtMbarimi') || (idKontrollet[i].KodKontrolli == 'radDtDokReg') || (idKontrollet[i].KodKontrolli == 'radDtKrijimi')) && ((RaportiEmerReal == 'regjistriVjetorPunonjes') || (RaportiEmerReal == 'Depreciation'))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtTakimi') && (RaportiEmerReal == 'analitikVeprimtari')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Aktuale');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtDok') && (RaportiEmerReal == 'MaturimiFaturaveTeFurnitoreve')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('VitiUshtrimor');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtDok') && (RaportiEmerReal == 'Kartolina_ditelindjes_klientit')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtDok') && (RaportiEmerReal == 'EksportimiFaturaShitje')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtDok') && (RaportiEmerReal == 'arketimeDitore')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Aktuale');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtRegj') || (idKontrollet[i].KodKontrolli == 'radDtKrijimi') || (idKontrollet[i].KodKontrolli == 'radDtDokAmortizimi') || (idKontrollet[i].KodKontrolli == 'radDtDokAfatKohor') || (idKontrollet[i].KodKontrolli == 'radPeriudheFillimi') || (idKontrollet[i].KodKontrolli == 'radPeriudheMbarimi') || (idKontrollet[i].KodKontrolli == 'radDtDtFillimi') || (idKontrollet[i].KodKontrolli == 'radPeriudheMaturimi') && RaportiEmerReal != "HistorikuTeDhenaveTePerfaqesuesveTeShitjes" && RaportiEmerReal != "porosiPromotions" && RaportiEmerReal != "logePerKartelePunonjesi") {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtKrijimiAqtSerial') && (RaportiEmerReal == 'printimSerialesh')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                }

                else if (((idKontrollet[i].KodKontrolli == 'radDtDok') || (idKontrollet[i].KodKontrolli == 'radDtMbarimi')) && ((RaportiEmerReal == 'VeprimetEKlientitPerDatatMeTeFundit') || (RaportiEmerReal == 'klienteMeKontrate'))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('GjitheVitet');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtPorosie') && ((RaportiEmerReal == 'gjendjeKerkeseArtikujshPerProdhim') || (RaportiEmerReal == 'gjendjeKerkeseTotaleRecepturashProdhim'))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                }
                else if (((idKontrollet[i].KodKontrolli == 'radDtDok') || (idKontrollet[i].KodKontrolli == 'radDtDokKrahasues')) && ((RaportiEmerReal == 'grafikTeArdhurash') || (RaportiEmerReal == 'grafikKrahasueshTeArdhurash') || (RaportiEmerReal == 'grafikMarzhiShitje') || (RaportiEmerReal == 'analizeCmimeShitje') || (RaportiEmerReal == 'analizaPorosive') || (RaportiEmerReal == 'shitjeSipasMuajveGrafik') || (RaportiEmerReal == 'permbledhesKliente') || (RaportiEmerReal == 'regjistriVjetorPunonjes') || (RaportiEmerReal == 'kontrataPensionit') || (RaportiEmerReal == 'shitjeKlienteveIntervale') || (RaportiEmerReal == 'Rap_BuxhetiDheFondesh') || (RaportiEmerReal == 'klienteMeAfateMaturimi') || (RaportiEmerReal == 'KartelaPunonjesveMePagesa'))) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('VitiUshtrimor');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDateLidhes') && (RaportiEmerReal == 'permbledhesFaturimePagesa'))
                {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('GjitheVitet');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtSkadence') && (RaportiEmerReal == 'KontrolliSkadencesArtikujve')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                }
                else if ((idKontrollet[i].KodKontrolli == 'radDtDok') && (RaportiEmerReal == 'KontrolliSkadencesArtikujve')) {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('GjitheVitet');
                }
                    
                else {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Aktuale');
                }

                break;
        }

        if (idKontrollet[i].KodKontrolli.search('Interval') != -1) {
            var int2, int3, int4, int5 = 120, int6 = 150, int7 = 140000000;
            int2 = (RaportiEmerReal == "vjetersiDetyrimeshKliente" || RaportiEmerReal == "maturimStokuArtikujAfatgjate" || RaportiEmerReal == "MaturimiStokutPerArtikujtMeSeriale") ? "90" : "30";
            int3 = (RaportiEmerReal == "vjetersiDetyrimeshKliente" || RaportiEmerReal == "maturimStokuArtikujAfatgjate" || RaportiEmerReal == "MaturimiStokutPerArtikujtMeSeriale") ? "180" : "60";
            int4 = (RaportiEmerReal == "vjetersiDetyrimeshKliente" || RaportiEmerReal == "maturimStokuArtikujAfatgjate" || RaportiEmerReal == "MaturimiStokutPerArtikujtMeSeriale") ? "270" : "90";
            if (RaportiEmerReal == "vjetersiDetyrimeshKliente")
                int5 = "360";
            if (RaportiEmerReal == "shitjeKlienteveIntervale") {
                int2 = 70000;
                int3 = 210000;
                int4 = 420000;
                int5 = 1400000;
                int6 = 42000000;
            }
            if (RaportiEmerReal == "MaturimiFaturaveTeKlienteve") {
                int2 = "15";
                int3 = "31";
                int4 = "61";
            }
            if (RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve") {
                int2 = "15";
                int3 = "31";
                int4 = "61";
            }
            if (RaportiEmerReal == "PagesatPerMPesaSipasIntervaleve") {
                int2 = "1000";
                int3 = "2000";
                int4 = "3000";
                int5 = "5000";
                int6 = "10000";
                int7 = "15000";
            }

            txtInterval1.SetText("0");
            txtInterval2.SetText(int2);
            txtInterval3.SetText(int3);
            txtInterval4.SetText(int4);
            txtInterval5.SetText(int5);
            txtInterval6.SetText(int6);
            txtInterval7.SetText(int7);
        }
        else if (idKontrollet[i].KodKontrolli.search('2') != -1 & idKontrollet[i].KodKontrolli != "btneGrupimiKF2")
            Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);
    }
    vendosVlereDefaultFiltriNeBazeRaporti();
}
function vendosVlereDefaultFiltriNeBazeRaporti() {
    if (RaportiEmerReal == "Rap_BuxhetiHarxhuar" || RaportiEmerReal == "Rap_StatusiMarreveshjeve") {
        radDtDok.SetValue('Periudha');
        txtNgaDok.SetDate(new Date());
        txtDeriDok.SetDate(new Date());
        txtNgaDok.SetEnabled(true);
        txtDeriDok.SetEnabled(true);
    }
    else if (RaportiEmerReal == "Depreciation") {
        radDtDokReg.SetValue('Periudha');
        txtNgaDokReg.SetEnabled(true);
        txtDeriDokReg.SetEnabled(true);
        txtNgaDokReg.SetText('01/01/1900');
        txtDeriDokReg.SetText('30/12/9999');
    }
    else if (RaportiEmerReal == "analitikVeprimtari") {
        txtNgaDttakimi.SetDate(new Date());
        txtDeriDttakimi.SetDate(new Date());
        txtNgaDttakimi.SetEnabled(false);
        txtDeriDttakimi.SetEnabled(false);
        radDtTakimi.SetValue('Aktuale');
    }
    else if (RaportiEmerReal == "ShitjetSipasDegeveAdministrative") {
        radDtDok.SetValue('Periudha');
    }
    else if (RaportiEmerReal == "regjistriVjetorPunonjes") {
        radDtMbarimi.SetValue('Periudha');
        txtNgaDtMbarimi.SetEnabled(true);
        txtDeriDtMbarimi.SetEnabled(true);

    } else if (RaportiEmerReal == "historikuVeprimeve") {
        txtNgaRegj.SetDate(new Date());
        txtDeriRegj.SetDate(new Date());

    }
    else if (RaportiEmerReal == "bonuseVjetoreTeKlienteve") {
        radDtDok.SetValue('VitiUshtrimor');
    }
    else if (RaportiEmerReal == "kerkesaOfertaPorosiFaturaAutorizim") {
        txtNgaRegj.SetText('01/01/1900');
        txtDeriRegj.SetText('30/12/9999');
        txtNgaRegj.SetEnabled(true);
        txtDeriRegj.SetEnabled(true);
        txtNgaDok.SetEnabled(false);
        txtDeriDok.SetEnabled(false);
        radDtDok.SetValue('Aktuale');
    }

    else if (RaportiEmerReal == "amendimKontratePune") {
        radDtDtFillimi.SetValue('Aktuale');
        txtNgaDtFillimi.SetEnabled(false);
        txtDeriDtFillimi.SetEnabled(false);
    } else if (RaportiEmerReal == "shitjeDitore" || RaportiEmerReal == "shitjeDetyrimeshAutorizime" || RaportiEmerReal == "shitjeArketimeDitoreAutorizime") {
        radDtDok.SetValue('Periudha');
        txtNgaDok.SetDate(txtDeriDok.GetDate());
        txtDeriDok.SetDate(txtNgaDok.GetDate());
        txtNgaDok.SetEnabled(true);
        txtDeriDok.SetEnabled(false);
    } else if (RaportiEmerReal == "gjendjaLlogarive" || RaportiEmerReal == "gjendjaPermbledheseArkeBankeMonedheKerkuar" || RaportiEmerReal == "Rap_GjendjaBuxhetit") {

        txtNgaDok.SetEnabled(false);
        txtDeriDok.SetEnabled(true);
        radDtDok.SetValue('Periudha');
        if (RaportiEmerReal == "gjendjaPermbledheseArkeBankeMonedheKerkuar") {
            cmbMonedha.SetSelectedIndex(hfState.Get("cmbMonedhaSelectedIndex"));
            cmbMonedhaKerkuar.SetSelectedIndex(hfState.Get("cmbMonedhaSelectedIndex"));
        }

    } else if (RaportiEmerReal == "permbledhesSipasKlientevePajisjeve" || RaportiEmerReal == "analitikSipasKartave") {
        radDtDok.SetValue('GjitheVitet');
    } else if (RaportiEmerReal == "permbledhesUrdherShitje") {
        radDtKrijimi.SetValue('Aktuale');
        txtNgaKrijimi.SetEnabled(false);
        txtDeriKrijimi.SetEnabled(false);

    } else if (RaportiEmerReal == "porosiPromotions") {
        txtNgaDokAfatKohor.SetDate(new Date(d.getFullYear(), d.getMonth(), 1));
        txtNgaDokAfatKohor.SetEnabled(false);
        txtDeriDokAfatKohor.SetEnabled(false);
    }
    else if (RaportiEmerReal == "pasqyraSigurimitSuplementar" || RaportiEmerReal == "listepagesaStandarte" || RaportiEmerReal == "borderojaPagave" || RaportiEmerReal == "artikujShiturDegeAdm" || RaportiEmerReal == "artikujTeShiturDegeAdministrative" || RaportiEmerReal == "artikujTeShitur" || RaportiEmerReal == "analitikShitje" || RaportiEmerReal == "artikujShiturKlienteVartes" || RaportiEmerReal == "blerjeAnalitike" || RaportiEmerReal == "artikujTeShiturGrupe" || RaportiEmerReal == "kartelaFurnitorit" || RaportiEmerReal == "kartelaKartaMePike") {
        txtNgaRegj.SetText('01/01/1900');
        txtDeriRegj.SetText('30/12/9999');
        radDtDok.SetValue('Aktuale');

        txtNgaRegj.SetEnabled(true);
        txtDeriRegj.SetEnabled(true);
        if (RaportiEmerReal == "analitikShitje" || RaportiEmerReal == "kartelaFurnitorit") {
            radDtRegj.SetValue('Periudha');
        } else if (RaportiEmerReal == "blerjeAnalitike") {
            cmbNiveli.SetSelectedIndex(3);
        }
    } else if (RaportiEmerReal == "gjendjaPermbledhurEArkes" || RaportiEmerReal == "gjendjaPermbledhurEBankes" || RaportiEmerReal == "gjendjaMagazine" || RaportiEmerReal == "gjendjaMagazineSipasAutorizimeve" || RaportiEmerReal == "gjendjeArtikujsh") {
        cmbGjendjeArt.SetSelectedIndex(1);
    } else if (RaportiEmerReal && RaportiEmerReal.toLowerCase().indexOf('marzhiShitje') != -1)
        vendosVleraDefaultKlasaArtikullit();

    else if (RaportiEmerReal == "dokumentaKonvertuar") {
        vendosVleraDefaultStatusKonvertuar();
    }
    else if (RaportiEmerReal == "artikujShiturDegeAdm" || RaportiEmerReal == "buxhetimiSipasShpenzimeve" || RaportiEmerReal == "buxhetimeSipasUrdherPagesave") {
        cbDetajuar.SetChecked(true);
    }
    else if ((RaportiEmerReal == "situacioniMonedheKlienti") || (RaportiEmerReal == "situacioniMonedheFurnitori")) {
        cbMonedheKF.SetChecked(true);
    }

    else if (RaportiEmerReal == "gjendjeArtikujMagazineVartese") {
        cbmagvartese.SetChecked(true);
        cmbGjendjeArt.SetSelectedIndex(1);
    } else if (RaportiEmerReal == "grafikuShitjeveSipasOreve" || RaportiEmerReal == "grafikuVleresSeShiturSipasOreve") {
        cmbCikli.SetValue(1);
        cmbShfaqVlerat.SetValue(1);
        cmbTipGrafiku.SetValue('Bar');
    } else if (RaportiEmerReal == "gjendjaPermbledhurArtikujNjesiMatese" || RaportiEmerReal == "artikuTeShiturGrupeKryesore") {
        cmbFormatNumri.SetSelectedIndex(2);
    } else if (RaportiEmerReal == "bilancikontabelformat2") {
        cmbFormatNumri.SetSelectedIndex(0);
    } else if (RaportiEmerReal == "blerjeAnalitike") {
        cmbNiveli.SetSelectedIndex(3);
    } else if (RaportiEmerReal == "SituacioniIKlienteveSipasMuajve") {
        cmbViti.SetSelectedIndex(cmbViti.GetItemCount() - 1);
    } else if (RaportiEmerReal == "veprimtariaDitore") {
        txtNgaDok.SetDate(new Date());
        txtDeriDok.SetDate(new Date());
        txtNgaDok.SetEnabled(true);
        txtDeriDok.SetEnabled(true);
        radDtDok.SetValue('Periudha');
        cmbMonedhaQK.SetSelectedIndex(1);
    }
    else if (RaportiEmerReal == "artikujTeBlere") {
        cbMonedheKF.SetEnabled(false);
    }
}

function vendosVleraDefaultStatusKonvertuar() {
    cmbStatusKonvertimiDyte1.SetValue(3);
    cmbStatusKonvertimiDyte2.SetValue(2);
    cmbStatusKonvertimiDyte2.SetEnabled(true);
    cmbVeprimiStatusKonvertimiDyte2.SetEnabled(true);
    cmbVeprimiStatusKonvertimiDyte2.SetSelectedIndex(0);
    cmbLidhesaStatusKonvertimDyte.SetSelectedIndex(2);
}

function vendosVleraDefaultKlasaArtikullit() {
    cmbVeprimKlasaArtikullit1.SetValue(3);
    cmbKlasaArtikullit1.SetValue(2);
    cmbLidhesaKlasaArtikullit.SetValue("and");
    cmbVeprimKlasaArtikullit2.SetValue(3);
    cmbVeprimKlasaArtikullit2.SetEnabled(true);
    cmbKlasaArtikullit2.SetValue(3);
    cmbLlojArtikulli.SetSelectedIndex(1);
    cmbKlasaArtikullit2.SetEnabled(true);
}

function fshiFleteKontabel() {
    uEkzekutuaFshiRaportNgaSesioni = true;
    window.parent.$.ajax({
        pritPergjigje: false,
        url: Utils.getServerApiUrl("ListPagesa", "fshiRaportNgaSessioni"),
        data: JSON.stringify({})
    }).done(function () { });
}

function onInitDetajuar() {
    if (RaportiEmerReal == "buxhetimeSipasUrdherPagesave" || RaportiEmerReal == "buxhetimiSipasShpenzimeve" || RaportiEmerReal == "artikujShiturDegeAdm" || RaportiEmerReal == "RaportiPermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime" )
        cbDetajuar.SetChecked(true);
}
function onInitNenprodukte() {

    cbNenprodukte.SetChecked(false);
}
function onInitMonedheKF() {
    if ((RaportiEmerReal == "situacioniMonedheKlienti") || (RaportiEmerReal == "situacioniMonedheFurnitori"))
        cbMonedheKF.SetChecked(true);
}

function onInitGrupimSipasKF() {
    if (RaportiEmerReal == "analitikShitje")
        cbGrupimSipasKF.SetChecked(false);
}
///hap lupen e tip kontrate
function ButtonClickedTipKontrate() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniTipinEKontrates"), 'LupaTipKontrate.aspx', widthlupa, heightlupa);
}

function onSelectionChangedTakimi(theRadio) {

}

function ButtonClickedModeliFushaShtese(editor) {
    var headerText = hfState.Get("roundPanelZgjidhModelin");
    var contentUrl = RaportiEmerReal != "historikRecepturash" ? 'LupaModelFushaShtese.aspx?vjenNgaRaporti=true' :
        'LupaModelFushaShtese.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value;
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupArtikulli = "raportHistorikFushaShtese";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedObjekteGis(editor) {
    var headerText = hfState.Get("roundPanelZgjidhObjektinGis");
    var contentUrl = RaportiEmerReal != "historikRecepturash" ? 'GISLupaObjekte.aspx?vjenNgaRaporti=true' :
        'GISLupaObjekte.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value;
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupArtikulli = "raportHistorikFushaShtese";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function textChangedBtneKodKf(kontrolli) {
    switch (RaportiEmerReal) {
        case "artikujTeBlere":
            if (kontrolli.GetValue() && kontrolli.GetValue() != null && kontrolli.GetValue() != "")
                cbMonedheKF.SetEnabled(true);
            else
                cbMonedheKF.SetEnabled(false);
            break;
        default:
            break;
    }
}

function InitReportViewer(s, e) {
    if (hfState.Get("oldViewer") != true) {
        DevExpress.Report.Preview.AsyncExportApproach = true;
        s.SetHeight(window.innerHeight);
        $("#ASPxCallbackPanel1_reportViewer2_Div").attr('style', 'display: none !important');
    }
    else {
        $("#ASPxCallbackPanel1_reportViewer").attr('style', 'height: unset !important');
        $(".dx-designer").attr('style', 'border-right: none !important');
        $(".dxrd-right-tabs").remove();
        $(".dxrd-right-panel").remove();
        $(".dxrd-right-panel-collapse").remove();
    }
    RaporteUtils.InitReportViewer(s, e);
}

function CustomizeMenuActions(s, e) {
    RaporteUtils.CustomizeMenuActions(s, e);
}

function ASPxCallbackPanel1_BeginCallback(s, e) {
    RaporteUtils.DeactivateViewer();
    RaporteUtils.PageNumber = RaporteUtils.Viewer.GetPreviewModel().reportPreview.pageIndex();
    reportViewer2.setCurrentPageIndex(0);
}

window.onbeforeunload = function (e) {
    if ($(".dxrd-preview-progress").is(":visible"))
        return false;
    if (!exportOption.value) {
        RaporteUtils.PageNumber = 0;
        reportViewer.Close();
    }
};