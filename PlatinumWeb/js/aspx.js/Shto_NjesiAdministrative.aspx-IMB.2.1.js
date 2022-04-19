;
//variabla per te kaluar nga nje reshti i grides tek tjetri
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
                window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblMagazina",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    })
    changeName();
});

function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvNjesiAdm, "509", cmbKonfigurimi.GetText());
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    //indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvNjesiAdm, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    //indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvNjesiAdm, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    //indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvNjesiAdm, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    //indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvNjesiAdm, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:



e-eventi
*/

//Te validojme qe kodi nuk permban hapsira

function isValidKodi() {
    var kodi = txtKodi.GetText();
    {
        if (kodi.indexOf(' ') >= 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKodiMagazinaHapsira"));
            return false;
        }
        else return true;
    }
}
var indexStatus = -1;
function menu_click(s, e) {

    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name === "Klono") {
        hfNrAuto.Clear();
        hfNrAutoKF.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    if (e.item.name == 'Ruaj') {
        Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
        if (isValidKodi()) {
            ruajFushaShtese();
            $('#hfStatusMagazine').val(IdStatusMagazine.GetValue());
            $('#hfData').val(DataStatusit.GetText());
        }
        else e.processOnServer = false;
    }
    else if (e.item.name === 'Arkiva') {
        if ($('#hfShtimModifikim')[0].value == "modifikim" && gvNjesiAdm.GetRowKey(gvNjesiAdm.GetFocusedRowIndex()) == undefined) {

            myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        } else
            ButtonClickArkiva();
        // myFaqeCelje.kontrolloTeDrejta('LupaArkiva.aspx?veprimi=' + Utils.getUrlVar('shitje_blerje') + '&idDok=' + grid_RegDok.GetRowKey(grid_RegDok.GetFocusedRowIndex()) + '&shtim_modifikim=modifikim');
        e.processOnServer = false;
    }

}
function ButtonClickArkiva() {//po
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);
    //popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=njesiadmin&idDok=' + gvNjesiAdm.GetRowKey(gvNjesiAdm.GetFocusedRowIndex()) + '&shtim_modifikim=modifikim');
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=njesiadmin&idDok=' + gvNjesiAdm.GetRowKey(gvNjesiAdm.GetFocusedRowIndex())
        //+ '&shtim_modifikim=modifikim'
        + "&tmpfolder=" + hfArkiva.Get("rootFolder"));
    popupUniversal.Show();
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvNjesiAdm.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCeljeMagazinatDuhetTeZgjidhniNjeMagazine"));
    else
        gvNjesiAdm.GetRowValues(indexModifiko, 'IdNjesiAdministrative;Kodi;Pershkrimi;Adresa;IdInventarizimi;Aktiv;NdjekjeGjendje;DateRegjistrimi;IdDegeAdministrative;IdLlojMagazine;IdStatusAktualMagazine;DataNdryshimStatus;Kohezgjatja;Koordinata;CelPerdoruesTollonash;Shenime;Statusi;Telefon;LlojLayeri;IdMagPrind;KodMagPrind;Email;IdElementiPerIntegrim;QendraKostos;IdSkemaQendraKosto;LlojQendre;Qendra;KontrollGjendjeDet1;KontrollGjendjeDet2;OwnShop', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
   
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'Magazina', idPerdorues: hfState.Get('idPerdorues') })
    }).done(SucceededCallbackKtheAutorizime);
    txtKodi.SetText(values[1]);
    //if ($('#hfShtimModifikim').val() == "modifikim") {
    //    txtKodi.SetEnabled(false);
    //}
    txtPershkrimi.SetText(values[2]);
    txtAdresa.SetText(values[3]);
    cmbInventarizimi.SetValue(values[4]);
    cbAktiv.SetChecked(values[5]);
    cbNdjekjeGjendje.SetChecked(values[6]);
    cbDet1.SetChecked(values[27]);
    cbDet2.SetChecked(values[28]);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrTipiMagDheQyteti"),
        data: JSON.stringify({ idNjesi: values[0] })
    }).done(SucceededCallbackTipiMagDheQyteti);
    if ($('#hfShtimModifikim').val() == 'klonim') {
        var today = new Date();
        dteDtRegjistrimi.SetDate(today);
    }
    else
        dteDtRegjistrimi.SetDate(values[7]);
    cmbDegeAdministrative.SetValue(values[8]);
    TextChangedDega();

    cmbLloji.SetValue(values[9]);
    cmbStatusi.SetValue(values[10]);
    dteDtFillimStatusi.SetDate(values[11]);
    txtKohezgjatja.SetText(values[12]);
    if (values[13] != null && values[13] != "") {
        var koordinata = values[13].substring(values[13].indexOf('(') + 1, values[13].length - 1);
        koordinata = koordinata.split(' ');
        btneCaktoNeHarte.SetText(koordinata[1] + ', ' + koordinata[0]);
    }
    else
        btneCaktoNeHarte.SetText("");
    var tmpGeoms = new Array(1);
    tmpGeoms[0] = values[13];
    hfState.Set("geom", JSON.stringify(tmpGeoms));
    cbPerdorues.SetChecked(values[14]);
    if (values[14])
        cbPerdorues.SetEnabled(false);
    txtShenime.SetText(values[15]);
    txtTelefon.SetText(values[17]);
    cmbLlojLayer.SetValue(values[18]);
    if (values[19] != null) {
        Utils.SelectComboItem(cmbMagPrind, values[19], values[20]);
    }
    else
        cmbMagPrind.SetText('');
    txtEmail.SetText(values[21]);
    btneKodiPerIntegrim.SetValue(values[22]);
    cmbLlojiQ.SetValue(values[25]);
    if (values[25] == 1)
        qendraKostos_TextBox.SetSelectedIndex(qendraKostos_TextBox.AddItem(values[26], values[23]));
    else qendraKostos_TextBox.SetSelectedIndex(qendraKostos_TextBox.AddItem(values[26], values[24]));
    cbOwnShop.SetChecked(values[29]);
    RefreshFushatShtese($('#hfId').val(), 'mod');
    gvStatus.PerformCallback();
    LlojiChanged(cmbLloji);

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get("idNdermarrje");
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "509", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SucceededCallbackTipiMagDheQyteti(result) {
    btnTipiMag.SetValue(result.Tipimag);
    btnQyteti.SetValue(result.qyteti);
}
function SucceededCallbackKtheAutorizime(result) {
    if (result.idLidhese == $('#hfId').val()) {
        if (!Utils.IsNullOrEmpty(result.autorizime))
            cmbAutorizimi.SetText(result.autorizime);
        else
            cmbAutorizimi.SetValue(null);
    }
    else cmbAutorizimi.SetValue(null);
}
function TextChangedDega() {

    var dega = cmbDegeAdministrative.GetSelectedItem();
    if (dega != null)
        txtPershkrimDege.SetText(dega.GetColumnText('Pershkrimi'));
    else txtPershkrimDege.SetText("");
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {

    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;

    //        aktivizoFusha(hf.value);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
    LlojiChanged(cmbLloji);
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    $('#hfId').val(0);
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    txtAdresa.SetText('');
    cmbInventarizimi.SetValue(1);
    cbAktiv.SetChecked(true);
    cbNdjekjeGjendje.SetChecked(true);
    cbDet1.SetChecked(false);
    cbDet2.SetChecked(false);
    cmbAutorizimi.SetValue(null);
    qendraKostos_TextBox.SetSelectedIndex(-1);
    qendraKostos_TextBox.SetText('');
    dteDtRegjistrimi.SetDate(new Date());
    cmbDegeAdministrative.SetSelectedIndex(-1);
    cmbLloji.SetSelectedIndex(0); cmbLlojiQ.SetSelectedIndex(0);
    cmbStatusi.SetText('');
    dteDtFillimStatusi.SetText('');
    txtKohezgjatja.SetText('1');
    cbPerdorues.SetChecked(false);
    cbPerdorues.SetEnabled(true);
    btneCaktoNeHarte.SetText('');
    cmbLlojLayer.SetValue(1);
    cmbLlojLayer.SetText('');
    hfState.Set("geom", '');
    txtShenime.SetText('');
    txtTelefon.SetText('');
    txtEmail.SetText('');
    txtPershkrimDege.SetText('');
    cmbMagPrind.SetSelectedIndex(-1);
    btneKodiPerIntegrim.SetSelectedIndex(-1);
    cbOwnShop.SetChecked(false);
    gvStatus.PerformCallback();
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    PastroFushatShtese();
    LlojiChanged(cmbLloji);
}
function Qendra_Click() {
    if (cmbLlojiQ.GetValue() == 1)
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Shto_Llogari', 900, 600);
    else myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhSkemenKostos"), 'LupaSkemaKosto.aspx?vjenNga=Shto_Llogari', 600, 600);
}
/* 
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
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

    gvNjesiAdm.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvMagazina").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}
function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}
var resultkonf;
var colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblMagazina', 'tblFushatShtese'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
    if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
        hfNrAuto.Clear();
        hfNrAutoKF.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    LlojiChanged(cmbLloji);
    cmbStatusi.SetEnabled(false); //duhet te jene gjithmone te paeditueshme
    dteDtFillimStatusi.SetEnabled(false);
    if (Utils.getUrlVar('vjenNga') == 'GIS' && Utils.getUrlVar('idmagazina') !== '' && typeof hfState.Get("idNjesiPerSelektim") !== "undefined") {
        gvNjesiAdm.SelectRowOnPage(hfState.Get("idNjesiPerSelektim"));
        gvNjesiAdm.SetFocusedRowIndex(hfState.Get("idNjesiPerSelektim"));
        kaloTab = true;
        lista = true;
        mbush = false;
        $('#hfShtimModifikim')[0].value = "modifikim";
        gvNjesiAdm.GetRowValues(hfState.Get("idNjesiPerSelektim"), 'IdNjesiAdministrative;Kodi;Pershkrimi;Adresa;IdInventarizimi;Aktiv;NdjekjeGjendje;DateRegjistrimi;IdDegeAdministrative;IdLlojMagazine;IdStatusAktualMagazine;DataNdryshimStatus;Kohezgjatja;Koordinata;CelPerdoruesTollonash;Shenime;Statusi;Telefon;LlojLayeri;IdMagPrind;KodMagPrind', OnGetRowValuesMod);
        Utils.shfaqLoadingGif();;
    }
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf6 = $("#hfLupaAutorizimi");
    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi")
            hf6.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
        if (kontrollet[i].KodKontrolli == "cmbMagPrind")
            $("#hfLupaMagPrind").val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
        if (kontrollet[i].KodKontrolli == "btneKodiPerIntegrim")
            $("#hfLupaElementePerIntegrim").val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("509", cmbKonfigurimi.GetText());
    ndryshoKonfigurimFushaShtese(cmbKonfigurimi.GetValue());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("509", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    PageControl.GetTabByName('Ndryshim').SetVisible(false);
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_NjesiAdministrative.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
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
    //        indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvNjesiAdm, "509")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvNjesiAdm, "509", pastrofusha, hfTeDrejta, undefined, undefined, false);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
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

var KPF;
function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, 650, 550);
}

function KodiPerIntegrim_Click() {
    var hf = $("#hfLupaElementePerIntegrim");
    myButtonClickLupa.ElementePerIntegrim_Click(hfState.Get("headerPopUpZgjidhElementinPerIntegrim"), hf.val(), 650, 550, 1);
}

function MagPrindClick() {
    var hf = $("#hfLupaMagPrind")[0];
    identifikuesPerPopupMagazina = 'NjesiAdministrative';
    myButtonClickLupa.ButtonClickMagazina(hfState.Get("msgZgjidhMagazinen"), hf.value, 650, 550);
}

function EndCallbackGrida(s, e) {
    if ($('#hfShtimModifikim').val() != 'modifikim' || ($('#hfShtimModifikim').val() == 'modifikim' && gvStatus.cpRowCount == 0)) {
        IdStatusMagazine.SetSelectedIndex(1);
        DataStatusit.SetDate(new Date(new Date().getFullYear(), 0, 1));
    }
    else {
        IdStatusMagazine.SetSelectedIndex(-1);
        DataStatusit.SetText('');
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
    gvNjesiAdm.GetRowValues(gvNjesiAdm.GetFocusedRowIndex(), 'IdStatusAktualMagazine;DataNdryshimStatus;Statusi', OnGetRowValuesStatus);
}

function OnGetRowValuesStatus(values) {
    cmbStatusi.SetValue(values[0]);
    if ($('#hfShtimModifikim').val() != 'modifikim') {
        var date = new Date(new Date().getFullYear(), 0, 1);
        dteDtFillimStatusi.SetDate(date);
    }
    else
        dteDtFillimStatusi.SetDate(values[1]);
}


function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}

function LlojiChanged(s) {
    if (s.GetValue() == 1 || s.GetValue() == undefined) {
        PageControl.GetTabByName('Ndryshim').SetVisible(false);
        lblDtFillimStatusi.SetVisible(false);
        dteDtFillimStatusi.SetVisible(false);
        lblKohezgjatja.SetVisible(false);
        cmbStatusi.SetVisible(false);
        lblStatusi.SetVisible(false);
        txtKohezgjatja.SetVisible(false);
    }
    else {
        PageControl.GetTabByName('Ndryshim').SetVisible(true);
        setVisibleFushatPerAfatgjateSipasKonfigurimit(colKontrollet, colAtrTrupi)
        gvStatus.PerformCallback();
    }
}

function setVisibleFushatPerAfatgjateSipasKonfigurimit(colKontrollet, colAtrTrupi) {
    for (var i = 0; i < colKontrollet.length; i++) {
        k = Utils.ktheKontroll(colKontrollet[i].KodKontrolli);
        k.SetVisible(colAtrTrupi[i].Visible);
    }
}
function hapLupeHarte(s, e) {
    popupUniversal.SetHeaderText('Cakto ne harte');
    popupUniversal.SetContentUrl('LupaHarta.aspx?idNjesia=' + $('#hfId')[0].value);
    popupUniversal.SetSize(700, 800);
    popupUniversal.Show();
}

function clickExport(e) {
    if (gvNjesiAdm.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function vendosGeomNeHfState(s, e) {
    if (btneCaktoNeHarte.GetText() == '')
        hfState.Set("geom", '');
    else {
        var koordinata = btneCaktoNeHarte.GetText();
        koordinata = koordinata.split(', ');
        if (koordinata.length != 2) {
            alert('Formati i kordinatave duhet te jete: "x.x, y.yy" - pra te ndara me ", "');
        }
        var k = 'POINT (' + koordinata[1] + ' ' + koordinata[0] + ')';
        hfState.Set("geom", JSON.stringify(([k])));
    }
}

function activeTabsChanged(s, e) {
    indexModifiko = gvNjesiAdm.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim';
            $('#hfId')[0].value = 0; pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function kontrolloKodMagazine() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Celje", "kontrolloEkzistonKodObjekti"), data: JSON.stringify({ kodi: txtKodi.GetText(), idObjekti: $('#hfId').val(), idndermarje: hfState.Get("idNdermarrje"), idPerdorues: hfState.Get("idPerdorues"), kategoria: "23" })
    }).done(SucceededCallbackKontrolloKod);
}

function SucceededCallbackKontrolloKod(result) {
    if (result) {
        myMesazh.ShtoMesazhGabimi("Ekziston nje magazine me kete kod!");
        txtKodi.GetMainElement().style.borderColor = 'red'
    }
    else {
        txtKodi.GetMainElement().style.borderColor = 'green'
    }
}