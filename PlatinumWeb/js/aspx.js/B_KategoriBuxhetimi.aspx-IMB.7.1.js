;

var mbush = true;
var kaloTab = false;
var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var resultkonf;
var colKontrollet, colAtrTrupi;
var indexModifiko;
var identifikuesPerPopupLlogari = "KategoriBuxhetimi";
var identifikuesPerPopup = "KategoriBuxhetimi";

$(window).on("load", function () {
    bejGatiCmbBuxheti();
    Init();
    changeName();
});

function Init() {
    myMesazh.shtoHandler();
}

function changeName() {
    _idKomponente = hfState.Get("_idKomponente");
    _idGjuha = hfState.Get("_idGjuha");
    _idNdermarrje = hfState.Get("_idNdermarrje");

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName("B_KategoriBuxhetimi.aspx?lupe=" + Utils.getUrlVar("lupe"), 0, hf);
    percaktoMenuSipasTabitDheTeDrejta(0);
}
function percaktoMenuSipasTabitDheTeDrejta(tabIndex) {
    myMenu.PercaktoMenuSipasTabit(tabIndex, hfTeDrejta, $("#hfShtimModifikim"));
    var btnFshi = ASPxMenu1.GetItemByName('Fshi');
    var btnFshiPrind = ASPxMenu1.GetItemByName('FshiPrind');
    if (hfTeDrejta.Get('Fshi') == true) {
        if (btnFshi && btnFshi != null) btnFshi.SetEnabled(true);
        if (btnFshiPrind && btnFshiPrind != null) btnFshiPrind.SetEnabled(true);
    }
    else {
        if (btnFshi && btnFshi != null) btnFshi.SetEnabled(false);
        if (btnFshiPrind && btnFshiPrind != null) btnFshiPrind.SetEnabled(false);
    }
}
function menu_click(s, e) {
    var hfShtimModifikim = $("#hfShtimModifikim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $("#hfId"); //hidden fieldi qe ruan id  e rreshtit te selektuar
    var indexModifiko = gvKategoriBuxhetimi.GetFocusedRowIndex();
    switch (e.item.name) {
        case "Ruaj":
            if (!validoFusha(s,e)) return;
            Utils.vendosVlereNeHiddenField("cmbBuxhetiHf", cmbBuxheti.GetValue());
            break;
        case "ShtoPrind":
            myMenu.ShtoClick(e, hfShtimModifikim, hfId, PageControl1, undefined, indexModifiko, pastroFusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
            hfShtimModifikim.val("shtimPrindi");
            break;
        case "ModifikoPrind":
            myMenu.ModifikoClick(e);
            hfShtimModifikim.val("modifikimPrindi");
            break;
        case "FshiPrind":
            hfState.Set("fshiPrindi",true);
            break;
        case "Fshi":
            hfState.Set("fshiPrindi", false);
            break;
        case "OK":
            e.processOnServer = false;
            ZgjidhRreshtaNgaLupa();
            return;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            return;
        default:
            myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl1, false, undefined, indexModifiko, pastroFusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
            break;
    }
    if (e.item.name == "Shto" || e.item.name == "ShtoPrind" || e.item.name == "Modifiko" || e.item.name == "ModifikoPrind")
        enableDisableBtnePrindi(hfShtimModifikim);
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(";")[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(";")[0]);
    callWebserviceKonfigurimi(cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimi(kodKonf) {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: _idKomponente, kodKonf: kodKonf, idNdermarrje: _idNdermarrje, idGjuha: _idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(kodKonf) {
    callWebserviceKonfigurimi(kodKonf);
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null && !hfState.Get("lupe")) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        var hf = $("#hfKontrollet");
        var hfMod = $("#hfShtimModifikim");
        var arrTabela = ["tblInformacion"];
        vendosKushteNeHfState(result.colKushte, result.colAlterKusht);
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, "PageControl1_C");
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
    }
    Utils.hiqLoadingGif();
    enableDisableBtnePrindi($("#hfShtimModifikim"));
}

function vendosKushteNeHfState(colKushte, colAlterKusht) {
    if (colKushte && colKushte.length > 0) {
        var stateName;
        for (var i = 0; i < colKushte.length; i++) {
            switch (colKushte[i].Kodi) {
                case "SHIPSHKB":
                    stateName = "shfaqKontrollePerNdermBije";
                    break;
                case "SHIPSHKP":
                    stateName = "shfaqKontrollePerNdermPrind";
                    break;
                case "IKKTB":
                    stateName = "integroKategoriTeNdermBija";
                    break;
                default:
                    stateName = "";
                    break;
            }
            if (!stateName)
                continue;
            hfState.Set(stateName, colAlterKusht.filter(function(alternativa){return alternativa.IdKushti == colKushte[i].IdKusht})[0].Alternativa.toLowerCase() == "po");
        }
    }
}

function validoFusha(s,e) {
    myFaqeCelje.valido(s, e, PageControl1, hfTeDrejta, $('#hfShtimModifikim'));
    if (txtKoeficenti.GetText() && isNaN(parseFloat(txtKoeficenti.GetText()))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiKoeficenti"));
        e.processOnServer = false;
        return false;
    }
    return true;
}
function onActiveTabChanged(s, e) {
    hapRreshtPerModifikim();
    kaloTab = false; //ishte false
    percaktoMenuSipasTabitDheTeDrejta(PageControl1.GetActiveTabIndex());

}


function enableDisableBtnePrindi(hfShtimModifikim){
    if (hfShtimModifikim.val() == "shtimPrindi" || hfShtimModifikim.val() == "modifikimPrindi") {
        btnePrindi.validationGroup = "entries1";
        btnePrindi.SetEnabled(false);
        return;
    }
    btnePrindi.validationGroup = "entries";
    btnePrindi.SetEnabled(true);
}

function hapRreshtPerModifikim() {
    var indexModifiko = gvKategoriBuxhetimi.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick();
        }
        else {
            mbush = false;
            var veprimShtimi = (hfState.Get("shfaqKontrollePerNdermBije") == false && hfState.Get("shfaqKontrollePerNdermPrind") == true) ? "shtimPrindi" : "shtim";
            $("#hfShtimModifikim")[0].value = veprimShtimi;
            $("#hfId")[0].value = 0;
            pastroFusha();
            enableDisableBtnePrindi($("#hfShtimModifikim"));
        }
    }
}

function OnGridDoubleClick() {
    if (hfState.Get("lupe")) {
        ZgjidhRreshtaNgaLupa();
        return;
    }
    mbushfusha();
}

function mbushfusha() {
    mbush = false;
    var veprimModifikimi = (hfState.Get("shfaqKontrollePerNdermBije") == false && hfState.Get("shfaqKontrollePerNdermPrind") == true) ? "modifikimPrindi" : "modifikim";
    $("#hfShtimModifikim")[0].value = veprimModifikimi;
    enableDisableBtnePrindi($("#hfShtimModifikim"));
    var indexModifiko = gvKategoriBuxhetimi.GetFocusedRowIndex();
    if (indexModifiko === -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiZgjidhPerModifikim"));
    else {
        GetRowValues(indexModifiko);
        Utils.shfaqLoadingGif();
    }

}

function GetRowValues(index) {
    gvKategoriBuxhetimi.GetRowValues(index, "IdKategoriBuxhetimi;Kodi;Pershkrimi;NivelKategorie;IdPrindi;Buxheti;Koeficenti;IdLlogaria;Aktive", function (values) {
        aktivizoFusha(colKontrollet, colAtrTrupi);
        var veprimi = $("#hfShtimModifikim").val();
        if (veprimi == "shtim" || veprimi == "shtimPrindi")
            return;
        $("#hfId").val(values[0]);
        txtKodi.SetValue(values[1]);
        txtEmertimi.SetValue(values[2]);
        txtNiveli.SetValue(values[3]);

        if (values[6] && values[6]>0)
            txtKoeficenti.SetValue(values[6]);
        else
            txtKoeficenti.SetValue(null);

        var merrPrind = false;
        var merrLlogari = false;
        if (values[3] > 1) {
            if (values[4] > 0)
                merrPrind = true;
            else
                btnePrindi.SetSelectedIndex(-1);
        }
        else {
            btnePrindi.SetEnabled(false);
            btnePrindi.SetSelectedIndex(-1);
        }

        if (values[7] > 0) {
            merrLlogari = true;
        }
        else
            btneLlogaria.SetSelectedIndex(-1);

        cbAktive.SetValue(values[8]);

        if (kaloTab) {
            PageControl1.SetActiveTabIndex(1);
            percaktoMenuSipasTabitDheTeDrejta(1);
        }

        if (merrLlogari || merrPrind)
            callWebServicePrindDheLlogari(values[4], values[7]);

        vendosLlojeBuxhetiNeCombo(values[0], values[5], false);

    });
}

function callWebServicePrindDheLlogari(idPrindi, idLlogari) {
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "KtheLlogariDhePrindSipasIds"),
        data: JSON.stringify({ idPrindi: idPrindi, idLlogari: idLlogari })
    }).done(function (result) {
        if(result.llogaria.IdLlogari > 0)
            Utils.ShtoNeseNukGjendetDheSelektoCombo(btneLlogaria, result.llogaria.IdLlogari, new Array(result.llogaria.NrLlogari, result.llogaria.EmerLlogari1));
        if (result.prindi.IdKategoriBuxhetimi > 0)
            Utils.ShtoNeseNukGjendetDheSelektoCombo(btnePrindi, result.prindi.IdKategoriBuxhetimi, new Array(result.prindi.Kodi, result.prindi.Pershkrimi));
        Utils.hiqLoadingGif();
    });
}

function callWebServiceLlojeBuxheti(idKategoriBuxhetimi) {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "KtheLlojeBuxhetiSipasIdKategoriBuxhetimi"),
        data: JSON.stringify({ idKategoriBuxhetimi: idKategoriBuxhetimi })
    }).done(SucceededCallbackLlojeBuxheti);
}

function SucceededCallbackLlojeBuxheti(result) {
    cmbBuxheti.SetValue(result.LlojeBuxheti.map(function(a){return a.IdLlojBuxheti}));
    Utils.hiqLoadingGif();
}

function bejGatiCmbBuxheti() {
    var options = JSON.parse(hfState.Get("colBuxheti"));
    //hfState.Remove("colBuxheti");
    cmbBuxheti = new MultiSelect({
        container: "tblInformacion",
        valueField: "IdLlojBuxheti",
        labelField: "Kodi",
        searchField: ["Kodi","Pershkrimi"],
        options: options,
        meLupe: true,
        onButtonClickLupa: Buxheti_Click,
        multiSelectId: "cmbBuxheti"
    });
}

function Buxheti_Click() {
    popupUniversal.SetHeaderText("Zgjidh Llojin e Buxhetit");
    popupUniversal.SetContentUrl('B_LlojeBuxheti.aspx');
    popupUniversal.SetSize(1000, 600);
    popupUniversal.Show();
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $("#hfShtimModifikim");
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, "#PageControl1_C");
}

function pastroFusha() {
    $("#hfId").val(0);
    txtKodi.SetValue("");
    txtEmertimi.SetValue("");
    txtNiveli.SetValue(1);
    txtKoeficenti.SetValue(null);
    btnePrindi.SetSelectedIndex(-1);
    btneLlogaria.SetSelectedIndex(-1);
    cmbBuxheti.SetValue(null);
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    cbAktive.SetValue(true);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl1, hfTeDrejta, $("#hfShtimModifikim"));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $("#hfKontrollet");
    var hfShtimModifikim = $("#hfShtimModifikim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $("#hfId");  //hidden fieldi qe ruan id  e rreshtit te selektuar

    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl1, gvKategoriBuxhetimi, _idKomponente, pastroFusha, hfTeDrejta, undefined, undefined, false);

    percaktoMenuSipasTabitDheTeDrejta(PageControl1.GetActiveTabIndex());
    shtoHiqKategoriBuxhetimiNgaComboPrindi();

    if (hfState.Get("ruajtjeFiltri") && btnFiltrat) {
        btnFiltrat.PerformCallback();
    }
    hfState.Set("ruajtjeFiltri", false);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKategoriBuxhetimi, _idKomponente, cmbKonfigurimi.GetText());
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

function ButtonClickPrindi() {
    popupUniversal.SetHeaderText(hfState.Get("msgKategoriBuxhetimiLupaName"));
    popupUniversal.SetContentUrl('B_KategoriBuxhetimi.aspx?lupe=true');
    popupUniversal.SetSize(1000, 600);
    popupUniversal.Show();
}


function ZgjidhRreshtaNgaLupa() {
    gvKategoriBuxhetimi.GetSelectedFieldValues('IdKategoriBuxhetimi;NivelKategorie;IdLlogaria;Buxheti;Kodi;Pershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(value) {
    if (value.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiZgjidhPrind"));
        return;
    }
   
    switch (window.parent.identifikuesPerPopup) {
        case "KategoriBuxhetimi":
            var hide = vendosKategoriPrindBuxheti(value);
            if (!hide)
                return;
            break;
        case "RialokimBuxheti":
            if (value.length > 1) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhVetemNjeArtikullBuxhetimiNgaLista"));
                return;
            }
            window.parent.VendosKategoriBuxhetiNeTrup(value[0][0], value[0][4], value[0][5]);
            break;
        case "RaportiRialokimidheEkzekutimiBuxhetitQeveritar":
                var kodet = Utils.ktheVleratESelektuaraTeBashkuara(value, 4, ",")
                window.parent.editorGlobal.SetValue(kodet);
                window.parent.editorGlobal.SetFocus(true);
            break;
        case "Import": {
            if (value.length > 1) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhVetemNjeArtikullBuxhetimiNgaLista"));
                return;
            }
            window.parent.editorGlobal.SetValue(value[0][4]);
            window.parent.editorGlobal.SetFocus();
        }
        default:
            break;
    }
    window.parent.popupUniversal.Hide();
}

function vendosKategoriPrindBuxheti(value) {
    if (value.length > 1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiNukZgjedhMeShumeSeNjePrind"));
        return false;
    }
    var IdKategoriBuxhetimi = value[0][0];
    var NivelKategorie = parseInt(value[0][1]);
    var IdLlogaria = value[0][2];
    var Buxheti = value[0][3];

    if (IdKategoriBuxhetimi == parseInt(window.parent.$("#hfId").val())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiPrindVetvetja"));
        return false;
    }

    if (IdLlogaria && IdLlogaria > 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiNukZgjedhPrindMeLlogari"));
        return false;
    }

    if (window.parent.btnePrindi.GetText() != '') {
        window.parent.btnePrindi.SetSelectedIndex(-1);
        window.parent.btnePrindi.Focus();
    }
    Utils.ShtoNeseNukGjendetDheSelektoCombo(window.parent.btnePrindi, IdKategoriBuxhetimi, new Array(value[0][4], value[0][5]));
    window.parent.txtNiveli.SetValue(NivelKategorie + 1);
    window.parent.vendosLlojeBuxhetiNeCombo(IdKategoriBuxhetimi, Buxheti, true);
    return true;
}

function SelectedIndexChangedPrindi() {
    if (!(parseInt(btnePrindi.GetValue()) == parseInt($("#hfId").val())))
        callWebServicektheKategoriBuxhetimiPrind(parseInt(btnePrindi.GetValue()));
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiPrindVetvetja"));
        btnePrindi.SetSelectedIndex(-1);
    }
}

function ButtonClickLlogaria() {
    var kontrollLlogaria = colKontrollet.filter(function (item) { return item.KodKontrolli == "btneLlogaria" })[0];
    if (kontrollLlogaria) {
        atributeLlogaria = colAtrTrupi.filter(function (item) { return item.IdKontroll == kontrollLlogaria.IdKontrolli })[0];
        var idKonfigLupa = atributeLlogaria ? atributeLlogaria.IdKonfigAmbjenteLupa : 0;
        myButtonClickLupa.ButtonClickLlogaria('Zgjidh Llogarine', idKonfigLupa, 750, 600);
    }
}


function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function callWebServicektheKategoriBuxhetimiPrind(idKategoriBuxhetimi) {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "KtheKategoriBuxhetimiSipasIdKategoriBuxhetimi"),
        data: JSON.stringify({ idKategoriBuxhetimi: idKategoriBuxhetimi })
    }).done(SucceededCallbackKategoriBuxhetimiPrind);
}

function SucceededCallbackKategoriBuxhetimiPrind(result) {
    var clsKategoriBuxhetimi = result.clsKategoriBuxhetimi;
    if (clsKategoriBuxhetimi.IdLlogaria && clsKategoriBuxhetimi.IdLlogaria > 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiNukZgjedhPrindMeLlogari"));
        btnePrindi.SetSelectedIndex(-1);
        return;
    }
    btnePrindi.SetValue(clsKategoriBuxhetimi.IdKategoriBuxhetimi);
    txtNiveli.SetValue(clsKategoriBuxhetimi.NivelKategorie + 1);

    vendosLlojeBuxhetiNeCombo(clsKategoriBuxhetimi.IdKategoriBuxhetimi, clsKategoriBuxhetimi.Buxheti, true);

}

function btnePrindiLostFocus(s, e) {
    if(btnePrindi.GetSelectedIndex() < 0)
    {
        cmbBuxheti.SetEnabled(true);
        txtNiveli.SetValue(1);
    }
}

function vendosLlojeBuxhetiNeCombo(IdKategoriBuxhetimi, Buxheti, ngaPrindi) {
    if (txtNiveli.GetValue() > 1)
        cmbBuxheti.SetEnabled(false);
    else
        cmbBuxheti.SetEnabled(true);

    if (Buxheti) {
        callWebServiceLlojeBuxheti(IdKategoriBuxhetimi);
    }
    else {
        cmbBuxheti.SetValue(null);
        Utils.hiqLoadingGif();
    }
}

function shtoHiqKategoriBuxhetimiNgaComboPrindi() {
    if (!hfState.Get("editedKategoriBuxhetimi")) return;
    var editedKategoriBuxhetimi = JSON.parse(hfState.Get("editedKategoriBuxhetimi"));
    if (!editedKategoriBuxhetimi) return;
    var kategoriBuxhetimi;
    var existentKategori;
    for (var i = 0; i < editedKategoriBuxhetimi.kategoriBuxhetimi.length; i++) {
        kategoriBuxhetimi = editedKategoriBuxhetimi.kategoriBuxhetimi[i];
        existentKategori = btnePrindi.FindItemByValue(kategoriBuxhetimi.IdKategoriBuxhetimi);
        if (existentKategori)
            btnePrindi.RemoveItem(existentKategori.index);
        if (editedKategoriBuxhetimi.veprimi == "shto")
            btnePrindi.AddItem(new Array(kategoriBuxhetimi.Kodi, kategoriBuxhetimi.Pershkrimi), kategoriBuxhetimi.IdKategoriBuxhetimi);
    }
    hfState.Set("editedKategoriBuxhetimi", null);
};

function shtoHiqBuxheteNgaComboBuxheti(unparsedObjectBuxheti) {
    var editedLlojeBuxheti = JSON.parse(unparsedObjectBuxheti);
    if (!editedLlojeBuxheti) return;

    var llojBuxheti;
    var existentBuxheti;
    for (var i = 0; i < editedLlojeBuxheti.llojeBuxheti.length; i++) {
        llojBuxheti = editedLlojeBuxheti.llojeBuxheti[i];
        existentBuxheti = cmbBuxheti.FindOptionByValue(llojBuxheti.IdLlojBuxheti);
        if (existentBuxheti)
            cmbBuxheti.RemoveOption(existentBuxheti.IdLlojBuxheti);
        if (editedLlojeBuxheti.veprimi == "shto")
            cmbBuxheti.AddOption(llojBuxheti);
    }
};

function SetBtnFiltraValue(s, e) {
    cmbfiltra.SetValue(s.GetValue());
}