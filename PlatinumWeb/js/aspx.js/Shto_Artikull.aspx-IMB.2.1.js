;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = false;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
//madhesite e lupave
var widthLupaLlogaria = 750;
var heightLupaLlogaria = 600;
var widthLupaKF = 750;
var heightLupaKF = 600;
var widthLupaArtPerberes = 600;
var heightLupaArtPerberes = 600;
var widthLupaShtoArt = 600;
var heightLupaShtoArt = 600;
var widthLupaKodbare = 1100;
var heightLupaKodbare = 750;
var widthLupaSkema = 1150;
var heightLupaSkema = 600;
var widthLupaDetajime = 800;
var heightLupaDetajime = 560;
var widthLupaKodifikime = 800;
var heightLupaKodifikime = 700;
var widthLupaAutorizime = 600;
var heightLupaAutorizime = 600;

var arrayIdKolonaSubGrides = new Array();
var arrayPershkrimiKolonaSubGrides = new Array();
var arrayVisibleKolonaSubGrides = new Array();
var arrayWidthKolonaSubGrides = new Array();
var arrayReadOnlyKolonaSubGrides = new Array();
var arrayRenditjeKolonaSubGrides = new Array();
var widthLupaAutomjet = 800;
var heightLupaAutomjet = 600;
var identifikuesPerPopupAutomjet = "Shto_Artikull";
var kaNrAutomatik = false;
var colMagazina;

var pageState = {
    gridaSelector: "#rowed6",
    teDhenaImporti: null
};

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
   
    $(window).on('load', function () {
        Init();
    });


    $(window).on("resize", function () {//po
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
        }
        var grida = $('#rowed5');
        if ($('#divgride2').width() != null) {
            myJQGrid.fixGridWidth(grida, $('#divgride2'));
        }
        if ($('#divgrideGjendje2').width() != null) {
            myJQGrid.fixGridWidth($('#rowed6'), $('#divgrideGjendje2'));
        }
        if ($('#divgride12').width() != null) {
            myJQGrid.fixGridWidth($('#rowed7'), $('#divgride12'));
        }
    }).trigger("resize");
});

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
        for (var i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1); i++) {
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
            Utils.formatoTextBox(Utils.ktheKontroll('CmimiTvsh' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMin' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMax' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kursi' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kosto' + i));
        }
    else {
        for (var i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
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
            Utils.formatoTextBox(Utils.ktheKontroll('CmimiTvsh' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMin' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('SasiMax' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kursi' + i));
            Utils.formatoTextBox(Utils.ktheKontroll('Kosto' + i));
        }
    }
}

function gjejFormatSipasMonedhes(idmonedha) {
    var formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    for (var i = 0; i < formatNumri.KonfigTrupi.length; i++)
        if (formatNumri.KonfigTrupi[i].IdMonedha == idmonedha) {
            return formatNumri.KonfigTrupi[i];
        }
    return formatNumri.KonfigTrupi[0];
}

var arrformatevlefta = new Array();
var arrformatesasia = new Array();
var arrformatecmimi = new Array();
var arrformatekursi = new Array();

function unformatoFushaDevi() {
    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1)) {
        for (var i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1); i++) {
            arrformatecmimi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Cmimi' + i));
            arrformatesasia[i] = Utils.getFormatNumri(Utils.ktheKontroll('SasiMin' + i));
            arrformatekursi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Kursi' + i));
        }
    }
    else {
        for (var i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            arrformatecmimi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Cmimi' + i));
            arrformatesasia[i] = Utils.getFormatNumri(Utils.ktheKontroll('SasiMin' + i));
            arrformatekursi[i] = Utils.getFormatNumri(Utils.ktheKontroll('Kursi' + i));
        }
    }
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function clickExport(e) {
    if (ASPxGridView_Artikull.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Artikull, "402", cmbKonfigurimi.GetText());
}

function gridFocusRowCanged(s, e) {
    mbush = true;
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    colNorma = new Array(); //pastrohet array i artikujve perberes.
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

function textChangedKodbari(s, e) {
    var arrayKodbaretOld;
    //rasti kur modifikojme nje artikull 
    if ($('#hfKodbaret').val() != '') {
        arrayKodbaretOld = JSON.parse($('#hfKodbaret').val());
    }
        //rasti kur celim nje artikull dhe ai nuk ka kodbar 
    else if ($('#hfKodbaret').val() == '') {
        arrayKodbaretOld = [];
    }
    var kodbaret = btneKodbari.GetText().split(',');
    var ArrayKodbaret = new Array();
    for (var i = 0; i < kodbaret.length; i++) {
        if (kodbaret[i] == '')
            continue;
        var barkodi = kodbaret[i].replace(/\s/g, '');        
        ArrayKodbaret[i] = new Object();
        var ind = Utils.findArrayIndexByAttrValue(arrayKodbaretOld, 'pershkrimi', barkodi);
        if (ind != -1)
            ArrayKodbaret[i] = arrayKodbaretOld[ind];
        else
            ArrayKodbaret[i] = { indeksi: i, pershkrimi: barkodi, njesia: 1, detajimi1:0, detajimi2:0 };
    }
    $('#hfKodbaret').val(JSON.stringify(ArrayKodbaret));
}

function tabsActiveTabChanged(s, e) {
    indexModifiko = ASPxGridView_Artikull.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko !== -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);
            $('#hfFormatSeriali').val(0);
            pastrofusha();
            //                SucceededCallbackKonfigurimiInit(resultkonf);
            //                    SucceededCallbackKonfig(resultkonf);

        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Artikull, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Artikull, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Artikull, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Artikull, indexSel);
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
    //            var hfKontrollet = $('#hfKontrollet');
    var ruajbuxhetet = false; //i here per i here
    switch (e.item.name) {
        case "Fshi":
            e.processOnServer = false;
            Utils.konfirmoFshirje(popFshi, lblMsgbox, ASPxGridView_Artikull.GetSelectedRowCount(), hfState.Get("msgnumRreshtashSelektuar"), hfState.Get("msgShtoArtikullZgjidhNjeArtikull"));
            break;
        case "Arkiva":
            ButtonClickArkiva();
            e.processOnServer = false;
            break;
        case "Sinkronizo":
            e.processOnServer = false;
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpTextZgjidhNdermarrjet"), 'LupaNdermarjeBij.aspx?vjenNga=Artikulli', 500, 500);
            break;
        case "Importo":

            pageState.teDhenaImporti = {
                tipi: 'XLS',
                idKategoria: 13,
                formatImporti: Utils.getUrlVar('llojiart') == "afatshkurter" ? "Format Standart Artikulli" : "Format Standart Artikull Afatgjate"
            };
            myButtonClickLupa.LupaUniversal_Click("Import", "Import.aspx", 880, 600);
            break;
        default:
            myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, undefined, indexModifiko, pastrofusha, vendosKonfig, resultkonf, colKontrollet, aktivFusha, colAtrTrupi);
            if (e.item.name === "Klono" && kaNrAutomatik) {
                hfNrAuto.Clear();
                hfNrAutoKF.Clear();
                vendosNrAutomatik(colAtrTrupi, colKontrollet);
            }
            if (e.item.name === 'Ruaj') {
                if (!myFaqeCelje.validim(s, e))
                    return;
                Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
                if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
                    e.processOnServer = false;
                    return;
                }
                if (Utils.PermbanKaraktereSpeciale(txtPershkrimi.GetText())) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPershkrimiNukDuhetTePermbajeKetoKaraktere") + " <,>");
                    e.processOnServer = false;
                    return;
                }
                if (btneKodbari.GetText().indexOf('+') != -1) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgZevendesimPlusi"));
                    e.processOnServer = false;
                    return;
                }
                if (cbIRimbursueshem.GetChecked() && (Utils.IsNullOrEmpty(txtKodiIBarit.GetText()) || Utils.IsNullOrWhiteSpace(txtKodiIBarit.GetText()))) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("MsgNdaluesPlotesoKodinEBarit"));
                    e.processOnServer = false;
                    return;
                }

                //  merrTeDhena();
                if (hfTeDrejtaCmimi.Get("Amb") == true) {
                    merrTeDhenat();
                }
                ruajFushaShtese();
                merrTeDhenaNorma();
                merrTeDhenaNormaAmortizimi();

                grida.jqGrid('saveRow', idRresht, false, 'clientArray');
                var grida1 = $('#rowed6');
                var grida2 = $('#rowed7');
                var idRow = grida1.getLastSel2();
                var idRow2 = grida2.getLastSel2();
                grida1.jqGrid('saveRow', idRow, false, 'clientArray');
                grida2.jqGrid('saveRow', idRow2, false, 'clientArray');
                grida.setLastSel2(0);
                grida1.setLastSel2(0);
                grida2.setLastSel2(0);
                if (cmbKlasa.GetValue() == 4 || cmbKlasa.GetValue() == 5 || cmbKlasa.GetValue() == 6)
                    merrTeDhenaArt(s, e);
                if (cbDhurate.GetChecked())
                    merrTeDhenaVfone(s, e);
                else
                    $('#hfVfone').val('');
                merrGjendjeArt(s, e);
                pastro();
            }
            break;
    }
}
function LostFocusKodifikim(s)
{
    var kod = Utils.findInArray(colAtrTrupi, function (item) { return item.IdKontroll == Utils.findInArray(colKontrollet, function (item) { return item.KodKontrolli == s.globalName; }).IdKontrolli; });
    if (s.GetValue() == "0" && kod.Detyrueshme)
        s.SetValue(null);
}

function ButtonClickArkiva() {
    var idObjekti = ($('#hfShtimModifikim').val() != "shtim" || Utils.IsNullOrEmpty($('#hfShtimModifikim').val()) || PageControl.GetActiveTab().index == 0) ? ASPxGridView_Artikull.GetRowKey(ASPxGridView_Artikull.GetFocusedRowIndex()) : 0;
    if (idObjekti == -1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoArtikullZgjidhArt"));
        return;
    }

    Utils.hapLupe({ emerPopUpi: popupUniversal, titull: hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"), baseUrl: "LupaArkiva.aspx", width: 738, height: 548, params: { vjenNga: "Lista", veprimi: "artikull", idDok: idObjekti, tmpfolder: hfArkiva.Get("rootFolder") } });
}

function cmbKlasaIndexChanged(s, e) {
    cbProdhimMePorosi.SetChecked(false);
    cbRezervueshem.SetChecked(false);
    var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true');
    enable(true, isLidhur, false);
    btneLlogInv.SetSelectedIndex(-1);
    btneLlogInv.SetText('');
    btneLlogTretet.SetSelectedIndex(-1);
    btneLlogTretet.SetText('');
    btnLlogShpe.SetSelectedIndex(-1);
    btnLlogShpe.SetText('');
    cmbLlogAmortizimi.SetSelectedIndex(-1);

    btneLlogBle.SetSelectedIndex(-1);
    btneLlogBle.SetText('');
    btneLlogShit.SetSelectedIndex(-1);
    btneLlogShit.SetText('');
    btnLlogPakesim.SetSelectedIndex(-1);
    btnLlogPakesim.SetText('');
    btneLlogPakesimRez.SetSelectedIndex(-1);
    btneLlogPakesimRez.SetText('');
    btneLlogRez.SetSelectedIndex(-1);
    btneLlogRez.SetText('');
    btneSkema.SetSelectedIndex(-1);
    txtSasiNjesi.SetText(1.00);
    txtScrap.SetText(0.00);
}

function SucceededCallbackKodArtikulli(result) {
    cmbLidhMeNdermRap.SetText(result);
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = ASPxGridView_Artikull.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoArtikullZgjidhArt"));
    else
        ASPxGridView_Artikull.GetRowValues(indexModifiko, 'IdArtikulli;KodArtikulli;PershkrimArtikulli;PershkrimiAngArtikulli;KodiDoganorArtikulli;VendodhjeArtikulli;KodKodifikimi1;KodKodifikimi2;OrigjineArtikulli;KodNjesia1;KodNjesia2;KoeficientArtikulli;KodFurnitori;PeshaBrutoArtikulli;PeshaNetoArtikulli;DetajimArtikulli;PershkrimKlasa;KodSkema;NrLlogInventari;NrLlogBlerje;NrLlogShitje;NrLlogTeTrete;NrLlogShpenzime;MinimumArtikulli;MaximumArtikulli;MetodeKostojeArtikulli;LlogaritjaKMSHArtikulli;ZevendesimAutomatikArtikulli;KontrollGjendje;KontrollCmimi;KontrollGjendjeArtikulli;IdTvsh;Kodbari;Aktiv;LlojiArt;Kodifikimi1Artikulli;Kodifikimi2Artikulli;IdFurnitoriKryesor;Njesi1Artikulli;Njesi2Artikulli;Klasa;IdSkemaKontabilitetiArtikulli;IdLlogariInventari;IdLlogariBlerje;IdLlogariShitje;IdLlogariTeTrete;IdLlogariShpenzime;IdLlogariAmortizimi;NrLlogAmortizimi;EmertimFurnitori;SasiNjesi;Scrap;ProdhimMePorosi;IdKategoriDetajimi;IdKategoriDetajimi2;KontrollGjendjeDetajim2;IdObjektivaKosto;Objektiva;IdLlojGarancia;Garancia;IdMagazina;Magazina;IRezervueshem;PerTransferim;Loan;Dhurate;AplikimDhurate;Pike;Vlere;KodVFOne;MeSerial;IShitshem;MbetjeShitshme;IdLlogariPakesimi;NrLlogPakesimi;IdArtRaportuesi;Autorizimet;Njesia;PerPeshore;PershkrimTeFurnitori;SiperfaqjaM2;NrKontrate;NrPasurie;ZonaKadastrale;Shasia;Marka;Modeli;VitProdhimi;TeDhenaTeknike;MeBarkodLogjik;SkemaBarkodit;KodKodifikimi3;Kodifikimi3Artikulli;Detajim1;Detajim2;AparatBazaar;KodOferte;ArtikullIVjeter;IdKategoriSeriali;IdLlogariRezerve;NrLlogRez;IdLlogariPakesimRezerve;NrLlogPakRez;MeRezerveRivleresimi;KaraktereTAC;LLOGARITKOMISION;LlogariKomisioni;NrLlogariKomisioni;StokuMaxVfOne;KodiIBarit;IRimbursueshem', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    txtKodi.GetMainElement().style.borderColor = '';
    //if ($('#hfShtimModifikim').val() !== "klonim")
        $('#hfId').val(values[0]);
    HfFushaShtese.Set("idRreshti", values[0]);
    if(values[76]!=null && values[76]!="")
        cmbAutorizimi.SetValue(values[76].split(','));
    else cmbAutorizimi.SetValue(null);
    if ($('#hfShtimModifikim').val() !== "klonim" || !kaNrAutomatik)
        txtKodi.SetText(values[1]);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "merrTeDhenaArtikulli"),
        data: JSON.stringify({
            idkomp: "402",
            kodkonfi: cmbKonfigurimi.GetText(),
            idArtikulli: values[0],
            merrCmime: (hfTeDrejtaCmimi.Get("Amb") == true || hfTeDrejtaCmimi.Get("Amb") == 'true') ? true : false,
            merrCmimeBlerje: (hfKushtet.Get("CB") == "Po"),
            idndermarje:  hfState.Get('idNdermarrje'),
            gjuhe: hfState.Get('idGjuha'),
            idPerdorues: hfState.Get('idPerdoruesi'),
            kodArtikulli :txtKodi.GetText()
        })
    }).done(function (result) { SucceededCallbackTeDhenaArtikulli(result, values[0]); });

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulli"),
        data: JSON.stringify({ idartikulli: values[0] })
    }).done(SucceededCallbackGjendjeArtikulli);

    txtPershkrimi.SetText(values[2]);
    txtPershkrimiAng.SetText(values[3]);
    if (values[31] != null)
        cmbNivelTvsh.SetValue(values[31]);
    else
        cmbNivelTvsh.SetSelectedIndex(-1);
    txtVendodhja.SetText(values[5]);
    txtKodiDoganor.SetText(values[4]);
    txtOrigjina.SetText(values[8]);
    if (values[35] != null) {
        Utils.SelectComboItem(btneKodifikimi1, values[35], values[6]);
    }
    else
        btneKodifikimi1.SetSelectedIndex(-1);
    if (values[36] != null) {
        Utils.SelectComboItem(btneKodifikimi2, values[36], values[7]);
    }
    else
        btneKodifikimi2.SetSelectedIndex(-1);
    if (values[92] != null) {
        Utils.SelectComboItem(btneKodifikimi3, values[92], values[91]);
    }
    else
        btneKodifikimi3.SetSelectedIndex(-1);
    cbDetajim.SetChecked(values[15]);
    if (values[38] != null) {
        var itemFound = cmbNjesia1.FindItemByValue(values[38]);
        if (itemFound != null)
            cmbNjesia1.SetValue(values[38]);
        else
            cmbNjesia1.SetSelectedIndex(cmbNjesia1.AddItem(values[9], values[38]));
    }
    else
        cmbNjesia1.SetSelectedIndex(-1);
    if (values[39] != null) {
        var itemFound = cmbNjesia2.FindItemByValue(values[39]);
        if (itemFound != null)
            cmbNjesia2.SetValue(values[39]);
        else
            cmbNjesia2.SetSelectedIndex(cmbNjesia2.AddItem(values[10], values[39]));
    }
    else
        cmbNjesia2.SetSelectedIndex(-1);
    txtKoeficienti.SetText(values[11]);
    if (values[37] != null) {
        Utils.SelectComboItem(txtFurnitori, values[37], values[12], values[49]);
    }
    else
        txtFurnitori.SetSelectedIndex(-1);

    if (values[13] != 0)
        txtPeshaBruto.SetText(values[13]);
    else txtPeshaBruto.SetText('');
    if (values[14] != 0)
        txtPeshaNeto.SetText(values[14]);
    else txtPeshaNeto.SetText('');
    checkKontrollGjendje.SetChecked(values[28]);
    checkKontrollGjendjeArtikulli.SetChecked(values[30]);
    checkKontrollCmimi.SetChecked(values[29]);
    txtKodi2.SetText(values[1]);
    txtPershkrimi2.SetText(values[2]);
    txtKodi3.SetText(values[1]);
    txtPershkrimi3.SetText(values[2]);
    Utils.SelectComboItem(cmbKlasa, values[40], values[16]);
    Utils.SelectComboItem(btneSkema, values[41], values[17]);
    Utils.SelectComboItem(btneLlogInv, values[42], values[18]);
    Utils.SelectComboItem(btneLlogBle, values[43], values[19]);
    Utils.SelectComboItem(btneLlogShit, values[44], values[20]);
    Utils.SelectComboItem(btneLlogTretet, values[45], values[21]);
    Utils.SelectComboItem(btnLlogShpe, values[46], values[22]);
    Utils.SelectComboItem(cmbLlogAmortizimi, values[47], values[48]);
    Utils.SelectComboItem(btnLlogPakesim, values[73], values[74]);
    if (values[106] != 0 || values[107] != null)
        Utils.SelectComboItem(btnLLogariKomisioni, values[106], values[107]);
    else
        btnLLogariKomisioni.SetText("");
    txtSasiNjesi.SetText(values[50]);
    txtScrap.SetText(values[51]);
    if (cmbKlasa.GetValue() !== "5" && cmbKlasa.GetValue() !== "1")
    { cbRezervueshem.SetEnabled(false); }
    else { cbRezervueshem.SetEnabled(true); }
    if (cmbKlasa.GetValue() !== "5")
    { cbProdhimMePorosi.SetVisible(false); lblProdhimMePorosi.SetVisible(false); }
    else { cbProdhimMePorosi.SetVisible(true); lblProdhimMePorosi.SetVisible(true); }
    if (cmbKlasa.GetValue() === "4" || cmbKlasa.GetValue() === "5" || cmbKlasa.GetValue() === "6") {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrDataNdryshimiArtPerberes"),
            data: JSON.stringify({ idartikulli: values[0] })
        }).done(SucceededCallbackData);
    }
    cbProdhimMePorosi.SetChecked(values[52]);
    if (values[23] != 0)
        txtMinimumi.SetText(values[23]);
    else txtMinimumi.SetText('');
    if (values[24] != 0)
        txtMaximumi.SetText(values[24]);
    else txtMaximumi.SetText('');
    Utils.SelectComboItem(cmbMetode, values[25]);
    var selectedMetode = cmbMetode.GetSelectedItem();
    if (selectedMetode !== null) {
        lblMetodePershkrimi.SetText(selectedMetode.GetColumnText('Pershkrimi'));
        memoNdihma.SetText(selectedMetode.GetColumnText('Shpjegimi'));
    }
    Utils.SelectComboItem(cmbKMSH, values[26]);
    Utils.SelectComboItem(cmbZevendesim, values[27]);
    txtKodi4.SetText(values[1]);
    txtPershkrimi4.SetText(values[2]);
    txtKodi5.SetText(values[1]);
    txtPershkrimi5.SetText(values[2]);
    cbAktiv.SetChecked(values[33]);
    cbMeSerial.SetChecked(values[70]);
    btnSerial.SetEnabled(cbMeSerial.GetChecked());
    cmbLloji.SetValue(values[34].toString());
    cmbKategoriDetajimi.SetValue(values[53]);
    cmbKategoriDetajimi2.SetValue(values[54]);
    checkKontrollGjendjeDetajim2.SetChecked(values[55]);
    cmbObjektiva.SetSelectedIndex(cmbObjektiva.AddItem(values[57], values[56]));
    if (values[58] != null) {
        cmbGarancia.SetValue(values[58]);
        txtGarancia.SetText(values[59]);
    }
    else {
        cmbGarancia.SetSelectedIndex(-1);
        txtGarancia.SetText('');
    }
    if (values[60] != null) {
        btnMagazina.SetValue(values[60]);
        btnMagazina.SetText(values[61]);
    }
    else {
        btnMagazina.SetSelectedIndex(-1);
        btnMagazina.SetValue(null);
    }
    cbRezervueshem.SetChecked(values[62]);
    cbPerTransferim.SetChecked(values[63]);
    cbLoan.SetChecked(values[64]);
    cbDhurate.SetChecked(values[65]);
    cmbAplikim.SetValue(values[66]);
    txtPike.SetText(values[67]);
    txtVlere.SetText(values[68]);
    txtKodVFOne.SetText(values[69]);
    cbShitshem.SetChecked(values[71]);
    cbMbetjeShitshem.SetChecked(values[72]);
    var ArrayKodbaret = new Array();
    if (values[32] != null && values[32] != '' && $('#hfShtimModifikim').val() !== "klonim") {
        btneKodbari.SetText(values[32]);
        var kodbaret = values[32].toString().split(',');
        var njesite = values[77].toString().split(',');
        var detajim1te = values[93].toString().split(',');
        var detajim2te = values[94].toString().split(',');
        for (var j = 0; j < kodbaret.length; j++) {
            if (kodbaret[j] == '')
                continue;
            var barkodi = kodbaret[j].replace(/\s/g, '');
            var njesia = njesite[j].replace(/\s/g, '');
            if (detajim1te.length > j)
                var detajim1 = detajim1te[j].replace(/\s/g, '');
            else detajim1 = 0;
            if (detajim2te.length > j)
                var detajim2 = detajim2te[j].replace(/\s/g, '');
            else detajim2 = 0;
            ArrayKodbaret[j] = new Object();
            ArrayKodbaret[j] = { indeksi: j, pershkrimi: barkodi, njesia: njesia,detajimi1:detajim1,detajimi2:detajim2 };
        }
        $('#hfKodbare').val(JSON.stringify(ArrayKodbaret));
    }
    else
        btneKodbari.SetText('');
    if (values[75])
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKodArtikulliSipasId"),
            data: JSON.stringify({ idArtikulli: values[75] })
        }).done(SucceededCallbackKodArtikulli);
    else {
        cmbLidhMeNdermRap.SetValue(null);
    }
    cbPerPershore.SetChecked(values[78]);
    txtPershkrimiFurnitori.SetText(values[79]);
    txtSiperfaqjaM2.SetText(values[80]);
    txtNrKontrate.SetText(values[81]);
    txtNrPasurie.SetText(values[82]);
    txtZonaKadastrale.SetText(values[83]);
    txtShasia.SetText(values[84]);
    txtMarka.SetText(values[85]);
    txtModeli.SetText(values[86]);
    txtVitProdhimi.SetText(values[87]);
    txtTeDhenaTeknika.SetText(values[88]);
    cbMeBarkodLogjik.SetChecked(values[89]);
    txtSkemeBarkodi.SetText(values[90]);
    chkbAparatBazaar.SetChecked(values[95]);
    txtKodOferte.SetText(values[96]);
    cbArtikullVjeter.SetChecked(values[97]);
    cmbFormatSeriali.SetValue(values[98]);
    if (values[98] == null)
        $('#hfFormatSeriali').val(0);
    else $('#hfFormatSeriali').val(values[98]);
    cbRezRivleresimi.SetChecked(values[103]);
    Utils.SelectComboItem(btneLlogRez, values[99], values[100]);
    Utils.SelectComboItem(btneLlogPakesimRez, values[101], values[102]);
    txtKaraktereTAC.SetText(values[104]);
    cbLLogaritKomision.SetChecked(values[105]);
    txtStokuMaxVfOne.SetText(values[108]);
    txtKodiIBarit.SetText(values[109]);
    cbIRimbursueshem.SetChecked(values[110]);
    RefreshFushatShtese($('#hfId').val(), 'mod');
    $('#hfKodbaret').val(JSON.stringify(ArrayKodbaret));
    ShfaqGrideNormaAmortizimi();
    enableDetajim(true);
    Dhurate(true);
    Aplikim();
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

var arrayIdKolonaGridesV = new Array();
var arrayPershkrimiKolonaGridesV = new Array();
var arrayVisibleKolonaGridesV = new Array();
var arrayWidthKolonaGridesV = new Array();
var arrayReadOnlyKolonaGridesV = new Array();
var arrayRenditjeKolonaGridesV = new Array();
function SucceededCallbackTeDhenaArtikulli(result, idObjekti) {
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    if (!result) {
        console.log("GABIM: Nuk u morren te dhenat a eshte i lidhur, artikujt perberes dhe cmimet e artikullit!");
        return;
    }
    if (result.cmimet)
        SucceededCallbackCmime(result.cmimet);
    SucceededCallbackLidhur(result.lidhur, idObjekti);
    SucceededCallbackArtPerberes2(result.artPerberes, true);
    SucceededCallbackDetajime(result.detajimet);
    SucceededCallbackDataNorma(result.dataNorma);
    SucceededCallbackArtvfone(result.artVFOne);
}
function SucceededCallbackDataNorma(result) {
    cmbNdryshim2.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbNdryshim2.AddItem(result[i], result[i]); //AddItem(teksti, vlera);
    cmbNdryshim2.SetSelectedIndex(0);
    if (cmbNdryshim2.GetText() !== "")
        dteDateAk2.SetDate(new Date(cmbNdryshim2.GetText().split('/')[1] + '/' + cmbNdryshim2.GetText().split('/')[0] + '/' + cmbNdryshim2.GetText().split('/')[2]));
    else {
        var data = new Date().getDate() + '/' + parseInt(new Date().getMonth() + 1) + '/' + new Date().getFullYear();
        dteDateAk2.SetDate(new Date('1/1/2000'));
        //        cmbNdryshimi.SetText(data);
    }
    gvAmortizimi.PerformCallback();
    gvNormaAmortizimi.PerformCallback();
}
function gvAmortizimiDateChanged(s, e) {
    gvAmortizimi.PerformCallback();
    gvNormaAmortizimi.PerformCallback();
   
}
function gvAmortizimiSelectedIndexChanged(s, e) {
    dteDateAk2.SetDate(new Date(cmbNdryshim2.GetText().split('/')[1] + '/' + cmbNdryshim2.GetText().split('/')[0] + '/' + cmbNdryshim2.GetText().split('/')[2]));
  

    gvAmortizimi.PerformCallback();
    gvNormaAmortizimi.PerformCallback();
 
   
}

function ShfaqGrideNormaAmortizimi( ) {
    if (cbRezRivleresimi.GetChecked()) {
        gvNormaAmortizimi.SetVisible(true);
        if ($('#hfShtimModifikim').val() == "modifikim" || $('#hfShtimModifikim').val() === "klonim") gvNormaAmortizimi.PerformCallback();
        else
            gvNormaAmortizimi.PerformCallback(btneKodifikimi1.GetValue());
    }
    else gvNormaAmortizimi.SetVisible(false);

}
function Dhurate(krijoGride) {
    if (cbDhurate.GetChecked()) {
        cmbAplikim.SetEnabled(true); if (cmbAplikim.GetText() == "") cmbAplikim.SetValue(1);
        txtPike.SetEnabled(true); txtKodVFOne.SetEnabled(true);
        if (krijoGride) {
            var grida = $('#rowed7');
           grida.setLastSel2(-1);
      
            jQuery("#rowed7").jqGrid('GridUnload', "rowed7");
            formGridColsArrayV($('#hfMeme').val() == "True" ? false : true);
            ruajFormatetNeGrideVfOne(grida);
            inicializoGrideVfone($('#hfMeme').val() == "True" ? false : true);
          

        } myJQGrid.fixGridWidth($('#rowed7'), $('#dvArtikulli'));
        $("#divgride11").show();
      
    }
    else {
        cmbAplikim.SetEnabled(false); cmbAplikim.SetText('');
        txtPike.SetEnabled(false); txtPike.SetText(0);
        txtKodVFOne.SetEnabled(false); txtKodVFOne.SetText('');
        txtVlere.SetEnabled(false); txtVlere.SetText(0);
        $("#divgride11").hide();
  
    }
}

function Aplikim() {
    if (cmbAplikim.GetValue() == 1) {
        txtVlere.SetEnabled(false); txtVlere.SetText(0);
    }
    else txtVlere.SetEnabled(true);
}

function SucceededCallbackDetajime(result) {
    if (!result || $('#hfShtimModifikim').val() === "klonim") {
        btnDetajim1Nga.SetText('');
        btnDetajim2Nga.SetText('');
        return;
    }

    btnDetajim1Nga.SetText(result.detajime1);
    btnDetajim2Nga.SetText(result.detajime2);
}

function SucceededCallbackData(result) {
    cmbNdryshimi.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbNdryshimi.AddItem(result[i], result[i]); //AddItem(teksti, vlera);
    cmbNdryshimi.SetSelectedIndex(0);
    if (cmbNdryshimi.GetText() !== "")
        dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));
    else  {
        var data = new Date();
        dteDateAkt.SetDate(new Date(data.toDateString()));
    }

}

function SucceededCallbackArtPerberes2(result, riKrijoGride) { //pati

    var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true');

    if (riKrijoGride)
        enable(true, isLidhur, false);
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
   
    if ($('#hfShtimModifikim').val() == "modifikim" || $('#hfShtimModifikim').val() === "klonim") {
        idRresht = 0;
        var colTrup = result[0];
        var colArt = result[1];
        var colMakro = result[2];
        var colkosto = result[3];
        grida.setLastSel2(1);
        var shifraPasPresjes = parseInt(hfFormatNumri.Get("ShifraPasPresjesSasia"));
        grida.jqGrid('clearGridData');
        var lloji, kodi, pershkrimi, njesia, koeficienti, kosto, firo, ngastoku, idkodi, be;

        if (colArt.length == 0) {
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || isLidhur)
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
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || isLidhur)
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
            if (colTrup[i].Lloji == 1) {
                lloji = "Artikull";
                kodi = colArt[i].KodArtikulli;
                arrArtikull[idRresht] = colArt[i];
                pershkrimi = colArt[i].PershkrimArtikulli;
                njesia = colArt[i].KodNjesia1 == null ? "" : colArt[i].KodNjesia1;
                idkodi = colArt[i].IdArtikulli;
                kosto = colkosto[i].toFixed(2);
            }
            else if (colTrup[i].Lloji == 2) {
                lloji = "Aktivitete";
                kodi = colMakro[i].Kodi;
                pershkrimi = colMakro[i].Emertimi;
                idkodi = colMakro[i].IdKoka;
                njesia = (colMakro[i].NjesiKohe == 1 ? 'sec' : colMakro[i].NjesiKohe == 2 ? 'min' : colMakro[i].NjesiKohe == 3 ? 'ore' : 'dite');
                kosto = colkosto[i].toFixed(2);
                //kosto = '';
            }

            koeficienti = colTrup[i].Koeficienti == null ? "" : colTrup[i].Koeficienti;
            firo = colTrup[i].Scrap.toFixed(2) == null ? "" : colTrup[i].Scrap.toFixed(2);

            var datarow = {
                cmbLloji: lloji, txtIdKodi: idkodi, txtKodi: kodi, txtPershkrimi: pershkrimi, txtNjesia: njesia, txtKoeficienti: koeficienti, txtFiro: firo, txtKosto: kosto, txtFshi: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
    }
}

function SucceededCallbackGjendjeArtikulli(colGjendjeArt) {
    
    var isLidhur = false;
    enable(false, isLidhur, true);
    if ($('#hfShtimModifikim').val() == "modifikim" || $('#hfShtimModifikim').val() === "klonim") {
        idRow = 1;
        var grida = $('#rowed6');
        var shifraPasPresjes = parseInt(hfFormatNumri.Get("ShifraPasPresjesSasia"));
      grida.jqGrid('clearGridData');
        var gjendjaMax, gjendjaMin, idMagazina, be;

        if (colGjendjeArt == undefined || colGjendjeArt.length == 0) {
            if (arrayReadOnlyKolonaGridesGjendjeArt[arrayReadOnlyKolonaGridesGjendjeArt - 1] == 'True' || isLidhur)
                be = myJQGrid.myValueButtonFshi(true, idRow, "#rowed6");
            else
                be = myJQGrid.myValueButtonFshi(false, idRow, "#rowed6");
            var datarow = {
                txtFshiR: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRow), datarow);

            grida.setLastSel2(idRow + 1);

            return;
        }
        for (var i = 0; i < colGjendjeArt.length; i++) {
            be = myJQGrid.myValueButtonFshi(true, idRow, "#rowed6");
            gjendjaMax = colGjendjeArt[i].GjendjaMax.toFixed(2);
            gjendjaMin = colGjendjeArt[i].GjendjaMin.toFixed(2);
            idMagazina = colGjendjeArt[i].IdMagazina;
            magazina = colGjendjeArt[i].Magazina;
            //idArtikulli = colGjendjeArt[i].IdArtikulli;
            var datarow = {
                txtMagazina: magazina, txtGjendjaMin: gjendjaMin, txtGjendjaMax: gjendjaMax, txtFshiR: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRow), datarow);
            idRow = idRow + 1;
            grida.setLastSel2(idRow );
        }
    }
}
function SucceededCallbackArtvfone(result) { //pati

    var grida = $('#rowed7');
    var idRresht = grida.getLastSel2();
    var be;
    var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true');

    if ($('#hfShtimModifikim').val() == "modifikim" || $('#hfShtimModifikim').val() === "klonim") {
        var colTrup = result;
        grida.setLastSel2(-1);
        grida.jqGrid('clearGridData');
        grida.setLastSel2(1);
        idRresht = 1;
       // myJQGrid.fixGridWidth(grida, $('#dvArtikulli'));
        var shifraPasPresjes = parseInt(hfFormatNumri.Get("ShifraPasPresjesSasia"));
      
        var kodi, pike, vlera;

        if (colTrup.length == 0) {
            if (arrayReadOnlyKolonaGridesV[arrayReadOnlyKolonaGridesV - 1] == 'True' || isLidhur || $('#hfMeme').val() == "False")
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed7");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed7");
            var datarow = {
                txtFshiZ: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
            return;
        }
        for (var i = 0; i < colTrup.length; i++) {
            if (arrayReadOnlyKolonaGridesV[arrayReadOnlyKolonaGridesV - 1] == 'True' || isLidhur || $('#hfMeme').val() == "False")
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed7");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed7");

            kodi = colTrup[i].KodVfone;
            pike = colTrup[i].Pike.toFixed(shifraPasPresjes);
            vlere = colTrup[i].Vlere.toFixed(shifraPasPresjes);



            var datarow = {
                txtKodVfone: kodi, txtPike: pike, txtVlere: vlere, txtFshiZ: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
    }
}
function changeDate(s, e) {
    dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikujPerberesSipasDates"),
        data: JSON.stringify({ id: $('#hfId').val(), data: s.GetText() })
    }).done(function (result) { SucceededCallbackArtPerberes2(result, true); });
    
    var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true');
    enable(true, isLidhur, false);
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
    
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

var colArt;

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.GetMainElement().style.borderColor = '';
    enable(true, false, true);
    btnMagazina.SetValue(null);
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    txtPershkrimiFurnitori.SetText('');
    btneKodbari.SetText('');
    txtPershkrimiAng.SetText('');
    cmbNivelTvsh.SetSelectedIndex(-1);
    txtVendodhja.SetText('');
    txtKodiDoganor.SetText('');
    txtOrigjina.SetText('');
    btneKodifikimi1.SetSelectedIndex(-1);
    btneKodifikimi2.SetSelectedIndex(-1);
    btneKodifikimi3.SetSelectedIndex(-1);
    cmbNdryshim2.ClearItems();
    cmbNdryshim2.SetText('');
    dteDateAk2.SetDate(new Date('1/1/2000'));
    cbAktiv.SetChecked(true);
    if (Utils.getUrlVar('llojiart') == 'afatshkurter')
        cbMeSerial.SetChecked(false);
    else cbMeSerial.SetChecked(true);
    cbDetajim.SetChecked(false);
    cbPerPershore.SetChecked(false);
    cmbNjesia1.SetSelectedItem(cmbNjesia1.FindItemByText('cope'));
    cmbNjesia2.SetSelectedItem(cmbNjesia2.FindItemByText('cope'));
    txtFurnitori.SetSelectedIndex(-1);
    $('#hfLidhur').val('False');
    cmbKategoriDetajimi.SetText('');
    cmbKategoriDetajimi2.SetText('');
    txtPeshaBruto.SetText('');
    btnDetajim1Nga.SetText('');
    btnDetajim2Nga.SetText('');
    txtPeshaNeto.SetText('');
    checkKontrollGjendje.SetChecked(false);
    checkKontrollGjendjeDetajim2.SetChecked(false);
    checkKontrollGjendjeArtikulli.SetChecked(true);
    checkKontrollCmimi.SetChecked(false);
    txtKodi2.SetText('');
    txtPershkrimi2.SetText('');
    txtKodi3.SetText('');
    txtPershkrimi3.SetText('');
    cmbObjektiva.SetSelectedIndex(-1);
    cmbObjektiva.SetValue(null);
    Utils.SelectComboItem(cmbKlasa, undefined, 'Inventar');
    btneSkema.SetValue(null);
    btneLlogInv.SetSelectedIndex(-1);
    btneLlogInv.SetText('');
    btneLlogBle.SetSelectedIndex(-1);
    btneLlogBle.SetText('');
    btneLlogShit.SetSelectedIndex(-1);
    btneLlogShit.SetText('');
    btneLlogTretet.SetSelectedIndex(-1);
    btneLlogTretet.SetText('');
    btneLlogPakesimRez.SetSelectedIndex(-1);
    btneLlogPakesimRez.SetText('');
    btneLlogRez.SetSelectedIndex(-1);
    btneLlogRez.SetText('');
    btnLLogariKomisioni.SetSelectedIndex(-1);
    btnLLogariKomisioni.SetText("");
    txtSasiNjesi.SetText('1.00');
    txtScrap.SetText('0.00');
    cbProdhimMePorosi.SetChecked(false);
    cbRezervueshem.SetChecked(false);
    cbPerTransferim.SetChecked(false);
    cbLoan.SetChecked(false);
    cbDhurate.SetChecked(false);
    cmbAplikim.SetValue(null);
    txtPike.SetText(0);
    txtVlere.SetText(0); txtKodVFOne.SetText('');
    btnLlogShpe.SetSelectedIndex(-1); cmbLlogAmortizimi.SetSelectedIndex(-1); btnLlogPakesim.SetSelectedIndex(-1);
    txtMinimumi.SetText('');
    txtMaximumi.SetText('');
    Utils.SelectComboItem(cmbMetode, $("#hfMetoda").val());
    var selectedMetode = cmbMetode.GetSelectedItem();
    if (selectedMetode != null) {
        lblMetodePershkrimi.SetText(selectedMetode.GetColumnText('Pershkrimi'));
        memoNdihma.SetText(selectedMetode.GetColumnText('Shpjegimi'));
    }
    cmbKMSH.SetSelectedIndex(-1);
    $('#hfArtikujtPerberes').val('');
    colNorma = new Array();
    cmbZevendesim.SetSelectedIndex(-1);
    txtKodi4.SetText('');
    txtPershkrimi4.SetText('');
    txtKodi5.SetText('');
    txtPershkrimi5.SetText('');
    cmbLloji.SetSelectedIndex(-1);
    txtPershkrimiFurnitori.SetText('');
    txtSiperfaqjaM2.SetText('');
    txtNrKontrate.SetText('');
    txtNrPasurie.SetText('');
    txtZonaKadastrale.SetText('');
    txtShasia.SetText('');
    txtMarka.SetText('');
    txtModeli.SetText('');
    txtVitProdhimi.SetText('');
    txtTeDhenaTeknika.SetText('');
    cbMeBarkodLogjik.SetChecked(false);
    txtSkemeBarkodi.SetText('');
    cmbGarancia.SetSelectedIndex(-1);
    txtGarancia.SetText('');
    cmbAutorizimi.SetValue(null);
    cmbLidhMeNdermRap.SetValue(null);
    cbShitshem.SetChecked(true);
    cbMbetjeShitshem.SetChecked(false);
    cbArtikullVjeter.SetChecked(false);
    chkbAparatBazaar.SetChecked(false);
    txtKodOferte.SetText("");
    cmbFormatSeriali.SetSelectedIndex(-1);
    txtKaraktereTAC.SetText('');
    cbLLogaritKomision.SetChecked(false);
    txtStokuMaxVfOne.SetText('');
    txtKodiIBarit.SetText('');
    cbIRimbursueshem.SetChecked(false);
    RefreshFushatShtese($('#hfId').val(), 'mod');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheArtikujPerberes2"),
        data: JSON.stringify({ id: -1 })
    }).done(function (result) { SucceededCallbackArtPerberes2(result, true); });
    $('#hfKodbaret').val('');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "merrcolMeCmimeMeFormatNumrash"),
        data: JSON.stringify({ idartikulli: -1, merrCmimeBlerje: (hfKushtet.Get("CB") == "Po"), idNdermarrje: hfState.Get('idNdermarrje'), idPerdorues: hfState.Get('idPerdoruesi') })
    }).done(SucceededCallbackCmime);
    aktivFusha(colKontrollet, colAtrTrupi, false);

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrDataNdryshimiArtPerberes"),
        data: JSON.stringify({ idartikulli: 0 })
    }).done(SucceededCallbackData);
    dteDateAkt.SetDate(new Date());
    hfArkiva.Clear();
    Dhurate(true);
    Aplikim();
    PastroFushatShtese();
}

var modeliindex = 0;
function Objektiva_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniObjektivenEKostos"), 'LupaObjektivaKosto.aspx?vjenNga=Shto_Punonjes', widthLupaKF, heightLupaKF);
}
function ButtonClickFormatSeriali() {
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni formatin e serialit", 'Shto_FormatSeriali.aspx?lupe=true', widthLupaSkema, heightLupaSkema);
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
    ASPxGridView_Artikull.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdorues = hfState.Get('idPerdoruesi');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
        data: JSON.stringify({
            idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: " ", idObjekti: -1, shtim: false,
            merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdorues, idNdermarrje: idNdermarrje
        })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvArtikulli").show();
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdorues = hfState.Get('idPerdoruesi');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
        data: JSON.stringify({
            idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: " ", idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdorues, idNdermarrje: idNdermarrje
        })
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

var resultkonf;
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;

function SucceededCallbackKonfig(result) {
    var grida = $("#rowed5");
    formatNumriZgjedhur = result.formatNumri;
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    ruajFormatetNeGrideVfOne(grida, formatNumriZgjedhur);
  //  ruajFormatetNeGride($("#rowed6"), formatNumriZgjedhur);
    ruajFormatetNeGrideCmimesh();
    vendosKonfig(result);
}

var hapLupeSerialeSipasGrupArt = false;
var mbushtedhenasipagrupit = false;

function vendosKonfig(result) {
    //$("#dvArtikulli").show();
    if (result != "" && result != null) {
        colKontrollet = result.colKontroll;
        colAtrTrupi = result.colAtrTrupi;
        var colGrida = result.colGrida;
        colKushte = result.colKushte;
        colAlterKusht = result.colAlterKusht;
        var kodniveli = result.kodniveli;
        var konfLlojRreshti = result.konfLlojRreshti;
        resultkonf = result;
        mbushArrayMagazinat();
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblKonfig', 'tblInformacion', 'tblInventari', 'tblKontabiliteti', 'tblRegjistrime','tblAmortizimi'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C", 0);
        $("#tblKontrolleTeKonfigurueshmeTabPare").show();
        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
        for (j = 0; j < colKushte.length; j++) {
            var kusht = colKushte[j];
            var alterKushti = colAlterKusht[j];
            if (kusht.Kodi == 'HSSGA') {
                if (alterKushti.Alternativa == "Po") {
                    hapLupeSerialeSipasGrupArt = true;
                    hfState.Set('HSSGA', hapLupeSerialeSipasGrupArt);
                }
                else {
                    hapLupeSerialeSipasGrupArt = false;
                    hfState.Set('HSSGA', hapLupeSerialeSipasGrupArt);
                }
            }
            else if (kusht.Kodi == 'NSLKDG') {
                if (alterKushti.Alternativa == "Po") {
                    mbushtedhenasipagrupit = true;

                }
                else {
                    mbushtedhenasipagrupit = false;

                }
            }
        }
        var skema = btneSkema.GetText();
        if (skema !== "") {
            var v = btneSkema.GetText().split(',');
            btneSkema.SetText(v[0]);
            var hfs = $('#hfSkema');
            hfs.val(btneSkema.GetText());
        }
        var selectedMetod = cmbMetode.GetSelectedItem();
        if (Utils.getUrlVar('llojiart') == 'afatshkurter')
            cmbLloji.SetValue('Afatshkurter');
        else
            cmbLloji.SetValue('Afatgjate');
        if (selectedMetod !== null) {
            lblMetodePershkrimi.SetText(selectedMetod.GetColumnText("Pershkrimi"));
            memoNdihma.SetText(selectedMetod.GetColumnText("Shpjegimi"));
        }

        kaNrAutomatik = kaKontrolliNrAutomatik(colKontrollet, colAtrTrupi);
    }
    enable(true, true, true);
    mbushGrideNgaHiddenFieldet();
    enableDetajim(true);
    Dhurate(true);
    Aplikim();
    ShfaqGrideNormaAmortizimi();
    gvAmortizimi.PerformCallback(btneKodifikimi1.GetValue());
    gvNormaAmortizimi.PerformCallback(btneKodifikimi1.GetValue());
    if (cbDhurate.GetVisible() && !($('#hfMeme').val() == 'True')) {
       // cbDhurate.SetVisible(false);
        cmbAplikim.SetVisible(false);
        txtPike.SetVisible(false);
        txtVlere.SetVisible(false);
        txtKodVFOne.SetVisible(false);
       // lblDhurate.SetVisible(false);
        lblAplikim.SetVisible(false);
        lblPike.SetVisible(false);
        lblVlere.SetVisible(false);
        lblKodVFOne.SetVisible(false);
    }
    if (hfMod.val() != 'modifikim')
        btnSerial.SetEnabled(false);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}

function kaKontrolliNrAutomatik(colKontrollet, colAtrTrupi) {
    return (colKontrollet.find != undefined) ?
        kaKontrolliNrAutomatikUsingFind(colKontrollet, colAtrTrupi)
        :
        kaKontrolliNrAutomatikCompatibility(colKontrollet, colAtrTrupi);
    /*
        funksioni find() suportohet nga versioni 45 i chrome e me siper, dhe vetem nga versionet me te fundit
        te browserave te tjere
    */
   
}

function kaKontrolliNrAutomatikUsingFind(colKontrollet, colAtrTrupi) {
    var attr = undefined;
    var kontroll = colKontrollet.find(function (kontrolli) { return kontrolli['KodKontrolli'] == 'txtKodi'; });
    if (kontroll != undefined) {
        attr = colAtrTrupi.find(function (atributi) { return atributi['IdKontroll'] == kontroll.IdKontrolli; });
    }
    return attr != undefined && attr.IdNrAutomatik > 0;
}

function kaKontrolliNrAutomatikCompatibility(colKontrollet, colAtrTrupi) {
    var idKontrolli = Utils.findFieldValueByAttribute(colKontrollet, "KodKontrolli", "IdKontrolli", "txtKodi");
    var idNrAutomatik = Utils.findFieldValueByAttribute(colAtrTrupi, "IdKontroll", "IdNrAutomatik", idKontrolli);
    return idNrAutomatik > 0;
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaKodbari");
    var hf2 = $("#hfLupaKodifikim1");
    var hf3 = $("#hfLupaKodifikim2");
    var hf33 = $("#hfLupaKodifikim3");
    var hf4 = $("#hfLupaAutorizimi");
    var hf5 = $("#hfLupaFurnitori");
    var hf6 = $("#hfLupaKategoriDetajimi");
    var hf7 = $("#hfLupaDetajimNga");
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
    var hfAuto = $("#hfLupaAutomjet");
    var hfSer = $("#hfSeriale");
    var hf20 = $("#hfLupaLlogR");
    var hf21 = $("#hfLupaLlogPakR");
    var hf22 = $("#hfLupaLlogKomision");
    for (var i = 0; i < kontrollet.length; i++) {
        // && idkontrolli != 'cmbMetode' && idkontrolli != 'cmbKMSH' && idkontrolli != 'cmbKlasa') {
        //id e konfigurimit te lupes vendoset ne hidden field
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
        if (kontrollet[i].KodKontrolli == "cmbKategoriDetajimi") {
            hf6.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnDetajim1Nga") {
            hf7.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
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
        if (kontrollet[i].KodKontrolli == "cmbLlogAmortizimi") {
            hf17.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
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
        if (kontrollet[i].KodKontrolli == "btnMagazina") {
            hf18.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnLlogPakesim") {
            hf19.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btneAutomjeti") {
            hfAuto.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }

        if (colKontrollet[i].KodKontrolli == "btnSerial") {
            hfSer.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneLlogRez") {
            hf20.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnLlogPakesimRez") {
            hf21.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnLlogariKomisioni") {
            hf22.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
        continue;
        }
    }
}

function Lupa(kontrollet) {
    var hf1 = $("#hfLupaKodbari")[0];
    var hf2 = $("#hfLupaKodifikim1")[0];
    var hf3 = $("#hfLupaKodifikim2")[0];
    var hf33 = $("#hfLupaKodifikim3")[0];
    var hf4 = $("#hfLupaAutorizimi")[0];
    var hf5 = $("#hfLupaFurnitori")[0];
    var hf6 = $("#hfLupaKategoriDetajimi")[0];
    var hf7 = $("#hfLupaDetajimNga")[0];
    var hf8 = $("#hfLupaSkema")[0];
    var hf9 = $("#hfLupaLlogInv")[0];
    var hf10 = $("#hfLupaLlogBle")[0];
    var hf11 = $("#hfLupaLlogShit")[0];
    var hf12 = $("#hfLupaLlogTretet")[0];
    var hf13 = $("#hfLupaLlogShpe")[0];
    var hf14 = $("#hfLupaArtikuj")[0];
    var hf15 = $("#hfLupaArtikujPerberes")[0];
    var hf16 = $("#hfMetoda")[0]; var hf17 = $("#hfLupaLlogAmortizimi");
    var hf18 = $("#hfLupaLlogPakesim")[0];
    var hf20 = $("#hfLupaLlogR");
    var hf21 = $("#hfLupaLlogPakR");
    for (var i = 0; i < kontrollet.length - 1; i++) {
        // && idkontrolli != 'cmbMetode' && idkontrolli != 'cmbKMSH' && idkontrolli != 'cmbKlasa') {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (kontrollet[i].split(',')[0] == "btneKodbari")
            hf1.value = kontrollet[i].split(',')[11].toString();
        else
            if (kontrollet[i].split(',')[0] == "btneKodifikimi1")
                hf2.value = kontrollet[i].split(',')[11].toString();
            else
                if (kontrollet[i].split(',')[0] == "btneKodifikimi2")
                    hf3.value = kontrollet[i].split(',')[11].toString();
                else
                    if (kontrollet[i].split(',')[0] == "btneKodifikimi3")
                        hf33.value = kontrollet[i].split(',')[11].toString();
                    else if (kontrollet[i].split(',')[0] == "cmbMetode")
                        hf16.value = kontrollet[i].split(',')[3].toString();
                    else
                        if (kontrollet[i].split(',')[0] == "cmbAutorizimi")
                            hf4.value = kontrollet[i].split(',')[11].toString();
                        else
                            if (kontrollet[i].split(',')[0] == "txtFurnitori")
                                hf5.value = kontrollet[i].split(',')[11].toString();
                            else
                                if (kontrollet[i].split(',')[0] == "cmbKategoriDetajimi")
                                    hf6.value = kontrollet[i].split(',')[11].toString();
                                else
                                    if (kontrollet[i].split(',')[0] == "btnDetajim1Nga")
                                        hf7.value = kontrollet[i].split(',')[11].toString();
                                    else
                                        if (kontrollet[i].split(',')[0] == "btneSkema")
                                            hf8.value = kontrollet[i].split(',')[11].toString();
                                        else
                                            if (kontrollet[i].split(',')[0] == "btneLlogInv")
                                                hf9.value = kontrollet[i].split(',')[11].toString();
                                            else
                                                if (kontrollet[i].split(',')[0] == "btneLlogBle")
                                                    hf10.value = kontrollet[i].split(',')[11].toString();
                                                else
                                                    if (kontrollet[i].split(',')[0] == "btneLlogShit")
                                                        hf11.value = kontrollet[i].split(',')[11].toString();
                                                    else
                                                        if (kontrollet[i].split(',')[0] == "btneLlogTretet")
                                                            hf12.value = kontrollet[i].split(',')[11].toString();
                                                        else
                                                            if (kontrollet[i].split(',')[0] == "btnLlogShpe")
                                                                hf13.value = kontrollet[i].split(',')[11].toString();
                                                            else if (kontrollet[i].split(',')[0] == "cmbLlogAmortizimi")
                                                                hf17.val(kontrollet[i].split(',')[11].toString());
                                                            else
                                                                if (kontrollet[i].split(',')[0] == "btnArtikuj")
                                                                    hf14.value = kontrollet[i].split(',')[11].toString();
                                                                else
                                                                    if (kontrollet[i].split(',')[0] == "btnArtikujPerberes")
                                                                        hf15.value = kontrollet[i].split(',')[11].toString();
                                                                    else
                                                                        if (kontrollet[i].split(',')[0] == "btnLlogPakesim")
                                                                            hf18.value = kontrollet[i].split(',')[11].toString();
                                                                        else
                                                                            if (kontrollet[i].split(',')[0] == "btnLlogRez")
                                                                                hf20.value = kontrollet[i].split(',')[11].toString();
                                                                            else
                                                                                if (kontrollet[i].split(',')[0] == "btnLlogPakesimRez")
                                                                                    hf21.value = kontrollet[i].split(',')[11].toString();
    }

}

function aktivFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    enable(false, isLidhur, false);
    enableDetajim(true);
    Dhurate(false);
    Aplikim();
    if (hfMod.val() != 'modifikim')
        btnSerial.SetEnabled(false);
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    //            cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(402, cmbKonfigurimi.GetText());
    ndryshoKonfigurimFushaShtese(cmbKonfigurimi.GetValue());
    //var grida = $('#rowed5');
    //ndryshoKonfigFormatNumri(grida);

}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(402, cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

//var Detajim = "detajim1ne";
var identifikuesPerPopupKodifikimin = "Shto_Artikull";
var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupArtikulli = 'artikull';
var identifikuesPerSkemat = "shtim";
var identifikuesPerPopupMagazina = "ShtoArtikull";
var identifikuesPerPopupAutorizime = 'ShtoArtikull';
var identifikuesPerPopupKodbare = 'ShtoArtikull';

function enable(krijoGride, isLidhur, gjendjeart) {

    var enableGrida = false;
    btnArtikujtPerberes.SetVisible(false);
    if (cmbKlasa.GetValue() == 5) {
        cbProdhimMePorosi.SetVisible(true);
        lblProdhimMePorosi.SetVisible(true);
        if ($('#hfShtimModifikim').val() == 'shtim')
            cbProdhimMePorosi.SetChecked(true);
    }
    else {
        cbProdhimMePorosi.SetVisible(false);
        lblProdhimMePorosi.SetVisible(false);
    }
    if (cmbKlasa.GetValue() == 5 || cmbKlasa.GetValue() == 1) {
        cbRezervueshem.SetEnabled(true);

        //        if ($('#hfShtimModifikim').val() == 'shtim')
        //            cbRezervueshem.SetChecked(true);
    }
    else {
        cbRezervueshem.SetEnabled(false);
    }

    var gridInaktive = false;
    if (cmbLloji.GetText() == 'Afatshkurter') {
        lblDateAkt.SetVisible(false); dteDateAkt.SetVisible(false);
        lblNdryshimi.SetVisible(false); cmbNdryshimi.SetVisible(false);
        txtSasiNjesi.SetVisible(false); lblSasiNjesi.SetVisible(false);
        txtScrap.SetVisible(false); lblScrap.SetVisible(false);

        switch (parseInt(cmbKlasa.GetValue())) {
            case 5: //prodhim
                lblDateAkt.SetVisible(true);
                dteDateAkt.SetVisible(true);
                lblNdryshimi.SetVisible(true);
                cmbNdryshimi.SetVisible(true);
                txtSasiNjesi.SetVisible(true);
                lblSasiNjesi.SetVisible(true);
                txtScrap.SetVisible(true);
                lblScrap.SetVisible(true);
                enableGrida = true;

                if (!isLidhur || $('#hfShtimModifikim').val() == 'klonim') {
                    btneLlogInv.SetEnabled(true);
                    btneLlogBle.SetEnabled(false);
                    btneLlogBle.SetText('');
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(false);
                    btneLlogTretet.SetText('');
                    btnLlogPakesim.SetEnabled(false);
                    btnLlogPakesim.SetText('');
                    btnLlogShpe.SetEnabled(true);

                }
                break;
            case 6: //prodhim ne proces
                lblDateAkt.SetVisible(true);
                dteDateAkt.SetVisible(true);
                lblNdryshimi.SetVisible(true);
                cmbNdryshimi.SetVisible(true);
                txtSasiNjesi.SetVisible(true);
                lblSasiNjesi.SetVisible(true);
                txtScrap.SetVisible(true);
                lblScrap.SetVisible(true);
                enableGrida = true;
                if (!isLidhur || $('#hfShtimModifikim').val() == 'klonim') {
                    btneLlogInv.SetEnabled(true);
                    btneLlogBle.SetEnabled(false);
                    btneLlogBle.SetText('');
                    btneLlogShit.SetEnabled(false);
                    btneLlogShit.SetText('');
                    btneLlogTretet.SetEnabled(false);
                    btneLlogTretet.SetText('');
                    btnLlogShpe.SetEnabled(true);
                    btnLlogPakesim.SetEnabled(true);
                }
                break;
            case 1:
                if (!isLidhur || $('#hfShtimModifikim').val() == 'klonim') {
                    btneLlogInv.SetEnabled(true);
                    btneLlogBle.SetEnabled(true);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(true);
                    btnLlogShpe.SetEnabled(false);
                    btnLlogShpe.SetText('');
                    btnLlogPakesim.SetEnabled(false);
                    btnLlogPakesim.SetText('');
                }
                else
                    if (isLidhur && $('#hfShtimModifikim').val() == "modifikim" && btnLLogariKomisioni.GetValue() != "" && btnLLogariKomisioni.GetValue() != null)
                        btnLLogariKomisioni.SetEnabled(false);
                break;
            case 2:
                if (!isLidhur || $('#hfShtimModifikim').val() == 'klonim') {
                    btneLlogInv.SetEnabled(false);
                    btneLlogInv.SetText('');
                    btneLlogBle.SetEnabled(true);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(false);
                    btneLlogTretet.SetText('');
                    btnLlogShpe.SetEnabled(false);
                    btnLlogShpe.SetText('');
                    btnLlogPakesim.SetEnabled(false);
                    btnLlogPakesim.SetText('');
                }
                break;
            case 3:
                if (!isLidhur || $('#hfShtimModifikim').val() == 'klonim') {
                    btneLlogInv.SetEnabled(false);
                    btneLlogInv.SetText('');
                    btneLlogBle.SetEnabled(true);
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(false);
                    btneLlogTretet.SetText('');
                    btnLlogShpe.SetEnabled(false);
                    btnLlogShpe.SetText('');
                    btnLlogPakesim.SetEnabled(false);
                    btnLlogPakesim.SetText('');
                }
                break;
            case 4: //i perbere
                lblDateAkt.SetVisible(true);
                dteDateAkt.SetVisible(true);
                lblNdryshimi.SetVisible(true);
                cmbNdryshimi.SetVisible(true);
                if (!isLidhur || $('#hfShtimModifikim').val() == 'klonim') {
                    btneLlogInv.SetEnabled(false);
                    btneLlogInv.SetText('');
                    btneLlogBle.SetEnabled(false);
                    btneLlogBle.SetText('');
                    btneLlogShit.SetEnabled(true);
                    btneLlogTretet.SetEnabled(true);
                    btnLlogShpe.SetEnabled(false);
                    btnLlogShpe.SetText('');
                    btnLlogPakesim.SetEnabled(false);
                    btnLlogPakesim.SetText('');
                }
                enableGrida = true;
                //if ($('#hfShtimModifikim').val() !== 'klonim') {
                //    if (isLidhur)
                //        gridInaktive = true;
                //}
                break;
            default:
                break;
        }
    }
    else if (cmbLloji.GetText() == 'Afatgjate') {
        //btneLlogInv.SetEnabled(true);
        //btneLlogBle.SetEnabled(true);
        //btneLlogShit.SetEnabled(true);
        //btneLlogTretet.SetEnabled(true);
        //btnLlogShpe.SetEnabled(true);
        //btnLlogPakesim.SetEnabled(true);
        cbProdhimMePorosi.SetVisible(false);
        lblProdhimMePorosi.SetVisible(false);
        cbRezervueshem.SetEnabled(false);
        cbRezervueshem.SetChecked(false);
        //        btnArtikujtPerberes.SetVisible(false);
        enableGrida = false;
        lblDateAkt.SetVisible(false);
        dteDateAkt.SetVisible(false);
        lblNdryshimi.SetVisible(false);
        txtSasiNjesi.SetVisible(false);
        lblSasiNjesi.SetVisible(false);
        txtScrap.SetVisible(false);
        lblScrap.SetVisible(false);
        cmbNdryshimi.SetVisible(false);
    }


    if (gjendjeart) {

        var grida1 = $(pageState.gridaSelector);
        grida1.setLastSel2(-1);

        $(pageState.gridaSelector).jqGrid('GridUnload', pageState.gridaSelector);
        formGridColsArray($("input[id$='HfGridColGjendjeArt']"),
        false, arrayPershkrimiKolonaGridesGjendjeArt, arrayReadOnlyKolonaGridesGjendjeArt, arrayIdKolonaGridesGjendjeArt, arrayVisibleKolonaGridesGjendjeArt, arrayWidthKolonaGrides, arrayRenditjeKolonaGridesGjendjeArt);


        inicializoGrideGjendjeArt(false);
        //myJQGrid.fixGridWidth($(pageState.gridaSelector), $('#dvArtikulli'), 1);
    }


    if (krijoGride) {
        var grida = $('#rowed5');
        grida.setLastSel2(-1);
        $('#rowed5').jqGrid('GridUnload', "rowed5");
        formGridColsArray($("input[id$='HfGridCol']"), gridInaktive, arrayPershkrimiKolonaGrides, arrayReadOnlyKolonaGrides, arrayIdKolonaGrides, arrayVisibleKolonaGrides, arrayWidthKolonaGrides, arrayRenditjeKolonaGrides);
        ruajFormatetNeGride(grida);
        inicializoGride(gridInaktive);
        //myJQGrid.fixGridWidth($('#rowed5'), $('#dvArtikulli'), 1);

    }

    $("#divgrideGjendje1").show();

    if (enableGrida)
        $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible';
    else
        $("#divgride1").hide();//$("#divgride1")[0].style.visibility = 'hidden';



    //    $("#divgride1")[0].style.visibility = 'hidden';
}

function ndryshoScrap() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (isNaN(parseFloat(txtScrap.GetText()))) {
        txtScrap.SetText(0);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoArtikullFirLigjoreDuhetNumer"));

    }
    if (parseFloat(txtScrap.GetText()) < 0 || parseFloat(txtScrap.GetText()) > 100) {
        txtScrap.SetText(0);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgClsArtikulliFiroLigjoreDuhetNga0Deri100"));
    } var reshtiieditueshem = false;
    if ($('#txtFiro' + idRresht).val() != undefined) {
        reshtiieditueshem = true;
        if ($('#cmbLloji' + idRresht).val() == 1)
            $('#txtFiro' + idRresht).val(txtScrap.GetText());
    }


    //vendoset magazina e zgjedhur te koka ne rreshtat e tjere te grides
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        var rreshti = grida.jqGrid('getRowData', [rreshtaTeGrides[i]]);
        if (!reshtiieditueshem) {
            if (rreshti.cmbLloji == 'Artikull')
                grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtFiro', txtScrap.GetText(), 'clientArray', '');
        }
        else if (rreshtaTeGrides[i] != idRresht) {
            if (rreshti.cmbLloji == 'Artikull')
                grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtFiro', txtScrap.GetText(), 'clientArray', '');
        }
    }
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];

    myFaqeCelje.changeName('Shto_Artikull.aspx?llojiart=' + Utils.getUrlVar("llojiart"), 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));
    enableDetajim(true);
    Dhurate(true);
    Aplikim();
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
    enableDetajim(false);
    btnDetajim1Nga.SetText('');
    btnDetajim2Nga.SetText('');
    cmbKategoriDetajimi.SetText('');
    cmbKategoriDetajimi2.SetText('');
}

function SucceededCallbackDetajimLidhur(result) {
    if (!result) {
        enableDetajim(false);
        btnDetajim1Nga.SetText('');
        btnDetajim2Nga.SetText('');
        cmbKategoriDetajimi.SetText('');
        cmbKategoriDetajimi2.SetText('');
    }
    else {
        cbDetajim.SetChecked(true);
        myMesazh.ShtoMesazhGabimi("Nuk mund te hiqni detajimet e ketij artikulli sepse ka veprime me to!");
    }
}

function KategoriaChanged(lloji) {
    if ($('#hfShtimModifikim').val() == "modifikim" && ((btnDetajim1Nga.GetText() != "" && lloji == 1) || (btnDetajim2Nga.GetText() != "" && lloji == 2))) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloDetajimLidhurSipasLlojit"),
            data: JSON.stringify({ idartikulli: $('#hfId').val(), lloji: lloji })
        }).done(SucceededCallbackDetajimLidhurLloji);
        return;
    }
    lloji == 1 ? btnDetajim1Nga.SetText('') : btnDetajim2Nga.SetText('');
}

function SucceededCallbackDetajimLidhurLloji(result) {
    if (!result[0]) {
        if (result[2] == 1)
            btnDetajim1Nga.SetText('');
        else
            btnDetajim2Nga.SetText('');
    }
    else {
        if (result[2] == 1)
            cmbKategoriDetajimi.SetValue(result[1]);
        else
            cmbKategoriDetajimi2.SetValue(result[1]);
        myMesazh.ShtoMesazhGabimi("Nuk mund te ndryshoni llojin e kategorise se detajimit per kete artikull sepse ka veprime me to!");
    }
}


function enableDetajim(konfigurimifill) {
    if (cbDetajim.GetEnabled() && !konfigurimifill) {
        btnDetajim1Nga.SetEnabled(cbDetajim.GetChecked());
        btnDetajim2Nga.SetEnabled(cbDetajim.GetChecked());
        checkKontrollGjendje.SetEnabled(cbDetajim.GetChecked());
        checkKontrollGjendjeDetajim2.SetEnabled(cbDetajim.GetChecked());
        checkKontrollCmimi.SetEnabled(cbDetajim.GetChecked());
    }
    cmbKategoriDetajimi.SetEnabled(cbDetajim.GetChecked());
    cmbKategoriDetajimi2.SetEnabled(cbDetajim.GetChecked());
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    formatoFushaDevi();
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Artikull, "402", pastrofusha, hfTeDrejta, vendosKonfig, resultkonf);
    var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true');
    enable(false, isLidhur, false);
}

var KPF;
function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("MsgBlerjeShitjeAutorizime"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}

function KodifikimArtikulli_Click(llojKodifikimi) {   
    var llojartikulli = pageState.eshteArtAfatgjate ? 'aqt' : 'afatshkurter';   
    var queryStr = $("#hfLupaKodifikim" + llojKodifikimi).val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'KodifikimArtikulli.aspx?llojiart=' + llojartikulli + '&llojKodifikimi=' + llojKodifikimi +  '&lupe=true&idKonfigAmbjente=' + queryStr, widthLupaKodifikime, heightLupaKodifikime);
}


var editordetajimi;
var editorKategoriDetajimi;
var editorCheckBoxDetajimi;
function DetajimArtikulli_Click(s) {
    var hf = $("#hfLupaDetajimNga");
    var veprimi = "";
    var lloji = 0;
    editordetajimi = s;
    editorCheckBoxDetajimi = cbDetajim;
    if (editordetajimi == btnDetajim1Nga) {
        if (cmbKategoriDetajimi.GetValue() !== null && cmbKategoriDetajimi.GetValue() !== "")
            veprimi = cmbKategoriDetajimi.GetValue();
        lloji = 1;
        editorKategoriDetajimi = cmbKategoriDetajimi;
    }
    else if (editordetajimi == btnDetajim2Nga) {
        if (cmbKategoriDetajimi2.GetValue() !== null && cmbKategoriDetajimi2.GetValue() !== "")
            veprimi = cmbKategoriDetajimi2.GetValue();
        lloji = 2;
        editorKategoriDetajimi = cmbKategoriDetajimi2;
    }
    //if ($('#hfShtimModifikim')[0].value == "shtim")
    //    queryStr = queryStr + '&lloji=' + lloji + '&detajime=' + (lloji == 1 ? btnDetajim1Nga.GetText() : btnDetajim2Nga.GetText());
    //else

    var queryStr = "?lupe=true&idKonfigAmbjente=" + hf.val() + '&kodArtikulli=' + txtKodi.GetText() + '&lloji=' + lloji + '&detajime=' + (lloji == 1 ? btnDetajim1Nga.GetText() : btnDetajim2Nga.GetText());
    if (veprimi)
        queryStr += "&veprimi=" + veprimi;
    if (veprimi == 0) {
        lloji == 1 ? myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhKategorineDetajimNje")) : myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhKategorineDetajimDy"));
    } else {
        myButtonClickLupa.LupaUniversal_Click("Zgjidh detajimet e artikullit", "DetajimeArtikulli.aspx" + queryStr, widthLupaDetajime, heightLupaDetajime);
    }
}

function Skema_Click() {

    var hf = $("#hfLupaSkema")[0];
    var queryStr = hf.value;
    //            alert(hf.value);
    var parametri = Utils.getUrlVar('llojiart');
    myButtonClickLupa.Skema_Click(hfState.Get('headerPopUpTextSkemaKont'), queryStr, widthLupaSkema, heightLupaSkema, cmbKlasa.GetValue(), parametri);
}

function Kodbare_Click() {
    var hf = $("#hfLupaKodbari")[0];
    var queryStr = hf.value;
    if ($('#hfShtimModifikim').val() == "klonim")
        queryStr += "&klonim=true";
    else
        queryStr += '&idartikulli=' + $('#hfId').val();

    var kodbaret = $("#hfKodbaret")[0].value;
    if (kodbaret != "" && JSON.parse(kodbaret).length > 20)
    {

        queryStr += "&mbushNgaArtikulli=true";
        kodbaret = "";
    }
    myButtonClickLupa.Kodbare_Click(hfState.Get('headerPopUpKodBar'), queryStr, widthLupaKodbare, heightLupaKodbare, kodbaret);
}

function Artikull_Click() {
    var hf = $("#hfLupaArtikuj")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ShtoArtikull_Click(queryStr, widthLupaShtoArt, heightLupaShtoArt, $('#hfArtikulli')[0].value, $('#hfEmertimiA')[0].value, $('#hfPrioritetiA')[0].value);
}


function HapLLogariSipasLlojitClick(idHidden)
{
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), $(idHidden)[0].value, widthLupaLlogaria, heightLupaLlogaria);
}


function Furnitori_Click() {
    var hf = $("#hfLupaFurnitori")[0];
    var queryStr = hf.value;
    identikuesPerPopupKlientFurnitori = "Artikull_ButtonEdit";
    txtLlog.SetText('ld');
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, "Furnitor", widthLupaKF, heightLupaKF);
}

var arr1 = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;

var editorValues = new Object();
//var arrVlerat = new Array();
var countvlerat = 0;

//pastron fushat
function Init() {
    $("#tblKontrolleTeKonfigurueshmeTabPare").hide();
    changeName();
    pageState.eshteArtAfatgjate = Utils.getUrlVar("llojiart") === 'aqt';
    if (pageState.eshteArtAfatgjate) {
        PageControl.GetTabByName('Amortizimi').SetVisible(true);
    }
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
    var grida = $('#rowed7');
    formGridColsArrayV($('#hfMeme').val() == "True" ? false : true);
    ruajFormatetNeGrideVfOne(grida);
    inicializoGrideVfone($('#hfMeme').val() == "True" ? false : true);
}

//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    arr1 = new Array();
    counter = 0;
    arr2 = new Array();
    counter2 = 0;
    var hf = $("#status1")[0];
    hf.value = "false";
    //arrVlerat = new Array();
    countvlerat = 0;
    colNorma = new Array();
}

var arrNrLlogariFurnitori = new Array();
var arrEmertimiF = new Array();
var arrPrioritetiF = new Array();
var cou1 = 0;
var indeksi = -1;
var editorNrLlogariFurnitor;
var editorEmertimiF;
var editorPrioritetiF;
var indexCounter = 0;


//buxhetet
function ruajBuxhet() {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    myBuxhet.ruajBuxhet(hidField, hidField2);
}

function ShtoBuxhet1(editor, edgjendja, eddiff, key) {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter = myBuxhet.ShtoBuxhet1(editor, edgjendja, eddiff, key, hidField, counter);
}

function ShtoBuxhet2(editor, edgjendja, eddiff, key) {
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter2 = myBuxhet.ShtoBuxhet2(editor, edgjendja, eddiff, key, hidField2, counter2);
}

//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal1(editor, edgjendja, eddiff) {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter = myBuxhet.ShtoTotal1(editor, edgjendja, eddiff, hidField, counter);
}

//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal2(editor, edgjendja, eddiff) {
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter2 = myBuxhet.ShtoTotal2(editor, edgjendja, eddiff, hidField2, counter2);
}

var editorAutorizime;
var editorKodifikime;
var editorSkema;

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
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
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgSkemaNukPerketKlase"));
                    btneSkema.SetSelectedIndex(-1);
                    btneLlogInv.SetSelectedIndex(-1);
                    btneLlogBle.SetSelectedIndex(-1);
                    btneLlogShit.SetSelectedIndex(-1);
                    btneLlogTretet.SetSelectedIndex(-1);
                    btneLlogPakesim.SetSelectedIndex(-1);
                    btnLlogShpe.SetSelectedIndex(-1); cmbLlogAmortizimi.SetSelectedIndex(-1);
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
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1); i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorCmimi2Tvsh = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorNorma = Utils.ktheKontroll('Norme' + i);
            editorCmimi2.SetText(editorCmimi.GetText() * txtKoeficienti.GetText());                  
            editorCmimi2Tvsh.SetText((1 + editorNorma.GetText() / 100) * editorCmimi2.GetText());
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2Tvsh' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));

        }
    else {
        for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorCmimi2Tvsh = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorNorma = Utils.ktheKontroll('Norme' + i);
            editorCmimi2.SetText(editorCmimi.GetText() * txtKoeficienti.GetText());
            editorCmimi2Tvsh.SetText((1 + editorNorma.GetText() / 100) * editorCmimi2.GetText());
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2Tvsh' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));
        }
    }
}

function kontrolloTextGarancia(s, e) {
    if (txtGarancia.GetText().match(/[^0-9\.]/)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoArtikullGaranciaDuhetNumer"));
    }
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

var col;
function EndCallback(s, e) {
    ShfaqTeDhenat();
}

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

    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1); i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorDateFillimi = Utils.ktheKontroll('DateFillimi' + i);
            editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i);
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            // $("input[id$=" + stringDate + "]")[0]    //nestila u komentuan per momentin sepse jane bere te padukshme dhe japin probleme do perdoren perseri kur te behen visible
            editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);

            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorKosto = Utils.ktheKontroll('Kosto' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            col[i].Cmimi = editorCmimi.GetText();
            col[i].Cmimi2 = editorCmimi2.GetText();
            col[i].Norme = editorNorme.GetText();
            col[i].IdTvsh = editorTvsh.GetValue();
            col[i].CmimiTvsh = editorCmimiTvsh.GetText();
            col[i].Cmimi2Tvsh = editorCmimiTvsh2.GetText();
            col[i].DateFillimi = editorDateFillimi.GetDate();
            col[i].DateMbarimi = editorDateMbarimi.GetDate();
            col[i].KoheFillimi = editorKoheFillimi.GetDate();
            col[i].KoheMbarimi = editorKoheMbarimi.GetDate();
            col[i].Kosto = editorKosto.GetText();
            col[i].Kursi = editorKursi.GetText();
            col[i].SasiMin = editorSasiMin.GetText();
            col[i].SasiMax = editorSasiMax.GetText();            
        }
    else {
        for (i = 15 * gvCmimet.cpNoPage; i < gvCmimet.cpNoRows; i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorDateFillimi = Utils.ktheKontroll('DateFillimi' + i);
            editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i);
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            // $("input[id$=" + stringDate + "]")[0]    //nestila u komentuan per momentin sepse jane bere te padukshme dhe japin probleme do perdoren perseri kur te behen visible
            editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorKosto = Utils.ktheKontroll('Kosto' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            col[i].Cmimi = editorCmimi.GetText();
            col[i].Cmimi2 = editorCmimi2.GetText();
            col[i].Norme = editorNorme.GetText();
            col[i].IdTvsh = editorTvsh.GetValue();
            col[i].CmimiTvsh = editorCmimiTvsh.GetText();
            col[i].Cmimi2Tvsh = editorCmimiTvsh2.GetText();
            col[i].DateFillimi = editorDateFillimi.GetDate();
            col[i].DateMbarimi = editorDateMbarimi.GetDate();
            col[i].KoheFillimi = editorKoheFillimi.GetDate();
            col[i].KoheMbarimi = editorKoheMbarimi.GetDate();
            col[i].Kosto = editorKosto.GetText();
            col[i].Kursi = editorKursi.GetText();
            col[i].SasiMin = editorSasiMin.GetText();
            col[i].SasiMax = editorSasiMax.GetText();
        }
    }
    $('#hfArtikuj').val(JSON.stringify(col));
    unformatoFushaDevi();
}

function ndryshoTvsh()
{
    merrTeDhenat();
    ShfaqTeDhenat();
}

function ShfaqTeDhenat() {
    if (col == undefined || col == null || col.length == 0)
        return;
    //if (col != undefined || col != null || col.length != 0) {
    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1); i++) {
            editorCmimi2 = Utils.ktheKontroll('Cmimi2' + i);
            editorDateFillimi = Utils.ktheKontroll('DateFillimi' + i);
            //$("input[id$=" + stringDate + "]")[0];
            editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i);
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            //$("input[id$=" + stringDate + "]")[0]
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            // $("input[id$=" + stringDate + "]")[0];    //nestila u komentuan per momentin sepse jane bere te padukshme dhe japin probleme do perdoren perseri kur te behen visible
            editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);
            editorKosto = Utils.ktheKontroll('Kosto' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            editorFormula = Utils.ktheKontroll('txtFormula' + i);
            editorCmimi2.SetText(col[i].Cmimi2);

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


            editorDateFillimi.SetDate(new Date(col[i].DateFillimi));
            editorDateMbarimi.SetDate(new Date(col[i].DateMbarimi));
            editorKoheFillimi.SetDate(new Date(col[i].KoheFillimi));
            editorKoheMbarimi.SetDate(new Date(col[i].KoheMbarimi));
            editorSasiMin.SetText(col[i].SasiMin);
            editorSasiMax.SetText(col[i].SasiMax);
            editorCmimi.SetText(col[i].Cmimi);

            editorKosto.SetText(col[i].Kosto);
            editorKursi.SetText(col[i].Kursi);
            editorFormula.SetText(hfKushtet.Get("formula"));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('CmimiTvsh' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('CmimiTvsh' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2Tvsh' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));
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
            //$("input[id$=" + stringDate + "]")[0];
            editorDateMbarimi = Utils.ktheKontroll('DateMbarimi' + i);
            editorKoheFillimi = Utils.ktheKontroll('KoheFillimi' + i);
            //$("input[id$=" + stringDate + "]")[0]
            editorKoheMbarimi = Utils.ktheKontroll('KoheMbarimi' + i);
            // $("input[id$=" + stringDate + "]")[0];    //nestila u komentuan per momentin sepse jane bere te padukshme dhe japin probleme do perdoren perseri kur te behen visible
            editorSasiMin = Utils.ktheKontroll('SasiMin' + i);
            editorSasiMax = Utils.ktheKontroll('SasiMax' + i);
            editorNorme = Utils.ktheKontroll('Norme' + i);
            editorTvsh = Utils.ktheKontroll('IdTvsh' + i);
            editorCmimi = Utils.ktheKontroll('Cmimi' + i);
            editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + i);
            editorCmimiTvsh2 = Utils.ktheKontroll('Cmimi2Tvsh' + i);

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

            editorKosto = Utils.ktheKontroll('Kosto' + i);
            editorKursi = Utils.ktheKontroll('Kursi' + i);
            editorFormula = Utils.ktheKontroll('txtFormula' + i);
            editorCmimi2.SetText(col[i].Cmimi2);

            editorDateFillimi.SetDate(new Date(col[i].DateFillimi));
            editorDateMbarimi.SetDate(new Date(col[i].DateMbarimi));
            editorKoheFillimi.SetDate(new Date(col[i].KoheFillimi));
            editorKoheMbarimi.SetDate(new Date(col[i].KoheMbarimi));
            editorSasiMin.SetText(col[i].SasiMin);
            editorSasiMax.SetText(col[i].SasiMax);
            editorCmimi.SetText(col[i].Cmimi);
            editorKosto.SetText(col[i].Kosto);
            editorKursi.SetText(col[i].Kursi);
            editorFormula.SetText(hfKushtet.Get("formula"));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('CmimiTvsh' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('CmimiTvsh' + i));
            Utils.setFormatNumri(Utils.ktheKontroll('Cmimi2Tvsh' + i), arrformatecmimi[i]);
            Utils.formatoTextBox(Utils.ktheKontroll('Cmimi2Tvsh' + i));
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
function  changeTvsh(s,e)
{
    merrTeDhenat();
    ShfaqTeDhenat();
}

function TextChangedDataFill(editor, e, key) {
}

function TextChangedDataMbar(editor, e, key) {
}
function TextChangedKohaFill(editor, e, key) {
    if (editor.GetDate() > Utils.ktheKontroll('KoheMbarimi' + key).GetDate()) {
        myMesazh.ShtoMesazhGabimi("Koha e fillimit nuk mund te jete me e madhe se koha e mbarimit!");
       
        editor.SetDate(Utils.ktheKontroll('KoheMbarimi' + key).GetDate());
       
    }
}

function TextChangedKohaMbar(editor, e, key) {
    if (editor.GetDate() < Utils.ktheKontroll('KoheFillimi' + key).GetDate()) {
        myMesazh.ShtoMesazhGabimi("Koha e fillimit nuk mund te jete me e madhe se koha e mbarimit!");

        editor.SetDate(Utils.ktheKontroll('KoheFillimi' + key).GetDate());

    }
}

function TextChangedCmimi2(editor, e, key, artNjesiTeVarura) {
    if (isNaN(editor.GetValue()) || editor.GetValue() == null) {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmime2Numerike"));
        editor.SetText(0);
    }
    editorCmimi1 = Utils.ktheKontroll('Cmimi' + key);
    if (artNjesiTeVarura.toLowerCase() === 'true')
        editorCmimi1.SetText(editor.GetValue() / txtKoeficienti.GetText());
    editorNorma = Utils.ktheKontroll('Norme' + key);
    editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + key);
    editorCmimiTvsh.SetText((1 + editorNorma.GetText() / 100) * editorCmimi1.GetText());
    editorCmimi2Tvsh = Utils.ktheKontroll('Cmimi2Tvsh' + key);
    editorCmimi2Tvsh.SetText((1 + editorNorma.GetText() / 100) * editor.GetValue());
}
function TextChangedCmimi2Tvsh(editor, e, key) {
    if (isNaN(editor.GetValue()) || editor.GetValue() == null) {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmime2Numerike"));
        editor.SetText(0);
    }
    editorCmimi1 = Utils.ktheKontroll('CmimiTvsh' + key);
    editorCmimi1.SetText(editor.GetValue() / txtKoeficienti.GetText());
    editorNorma = Utils.ktheKontroll('Norme' + key);
    editorCmimi = Utils.ktheKontroll('Cmimi' + key);
    editorCmimi.SetText(editorCmimi1.GetText() / (1 + editorNorma.GetText() / 100));
    editorCmimi2 = Utils.ktheKontroll('Cmimi2' + key);
    editorCmimi2.SetText(editor.GetValue() / (1 + editorNorma.GetText() / 100));
}

$(window).on('load', function () {
});

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

function KlientFurnitoriChanged() {
    if (isNaN(txtFurnitori.GetValue())) {
        txtFurnitori.SetText('');
        txtFurnitori.Focus();
        return;
    }
}

function cmbMetodeTextChanged(s, e) {
    //    var text = s.GetText();
    //    s.SetText(text.split(';')[0]);
    //    lblMetodePershkrimi.SetText(text.split(';')[1]);
    //    memoNdihma.SetText(text.split(';')[2]);

    var item = s.GetSelectedItem();
    s.SetText(item.texts[0]);
    lblMetodePershkrimi.SetText(item.texts[1]);
    memoNdihma.SetText(item.texts[2]);
}

function cmbMetodeEndCallback(s, e) {
    if (cmbMetode.GetEnabled() == false) {
        cmbMetode.HideDropDown();
    }
}

function ClickUpdateBtn(s, e, id) {
    editorCmimi = Utils.ktheKontroll('Cmimi' + id);
    editorCmimiTvsh = Utils.ktheKontroll('CmimiTvsh' + id);
    editorCmimi2Tvsh = Utils.ktheKontroll('Cmimi2Tvsh' + id);
    editorKosto = Utils.ktheKontroll('Kosto' + id);
    editorKursi = Utils.ktheKontroll('Kursi' + id);
    editorFormula = Utils.ktheKontroll('txtFormula' + id);
    editorCmimi2 = Utils.ktheKontroll('Cmimi2' + id);
    editorNorma = Utils.ktheKontroll('Norme' + id);
    if (editorKursi.GetValue() != 0) {
        try {
            var expr = '(' + editorKosto.GetValue().replace(',', '') + '/' + editorKursi.GetValue() + ')' + editorFormula.GetText();
            var rezultati = eval(expr);
            editorCmimi.SetText(rezultati);
            editorCmimi2.SetText(txtKoeficienti.GetText() * rezultati);
            editorCmimiTvsh.SetText(rezultati*(1+editorNorma.GetText()/100));
            editorCmimi2Tvsh.SetText(txtKoeficienti.GetText() * rezultati*(1+editorNorma.GetText()/100));
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimLlogCmim"));
        }
    }
}

/*
Function: ButtonClickMagazina
    
Hap lupen e magazinave.
*/
function ButtonClickMagazina() {//po
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhMagazinen"), 'LupaMagazina.aspx?idKonfigAmbjente=' + $('#hfLupaMagazina').val(), 600, 560);
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
        identikuesPerPopupFormula = 'Artikull';
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhFormuleLupa"), 'LupaFormula.aspx', 850, 600);
    }
}

function LostFocusFormula(editori, indexi) {

}

function ButtonClickFormula(editori, indexi) {
    editorFormula = Utils.ktheKontroll(editori);
    identikuesPerPopupFormula = 'Artikull';
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhFormuleLupa"), 'LupaFormula.aspx', 850, 600);
}

function llogaritKoston(key) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    colNorma = new Array();
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
        data: JSON.stringify({ artikujtPerberes: JSON.stringify(colNorma), idNdermarrje: hfState.Get("idNdermarrje") })
    }).done(SucceededCallbackKostoArtPerb);
}

function SucceededCallbackKostoArtPerb(result) {
    if (gvCmimet.cpNoRows > 15 * (gvCmimet.cpNoPage + 1))
        for (i = 15 * gvCmimet.cpNoPage; i < 15 * (gvCmimet.cpNoPage + 1); i++) {
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

var arrayIdKolonaGrides = new Array();
var arrayIdKolonaGridesGjendjeArt = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayPershkrimiKolonaGridesGjendjeArt = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayVisibleKolonaGridesGjendjeArt = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayWidthKolonaGridesGjendjeArt = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayReadOnlyKolonaGridesGjendjeArt = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrayRenditjeKolonaGridesGjendjeArt = new Array();

//var lidhur = false;
function inicializoGride(isLidhur) {//po
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    var classes = '';
    if (isLidhur === true)
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2],
                           arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6],
                            arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9]];
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
        widthi: $('#divgride2').width(),
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        resetRreshtKorent: resetRreshtKorent,
        selektorDivgride3: "#dvArtikulli",
        autocompleteList:
            [{ emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false }]

    };
    return myJQGrid.initGride(gridParams);
    //myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, isLidhur, lastsel2, "#txtKodi", "#cmbLloji", "", null, null, null, $('#divgride2').width(), undefined, undefined, null, null, '', '', undefined, undefined, undefined, undefined, undefined, undefined);
}

function inicializoGrideGjendjeArt(isLidhur) {
    if (arrayPershkrimiKolonaGridesGjendjeArt.length == 0)
        return;
    var classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGridesGjendjeArt[0], arrayPershkrimiKolonaGridesGjendjeArt[1], arrayPershkrimiKolonaGridesGjendjeArt[2], arrayPershkrimiKolonaGridesGjendjeArt[3]];
    var arrayModel = [
          { name: arrayIdKolonaGridesGjendjeArt[0], index: arrayIdKolonaGridesGjendjeArt[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGridesGjendjeArt[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina, custom_value: myJQGrid.myValueCombo } },
            { name: arrayIdKolonaGridesGjendjeArt[1], index: arrayIdKolonaGridesGjendjeArt[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGridesGjendjeArt[1], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjendjeMin, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGridesGjendjeArt[2], index: arrayIdKolonaGridesGjendjeArt[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGridesGjendjeArt[2], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjendjeMax, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGridesGjendjeArt[3], index: arrayIdKolonaGridesGjendjeArt[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGridesGjendjeArt[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshiR, custom_value: myValueButtonFshiR }, hidedlg: true }
    ];
    var gridParams = {
        emergride: "#rowed6",
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtMagazina",
        arrayReadOnlyKolonaGrides:arrayReadOnlyKolonaGridesGjendjeArt,
        widthi: $('#divgride2').width(),
        lostFocusKoloneFundit: lostFocusKoloneFunditGjendjeArt,
        arrayRenditjeKolonaGridesName: "arrayRenditjeKolonaGridesGjendjeArt",
        selektorDivgride3: "#dvArtikulli"
        

        //colMagazina: colMagazina,

        //autocompleteList:
        //    [{ emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
        //     { emerEditor: "txtMagazina", selectFunc: selectFunc1, changeFunc: changeFunc1, shtoDataKod: false }],

    };
    return myJQGrid.initGride(gridParams);
    //  myJQGrid.inicializoGrideGjendjeArt("#rowed6", arrayPershkrime, arrayModel, false, lastsel3, "txtMagazina", undefined, "", null, null, null, $('#divgride2').width(), undefined, undefined, null, null, undefined, undefined, undefined, undefined);
    // myJQGrid.inicializoGride2("#rowed6", arrayPershkrime, arrayModel, lidhur, lastsel3, "#txtKodiArtikullR", "txtLloji", "", null, null, null, $('#divgride2').width(), undefined, undefined, null, null, '', '', undefined, undefined, undefined, undefined, undefined, undefined);
}
function inicializoGrideVfone(isLidhur) {//po
    if (arrayPershkrimiKolonaGridesV.length == 0)
        return;
    var classes = '';
    if (isLidhur === true)
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGridesV[0], arrayPershkrimiKolonaGridesV[1], arrayPershkrimiKolonaGridesV[2],
                           arrayPershkrimiKolonaGridesV[3]];
    var arrayModel = [
            { name: arrayIdKolonaGridesV[0], index: arrayIdKolonaGridesV[0], width: arrayWidthKolonaGridesV[0], hidden: arrayVisibleKolonaGridesV[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodVfone, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGridesV[1], index: arrayIdKolonaGridesV[1], width: arrayWidthKolonaGridesV[1], hidden: arrayVisibleKolonaGridesV[1], classes: classes, sortable: false, editable: true, edittype: 'custom',  editoptions: { custom_element: myElemPike, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGridesV[2], index: arrayIdKolonaGridesV[2], width: arrayWidthKolonaGridesV[2], hidden: arrayVisibleKolonaGridesV[2], classes: classes, sortable: false, editable: true, edittype: 'custom',  editoptions: { custom_element: myElemVlere, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGridesV[3], index: arrayIdKolonaGridesV[3], width: arrayWidthKolonaGridesV[3], hidden: arrayVisibleKolonaGridesV[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshiV, custom_value: myValueButtonFshiV }, hidedlg: true }
    ];
    var gridParams = {
        emergride: "#rowed7",
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "",
        arrayReadOnlyKolonaGrides: arrayReadOnlyKolonaGridesV,
        widthi: $('#divgride12').width(),
        lostFocusKoloneFundit: lostFocusKoloneFunditV,
        arrayRenditjeKolonaGridesName: "arrayRenditjeKolonaGridesV",
        selektorDivgride3: "#dvArtikulli"


        //colMagazina: colMagazina,

        //autocompleteList:
        //    [{ emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
        //     { emerEditor: "txtMagazina", selectFunc: selectFunc1, changeFunc: changeFunc1, shtoDataKod: false }],

    };
    return myJQGrid.initGride(gridParams);

    //myJQGrid.inicializoGride2("#rowed6", arrayPershkrime, arrayModel, isLidhur, lastsel3, "", "", "", null, null, null, $('#divgride12').width(), undefined, undefined, null, null, '', '', undefined, undefined, undefined, undefined, undefined, undefined);

}
function formGridColsArrayV(isLidhur) {//po                       
    var IdKonfigAmbjenteLupat = [];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridColV']"), arrayIdKolonaGridesV, arrayPershkrimiKolonaGridesV, arrayVisibleKolonaGridesV, arrayReadOnlyKolonaGridesV, isLidhur, arrayWidthKolonaGridesV, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGridesV);
}
function myElemKodVfone(value) {//po
    grida = $('#rowed7');
    var idRow = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGridesV[0];
    return myJQGrid.myElemEmertimi(value, disabled, idRow, arrayIdKolonaGridesV[0]);
}
function myElemPike(value, options) {//po
    if (value == '')
        value = 0;
    grida = $('#rowed7');
    vendosVleraDefaultNeGrideVfone(grida);
    var idRow = grida.getLastSel2();
    disable = arrayReadOnlyKolonaGridesV[1];
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida,value, options, disable, idRow, 'txtPike', keyupPike, hfFormatNumri, changedPike);

}
function myElemVlere(value, options) {//po
    if (value == '')
        value = 0;
    grida = $('#rowed7');
    var idRow = grida.getLastSel2();
    disable = arrayReadOnlyKolonaGridesV[2];
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRow, 'txtVlere', keyupVlere, hfFormatNumri, changedVlere);
}
function myElemButtonFshiV() {//po
    grida = $('#rowed7');
    var idRow = grida.getLastSel2();

    if (arrayReadOnlyKolonaGridesV[arrayReadOnlyKolonaGridesV.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRow, '#rowed7', lostFocusKoloneFunditV);
    else
        return myJQGrid.myElemButtonFshi(false, idRow, '#rowed7', lostFocusKoloneFunditV);
}
function keyupPike() {
  
}
function keyupVlere() {
   
}
function changedPike() {
    grida = $('#rowed7');
    var idRow = grida.getLastSel2();
  
    var formatSasia = hfFormatNumri.Get("FormatZgjedhurSasia");
    var pike = grida.getTekstQelize('txtPike', idRow);
    if (pike === "" || isNaN(pike)) {
        myMesazh.ShtoMesazhGabimi('Piket duhet te jene numer!');
       
        grida.setTekstQelize('txtPike', idRow);

    }
    
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtPike', idRreshti);
    }

}
function changedVlere() {
    grida = $('#rowed7');
    var idRow = grida.getLastSel2();
    var formatSasia = hfFormatNumri.Get("FormatZgjedhurSasia");
    var vlere = grida.getTekstQelize('txtVlere', idRow);
    if (vlere === "" || isNaN(vlere)) {
        myMesazh.ShtoMesazhGabimi('Vlera duhet te jete numer!');
        grida.setTekstQelize('txtVlere', idRow);
    }
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtVlere', idRreshti);
    }
}
function lostFocusKoloneFunditV() {     //po 
    grida = $('#rowed7');
    var idRow = grida.getLastSel2();
    idRow = grida.lostFocusKoloneFundit();
   
    if (($('#' + arrayIdKolonaGridesV[0] + idRow).attr("disabled") == 'disabled')) {
        $('#txtKodVfone' + idRow).focus();
        $('#txtKodVfone' + idRow).blur();
        $('#txtKodVfone' + idRow).focus();
    }
   
}

function myValueButtonFshiV(elem, operation, value) {//po
    grida = $('#rowed7');
    var idRow = grida.getLastSel2();
    if (arrayReadOnlyKolonaGridesV[arrayReadOnlyKolonaGridesV.length - 1] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRow, '#rowed7');
    else
        return myJQGrid.myValueButtonFshi(false, idRow, '#rowed7');


}
function merrTeDhenaVfone(s, e) {//po
    var rreshtaTeGrides = $("#rowed7").jqGrid('getRowData');
    var total = 0;
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        rreshtaTeGrides[i].txtFshiZ = "";
    }
    $('#hfVfone').val(JSON.stringify(rreshtaTeGrides));
}
function keyupGjendjeMin() {
    //changeMagazina();



}
function keyupGjendjeMax() {



}

function changedGjendjeMin() {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    var gjendje = grida.getTekstQelize('txtGjendjaMin', idRow);
    //var editorgjendje = $("#" + 'txtGjendjaMin' + idRow);
    if (gjendje == "" || isNaN(gjendje)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniVleren"));
       // editorgjendje.focus();
        //sasia = '1.00';
        grida.setTekstQelize('txtGjendjaMin', idRow);


    }
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtGjendjaMin', idRreshti);
    }

    //editorSasia = $("#" + 'txtGjendjaMin' + lastsel3);
    //if (editorSasia.val() == "" || isNaN(editorSasia.val())) {
    //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficDuhetNr"));
    //    editorSasia.focus();
    //    grida.setTekstQelize('txtGjendjaMin', lastsel3);
    //    //gjeresi = '1.00';
    //}
    //else
    //    if (editorSasia.val() == "0" || editorSasia.val() == "0.") {
    //        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficJoZero"));
    //        editorSasia.focus();
    //        grida.setTekstQelize('txtGjendjaMin', lastsel3);
    //    }

    //        grida.setTekstQelize('txtGjendjaMin', lastsel3);



}
function changedGjendjeMax() {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    var gjendje = grida.getTekstQelize('txtGjendjaMax', idRow);
   // var editorgjendje = $("#" + 'txtGjendjaMax' + idRow);
    if (gjendje == "" || isNaN(gjendje)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniVleren"));
       // editorgjendje.focus();
        //sasia = '1.00';
        grida.setTekstQelize('txtGjendjaMax', idRow);


    }


    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtGjendjaMax', idRreshti);
    }
    changeMagazina();
    //editorSasia = $("#" + 'txtGjendjaMax' + lastsel3);
    //if (editorSasia.val() == "" || isNaN(editorSasia.val())) {
    //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficDuhetNr"));
    //    editorSasia.focus();
    //    grida.setTekstQelize('txtGjendjaMax', lastsel3);
    //    //gjeresi = '1.00';
    //}
    //else
    //    if (editorSasia.val() == "0" || editorSasia.val() == "0.") {
    //        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficJoZero"));
    //        editorSasia.focus();
    //        grida.setTekstQelize('txtGjendjaMax', lastsel3);
    //    }

    //        grida.setTekstQelize('txtGjendjaMax', lastsel3);
    //    }
    //}


}

function myelemGjendjeMin(value, options) {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: "False", indexRow: idRow, id: "txtGjendjaMin", onKeyDown: keyupGjendjeMin, onFocusout: changedGjendjeMin });
}

function myelemGjendjeMax(value, options) {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: "False", indexRow: idRow, id: "txtGjendjaMax", onKeyDown: keyupGjendjeMax, onFocusout: changedGjendjeMax });
}

function mbushArrayMagazinat() {//po
    var colMag = $('#hfTmpColMag').val();
    if (colMag != '') {
        colMagazina = $.parseJSON(colMag);
        $('#hfTmpColMag').val(''); //boshatisim HF-ne qe mos te harxhojme kot memorie
    }
}

function myelemComboMagazina(value, options, idRreshti) {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    if (idRreshti === undefined)
        idRreshti = idRow;
   
    var arrayOptions;
    var arrayOptions = new Array(0);
    var j = 0;
    if (value == ''&&colMagazina.length>0)
        value = colMagazina[0].Kodi;
    if (colMagazina !== "" || colMagazina !== undefined) {
        arrayOptions = new Array();
        for (var i = 0; i < colMagazina.length; i++) {
            var objNjesi = new Object();
            objNjesi.value = colMagazina[i].IdNjesiAdministrative;
            objNjesi.text = colMagazina[i].Kodi;
            objNjesi.desc = colMagazina[i].Pershkrimi;
            arrayOptions[j] = objNjesi;
            j++;
        }
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaGridesGjendjeArt[0], idRow, changeMagazina, arrayOptions, false);
}

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(grida, formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfFormatNumri.Get("formatMonedhe"));
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosVleraDefaultNeGride(grida);
    vendosKonfigFormatNumri(grida);
    return;
}
function ruajFormatetNeGrideVfOne(grida, formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfFormatNumri.Get("formatMonedhe"));
    ndryshoKonfigFormatNumriVfone(grida, formatNumri);
    vendosVleraDefaultNeGrideVfone(grida);
    vendosKonfigFormatNumriVfone(grida);
    return;
}

function vendosKonfigFormatNumri(grida) {
    //var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtKoeficienti', idRreshti);
        grida.formatoQelize('txtKosto', idRreshti);
    }
}
function vendosKonfigFormatNumriVfone(grida) {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtPike', idRreshti);
        grida.formatoQelize('txtVlere', idRreshti);
    }
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
function vendosVleraDefaultNeGrideVfone(grida) {
    grida.setVlereDefault('txtPike', 0);
    //  grida.setVlereDefault('txtFiro', 0);
    grida.setVlereDefault('txtVlere', 0);
    return;
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
function ndryshoKonfigFormatNumriVfone(grida, formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfFormatNumri.Get("formatMonedhe"));
    else
        hfFormatNumri.Set("formatMonedhe", JSON.stringify(formatNumri));
    grida.setShifraPasPresjes('txtPike', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtVlere', formatNumri.ShifraPasPresjesCmimi);
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
                }
                catch (e) {
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
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulli"),
            data: JSON.stringify({ idartikulli: -1 })
        }).done(SucceededCallbackGjendjeArtikulli);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
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
            if (typeof (kodi) != "undefined" && kodi != undefined && kodi != "") {
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
                            data: JSON.stringify({ kodi: kodi, rreshti: idRresht })
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
                    myMesazh.ShtoMesazhGabimi("Ndodhi nje gabim gjate marrjes se e dhenave!");
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
                    myMesazh.ShtoMesazhGabimi("Ndodhi nje gabim gjate marrjes se e dhenave!");
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
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulli"),
            data: JSON.stringify({ idartikulli: -1 })
        }).done(SucceededCallbackGjendjeArtikulli);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
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
var arrArtikull = new Array();
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
        if (artikulli.Klasa == 4) {
            myMesazh.ShtoMesazhGabimi("Nuk lejohen artikuj te perbere ne recepture");
            resetRreshtKorent(idRreshti);
            return;
        }

        var comboNjesia = $('#txtNjesia' + idRreshti);
        var comboTVSH = $('#' + 'cbNgaStoku' + idRreshti);
        comboNjesia.val(artikulli.KodNjesia1);

        arrArtikull[idRreshti] = artikulli;
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
            kontrolloRow(idRreshti); llogaritKoston(-1);
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
        kontrolloRow(idRreshti); llogaritKoston(-1);
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
        rreshti.txtNjesia = (llogaria.NjesiKohe == 1 ? 'sec' : llogaria.NjesiKohe == 2 ? 'min' : llogaria.NjesiKohe == 3 ? 'ore' : 'dite');
        rreshti.txtKosto = result[2].toFixed(2);
        grida.jqGrid('setRowData', idRreshti, rreshti); kontrolloRow(idRreshti); llogaritKoston(-1);
    }
}
//var grida = $('#rowed5');
//grida.setLastSel2(0);
//var lastsel3 = 0;

function formGridColsArray(hfGridColArray, isLidhur, arrayPershkrimiKolonaGrides, arrayReadOnlyKolonaGrides, arrayIdKolonaGrides, arrayVisibleKolonaGrides, arrayWidthKolonaGrides,arrayRenditjeKolonaGrides) {//po                       
    var IdKonfigAmbjenteLupat = [];
    myJQGrid.formArrayKolGrides(hfGridColArray, arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}
/*
Function: myelemCombo

Nderton combo-n Lloji per griden. Combo ka vlerat: Artikull, Makro, Llogari,  Text, Credit Note, Nentotali
*/
function myelemCombo(value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
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
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[0], idRresht, change, arrayOptions, disabled);
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
function changeMagazina() {//po
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length - 2; j++) {
        var idRreshti = idTe[j];
        var magazina = grida.getTekstQelize('txtMagazina', idRreshti);
        if (magazina === grida.getTekstQelize('txtMagazina', idRow) && idRow != parseInt(idRreshti)) {
            grida.jqGrid('delRowData', idRow);
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgMagNjejte"));
        }
    }
}

function myelemKodi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[2];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[2], ButtonClickKodi, keyPressKodi, changeFunc, undefined, lostFocusKoloneFundit);
}

function myelemIdKodi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1]);
}

function keyPressKodi(event) {     //po           
    var idRresht = $('#rowed5').getLastSel2();
    try {
        var vlera = event.target.value;  //$('#txtKodi' + lastsel2).val();
        if (vlera == "")
            $('#' + arrayIdKolonaGrides[2] + idRresht).autocomplete("close");
        else
            callWebserviceKodi(vlera);
    } catch (e) { }
}

function myValueButtonFshi(elem, operation, value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
    else
        return myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");

}
function myValueButtonFshiR(elem, operation, value) {//po
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    if (arrayReadOnlyKolonaGridesGjendjeArt[arrayReadOnlyKolonaGridesGjendjeArt.length - 1] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRow, '#rowed6');
    else
        return myJQGrid.myValueButtonFshi(false, idRow, '#rowed6');

}
function myElemButtonFshiR() {//po
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    if (arrayReadOnlyKolonaGridesGjendjeArt[arrayReadOnlyKolonaGridesGjendjeArt.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRow, '#rowed6', lostFocusKoloneFunditGjendjeArt);
    else
        return myJQGrid.myElemButtonFshi(false, idRow, '#rowed6', lostFocusKoloneFunditGjendjeArt);
}

function myElemButtonFshi() {//po
    var idRresht = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}



function myElemPershkrimi(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[3];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[3]);
}

function myelemKosto(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[8], indexRow: idRresht, id: "txtKosto", onKeyDown: keyup });
}

function myelemNgaStoku(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[6];
    return myJQGrid.myElemCheckBox(value, disabled, idRresht, arrayIdKolonaGrides[6]);
}

function myelemComboNjesia(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[4];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[4]);
}

function myelemKoeficienti(value, options) {
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
    if (vleraLlojit == "Artikull" && value == '' && txtScrap.GetText() != "0.00")

        value = txtScrap.GetText();
    return myJQGrid.myElemTextBoxVlefte(value, options, disable, idRresht, 'txtFiro', keyup, '0.00', changedFiro);

}

function keyup() {
    console.log("1");
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var koeficient = grida.getTekstQelize('txtKoeficienti', idRresht);
    if (koeficient == '.') {
        grida.setTekstQelize('txtKoeficienti', idRresht, '0.'); return;
    }
    var editorKoeficenti = $("#" + 'txtKoeficienti' + idRresht);
    if (editorKoeficenti.val() == "-")
        return;
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
            //grida.setTekstQelize('txtKoeficienti', lastsel2);
        }    
}

function fshiClicked(index) {//po
    myJQGrid.fshiClicked(index, '#rowed5', inicializoGride);

    llogaritKoston(index);
}
function fshiClicked2(index) {//po
    myJQGrid.fshiClicked(index, '#rowed6', inicializoGrideGjendjeArt);

}
function fshiClicked3(index) {//po
    myJQGrid.fshiClicked(index, '#rowed7', inicializoGrideVfone);
    var grida = $('#rowed7');

}

function callWebserviceKodi(vlera) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
    //var idPerdoruesi = hfState.Get("idPerdoruesi");
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
    var idRresht = $('#rowed5').getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRresht);
}

function lostFocusKoloneFundit() {     //po 
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    idRresht = grida.lostFocusKoloneFundit();
    if (($('#' + arrayIdKolonaGrides[0] + idRresht).attr("disabled") == 'disabled')) {
        $('#txtKodi' + idRresht).focus();
        $('#txtKodi' + idRresht).blur();
        $('#txtKodi' + idRresht).focus();
    }
}

function lostFocusKoloneFunditGjendjeArt() {

    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    idRow = grida.lostFocusKoloneFundit();
    if (($('#' + arrayIdKolonaGridesGjendjeArt[0] + idRow).attr("disabled") == 'disabled')) {
        $('#txtMagazina' + idRow).focus();
        $('#txtMagazina' + idRow).blur();
        $('#txtMagazina' + idRow).focus();
    }
}

function mbushGrideNgaHiddenFieldet() {
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;

    if ($('#hfShtimModifikim').val() == "shtim") {
        //myJQGrid.keyPressKodi("#rowed5", window.lastsel2, arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1]);
        return;
    }
}

function merrTeDhenaArt(s, e) {//po
    var grida = $('#rowed5');
    //var gridaGjendjaArt = $('#rowed6');
    //var rreshta = gridaGjendjaArt.getTeDhenaRreshti();
    //for (i = 0; i < rreshta.length; i++) {
    //    rreshta[i].txtFshi = "";
    //}
    //$('#hfGjendjeArtikulli').val(JSON.stringify(rreshta));

    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    var total = 0;
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        rreshtaTeGrides[i].txtFshi = "";
    }
    $('#hfArtikujtPerberes').val(JSON.stringify(rreshtaTeGrides));

}
function merrGjendjeArt(s, e) {//po

    var gridaGjendjaArt = $('#rowed6');
    // gridaGjendjaArt.jqGrid('saveRow', lastsel3, false, 'clientArray');
    var rreshtaTeGridesGjendjeArt = gridaGjendjaArt.getTeDhenaRreshti();
    for (var j = 0; j < rreshtaTeGridesGjendjeArt.length; j++) {
        rreshtaTeGridesGjendjeArt[j].txtFshiR = "";
    }


    $('#hfGjendjeArtikulli').val(JSON.stringify(rreshtaTeGridesGjendjeArt));
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
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoArtikullZgjidhArt"), 'LupaArtikull.aspx?klasa=' + cmbKlasa.GetValue() + '&idKonfigAmbjente=' + $("#hfLupaArtikuj")[0].value, 600, 500);

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
    console.log("2");
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorSasia = $("#" + 'txtKoeficienti' + idRresht);
    if (editorSasia.val() == "" || isNaN(editorSasia.val())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficDuhetNr"));
        grida.setTekstQelize('txtKoeficienti', idRresht, 1);
        editorSasia.focus();
        
        //gjeresi = '1.00';
    }
    else
        if (editorSasia.val() == "0" || editorSasia.val() == "0.") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficJoZero"));
            grida.setTekstQelize('txtKoeficienti', idRresht, 1);
            editorSasia.focus();            
        }
    var vleraLlojit = grida.getTekstQelize('cmbLloji', idRresht);
    if (vleraLlojit == "Artikull") {
        var art = arrArtikull[idRresht];
        if (art == undefined || isNaN($("#" + 'txtKoeficienti' + idRresht).val())) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoArtikullZgjidhArt"));
            $("#" + 'txtKodi' + idRresht).focus();
            grida.setTekstQelize('txtKoeficienti', idRresht, 1);
        }else
        if (!art.MbetjeShitshme && parseFloat(editorSasia.val()) < 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgClsArtikulliKoefArtPerberesDuhetNumerPozitiv"));
            editorSasia.focus();
            grida.setTekstQelize('txtKoeficienti', idRresht);
        }
    }
    var sasia = editorSasia.val();
    //editorSasia.val(sasia.toFixed(shifraPasPresjes));
    llogaritKoston(-1);
}

function changedFiro() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorSasia = $("#" + 'txtFiro' + idRresht);

    var gjeresi;
    if (editorSasia.val() == "" || isNaN(editorSasia.val())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgFiroNumer"));
        editorSasia.focus();
        gjeresi = '0.00';
    }
    if (parseFloat(editorSasia.val()) < 0 || parseFloat(editorSasia.val()) > 100) {
        editorSasia.val(gjeresi);
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
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosetVetArtikulli"));
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
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonKodiGride"));
            }
            else {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonArtikullGride"));
            }

            break;
        }
    }
    var grida1 = $('#rowed6');
    var idRow = grida1.getLastSel2();
    var gridGjendjeIds = grida1.jqGrid('getDataIDs');
    for (i = 0; i < gridGjendjeIds.length; i++) {
        if (rowid == gridGjendjeIds[i])
            continue;
        var editorMag;
        var rreshti = grida1.jqGrid('getRowData', [gridGjendjeIds[i]]);
        if (rreshti.txtMagazina.toString().search('value') != -1) {
            editorMag = $("#txtMagazina" + idRow).val();
            // editorlloji = $('#cmbLloji' + lastsel2 + ' option:selected').text();
        }
        else {
            editorMag = rreshti.txtMagazina;
            // editorlloji = rreshti.cmbLloji;
        }
        if (editorkodi == name && editorkodi != "" && editorlloji == lloji) {
            resetRreshtKorent(rowid);
            if (lloji == 'Artikull') {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonKodiGride"));
            }
            else {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonArtikullGride"));
            }

            break;
        }
    }
}

function onNdryshimFokusi() {
    if (PageControl.GetActiveTabIndex() == 0)
        mbush = true;
}

function MerrGjendjeKosto() {
    ASPxGridView_Artikull.PerformCallback("kerko;402;" + cmbKonfigurimi.GetText());
}

function merrTeDhenaNorma() {//merren te dhenat qe ka grida
    var gridDataObject = $('#gridDataObject');
    colNorma = new Array();
    if (pageState.eshteArtAfatgjate) {
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

function merrTeDhenaNormaAmortizimi() {//merren te dhenat qe ka grida
    var gridDataObject = $('#gridRezerva');
    colNormaAmort = new Array();
    if (pageState.eshteArtAfatgjate) {
        for (i = 0; i < gvNormaAmortizimi.cpRowCount; i++) {
            colNormaAmort[i] = new Object();
            colNormaAmort[i].Standart = Utils.ktheKontroll('lblStandartR' + i).GetText();
            colNormaAmort[i].IdLlojAmortizimi = Utils.ktheKontroll('cmbLlojAmortizimiR' + i).GetValue();
            colNormaAmort[i].NormeMagazine = Utils.ktheKontroll('cmbNormeMagazineR' + i).GetText();
            colNormaAmort[i].Norme = Utils.ktheKontroll('txtNormeR' + i).GetText();
        }
    }
    gridDataObject.val(JSON.stringify(colNormaAmort));
}




function Seriali_Click() {
    if (!hapLupeSerialeSipasGrupArt || btneKodifikimi1.GetText() == '')
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgSerialetLupa"), 'Seriale.aspx?idartikulli=' + $('#hfId').val() + '&idKonfigAmbjente=' + $("#hfSeriale").val(), 700, 500);
    else {
        //te hapet me idkonfigambjente te konfigurimit me kodin njesoj me grupin e artikullit te selektuar
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "ktheIdKonfigurimiSipasKodit"),
            data: JSON.stringify({ kodKonfig: btneKodifikimi1.GetText(), idNderm: hfState.Get('idNdermarrje') })
        }).done(function (result) {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgSerialetLupa"), 'Seriale.aspx?idartikulli=' + $('#hfId').val() + '&idKonfigAmbjente=' + result, 700, 500);
        });
    }
}

function Updatenormat(s)
{
    gvAmortizimi.PerformCallback(s.GetValue());
    gvNormaAmortizimi.PerformCallback(s.GetValue());
}


function merrSkeme(s) {
    if (pageState.eshteArtAfatgjate && s.GetValue() != null && mbushtedhenasipagrupit) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "merrSkeme"),
            data: JSON.stringify({ idkodifikim: s.GetValue() })
        }).done(SucceededCallbackSkemaPrindi);
    }
    
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
    gvAmortizimi.PerformCallback(result.IdKodifikimi);
    gvNormaAmortizimi.PerformCallback(result.IdKodifikimi);
}

/*
Function: Auto_Click
    
Hap lupen e automjeteve.
*/
function Auto_Click() { //po    
    var hfAuto = $("#hfLupaAutomjet");
    var queryStr = hfAuto.val();
    popupUniversal.SetContentUrl('LupaAutomjetShpejte.aspx?vjenNga=ShtoArtikull&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetHeaderText('Shto Automjetin');
    popupUniversal.SetSize(widthLupaAutomjet, heightLupaAutomjet);
    popupUniversal.Show();
}

/*
funksion qe heq selektimin nga kontrolli i gjendjes ne nivel detajimi,kur hiqet selektimi i kontrollit
te gjendjes.
*/
function KontrollGjendjeDetajim() {
    if (checkKontrollGjendjeArtikulli.GetCheckState() == 'Unchecked') {
        checkKontrollGjendje.SetChecked(false);
        checkKontrollGjendjeDetajim2.SetChecked(false);
    }
}

function kontrolloKod() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Celje", "kontrolloEkzistonKodObjekti"), data: JSON.stringify({ kodi: txtKodi.GetText(), idObjekti: $('#hfId').val(), idndermarje: hfState.Get("idNdermarrje"), idPerdorues: hfState.Get("idPerdoruesi"), kategoria: "13" })
    }).done(SucceededCallbackKontrolloKod);
}

function SucceededCallbackKontrolloKod(result) {
    if (result) {
        myMesazh.ShtoMesazhGabimi("Ekziston nje artikull me kete kod!");
        txtKodi.GetMainElement().style.borderColor = 'red';
    }
    else {
        txtKodi.GetMainElement().style.borderColor = 'green';
    }
}

function LidhMeNdermRap_Click() {
    identikuesPerPopupArtikulli = 'LidhArtikull';
    myButtonClickLupa.LupaUniversal_Click(hfState.Get('headerPopUpZgjidhArtikullin'), 'LupaArtikull.aspx?vjenNgaKartelaArt=true', 860, 560);
}
function TextChanged_txtKodi(s, e) {
    txtKodi3.SetText(txtKodi.GetText());
    txtKodi2.SetText(txtKodi.GetText());
    txtKodi4.SetText(txtKodi.GetText());
    txtKodi5.SetText(txtKodi.GetText());
    kontrolloKod();
}

function TextChanged_txtPershkrimi(s, e) {
    txtPershkrimi3.SetText(txtPershkrimi.GetText());
    txtPershkrimi2.SetText(txtPershkrimi.GetText());
    txtPershkrimi4.SetText(txtPershkrimi.GetText());
    txtPershkrimi5.SetText(txtPershkrimi.GetText());
}

function TextChanged_txtKodi3(s, e) {
    txtKodi.SetText(txtKodi3.GetText());
    txtKodi2.SetText(txtKodi3.GetText());
    txtKodi4.SetText(txtKodi3.GetText());
    txtKodi5.SetText(txtKodi3.GetText());
}

function TextChanged_txtPershkrimi3(s, e) {
    txtPershkrimi.SetText(txtPershkrimi3.GetText());
    txtPershkrimi2.SetText(txtPershkrimi3.GetText());
    txtPershkrimi4.SetText(txtPershkrimi3.GetText());
    txtPershkrimi5.SetText(txtPershkrimi3.GetText());
}
function TextChanged_btneArtikuj(s, e) {
    var hf = $('#hfArtikulli')[0];
    var listeFushash = hf.value.split(',');
    var gja = listeFushash[0].length;
    hf.value = '0:' + btneArtikuj.GetText() + hf.value.substring(gja, hf.value.length);
}

function TextChanged_txtKodi2(s, e) {
    txtKodi.SetText(txtKodi2.GetText());
    txtKodi3.SetText(txtKodi2.GetText());
    txtKodi4.SetText(txtKodi2.GetText());
    txtKodi5.SetText(txtKodi2.GetText());
}

function TextChanged_txtPershkrimi2(s, e) {
    txtPershkrimi.SetText(txtPershkrimi2.GetText());
    txtPershkrimi3.SetText(txtPershkrimi2.GetText());
    txtPershkrimi4.SetText(txtPershkrimi2.GetText());
    txtPershkrimi5.SetText(txtPershkrimi2.GetText());
}

function TextChanged_txtKodi4(s, e) {
    txtKodi.SetText(txtKodi4.GetText());
    txtKodi2.SetText(txtKodi4.GetText());
    txtKodi3.SetText(txtKodi4.GetText());
    txtKodi5.SetText(txtKodi4.GetText());
}

function TextChanged_txtPershkrimi4(s, e) {
    txtPershkrimi.SetText(txtPershkrimi4.GetText());
    txtPershkrimi2.SetText(txtPershkrimi4.GetText());
    txtPershkrimi3.SetText(txtPershkrimi4.GetText());
    txtPershkrimi5.SetText(txtPershkrimi4.GetText());
}

function UpdateGrida(s) {
    gvAmortizimi.PerformCallback(s);
    gvNormaAmortizimi.PerformCallback(s);
}

function nrLlogariChange() {

}

function llogari_TextChanged() {

}

function OnChange(s, e) {
    var v = s.GetText().split(';');
    s.SetText(v[0]);
}

function ReloadFaqe(url) {
    myFaqeCelje.Kontrollote;
}

function RifreskoGride() {
    ASPxGridView_Artikull.PerformCallback("CustomButtonRefreshGrid");
}