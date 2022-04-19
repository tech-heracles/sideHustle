;
var mbushqendra;
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var widthlupa = 900;
var heightlupa = 600;
// kontrollon nese ekziston filtri

function checkText(s, e) {
    myMenu.checkText(s, e);
}

$(document).ready(function () {
    changeName();
});
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
        case 8:
            if (!$(e.target).is("input, textarea")) {
                e.preventDefault();
            }
            break;
        default:
            break;

    }
});

var btnFiltrat;
//aplikon filtrin
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvPunonjesit, "708", cmbKonfigurimi.GetText());
}
//kur ndryshon tekstin
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvPunonjesit, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvPunonjesit, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes

Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvPunonjesit, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvPunonjesit, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/

var Klonim = false;
function menuClick(s, e, doPostback) {
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    e.processOnServer = true;
    Klonim = e.item.name === "Klono";

    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name === 'Ruaj') {
        if (dteDateAktQK.GetDate() == null)
            dteDateAktQK.SetDate(new Date());
        if (dteDateAktBanda.GetDate() == null)
            dteDateAktBanda.SetDate(new Date());
        ruajPagaShtesa(e);
    }
    if (e.item.name == "Arkiva") {
        ButtonClickArkiva();
        e.processOnServer = false;
    }
    if (e.item.name === "Klono") {
        hfNrAuto.Clear();
        hfNrAutoKF.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    if (e.item.name == "Shto") {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "FshiPunesimeNgaSessioni")
        });
        e.processOnServer = false;
    }
    if (e.item.name === "ShtoPunesim") {
        e.processOnServer = false;
        PageControl.SetActiveTabIndex(3);
        shtoPunesim(true);
    }
    if (e.item.name === 'RuajPunesim') {
        validoPun(s, e, PageControl);

    }
    if (e.item.name === "FshiPunesim") {
        e.processOnServer = false;

        if (gvPunesim.GetSelectedRowCount() != 0) {

            myMesazh.ShtoMesazh({
                text: hfState.Get("msgPyetjeFshirjePunesim"),
                type: "confirm",
                modal: true,
                idGjuha: hfState.Get('idGjuha'),
                okClick: function () {
                    gvPunesim.PerformCallback('FshiPunesim');
                    $('#hfStatusiPunesim').val('true');
                },
                cancelClick: function () {}
            });
        }else
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniPunesim"));
    }
    Utils.doPostback(s, e, doPostback);
}


function menu_click(s, e) {
    if (!Utils.KanePerfunduarWs()) {
        Utils.shtoFunksionNeRadheMeParametra(menuClick, [s, e, true], parseInt(hfState.Get("idGjuha")), this);
        e.processOnServer = false;
        return;
    }
    menuClick(s, e, false);
}

function mbushfushaexp(s, e) {
    for (i = 0; i < gvKomponenteListPagese.cpRowCount; i++) {
        if (gvKomponenteListPagese.IsDataRow(i)) {
            Utils.ktheKontroll('cbDukshme' + i).SetChecked(hfKomponente.Get(Utils.ktheKontroll('lblKod' + i).GetText()));
            Utils.ktheKontroll('cbDetyrueshme' + i).SetChecked(hfKomponente2.Get(Utils.ktheKontroll('lblKod' + i).GetText()));
            try {
                Utils.ktheKontroll('txtVleraC' + i).SetText(hfKomponente3.Get(Utils.ktheKontroll('lblKod' + i).GetText()));///kur nuk eshte e dukshme jep error

            } catch (ex) {

            }
        }
    }
}

function ButtonClickArkiva() {
    var idObjekti = ($('#hfShtimModifikim').val() != "shtim" || Utils.IsNullOrEmpty($('#hfShtimModifikim').val()) || PageControl.GetActiveTab().index == 0) ? gvPunonjesit.GetRowKey(gvPunonjesit.GetFocusedRowIndex()) : 0;
    if (idObjekti == -1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPunonjesDuhetTeZgjidhni1Punonjes"));
        return;
    }

    Utils.hapLupe({ emerPopUpi: popupUniversal, titull: hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"), baseUrl: "LupaArkiva.aspx", width: 738, height: 548, params: { vjenNga: "Lista", veprimi: "punonjes", idDok: idObjekti, tmpfolder: hfArkiva.Get("rootFolder") } });
}

function duksmecheck(kod, fusha) {
    hfKomponente.Set(kod, Utils.ktheKontroll(fusha).GetChecked());
}
function detyrueshmecheck(kod, fusha) {
    hfKomponente2.Set(kod, Utils.ktheKontroll(fusha).GetChecked());
}
function vleracheck(kod, fusha) {
    hfKomponente3.Set(kod, Utils.ktheKontroll(fusha).GetText());
}

//ruan pagat dhe shtesat
function ruajPagaShtesa(e) {

    if (isNaN(txtLlogBankare.GetText()) && $('#hfLejoNrLlogBank').val() == "False" ) {
    myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlogariaBankareDuhetTePermbajVetemNumra"));
    e.processOnServer = false;
    return;
  }
    var arrparam = new Array();
    var arrvlera = new Array();
    for (i = 0; i < gvPagaShtesa.cpRowCount; i++) {
        arrparam[i] = Utils.ktheKontroll('txtVleraParam' + i).GetText();
        arrvlera[i] = Utils.ktheKontroll('txtVlera' + i).GetText();
        if (arrparam[i] === '')
            arrparam[i] = 0;
        if (arrvlera[i] === '')
            arrvlera[i] = 0;
    }
    $('#hfParam').val(JSON.stringify(arrparam));
    $('#hfVlera').val(JSON.stringify(arrvlera));
}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvPunonjesit.GetFocusedRowIndex();
    if (indexModifiko === -1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPunonjesDuhetTeZgjidhni1Punonjes"));
        Klonim = false;
    }
    else {
        Utils.shfaqLoadingGif();;
        $.ajax({
            url: Utils.getWebMethodUrl("Shto_Punonjes", "GetPunonjesSipasID"),
            data: JSON.stringify({ idPunonjesi: gvPunonjesit.GetRowKey(indexModifiko) }),
            pritPergjigje: true
        }).done(function (result) {
            var teDhena = JSON.parse(result.d);
            //var values = Object.keys(teDhena).map(function (key) { return teDhena[key] });
            $('#hfId').val(teDhena.IdPunonjes);
            Utils.hiqLoadingGif();;
            //return;
            if (!Klonim)
                txtNrPersonal.SetText(teDhena.NrPersonal);

            txtEmri.SetText(teDhena.Emer);
            txtEmri2.SetText(teDhena.Emer + ' ' + teDhena.Mbiemer);
            txtEmri3.SetText(teDhena.Emer + ' ' + teDhena.Mbiemer);
            txtEmri4.SetText(teDhena.Emer + ' ' + teDhena.Mbiemer);
            txtEmri5.SetText(teDhena.Emer + ' ' + teDhena.Mbiemer);
            txtEmri15.SetText(teDhena.Emer + ' ' + teDhena.Mbiemer);
            txtEmri6.SetText(teDhena.Emer + ' ' + teDhena.Mbiemer);
            txtEmri7.SetText(teDhena.Emer + ' ' + teDhena.Mbiemer);
            txtMbiemri.SetText(teDhena.Mbiemer);
            txtAtesia.SetText(teDhena.Atesia);
            var dtLindja = Utils.KtheDateOseBosh(teDhena.Datelindja);
            if (dtLindja == "") dteDatelindja.SetDate(null);
            else dteDatelindja.SetDate(dtLindja);


            txtNrSig.SetText(teDhena.NrSig);
            Utils.SelectComboItem(cmbQyteti, teDhena.IdQyteti, teDhena.Qyteti);
            //cmbQyteti.SetSelectedIndex(cmbQyteti.AddItem(values[21], values[7]));
            txtAdresa.SetText(teDhena.Adresa);
            cbAktiv.SetChecked(teDhena.Aktiv);
            cbLlogaritNgaListorare.SetChecked(teDhena.LlogaritNgaListorare);
            txtTel.SetText(teDhena.Telefon);
            txtEmail.SetText(teDhena.Email);
            txtEmriKontakti.SetText(teDhena.EmerKontakti);
            txtMbiemriKontakti.SetText(teDhena.MbiemerKontakti);
            txtAdresaKontakti.SetText(teDhena.AdresaKontakti);
            txtTelKontakti.SetText(teDhena.TelKontakti);
            txtEmailKontakti.SetText(teDhena.EmailKontakti);
            txtShenimeKontakti.SetText(teDhena.ShenimeKontakti);
            Utils.SelectComboItem(cmbGrupi, teDhena.IdGrupPunonjesish, teDhena.GrupPunonjesish)
            cmbLlojPagese.SetValue(teDhena.LlojPagese);
            Utils.SelectComboItem(cmbObjektiva, teDhena.IdObjektivaKosto, teDhena.Objektiva);
            Utils.SelectComboItem(cmbMonedha, teDhena.IdMonedha, teDhena.Monedha)
            dteDtFillimi.SetDate(new Date());
            txtSap.SetText(teDhena.SapId);
            txtNrPashaporte.SetText(teDhena.NrPashaporte);
            if (teDhena.Gjinia) cmbGjinia.SetSelectedIndex(1); else cmbGjinia.SetSelectedIndex(0);
            if (teDhena.Kombesia != null)
                cmbKombesia.SetValue(teDhena.Kombesia);
            else cmbKombesia.SetSelectedIndex(0);

            cbKryefamiliar.SetChecked(teDhena.Kryefamiliar);
            if (teDhena.Edukimi != null)
                cmbEdukimi.SetValue(teDhena.Edukimi);
            else cmbEdukimi.SetSelectedIndex(0);
            if (teDhena.PunaMeparshme != null)
                cmbPunaMeparshme.SetValue(teDhena.PunaMeparshme);
            else cmbPunaMeparshme.SetSelectedIndex(0);

            if (teDhena.Vendndodhja == null)
                cmbVendndodhjet.SetValue(null);
            else {
                Utils.SelectComboItem(cmbVendndodhjet, teDhena.Vendndodhja, teDhena.PershkrimiVendodhja);
            }

            txtNrJupiter.SetText(teDhena.NrJupiter);
            txtUsername.SetText(teDhena.Username);
            txtShenime.SetText(teDhena.Shenime);
            txtNrRendor.SetText(teDhena.NrRendor);
            if (cmbKombesia.GetValue() > 2)
                txtLejePune.SetEnabled(true);
            else
                txtLejePune.SetEnabled(false);
            txtLejePune.SetText(teDhena.LejePune);

            if (teDhena.IdLlogari == null) {
                cmbIdLlogari.SetSelectedIndex(-1);
                cmbIdLlogari.SetText('');
            }
            else Utils.SelectComboItem(cmbIdLlogari, teDhena.IdLlogari, teDhena.IdLlogari);
             
            mbushpunesim = !Klonim;
            mbushqendra = true;
            mbushbanda = true;

            $.ajax({
                url: Utils.getServerApiUrl("ListPagesa", "merrTeDhenaPerPunonjesDheListePagesatEShtesaPaga"),
                data: JSON.stringify({ idkomp: "708", kodkonfi: cmbKonfigurimi.GetText(), idpunonjes: teDhena.IdPunonjes, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') , klonim: Klonim }),
                pritPergjigje: true
            }).done(function (result) {
                //some instrutions
                SucceededCallbacMerrTedhenaPerPunonjesDheListePagesashEShtesaPaga(result, teDhena.IdPunonjes);
            }).fail(function (err) {
                console.log(err);

            });

            if (kaloTab) {
                PageControl.SetActiveTabIndex(1);
                myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
            }
        }).fail(function (err, e) {
            console.log(err);
        }).always(function () {
            Klonim = false;
        });
    }
}

//merr te dhenat per reshtin e punesimeve kur ben double click
function mbushFushaPunesim() {
    indexModifiko = gvPunesim.GetFocusedRowIndex();
    $.ajax({
        url: Utils.getWebMethodUrl("Shto_Punonjes", "GetPunonesimSipasID"),
        data: JSON.stringify({ idPunesim: gvPunesim.GetRowKey(indexModifiko), idPunonjesi: $('#hfId').val() }),
        pritPergjigje: true
    }).done(function (result) {
        var punesim = JSON.parse(result.d);
        $('#hfIdPunesim').val(punesim.IdPunesim);
        //cmbDepartamenti.SetSelectedIndex(cmbDepartamenti.AddItem(values[14], values[1]));
        if (punesim.IdDepartament > 0)
            Utils.SelectComboItem(cmbDepartamenti, punesim.IdDepartament, punesim.Departament);
        else cmbDepartamenti.SetSelectedIndex(-1);
        if (cmbDepartamenti.GetText() === '')
            cmbNenDepartamenti.SetEnabled(false);
        else cmbNenDepartamenti.SetEnabled(true);
        Utils.SelectComboItem(cmbNenDepartamenti, punesim.IdNenDepartament, punesim.NenDepartament);
        //  cmbNenDepartamenti.SetSelectedIndex(cmbNenDepartamenti.AddItem(values[15], values[2]));
        txtDetyra.SetText(punesim.Detyra);
        txtNrKontrate.SetText(punesim.NrKontrate);
        if (punesim.IdTipKontrate > 0)
            Utils.SelectComboItem(cmbTipKontrate, punesim.IdTipKontrate, punesim.TipKontrate);
        else cmbTipKontrate.SetSelectedIndex(-1);
        //cmbTipKontrate.SetSelectedIndex(cmbTipKontrate.AddItem(values[16], values[5]));


        var dtPerfundimi = Utils.KtheDateOseBosh(punesim.DtPerfundimi);
        if (dtPerfundimi == "") dteDtPerfundimi.SetDate(null);
        else dteDtPerfundimi.SetDate(dtPerfundimi);
        var dtFillimi = Utils.KtheDateOseBosh(punesim.DtFillimi);
        if (dtFillimi == "") dteDtFillimi.SetDate(null);
        else dteDtFillimi.SetDate(dtFillimi);
        //txtLlogBankare.SetText(values[8]);
        //cmbBanka.SetSelectedIndex(cmbBanka.AddItem(values[19], values[9]));
        // cmbGrupi.SetSelectedIndex(cmbGrupi.AddItem(values[29], values[28]));
        Utils.SelectComboItem(cmbGrupi, punesim.IdGrupPunonjesish, punesim.GrupPunonjesish);
        cbLarguar.SetChecked(punesim.Larguar);
        var dtLargimi = Utils.KtheDateOseBosh(punesim.DtLargimi);
        if (!punesim.Larguar || dtLargimi == "")
            dteDtLargimi.SetDate(null);
        else
            dteDtLargimi.SetDate(dtLargimi);
        dteDtLargimi.SetEnabled(punesim.Larguar);
        txtArsyeja.SetText(punesim.Arsyeja);
        txtArsyeja.SetEnabled(punesim.Larguar);
        txtPeriudhaNjoftimi.SetText(punesim.PeriudhaNjoftimi);
        txtPeriudhaProve.SetText(punesim.PeriudhaProve);
        txtPeriudhaProve.SetEnabled(punesim.NeProve);
        cbNeProve.SetChecked(punesim.NeProve);
        if (punesim.IdKodeProfesione > 0)
            Utils.SelectComboItem(cmbKodeProfesione, punesim.IdKodeProfesione, punesim.KodeProfesione);
        else cmbKodeProfesione.SetSelectedIndex(-1);
        // cmbKodeProfesione.SetSelectedIndex(cmbKodeProfesione.AddItem(values[30], values[31]));
        // cmbProfesioni.SetSelectedIndex(cmbProfesioni.AddItem(values[26], values[17]));// SetValue(values[17]);
        Utils.SelectComboItem(cmbProfesioni, punesim.IdProfesioni, punesim.Profesioni);
        Utils.SelectComboItem(cmbTitull, punesim.IdTitullPune, punesim.TitullPune);
        //  cmbTitull.SetSelectedIndex(cmbTitull.AddItem(values[27], values[18]));
        //cmbTitull.SetValue(values[18]);

        cbShifte.SetChecked(punesim.PunonjesTurne);
        cbKomisione.SetChecked(punesim.MeKomisione);
        cbStandBy.SetChecked(punesim.PunonjesGatishmeri);
        if (punesim.Statusi != null)
            cmbStatusi.SetValue(punesim.Statusi);
        else cmbStatusi.SetSelectedIndex(0);

        if (punesim.NdryshimPozicioni != null)
            cmbNryshimPozicioni.SetValue(punesim.NdryshimPozicioni);
        else cmbNryshimPozicioni.SetSelectedIndex(0);
        var dtNenshkrimi = Utils.KtheDateOseBosh(punesim.DtNenshkrimi);
        if (dtNenshkrimi == "")
            dteDtNenshkrimi.SetDate(null);
        else dteDtNenshkrimi.SetDate(dtNenshkrimi);

        txtShenimePun.SetText(punesim.Shenime);
        var dtAktivizimi = Utils.KtheDateOseBosh(punesim.DtAktivizimi);
        if (dtAktivizimi == "")
            dteDateAktPun.SetDate(null);
        else dteDateAktPun.SetDate(dtAktivizimi);
        $('#hfShtimModifikimPunesim').val('modifikim');

    }).fail(function (err) {
        console.log(err);
    });
}
function mbushFushaQendra() {
    indexModifiko = gvQendra.GetFocusedRowIndex();
    $.ajax({
        url: Utils.getWebMethodUrl("Shto_Punonjes", "GetQendraKostoSipasID"),
        data: JSON.stringify({ id: gvQendra.GetRowKey(indexModifiko), idPunonjesi: $('#hfId').val() }),
        pritPergjigje: true
    }).done(function (result) {
        var qendra = JSON.parse(result.d);
        $('#hfIdQendra').val(qendra.Id);
        //  cmbDepartamenti.SetSelectedIndex(cmbDepartamenti.AddItem(values[14], values[1]));

        Utils.SelectComboItem(cmbQK1, qendra.IdQenderKosto1, qendra.Qendra1);
        //cmbQK1.SetSelectedIndex(cmbQK1.AddItem(values[7], values[2]));
        Utils.SelectComboItem(cmbQK2, qendra.IdQenderKosto2, qendra.Qendra2);

        dteDateAktQK.SetDate(new Date(qendra.DtAktivizimi));
        $('#hfShtimModifikimQendra').val('modifikim');

    }).fail(function (err) {
        console.log(err);
    });
}
function mbushFushaBanda() {
    indexModifiko = gvBanda.GetFocusedRowIndex();
    $.ajax({
        url: Utils.getWebMethodUrl("Shto_Punonjes", "GetBandaSipasID"),
        data: JSON.stringify({ id: gvBanda.GetRowKey(indexModifiko), idPunonjesi: $('#hfId').val() }),
        pritPergjigje: true
    }).done(function (result) {
        var banda = JSON.parse(result.d);
        $('#hfIdBanda').val(banda.Id);
        //  cmbDepartamenti.SetSelectedIndex(cmbDepartamenti.AddItem(values[14], values[1]));


        Utils.SelectComboItem(cmbGlobal, banda.IdGrupimGlobal, banda.Global);
        //cmbGlobal.SetSelectedIndex(cmbGlobal.AddItem(values[9], values[4]));
        Utils.SelectComboItem(cmbLocal, banda.IdGrupimLokal, banda.Local);
        // cmbLocal.SetSelectedIndex(cmbLocal.AddItem(values[10], values[5]));
        dteDateAktBanda.SetDate(new Date(banda.DtAktivizimi));
        $('#hfShtimModifikimBanda').val('modifikim');

    }).fail(function (err) {
        console.log(err);
    });
}
var mbushpunesim = false;
var mbushqenda = false;
var mbushbanda = false;

function SucceededCallbacMerrTedhenaPerPunonjesDheListePagesashEShtesaPaga(result, idObjekti) {
    SucceededCallbackLidhur(result.lidhur, idObjekti, result.lidhurPunMeVepArkeBanke);
    SucceededCallbackPagaShtesa(result.result);
}
//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti, isLidhurPunMeVepArkeBanke) {

    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()), eval(isLidhurPunMeVepArkeBanke.toLowerCase()));
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "merrDataKomponenteListPagesaDheShtesaPaga"),
        data: JSON.stringify({ idpunonjes: $('#hfId').val(), klonim: $('#hfShtimModifikim').val() == "klonim" }),
        pritPergjigje: true
    }).done(function (result) {
        //some instrutions
        SucceededCallbackPagaShtesa(result);
    }).fail(function (err) {
        console.log(err);

    });
    //gvKomponenteListPagese.ClearFilter();
    $('#hfStatusiPunesim').val('');
    cmbIdLlogari.SetSelectedIndex(-1);
    cmbIdLlogari.SetText('');
    txtNrPersonal.SetText('');
    txtEmri.SetText('');
    txtEmri2.SetText('');
    txtEmri3.SetText('');
    txtEmri4.SetText('');
    txtEmri5.SetText('');txtEmri15.SetText('');
    txtEmri6.SetText('');
    txtEmri7.SetText('');
    txtMbiemri.SetText('');
    txtAtesia.SetText('');
    cbDetyrueshme.SetChecked(false);
    cbDukshme.SetChecked(false);
    dteDatelindja.SetDate(new Date());
    txtNrSig.SetText('');
    cmbQyteti.SetSelectedIndex(-1);
    txtAdresa.SetText('');
    cbAktiv.SetChecked(true); cbLlogaritNgaListorare.SetChecked(false);
    txtTel.SetText('');
    txtEmail.SetText('');
    txtEmriKontakti.SetText('');
    txtMbiemriKontakti.SetText('');
    txtAdresaKontakti.SetText('');
    txtTelKontakti.SetText('');
    txtEmailKontakti.SetText('');
    txtShenimeKontakti.SetText('');
    cmbGrupi.SetSelectedIndex(-1);
    cmbObjektiva.SetSelectedIndex(-1);

    cmbDepartamenti.SetSelectedIndex(-1);;
    cmbNenDepartamenti.SetSelectedIndex(-1);

    cmbTipKontrate.SetSelectedIndex(-1);
    departamentiChanged();
    txtDetyra.SetText('');
    txtNrKontrate.SetText('');
    cmbLlojPagese.SetSelectedIndex(0);
    cmbMonedha.SetSelectedIndex(0);
    dteDtFillimi.SetDate(new Date());
    dteDtPerfundimi.SetDate(null);
    txtLlogBankare.SetText(''); txtLimitTel.SetText('0.00'); txtLimitInternet.SetText('0.00');
    cmbBanka.SetText('');
    cbLarguar.SetChecked(false);
    dteDtLargimi.SetDate(null);
    dteDtLargimi.SetEnabled(false);
    txtPeriudhaProve.SetText('');
    txtPeriudhaProve.SetEnabled(false);
    cbNeProve.SetChecked(false);
    txtPeriudhaNjoftimi.SetText('');
    txtArsyeja.SetText(''); txtArsyeja.SetEnabled(false);
    cmbSkemaSigurimi.SetSelectedIndex(0);
    cmbNdryshimi.ClearItems();
    cmbNdryshimi.SetText('');
    dteDateAkt.SetDate(new Date());
    cmbNdryshim2.ClearItems();
    cmbNdryshim2.SetText('');
    dteDateAk2.SetDate(new Date())
    txtSap.SetText('');
    txtNrPashaporte.SetText('');
    cmbGjinia.SetSelectedIndex(0);
    cmbKombesia.SetSelectedIndex(1);
    cbKryefamiliar.SetChecked(false);
    cmbEdukimi.SetSelectedIndex(0);
    cmbPunaMeparshme.SetSelectedIndex(0);
    cmbVendndodhjet.SetSelectedIndex(-1);
    cmbVendndodhjet.PerformCallback(); txtLejePune.SetText('');
    txtNrJupiter.SetText(''); txtNrRendor.SetText('0');
    txtUsername.SetText('');
    txtShenime.SetText('');
    cmbProfesioni.SetSelectedIndex(-1);
    cmbTitull.SetSelectedIndex(-1);
    cmbKodeProfesione.SetValue(null);
    cbShifte.SetChecked(false); cbKomisione.SetChecked(false);
    cbStandBy.SetChecked(false);
    cmbStatusi.SetSelectedIndex(1);;
    cmbNryshimPozicioni.SetSelectedIndex(0);
    if (cmbKombesia.GetValue() > 2) txtLejePune.SetEnabled(true); else txtLejePune.SetEnabled(false);
    dteDtNenshkrimi.SetDate(null)

    txtShenimePun.SetText('');
    dteDateAktPun.SetDate(new Date());
    cmbQK1.SetText('');
    cmbQK2.SetText('');
    cmbGlobal.SetText('');
    cmbLocal.SetText('');
 dteDateAktBanda.SetDate(new Date());
    dteDateAktQK.SetDate(new Date());
    MerrSkemaSigurimi();

    $('#hfShtimModifikimPunesim').val('shtim');
    $('#hfShtimModifikimQendra').val('shtim');
    $('#hfShtimModifikimBanda').val('shtim');
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    hfArkiva.Clear();
}

///pastron fushat per punesimin
function shtoPunesim(nrkont) {

    $('#hfShtimModifikimPunesim').val('shtim');
    cmbGrupi.SetSelectedIndex(-1);
    cmbDepartamenti.SetSelectedIndex(-1);
    departamentiChanged();
    txtDetyra.SetText('');
    cmbTipKontrate.SetSelectedIndex(-1);
    //alert(1);
    if (!($('#hfShtimModifikim').val() == 'shtim'))
        txtNrKontrate.SetText('');
    dteDtFillimi.SetDate(new Date());
    dteDtPerfundimi.SetDate(null);
    txtLlogBankare.SetText('');
    cmbBanka.SetText('');
    cmbBanka.SetText('');
    cbLarguar.SetChecked(false);
    dteDtLargimi.SetDate(null);
    dteDtLargimi.SetEnabled(false);
    txtPeriudhaProve.SetText('');
    txtPeriudhaProve.SetEnabled(false);
    cbNeProve.SetChecked(false);
    txtPeriudhaNjoftimi.SetText('');
    txtArsyeja.SetText(''); txtArsyeja.SetEnabled(false);
    cmbProfesioni.SetText('');
    cmbTitull.SetText(''); cmbKodeProfesione.SetText('');
    cmbProfesioni.SetSelectedIndex(-1);
    cmbTitull.SetSelectedIndex(-1);
    cbShifte.SetChecked(false); cbKomisione.SetChecked(false);
    cbStandBy.SetChecked(false);
    cmbStatusi.SetSelectedIndex(1);;
    cmbNryshimPozicioni.SetSelectedIndex(0);

    dteDtNenshkrimi.SetDate(null)

    txtShenimePun.SetText('');
    dteDateAktPun.SetDate(new Date());
}
function shtoQendra() {
    $('#hfShtimModifikimQendra').val('shtim');

    cmbQK1.SetText('');
    cmbQK2.SetText('');


    dteDateAktQK.SetDate(new Date());
}function shtoBanda() {
    $('#hfShtimModifikimBanda').val('shtim');


    cmbGlobal.SetText('');
    cmbLocal.SetText('');

    dteDateAktBanda.SetDate(new Date());
}
///mbush kombon e skemave te sigurimit
function SucceededCallbackSkema(result) {
    cmbSkemaSigurimi.ClearItems();
    cmbSkemaSigurimi.BeginUpdate();
    for (i = 0; i < result[0].length; i++)
        cmbSkemaSigurimi.AddItem(result[0][i].Kodi, result[0][i].IdSigurime); //AddItem(teksti, vlera);
    if (result[1] !== null && result[1].IdSigurimi !== 0)
        cmbSkemaSigurimi.SetValue(result[1].IdSigurimi);
    else cmbSkemaSigurimi.SetSelectedIndex(0);
    cmbSkemaSigurimi.EndUpdate();


}
///mbush kombon e ndryshimit te pages dhe shtesa
function SucceededCallbackPagaShtesa(result) {
    cmbNdryshimi.ClearItems();
    for (i = 0; i < result[1].length; i++)
        cmbNdryshimi.AddItem(result[1][i], result[1][i]); //AddItem(teksti, vlera);
    cmbNdryshimi.SetSelectedIndex(0);
    if (cmbNdryshimi.GetText() !== "")
        dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));
    else dteDateAkt.SetDate(new Date());
    cmbNdryshim2.ClearItems();
    for (i = 0; i < result[0].length; i++)
        cmbNdryshim2.AddItem(result[0][i], result[0][i]); //AddItem(teksti, vlera);
    cmbNdryshim2.SetSelectedIndex(0);
    if (cmbNdryshim2.GetText() !== "") {
        dteDateAk2.SetDate(new Date(cmbNdryshim2.GetText().split('/')[1] + '/' + cmbNdryshim2.GetText().split('/')[0] + '/' + cmbNdryshim2.GetText().split('/')[2]));
    }
    else {
        dteDateAk2.SetDate(new Date());
    }
    MerrSkemaSigurimi();
    txtLlogBankare.SetText(result[2].LlogBankare);
    txtLimitTel.SetText(result[2].LimitTel);
    txtLimitInternet.SetText(result[2].LimitInternet);
    if (result[2].IdBanka == undefined) {
        cmbBanka.SetValue(null);
    } else {
        Utils.SelectComboItem(cmbBanka, result[2].IdBanka, result[2].Banka);
        //cmbBanka.SetSelectedIndex(cmbBanka.AddItem(result[2].Banka, result[2].IdBanka));
    }//TODO getson,duhet eleminuar kjo pjese
    pnlcallback.PerformCallback();
    gvPagaShtesa.PerformCallback();
    gvPunesim.PerformCallback('Ri');
    gvQendra.PerformCallback('Ri');
    gvBanda.PerformCallback('Ri');

}
function SucceededCallbackBanka(result) {
    txtLlogBankare.SetText(result.LlogBankare);
    txtLimitTel.SetText(result.LimitTel);
    txtLimitInternet.SetText(result.LimitInternet);
    if (result.IdBanka != 0)
        cmbBanka.SetSelectedIndex(cmbBanka.AddItem(result.Banka, result.IdBanka));
    else cmbBanka.SetValue(null);

}

/*
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/
function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
    if (mbush) {
        // gvKomponenteListPagese.ClearFilter();
        // gvKomponenteListPagese.CollapseAll();
    }
}

function mbushEmail() {
    var emri = txtEmri.GetText();
    var mbiemri = txtMbiemri.GetText();
    if (sugjerim)
        txtEmail.SetText(emri + '.' + mbiemri + '@vodafone.com');
    txtUsername.SetText(emri + '.' + mbiemri);
    if (emri != "" && mbiemri != "") {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "kontrolloUsername"),
            data: JSON.stringify({ username: emri + '.' + mbiemri, nr: 1, idndermarje: hfState.Get("idNdermarrje") }),
            pritPergjigje: true
        }).done(function (result) {
            //some instrutions
            SucceededCallbackUsername(result);
        }).fail(function (err) {
            console.log(err);

        });

    }
    txtEmri2.SetText(emri + ' ' + mbiemri);
    txtEmri3.SetText(emri + ' ' + mbiemri);
    txtEmri4.SetText(emri + ' ' + mbiemri);
    txtEmri5.SetText(emri + ' ' + mbiemri);
    txtEmri15.SetText(emri + ' ' + mbiemri);
    txtEmri6.SetText(emri + ' ' + mbiemri);
}
function SucceededCallbackUsername(result) {
    if (!result[0]) {
        var emri = txtEmri.GetText();
        var mbiemri = txtMbiemri.GetText();
        txtUsername.SetText(emri + '.' + mbiemri + result[1]);
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "kontrolloUsername"),
            data: JSON.stringify({ username: txtUsername.GetText(), nr: parseInt(result[1]) + 1, idndermarje: hfState.Get("idNdermarrje") }),
            pritPergjigje: true
        }).done(function (result) {
            //some instrutions
            SucceededCallbackUsername(result);
        }).fail(function (err) {
            console.log(err);

        });
    }
}

function mbushEmailPunonjesi(s, e) {
    var emri = s.GetText().split(' ')[0];
    var mbiemri = s.GetText().split(' ')[1];
    if (sugjerim)
        txtEmail.SetText(emri + '.' + mbiemri + '@vodafone.com');
    txtUsername.SetText(emri + '.' + mbiemri);
    txtEmri.SetText(emri);
    txtMbiemri.SetText(mbiemri);
    txtEmri2.SetText(emri + ' ' + mbiemri);
    txtEmri3.SetText(emri + ' ' + mbiemri);
    txtEmri4.SetText(emri + ' ' + mbiemri);
    txtEmri5.SetText(emri + ' ' + mbiemri);  txtEmri15.SetText(emri + ' ' + mbiemri);
    txtEmri6.SetText(emri + ' ' + mbiemri);
}
//thiret konfigurimi kur e ndryshojme ate
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    // var idGjuha = hfState.Get('idGjuha');
    try {
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idGjuha = hfState.Get('idGjuha');
        var url = Utils.getServerApiUrl("Konfigurime", "ktheKonfigDB");


        $.ajax({
            url: url,
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: idNdermarrje, kodKontrollKlienti: "", idKlienti: -1, shtim: true, merrFormatKursi: true, idGjuha: idGjuha })
        }).done(SucceededCallbackKonfig);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
    gvPunonjesit.PerformCallback(idKomp + ";" + kodKonf);
}

function ndryshoKonfiguriminKomp() {
    gvKomponenteListPagese.PerformCallback("708" + ";" + cmbKonfigurimiKomp.GetText());

}
//thirret kur hapet faqja ne fillim
function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvPunonjes").show();
    try {
        var idGjuha = hfState.Get('idGjuha');
        var idNdermarrje = hfState.Get('idNdermarrje');
        var url = Utils.getServerApiUrl("Konfigurime", "ktheKonfigDB");


        $.ajax({
            url: url,
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: idNdermarrje, kodKontrollKlienti: "", idKlienti: -1, shtim: true, merrFormatKursi: true, idGjuha: idGjuha })
        }).done(SucceededCallbackKonfig);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}
var resultkonf;
var sugjerim;
var colKontrollet, colAtrTrupi;
//        var colAlterKusht;
//        var colKushte;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
       
        colKontrollet = result.colKontroll;
        colAtrTrupi = result.colAtrTrupi;
        colKushte = result.colKushte;
        // colAlterKusht = result[4];
        //                var colGrida = result[2];
        //                colKushte = result[3];
        //                colAlterKusht = result[4];
        //            var kodniveli = result[5];
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblPunonjes', 'tblKontakti', 'tblPunesim', 'tblHistoriku', 'tblQendra',  'tblBanda','tblPagaShtesa', 'tblKomponente'];
        var arrdrejta = [hftabe.Get("1"), hftabe.Get("2"), hftabe.Get("3Mod"), hftabe.Get("4Mod"), hftabe.Get("5"), hftabe.Get("6"), hftabe.Get("7"), hftabe.Get("8")];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C", 1);
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C", 1);
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C", undefined, undefined, undefined, undefined, arrdrejta);
        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }

        sugjerim = false;
        for (k = 0; k < colKushte.length; k++) {
            var kusht = colKushte[k];
            if (kusht.Kodi == 'SE') {
                if (kusht.Alternativa == "Po") {
                    sugjerim = true;
                }
                else {
                    sugjerim = false;
                }
            }
            if (kusht.Kodi == 'LSHNRB') {
                if (kusht.Alternativa == "Po") {
                    $('#hfLejoNrLlogBank').val("True")
                }
                else {
                    $('#hfLejoNrLlogBank').val("False")
                }
            }
        }
    }



    // $("#dvPunonjes").show();//$("#dvPunonjes")[0].style.visibility = 'visible'; $("#dvPunonjes")[0].style.display = '';

}
function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}

//mbush hidden fieldet me konfigurimet e lupes
function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaDep");
    var hf2 = $("#hfLupaNendep");
    var hf3 = $("#hfLupaTipKontrate");
    var hf4 = $("#hfLupaBanka");
    var hf5 = $("#hfLupaGrupi");
    for (var i = 0; i < kontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (kontrollet[i].KodKontrolli === "cmbDepartamenti") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli === "cmbNenDepartamenti") {
            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli === "cmbTipKontrate") {
            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli === "cmbBanka") {
            hf4.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli === "cmbGrupi") {
            hf5.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}
// aktivizon fushat
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur, isLidhurPunMeVepArkeBanke) {
    var hfMod = $('#hfShtimModifikim');
    var arrTabela = ['tblPunonjes', 'tblKontakti', 'tblPunesim', 'tblHistoriku', 'tblQendra',  'tblBanda', 'tblPagaShtesa', 'tblKomponente'];
    var arrdrejta = [hftabe.Get("1"), hftabe.Get("2"), hftabe.Get("3Mod"), hftabe.Get("4Mod"), hftabe.Get("5"), hftabe.Get("6"), hftabe.Get("7"), hftabe.Get("8")];

    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_', arrdrejta, arrTabela);
    if (cmbKombesia.GetValue() > 2) txtLejePune.SetEnabled(true); else txtLejePune.SetEnabled(false);
    if (cmbIdLlogari.GetValue() != null && isLidhurPunMeVepArkeBanke == true) cmbIdLlogari.SetEnabled(false); else cmbIdLlogari.SetEnabled(true);
}
//thirret kur lodohet faqja
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("708", cmbKonfigurimi.GetText());
}
//thirret kur ndryshon konfigurimi
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    cmbfiltra.PerformCallback();
    callWebserviceKonfigurimi("708", cmbKonfigurimi.GetText());
}
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
        evt.keyCode : evt.charCode;
}
// ndryshon emrin e faqes
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Punonjes.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
//        Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvPunonjesit, "708", pastrofusha, hfTeDrejta);
    if ($('#hfStatusiPunesim').val() == "true") {
        gvPunesim.PerformCallback();
        mbushpunesim = true;
        $('#hfShtimModifikimPunesim').val('modifikim');
    }
    if (PageControl.GetActiveTab().index === 3) {
        ASPxMenu1.GetItemByName('FshiPunesim').SetEnabled(hftabe.Get("3Fshi"));
        if (!hftabe.Get("3Mod")) {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(false);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(false);
        }
        else {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(true);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(true);
        }
        ASPxMenu1.GetItemByName('ShtoPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('RuajPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('FshiPunesim').SetVisible(true);
    }
    else if (PageControl.GetActiveTab().index === 4) {
        ASPxMenu1.GetItemByName('ShtoPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('RuajPunesim').SetVisible(false);
        ASPxMenu1.GetItemByName('FshiPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('FshiPunesim').SetEnabled(hftabe.Get("4Fshi"));
        if (!hftabe.Get("4Mod")) {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(false);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(false);
        }
        else {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(true);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(true);

        }
    }
}

//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    changeName();
}

//validon te dhenat
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'), true);
}
function validoPun(s, e) {
    myFaqeCelje.validoPun(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}
//krijon tabelen e kontrolleve
function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
///zbras filtrin e grides
function BeginCallback(s, e) {

    if (e.command === 'APPLYFILTER' && btnFiltrat !== undefined)
        btnFiltrat.SetText('');
}
function EndCallbackPunonjesit(s, e) {
    hfState.Set('NdryshoiDepi', false);
}
//heq veprimin e enterit
function enter() {
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}
function clickExport(e) {
    if (gvPunesim.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}
function clickExport2(e) {
    if (gvQendra.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}function clickExport3(e) {
    if (gvBanda.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}
///hap lupen e tip kontrate
function ButtonClickedTipKontrate() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniTipinEKontrates"), 'LupaTipKontrate.aspx', widthlupa, heightlupa);
}///hap lupen e tip kontrate
function ButtonClickedProfesioni() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhProfesionin"), 'LupaProfesioneTitujPune.aspx?vjenNga=PunonjesProfesione', widthlupa, heightlupa);
}///hap lupen e tip kontrate
function ButtonClickedTitulli() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPozicionin"), 'LupaProfesioneTitujPune.aspx?vjenNga=PunonjesTitujPune', widthlupa, heightlupa);
}///hap lupen e tip kontrate
function ButtonClickedKodeProfesione() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhKodProfesionin"), 'LupaKodeProfesione.aspx?vjenNga=PunonjesProfesione', widthlupa, heightlupa);
}///hap lupen e tip kontrate
function ButtonClickedQendra1() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=prind&vjenNga=PunonjesQK1', widthlupa, heightlupa);
}///hap lupen e tip kontrate
function ButtonClickedQendra2() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=PunonjesQK2&idPrindi=' + (cmbQK1.GetValue() == null ? 0 : cmbQK1.GetValue()), widthlupa, heightlupa);
}
function ButtonClickedGrupim1() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhGrupimLokalGlobal"), 'LupaGrupimeLocaleGlobale.aspx?vjenNga=PunonjesGlobale', widthlupa, heightlupa);
}
function ButtonClickedGrupim2() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhGrupimLokalGlobal"), 'LupaGrupimeLocaleGlobale.aspx?vjenNga=PunonjesLocale&idPrindi=' + cmbGlobal.GetValue(), widthlupa, heightlupa);
}
function Objektiva_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniObjektivenEKostos"), 'LupaObjektivaKosto.aspx?vjenNga=Shto_Punonjes', widthlupa, heightlupa);
}

function PunonjesIdLlogari_Click(s, e) {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniLlogarine"), 'LupaLlogaria.aspx?vjenNga=Shto_Punonjes', widthlupa, heightlupa);
}

///hap lupen e departamentit nendepartamentit
function ButtonClickedDepartamenti(vjenNga) {


    if (vjenNga === 'NenDepartamenti') {
        var hf = $("#hfLupaNendep");
        var idprindi = cmbDepartamenti.GetValue();
        var title = hfState.Get("msgZgjidhniNenDepartamentin");
    }
    else {
        idprindi = 0; hf = $("#hfLupaDep");
        title = hfState.Get("msgZgjidhniDepartamentin");
    }
    var queryStr = hf.val();

    myButtonClickLupa.LupaUniversal_Click(title, 'LupaStrukturaAdministrative.aspx?vjenNga=' + vjenNga + '&IdPrindi=' + idprindi + '&idKonfigAmbjente=' + queryStr, widthlupa, heightlupa);
}

///kur selektohet nje departament
function departamentiChanged() {
    cmbNenDepartamenti.SetEnabled(cmbDepartamenti.GetSelectedIndex() !== -1);
    cmbNenDepartamenti.SetSelectedIndex(-1);
    cmbNenDepartamenti.ClearItems();
}
function merrPrind() {

    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "MerrPrindGrupimi"),
        data: JSON.stringify({ idgrupilocal: cmbLocal.GetValue() }),
        pritPergjigje: true
    }).done(function (result) {
        //some instrutions
        SuccedeedPrind(result);
    }).fail(function (err) {
        console.log(err);

    });
}
function merrPrindQK() {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "MerrPrindQK"),
        data: JSON.stringify({ idqk: cmbQK2.GetValue() }),
        pritPergjigje: true
    }).done(function (result) {
        //some instrutions
        SuccedeedPrindQK(result);
    }).fail(function (err) {
        console.log(err);

    });
}
function SuccedeedPrind(result) {
    cmbGlobal.SetSelectedIndex(cmbGlobal.AddItem(result.Kodi, result.Id));

}
function SuccedeedPrindQK(result) {
    cmbQK1.SetSelectedIndex(cmbQK1.AddItem(result.Kodi, result.Id));

}
///hap lupen e grupit
function ButtonClickedGrupi() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgLupaLlogariShpejteZgjidhniGrupin"), 'LupaGrupPunonjesish.aspx', widthlupa, heightlupa);
}
///hap lupen e bankes
function ButtonClickedBanka() {
    var hf = $("#hfLupaBanka");
    var queryStr = hf.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniBankenLupa"), 'LupaBanka.aspx?vjenNga=Punonjes&idKonfigAmbjente=' + queryStr, widthlupa, heightlupa);
}
///ndryshon e emrin sipas lloj pagese
function ndryshoEmerNjesi() {
    Utils.ktheKontroll('lblKomponente0').SetText('Paga/' + cmbLlojPagese.GetText());
    TextChangedVlera(Utils.ktheKontroll('txtVlera' + 0), null, 'txtVlera', 0);
    if (Utils.ktheKontroll('txtVleraParam0').GetEnabled()) {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "merrKategoriPage"),
            data: JSON.stringify({ idllojpage: cmbLlojPagese.GetValue(), idndermarje: hfState.Get("idNdermarrje") }),
            pritPergjigje: true
        }).done(function (result) {
            //some instrutions
            SucceededCallbackKategori(result);
        }).fail(function (err) {
            console.log(err);

        });
    }

}
function DtAktChange() {
    gvPagaShtesa.PerformCallback();
    if (Utils.ktheKontroll('txtVleraParam0').GetEnabled()) {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "merrSigurimeSuplementare"),
            data: JSON.stringify({ date: dteDateAkt.GetDate(), idndermarje: hfState.Get("idNdermarrje") }),
            pritPergjigje: true
        }).done(function (result) {
            //some instrutions
            SucceededCallbackSigSup(result);
        }).fail(function (err) {
            console.log(err);

        });
    }

}
function SucceededCallbackSigSup(result) {
    var editor = null; var k = 0;
    for (i = 0; i < gvPagaShtesa.cpRowCount; i++) {
        if (Utils.ktheKontroll('lblKomponente' + i).GetText() === "Sigurimi Suplementare") {
            editor = Utils.ktheKontroll('txtVleraParam' + i);
            k = i;
        }
    }
    if (editor !== null) {
        var count = editor.GetItemCount()
        for (j = 0; j < count; j++)
            editor.RemoveItem(0);

        for (i = 0; i < result.length; i++) {
            var arr = new Array();
            arr[0] = result[i].Kodi;
            arr[1] = result[i].Grupi;
            arr[2] = result[i].Perqindja.toFixed(2);
            editor.AddItem(arr, result[i].IdSigurimeSuplementare); //AddItem(teksti, vlera);
        }
        if ($('#hfShtimModifikim').val() === 'shtim') {
            Utils.ktheKontroll('txtVlera' + k).SetText(0); editor.SetText('');
            TextChangedVlera(Utils.ktheKontroll('txtVlera' + k), null, 'txtVlera', k);
        }
    }
}
function SucceededCallbackKategori(result) {
    var count = Utils.ktheKontroll('txtVleraParam0').GetItemCount()
    for (j = 0; j < count; j++)
        Utils.ktheKontroll('txtVleraParam0').RemoveItem(0);

    for (i = 0; i < result.length; i++) {
        var arr = new Array();
        arr[0] = result[i].Kodi;
        arr[1] = result[i].Pershkrimi;
        arr[2] = result[i].Paga.toFixed(2);
        Utils.ktheKontroll('txtVleraParam0').AddItem(arr, result[i].IdKategoriPage); //AddItem(teksti, vlera);
    }
    if ($('#hfShtimModifikim').val() === 'shtim') {
        Utils.ktheKontroll('txtVlera' + 0).SetText(0); Utils.ktheKontroll('txtVleraParam0').SetText('');
        TextChangedVlera(Utils.ktheKontroll('txtVlera' + 0), null, 'txtVlera', 0);
    }
}
function TextChangedVleraCombo(s, e, kolona, key) {
    Utils.ktheKontroll('txtVlera' + key).SetText(Utils.ktheKontroll('txtVleraParam' + key).GetSelectedItem().GetColumnText('Paga'));
    TextChangedVlera(s, e, kolona, key);
}
function TextChangedVleraShtesa(s, e, kolona, key) {
    Utils.ktheKontroll('txtVlera' + key).SetText(Utils.ktheKontroll('txtVleraParam' + key).GetSelectedItem().GetColumnText('Vlera'));
    TextChangedVlera(s, e, kolona, key);
}
function TextChangedVleraSig(s, e, kolona, key) {
    Utils.ktheKontroll('txtVlera' + key).SetText(Utils.ktheKontroll('txtVleraParam' + key).GetSelectedItem().GetColumnText('Perqindja'));
    TextChangedVlera(s, e, kolona, key);
}
///kur caktojme vlera komponenteve
function TextChangedVlera(s, e, kolona, key) {
    var idPunonjesi = $('#hfId').val();
    if (isNaN(s.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraDuhetJeteNumer"));
        s.SetFocus(true);
        return;
    }
    var arrvlera = new Array();
    var arrvleraParam = new Array();
    for (i = 0; i < gvPagaShtesa.cpRowCount; i++) {
        arrvlera[i] = Utils.ktheKontroll('txtVlera' + i).GetText();
        arrvleraParam[i] = Utils.ktheKontroll('txtVleraParam' + i).GetText();
        if (arrvleraParam[i] === '')
            arrvleraParam[i] = 0;
        if (arrvlera[i] === '')
            arrvlera[i] = 0;
    }
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "llogaritVlera"),
        data: JSON.stringify({
            arrvlera: arrvlera, arrvleraParam: arrvleraParam, tipi: cmbLlojPagese.GetText(), data: dteDateAkt.GetDate(), idsigurimi: cmbSkemaSigurimi.GetValue(), idmonedha: cmbMonedha.GetValue(),
            idPunonjes: idPunonjesi, idndermarje: hfState.Get("idNdermarrje")
        }),
        pritPergjigje: true
    }).done(function (result) {
        //some instrutions
        SucceededCallbackVlera(result);
    }).fail(function (err) {
        console.log(err);

    });
}
///formulat e kthyera ne vlera
function SucceededCallbackVlera(result) {
    for (var i = 0; i < result.length; i++)
        if (result[i].Formula !== "") {
            Utils.ktheKontroll('txtVlera' + i).SetText(parseFloat(eval(result[i].Formula)).toFixed(2));
        }
}

function checkDukshme() {
    var checked = cbDukshme.GetChecked();
    for (i = 0; i < gvKomponenteListPagese.cpRowCount; i++) {
        if (gvKomponenteListPagese.IsDataRow(i)) {
            Utils.ktheKontroll('cbDukshme' + i).SetChecked(checked);
        }
    }

    for (j = 0; j < hfKodeKomp.Get("count"); j++) {
        hfKomponente.Set(hfKodeKomp.Get(j), checked);
    }

}

function checkDetyrueshme() {
    var checked = cbDetyrueshme.GetChecked();
    for (i = 0; i < gvKomponenteListPagese.cpRowCount; i++) {
        if (gvKomponenteListPagese.IsDataRow(i)) {
            Utils.ktheKontroll('cbDetyrueshme' + i).SetChecked(checked);
        }
    }
    for (j = 0; j < hfKodeKomp.Get("count"); j++) {
        hfKomponente2.Set(hfKodeKomp.Get(j), checked);
    }
}

function hapRaportin() {
    window.open("RaportiShpejte.aspx?emriReal=historikPagaPunonjesish&printo=0&Sesioni=false&idPunonjes=" + $('#hfId').val() + "&scopeID=" + Utils.getUrlVar("scopeID"));
    for (j = 0; j < hfKodeKomp.Get("count"); j++) {
        hfKomponente2.Set(hfKodeKomp.Get(j), cbDukshme.GetChecked());
    }
}

function beginCallbackQendra(s, e) {
    Utils.nrWsRrugesManager.rritNrWsRruges();
}
function endCallbackQendra(s, e) {
    if (mbushqendra) {
        if (gvQendra.GetVisibleRowsOnPage() > 0) {
            gvQendra.SetFocusedRowIndex(0); mbushFushaQendra();
        } else { shtoQendra(); }
        mbushqendra = false;
    }
    Utils.nrWsRrugesManager.zbritNrWsRruges()
}
function endCallbackBanda(s, e) {
    if (mbushbanda) {
        if (gvBanda.GetVisibleRowsOnPage() > 0) {
            gvBanda.SetFocusedRowIndex(0); mbushFushaBanda();
        } else { shtoBanda(); }
        mbushbanda = false;
    }
}

function beginCallbackGridaPunesim(s, e) {
    Utils.nrWsRrugesManager.rritNrWsRruges();
    Utils.shfaqLoadingGif();
}
function endCallbackGridaPunesim(s, e) {

    if (mbushpunesim) {
        if (gvPunesim.GetVisibleRowsOnPage() > 0) {
            gvPunesim.SetFocusedRowIndex(0);
            mbushFushaPunesim();

        } else {
            mbushpunesim = false;
            $('#hfShtimModifikimPunesim').val('shtim');
        }
    }
    else
    {
        shtoPunesim(false);
        mbushpunesim = false;

    }
    myMesazh.ShtoMesazhNgaGrida(s);
    Utils.hiqLoadingGif();
    Utils.nrWsRrugesManager.zbritNrWsRruges()
}

function rowDblClickGridaPunesim(s, e) {
    PageControl.SetActiveTabIndex(3);
    mbushFushaPunesim();
}

function activeTabChanged(s, e) {
    if (e.tab.index === 3) {
        ASPxMenu1.GetItemByName('ShtoPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('RuajPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('FshiPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('FshiPunesim').SetEnabled(hftabe.Get('3Fshi'));
        if (!hftabe.Get('3Mod')) {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(false);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(false);

        } else {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(true);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(true);

        }
    } else if (e.tab.index === 4) {
        ASPxMenu1.GetItemByName('ShtoPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('RuajPunesim').SetVisible(false);
        ASPxMenu1.GetItemByName('FshiPunesim').SetVisible(true);
        ASPxMenu1.GetItemByName('FshiPunesim').SetEnabled(hftabe.Get('4Fshi'));
        if (!hftabe.Get('4Mod')) {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(false);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(false);

        }
        else {
            ASPxMenu1.GetItemByName('ShtoPunesim').SetEnabled(true);
            ASPxMenu1.GetItemByName('RuajPunesim').SetEnabled(true);

        }
    }
    else {
        ASPxMenu1.GetItemByName('ShtoPunesim').SetVisible(false);
        ASPxMenu1.GetItemByName('RuajPunesim').SetVisible(false);
        ASPxMenu1.GetItemByName('FshiPunesim').SetVisible(false);
    }
    indexModifiko = gvPunonjesit.GetFocusedRowIndex();

    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);
            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}
function beginCallbackPagaShtesa(s, e) {
    Utils.nrWsRrugesManager.rritNrWsRruges();
}
function endCallbackPagaShtesa(s, e) {
    ndryshoEmerNjesi();
    Utils.nrWsRrugesManager.zbritNrWsRruges()
}
function beginCallbackGridaKomponenteListPagese(s, e) {
    Utils.nrWsRrugesManager.rritNrWsRruges();
}
function endCallbackGridaKomponenteListPagese(s, e) {
    mbushfushaexp(s, e);
    Utils.nrWsRrugesManager.zbritNrWsRruges()
}

function beginCallbackPanel(s, e) {
    Utils.nrWsRrugesManager.rritNrWsRruges();
}
function endCallbackPanel(s, e) {
    Utils.nrWsRrugesManager.zbritNrWsRruges()
}

function buttonClickCmbNenDep(s, e) {
    ButtonClickedDepartamenti('NenDepartamenti');
}
function gvPagaShtesaSelectedIndexChanged(s, e) {
    gvPagaShtesa.PerformCallback();
    dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "merrBankaPunonjes"),
        data: JSON.stringify({ idpunonjes: $('#hfId').val(), data: dteDateAkt.GetDate() }),
    }).done(SucceededCallbackBanka)
}
function gvKomponenteListPageseDateChanged(s, e) {
    pnlcallback.PerformCallback();
    MerrSkemaSigurimi();
}
function gvKomponenteListPageseSelectedIndexChanged(s, e) {
    dteDateAk2.SetDate(new Date(cmbNdryshim2.GetText().split('/')[1] + '/' + cmbNdryshim2.GetText().split('/')[0] + '/' + cmbNdryshim2.GetText().split('/')[2]));

    pnlcallback.PerformCallback();
    MerrSkemaSigurimi();
}
function CheckedChanged_cbLarguar(s, e) {
    dteDtLargimi.SetEnabled(cbLarguar.GetChecked()); txtArsyeja.SetEnabled(cbLarguar.GetChecked());
    if (cbLarguar.GetChecked())
        dteDtLargimi.SetDate(new Date());
    else { dteDtLargimi.SetDate(null); txtArsyeja.SetText(''); }
}


function MerrSkemaSigurimi() {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "merrSkemaSigurimi"),
        data: JSON.stringify({ date: dteDateAk2.GetDate(), datendryshimi: cmbNdryshim2.GetText(), idpunonjes: $('#hfId').val(), idndermarje: hfState.Get("idNdermarrje") }),
        pritPergjigje: true
    }).done(function (result) {
        SucceededCallbackSkema(result);
    }).fail(function (err) {
        console.log(err);

    });
} 



function OnChange(s, e) {
    var v = s.GetText().split(';');
    s.SetText(v[0]);
}
