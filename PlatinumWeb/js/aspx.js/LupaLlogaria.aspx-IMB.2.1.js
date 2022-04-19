function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaLlog, "628", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload', function () {
});

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        //gvLupaLlog.SetFocusedRowIndex(0);
        //gvLupaLlog.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaLlog.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaLlog.GetVisibleRowsOnPage() - 1) {
            gvLupaLlog.SetFocusedRowIndex(0);
        }
        else {
            gvLupaLlog.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaLlog.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaLlog.GetSelectedFieldValues('NrLlogari;EmerLlogari1;KodiMonedha;IdLlogari;EmerLlogari2;PershkrimiMonedha;IdKategoriShpenzimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var grida = window.parent.$('#rowed5');

    if (Utils.getUrlVar('komponentenga') === 'Shto_KomponentePage1') {
        window.parent.cmbLlogDebi.SetSelectedIndex(window.parent.cmbLlogDebi.AddItem(values[0][0], values[0][3]));
        window.parent.cmbLlogDebi.Focus();
    }
    else if (Utils.getUrlVar('komponentenga') === 'Shto_KomponentePage2') {
        window.parent.cmbLlogKredi.SetSelectedIndex(window.parent.cmbLlogKredi.AddItem(values[0][0], values[0][3]));
        window.parent.cmbLlogKredi.Focus();
    }
    else if (Utils.getUrlVar('vjennga') === "KonfigPash") {
        window.parent.editorGlobal.val(values[0][0]);
        window.parent.editorPershkrimillogaria.val(values[0][1]);
        window.parent.editorGlobal.focus();
    }
    else if (Utils.getUrlVar('vjennga') === "LlogArt") {
        var idRow = grida.getLastSel2();
        if ((typeof (window.parent.vjenNgaKokeApoTrup) !== "undefined") && window.parent.vjenNgaKokeApoTrup == true) {
            window.parent.btneArtikulli.SetText(values[0][0]);
            window.parent.LostFocusObjekti('txtArtikulli', window.parent.btneArtikulli);
            window.parent.$('#txtArtikulli' + idRow).focus();
        }
        else {
            window.parent.$('#txtArtikulli' + idRow).val(values[0][0]);
            window.parent.$('#txtArtikulli' + idRow).focus();
        }
    }
    else if (Utils.getUrlVar('vjennga') === "Analiza") {
        var idRow = grida.getLastSel2();
        if (window.parent.varKonfig.kontrolloPerKategoriShpenzimi && values[0][6] == undefined) {
            window.parent.mesazhGabimiLlogariPaKategoriShpenzimi(values[0][0]);
        } else {
            window.parent.$('#txtAnalize' + idRow).val(values[0][0]);
            window.parent.$('#txtObjekti' + idRow).val(values[0][1]);
            window.parent.$('#txtAnalize' + idRow).focus();

        }
    }
    else if (Utils.getUrlVar('vjennga') === "NjesiVartese") {
        window.parent.cmbLlogari.SetText(values[0][0]);
        window.parent.cmbLlogari.SetFocus(true);
    }
    else if (Utils.getUrlVar('vjennga') === "Burime") {
        window.parent.cmbLlog.SetText(values[0][0]);
        window.parent.cmbLlog.SetFocus(true);
    }
    else if (Utils.getUrlVar('vjenNga') === "Shto_Punonjes") {
        window.parent.cmbIdLlogari.SetText(values[0][0]);
        window.parent.cmbIdLlogari.SetFocus(true);
    }
    else if (Utils.getUrlVar('vjennga') === "RegjistrimQendraKostoGrida" || Utils.getUrlVar('vjennga') === "RegjistrimQendraKostoGridaRe") {
        window.parent.editorKodi.SetText(values[0][0]);
        window.parent.editorEmertimi.SetText(values[0][1]);
        try { window.parent.editorMonedha.SetText(values[0][2]); }
        catch (eee) {
        }
        window.parent.editorKodi.SetFocus(true);
    }
    else if (Utils.getUrlVar('vjennga') === "RegjistrimQendraKostoDXDATAGRID") {
        window.parent.regjQK.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat(0, values[0][3], 0);
    }
    else if (window.parent.arrLL != null) {
        window.parent.editorGlobal.val(values[0][0]);
        window.parent.editorPershkrimillogaria.val(values[0][1]);
        window.parent.editorGlobal.focus();
    }
    else if (window.parent.arrLlogari != null) {//rasti kur thiret nga fleta kontabel
        var index = grida.getLastSel2();
        var idKontrolli = "#txtNrllogarie" + index;
        window.parent.selectFunc(null, null, idKontrolli, values[0][3], values[0][0]);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.gridabanka == true) {//rasti kur thiret nga grida e bankave
        window.parent.editorLlogari.SetValue(values[0][0]);
        window.parent.editorMonedha.SetText(values[0][2]);
        window.parent.editorLlogari.SetFocus(true);
    }
    else if (window.parent.gridabanka == false) {//rasti kur thiret nga bankat
        if (window.parent.komisionApoLlogari == 1) {//nqs popup u hap nga NrLlogari
            window.parent.txtNrLlogari.SetText(values[0][0]);
            window.parent.cmbMonedha.SetEnabled(true);
            window.parent.cmbMonedha.SetText(values[0][5]);
            window.parent.cmbMonedha.SetEnabled(false);
            window.parent.txtNrLlogari.SetFocus(true);
        }
        else if (window.parent.komisionApoLlogari == 2) {// nqs popup u hap nga fusha Komisioni
            window.parent.komisioni_ButtonEdit.SetText(values[0][0]);
        }
    }
    else if (window.parent.identikuesPerPopupLlogari == "SkemaKontabel") {
        window.parent.editorGlobal.SetText(values[0][0]);
        window.parent.editorGlobal.Focus();
    }
    else if (window.parent.identikuesPerPopupLlogari == "Vitet") {
        window.parent.txtLlogMbylljeViti.SetText(values[0][0]);
        window.parent.txtLlogMbylljeViti.Focus();
    }
    else if (window.parent.identikuesPerPopupLlogari == "AzhornimKlientFurnitoriLlogariKredi") {
        window.parent.llogariKredi_ButtonEdit.SetText(values[0][0]);
        window.parent.llogariKredi_ButtonEdit.Focus(); window.parent.VendosKredi();
    }
    else if (window.parent.identikuesPerPopupLlogari == "AzhornimKlientFurnitoriLlogariDebi") {
        window.parent.llogariDebi_ButtonEdit.SetText(values[0][0]);
        window.parent.llogariDebi_ButtonEdit.Focus(); window.parent.vendosDebi();
    }
    else if (window.parent.identikuesPerPopupLlogari == "AzhornimKlientFurnitoriLlogariNgaGrida") {
        window.parent.editorGlobal.value = values[0][0];
        window.parent.editorGlobal.focus();
    }
    else if (window.parent.identikuesPerPopupLlogari == "VeprimeKF") {
        window.parent.cmbLlogariKunderParti.SetText(values[0][0]);
        window.parent.cmbLlogariKunderParti.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupLlogari == "Magazina") {
        window.parent.cmbLlogariKunderParti.SetText(values[0][0]);
        window.parent.cmbLlogariKunderParti.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupLlogari == "VeprimeKFgrida") {
        window.parent.editorGlobal.val(values[0][0]);
        window.parent.editorGlobal.focus();
    }
    else if (window.parent.identikuesPerPopupLlogari == "Shto_Monedhe") {
        if (window.parent.fitim == "fitim") {
            window.parent.editorLlogFitimi.SetText(values[0][0]);
            window.parent.editorLlogFitimi.SetFocus(true);
        }
        else {
            window.parent.editorLlogHumbje.SetText(values[0][0]);
            window.parent.editorLlogHumbje.SetFocus(true);
        }
    }
    else if (window.parent.identifikuesPerPopupLlogari == "Modifiko_Monedhe") {
        if (window.parent.fitim == "fitim") {
            window.parent.btneLlogFitimi.SetText(values[0][0]);
            window.parent.btneLlogFitimi.SetFocus(true);
        }
        else {
            window.parent.btneLlogHumbje.SetText(values[0][0]);
            window.parent.btneLlogHumbje.SetFocus(true);
        }
    }
    else if (window.parent.identifikuesPerPopupLlogari == "Taksa") {
        if (window.parent.fitim == "debi") {
            window.parent.cmbLlogDebi.SetText(values[0][0]);
            window.parent.cmbLlogDebi.SetFocus(true);
        }
        else if (window.parent.fitim == "kredi") {
            window.parent.cmbLlogKredi.SetText(values[0][0]);
            window.parent.cmbLlogKredi.SetFocus(true);
        }
        else {
            window.parent.cmbLlogDog.SetText(values[0][0]);
            window.parent.cmbLlogDog.SetFocus(true);
        }
    }
    else if (window.parent.identifikuesPerPopupLlogari == "KonfigurimDokumentash") {
        window.parent.editorLL.SetValue(values[0][3]);
        window.parent.editorLL.SetText(values[0][0]);
        window.parent.editorLL.SetFocus();
    }
    else if (window.parent.identifikuesPerPopupLlogari == "KonfigurimDokumentashArketim") {
        window.parent.editorLL.AddItem(values[0][0], values[0][3]);
        window.parent.editorLL.SetText(values[0][0]);
        window.parent.editorLL.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupLlogari == "KonfigurimDokumentashBuxhetim") {
        var kodi = values[0][0];
        if (values.length > 1) {
            for (i = 1; i < values.length; i++)
                kodi = kodi + "," + values[i][0];
        }
        window.parent.editorLL.SetText(kodi);
        window.parent.editorLL.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupLlogari == "Import") {
        window.parent.editorLL.SetValue(values[0][0]);
        window.parent.editorLL.SetFocus();
    }
    else if (window.parent.identifikuesPerPopupLlogari == "ImportPerfitimBuxheti") {
        window.parent.editorLL.SetValue(values[0][0]);
        window.parent.editorLL.SetFocus();
    }
    else if (window.parent.identikuesPerPopupLlogari == "RaporteKontabiliteti") {
        window.parent.editorGlobal.SetText(values[0][0]);
    }
    else if (window.parent.identifikuesPerPopupLlogari == "KlientFurnitor") {
        window.parent.editorLlogaria.SetText(values[0][0]);
        if (window.parent.editorPershkrimi != "") {
            window.parent.editorPershkrimi.SetText(values[0][5]);

        } else window.parent.nrLlogariParaChange();
        window.parent.editorLlogaria.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopup == "Shto_KlientFurnitor") {
        window.parent.editorLlogaria.SetText(values[0][0]);
        window.parent.editorLlogaria.SetFocus(true);
        window.parent.editorPershkrimi.SetText(values[0][5]);
    }
    else if (window.parent.identifikuesPerPopupLlogari == "raportllogariafillim") {
        var kodi = values[0][0];
        if (values.length > 1) {
            for (i = 1; i < values.length; i++)
                kodi = kodi + "," + values[i][0];
        }
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupLlogari == "Modifiko_KF") {
        window.parent.editorGlobal.SetText(values[0][0]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupLlogari == "VeprimeBanka") {
        if (window.parent.comboKreditet == true) {
            window.parent.kredite_ButtonEdit.SetText(values[0][0]);
            window.parent.comboKreditet = false;
        }
        else {
            window.parent.vendosSubjektNgaLupa(values, "Llogari");
        }
    }
    else if (window.parent.identikuesPerPopupLlogari == "LidhjaDokumentave") {
        window.parent.llogari_ButtonEdit.SetText(values[0][0]);
        window.parent.llogari_ButtonEdit.Focus();
    }
    else if (window.parent.identikuesPerPopupLlogari == "AgjenteShitje") {
        if (window.parent.grida == true) {
            window.parent.IdLlogari.SetText(values[0][0]);
            window.parent.IdLlogari.SetFocus(true);
        }
        else {
            window.parent.txtLlogari.SetText(values[0][0]);
            window.parent.txtLlogari.SetFocus(true);
        }
    }
    else if (window.parent.identikuesPerPopupLlogari == "RegjistrimQendraKostoDXDATAGRID") {
        window.parent.regjQK.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat(0, values[0][3], 0);
    }
    else if (window.parent.identikuesPerPopupLlogari == "ShperndarjeShpenzimesh") {
        window.parent.VendosLlogariNeGride(values[0][0], values[0][3], values[0][1]);
    }
    else if (window.parent.identikuesPerPopupLlogari == "ArtikullPerberes") {
        window.parent.editorKodi.SetText(values[0][0]);
        window.parent.editorEmertimi.SetText(values[0][1]);
        window.parent.arr[1][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + values[0][0];
        window.parent.arr[2][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + values[0][1];
        window.parent.arr[5][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + values[0][3];
    }
    else if (window.parent.identikuesPerPopupLlogari == "RegjistrimDokumentashKodi") {

        var index = grida.getLastSel2();
        var idKontrolli = "#txtKodi" + index;
        window.parent.$(idKontrolli).val(values[0][0]);
        window.parent.$(idKontrolli).change();
        window.parent.$(idKontrolli).focus();
        idKontrolli = "#txtPershkrimi" + index;
        if (window.parent.pershk == 1)
            window.parent.jQuery(idKontrolli).val(values[0][1]);
        else
            window.parent.jQuery(idKontrolli).val(values[0][4]);
    }
    else if (window.parent.identikuesPerPopupLlogari == "RegjistrimDokumentashLlogShpenzimi") {

        var index = grida.getLastSel2();
        var idKontrolli = "#txtIdLlogShpenzimi" + index;
        window.parent.$(idKontrolli).val(values[0][0]);
        window.parent.$(idKontrolli).change();
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identifikuesPerPopupLlogari == "KategoriBuxhetimi") {
        Utils.ShtoNeseNukGjendetDheSelektoCombo(window.parent.btneLlogaria, values[0][3], new Array(values[0][0], values[0][1]));
        window.parent.btneLlogaria.Focus();
    }
    else if (window.parent.identifikuesPerPopupLlogari == "PerfitimBuxhetimi") {
        window.parent.VendosLlogariArtikullNeTrup(values[0][0], values[0][3], values[0][1]);
    }
    else {
        var kushLlog = window.parent.txtLlog.GetText();
        switch (kushLlog) {//rasti kur thirret nga llogaria
            case 'kons': window.parent.llogKonsoliduese_TextBox.SetText(values[0][0]);
                break;
            case 'korr': window.parent.llogKorresponduese_TextBox.SetText(values[0][0]);
                break;
            case 'fillpop': window.parent.editorGlobal.SetText(values[0][0]);
                window.parent.editorGlobal.SetFocus();
                break;
            case 'lk': window.parent.txtLlogKons.SetText(values[0][0]);
                break;
            //rastet kur thiret nga artikuli    
            case 'inv': Utils.SelectComboItem(window.parent.btneLlogInv, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'ble': Utils.SelectComboItem(window.parent.btneLlogBle, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'shit': Utils.SelectComboItem(window.parent.btneLlogShit, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'tretet': Utils.SelectComboItem(window.parent.btneLlogTretet, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'shpe': Utils.SelectComboItem(window.parent.btnLlogShpe, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'amor': Utils.SelectComboItem(window.parent.cmbLlogAmortizimi, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'pakesim': Utils.SelectComboItem(window.parent.btnLlogPakesim, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'rez': Utils.SelectComboItem(window.parent.btneLlogRez, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'pakr': Utils.SelectComboItem(window.parent.btneLlogPakesimRez, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            case 'kom': Utils.SelectComboItem(window.parent.btnLLogariKomisioni, values[0][3], values[0][0]);
                window.parent.btneSkema.SetSelectedIndex(-1);
                break;
            default:
        }
    }

    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {

    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;

        case "Shto":
            e.processOnServer = false;
            if (window.parent.identikuesPerPopupLlogari == "RegjistrimDokumentash") {
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("lupaShtoLlogari"), 'LupaLlogariShpejte.aspx?vjenNga=Shto_RegjistrimDokumentash', 900, 600);
            }
            else {               
                ruajNeSesionUrlDheHapLupen(false);
            }
            break;
        case "Klono":
            e.processOnServer = false;
            gvLupaLlog.GetRowValues(gvLupaLlog.GetFocusedRowIndex(), 'NrLlogari;EmerLlogari1;KodiMonedha;IdLlogari;EmerLlogari2;PershkrimiMonedha', klonoLlog);
            break;
    }
}

function ruajNeSesionUrlDheHapLupen(klono, kodi) {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ruajNeSessionURLllog"),
        data: JSON.stringify({ url: window.location.href })
    }).done(function () {
        var headerText = klono ? hfState.Get("headerPopUpKlonoLlogari") : hfState.Get("lupaShtoLlogari");
        //window.parent.gridabanka = true eshte rasti kur thiret nga bankat
        var queryString = klono ? '?klonim=true&kodi=' + kodi : (window.parent.gridabanka ? '?vjenNga=Shto_Banka' : '');
        var url = 'LupaLlogariShpejte.aspx' + queryString;
        window.parent.myButtonClickLupa.LupaUniversal_Click(headerText, url, 850, 600);
    });
}

function klonoLlog(result) {
    ruajNeSesionUrlDheHapLupen(true, result[0]);
}

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaLlog.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaLlog.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');