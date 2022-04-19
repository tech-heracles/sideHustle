; var constanteParashtese = 'ASPxRoundPanel1_ASPxCallbackPanel1_gvFleteKontabelTrupi_cell';
var constantePrapashtese = '_4_';
var editorGlobal;
var editorPershkLlojDok;
var identikuesPerPopupLlogari;
var arkabanka = 0;
var idGjuha;
var eshtememe;
var cpi = 0;
function Init() {
    changeName();
    identikuesPerPopupLlojDokumenti = "Raporti";
    identikuesPerPopupLlogari = "Raporti";
    identikuesPerPopupBurimi = "Raporti";
    identikuesPerPopupAktiviteti = "Raporti";
    identikuesPerPopupKategoriShpenzimi = "Raporti";
}
function checkText(s, e) {
    myMenu.checkText(s, e);
}
function ktheFiltra(value) {
    if (value == 0)
        pastrofiltrat();
    else if (cmbfiltra.FindItemByText(cmbfiltra.GetInputElement().value) != null)
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheVleraFiltri"),
        data: JSON.stringify({ id: value })
    }).done(SuccedktheFiltra);
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function reportViwerEndCallBack(s, e) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
        data: JSON.stringify({})
    }).done(myMesazh.ShtoMesazhSesioni);
}
function SuccedktheFiltra(result) {
    // eshtememe = hfgjuha.Get("eshtememe");
    if (result != "" || result != "undefined") {
        pastrofiltrat();
        var rezultati, filertrupikontrolle, filtertrupivlera, i, val;
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
                    var n = radDtDok.GetItemCount();
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
                continue
            }
            if (val.toString() == "radDtDokKrahasues") {
                try {
                    var n = radDtDokKrahasues.GetItemCount();
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
                    var n = radDtDokKryesor.GetItemCount();
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
                    var n = radDtDokAmortizimi.GetItemCount();
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
                    var n = radDtDokKonvertuar.GetItemCount();
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
                    var n = radDtDokLidhes.GetItemCount();
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


            if (val.toString() == "radDtDokUrdherPagese") {
                try {
                    var n = radDtDokUrdherPagese.GetItemCount();
                    for (j = 0; j < n; j++) {
                        if (radDtDokUrdherPagese.GetItem(j).value == filtertrupivlera[i]) {
                            radDtDokUrdherPagese.SetSelectedIndex(j);
                            toggleKontrolletPeriudha(radDtDokUrdherPagese, txtNgaDokUrdherPagese, txtDeriDokUrdherPagese);
                            break;
                        }
                    }
                }
                catch (ex) {
                    k
                }
                continue;
            }

            if (val.toString() == "radDtDokAprovimit") {
                try {
                    var n = radDtDokAprovimit.GetItemCount();
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
                    var n = radDtRegj.GetItemCount();
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
                    var n = radDtDtFillimi.GetItemCount();
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
                    var n = radDtRegj.GetItemCount();
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
                    var n = radDtDokAfatKohor.GetItemCount();
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
                    var n = radPeriudheMaturimi.GetItemCount();
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
                    var n = radPeriudheFillimi.GetItemCount();
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
                    var n = radPeriudheMbarimi.GetItemCount();
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

                //  if( (val.toString() == "btneKompania")&&(!eshtememe))  { filtertrupivlera[i] = ""; }
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

function ButtonClickedLlojDok(editor) {//TODO PATI - Perdoret edhe ketu id e raportit
    var headerText = hfState.Get("msgRaportiZgjidhLlojinEDokumentit");
    var contentUrl;
    var idRaporti = Utils.getUrlVar('idraporti');
    switch (idRaporti) {
        case 20:
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31885&vjenNgaRaporti=true';
            break;
        case 28:
        case 27:
        case 39:
        case 59:
        case 60:
        case 233:
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31886&vjenNgaRaporti=true';
            break;
        case 40:
        case 22:
        case 140:
        case 121:
        case 29:
        case 141:
        case 143:
        case 190:
        case 33:
        case 139:
        case 248:
            contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=31887&vjenNgaRaporti=true';
            break;
        case 30:
        case 31:
        case 32:
        case 130:
        case 131:
        case 150:
        case 158:
        case 219:
            contentUrl = 'LupaKonfigurime.aspx?veprimi=6&vjenNgaRaporti=true';
            break;
        case 119:
            contentUrl = 'LupaKonfigurime.aspx?veprimi=75&vjenNgaRaporti=true';
            break;
        case 124:
            contentUrl = 'LupaKonfigurime.aspx?veprimi=78&vjenNgaRaporti=true';
            break;
        case 171:
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

function ButtonClickedLlogaria(editor) {
    var headerText = hfState.Get("popupAdministrimiUniversal");
    var contentUrl = 'LupaLlogaria.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupLlogari = "raportllogariafillim";
    if (Utils.getUrlVar('idraporti') != 99)//raportProdhimi
        myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
    else
        contentUrl = 'LupaLlogaria.aspx?idKonfigAmbjente=6821&vjenNgaRaporti=true'
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

var editorGlobalValue = 0;
///hap lupen e departamentit nendepartamentit
function ButtonClickedDepartamenti(vjenNga, s) {
    var idRaporti = Utils.getUrlVar('idraporti');
    if (vjenNga !== 'Nendepartamenti') {
        var idprindi = 0;
        var title = hfState.Get("msgZgjidhniDepartamentin");
    }
    else {
        idprindi = editorGlobalValue;
        title = hfState.Get("msgZgjidhniNenDepartamentin");
    }
    editorGlobal = s;
    myButtonClickLupa.LupaUniversal_Click(title, 'LupaStrukturaAdministrative.aspx?vjenNga=' + vjenNga + '&IdPrindi=' + idprindi + '&raporti=raporti' + '&idraporti=' + idRaporti, 600, 600);
}

function ButtonClickedTitulli(editor) {
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni pozicionin', 'LupaProfesioneTitujPune.aspx?vjenNga=Raporti', '600', '600');
}

function ButtonClickedPunonjes(editor) {
    var headerText = hfState.Get("msgZgjidhPunonjesin");
    var contentUrl = 'LupaPunonjes.aspx?vjenNga=raporti';
    var widthLupa = '800';
    var heightLupa = '600';
    editorGlobal = editor;
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
    var idRaporti = Utils.getUrlVar('idraporti');
    switch (idRaporti) {
        case "133": case "144": case "162": case "134": case "173": case "220": case "222":
            contentUrl = 'LupaBanka.aspx?arkabanka=3&vjenNgaRaporti=true';
            break;
        case "20": case "163": case "174": case "223": case "224":
            contentUrl = 'LupaBanka.aspx?arkabanka=4&vjenNgaRaporti=true';
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

function ButtonClickedMagazina(editor) {
    var headerText = hfState.Get("msgZgjidhMagazinen");
    var idRaporti = Utils.getUrlVar('idraporti');
    switch (idRaporti) {
        case "171": case "175": case "176": case "177": case "212":
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

function ButtonClickedKlientFurnitor(editor) {//TODO PATI - Perdoret edhe ketu id e raportit
    var headerText = hfState.Get("msgZgjidhKF");
    var contentUrl;
    var idRaporti = Utils.getUrlVar('idraporti');
    switch (idRaporti) {
        case "28": case "27": case "39": case "42": case "48": case "59": case "60": case "61": case "64": case "193": case "225": case "200": case "233":
            contentUrl = 'LupaKlientFurnitor.aspx?KlientApoFurnitor=Furnitor&vjenNgaRaporti=true';
            break;
        case "41": case "40": case "22": case "140": case "29": case "141": case "145": case "143": case "190": case "33": case "139": case "43":
        case "67": case "46": case "62": case "63": case "118": case "121": case "166": case "182": case "106": case "196": case "198": case "211": case "248": case "254": case "261": case "267": case "280":
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
    var contentUrl = 'LupaPerdorues.aspx';
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
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni qendra', 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Raporti2&idPrindi=' + (btnQenderKosto1.GetValue() == null ? 0 : btnQenderKosto1.GetValue()), '900', '600');
}

function merrPrindQK() {
    $.ajax({
        pritPergjigje: true,
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
    $.ajax({
        pritPergjigje: true,
       url: Utils.getServerApiUrl("ListPagesa", "MerrPrindGrupimi"),
        data: JSON.stringify({ idgrupilocal: btnLocalBand.GetValue() })
    }).done(SuccedeedPrind);
}
function SuccedeedPrind(result) {
    btnGlobalBand.SetText(result.Kodi);
    btnGlobalBand.SetValue(result.Id);

    //btnGlobalBand.SetSelectedIndex(btnGlobalBand.SetText(result.Kodi, result.Id));

}

function ButtonClickedQenderKosto(editor) {
    var headerText = hfState.Get("msgZgjidhQendrenEKostos");
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    var contentUrl = 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=raporti';
    if (Utils.getUrlVar('idraporti') == 178 || Utils.getUrlVar('idraporti') == 179 || Utils.getUrlVar('idraporti') == 180)
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

function ButtonClickedKartela(editor) {
    var headerText = hfState.Get("roundPanelZgjidhArtikullin");
    var contentUrl = 'LupaArtikull.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupArtikulli = "raportArtikull";
    var idRaporti = Utils.getUrlVar('idraporti');
    if (idRaporti != 99) //raportProdhimi
    {
        if (idRaporti == 171 || idRaporti == 175 || idRaporti == 177 || idRaporti == 212 || idRaporti == 216)
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?aqt=aqt&vjenNgaRaporti=true', widthLupa, heightLupa);
        else
            myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
    }
    else
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value, widthLupa, heightLupa);
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
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni kategorine e shpenzimit', 'LupaKategoriShpenzimi.aspx', 680, 600);
}

function ButtonClickedPershkrimArt(editor) {
    var headerText = hfState.Get("roundPanelZgjidhArtikullin");
    var contentUrl = 'LupaArtikull.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupArtikulli = "raportPershArt";
    if (Utils.getUrlVar('idraporti') != 99) //raportProdhimi
    {
        if (Utils.getUrlVar('idraporti') == 171 || Utils.getUrlVar('idraporti') == 175 || Utils.getUrlVar('idraporti') == 177 || Utils.getUrlVar('idraporti') == 212)
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?aqt=aqt&vjenNgaRaporti=true', widthLupa, heightLupa);
        else
            myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
    }
    else
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value, widthLupa, heightLupa);
}

function ButtonClickedArtikullBurim(editor) {
    var headerText = hfState.Get("roundPanelZgjidhArtikullin");
    var contentUrl = 'LupaArtikull.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupArtikulli = "raportArtikullBurim";
    if (Utils.getUrlVar('idraporti') != 99)//raportProdhimi
        myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
    else
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value, widthLupa, heightLupa);

}


function ButtonClickedNjesiProdhimi(editor) {
    var headerText = hfState.Get("roundPanelZgjidhNjesiProdhimi");
    var contentUrl = 'LupaNjesiProdhimi.aspx?vjenNgaRaporti=true';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupNjesiProdhimi = "raportNjesiProdhimi";
    //if (Utils.getUrlVar('idraporti') != 99)//raportProdhimi
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
    //else
    //    myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?klasa=0&vjenNgaRaporti=true&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value, widthLupa, heightLupa);

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
    var id = Utils.getUrlVar("idraporti")
    if (id === "107" || id === "108" || id === "110")
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
    var id = Utils.getUrlVar("idraporti");
    contentUrl = 'LupaDetajime.aspx';
    var widthLupa = '750';
    var heightLupa = '600';
    editorGlobal = editor;
    identifikuesPerPopupDetajime = "raportDetajimePershkrim";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function ButtonClickedPikeShitjeFurnizim(editor) {//TODO PATI - Perdoret edhe ketu id e raportit
    var headerText = hfState.Get("msgZgjidhPikeShitjeFurnizimi");
    var contentUrl;
    var idRaporti = Utils.getUrlVar('idraporti');
    switch (idRaporti) {
        case "22": case "140": case "29": case "141": case "145": case "33": case "139": case "40": case "43": case "190": case "57": case "67": case "121": case "211": case "248": case "261": case "280":
            contentUrl = 'LupaPikeShitjeFurnizimi.aspx?shitjeblerje=Shitje&vjenNgaRaporti=true';
            break;
        case "27": case "28": case "39": case "54": case "59": case "60": case "145": case "143": case "190": case "33": case "139": case "43": case "233":
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
    switch (parseInt(Utils.getUrlVar('idraporti'))) {
        case 176:
            var contentUrl = 'LupaKodifikimArtikulli.aspx?llojartikulli=true&llojkodifikimi=1&nivelKodifikimi=1';
            break;
        case 171:
        case 175:
        case 177:
        case 212:
        case 216:
        case 269:
            var contentUrl = 'LupaKodifikimArtikulli.aspx?llojartikulli=true&llojkodifikimi=1';
            break;
        default:
            var contentUrl = 'LupaKodifikimArtikulli.aspx?llojkodifikimi=1';
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

function ButtonClickedGrupimArt2(editor) {
    var headerText = hfState.Get("msgZgjidhGrupin2TeArtikullit");
    if (Utils.getUrlVar('idraporti') == 171 || Utils.getUrlVar('idraporti') == 175 || Utils.getUrlVar('idraporti') == 212) {
        var contentUrl = 'LupaKodifikimArtikulli.aspx?llojartikulli=true&llojkodifikimi=2';
    } else {
        var contentUrl = 'LupaKodifikimArtikulli.aspx?llojkodifikimi=2';
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
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpTextZgjidhNdermarrjet"), 'LupaNdermarjeBij.aspx?vjenNgaRaporti=true', 750, 600);
}

function ButtonClickedGrupim1KF(editor) {
    if (lblGrupPKF.GetText() == "Grup Klienti 1") {
        var headerText = hfState.Get("msgZgjidhGrupin1TeKlientit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=1&kf=klient';
    } else if (lblGrupPKF.GetText() == "Grup Furnitori 1") {
        headerText = hfState.Get("msgZgjidhGrupin1TeFurnitorit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=1&kf=furnitor';
    } else {
        headerText = hfState.Get("msgZgjidhGrupin1TeKlientFurnitorit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=1';
    }
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}
function ButtonClickedGrupim2KF(editor) {
    if (lblGrupPKF.GetText() == "Grup Klienti 1") {
        var headerText = hfState.Get("msgZgjidhGrupin2TeKlientit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=2&kf=klient';
    } else if (lblGrupPKF.GetText() == "Grup Furnitori 1") {
        headerText = hfState.Get("msgZgjidhGrupin2TeFurnitorit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=2&kf=furnitor';
    } else {
        headerText = hfState.Get("msgZgjidhGrupin2TeKlientFurnitorit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=2';
    }
    var widthLupa = '750';
    var heightLupa = '600';
    identifikuesPerPopupKodifikimin = "raportGrupim";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}
function ButtonClickedGrupim3KF(editor) {
    if (lblGrupPKF.GetText() == "Grup Klienti 1") {
        var headerText = hfState.Get("msgZgjidhGrupin3TeKlientit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=3&kf=klient';
    } else if (lblGrupPKF.GetText() == "Grup Furnitori 1") {
        headerText = hfState.Get("msgZgjidhGrupin3TeFurnitorit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=3&kf=furnitor';
    } else {
        headerText = hfState.Get("msgZgjidhGrupin3TeKlientFurnitorit");
        contentUrl = 'LupaGrupimeKlientFurnitor.aspx?llojkodifikimi=3';
    }
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
    if (Utils.getUrlVar('idraporti') == 111 || Utils.getUrlVar('idraporti') == 138 || Utils.getUrlVar('idraporti') == 187) {
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

function klickselectedvaluedokKrahasues(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDokKrahasues = theRadio.GetSelectedItem().value;
        if (rblCaseControlDokKrahasues == 'Periudha') {
            txtNgaDtKrahasues.SetEnabled(true);
            txtDeriDtKrahasues.SetEnabled(true);
        }
        else {
            txtNgaDtKrahasues.SetEnabled(false);
            txtDeriDtKrahasues.SetEnabled(false);
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
    //else
    //    window.parent.callWebServiceKtheInfoLart('Raporti.aspx', Utils.getUrlVar('idraporti'));
    window.parent.createCookie('adresa', 'Raporti.aspx', 1);
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
function initNgaDok(s, e) {
    //$(s.GetInputElement()).css('zIndex', 3000);
}
function ngaDokDateChanged(s, e) {
    if (txtDeriDok.GetDate() < txtNgaDok.GetDate())
        txtDeriDok.SetDate(txtNgaDok.GetDate());

    if (Utils.getUrlVar('idraporti') == 111 || Utils.getUrlVar('idraporti') == 138 || Utils.getUrlVar('idraporti') == 187) {
        txtDeriDok.SetDate(txtNgaDok.GetDate());
    }
}
function deriDokDateChanged(s, e) {
    if (Utils.getUrlVar('idraporti') == 111 || Utils.getUrlVar('idraporti') == 138 || Utils.getUrlVar('idraporti') == 187) {
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

function NgaDtStatusChanged(s, e) {
    if (txtDeriDtStatus.GetDate() < txtNgaDtStatus.GetDate())
        txtDeriDtStatus.SetDate(txtNgaDtStatus.GetDate());
}

function ngaDokDateChangedKryesor(s, e) {
    if (txtDeriDokKryesor.GetDate() < txtNgaDokKryesor.GetDate())
        txtDeriDokKryesor.SetDate(txtNgaDokKryesor.GetDate());
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
function ngaRegjDateChanged(s, e) {
    if (txtDeriRegj.GetDate() < txtNgaRegj.GetDate())
        txtDeriRegj.SetDate(txtNgaRegj.GetDate());
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
    //return constanteParashtese + key + constantePrapashtese + 'txtNrLlogari' + key;
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
    //s += values[0];
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
            window.open("Raporti.aspx");
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
    $(document).keydown(function (e) {//po
        if (event.which == 13 || event.keyCode == 13) {
            faqjere = true;
            Pageload();
            return false;
        }
        return true;
    });
    $(window).on('load', function () {
        Init();
        renditFiltra();
    });
    $(window).bind("beforeunload", function () { fshiFleteKontabel(); });
});

function renditFiltra() {//TODO PATI - Perdoret edhe ketu id e raportit
    myMesazh.shtoHandler();
    idGjuha = hfgjuha.Get("idGjuha");

    if (Utils.getUrlVar('idraporti') == 273) {
        radDtRegj.SetSelectedIndex(0);
        txtNgaRegj.SetDate(new Date());
        txtDeriRegj.SetDate(new Date());

        if (hfPerdorues.Get("Roli") === "RA")
            btnePerdorues.SetEnabled(true);
        else
            btnePerdorues.SetEnabled(false);
        btnePerdorues.SetText(hfPerdorues.Get("Perdoruesi"));

    }


    var id = Utils.getUrlVar("idraporti");
    if (id === "27" || id === "28" || id === "39" || id === "54" || id === "59" || id === "60" || id === "61" || id === "64" || id === "42" || id === "48" || id === "117" || id === "103" || id === "107" || id === "193" || id === "233") {

        lblGrupTKF.SetText(hfgjuha.Get("labelGrupFurnitori3"));
        lblGrupDKF.SetText(hfgjuha.Get("labelGrupFurnitori2"));
        lblGrupPKF.SetText(hfgjuha.Get("labelGrupFurnitori1"));
    }
    if (id === "22" || id === "140" || id === "29" || id === "141" || id === "143" || id === "145" || id === "33" || id === "139" || id === "40" || id === "43" || id === "57" || id === "62" || id === "67" || id === "63" || id === "41" || id === "46" || id === "121" || id === "166" || id === "182" || id === "190" || id === "211" || id === "248" || id === "254" || id === "261" || id === "279" || id === "280") {

        lblGrupTKF.SetText(hfgjuha.Get("labelGrupKlienti3"));
        lblGrupDKF.SetText(hfgjuha.Get("labelGrupKlenti2"));
        lblGrupPKF.SetText(hfgjuha.Get("labelGrupKlienti1"));
    }
    if (id === "23" || id === "26" || id === "45" || id === "47") {

        lblGrupTKF.SetText(hfgjuha.Get("labelGrupKlientFurnitor3"));
        lblGrupDKF.SetText(hfgjuha.Get("labelGrupKlientFurnitor2"));
        lblGrupPKF.SetText(hfgjuha.Get("labelGrupKlientFurnitor1"));
    }
    if (id == 268)
        txtInterval6.SetVisible(false);
    if (id === "122") {
        var filtraKryesor = navBarFiltrat.GetGroupByName("filtraKryesor");
        filtraKryesor.SetVisible(false);
    }
    var filtroQueryString = Utils.getUrlVar("Filtro");
    if (filtroQueryString == "true") {
        $('#divToolbarContainer').css('display', 'none');
        var filtraAvancuar = navBarFiltrat.GetGroupByName("filtraAvancuar");
        filtraAvancuar.SetExpanded(true);
    }
    else {
        $('#divToolbarContainer').css({ display: '' });
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
        //$('#' + idDivKrye[i]).css({ display: 'inline', left: '1%', width: '100%' });//, position: 'absolute', top: topDivKrye[i] + '%'
        $('#' + idDivKrye[i]).show();
        $('#' + idDivKrye[i]).css({ left: '1%' });
    }
    for (i in idDivAvanc) {
        //$('#' + idDivAvanc[i]).css({ display: 'inline', left: '1%', width: '100%' });//, position: 'absolute', top: topDivAvanc[i] + '%'
        $('#' + idDivAvanc[i]).show();
        $('#' + idDivAvanc[i]).css({ left: '1%' });
    }
    //$('#filtraKrye').css({ height: (40 * idDivKrye.length) });
    //$('#filtraAvanc').css({ height: (40 * idDivAvanc.length) });
    if (id == 232 || id == 247) {
        cmbLidhesaKatShpenzimi.SetEnabled(false);
        cmbVeprimiKatShpenzimi2.SetEnabled(false);
        btKatShpenzimi2.SetEnabled(false);
        cmbVeprimiKatShpenzimi1.SetEnabled(false);
    }
    if (id == 256) {
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
    if (Utils.getUrlVar('idraporti') == 111 || Utils.getUrlVar('idraporti') == 138 || Utils.getUrlVar('idraporti') == 187) {
        radDtDok.SetSelectedIndex(1);
        txtNgaDok.SetDate(new Date());
        txtDeriDok.SetDate(new Date());
        radDtDok.SetEnabled(false);
        txtDeriDok.SetEnabled(false);
    }
    if (Utils.getUrlVar('idraporti') == 194) {
        radDtRegj.SetSelectedIndex(1);
        txtNgaRegj.SetDate(new Date());
        txtDeriRegj.SetDate(new Date());
    }

    if (Utils.getUrlVar('idraporti') == 98) {
        radDtDok.SetSelectedIndex(1);
        radDtDok.SetEnabled(false);

    }
    if (Utils.getUrlVar('idraporti') == 229 || Utils.getUrlVar('idraporti') == 230) {
        radDtDok.SetSelectedIndex(2);
    }
    if (Utils.getUrlVar('idraporti') == 235 || Utils.getUrlVar('idraporti') == 241) {
        radDtDokKrahasues.SetSelectedIndex(2);
        radDtDok.SetSelectedIndex(2);
    }


}

function DisableCombo() {
    if (Utils.getUrlVar('idraporti') == '22'
          || Utils.getUrlVar('idraporti') == '29'
           || Utils.getUrlVar('idraporti') == '33'
           || Utils.getUrlVar('idraporti') == '56'
           || Utils.getUrlVar('idraporti') == '86'
           || Utils.getUrlVar('idraporti') == '111'
           || Utils.getUrlVar('idraporti') == '23'
           || Utils.getUrlVar('idraporti') == '41'
           || Utils.getUrlVar('idraporti') == '46'
           || Utils.getUrlVar('idraporti') == '106'
         || Utils.getUrlVar('idraporti') == '196'
         || Utils.getUrlVar('idraporti') == '182'
        || Utils.getUrlVar('idraporti') == '160'
         || Utils.getUrlVar('idraporti') == '187'
        || Utils.getUrlVar('idraporti') == '198'
        || Utils.getUrlVar('idraporti') == '249'
         || Utils.getUrlVar('idraporti') == '254') {
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
        if ((filterVeprim.name.indexOf('cmbVeprimi1GrupPKF') != -1 || filterVeprim.name.indexOf('cmbVeprimi2GrupPKF') != -1 || filterVeprim.name.indexOf('cmbVeprimi3GrupPKF') != -1) && (Utils.getUrlVar('idraporti') == '22' || Utils.getUrlVar('idraporti') == '29' || Utils.getUrlVar('idraporti') == '33' || Utils.getUrlVar('idraporti') == '56'
            || Utils.getUrlVar('idraporti') == '86' || Utils.getUrlVar('idraporti') == '111' || Utils.getUrlVar('idraporti') == '23' || Utils.getUrlVar('idraporti') == '41'
            || Utils.getUrlVar('idraporti') == '46' || Utils.getUrlVar('idraporti') == '106' || Utils.getUrlVar('idraporti') == '182' || Utils.getUrlVar('idraporti') == '187' || Utils.getUrlVar('idraporti') == '196' || Utils.getUrlVar('idraporti') == '198' || Utils.getUrlVar('idraporti') == '249' || Utils.getUrlVar('idraporti') == '254')) {
            filterVeprim.SetEnabled(false);
        }
    }
}

function lidhes_valueChanged1(s, e, filterVeprim, filter) {
    if (s.GetSelectedIndex = 0) {
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
    if (Utils.getUrlVar('idraporti') == '65' || Utils.getUrlVar('idraporti') == '66' || Utils.getUrlVar('idraporti') == '67') {
        if (s.GetSelectedIndex() == 0)
            radDtDok.SetSelectedIndex(0);
        if (Utils.getUrlVar('idraporti') == '66' && s.GetSelectedIndex() == 0) {
            radDtDokKrahasues.SetSelectedIndex(0);
            Utils.toggleKontrolletPeriudha(radDtDokKrahasues, txtNgaDtKrahasues, txtDeriDtKrahasues);
        }
        if (s.GetSelectedIndex() == 1)
            radDtDok.SetSelectedIndex(2);
        if (Utils.getUrlVar('idraporti') == '66' && s.GetSelectedIndex() == 1) {
            radDtDokKrahasues.SetSelectedIndex(2);
            Utils.toggleKontrolletPeriudha(radDtDokKrahasues, txtNgaDtKrahasues, txtDeriDtKrahasues);
        }

        Utils.toggleKontrolletPeriudha(radDtDok, txtNgaDok, txtDeriDok);

    }
}


function filter2_Init(sender, event, lidhes) {
    if (lidhes.GetText() == ' ') {
        sender.SetEnabled(false);
        var name = sender.name;
        if (name.indexOf('cmbVepri') > 0)
            sender.SetSelectedIndex(-1);
        else
            sender.SetText("");
    }
    var idRaporti = Utils.getUrlVar("idraporti");
    if (idRaporti == 43 || idRaporti == 67 || idRaporti == 190 || idRaporti == 211 || idRaporti == 261)
        vendosVleraDefaultKlasaArtikullit();
}
function Pageload(s, e) {
    ImbReportToolbar2.OnPageLoad(e);
    if (faqjere) {
        var windowWidth = $(window).width();
        var idRaporti = Utils.getUrlVar("idraporti");
        window.open("RaportiShpejte.aspx?idraporti=" + idRaporti + "&printo=0&Sesioni=true&windowWidth=" + windowWidth + "&Orientimi=" + ImbReportToolbar2.GetOrientation() + "&reportStyle=" + ImbReportToolbar2.GetStyle() + "&scopeID=" + Utils.getUrlVar("scopeID"));
        faqjere = false;
    }
}


var faqjere = false;
function menu_click(s, e) {
    if (e.item.name != "Anullo")
        $('#divToolbarContainer').css('display', '');
    if (e.item.name == 'Pastro') {
        pastrofiltrat();
        cmbfiltra.SetValue(0);
    }
    else if (e.item.name == "Anullo") {//TODO PATI - Perdoret edhe ketu id e raportit
        var id = Utils.getUrlVar("idraporti");
        if (id === "0" || id === "1" || id === "2" || id === "3" || id === "4" || id === "5" || id === "24" || id === "25" || id === "53" || id === "65" || id === "66" || id === "98" || id === "188" || id === "202" || id === "205" || id === "207" || id === "208" || id === "229" || id === "230" || id === "232" || id === "236" || id === "235" || id === "239" || id === "240" || id === "241" || id === "247")
            window.location = "Raportet.aspx?idmod=7";
        if (id === "27" || id === "28" || id === "39" || id === "54" || id === "59" || id === "60" || id === "61" || id === "64" || id === "103" || id === "107" || id === "117" || id === "181" || id === "172" || id === "183" || id === "184" || id === "193" || id === "214" || id === "218" || id === "226" || id === "233" || id === "237")
            window.location = "Raportet.aspx?idmod=13";
        if (id === "22" || id === "140" || id === "111" || id === "138" || id === "29" || id === "141" || id === "145" || id === "33" || id === "139" || id === "40" || id === "43" || id === "57" || id === "62" || id === "67" || id === "63" || id === "82" || id === "83" || id === "86" || id === "102" || id === "110" || id === "112" || id === "121" || id === "122" || id === "127" || id === "56" || id === "143" || id === "155" || id === "156" || id === "157" || id === "160" || id === "165" || id === "169" || id === "185" || id === "187" || id === "189" || id === "190" || id === "203" || id === "204" || id === "209" || id === "210" || id === "211" || id === "215" || id === "217" || id === "238" || id === "248" || id === "249" || id === "250" || id === "256" || id === "255" || id === "259" || id === "260" || id === "261" || id === "265" || id === "280")
            window.location = "Raportet.aspx?idmod=12";
        if (id === "30" || id === "31" || id === "32" || id === "55" || id === "58" || id === "68" || id === "80" || id === "81" || id === "84" || id === "100" || id === "101" || id === "108" || id === "123" || id === "124" || id === "125" || id === "126" || id === "128" || id === "130" || id === "131" || id === "132" || id === "150" || id === "153" || id === "151" || id === "154" || id === "152" || id === "158" || id === "159" || id === "164" || id === "167" || id === "186" || id === "195" || id === "200" || id === "206" || id === "219" || id === "225" || id === "231" || id === "227" || id === "234" || id === "243" || id === "245" || id === "258" || id === "266" || id === "268")
            window.location = "Raportet.aspx?idmod=16";
        if (id === "23" || id === "26" || id === "41" || id === "42" || id === "45" || id === "46" || id === "47" || id === "48" || id === "105" || id === "106"
            || id === "161" || id === "166" || id === "182" || id === "196" || id === "197" || id === "198" || id === "213" || id === "251" || id === "254"
            || id === "253" || id === "263" || id === "264" || id === "267" || id === "277")
            window.location = "Raportet.aspx?idmod=9";
        if (id === "20" || id === "49" || id === "50" || id === "78" || id === "79" || id === "135" || id === "163" || id === "174" || id === "223" || id === "224")
            window.location = "Raportet.aspx?idmod=6";
        if (id === "69" || id === "70" || id === "71" || id === "72" || id === "73" || id === "74" || id === "75" || id === "76" || id === "77" || id === "192" || id === "274" || id === "276" || id === "278" || id === "279" || id === "280" || id === "281" || id === "282" || id === "283" || id === "284" || id === "288")
            window.location = "Raportet.aspx?idmod=17";
        if (id === "90" || id === "92" || id === "93" || id === "94" || id === "95" || id === "96" || id === "97" || id === "99" || id === "118" || id === "191" || id === "201")
            window.location = "Raportet.aspx?idmod=18";
        if (id === "119" || id === "120" || id === "178" || id === "179" || id === "180" || id === "262" || id == "257")
            window.location = "Raportet.aspx?idmod=20";
        if (id === "109" || id === "113" || id === "114" || id === "115" || id === "133" || id === "134" || id === "136" || id === "137" || id === "144" || id === "162" || id === "173" || id === "220" || id === "222")
            window.location = "Raportet.aspx?idmod=2";
        if (id === "171" || id === "175" || id === "176" || id === "177" || id === "199" || id === "212" || id === "216" || id === "244" || id === "269")
            window.location = "Raportet.aspx?idmod=21";
        if (id === "194" || id === "221" || id === "273")
            window.location = "Raportet.aspx?idmod=22";
        if (id === "272")
            window.location = "Raportet.aspx?idmod=24";
    }
        //else if (e.item.name == "Eksporto") { return; }

    else {
        var filtraAvancuar = navBarFiltrat.GetGroupByName("filtraAvancuar");
        filtraAvancuar.SetExpanded(false);
        $('#hfRilodo').val(true);
        ImbReportToolbar2.ResetArsyeReload();
        reportViewer.Refresh();
        if (e.item.name === "HapPopup") {
            faqjere = true;
        }
    }
    e.processOnServer = false;

}

function pastrofiltrat() {
    var idKontrollet = JSON.parse($('#hfKontrolle').val());
    var d = new Date();
    var idRaporti = Utils.getUrlVar("idraporti");
    for (i in idKontrollet) {

        switch (idKontrollet[i].IdTipiKontrollit) {
            case 1:
                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('');
                break;
            case 2:


                if (idKontrollet[i].KodKontrolli.search('cmbMenu1') != -1)
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);

                else
                    if (idKontrollet[i].KodKontrolli.search('cmbLlojVeprimi1') != -1)
                        Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                    else

                        if (idKontrollet[i].KodKontrolli.search('1') != -1)
                            Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(0);
                        else
                            if (idKontrollet[i].KodKontrolli.search('cmbLikuiduar') != -1)
                                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(2);
                            else if ((idRaporti == 203 || idRaporti == 204) && idKontrollet[i].KodKontrolli.search('cmbCikli') != -1)
                                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(1);
                            else if ((idRaporti == 203 || idRaporti == 204) && idKontrollet[i].KodKontrolli.search('cmbShfaqVlerat') != -1)
                                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(1);
                            else if ((idRaporti == 203 || idRaporti == 204) && idKontrollet[i].KodKontrolli.search('cmbTipGrafiku') != -1)
                                Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Bar');
                            else
                                if (idKontrollet[i].KodKontrolli.search('cmbPaguar') != -1)
                                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue(3);
                                else
                                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetSelectedIndex(0);
                break;
            case 3: if (idKontrollet[i].KodKontrolli == 'txtNgaRegj')
            { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
            else if (idKontrollet[i].KodKontrolli == 'txtDeriRegj')
            { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }

                if (idKontrollet[i].KodKontrolli == 'txtNgaDtFillimi')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDtFillimi')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }


                else if (idKontrollet[i].KodKontrolli == 'txtNgaDokAmortizim')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokAmortizim')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }


                else if (idKontrollet[i].KodKontrolli == 'txtNgaDokReg')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokReg')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('31/12/9999'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }

                else if (idKontrollet[i].KodKontrolli == 'txtNgaDokAfatKohor')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriDokAfatKohor')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaPeriudheFillimi')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriPeriudheFillimi')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtNgaPeriudheMbarimi')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('01/01/1900'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else if (idKontrollet[i].KodKontrolli == 'txtDeriPeriudheMbarimi')
                { Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText('30/12/9999'); Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true); }
                else {
                    Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetText(d.format("dd/mm/yyyy"));
                    if ((idRaporti == 111 || idRaporti == 138 || idRaporti == 187) && (idKontrollet[i].KodKontrolli == 'txtNgaDok')) Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(true);
                    else
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

                if (idRaporti == 194) {
                    txtNgaRegj.SetDate(new Date());
                    txtDeriRegj.SetDate(new Date());
                    break;
                }
                else {

                    if ((idKontrollet[i].KodKontrolli == 'radDtRegj') || (idKontrollet[i].KodKontrolli == 'radDtDokAmortizimi') || (idKontrollet[i].KodKontrolli == 'radDtDokAfatKohor') || (idKontrollet[i].KodKontrolli == 'radPeriudheFillimi') || (idKontrollet[i].KodKontrolli == 'radPeriudheMbarimi') || (idKontrollet[i].KodKontrolli == 'radDtDtFillimi'))

                        Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                    else {
                        if (idRaporti == 111 || idRaporti == 138 || idRaporti == 187) {
                            Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Periudha');
                            txtNgaDok.SetDate(txtDeriDok.GetDate());
                            txtDeriDok.SetDate(txtNgaDok.GetDate());
                            txtNgaDok.SetEnabled(true);
                            txtDeriDok.SetEnabled(false);
                        }

                        else
                            Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetValue('Aktuale');
                    }
                }
                break;
        }
        if (idKontrollet[i].KodKontrolli.search('Interval') != -1) {
            txtInterval1.SetText("0");
            txtInterval2.SetText("30");
            txtInterval3.SetText("60");
            txtInterval4.SetText("90");
            txtInterval5.SetText("120");
            txtInterval6.SetText("150");
        }
        else if (idKontrollet[i].KodKontrolli.search('2') != -1) Utils.ktheKontroll(idKontrollet[i].KodKontrolli).SetEnabled(false);

    }
    var idRaporti = Utils.getUrlVar("idraporti");


    if (idRaporti == 43 || idRaporti == 67 || idRaporti == 190 || idRaporti == 211 || idRaporti == 261)
        vendosVleraDefaultKlasaArtikullit();

    if (idRaporti == 82) vendosVleraDefaultStatusKonvertuar();
}
function vendosVleraDefaultStatusKonvertuar() {
    //  if (Utils.getUrlVar("idraporti") == 82) {
    cmbStatusKonvertimiDyte1.SetValue(3);
    cmbStatusKonvertimiDyte2.SetValue(2);
    cmbStatusKonvertimiDyte2.SetEnabled(true);
    cmbVeprimiStatusKonvertimiDyte2.SetEnabled(true);
    cmbVeprimiStatusKonvertimiDyte2.SetSelectedIndex(0);
    cmbLidhesaStatusKonvertimDyte.SetSelectedIndex(2);
    //   }

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
 $.ajax({
      pritPergjigje: false,
      url: Utils.getServerApiUrl("ListPagesa", "fshiRaportNgaSessioni"),
      data: JSON.stringify({})
   }).done(function () { });
}

function onInitDetajuar() {
    if (Utils.getUrlVar("idraporti") === "247")
        cbDetajuar.SetChecked(true);
}
