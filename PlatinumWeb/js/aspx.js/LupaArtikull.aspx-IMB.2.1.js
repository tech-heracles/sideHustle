;
var identifikuesPerPopupMagazina = 'LupaArtikull';
var identifikuesPerPopupKodifikimin = "LupaArtikull";

function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaArtikull, "610", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).load(function () {
    try {
        gvLupaArtikull.SetWidth(document.documentElement.clientWidth - 20);
        Init();
    }
    catch (e) {
    }
});


$(window).bind('resize', function () {
    try {
        gvLupaArtikull.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
}).trigger('resize');

//$(window).on('unload', function () {

//});

function Init() {
    try {
        if (window.parent.window.location.href.search('LupaArtikullShpejte.aspx') != -1)
            ASPxMenu1.GetItemByName('Shto').SetVisible(false);
    }
    catch (err) {
    }
    myFaqeCelje.shtoHandlerSession();
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idGjuha = hfState.Get('idGjuha');
    var kodKonfigLupa = hfState.Get('kodKonfigLupa');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({
            idKomp: 610, kodKonf: kodKonfigLupa, idNdermarrje: idNdermarrje, idGjuha: idGjuha
        })
    }).done(SucceededCallbackKonfig);
}

document.onkeydown = ProcessKeyPress;

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

var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    $("#dvArtikulli").show();
    PershkrimArtikulli.Focus();
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        hfMod.val("shtim");
        var hfLidhur = $("#hfLidhur");
        hfLidhur.value = false;
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPanel", undefined, hfLidhur);
        if (cbGjendje.GetVisible() || cbKosto.GetVisible()) {
            lblMagazina.SetVisible(true);
            btneMagazina.SetVisible(true);
            if (Utils.getUrlVar('idMag') !== '' && Utils.getUrlVar('idMag') !== "undefined" && Utils.getUrlVar('idMag') != -1) {
                var idMagazina = parseInt(Utils.getUrlVar('idMag'));
                btneMagazina.SetValue(idMagazina);
            }
        }
        else {
            lblMagazina.SetVisible(false);
            btneMagazina.SetVisible(false);
        }
        var hfMag = $("#hfLupaMagazina");
        var hfKod1 = $("#hfLupaKodifikim1");
        var hfKod2 = $("#hfLupaKodifikim2");
        var hfKod3 = $("#hfLupaKodifikim3");
        for (var i = 0; i < colKontrollet.length; i++) {
            if (colKontrollet[i].KodKontrolli == "btneMagazina")
                hfMag.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            else if (colKontrollet[i].KodKontrolli == "hfLupaKodifikim1")
                hfKod1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            else if (colKontrollet[i].KodKontrolli == "hfLupaKodifikim2")
                hfKod2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            else if (colKontrollet[i].KodKontrolli == "hfLupaKodifikim3")
                hfKod3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}

function OnGridSelectionChanged() {
    if (gvLupaArtikull.GetFocusedRowIndex() == 0)
        gvLupaArtikull.SelectRowOnPage(0)
    if (gvLupaArtikull.GetFocusedRowIndex() == -1 && gvLupaArtikull.GetSelectedRowCount() == 0)
        return;
    gvLupaArtikull.GetSelectedFieldValues('KodArtikulli;PershkrimArtikulli;PershkrimiAngArtikulli;Kodbari;IdArtikulli;DetajimArtikulli;PershkrimNjesia1;PershkrimNjesia2;Njesi1Artikulli;Njesi2Artikulli;LlojiArt;Klasa;IdLlogariBlerje;IdLlogariTeTrete;Kodifikimi1Artikulli', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values.length == 0) {
        alert(hfState.Get("msgSelektoniNjeRresht"));
        return;
    }
    var s = new String();
    s = s + values[0];
    var vl = s.split(",");
    var kodi = vl[0];
    if (window.parent.identikuesPerPopupArtikulli == 'artikull') {
        window.parent.editorKod.SetText(vl[0]);
        window.parent.editorEmertimiA.SetText(vl[1]);
        window.parent.editorKod.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'Planifikim') {
        var artikulli = {
            KodArtikulli: vl[0],
            PershkrimArtikulli: vl[1],
            IdArtikulli: vl[4],
            PershkrimNjesia1: vl[6],
            PershkrimNjesia2: vl[7],
            Njesi1Artikulli: vl[8],
            Njesi2Artikulli: vl[9],
            Klasa: vl[11]
        }
        window.parent.vendosArt(artikulli, window.parent.pageState.rreshtIndex);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'GjeneroProjekt') {
        window.parent.btneArtikulli.SetText(vl[0]);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'NdryshimCmimi') {
        window.parent.cmbArtikulli.SetText(vl[0]);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'EkzekutimProdhimi') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtKodiArtikull" + index;
        var kodi;
        kodi = vl[0];
        window.parent.selectFunc(null, null, idKontrolli, vl[4], kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'SkedulimProdhimi') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtProdukti" + index;
        var kodi;
        kodi = vl[0];
        window.parent.selectFunc3(null, null, idKontrolli, vl[4], kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'Receptura') {
        var grida = window.parent.$('#rowed6');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtKodiArtikullR" + index;
        var kodi;
        kodi = vl[0];
        window.parent.selectFunca(null, null, idKontrolli, vl[4], kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'Rivleresim') {
        for (i = 1; i < values.length; i++)
            kodi = kodi + "," + values[i][0];
        window.parent.cmbArtikuj.SetText(kodi);
        window.parent.cmbArtikuj.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'raportArtikull') {
        for (i = 1; i < values.length; i++)
            kodi = kodi + "," + values[i][0];
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'raportPershArt') {
        window.parent.editorGlobal.SetText(vl[1]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'raportArtikullBurim') {
        for (i = 1; i < values.length; i++)
            kodi = kodi + "," + values[i][0];
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'Makro') {
        window.parent.editorProdukti.value = vl[0];
        window.parent.editorProdukti.focus();
        window.parent.editorPershkrimi.SetText(vl[1]);
        window.parent.editorPershkrimi.SetEnabled(false);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'Cmim Artikulli') {
        if (window.parent.artikulli == 'kodi')
            window.parent.btneKodi.SetText(vl[0]);
        else if (window.parent.artikulli == 'emertimi1')
            window.parent.btneEmertimi1.SetText(vl[1]);
        else if (window.parent.artikulli == 'kodbari')
            window.parent.btneKodbari.SetText(vl[3]);
        else if (window.parent.artikulli == 'emertimi2')
            window.parent.btneEmertimi2.SetText(vl[2]);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'Zbritje Analitike') {
        if (window.parent.artikulli == 'kodi')
            window.parent.btneKodi.SetText(vl[0]);
        else if (window.parent.artikulli == 'emertimi1')
            window.parent.btneEmertimi1.SetText(vl[1]);
        else if (window.parent.artikulli == 'kodbari')
            window.parent.btneKodbari.SetText(vl[3]);
        else if (window.parent.artikulli == 'emertimi2')
            window.parent.btneEmertimi2.SetText(vl[2]);
    }
    else if (window.parent.identikuesPerPopupArtikulli == "Import") {
        window.parent.editorGlobal.SetText(vl[0]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'ArtikullPerberes') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht; //$("#rowed", window.parent.document).getLastSel2();
        var idKontrolli = "#txtKodi" + index;
        var kodi;
        kodi = vl[0];

        window.parent.selectFunc(null, null, idKontrolli, vl[4], kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.pageState && window.parent.pageState.identifikuesPopUp && window.parent.pageState.identifikuesPopUp == 'RegjistrimDokumentash') {
        window.parent.pageState.vendosKodNgaLupa({ idKodi: values[0][4], kodi: vl[0] });
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'RegjistrimMagazine') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtKodi" + index;
        var kodi;
        if (window.parent.kodkodbar == undefined || window.parent.kodkodbar == 1)
            kodi = vl[0];
        else
            kodi = vl[3];
        var idArt = values[0][4];
        window.parent.selectFunc(null, null, idKontrolli, idArt, kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'RegjistrimMagazineSet') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtIdArtikullSet" + index;
        var kodi = vl[0];

        var idArt = values[0][4];
        window.parent.selectFunc4(null, null, idKontrolli, idArt, kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'RegjistrimNdryshimCmimSasi') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtKodi" + index;
        var kodi;
        if (window.parent.kodkodbar == undefined || window.parent.kodkodbar == 1)
            kodi = vl[0];
        else
            kodi = vl[3];
        var idArt = values[0][4];
        window.parent.selectFunc(null, null, idKontrolli, idArt, kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'RivleresimeAmortizimi') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtKodi" + index;
        var kodi;
        if (window.parent.kodkodbar == undefined || window.parent.kodkodbar == 1)
            kodi = vl[0];
        else
            kodi = vl[3];
        var idArt = values[0][4];
        window.parent.selectFunc(null, null, idKontrolli, idArt, kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'ShperndarjeShpenzimesh') {
        window.parent.btnArtikulli.SetText(vl[0]);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'RegjistrimRezervimi') {
        var grida = window.parent.$('#rowed5');
        var idRresht = grida.getLastSel2();
        var index = idRresht;
        var idKontrolli = "#txtKodi" + index;
        var kodi;
        kodi = vl[0];
        window.parent.selectFunc(null, null, idKontrolli, vl[4], kodi);
        window.parent.$(idKontrolli).focus();
    }
    else if (window.parent.identikuesPerPopupArtikulli == "RecetaOptike") {
        if (vl[10] === "1") {
            myMesazh.ShtoMesazhGabimi("Nuk mund te kryeni veprime me artikujt afatgjate!");
            return;
        }
        window.parent.editorArt.SetValue(vl[4])

    }
    else if (window.parent.identikuesPerPopupArtikulli == 'LidhArtikull') {
        window.parent.cmbLidhMeNdermRap.SetText(vl[0]);
        window.parent.cmbLidhMeNdermRap.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "ShtoKartaKlientiLupaArtikull") {
        window.parent.VendosArtikull(vl[4], vl[0], vl[15]);
    }
    else if (window.parent.identikuesPerPopupArtikulli == 'AD') {
        window.parent.editorAD.AddItem(kodi, vl[4]);
        window.parent.editorAD.SetText(kodi);
        window.parent.editorAD.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopup == "PerfitimBuxheti") {
        window.parent.VendosLlogariArtikullNeTrup(values[0][0], values[0][4], values[0][1], values[0][12], values[0][13]);
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    if (e.item.name == "Anullo") {
        e.processOnServer = false;
        window.parent.popupUniversal.Hide();
    }
    if (e.item.name == "Kerko") {
        e.processOnServer = false;
        Utils.RaiseCustomCallbackFiltrimi(gvLupaArtikull);
    }
    if (e.item.name == 'Shto') {
        e.processOnServer = false;
        if (window.parent.pageState && window.parent.pageState.identifikuesPopUp && window.parent.pageState.identifikuesPopUp == 'RegjistrimDokumentash') {
            window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikull"), 'LupaArtikullShpejte.aspx?vjenNga=Shto_RegjistrimDokumentash&veprimi=shtim&llojiart=afatshkurter', 1100, 600);
        }
        else if (window.parent.pageState && window.parent.pageState.identifikuesPopUp && window.parent.pageState.identifikuesPopUp == 'RegjistrimMagazine')
            window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikull"), 'LupaArtikullShpejte.aspx?vjenNga=Shto_RegjistrimMagazina&veprimi=shtim&llojiart=afatshkurter', 1100, 600);
        else if (window.parent.identikuesPerPopupArtikulli == "RegjistrimNdryshimCmimSasi")
            window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikull"), 'LupaArtikullShpejte.aspx?vjenNga=Shto_RegjistrimNdryshimCmimSasi&veprimi=shtim&llojiart=afatshkurter', 1100, 600);

        else if (window.parent.identikuesPerPopupArtikulli == 'Planifikim')
            window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikull"), 'LupaArtikullShpejte.aspx?vjenNga=Shto_Planifikim&veprimi=shtim&llojiart=afatshkurter', 1100, 600);



        else {
            $.ajax({ url: Utils.getServerApiUrl("Autorizime", "ruajNeSessionURLART"), data: JSON.stringify({ url: window.location.href }) }).done(function () { Succeded(true) });
        }
    }
    if (e.item.name == 'ShtoArtAqt') {
        e.processOnServer = false;
        switch (window.parent.identikuesPerPopupArtikulli) {
            case "RegjistrimDokumentash":
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikullAqt"), 'LupaArtikullShpejte.aspx?vjenNga=Shto_RegjistrimDokumentash&veprimi=shtim&llojiart=aqt', 1100, 600);
                break;
            case "RegjistrimMagazine":
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikullAqt"), 'LupaArtikullShpejte.aspx?vjenNga=Shto_RegjistrimMagazina&veprimi=shtim&llojiart=aqt', 1100, 600);
                break;
            case "RegjistrimNdryshimCmimSasi":
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikullAqt"), 'LupaArtikullShpejte.aspx?vjenNga=Shto_RegjistrimNdryshimCmimSasi&veprimi=shtim&llojiart=aqt', 1100, 600);
                break;
            default:
                $.ajax({ url: Utils.getServerApiUrl("Autorizime", "ruajNeSessionURLART"), data: JSON.stringify({ url: window.location.href }) }).done(function () { Succeded(false) });
                break;
        }
    }
    else if (e.item.name == "Klono") {
        e.processOnServer = false;
        if (gvLupaArtikull.GetFocusedRowIndex() == -1) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSelektoniNjeRresht"));
            return;
        }
        gvLupaArtikull.GetRowValues(gvLupaArtikull.GetFocusedRowIndex(), 'KodArtikulli;LlojiArt;IdArtikulli', klonoArt);
    }
}

function klonoArt(result) {
    var qs = '?veprimi=klonim&kodArt=' + result[0];
    if (result[1] == 1)
        qs += '&llojiart=aqt';
    else
        qs += '&llojiart=afatshkurter';
    qs += '&idartikulli=' + result[2];
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpKlonoArtikull"), 'LupaArtikullShpejte.aspx' + qs, 1100, 600);
    $.ajax({ url: Utils.getServerApiUrl("Autorizime", "ruajNeSessionURLART"), data: JSON.stringify({ url: window.location.href }) }).done(SuccededArt);
}
function SuccededArt()
{ }

function Succeded(afatshkurter) {
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikull"), 'LupaArtikullShpejte.aspx?veprimi=shtim&llojiart=' + (afatshkurter ? 'afatshkurter' : 'aqt'), 1100, 600);
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerShtoFilter"), 'LupaFiltra.aspx?grida=gvLupaArtikull&page=LupaArtikull.aspx&idKonfigAmbjente=577', 850, 450);
    popFiltra.Show();
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

//function MerrGjendjeKosto() {
//    var mag = -1;
//    if (btneMagazina.GetText() !== '')
//        mag = btneMagazina.GetValue();
//    Utils.RaiseCustomCallbackFiltrimi(gvLupaArtikull);
//}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

/*
Function: ButtonClickMagazina
    
Hap lupen e magazinave.
*/
function ButtonClickMagazina() {//po
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhMagazinen"), 'LupaMagazina.aspx?idKonfigAmbjente=' + $('#hfLupaMagazina').val(), 600, 560);
}

function RuajFilterGrida() {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "RuajFilterGrida"),
        data: JSON.stringify({ ruajFilter: cbRuajFilter.GetChecked(), filterExpression: gvLupaArtikull.cpFilterExpression, idGrida: 'gvLupaArtikull', idNdermarrje: hfState.Get('idNdermarrje')})
    }).done(function (result) {   });
}

function KodifikimArtikulli_Click(llojKodifikimi) {
    switch (llojKodifikimi) {
        case 1:
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + llojKodifikimi + '&llojartikulli=false&idKonfigAmbjente=' + $("#hfLupaKodifikim1").val(), 600, 560);
            break;
        case 2:
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + llojKodifikimi + '&llojartikulli=false&idKonfigAmbjente=' + $("#hfLupaKodifikim2").val(), 600, 560);
            break;
        case 3:
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + llojKodifikimi + '&llojartikulli=false&idKonfigAmbjente=' + $("#hfLupaKodifikim3").val(), 600, 560);
            break;
    }      
}
