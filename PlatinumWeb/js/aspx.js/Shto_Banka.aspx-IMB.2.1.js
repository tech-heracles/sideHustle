;
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var widthLupaLlogaria = 750, heightLupaLlogaria = 600;
var widthLupaGrupiBanka = 700, heightLupaGrupiBanka = 600;
var widthLupaAutorizime = 600, heightLupaAutorizime = 600;
var resultkonf, colKontrollet, colAtrTrupi, KPF, komisionApoLlogari, gridabanka;

jQuery(document).ready(function () {
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

    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblBanka",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    });

    pastro();
});
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Bankat, "121", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

/**
 * Function: Hap faqen e modifikimit me double click.
 * @param {any} index Index i rreshtit te selektuar
 */
function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

/**
 * Function: Perdoret per te selektuar rreshtin me poshte.
 * @param {any} e eventi
 */
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Bankat, indexSel);
}

/**
 * Function: Perdoret per te selektuar rreshtin me lart.
 * @param {any} e eventi
 */
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Bankat, indexSel);
}

/**
 * Function: Perdoret per te shkuar ne fillim te faqes.
 * @param {any} e eventi
 */
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Bankat, indexSel);
}

/**
 * Function: Perdoret per te shkuar ne fund te faqes.
 * @param {any} e eventi
 */
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Bankat, indexSel);
}

/**
 * Function: Perdoret per veprimet e menuse ne javascript.
 * @param {any} s sender
 * @param {any} e eventi
 */
function menu_click(s, e) {
    Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

/**
 * Function: Merr te dhenat e rreshtit te selektuar ne gride.
 * */
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = ASPxGridView_Bankat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCeljeArkaBankaZgjidhNjeArkeBanke"));
    else
        ASPxGridView_Bankat.GetRowValues(indexModifiko, 'IdBanka;KodiBanka;EmerBanka;IdTipiBanka;NrLlogariBanka;IBAN;NrGrupBanke;ShenimeBanka;AktivBanka;NrLlogari;KodMonedha;RrugaBanka;QytetiBanka;ShtetiBanka;ZipKodBanka;TelBanka;EmerKontaktiBanka;MbiemerKontaktiBanka;TelKontaktiBanka;FaxKontaktiBanka;CelKontaktiBanka;EmailKontaktiBanka;NrKomisioni;LlojArkaBanka;AdresaKontaktiBanka;IdDegeAdministrative;Valuta;Dega;Tipi;KodiLlogarise;NrKlienti;KodiTCR;NrRendor', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();
}

/**
 * Function: Mbush fushat me te dhenat e rreshtit te selektuar.
 * @param {any} values Te dhenat e rreshtit.
 */
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'Banka', idPerdorues: hfState.Get('idPerdoruesi') })
    }).done(SucceededCallbackKtheAutorizime);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheVlerenEShfaqesSeEinvoice"),
        data: JSON.stringify({ idBanka: values[0]})
    }).done(SucceededCallbackKtheShaqeEinvoice);
    txtEmerBanke.SetText(values[2]);
    if (values[23] == true)
        cmbLloji.SetText(hfState.Get("cmbboxItemFilterAvancBanka"));
    else {
        cmbLloji.SetText(hfState.Get("cmbboxItemFilterAvancArka"));
        setFushatTCRVisible(true);
    }
    if(values[6]!=null && values[6]!="")
        txtGrupi.SetText(values[6]);
    else txtGrupi.SetValue(null);
    txtNrLlogariBankare.SetText(values[4]);
    txtIban.SetText(values[5]);
    txtShenime.SetText(values[7]);
    cbAktive.SetChecked(values[8]);
    if (values[9] != null && values[9] != "")
        txtNrLlogari.SetText(values[9]);
    else txtNrLlogari.SetValue(null);
    cmbMonedha.SetText(values[10]);
    komisioni_ButtonEdit.SetText(values[22]);
    txtKodiAdresa.SetText(values[1]);
    txtEmerBankeAdresa.SetText(values[2]);
    txtRruga.SetText(values[11]);
    txtQyteti.SetText(values[12]);
    txtShteti.SetText(values[13]);
    txtZipKod.SetText(values[14]);
    txtTel.SetText(values[15]);
    txtKodiKontakti.SetText(values[1]);
    txtEmerBankeKontakti.SetText(values[2]);
    txtEmer.SetText(values[16]);
    txtMbiemri.SetText(values[17]);
    txtTelK.SetText(values[18]);
    txtFax.SetText(values[19]);
    txtCel.SetText(values[20]);
    txtEmail.SetText(values[21]);
    txtAdresa.SetText(values[24]);
    cmbDegeAdministrative.SetValue(values[25]);
    //Valuta;Dega;Tipi;KodiLlogarise;NrKlienti;KodiTCR;NrRendor
    txtValuta.SetText(values[26]);
    txtDega.SetText(values[27]);
    txtTipi.SetText(values[28]);
    txtKodiLlogarise.SetText(values[29]);
    txtNrKlienti.SetText(values[30]);
    txtTCR.SetText(values[31]);
    txtNrRendor.SetText(values[32]);

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "121", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]); });

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SucceededCallbackKtheShaqeEinvoice(result) {
    cbShfaqEinvoice.SetChecked(result);
}
function SucceededCallbackKtheAutorizime(result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
    else cmbAutorizimi.SetValue(null);
}

/**
 * Function: Ben enabled dhe disabled fushat sipas lidhjes.
 * @param {any} result result
 * @param {any} idObjekti idObjekti
 */
function SucceededCallbackLidhur(result, idObjekti) {
    
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
    if (txtTCR.GetText() != '')
        btnTCR.SetEnabled(false);
}

/**
 * Function: Pastron dhe ben aktive fushat per shtim.
 * */
function pastrofusha() {
    txtKodi.SetText('');
    txtEmerBanke.SetText('');
    txtGrupi.SetValue(null);
    txtNrLlogariBankare.SetText('');
    txtIban.SetText('');
    txtShenime.SetText('');
    cbAktive.SetText('');
    txtNrLlogari.SetValue(null);
    cmbMonedha.SetValue(null);
    komisioni_ButtonEdit.SetValue(null);
    txtKodiAdresa.SetText('');
    txtEmerBankeAdresa.SetText('');
    txtRruga.SetText('');
    txtQyteti.SetText('');
    txtShteti.SetText('');
    txtZipKod.SetText('');
    txtTel.SetText('');
    txtKodiKontakti.SetText('');
    txtEmerBankeKontakti.SetText('');
    txtEmer.SetText('');
    txtMbiemri.SetText('');
    txtTelK.SetText('');
    txtFax.SetText('');
    txtCel.SetText('');
    txtEmail.SetText('');
    txtAdresa.SetText('');
    cmbDegeAdministrative.SetText('');
    txtValuta.SetText('');
    txtDega.SetText('');
    txtTipi.SetText('');
    txtKodiLlogarise.SetText('');
    txtNrKlienti.SetText('');
    cmbAutorizimi.SetValue(null);
    txtTCR.SetText('');
    txtNrRendor.SetText('');
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

/**
 * Function: Perdoret per te ruajtur indexin e selektimit.
 * @param {any} e eventi
 */
function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    ASPxGridView_Bankat.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvArkaBanka").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblBanka', 'tblAdresa', 'tblKontakti'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");

        if (Utils.getUrlVar('ab') == 'arka') {
            cmbLloji.SetText(hfState.Get("cmbboxItemFilterAvancArka"));
            ndryshoArkaBanka(hfState.Get("cmbboxItemFilterAvancArka"), hfMod.val());
        }
        else {
            cmbLloji.SetText(hfState.Get("cmbboxItemFilterAvancBanka"));
            ndryshoArkaBanka(hfState.Get("cmbboxItemFilterAvancBanka"), hfMod.val());
        }
        llogari_TextChanged();
    }
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaLlogari");
    var hf2 = $("#hfLupaKomisioni");
    var hf3 = $("#hfLupaGrupBanka");
    var hf4 = $("#hfLupaAutorizimi");
    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli == "txtNrLlogari") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "komisioni_ButtonEdit") {
            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "txtGrupi") {
            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
            hf4.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("121", cmbKonfigurimi.GetText());
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[0]);
    callWebserviceKonfigurimi("121", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}
function changeName() {

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Banka.aspx?ab=' + Utils.getUrlVar('ab'), 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

/**
 * Function: Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
 * Shiko funksionet  <ndryshoKonfigurimin>.
 * @param {any} sender sender
 * @param {any} args argumentat
 */
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Bankat, "121", pastrofusha, hfTeDrejta);
}

function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}
function ndryshoArkaBanka(text, shtim_modifikim) {
    if (text == hfState.Get("cmbboxItemFilterAvancArka")) {
        cmbTipi.SetVisible(false);
        tipi_Label.SetVisible(false);
        txtNrLlogariBankare.SetVisible(false);
        nrLlogBankare_Label.SetVisible(false);

        txtIban.SetVisible(false);
        iban_Label.SetVisible(false);
        komisioni_ButtonEdit.SetVisible(false);
        komisioni_Label.SetVisible(false);
        if (shtim_modifikim == "shtim") {
            setFushatTCRVisible(false);
        }
        PageControl.GetTab(2).SetVisible(false);
        PageControl.GetTab(1).SetText(hfState.Get("cmbboxItemFilterAvancArka"));
    }
    else {
        txtNrLlogariBankare.SetVisible(true);
        nrLlogBankare_Label.SetVisible(true);
        setFushatTCRVisible(false);
        PageControl.GetTab(2).SetVisible(true);
        PageControl.GetTab(1).SetText(hfState.Get("cmbboxItemFilterAvancBanka"));
    }
}

//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    changeName();
    cmbMonedha.SetEnabled(false);
}
var grida;
function Llogari_Click(nr) {//thirret popup i llogarive
    komisionApoLlogari = nr;
    gridabanka = false;
    var hf;
    if (nr == 1)
        hf = $("#hfLupaLlogari")[0];
    else
        hf = $("#hfLupaKomisioni")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("headerPopUpText"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function Grupi_Click() {//thiret popup i grupe banke
    var hf = $("#hfLupaGrupBanka")[0];
    var queryStr = hf.value;
    var header;
    var lloji = cmbLloji.GetText();
    if (lloji === 'Arka')
        header = hfState.Get("headerPopUpZgjidhGrupinEArkes");
    else
        header =  hfState.Get("headerPopUpZgjidhGrupinEBankes");
    myButtonClickLupa.GrupiBanka_Click(header, queryStr, widthLupaGrupiBanka, heightLupaGrupiBanka, lloji);
}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function llogari_TextChanged(s, e) {
    var text = txtNrLlogari.GetText().split(';');
    txtNrLlogari.SetText(text[0]);
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
        data: JSON.stringify({ kodi: txtNrLlogari.GetText(), idNderrmarje: hfState.Get("idNdermarrje") })
    }).done(SucceededCallbackLlog);

    cmbMonedha.SetText(text[2]);
}
function SucceededCallbackLlog(llogaria) {
    if (llogaria != null && llogaria.NrLlogari != -1) {
        cmbMonedha.SetText(llogaria.PershkrimiMonedha);
    }
} function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function clickExport(e) {
    if (ASPxGridView_Bankat.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function validoNrLlogarie()
{
    var llojNdermarrje = hfState.Get("llojNdermarrje");
    if (llojNdermarrje == 1 || llojNdermarrje == 3) {
        if (cmbLloji.GetText() == "Banka" && txtNrLlogari.GetText().split(';')[0].substring(0, txtNrLlogari.GetText().split(';')[0].length > 2 ? 2 : txtNrLlogari.GetText().split(';')[0].length) != "51") {
            myMesazh.ShtoMesazhInformues(hfState.Get("msgCeljeArkaBankaLlogariaTeFIllojeMe512"));
        }
    }
    else
        if (cmbLloji.GetText() == "Banka" && (txtNrLlogari.GetText().split(';')[0].substring(0, txtNrLlogari.GetText().split(';')[0].length > 3 ? 3 : txtNrLlogari.GetText().split(';')[0].length) != "520" && txtNrLlogari.GetText().split(';')[0].substring(0, txtNrLlogari.GetText().split(';')[0].length > 3 ? 3 : txtNrLlogari.GetText().split(';')[0].length) != "512")) {
        myMesazh.ShtoMesazhInformues(hfState.Get("msgCeljeArkaBankaLlogariaTeFIllojeMe520Ose5120"));
    }
    if (cmbLloji.GetText() == "Arka" && txtNrLlogari.GetText().split(';')[0].substring(0, txtNrLlogari.GetText().split(';')[0].length > 2 ? 2 : txtNrLlogari.GetText().split(';')[0].length) != "53") {
        myMesazh.ShtoMesazhInformues(hfState.Get("msgCeljeArkaBankaLlogariaTeFIllojeMe531"));
    }
}
function TextChanged_txtKodi(s, e) {
    txtKodiAdresa.SetText(txtKodi.GetText());
    txtKodiKontakti.SetText(txtKodi.GetText());
}

function TextChanged_txtEmerBanke(s, e) {
    txtEmerBankeAdresa.SetText(txtEmerBanke.GetText());
    txtEmerBankeKontakti.SetText(txtEmerBanke.GetText());
}

function TextChanged_txtKodiAdresa(s, e) {
    txtKodi.SetText(txtKodiAdresa.GetText());
    txtKodiKontakti.SetText(txtKodiAdresa.GetText());
}
function TextChanged_txtEmerBankeAdresa(s, e) {
    txtEmerBanke.SetText(txtEmerBankeAdresa.GetText());
    txtEmerBankeKontakti.SetText(txtEmerBankeAdresa.GetText());
}
function TextChanged_txtKodiKontakti(s, e) {
    txtKodi.SetText(txtKodiKontakti.GetText());
    txtKodiAdresa.SetText(txtKodiKontakti.GetText());
}

function TextChanged_txtEmerBankeKontakti(s, e) {
    txtEmerBanke.SetText(txtEmerBankeKontakti.GetText());
    txtEmerBankeAdresa.SetText(txtEmerBankeKontakti.GetText());
}

function Active_TabChanged(s, e) {
    indexModifiko = ASPxGridView_Bankat.GetFocusedRowIndex();
                      
    if(mbush)
    { 
        if(indexModifiko !=-1)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else {  
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim'; 
            $('#hfId')[0].value = 0; 
            pastrofusha();
        }
    }
    kaloTab=false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta,$('#hfShtimModifikim'));
}

function EndCallbackGrida(s, e) {
    if (hfState.Get("llog") != "-1")
        hfState.Set("llog", "-1");
}

function onNdryshimFokusi() {
    if (PageControl.GetActiveTabIndex() == 0)
        mbush = true;
}

function setFushatTCRVisible(enabled) {
    lblbtnTCR.SetVisible(enabled);
    btnTCR.SetVisible(enabled);
    lbltxtTCR.SetVisible(enabled);
    txtTCR.SetVisible(enabled);
}

function btnTCR_click(s, e) {
    
    var text = txtNrLlogari.GetText().split(';');
    txtNrLlogari.SetText(text[0]);
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "kontrolloVleratPerGjeneriminETcr"),
        data: JSON.stringify({ kodi: txtKodi.GetText(), idNderrmarje: hfState.Get("idNdermarrje"), kodBiznesi: cmbDegeAdministrative.GetText() })
    }).done(SucceededCallbackKontrollTCR);
    
}
function SucceededCallbackKontrollTCR(values) {
    if (values.Pershkrim != "Sukses")
        myMesazh.ShtoMesazhGabimi(values.Pershkrim);
    else {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "gjeneroKodinTCR"),
            data: JSON.stringify({ kodi: txtKodi.GetText(), idNderrmarje: hfState.Get("idNdermarrje"), kodBiznesi: cmbDegeAdministrative.GetText() })
        }).done(SucceededCallbackTCR);
    }
}
function SucceededCallbackTCR(values) {

    if (values == "Problem certifikate") {

        myMesazh.ShtoMesazhGabimi("Passwordi i certifikates elektronike nuk eshte i sakte!");
        return;
    }

    var kodi = values[1];

    if (values[1] != null) {

        myMesazh.ShtoMesazhGabimi("Ndodhi nje gabim ne gjenerimin e kodit TCR, Errori: " + kodi + " Pershkrimi i errorit:" + values[0]);
    }
    else {
        txtTCR.SetText(values[0]);
        btnTCR.SetEnabled(false);
    }}