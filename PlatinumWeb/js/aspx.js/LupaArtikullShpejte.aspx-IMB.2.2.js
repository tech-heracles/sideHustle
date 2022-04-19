;
var widthLupaKodbare = 500;
var heightLupaKodbare = 500;
var widthLupaSkema = 600;
var heightLupaSkema = 500;
var widthLupaKodifikime = 500;
var heightLupaKodifikime = 500;
var widthLupaAutorizime = 600;
var heightLupaAutorizime = 600;
var widthLupaLlogaria = 750;
var heightLupaLlogaria = 600;
var grida;
var KPF;

$(window).on("load", function () {
    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblInformacion",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"
    });
    Init();
});


$(document).keydown(function (e) {//po
    if (e.which == 13) {
        e.preventDefault();
    }
});

$(window).bind('resize', function () {//po
    var grida = $('#rowed5');
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth(grida, $('#divgride2'));
    }
}).trigger('resize');

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

var formatNumriCmimesh;

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGrideCmimesh() {
    formatNumriCmimesh = $.parseJSON(hfState.Get("formatMonedheCmimi"));
    ndryshoKonfigFormatNumriCmimesh(formatNumriCmimesh);
    vendosKonfigFormatNumriCmimesh();
    return;
}


function ndryshoKonfigFormatNumriCmimesh(formatNumriCmimesh) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumriCmimesh == 'undefined')
        formatNumriCmimesh = $.parseJSON(hfState.Get("formatMonedheCmimi"));
    else
        hfState.Set("formatMonedheCmimi", JSON.stringify(formatNumriCmimesh));
    arrformatesasia = $.parseJSON(hfState.Get('formatisasi'));
    arrformatecmimi = $.parseJSON(hfState.Get('formaticmim'));
    arrformatekursi = $.parseJSON(hfState.Get('formatikursi'));
}

/*
Formaton vlerat e fushave sipas formatit perkates ne te gjithe rreshtat e grides
*/
function vendosKonfigFormatNumriCmimesh() {
    formatoFushaDevi();
}

function formatoFushaDevi() {
    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1) ; i++) {
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('CmimiTvsh' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2Tvsh' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMin' + i), arrformatesasia[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMax' + i), arrformatesasia[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Kursi' + i), arrformatekursi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Kosto' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMin' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMax' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kursi' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kosto' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('CmimiTvsh' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));
        }
    else {
        for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMin' + i), arrformatesasia[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMax' + i), arrformatesasia[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Kursi' + i), arrformatekursi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Kosto' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('CmimiTvsh' + i), arrformatecmimi[i]);
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2Tvsh' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMin' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMax' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kursi' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kosto' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('CmimiTvsh' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));
        }
    }
}

function gjejFormatSipasMonedhes(idmonedha) {
    var formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    for (i = 0; i < formatNumri.KonfigTrupi.length; i++)
        if (formatNumri.KonfigTrupi[i].IdMonedha == idmonedha) {
            return formatNumri.KonfigTrupi[i]
        }
    return formatNumri.KonfigTrupi[0];
}

var arrformatevlefta = new Array();
var arrformatesasia = new Array();
var arrformatecmimi = new Array();
var arrformatekursi = new Array();

function unformatoFushaDevi() {
    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1)) {
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1) ; i++) {
            arrformatecmimi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Cmimi' + i));
            arrformatesasia[i] = Utils.getFormatNumri(Utils.ktheKontroll('SasiMin' + i));
            arrformatekursi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Kursi' + i));
        }
    }
    else {
        for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            arrformatecmimi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Cmimi' + i));
            arrformatesasia[i] = Utils.getFormatNumri(Utils.ktheKontroll('SasiMin' + i));
            arrformatekursi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Kursi' + i));
        }
    }
}


function textChangedKodbari(s, e) {
    //si ka qene
    //    var hf = hfKodbar.Get('kod');
    //    var listeFushash = hf.split(',');
    //    var gja = listeFushash[0].length;
    //    hf = '0:' + btneKodbari.GetText() + hf.substring(gja, hf.length);
    //    hfKodbar.Set('kod', hf);
    //end 'si ka qene'

    //var kodbaret = JSON.parse($('#hfKodbaret').val());
    //kodbaret[0] = { indeksi: '0', pershkrimi: kodbaretArr[j] };
    var kodbaret = btneKodbari.GetText().split(',');
    var ArrayKodbaret = new Array();
    for (var i = 0; i < kodbaret.length; i++) {
        if (kodbaret[i] == '')
            continue;
        var barkodi = kodbaret[i].replace(/\s/g, '');
        ArrayKodbaret[i] = new Object();
        ArrayKodbaret[i] = { indeksi: i, pershkrimi: barkodi, njesia: 1, detajimi1: 0, detajimi2: 0 };
    }
    $('#hfKodbaret').val(JSON.stringify(ArrayKodbaret));
}

function Succeded(result) {
    window.location = result;
    if (window.parent.ASPxGridView_Artikull)
        window.parent.ASPxGridView_Artikull.PerformCallback();
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    var hfRuaj = $('#hfRuaj');
    if (e.item.name == 'Ruaj') {
        Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
        if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
            e.processOnServer = false;
            return;
        }
        if (cbIRimbursueshem.GetChecked() && (Utils.IsNullOrEmpty(txtKodiIBarit.GetText()) || Utils.IsNullOrWhiteSpace(txtKodiIBarit.GetText()))) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("MsgNdaluesPlotesoKodinEBarit"));
            e.processOnServer = false;
            return;
        }
        if (btneKodbari.GetText().indexOf('+') != -1) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZevendesimPlusi"));
            e.processOnServer = false;
            return;
        }

        Utils.shfaqLoadingGif();;
        valido(s, e);

        if (hfTeDrejtaCmimi.Get("Amb") == true) {
            merrTeDhenat();
        }
        merrTeDhenaNorma();
        grida.jqGrid('saveRow', idRresht, false, 'clientArray');
        grida.setLastSel2(0);
        if (cmbKlasa.GetValue() == 4 || cmbKlasa.GetValue() == 5 || cmbKlasa.GetValue() == 6)
            merrTeDhenaArt(s, e);

        pastro();
    }
    else if (e.item.name == 'Arkiva') {
        ButtonClickArkiva();
        e.processOnServer = false;
    }
    else if (e.item.name == 'Anullo') {
        if (Utils.getUrlVar("vjenNga") == undefined) {
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLART"),
                data: JSON.stringify({})
            }).done(Succeded);
            return;
        }
        window.parent.popupUniversal.Hide();
        e.processOnServer = false;
    }
}

function ButtonClickArkiva() {
    //indexModifiko = ASPxGridView_Artikull.GetRowKey(ASPxGridView_Artikull.GetFocusedRowIndex())
    var idArt = $('#hfId')[0].value;
    //if(typeof(idArt) == 'undefined' || idArt <= 0)
    //    myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje artikull!');
    //else {
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=ListaShpejte&veprimi=artikull&idDok=' + idArt
        //+ '&shtim_modifikim=' + $('#hfShtimModifikim').val()
        );
    popupUniversal.Show();
    //}
}

function cmbKlasaIndexChanged(s, e) {
    cbProdhimMePorosi.SetChecked(false);
    cbRezervueshem.SetChecked(false);
    var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true');
    enable(true, isLidhur);
    btneLlogInv.SetSelectedIndex(-1);
    btneLlogTretet.SetSelectedIndex(-1);
    btnLlogShpe.SetSelectedIndex(-1);
    cmbLlogAmortizimi.SetSelectedIndex(-1); btnLlogPakesim.SetSelectedIndex(-1);
    btneLlogBle.SetSelectedIndex(-1);
    btneLlogShit.SetSelectedIndex(-1);
    btneSkema.SetSelectedIndex(-1);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: " ", idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje })
    }).done(SucceededCallbackKonfig);
}
function btneSkemaTextChanged(s, e) {
    var selectedSkema = btneSkema.GetSelectedItem();
    if (selectedSkema != null) {
        Utils.SelectComboItem(btneLlogInv, null, selectedSkema.GetColumnText('NrLlogariInventari'));
        Utils.SelectComboItem(btneLlogTretet, null, selectedSkema.GetColumnText('NrLlogariTekTeTretet'));
        Utils.SelectComboItem(btnLlogShpe, null, selectedSkema.GetColumnText('NrLlogariShpenzimi'));
        Utils.SelectComboItem(btneLlogBle, null, selectedSkema.GetColumnText('NrLlogariBlerje'));
        Utils.SelectComboItem(btneLlogShit, null, selectedSkema.GetColumnText('NrLlogariShitje'));
        Utils.SelectComboItem(cmbLlogAmortizimi, null, selectedSkema.GetColumnText('NrLlogariAmortizimi'));
        Utils.SelectComboItem(btnLlogPakesim, null, selectedSkema.GetColumnText('NrLlogariPakesimi'));
    }
}
var mbushtedhenasipagrupit = false;
var resultkonf;
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {

    var grida = $("#rowed5");
    formatNumriZgjedhur = result.formatNumri;    //[7];
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    ruajFormatetNeGrideCmimesh();
    $("#dvArtikulli").show();//[0].style.visibility = 'visible'; $("#dvArtikulli")[0].style.display = '';
    if (result != "" && result != null) {
        colKontrollet = result.colKontroll;   //[0];
        colAtrTrupi = result.colAtrTrupi;       //[1];
        colKushte = result.colKushte;     //[3];
        colAlterKusht = result.colAlterKusht;              //[4];
        var kodniveli = result.kodniveli;    //[5];

        resultkonf = result;

        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblInformacion'];
        var veprimi = Utils.getUrlVar("veprimi");
        var kodArtikulli = Utils.getUrlVar("kodArt");
        hfMod.val(veprimi);
        checkKontrollGjendjeArtikulli.SetChecked(true);
        if (hfMod.val() === "shtim")
            cmbNivelTvsh.SetSelectedIndex(-1);
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPanel", undefined, hfLidhur);
        if (hfMod.val() === "shtim" || hfMod.val() === "klonim") {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
        if (cmbNjesia1.GetText() === "")
            cmbNjesia1.SetSelectedItem(cmbNjesia1.FindItemByText('cope'));
        if (cmbNjesia2.GetText() === "")
            cmbNjesia2.SetSelectedItem(cmbNjesia2.FindItemByText('cope'));
        if (btneSkema.GetText() !== "") {
            var hfs = $('#hfSkema');
            hfs.val(btneSkema.GetText());
        }
    }
    for (j = 0; j < colKushte.length; j++) {
        var kusht = colKushte[j];
        var alterKushti = colAlterKusht[j];
        if (kusht.Kodi == 'NSLKDG') {
            if (alterKushti.Alternativa == "Po") {
                mbushtedhenasipagrupit = true;

            }
            else {
                mbushtedhenasipagrupit = false;

            }
        }
    }

    if (hfMod.val() == 'shtim')
        enable(true, false);

    mbushGrideNgaHiddenFieldet();
    enableDetajim();
}
function LostFocusKodifikim(s) {
    var kod = Utils.findInArray(colAtrTrupi, function (item) { return item.IdKontroll == Utils.findInArray(colKontrollet, function (item) { return item.KodKontrolli == s.globalName }).IdKontrolli });
    if (s.GetValue() == "0" && kod.Detyrueshme)
        s.SetValue(null);
}


function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());

}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaKodbari");
    var hf2 = $("#hfLupaKodifikim1");
    var hf3 = $("#hfLupaKodifikim2");
    var hf33 = $("#hfLupaKodifikim3");
    var hf4 = $("#hfLupaAutorizimi");
    var hf5 = $("#hfLupaFurnitori");
    var hf7 = $("#hfLupaDetajimi");
    var hf8 = $("#hfLupaSkema");
    var hf9 = $("#hfLupaLlogInv");
    var hf10 = $("#hfLupaLlogBle");
    var hf11 = $("#hfLupaLlogShit");
    var hf12 = $("#hfLupaLlogTretet");
    var hf13 = $("#hfLupaLlogShpe");
    var hf14 = $("#hfLupaArtikuj");
    var hf15 = $("#hfLupaArtikujPerberes");
    var hf16 = $("#hfMetoda");
    var hf17 = $("#hfLupaLlogAmortizimi");
    var hf18 = $("#hfLupaMagazina");
    var hf19 = $("#hfLupaLlogPakesim");
    for (var i = 0; i < kontrollet.length; i++) {

        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (kontrollet[i].KodKontrolli == "btneKodbari") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneKodifikimi1") {
            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneKodifikimi2") {
            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneKodifikimi3") {
            hf33.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbMetode") {
            hf16.val(colAtrTrupi[i].VlereDefault);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
            hf4.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "txtFurnitori") {
            hf5.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneSkema") {
            hf8.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneLlogInv") {
            hf9.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneLlogBle") {
            hf10.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneLlogShit") {
            hf11.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneLlogTretet") {
            hf12.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnLlogShpe") {
            hf13.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnArtikuj") {
            hf14.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnArtikujPerberes") {
            hf15.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbLlogAmortizimi") {
            hf17.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnMagazina") {
            hf18.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnLlogPakesim") {
            hf19.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnDetajimi1") {
            hf7.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue
        }
    }
}


function ndryshoKonfigurimin() {
    if (cmbKonfigurimi.GetSelectedItem() === null || cmbKonfigurimi.GetSelectedItem() === undefined) return;
    btneLlogInv.SetSelectedIndex(-1);
    btneLlogTretet.SetSelectedIndex(-1);
    btnLlogShpe.SetSelectedIndex(-1);
    cmbLlogAmortizimi.SetSelectedIndex(-1); btnLlogPakesim.SetSelectedIndex(-1);
    btneLlogBle.SetSelectedIndex(-1);
    btneLlogShit.SetSelectedIndex(-1);
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    callWebserviceKonfigurimi(441, cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimi(441, cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

var identifikuesPerPopupKodifikimin = "LupaArtikull";
var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupArtikulli = 'artikull';
var identifikuesPerSkemat = "shtim";
var identifikuesPerPopupMagazina = "ShtoArtikull";
var identifikuesPerPopupAutorizime = 'LupaArtShpejt';
var identifikuesPerPopupKodbare = 'LupaArtShpejt';

function enable(krijoGride, isLidhur) {
    var grida = $('#rowed5');
    var enableGrida = false;
    var veprimi = $('#hfShtimModifikim').val();
    if (veprimi == "klonim")
        isLidhur = false;
    if (cmbKlasa.GetValue() == 5) {
        cbProdhimMePorosi.SetVisible(true);
        lblProdhimMePorosi.SetVisible(true);
        if (veprimi == 'shtim')
            cbProdhimMePorosi.SetChecked(true);
    }
    else {
        cbProdhimMePorosi.SetVisible(false);
        lblProdhimMePorosi.SetVisible(false);
    }
    if (cmbKlasa.GetValue() == 5 || cmbKlasa.GetValue() == 1) {
        cbRezervueshem.SetEnabled(true);
    }
    else {
        cbRezervueshem.SetEnabled(false);
    }
    var gridInaktive = false;
    if (Utils.getUrlVar('llojiart') == 'afatshkurter') {
        switch (parseInt(cmbKlasa.GetValue())) {
            case 5: //prodhim            
                enableGrida = true;
                if (!isLidhur) {
                    btneLlogInv.SetEnabled(true);
                    btneLlogBle.SetEnabled(false);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(false);
                    btnLlogShpe.SetEnabled(true);
                }
                break;
            case 6: //prodhim ne proces            
                enableGrida = true;
                if (!isLidhur) {
                    btneLlogInv.SetEnabled(true);
                    btneLlogBle.SetEnabled(false);
                    btneLlogShit.SetEnabled(false);
                    btneLlogTretet.SetEnabled(false);
                    btnLlogShpe.SetEnabled(true);
                }
                break;
            case 1:
                if (!isLidhur) {
                    btneLlogInv.SetEnabled(true);
                    btneLlogBle.SetEnabled(true);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(true);
                    btnLlogShpe.SetEnabled(false);
                }
                break;
            case 2:
                if (!isLidhur) {
                    btneLlogInv.SetEnabled(false);
                    btneLlogBle.SetEnabled(true);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(false);
                    btnLlogShpe.SetEnabled(false);
                }
                break;
            case 3:
                if (!isLidhur) {
                    btneLlogInv.SetEnabled(false);
                    btneLlogBle.SetEnabled(true);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(false);
                    btnLlogShpe.SetEnabled(false);
                }
                break;
            case 4: //i perbere
                if (!isLidhur) {
                    btneLlogInv.SetEnabled(false);
                    btneLlogBle.SetEnabled(false);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(false);
                    btnLlogShpe.SetEnabled(false);
                }
                enableGrida = true;
                if (isLidhur) gridInaktive = true;
                break;
            default:
                break;
        }
    }
    else if (Utils.getUrlVar('llojiart') == 'aqt') {
        cbProdhimMePorosi.SetVisible(false);
        lblProdhimMePorosi.SetVisible(false);
        cbRezervueshem.SetEnabled(false);
        cbRezervueshem.SetChecked(false);
        enableGrida = false;
    }
    if (krijoGride) {
        grida.setLastSel2(-1);
        grida.jqGrid('GridUnload', "rowed5");
        formGridColsArray(gridInaktive);
        ruajFormatetNeGride(grida);
        inicializoGride(gridInaktive);
    }
    if (enableGrida) {
        $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible';

        if ($('#divgride2').width() != null) {

            myJQGrid.fixGridWidth($('#rowed5'), $('#divgride2'));
        }
    }
    else
        $("#divgride1").hide();//$("#divgride1")[0].style.visibility = 'hidden';
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.shtoHandlerSession();

    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
    enableDetajim();
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi")[0];
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim    
    if (hf.value == "true") {
        suksesRuajtjeArtikulli();
        hf.value = "false";
    }
    Utils.hiqLoadingGif();;
}

function suksesRuajtjeArtikulli() {
    var vjenNga = Utils.getUrlVar("vjenNga");
    if (Utils.IsUndefined(vjenNga)) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLART"),
            data: JSON.stringify({})
        }).done(Succeded);
        return;
    }
    var veprimi = Utils.getUrlVar('veprimi');
    var selektorGride = '#rowed5';
    if (veprimi == 'modifikim')
        modifikoArtikullNeGride(vjenNga, selektorGride);
    else
        shtoArtikullTeRiNeGride(vjenNga, selektorGride);
}

function modifikoArtikullNeGride(vjenNga, selektorGride) {
    var kodArtikulli = Utils.getUrlVar('kodArt');
    if (Utils.IsNullOrEmpty(kodArtikulli))
        return;

    var grida = window.parent.$(selektorGride);
    var idRow = grida.getLastSel2();
    var idPerdoruesi = window.parent.hfState.Get('idPerdoruesi');
    var detajim = Utils.IsNullOrEmpty(window.parent.$('#txtDetajimi' + idRow).val()) ? -1 : window.parent.$('#txtDetajimi' + idRow).val();
    window.parent.callWebserviceArtikulliIPlote({ idArt: -1, idRresht: idRow, detajim: detajim, magazine: "", kodKodbarArt: kodArtikulli, peshoreArt: "", ngaLupa: true, sasiaNeGride: new Object() });
}

function shtoArtikullTeRiNeGride(vjenNga, selektorGride) {
    if (vjenNga == 'Shto_Planifikim')
        mbushArtikullPerTaShtuarNeGride(undefined, vjenNga, undefined);
    else {
        var grida = window.parent.$(selektorGride);
        var rreshtILire = psonisRreshtBoshNeGride(vjenNga, selektorGride, grida);
        if (rreshtILire == -1)
            return;
        mbushArtikullPerTaShtuarNeGride(rreshtILire, vjenNga, grida);
    }
}

function psonisRreshtBoshNeGride(vjenNga, emerGride, grida) {
    var fushaIdentity = myJQGrid.ktheFushaIdentity(emerGride);
    var fusheKontrolli = fushaIdentity.fusheKontrolli, fusheNdihmese = fushaIdentity.fusheNdihmese, vlereFusheNdihmese = "Artikull";
    var rreshtILire = grida.gjeRreshtBosh(fusheKontrolli, fusheNdihmese, vlereFusheNdihmese);
    if (rreshtILire == -1) {
        rreshtILire = grida.shtoRresht(false, emerGride);
        grida.setLastSel2(-1);
    }
    grida.rregulloNrRendor();
    grida.setTekstQelize(fusheNdihmese, rreshtILire, vlereFusheNdihmese);
    return rreshtILire;
}

function mbushArtikullPerTaShtuarNeGride(rreshtILire, vjenNga, grida) {
    switch (vjenNga) {
        case "Shto_RegjistrimDokumentash":
            var idPerdoruesi = window.parent.hfState.Get('idPerdoruesi');
            var idNdermarrje = window.parent.hfState.Get('idNdermarrje');
            window.parent.callWebserviceArtikulliIPlote({ idArt: -1, idRresht: rreshtILire, detajim: -1, magazine: "", kodKodbarArt: txtKodi.GetText(), peshoreArt: "", ngaLupa: true, listeIMEIArtikull: new Array(), sasiaNeGride: new Object() });
            break;
        case "Shto_RegjistrimMagazina":
            var magazine = '', magazineDest = '';
            var magazinatKoka = window.parent.merrMagazinatKoka();
            var merrMagMeAutorizim = window.parent.hfState.Get("merrMagazinatMeAutorizim") == "Po";
            window.parent.callWebserviceMerrArtikullMeKodOseKodbar({ kodi: txtKodi.GetText(), index: rreshtILire, date: window.parent.dteDtDok.GetDate(), iddetajim: -1, magazina: magazine, magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim, idMagazina: 0, meDetajim: false, magazinaDest: magazineDest, ngaLupa: true });
            break;
        case "Shto_RegjistrimNdryshimCmimSasi":
            var magazine = grida.getTekstQelize('txtMagazina', rreshtILire);
            var magazineDest = grida.getTekstQelize('txtMagazina2', rreshtILire);
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodOseKodBar"),
                data: JSON.stringify({ kodi: txtKodi.GetText(), index: rreshtILire, date: window.parent.dteDtDok.GetDate(), iddetajim: -1, magazina: magazine, idMagazina: 0, meDetajim: false, magazinaDest: magazineDest, idNdermarrje: window.parent.hfState.Get('idNdermarrje'), idPerdorues: window.parent.hfState.Get('idPerdoruesi'), merrSipasDetajimit: false, njesiDef: 1, sasiaNeRresht: 1, merrCmim: false })
            }).done(function (result) { doneFunctionArtikull(result, grida); });
            break;
        case "Shto_RegjistrimRezervimi":
            var myDate = new Date();
            myDate.setDate(myDate.getDate() + 365 * 2);
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtRezMeKodOseKodBar"),
                async: false,
                data: JSON.stringify({ kodi: txtKodi.GetText(), rreshti: rreshtILire, data: myDate.toLocaleString(), iddetajim: -1, idmag: 0, idNdermarrje: window.parent.hfState.Get('idNdermarrje'), idPerdoruesi: window.parent.hfState.Get('idPerdoruesi') })
            }).done(function (result) { doneFunctionArtikull(result, grida); });
            break;
        case "Shto_Planifikim":
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullSipasKodit"),
                async: false,
                data: JSON.stringify({ kodi: txtKodi.GetText(), merrfurnitor: false, idNdermarrje: window.parent.hfState.Get('idNdermarrje'), merrDetajime: false })
            }).done(function (result) { window.parent.vendosArt(result.artikulli, true) });
            break;
    }
}

function doneFunctionArtikull(result, grida) {
    window.parent.SucceededCallbackArtNew(result);
    grida.jqGrid("setSelection", result[0]);
    window.parent.popupUniversal.Hide();
}

function vendosArtNgaLupa(result) {
    window.parent.vendosArtNgaLupa(result);
}

function kodeTeNjejta(rowID, kodi) {
    var grida = window.parent.$("#rowed5");
    var idRow = grida.getLastSel2();
    //ky funksion kthen nje array me id-te e rreshtave qe kane kodin e njejte si kodi i selektuar ne gride qe po modifikojme.
    var gridIds = grida.jqGrid('getDataIDs'); //marrim nr e rreshtave
    var idMeKodNjesoj = new Array(); //ketu ruhen id-te e rreshtave qe kane kodin njesoj me kodin tone.
    var j = 0; //indeksi qe do perdorim per array ku do ruhen id-te e rreshtave.
    idMeKodNjesoj[j] = rowID; //shtojme fillimisht id e rreshtit te selektuar
    var vjenNga = Utils.getUrlVar("vjenNga");
    var kodiGride;
    var llojiGride;
    for (i = 0; i < gridIds.length; i++) {
        var rreshti = grida.jqGrid('getRowData', [gridIds[i]]); //marrim te dhenat e rreshtit me id perkatese.
        if (gridIds[i] === rowID) //nqs eshte rreshti i selektuar nuk duhet ta shtojme, sepse e kemi shtuar nje here ne fillim.
            continue;
        if (rreshti.txtKodi.toString().search('value') != -1) {
            kodiGride = window.parent.$("#txtKodi" + idRow).val();
            if (vjenNga == "Shto_RegjistrimDokumentash")
                llojiGride = window.parent.$('#cmbLloji' + idRow + ' option:selected').text();
            else if (vjenNga == "Shto_RegjistrimMagazina")
                llojiGride = window.parent.$('#txtKategoria' + idRow + ' option:selected').text();
        }
        else {
            kodiGride = rreshti.txtKodi;
            if (vjenNga == "Shto_RegjistrimDokumentash")
                llojiGride = rreshti.cmbLloji;
            else if (vjenNga == "Shto_RegjistrimMagazina")
                llojiGride = rreshti.txtKategoria;
        }

        if (kodiGride === kodi && kodiGride !== '' && llojiGride === 'Artikull') {//nqs kodi eshte i njejte dhe lloji eshte artikull atehere shtojme id e rreshtit tek array.
            j++;
            idMeKodNjesoj[j] = gridIds[i];
        }
    }
    return idMeKodNjesoj;
}

function mbushFushat(values) {
    var veprimi = Utils.getUrlVar("veprimi");
    $('#hfShtimModifikim').value = veprimi;
    var artikulli = values.artikulli;
    var detajime = values.detajime;
    $('#hfId')[0].value = artikulli.IdArtikulli;
    $('#hfKoeficenti').val(artikulli.KoeficientArtikulli);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeArtikulli"),
        data: JSON.stringify({ idArtikulli: artikulli.IdArtikulli })
    }).done(SucceededCallbackArtAutorizime);

    Utils.SelectComboItem(cmbKlasa, artikulli.Klasa, artikulli.PershkrimKlasa);
    if (veprimi == "modifikim")
        txtKodi.SetEnabled(false);
    checkKontrollGjendjeArtikulli.SetChecked(artikulli.KontrollGjendjeArtikulli);
    checkKontrollCmimi.SetChecked(artikulli.KontrollCmimi);
    checkKontrollGjendjeDetajim2.SetChecked(artikulli.KontrollGjendjeDetajim2);
    checkKontrollGjendje.SetChecked(artikulli.KontrollGjendje);
    txtKodi.SetText(artikulli.KodArtikulli);
    txtPershkrimi.SetText(artikulli.PershkrimArtikulli);
    txtPershkrimiAng.SetText(artikulli.PershkrimiAngArtikulli);
    txtPershkrimiFurnitori.SetText(artikulli.PershkrimFurnitori);
    txtVendodhja.SetText(artikulli.VendodhjeArtikulli);
    cbPerPershore.SetChecked(artikulli.PerPeshore);
    cbMeSerial.SetChecked(artikulli.MeSerial);
    txtKoeficienti.SetText(artikulli.KoeficientArtikulli);
    if (artikulli.IdTvsh != null && artikulli.IdTvsh != 0)
        cmbNivelTvsh.SetValue(artikulli.IdTvsh);
    else
        cmbNivelTvsh.SetSelectedIndex(-1);
    if (artikulli.KodKodifikimi1 != null) {
        Utils.SelectComboItem(btneKodifikimi1, artikulli.Kodifikimi1Artikulli, artikulli.KodKodifikimi1, " ");
    }
    else
        btneKodifikimi1.SetSelectedIndex(-1);
    if (artikulli.KodKodifikimi2 != null) {
        Utils.SelectComboItem(btneKodifikimi2, artikulli.Kodifikimi2Artikulli, artikulli.KodKodifikimi2, " ");
    }
    else
        btneKodifikimi2.SetSelectedIndex(-1);
    if (artikulli.KodKodifikimi3 != null) {
        Utils.SelectComboItem(btneKodifikimi3, artikulli.Kodifikimi3Artikulli, artikulli.KodKodifikimi3, " ");
    }
    else
        btneKodifikimi3.SetSelectedIndex(-1);

    if (artikulli.KodNjesia1 != null) {
        Utils.SelectComboItem(cmbNjesia1, artikulli.Njesi1Artikulli, artikulli.KodNjesia1, " ");
    }
    else
        cmbNjesia1.SetSelectedIndex(-1);
    if (artikulli.KodNjesia2 != null) {
        Utils.SelectComboItem(cmbNjesia2, artikulli.Njesi2Artikulli, artikulli.KodNjesia2, " ");
    }
    else
        cmbNjesia2.SetSelectedIndex(-1);
    if (artikulli.KodFurnitori != null) {
        Utils.SelectComboItem(txtFurnitori, artikulli.IdFurnitoriKryesor, artikulli.KodFurnitori, values.furnitori.EmertimiKF);
    }
    else
        txtFurnitori.SetSelectedIndex(-1);
    if (artikulli.KodSkema != null)
        Utils.SelectComboItem(btneSkema, artikulli.IdSkemaKontabilitetiArtikulli, artikulli.KodSkema, " ");
    else
        btneSkema.SetSelectedIndex(-1);
    if (cmbKlasa.GetValue() !== "5")
    { cbProdhimMePorosi.SetVisible(false); lblProdhimMePorosi.SetVisible(false); }
    else { cbProdhimMePorosi.SetVisible(true); lblProdhimMePorosi.SetVisible(true); }
    if (cmbKlasa.GetValue() !== "5" && cmbKlasa.GetValue() !== "1")
    { cbRezervueshem.SetEnabled(false); }
    else { cbRezervueshem.SetEnabled(true); }
    cbProdhimMePorosi.SetChecked(artikulli.ProdhimMePorosi);
    cbRezervueshem.SetChecked(artikulli.IRezervueshem); cbPerTransferim.SetChecked(artikulli.PerTransferim); cbLoan.SetChecked(artikulli.Loan);
    if (!Utils.IsNullOrEmpty(artikulli.Magazina)) {
        Utils.SelectComboItem(btnMagazina, artikulli.IdMagazina, artikulli.Magazina);
        btnMagazina.SetValue(artikulli.IdMagazina);
        btnMagazina.SetText(artikulli.Magazina);
    }
    else {
        btnMagazina.SetSelectedIndex(-1);
        btnMagazina.SetText('');
    }
    txtKodiIBarit.SetText(artikulli.KodiIBarit);
    cbIRimbursueshem.SetChecked(artikulli.IRimbursueshem);
    Utils.SelectComboItem(btneLlogInv, artikulli.IdLlogariInventari, artikulli.NrLlogInventari);
    Utils.SelectComboItem(btneLlogBle, artikulli.IdLlogariBlerje, artikulli.NrLlogBlerje);
    Utils.SelectComboItem(btneLlogShit, artikulli.IdLlogariShitje, artikulli.NrLlogShitje);
    Utils.SelectComboItem(btneLlogTretet, artikulli.IdLlogariTeTrete, artikulli.NrLlogTeTrete);
    Utils.SelectComboItem(btnLlogShpe, artikulli.IdLlogariShpenzime, artikulli.NrLlogShpenzime);
    Utils.SelectComboItem(cmbLlogAmortizimi, artikulli.IdLlogariAmortizimi, artikulli.NrLlogAmortizimi);
    Utils.SelectComboItem(btnLlogPakesim, artikulli.IdLlogariPakesimi, artikulli.NrLlogPakesimi);
    Utils.SelectComboItem(cmbMetode, artikulli.MetodeKostojeArtikulli);

    cbDetajim.SetChecked(artikulli.DetajimArtikulli);
    Utils.SelectComboItem(cmbKategoriDetajimi1, artikulli.IdKategoriDetajimi, values.katDet1);
    Utils.SelectComboItem(cmbKategoriDetajimi2, artikulli.IdKategoriDetajimi2, values.katDet2);

    if (!detajime || veprimi == "klonim") {
        btnDetajimi1.SetText('');
        btnDetajimi2.SetText('');
    }
    else {
        btnDetajimi1.SetText(detajime.detajime1);
        btnDetajimi2.SetText(detajime.detajime2);
    }
    enableDetajim();

    var ArrayKodbaret = new Array();

    if (artikulli.OColKodbare != null && artikulli.OColKodbare.length > 0) {
        var kodbari = '';
        for (var i = 0; i < artikulli.OColKodbare.length; i++) {
            if (artikulli.OColKodbare[i].Pershkrimi == '')
                continue;
            var barkodiPershkrim = artikulli.OColKodbare[i].Pershkrimi.replace(/\s/g, '');
            kodbari += barkodiPershkrim + ",";
            var njesia = artikulli.OColKodbare[i].Njesia;


            var detajim1 = artikulli.OColKodbare[i].Detajim1;

            var detajim2 = artikulli.OColKodbare[i].Detajim2;

            ArrayKodbaret[i] = new Object();
            ArrayKodbaret[i] = { indeksi: i, pershkrimi: barkodiPershkrim, njesia: njesia, detajimi1: detajim1, detajimi2: detajim2 };
        }

        btneKodbari.SetText(kodbari)
    }
    else btneKodbari.SetText('');

    $('#hfKodbaret').val(JSON.stringify(ArrayKodbaret));
    if ($('#hfShtimModifikim').value != "shtim") {
        var idGjuha = hfState.Get('idGjuha');
        var idNdermarrje = hfState.Get('idNdermarrje');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
            data: JSON.stringify({ idkomp: "402", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: artikulli.IdArtikulli, idNdermarrje: idNdermarrje, idGjuha: idGjuha })
        }).done(function (result) { SucceededCallbackLidhur(result, artikulli.IdArtikulli) });
    }

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheArtikujPerberes2"),
        data: JSON.stringify({ id: artikulli.IdArtikulli })
    }).done(SucceededCallbackArtPerberes2);
    if (hfTeDrejtaCmimi.Get("Amb") == true || hfTeDrejtaCmimi.Get("Amb") == 'true') {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "merrcolMeCmimeMeFormatNumrash"),
            data: JSON.stringify({ idartikulli: artikulli.IdArtikulli, merrCmimeBlerje: false, idNdermarrje: idNdermarrje, idPerdorues: hfState.Get('idPerdoruesi') })
        }).done(SucceededCallbackCmime);
    }
}

function KodifikimArtikulli_Click(llojKodifikimi) {
    var queryStr = $("#hfLupaKodifikim1").val();
    var parametri = Utils.getUrlVar('llojiart') == 'afatshkurter' ? 'false' : 'true';
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + llojKodifikimi + '&llojartikulli=' + parametri + '&idKonfigAmbjente=' + queryStr, widthLupaKodifikime, heightLupaKodifikime);
}

function KodifikimArtikulli_Click2(llojKodifikimi) {
    var queryStr = $("#hfLupaKodifikim2").val();
    var parametri = Utils.getUrlVar('llojiart') == 'afatshkurter' ? 'false' : 'true';
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + llojKodifikimi + '&llojartikulli=' + parametri + '&idKonfigAmbjente=' + queryStr, widthLupaKodifikime, heightLupaKodifikime);
}
function KodifikimArtikulli_Click3(llojKodifikimi) {
    var queryStr = $("#hfLupaKodifikim3").val();
    var parametri = Utils.getUrlVar('llojiart') == 'afatshkurter' ? 'false' : 'true';
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + llojKodifikimi + '&llojartikulli=' + parametri + '&idKonfigAmbjente=' + queryStr, widthLupaKodifikime, heightLupaKodifikime);
}
function Skema_Click() {
    var hf = $("#hfLupaSkema")[0];
    var queryStr = hf.value;
    var parametri = Utils.getUrlVar('llojiart') == 'afatshkurter' ? 'afatshkurter' : 'aqt';
    myButtonClickLupa.Skema_Click(hfState.Get("headerZgjidhSkemenkont"), queryStr, widthLupaSkema, heightLupaSkema, cmbKlasa.GetValue(), parametri);
}

function Kodbare_Click() {
    var hf = $("#hfLupaKodbari")[0];
    var queryStr = hf.value + '&idartikulli=' + $('#hfId').val();
    var kodbaret = $("#hfKodbaret")[0].value;

    if (kodbaret != "") {
        if (JSON.parse(kodbaret).length > 20) {
            queryStr += "&mbushNgaArtikulli=true";
            kodbaret = "";
        }
    }
    myButtonClickLupa.Kodbare_Click(hfState.Get('headerPopUpKodBar'), queryStr, widthLupaKodbare, heightLupaKodbare, kodbaret);
}

var lengjth = 0;
//pastron fushat
function Init() {
    window.parent.popupUniversal.UpdatePosition();
    lengjth = window.history.length;
    changeName();
    var veprimi = Utils.getUrlVar('veprimi');
    var hfMod = $('#hfShtimModifikim');
    var kodArtikulli = Utils.getUrlVar('kodArt');

    if (!Utils.IsNullOrWhiteSpace(kodArtikulli) && !Utils.IsUndefined(kodArtikulli)) {
        $("#hfRuaj").val(veprimi == "modifikim" ? "Modifiko" : "Ruaj");
        if (veprimi == 'modifikim' || veprimi == "klonim") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullSipasKodit"), data: JSON.stringify({ kodi: kodArtikulli, merrfurnitor: true, idNdermarrje: hfState.Get('idNdermarrje'), merrDetajime: true })
            }).done(KonfiguroVleraFillestareModifikim);
        }
    }
    else
        konfiguroLupePerShtim();

    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    if (Utils.getUrlVar("llojiart") == 'aqt') {
        gvAmortizimi.SetVisible(true);
        gvCmimet.SetVisible(false);
    }
}

function konfiguroLupePerShtim() {
    var hfMod = $('#hfShtimModifikim');
    hfMod.val('shtim');
    $("#hfRuaj").val("Ruaj");
    $('#hfKoeficenti').val(1);
    hfArkiva.Clear();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "merrcolMeCmimeMeFormatNumrash"),
        data: JSON.stringify({ idartikulli: -1, merrCmimeBlerje: false, idNdermarrje: hfState.Get('idNdermarrje'), idPerdorues: hfState.Get('idPerdoruesi') })
    }).done(SucceededCallbackCmime);
}

function KonfiguroVleraFillestareModifikim(result) {
    var hfMod = $('#hfShtimModifikim');
    if (result == null) {
        konfiguroLupePerShtim();
        return;
    }

    var veprimi = Utils.getUrlVar('veprimi');
    hfMod.val(veprimi);
    if (veprimi == "modifikim")
        $("#hfRuaj").val("Modifiko");
    else
        $("#hfRuaj").val("Ruaj");
    mbushFushat(result);
}


//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {

    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
    enableDetajim();
}

function SucceededCallbackArtPerberes2(result) { //pati 
    var grida = $('#rowed5');
    var idRresht = 1;
    var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true')
    enable(true, isLidhur);
    if (Utils.getUrlVar('veprimi') == "shtim")
        return;
    var colTrup = result[0];
    var colArt = result[1];
    var colMakro = result[2];
    var colkosto = result[3];


    grida.jqGrid('clearGridData');
    var lloji, kodi, pershkrimi, njesia, koeficienti, kosto, firo, ngastoku, idkodi, pershkrimiang;
    var be;
    if (colArt.length == 0) {
        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || lidhur)
            be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
        var datarow = {
            txtFshi: be
        };
        var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
        idRresht = idRresht + 1;
        grida.setLastSel2(idRresht);
        return;
    }
    for (var i = 0; i < colArt.length; i++) {
        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || lidhur)
            be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
        if (colTrup[i].Lloji == 1) {
            lloji = "Artikull";
            kodi = colArt[i].KodArtikulli;
            pershkrimi = colArt[i].PershkrimArtikulli;
            // pershkrimiang = colArt[i].PershkrimiAngArtikulli;
            njesia = (colArt[i].KodNjesia1 == null) ? "" : colArt[i].KodNjesia1;
            idkodi = colArt[i].IdArtikulli;
            kosto = colkosto[i].toFixed(2);
        }
        else if (colTrup[i].Lloji == 2) {
            lloji = "Aktivitete";
            kodi = colMakro[i].Kodi
            pershkrimi = colMakro[i].Emertimi;
            pershkrimiang = "";
            idkodi = colMakro[i].IdKoka;
            njesia = (colMakro[i].NjesiKohe == 1 ? 'sec' : colMakro[i].NjesiKohe == 2 ? 'min' : colMakro[i].NjesiKohe == 3 ? 'ore' : 'dite');
            //kosto = '';
            kosto = colkosto[i].toFixed(2);
        }

        koeficienti = (colTrup[i].Koeficienti == null) ? "" : colTrup[i].Koeficienti;
        firo = (colTrup[i].Scrap.toFixed(2) == null) ? "" : colTrup[i].Scrap.toFixed(2);

        var datarow = {
            cmbLloji: lloji, txtIdKodi: idkodi, txtKodi: kodi, txtPershkrimi: pershkrimi, txtNjesia: njesia, txtKoeficienti: koeficienti, txtFiro: firo, txtKosto: kosto, txtFshi: be
        };
        var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
        idRresht = idRresht + 1;
        grida.setLastSel2(idRresht);
    }
}

function SucceededCallbackData(result) {
    cmbNdryshimi.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbNdryshimi.AddItem(result[i], result[i]); //AddItem(teksti, vlera);
    cmbNdryshimi.SetSelectedIndex(0);
    if (cmbNdryshimi.GetText() !== "")
        dteDateAkt.SetText(cmbNdryshimi.GetText());
}

function aktivFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    enable(false, isLidhur);
}


//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    var hf = $("#status1")[0];
    hf.value = "false";
    colNorma = new Array();
}

function valido(s, e) {
    myFaqeCelje.validim(s, e);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function kontrolloSkema() {
    var hf = $("#hfLupaSkema")[0];
    var queryStr = hf.value;
    var s = $("#hfSkemaKlasa")[0].value.split(';');
    if (btneSkema.GetText() != "" && popupUniversal.GetContentUrl().search('LupaSkemaKontabelArtikulli.aspx') == -1) {
        for (i = 0; i < s.length; i++) {
            if (s[i].split(':')[0] == btneSkema.GetText()) {
                if (s[i].split(':')[1] != cmbKlasa.GetValue()) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgClsArtikulliKjoSkemeNukIPerketKesajKlase"));
                    btneSkema.SetSelectedIndex(-1);
                    return;
                }
            }
        }
    }
}
/// kjo metode perdoret per te kontrolluar 1. nese koeficienti eshte numer
///2. nqs njesite jane te barabarta atehere koeficienti nuk mund te jete i ndryshem nga 1
///3. vendos cmimet e artikullit per njesine e dyte ne baze te koeficientit
function kontrolloNjesi() {
    if (txtKoeficienti.GetText() == "" || txtKoeficienti.GetText() == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficJoZeroOseBosh"));
        txtKoeficienti.SetText('1');
    }
    if (cmbNjesia1.GetText() == cmbNjesia2.GetText())
        if (txtKoeficienti.GetText() != 1) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficDuhetNje"));
            txtKoeficienti.SetText(1);
        }

    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1) ; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorCmimi2.SetText(editorCmimi.GetText() * txtKoeficienti.GetText());

            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));

        }
    else {
        for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);

            editorCmimi = Utils.ktheKontroll('Cmimi' + i);

            editorCmimi2.SetText(editorCmimi.GetText() * txtKoeficienti.GetText());

            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));

        }
    }
}



var col;
function EndCallback(s, e) {
    ShfaqTeDhenat();
}

var editorSasiMin, editorSasiMax;
function SucceededCallbackCmime(result) {
    col = result.cmimet;
    arrformatesasia = result.formatisasi;
    arrformatecmimi = result.formaticmim;
    arrformatekursi = result.formatikurs;
    ShfaqTeDhenat();
}

function merrTeDhenat(s, e) {//merren te dhenat qe ka grida
    if (col == undefined || col == null || col.length == 0)
        return;
    //if (col != undefined || col != null) {
    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1) ; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorDateFillimi = Utils.ktheKontroll('DateFillimi' + i); editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i);//$("input[id$=" + stringDate + "]")[0]
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            //$("input[id$=" + stringDate + "]")[0]
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);
            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorKosto = Utils.ktheKontroll('Kosto' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);

            col[i].Cmimi = editorCmimi.GetText();
            col[i].Cmimi2 = editorCmimi2.GetText();
            col[i].DateFillimi = editorDateFillimi.GetDate();
            col[i].DateMbarimi = editorDateMbarimi.GetDate();
            col[i].Kosto = editorKosto.GetText();
            col[i].Kursi = editorKursi.GetText();
            col[i].SasiMin = editorSasiMin.GetText();
            col[i].SasiMax = editorSasiMax.GetText();
            col[i].Norme = editorNorme.GetText();
            col[i].IdTvsh = editorTvsh.GetValue();
            col[i].CmimiTvsh = editorCmimiTvsh.GetText();
            col[i].Cmimi2Tvsh = editorCmimiTvsh2.GetText();
            col[i].KoheFillimi = editorKoheFillimi.GetDate();
            col[i].KoheMbarimi = editorKoheMbarimi.GetDate();
        }
    else {
        for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorDateFillimi = Utils.ktheKontroll('DateFillimi' + i); editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i) //$("input[id$=" + stringDate + "]")[0]
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            //$("input[id$=" + stringDate + "]")[0]
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);
            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorKosto = Utils.ktheKontroll('Kosto' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);
            col[i].Cmimi = editorCmimi.GetText();
            col[i].Cmimi2 = editorCmimi2.GetText();
            col[i].DateFillimi = editorDateFillimi.GetDate();
            col[i].DateMbarimi = editorDateMbarimi.GetDate();
            col[i].Kosto = editorKosto.GetText();
            col[i].Kursi = editorKursi.GetText();
            col[i].SasiMin = editorSasiMin.GetText();
            col[i].SasiMax = editorSasiMax.GetText();
            col[i].Norme = editorNorme.GetText();
            col[i].IdTvsh = editorTvsh.GetValue();
            col[i].CmimiTvsh = editorCmimiTvsh.GetText();
            col[i].Cmimi2Tvsh = editorCmimiTvsh2.GetText();
            col[i].KoheFillimi = editorKoheFillimi.GetDate();
            col[i].KoheMbarimi = editorKoheMbarimi.GetDate();
        }
    }
    $('#hfArtikuj').val(JSON.stringify(col));
    unformatoFushaDevi();
    //}
}

function ShfaqTeDhenat() {
    if (col == undefined || col == null || col.length == 0)
        return;
    //if (col != undefined || col != null)
    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1) ; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorDateFillimi = Utils.ktheKontroll('DateFillimi' + i);
            editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i); //$("input[id$=" + stringDate + "]")[0]
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorKosto = Utils.ktheKontroll('Kosto' + i); editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            editorFormula = Utils.ktheKontroll('txtFormula' + i);
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            //$("input[id$=" + stringDate + "]")[0]
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);
            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorCmimi2.SetText(col[i].Cmimi2);
            editorDateFillimi.SetDate(new Date(col[i].DateFillimi));
            editorDateMbarimi.SetDate(new Date(col[i].DateMbarimi));
            editorCmimi.SetText(col[i].Cmimi);
            editorKosto.SetText(col[i].Kosto);
            editorKursi.SetText(col[i].Kursi);
            editorSasiMin.SetText(col[i].SasiMin);
            editorSasiMax.SetText(col[i].SasiMax);
            editorNorme.SetText(col[i].Norme);


            if (cmbNivelTvsh.GetValue() == null) {
                editorTvsh.SetValue(col[i].IdTvsh);
                editorNorme.SetText(col[i].Norme);
                editorCmimiTvsh.SetText((col[i].Cmimi * (1 + col[i].Norme / 100)));
                editorCmimiTvsh2.SetText((col[i].Cmimi2 * (1 + col[i].Norme / 100)));
            }
            else {
                editorTvsh.SetValue(cmbNivelTvsh.GetValue());
                editorNorme.SetText(cmbNivelTvsh.GetText());
                editorCmimiTvsh.SetText((col[i].Cmimi * (1 + cmbNivelTvsh.GetText() / 100)));
                editorCmimiTvsh2.SetText((col[i].Cmimi2 * (1 + cmbNivelTvsh.GetText() / 100)));
            }

            editorKoheFillimi.SetDate(new Date(col[i].KoheFillimi));
            editorKoheMbarimi.SetDate(new Date(col[i].KoheMbarimi));

            editorFormula.SetText(hfKushtet.Get("formula"));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMin' + i), arrformatesasia[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMin' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMax' + i), arrformatesasia[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMax' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Kursi' + i), arrformatekursi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Kursi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Kosto' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Kosto' + i));
        }
    else {
        for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorDateFillimi = Utils.ktheKontroll('DateFillimi' + i);
            editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i); //$("input[id$=" + stringDate + "]")[0]
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorKosto = Utils.ktheKontroll('Kosto' + i); editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            editorFormula = Utils.ktheKontroll('txtFormula' + i);
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            //$("input[id$=" + stringDate + "]")[0]
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);
            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorNorme.SetText(col[i].Norme);
            if (cmbNivelTvsh.GetValue() == null) {
                editorTvsh.SetValue(col[i].IdTvsh);
                editorNorme.SetText(col[i].Norme);
                editorCmimiTvsh.SetText((col[i].Cmimi * (1 + col[i].Norme / 100)));
                editorCmimiTvsh2.SetText((col[i].Cmimi2 * (1 + col[i].Norme / 100)));
            }
            else {
                editorTvsh.SetValue(cmbNivelTvsh.GetValue());
                editorNorme.SetText(cmbNivelTvsh.GetText());
                editorCmimiTvsh.SetText((col[i].Cmimi * (1 + cmbNivelTvsh.GetText() / 100)));
                editorCmimiTvsh2.SetText((col[i].Cmimi2 * (1 + cmbNivelTvsh.GetText() / 100)));
            }

            editorKoheFillimi.SetDate(new Date(col[i].KoheFillimi));
            editorKoheMbarimi.SetDate(new Date(col[i].KoheMbarimi));

            editorCmimi2.SetText(col[i].Cmimi2);
            editorDateFillimi.SetDate(new Date(col[i].DateFillimi));
            editorDateMbarimi.SetDate(new Date(col[i].DateMbarimi));
            editorCmimi.SetText(col[i].Cmimi);
            editorKosto.SetText(col[i].Kosto);
            editorKursi.SetText(col[i].Kursi);
            editorSasiMin.SetText(col[i].SasiMin);
            editorSasiMax.SetText(col[i].SasiMax);
            editorFormula.SetText(hfKushtet.Get("formula"));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMin' + i), arrformatesasia[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMin' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('SasiMax' + i), arrformatesasia[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMax' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Kursi' + i), arrformatekursi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Kursi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Kosto' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Kosto' + i));
        }
    }
}
function TextChangedSasiMaxMin(editor, key, eshteSasiMax, e) {
    if (editor.GetValue() == null) {
        editor.SetFocus();
        if (eshteSasiMax == true)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiMaxBosh"));
        else
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiteMinBosh"));
        editor.SetText(0);
        e.processOnServer = false;
    }
    if (isNaN(parseFloat(editor.GetValue()))) {
        editor.SetFocus();
        if (eshteSasiMax == true)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimeArtikulliSasiteMaxDuhenNumerike"));
        else
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimeArtikulliSasiteMinimaleDuhenNumerike"));
        editor.SetText(0);
        e.processOnServer = false;
    }

    if (Utils.ktheKontroll('SasiMax' + key).GetValue() != null && Utils.ktheKontroll('SasiMin' + key).GetValue() != null && parseFloat(Utils.ktheKontroll('SasiMax' + key).GetValue().replace(/,/g, "")) < parseFloat(Utils.ktheKontroll('SasiMin' + key).GetValue().replace(/,/g, ""))) {
        myMesazh.ShtoMesazhGabimi("Sasia Min nuk mund te jete me e madhe se sasia Max!");
        editor.SetText(0);
        editor.SetFocus();
        e.processOnServer = false;

    }
}
function TextChangedKohaFill(editor, e, key) {
    if (editor.GetDate().getHours() * 60 + editor.GetDate().getMinutes() > Utils.ktheKontroll('KoheMbarimi' + key).GetDate().getHours() * 60 + Utils.ktheKontroll('KoheMbarimi' + key).GetDate().getMinutes()) {
        myMesazh.ShtoMesazhGabimi("Koha e fillimit nuk mund te jete me e madhe se koha e mbarimit!");

        editor.SetDate(Utils.ktheKontroll('KoheMbarimi' + key).GetDate());

    }
}

function TextChangedCmimet(editor, e, key, artNjesiTeVarura, llojCmimi) {
    if (isNaN(editor.GetValue()) || editor.GetValue() == null) {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimeArtikulliCmimetDuhenNumerike"));
        editor.SetText(0);
    }
    editorNorma = Utils.ktheKontroll('Norme' + key);
    editorCmimi = Utils.ktheKontroll('Cmimi' + key);
    editorCmimi2 = Utils.ktheKontroll('Cmimi2' + key);
    editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + key);
    editorCmimi2Tvsh = Utils.ktheKontroll('Cmimi2Tvsh' + key);

    if (llojCmimi.toLowerCase() === 'cmimi1') {
        editorCmimi2.SetText(txtKoeficienti.GetText() * editor.GetValue());
        editorCmimiTvsh.SetText((1 + editorNorma.GetText() / 100) * editor.GetValue());
        editorCmimi2Tvsh.SetText((1 + editorNorma.GetText() / 100) * editorCmimi2.GetText());
    }

    if (llojCmimi.toLowerCase() === 'cmimi1metvsh') {
        editorCmimi2Tvsh.SetText(txtKoeficienti.GetText() * editor.GetValue());
        editorCmimi.SetText(editor.GetValue() / (1 + editorNorma.GetText() / 100));
        editorCmimi2.SetText(editorCmimi2Tvsh.GetText() / (1 + editorNorma.GetText() / 100));
    }

    if (llojCmimi.toLowerCase() === 'cmimi2') {
        if (artNjesiTeVarura.toLowerCase() === 'true')
            editorCmimi.SetText(editor.GetValue() / txtKoeficienti.GetText());
        editorCmimiTvsh.SetText((1 + editorNorma.GetText() / 100) * editorCmimi.GetText());
        editorCmimi2Tvsh.SetText((1 + editorNorma.GetText() / 100) * editor.GetValue());
    }

    if (llojCmimi.toLowerCase() === 'cmimi2metvsh') {
        if (artNjesiTeVarura.toLowerCase() === 'true')
            editorCmimiTvsh.SetText(editor.GetValue() / txtKoeficienti.GetText());
        editorCmimi.SetText(editorCmimiTvsh.GetText() / (1 + editorNorma.GetText() / 100));
        editorCmimi2.SetText(editor.GetValue() / (1 + editorNorma.GetText() / 100));
    }
}



function TextChangedIdTvsh(editor, e, key) {

    editorNorma = Utils.ktheKontroll('Norme' + key);
    editorNorma.SetText(editor.GetSelectedItem().GetColumnText("NormaPerqindje"));
    editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + key);
    editorCmimiTvsh.SetText((1 + editorNorma.GetValue() / 100) * Utils.ktheKontroll('Cmimi' + key).GetText());
    editorCmimi2Tvsh = Utils.ktheKontroll('Cmimi2Tvsh' + key);
    editorCmimi2Tvsh.SetText((1 + editorNorma.GetValue() / 100) * Utils.ktheKontroll('Cmimi2' + key).GetText());
}
function changeTvsh(s, e) {
    merrTeDhenat();
    ShfaqTeDhenat();
}

function TextChangedKohaMbar(editor, e, key) {
    if (editor.GetDate().getHours() * 60 + editor.GetDate().getMinutes() < Utils.ktheKontroll('KoheFillimi' + key).GetDate().getHours() * 60 + Utils.ktheKontroll('KoheFillimi' + key).GetDate().getMinutes()) {

        myMesazh.ShtoMesazhGabimi("Koha e fillimit nuk mund te jete me e madhe se koha e mbarimit!");

        editor.SetDate(Utils.ktheKontroll('KoheFillimi' + key).GetDate());

    }
}



function TextChangedDataFill(editor, e, key) {

}

//pati --> funksione per griden e artikujve perberes
var colNorma = new Array();
var identikuesPerPopupLlogari;
var identikuesPerPopupArtikulliArtPerb;
var arr = new Array();
var keyGlobal;
var editorKodi;
var editorEmertimi;
var editorLloji;
var editorVlera;
var numerReshtashQeShtohen;
var indeksPerEmertim;
var indeksFillimi = 0;

$(window).on('unload', function () {
})



var indeksiArtPerb;
var tekstiShkruar;

function llogaritKoston(key) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    colNorma = new Array();
    //var rreshtaTeGrides = grida.jqGrid('getRowData');
    //for (i = 0; i < rreshtaTeGrides.length; i++) {
    //    if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1) {

    //        colNorma[i] = new Object();
    //        colNorma[i].Kodi = $("#txtKodi" + idRresht).val();
    //        colNorma[i].Lloji = grida.getTekstQelize('cmbLloji', idRresht);
    //        colNorma[i].Koeficienti = $("#txtKoeficienti" + idRresht).val();
    //    }
    //    else {
    //        colNorma[i] = new Object();
    //        colNorma[i].Kodi = rreshtaTeGrides[i].txtKodi;
    //        colNorma[i].Lloji = rreshtaTeGrides[i].cmbLloji;
    //        colNorma[i].Koeficienti = rreshtaTeGrides[i].txtKoeficienti;
    //    }
    //}
    var gridIds = grida.jqGrid('getDataIDs');
    for (i = 0; i < gridIds.length; i++) {
        var indeksi = gridIds[i];
        colNorma[i] = new Object();
        colNorma[i].Kodi = grida.getTekstQelize("txtKodi", indeksi);
        colNorma[i].Lloji = grida.getTekstQelize("cmbLloji", indeksi);
        colNorma[i].Koeficienti = grida.getTekstQelize("txtKoeficienti", indeksi);
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKostonArtikujvePerberes"),
        data: JSON.stringify({ artikujtPerberes: JSON.stringify(colNorma), idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackKostoArtPerb);
}

function SucceededCallbackKostoArtPerb(result) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
            for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1) ; i++) {
                editorKosto = Utils.ktheKontroll('Kosto' + i);
                editorKosto.SetText(result);
            }

        else {
            for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
                editorKosto = Utils.ktheKontroll('Kosto' + i);
                editorKosto.SetText(result);
            }
        }
}





function Furnitori_Click() {
    var hf = $("#hfLupaFurnitori");
    var queryStr = hf.val();
    identikuesPerPopupKlientFurnitori = "Artikull_ButtonEdit";
    //txtLlog.SetText('ld');
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, "Furnitor", 600, 400);
}

function KlientFurnitoriChanged() {
    if (isNaN(txtFurnitori.GetValue())) {
        txtFurnitori.SetText('');
        txtFurnitori.Focus();
        return;
    }
}

var editorCmimi;
var editorCmimi2;
var editorKosto;
var editorKursi;
var editorFormula;

function ClickUpdateBtn(s, e, id) {
    editorCmimi = Utils.ktheKontroll('Cmimi' + id);
    editorKosto = Utils.ktheKontroll('Kosto' + id);
    editorKursi = Utils.ktheKontroll('Kursi' + id);
    editorFormula = Utils.ktheKontroll('txtFormula' + id);
    editorCmimi2 = Utils.ktheKontroll('Cmimi2' + id);
    editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + id);
    editorCmimi2Tvsh = Utils.ktheKontroll('Cmimi2Tvsh' + id);
    editorNorma = Utils.ktheKontroll('Norme' + id);
    if (editorKursi.GetValue() != 0) {
        try {
            var expr = '(' + editorKosto.GetValue() + '/' + editorKursi.GetValue() + ')' + editorFormula.GetText();
            var rezultati = eval(expr.replace(',', ''));

            editorCmimi.SetText(rezultati);
            editorCmimi2.SetText($('#hfKoeficenti').val() * rezultati);
            editorCmimiTvsh.SetText(rezultati * (1 + editorNorma.GetText() / 100));
            editorCmimi2Tvsh.SetText(txtKoeficienti.GetText() * rezultati * (1 + editorNorma.GetText() / 100));
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimLlogCmim"));
        }
    }
}
function ndryshoTvsh() {
    merrTeDhenat();
    ShfaqTeDhenat();

}


/*
Function: ButtonClickMagazina

    
Hap lupen e magazinave.
*/
function ButtonClickMagazina() {//po    
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhMagazinen"), 'LupaMagazina.aspx?idKonfigAmbjente=' + $('#hfLupaMagazina').val(), 600, 560);
}

function LlogariI_Click() {
    var hf = $("#hfLupaLlogInv")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariB_Click() {
    var hf = $("#hfLupaLlogBle")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariS_Click() {
    var hf = $("#hfLupaLlogShit")[0];
    var queryStr = hf.value;
    identikuesPerPopupLlogari = '';
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function LlogariShp_Click() {
    var hf = $("#hfLupaLlogShpe")[0];
    var queryStr = hf.value;

    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function LlogariAmor_Click() {
    var hf = $("#hfLupaLlogAmortizimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function LlogariT_Click() {
    var hf = $("#hfLupaLlogTretet")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function LlogariPakesim_Click() {
    var hf = $("#hfLupaLlogPakesim")[0];
    var queryStr = hf.value;

    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function SelectedIndexChangedFormula(editori, indexi) {

}

function TextChangedFormula(editori, indexi) {
    var a = new Array();
    var editor = Utils.ktheKontroll(editori);
    a = editor.GetText().toString().split(',');
    editor.SetText(a[1]);
}

function KeyPressFormula(kodi, editori) {
    if (kodi === 13) {
        editorFormula = Utils.ktheKontroll(editori);
        identikuesPerPopupFormula = 'LupaArtShpejt';
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhFormulen"), 'LupaFormula.aspx', 850, 600);
    }
}

function LostFocusFormula(editori, indexi) {

}

function ButtonClickFormula(editori, indexi) {
    editorFormula = Utils.ktheKontroll(editori);
    identikuesPerPopupFormula = 'LupaArtShpejt';
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhFormulen"), 'LupaFormula.aspx', 850, 600);
}

function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("MsgBlerjeShitjeAutorizime"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}

function SucceededCallbackArtAutorizime(result) {

    if (result.idArtikulli == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
}


var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();

var lidhur = false;
function inicializoGride(isLidhur) {//po
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    var classes = '';
    if (lidhur === true)
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
                           arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7],
                           arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9]];
    var arrayModel = [
            { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo, custom_value: myJQGrid.myValueCombo } },
            { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKodi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesia, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKoeficienti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemNgaStoku, custom_value: myJQGrid.myvalueNormal }, hidedlg: true },
            { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemFiro, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKosto, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];

    var selektoriGrides = "#rowed5";
    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtKodi",
        emerEditorLloji: "cmbLloji",
        emerEditorCmimi: "",
        widthi: $('#divgride2').width(),
        resetRreshtKorent: resetRreshtKorent,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        arrayRenditjeKolonaGridesName: "arrayRenditjeKolonaGrides",

        autocompleteList: [
            { emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false }],

        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            shtoArtikull: $('#hfTeDrejtaArtRi').val(),
            modArtikull: $('#hfTeDrejtaArtMod').val()
        }

    };
    return myJQGrid.initGride(gridParams);

    //myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, isLidhur, lastsel2, "#txtKodi", "#cmbLloji", "", null, null, null, $('#divgride2').width(), undefined, undefined, null, null, '', '', undefined, undefined, undefined, undefined, undefined, undefined);
    // ruajFormatetNeGride(grida);

}
/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(grida, formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfFormatNumri.Get("formatMonedhe"));
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosVleraDefaultNeGride(grida);
    vendosKonfigFormatNumri();
    return;
}

/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtKoeficienti', 1);
    //  grida.setVlereDefault('txtFiro', 0);
    grida.setVlereDefault('txtKosto', 0);
    return;
}

function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtKoeficienti', idRreshti);
        grida.formatoQelize('txtKosto', idRreshti);

    }

}

function ndryshoKonfigFormatNumri(grida, formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfFormatNumri.Get("formatMonedhe"));
    else
        hfFormatNumri.Set("formatMonedhe", JSON.stringify(formatNumri));
    grida.setShifraPasPresjes('txtKoeficienti', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtKosto', formatNumri.ShifraPasPresjesCmimi);
}





function selectFunc(event, ui, emerfushe, idArt, kodArt) { //po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "" && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(kodArt);
        if (!kontrolloRow(idRresht)) {
            if (vleraLlojit == "Artikull") {
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDPerRec"),
                        data: JSON.stringify({ idja: idArt, rreshti: idRresht, data: new Date(), idmag: 0 })
                    }).done(SucceededCallbackArt);
                } catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
                }

            }
            else
                if (vleraLlojit == "Aktivitete") { //Llogari
                    try {
                        $.ajax({
                            pritPergjigje: true,
                            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraAktiviteteMeIDRow"),
                            data: JSON.stringify({ idja: idArt, rreshti: idRresht })
                        }).done(SucceededCallbackLlog);
                    } catch (e) {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
                    }

                }

        }

        return false;
    }
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
    if (ui !== null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        if (vleraLlojit == "Artikull") {
            try {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDPerRec"),
                    data: JSON.stringify({ idja: ui.item.value, rreshti: idRresht, data: new Date(), idmag: 0 })
                }).done(SucceededCallbackArt);
            } catch (e) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
            }

        }
        else
            if (vleraLlojit == "Aktivitete") { //Llogari
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraAktiviteteMeIDRow"),
                        data: JSON.stringify({ idja: ui.item.value, rreshti: idRresht })
                    }).done(SucceededCallbackLlog);
                } catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
                }

            }
        return false;
    }
}

function changeFunc(event, ui, emerKodi, index) {//po
    //var index = -1;
    //var idKod = 'txtKodi';
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (index == idRresht) {

        var emerfushe = '#' + emerKodi + idRresht;

        var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
        if (ui == null || ui.item == null) {
            var kodi = $(emerfushe).val();
            if (kodi != undefined && typeof (kodi) != "undefined" && kodi != "") {
                if (vleraLlojit == "Artikull") { //Artikull
                    try {
                        $.ajax({
                            pritPergjigje: true,
                            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
                            data: JSON.stringify({ kodi: kodi, rreshti: idRresht, data: new Date(), kodmag: '' })
                        }).done(SucceededCallbackArt);
                    }
                    catch (e) {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
                    }

                    return;
                }
                if (vleraLlojit == "Aktivitete") { //Llogari
                    try {
                        $.ajax({
                            pritPergjigje: true,
                            url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraAktiviteteMeKodRow"),
                            data: JSON.stringify({ kodi: $(emerfushe).val(), rreshti: idRresht })
                        }).done(SucceededCallbackLlog);
                    }
                    catch (e) {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
                    }
                    return;
                }
                return;
            }
            if (vleraLlojit == "Artikull") {
                vendosArt([idRresht, null]);
                return;
            }
            if (vleraLlojit == "Aktivetete") {
                vendosLlog([idRresht, null]);
                return;
            }
            return;
        }
        if (vleraLlojit == "Artikull") {
            try {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
                    data: JSON.stringify({ kodi: ui.item.label, rreshti: idRresht, data: new Date(), kodmag: '' })
                }).done(SucceededCallbackArt);
            }
            catch (e) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
            }

            return;
        }
        if (vleraLlojit == "Aktivitete") { //Llogari
            try {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraAktiviteteMeKodRow"),
                    data: JSON.stringify({ kodi: ui.item.label, rreshti: idRresht })
                }).done(SucceededCallbackLlog);
            }
            catch (e) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
            }
            return;
        }
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        if (rreshti.txtKodi != "") {
            if (rreshti.cmbLloji == "Artikull") { //Artikull
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
                        data: JSON.stringify({ kodi: rreshti.txtKodi, rreshti: index, data: new Date(), kodmag: '' })
                    }).done(SucceededCallbackArt);
                }
                catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
                }
                return;
            }
            if (rreshti.cmbLloji == "Aktivitete") { //Llogari
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraAktiviteteMeKodRow"),
                        data: JSON.stringify({ kodi: rreshti.txtKodi, rreshti: index })
                    }).done(SucceededCallbackLlog);
                }
                catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
                }
                return;
            }
            return;
        }
        if (rreshti.cmbLloji == "Artikull") {
            vendosArt([index, null]);
            return;
        }
        if (rreshti.cmbLloji == "Aktivitete") {
            vendosLlog([index, null]);
            return;
        }
    }

}
function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && $('#txtKodi' + idRow).val() != undefined) {
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');
        grida.setTekstQelize('txtPershkrimi', idRreshti, '');
        grida.setTekstQelize('txtKosto', idRreshti, '');
        grida.setTekstQelize('txtNjesia', idRreshti, '');
        grida.setTekstQelize('txtKoeficienti', idRreshti, 1);
        grida.setTekstQelize('txtFiro', idRreshti, 0);
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}

function vendosArt(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (result != null) {
        var idRreshti = result[0];
        var artikulli = result[1];

        var kodi = "#txtKodi" + idRreshti;
        var idKodi = "#txtIdKodi" + idRreshti;
        var emerArt = '#' + 'txtPershkrimi' + idRreshti;
        if (artikulli == null || artikulli == undefined || artikulli.IdArtikulli == -1) {

            resetRreshtKorent(idRreshti);
            return;
        }
        if (artikulli.Aktiv == false) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliEshteInaktiv"));
            resetRreshtKorent(idRreshti);
            return;
        }
        var comboNjesia = $('#txtNjesia' + idRreshti);
        var comboTVSH = $('#' + 'cbNgaStoku' + idRreshti);
        comboNjesia.val(artikulli.KodNjesia1);
        if (idRreshti == idRow && $(kodi).val() != undefined) {
            if ($(idKodi).val() == artikulli.IdArtikulli && grida.getTekstQelize('cmbLloji', idRreshti) == "Artikull") {
                grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
                return; //eshte i njejti artikull 
            }
            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            myJQGrid.closeAutocomplete('txtKodi', idRreshti);
            grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);
            $(emerArt).val(artikulli.PershkrimArtikulli);
            comboNjesia.val(artikulli.KodNjesia1);
            $('#txtKosto' + idRreshti).val(result[2].toFixed(2));
            kontrolloRow(idRreshti);
            llogaritKoston(-1);
            return;
        }

        var rreshti = grida.jqGrid('getRowData', idRreshti);
        if (rreshti.txtIdKodi == artikulli.IdArtikulli) {
            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            return; //eshte i njejti artikull 
        }
        rreshti.txtKodi = artikulli.KodArtikulli;
        grida.jqGrid('setRowData', idRreshti, rreshti);
        kontrolloRow(idRreshti);
        rreshti = grida.jqGrid('getRowData', idRreshti);

        rreshti.txtKodi = artikulli.KodArtikulli;
        rreshti.txtIdKodi = artikulli.IdArtikulli;
        rreshti.txtKosto = result[2].toFixed(2);
        rreshti.txtPershkrimi = artikulli.PershkrimArtikulli;

        rreshti.txtNjesia = artikulli.KodNjesia1;

        if (rreshti.txtKoeficienti == "")
            rreshti.txtKoeficienti = 1;


        grida.jqGrid('setRowData', idRreshti, rreshti);
        kontrolloRow(idRreshti);
        llogaritKoston(-1);
    }
}
function SucceededCallbackArt(result) {//po
    vendosArt(result);
}
function SucceededCallbackLlog(result) {//po
    vendosLlog(result);
}
function vendosLlog(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (result != null) {
        var idRreshti = result[0];
        var llogaria = result[1];
        if (llogaria == null || llogaria == undefined || llogaria.NrLlogari == -1) {
            resetRreshtKorent(idRreshti);
            return;
        }
        if (idRreshti == idRow && $('#txtKodi' + idRow).val() != undefined) {
            var kodi = "#txtKodi" + idRow;
            var idKodi = "#txtIdKodi" + idRow;
            var emerLlog = '#' + 'txtPershkrimi' + idRow;
            if ($(idKodi).val() == llogaria.IdKoka && grida.getTekstQelize('cmbLloji', idRreshti) == "Aktivitete") {
                grida.setTekstQelize('txtKodi', idRreshti, llogaria.Kodi);
                return; //eshte i njejta llogari 
            }
            $('#txtKodbari' + idRow).val('');
            $(kodi).val(llogaria.Kodi);
            $(idKodi).val(llogaria.IdKoka);
            $(emerLlog).val(llogaria.Emertimi);
            $('#txtKosto' + idRreshti).val(result[2].toFixed(2));
            $('#txtNjesia' + idRreshti).val(llogaria.NjesiKohe == 1 ? 'sec' : llogaria.NjesiKohe == 2 ? 'min' : llogaria.NjesiKohe == 3 ? 'ore' : 'dite');
            kontrolloRow(idRreshti);
            llogaritKoston(-1);
            return;
        }
        var rreshti = grida.jqGrid('getRowData', idRreshti);
        if (rreshti.txtIdKodi == llogaria.IdKoka && grida.getTekstQelize('cmbLloji', idRreshti) == "Aktivitete") {
            grida.setTekstQelize('txtKodi', idRreshti, llogaria.Kodi);
            return; //eshte i njejta llogari 
        }
        grida.jqGrid('setRowData', idRreshti, rreshti);
        kontrolloRow(idRreshti);
        rreshti = grida.jqGrid('getRowData', idRreshti);
        rreshti.txtKodi = llogaria.Kodi;
        rreshti.txtIdKodi = llogaria.IdKoka;
        rreshti.txtPershkrimi = llogaria.Emertimi;
        rreshti.txtKosto = result[2].toFixed(2);
        rreshti.txtNjesia = (llogaria.NjesiKohe == 1 ? 'sec' : llogaria.NjesiKohe == 2 ? 'min' : llogaria.NjesiKohe == 3 ? 'ore' : 'dite');

        grida.jqGrid('setRowData', idRreshti, rreshti); kontrolloRow(idRreshti); llogaritKoston(-1);
    }
}
grida = $('#rowed5');
var idRresht = grida.setLastSel2(0);

function formGridColsArray(isLidhur) {//po
    var IdKonfigAmbjenteLupat = [];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}
/*
Function: myelemCombo

Nderton combo-n Lloji per griden. Combo ka vlerat: Artikull, Makro, Llogari,  Text, Credit Note, Nentotali
*/
function myelemCombo(value) {//po
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var disabled = false;
    var objTmp;
    var arrayOptions = new Array(2);
    objTmp = new Object();
    objTmp.value = "1";
    objTmp.text = "Artikull";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "2";
    objTmp.text = "Aktivitete";
    arrayOptions[1] = objTmp;

    if (arrayReadOnlyKolonaGrides[0] == 'True' || cmbKlasa.GetValue() == 4) {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[0], idRow, change, arrayOptions, disabled);
}

function change() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.setTekstQelize('txtKodi', idRresht);
    grida.setTekstQelize('txtPershkrimi', idRresht);
    grida.setTekstQelize('txtNjesia', idRresht);
    grida.setTekstQelize('txtKosto', idRresht);
    grida.setTekstQelize('txtKoeficienti', idRresht);
    grida.setTekstQelize('txtFiro', idRresht);
    if ($('#cmbLloji' + idRresht).val() == 2) $('#txtFiro' + idRresht).prop('disabled', true);
    else $('#txtFiro' + idRresht).prop('disabled', false);
}

function myelemKodi(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[2];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[2], ButtonClickKodi, keyPressKodi, changeFunc, undefined, lostFocusKoloneFundit);
}

function myelemIdKodi(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1]);
}

function keyPressKodi(event) {     //po           
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var vlera = event.target.value;  //$('#txtKodi' + lastsel2).val();
        if (vlera == "")
            $('#' + arrayIdKolonaGrides[2] + idRresht).autocomplete("close");
        else
            callWebserviceKodi(vlera);
    } catch (e) { }
}

function myValueButtonFshi(elem, operation, value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
    else
        return myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
}

function myElemButtonFshi() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}

function myElemPershkrimi(value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[3];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[3]);
}

function myelemKosto(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[8] ? "True" : "False", indexRow: idRresht, id: "txtKosto", onKeyDown: keyup });
}

function myelemNgaStoku(value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[6];
    return myJQGrid.myElemCheckBox(value, disabled, idRresht, arrayIdKolonaGrides[6]);
}

function myelemComboNjesia(value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[4];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[4]);
}

function myelemKoeficienti(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[5] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtKoeficienti", onKeyDown: keyup, onFocusout: changedKoeficienti });
}

function myelemFiro(value, options) {//po
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
    if (arrayReadOnlyKolonaGrides[7] == 'True' || vleraLlojit == "Aktivitete" || cmbKlasa.GetValue() == 4)
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefte(value, options, disable, idRresht, 'txtFiro', keyup, '0.00', changedFiro);
}

function keyup() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var koeficient = grida.getTekstQelize('txtKoeficienti', idRresht);
    if (koeficient == '.') {
        grida.setTekstQelize('txtKoeficienti', idRresht, '0.'); return;
    }
    var editorKoeficenti = $("#" + 'txtKoeficienti' + idRresht);
    if (editorKoeficenti.val() == "" || isNaN(editorKoeficenti.val())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficDuhetNr"));
        editorKoeficenti.focus();
        //sasia = '1.00';
        //grida.setTekstQelize('txtKoeficienti', idRresht);
    }
    else
        if (editorKoeficenti.val() == "0") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficJoZero"));
            editorKoeficenti.focus();
            //sasia = '1.00';
            //grida.setTekstQelize('txtKoeficienti', idRresht);
        }
}

function fshiClicked(index) {//po
    myJQGrid.fshiClicked(index, '#rowed5', inicializoGride);

    llogaritKoston(index);
}

function callWebserviceKodi(vlera) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
    //var idPerdoruesi = hfState.Get('idPerdoruesi');
    if (vleraLlojit == "Artikull") { //Artikull        
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPerProdhim"),
            data: JSON.stringify({ prefixText: vlera, klasa: cmbKlasa.GetValue() })
        }).done(SucceededCallbackKodi);
        return;
    }
    if (vleraLlojit == "Aktivitete") { //Makro

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheAktivitete"),
            data: JSON.stringify({ prefixText: vlera, idNdermarrje: hfState.Get('idNdermarrje') })
        }).done(SucceededCallbackKodi);
        return;
    }
}

function SucceededCallbackKodi(result) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRresht);
}
function lostFocusKoloneFundit() {     //po 
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    idRresht = $("#rowed5").lostFocusKoloneFundit();
    if (($('#' + arrayIdKolonaGrides[0] + idRresht).attr("disabled") == 'disabled')) {
        $('#txtKodi' + idRresht).focus();
        $('#txtKodi' + idRresht).blur();
        $('#txtKodi' + idRresht).focus();
    }
}
function mbushGrideNgaHiddenFieldet() {
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;

    if (Utils.getUrlVar('veprimi') == "shtim") {
        //myJQGrid.keyPressKodi("#rowed5", window.lastsel2, arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1]);
        return;
    }

}
function merrTeDhenaArt(s, e) {//po

    var grida = $('#rowed5');
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    var total = 0;
    for (i = 0; i < rreshtaTeGrides.length; i++) {

        rreshtaTeGrides[i].txtFshi = "";

    }

    $('#hfArtikujtPerberes').val(JSON.stringify(rreshtaTeGrides));
}
function ButtonClickKodi() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);

    switch (vleraLlojit) {

        case "":
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojinEVeprimit"));
            break;
        case "Artikull":
            identikuesPerPopupArtikulli = 'ArtikullPerberes';
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhArtikullin"), 'LupaArtikull.aspx?klasa=' + cmbKlasa.GetValue() + '&idKonfigAmbjente=' + $("#hfLupaArtikuj")[0].value, 600, 500);

            break;
        case "Aktivitete":
            identikuesPerPopupLlogari = "ArtikullPerberes";
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhAktivitetin"), 'LupaAktivitete.aspx?vjenNga=Shto_Artikull.aspx', 600, 500);
            break;

        default: myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlojiVeprimitIPanjohur"));
            break;
    }



}
function ndryshoImazhin(nr, index) {
    var id = "butonFshi" + index;
    if (document.getElementById(id) != null) {
        if (nr == 1)
            document.getElementById(id).src = "images/blue-square-icon.png";
        else if (nr == 0)
            document.getElementById(id).src = "images/square-icon.png";
    }
}
function changedKoeficienti() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var editorKoeficienti = $("#" + 'txtKoeficienti' + idRresht);
    var gjeresi;
    if (editorKoeficienti.val() == "" || isNaN(editorKoeficienti.val())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficDuhetNr"));
        editorKoeficienti.focus();
        grida.setTekstQelize('txtKoeficienti', idRresht);
        //gjeresi = '1.00';
    }
    else
        if (editorKoeficienti.val() == "0" || editorKoeficienti.val() == "0.") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficJoZero"));
            editorKoeficienti.focus();
            grida.setTekstQelize('txtKoeficienti', idRresht, 1);
        }

    llogaritKoston(-1);
}

function changedFiro() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var editorSasia = $("#" + 'txtFiro' + idRresht);

    var gjeresi;
    if (editorSasia.val() == "" || isNaN(editorSasia.val())) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgFiroNumer"));
        editorSasia.focus();
        gjeresi = '0.00';
    }

    if (parseFloat(editorSasia.val()) < 0 || parseFloat(editorSasia.val()) > 100) {
        editor.SetText(0);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgFiroVlera"));
    }


}
function kontrolloRow(rowid) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var lloji;
    var name;
    if (rowid != idRresht || $('#txtKodi' + idRresht).val() == undefined) {
        var rreshti = grida.jqGrid('getRowData', rowid);
        name = rreshti.txtKodi;
        lloji = rreshti.cmbLloji;
    }
    else {
        lloji = $('#cmbLloji' + idRresht + ' option:selected').text();
        name = $("#txtKodi" + idRresht).val();
    }
    if ($('#hfShtimModifikim').val() != 'shtim') {
        kodArt = txtKodi.GetText();
        if (lloji == 'Artikull' && name == kodArt) {
            resetRreshtKorent(rowid);
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetTeVendoseeVetArtikulli"));
            return;
        }
    }
    var gridIds = grida.jqGrid('getDataIDs');
    for (i = 0; i < gridIds.length; i++) {
        if (rowid == gridIds[i])
            continue;
        var editorkodi;
        var editorlloji;
        var rreshti = grida.jqGrid('getRowData', [gridIds[i]]);
        if (rreshti.txtKodi.toString().search('value') != -1) {
            editorkodi = $("#txtKodi" + idRresht).val();
            editorlloji = $('#cmbLloji' + idRresht + ' option:selected').text();
        }
        else {
            editorkodi = rreshti.txtKodi;
            editorlloji = rreshti.cmbLloji;
        }
        if (editorkodi == name && editorkodi != "" && editorlloji == lloji) {
            resetRreshtKorent(rowid);
            if (lloji == 'Artikull') {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonArtikullGride"));
            }
            else {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonKodiGride"));
            }

            break;
        }
    }
}

function merrTeDhenaNorma() {//merren te dhenat qe ka grida
    var gridDataObject = $('#gridDataObject');
    colNorma = new Array();
    if (Utils.getUrlVar("llojiart") == "aqt") {
        for (i = 0; i < gvAmortizimi.cpRowCount; i++) {
            colNorma[i] = new Object();
            colNorma[i].Standart = Utils.ktheKontroll('lblStandart' + i).GetText();
            colNorma[i].IdLlojAmortizimi = Utils.ktheKontroll('cmbLlojAmortizimi' + i).GetValue();
            colNorma[i].NormeMagazine = Utils.ktheKontroll('cmbNormeMagazine' + i).GetText();
            colNorma[i].Norme = Utils.ktheKontroll('txtNorme' + i).GetText();
        }
    }
    gridDataObject.val(JSON.stringify(colNorma));
}

function merrSkeme(s) {
    if (Utils.getUrlVar("llojiart") == "aqt" && s.GetValue() != null && mbushtedhenasipagrupit) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "merrSkeme"),
            data: JSON.stringify({ idkodifikim: s.GetValue() })
        }).done(SucceededCallbackSkemaPrindi);
    }
    gvAmortizimi.PerformCallback(s.GetValue());
}

function SucceededCallbackSkemaPrindi(result) {
    Utils.SelectComboItem(btneSkema, result.IdSkemaKontabel, null);
    Utils.SelectComboItem(btneLlogInv, result.IdLlogariInventari, null);
    Utils.SelectComboItem(btneLlogTretet, result.IdLlogariNeProces, null);
    Utils.SelectComboItem(btnLlogShpe, result.IdLlogariShpenzimi, null);
    Utils.SelectComboItem(btneLlogBle, result.IdLlogariVlere, null);
    Utils.SelectComboItem(btneLlogShit, result.IdLlogariShitje, null);
    Utils.SelectComboItem(cmbLlogAmortizimi, result.IdLlogariAmortizimi, null);
    Utils.SelectComboItem(btnLlogPakesim, result.IdLlogariPakesimi, null);
}


function kontrolloDetajimLidhur() {
    if ($('#hfShtimModifikim').val() == "modifikim" && !cbDetajim.GetChecked()) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloDetajimLidhur"),
            data: JSON.stringify({ idartikulli: $('#hfId').val() })
        }).done(SucceededCallbackDetajimLidhur);
        return;
    }
    pastroDetajim();
}

function SucceededCallbackDetajimLidhur(result) {
    if (!result) {
        pastroDetajim();
    }
    else {
        cbDetajim.SetChecked(true);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukHiqniDotDetajim"));
    }
}

function pastroDetajim() {
    enableDetajim();
    btnDetajimi1.SetText('');
    btnDetajimi2.SetText('');
    cmbKategoriDetajimi1.SetText('');
    cmbKategoriDetajimi2.SetText('');
}

function enableDetajim() {
    var detajimiChecked = cbDetajim.GetChecked();
    cmbKategoriDetajimi1.SetEnabled(detajimiChecked);
    cmbKategoriDetajimi2.SetEnabled(detajimiChecked);
    btnDetajimi1.SetEnabled(detajimiChecked);
    btnDetajimi2.SetEnabled(detajimiChecked);
}

function KategoriaChanged(lloji) {
    if ($('#hfShtimModifikim').val() == "modifikim" && ((btnDetajimi1.GetText() != "" && lloji == 1) || (btnDetajimi2.GetText() != "" && lloji == 2))) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloDetajimLidhurSipasLlojit"),
            data: JSON.stringify({ idartikulli: $('#hfId').val(), lloji: lloji })
        }).done(SucceededCallbackDetajimLidhurLloji);
        return;
    }
    lloji == 1 ? btnDetajimi1.SetText('') : btnDetajimi2.SetText('');
}

function SucceededCallbackDetajimLidhurLloji(result) {
    if (!result[0]) {
        if (result[2] == 1)
            btnDetajimi1.SetText('');
        else
            btnDetajimi2.SetText('');
    }
    else {
        if (result[2] == 1)
            cmbKategoriDetajimi1.SetValue(result[1]);
        else
            cmbKategoriDetajimi2.SetValue(result[1]);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukNdryshonDotKategoriDetajimi"));
    }
}
function nrLlogariChange() {

}

function llogari_TextChanged() {

}
var editordetajimi;
var editorKategoriDetajimi;
var editorCheckBoxDetajimi;
function DetajimArtikulli_Click(s) {
    var hf = $("#hfLupaDetajimi");
    var veprimi = "";
    var lloji = 0;
    editordetajimi = s;
    editorCheckBoxDetajimi = cbDetajim;
    if (editordetajimi == btnDetajimi1) {
        if (cmbKategoriDetajimi1.GetValue() !== null && cmbKategoriDetajimi1.GetValue() !== "")
            veprimi = cmbKategoriDetajimi1.GetValue();
        lloji = 1;
        editorKategoriDetajimi = cmbKategoriDetajimi1;
    }
    else if (editordetajimi == btnDetajimi2) {
        if (cmbKategoriDetajimi2.GetValue() !== null && cmbKategoriDetajimi2.GetValue() !== "")
            veprimi = cmbKategoriDetajimi2.GetValue();
        lloji = 2;
        editorKategoriDetajimi = cmbKategoriDetajimi2;
    }

    var queryStr = "?lupe=true&idKonfigAmbjente=" + hf.val() + '&kodArtikulli=' + txtKodi.GetText() + '&lloji=' + lloji + '&detajime=' + (lloji == 1 ? btnDetajimi1.GetText() : btnDetajimi2.GetText());

    if (veprimi)
        queryStr += "&veprimi=" + veprimi;

    if (veprimi == 0)
        lloji == 1 ? myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhKategorineDetajimNje")) : myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhKategorineDetajimDy"));
    else
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDetajimArtikulli"), "DetajimeArtikulli.aspx" + queryStr, 800, 560);
}