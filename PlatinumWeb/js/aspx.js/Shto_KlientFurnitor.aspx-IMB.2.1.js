;
var editorGlobal;
var widthLupaLlogaria = 700;
var heightLupaLlogaria = 600;
var widthLupaKategoriaZbritje = 600;
var heightLupaKategoriaZbritje = 600;
var widthLupaNivelCmimi = 600;
var heightLupaNivelCmimi = 600;
var widthLupaNivelZbritje = 600;
var heightLupaNivelZbritje = 600;
var widthLupaMenyreTransporti = 700;
var heightLupamenyreTrasnporti = 600;
var widthLupaKushteDergimi = 600;
var heightLupaKushteDergimi = 600;
var widthLupaAgjenteShitje = 600;
var heightLupaAgjenteShitje = 600;
var widthLupaMaturimi = 800;
var heightLupaMaturimi = 600;
var widthLupaKushtePagese = 600;
var heightLupaKushtePagese = 600;
var widthLupaAutorizime = 600, heightLupaAutorizime = 600;
var prospekt = false;
var ekzistonNumerAutomatik = false;
var grida;
var editorKPFD;
var editorKPFK;
var editorLlogaria;
var KPF;
var editorPershkrimi;
var editorLlogZbritje;

var pageState = {
    cmbAutorizimi: {},
    teDhenaImporti: null
};

var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_KF, "123", cmbKonfigurimi.GetText());
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
    //callWebservice();    te drejtat
}

$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                try { window.parent.rifresko = true; } catch (e) { }
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    try { window.parent.rifresko = true; } catch (e) { }
                break;
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
    });
});

function clickExport(e) {
    if (ASPxGridView_KF.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function txtEmertimiTextChanged(s, e) {
    var tmp = Utils.hiqEnter(s.GetText());
    s.SetText(tmp);
    txtEmertimi2.SetText(tmp);
    txtEmertimi3.SetText(tmp);
    txtEmertimi4.SetText(tmp);
    txtEmertimi5.SetText(tmp);
    txtEmertimi6.SetText(tmp);
    txtEmertimi7.SetText(tmp);
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_KF, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_KF, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
				
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_KF, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_KF, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    ruajFushaShtese();
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    $('#hfDraft').val("");
    if (e.item.name == 'Fshi') {
        e.processOnServer = false;
        Utils.konfirmoFshirje(popFshi, lblMsgbox, ASPxGridView_KF.GetSelectedRowCount(), hfState.Get("msgnumRreshtashSelektuar"), hfState.Get("msgZgjidhniNjekf"));
    }
    else if (e.item.name == "Draft") {
        $('#hfDraft').val("PO");
        myMenu.RuajClick(s, e, false, PageControl);
    }
    else if (e.item.name == 'Kerko') {
        e.processOnServer = false;
        var subjekt;
        if (Utils.getUrlVar('kf') == 'klient')
            subjekt = 1;
        else
            subjekt = 0;
        myButtonClickLupa.LupaUniversal_Click('Kerkim me atribute', 'LupaKerkoFushaShtese.aspx?kodLlojModeliFushaShtese=Klient/Furnitor&subjekti=' + subjekt, 950, 560);
    }
    else if (e.item.name == 'Arkiva') {
        ButtonClickArkiva();
        e.processOnServer = false;
    }
    else if (e.item.name == 'Importo') {
        pageState.teDhenaImporti = {
            tipi: 'XLS',
            idKategoria: 12,
            formatImporti: Utils.getUrlVar('kf') == "klient" ? "Format Standart Klient" : "Format Standart Furnitor"
        };
        myButtonClickLupa.LupaUniversal_Click("Import", "Import.aspx", 880, 600);
    }
    else {
        if (e.item.name == 'Ruaj') {
            txtNr2.ValidationGroup = "entries";
            Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());

        }
        if (e.item.name == "Shto")
            $('#hfShtimModifikim').val("shtim");

        hfShtimModifikim = $('#hfShtimModifikim');
        myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);

        if (e.item.name == "Klono" && ekzistonNumerAutomatik) {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
    }
}


function ButtonClickArkiva() {
    indexModifiko = ($('#hfShtimModifikim').val() == "modifikim" || Utils.IsNullOrEmpty($('#hfShtimModifikim').val())) ? ASPxGridView_KF.GetRowKey(ASPxGridView_KF.GetFocusedRowIndex()) : 0;
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetZgjidhnikf"));
    else {
        popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
        popupUniversal.SetSize(738, 548);
        popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=kf&idDok=' + indexModifiko
            //+ '&shtim_modifikim=' + $('#hfShtimModifikim').val()
            + "&tmpfolder=" + hfArkiva.Get("rootFolder"));
        popupUniversal.Show();
    }
}
function ndryshoNivelTVSH(){
    if (cmbNivelTVSH.Text != null || cmbNivelTVSH != "") {
        cmbNivelTVSH.Text = cmbNivelTVSH.getText();
    }
    else {
        cmbNivelTVSH.SetSelectedIndex(-1);
        
    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = ASPxGridView_KF.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetZgjidhnikf"));
    else
        ASPxGridView_KF.GetRowValues(indexModifiko, 'IdKlientFurnitor', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(value) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    initAdresa();
    $('#hfId')[0].value = value;
    var idGjuha = hfState.Get('_idGjuha');

    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheTeDhenaPerKlientFurnitor"),
        data: JSON.stringify({
            idObjekti: value,
            kodLloji: 'KlientFurnitor',
            idkomp: "123",
            kodkonfig: cmbKonfigurimi.GetText(),
            idNdermarrje: pageState.idNdermarrje,
            idGjuha: idGjuha
        })
    }).done(function (result) { SucceededCallbackKtheTeDhenaKlientFurnitor(result, value); });

    if ($('#hfShtimModifikim').val() == "modifikim")
        txtKodi.SetEnabled(false);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        percaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
    //PastroFushatShtese();//getson
}

function SucceededCallbackKtheTeDhenaKlientFurnitor(result, idObjekti) {
    MbushFushaKlientFurnitori(result.klientFurnitor);
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    if (!result) {
        myMesazh.ShtoMesazhGabimi("Nuk u morren te dhenat nese eshte i lidhur klienti/furnitori, marreveshjet, autorizimet, dhe banka e Klientit/Furnitorit!");
        Utils.hiqLoadingGif();
        return;
    }
    SucceededCallbackKtheAutorizime(result.autorizime);
    SucceededCallbackBankaID(result.banka);
    SucceededCallbackKfID(result.kfKryesor);
    zgjidhLlojeMarreveshjesh(result.marreveshjeKlienti);
    SucceededCallbackLidhur(result.lidhur, idObjekti);

}

function MbushFushaKlientFurnitori(klientFurnitor) {
    if ($('#hfShtimModifikim').val() != 'klonim' || !ekzistonNumerAutomatik)
        txtKodi.SetText(klientFurnitor.KodKlientFurnitor);
    txtEmertimi.SetText(klientFurnitor.EmertimiKF);
    cmbLloji.SetText(klientFurnitor.LlojiKF ? 'Klient' : 'Furnitor');    
    txtEmerKerkimi.SetText(klientFurnitor.EmerKerkimiKF);
    txtNipt.SetText(klientFurnitor.NiptiKF);
    cmbTipiId.SetText(klientFurnitor.TipiId);
    if (klientFurnitor.TitulliKF != 0)
        cmbTitulli.SetValue(klientFurnitor.TitulliKF);    

    txtAktiviteti.SetText(klientFurnitor.AktivitetiKF);    
    cbAktiv.SetChecked(klientFurnitor.AktivKF);    
    txtTel.SetText(klientFurnitor.TelKF);    
    txtFax.SetText(klientFurnitor.FaxKF);    
    txtCel.SetText(klientFurnitor.CelKF);    

    if (klientFurnitor.QytetiKF != 0)
        txtQyteti.SetValue(klientFurnitor.QytetiKF);
    else
        txtQyteti.SetValue(null); 
        txtQyteti.SetText('');    

    txtEmail.SetText(klientFurnitor.EmailKF);    
    txtShteti.SetText(klientFurnitor.ShtetiKF);    
    txtWebpage.SetText(klientFurnitor.WebPageKF);    
    txtNr2.SetText(klientFurnitor.NrLlogKlientFurnitor);    
    txtLlogKons.SetText(klientFurnitor.NrLlogZbritje);  
    
    if (klientFurnitor.KodKatZbritje && klientFurnitor.IdKatZbritje != 0) {
        btneKategoriZbritje.SetText(klientFurnitor.KodKatZbritje);
        txtKategoriPerqindja.SetText(klientFurnitor.PerqindjeKatZbritje);
    }
    else {
        btneKategoriZbritje.SetValue(null);
        txtKategoriPerqindja.SetText('');
    }

    if (klientFurnitor.IdNivelCmimi != 0)
        btneNivelCmimi.SetText(klientFurnitor.PershkrimNivelCmimi);    
    else
        btneNivelCmimi.SetValue(null);    

    txtZbritjeTotale.SetText(klientFurnitor.ZbritjeTotal); 
    
    if (klientFurnitor.PershkrimNivelZbritje)
        btneNivelZbritje.SetText(klientFurnitor.PershkrimNivelZbritje);
    else
        btneNivelZbritje.SetValue(null);    

    cbOfertaAutomatike.SetChecked(klientFurnitor.OfertaAutomatike);    
    txtVleraMin.SetText(klientFurnitor.VleraLimitPorositur);
    
    if (klientFurnitor.Prioriteti != 0)
        cmbPrioriteti.SetText(klientFurnitor.Prioriteti);    

    cmbMenyraTransporti.SetText(klientFurnitor.MenyraTransportit);    
    cmbKushteDergimi.SetText(klientFurnitor.KushteDergimi);    
    txtLimitiParalajmerues.SetText(klientFurnitor.LimitParalajmerues);    
    txtLimitiBllokues.SetText(klientFurnitor.LimitBllokues);  
    
    if (klientFurnitor.MaturimiKF != 0)
        btneMaturimi.SetValue(klientFurnitor.MaturimiKF);
    else
        btneMaturimi.SetValue(null);
    
    if (klientFurnitor.KodKushtePagese)
        cmbKushtetPageses.SetText(klientFurnitor.KodKushtePagese);
    else
        cmbKushtetPageses.SetValue(null);    

    if (klientFurnitor.IdMetoda != -1)
        cmbMetoda.SetValue(klientFurnitor.IdMetoda);
    else
        cmbMetoda.SetValue(null);    

    if (klientFurnitor.KodPerfaqesuesShitje) 
        cmbAgjentShitjesh.SetText(klientFurnitor.KodPerfaqesuesShitje);
    else
        cmbAgjentShitjesh.SetValue(null);    

    lblPershkrimMonedha.SetText(klientFurnitor.Monedha);
    lblPershkrimMonedhaZ.SetText(klientFurnitor.MonedhaZbritje);
    txtLicenca.SetText(klientFurnitor.Licenca);
    txtSwift.SetText(klientFurnitor.Swift);

    var idbanka = klientFurnitor.EmriBanka;
    if (idbanka == null || idbanka == 0) {
        txtEmriBanka.SetText('');
        lblPershkrimiEmriBanka.SetText('');
    }

    txtAdresaBanka.SetText(klientFurnitor.AdresaBanka);
    txtLlogariBankare.SetText(klientFurnitor.LlogariBankareKF);

    if (klientFurnitor.Grupim1KF)
        btneGrupimi1.SetText(klientFurnitor.Grupim1KF);
    else
        btneGrupimi1.SetValue(null);

    if (klientFurnitor.Grupim2KF)
        btneGrupimi2.SetText(klientFurnitor.Grupim2KF);
    else
        btneGrupimi2.SetValue(null);

    if (klientFurnitor.Grupim3KF)
        btneGrupimi3.SetText(klientFurnitor.Grupim3KF);
    else
        btneGrupimi3.SetValue(null);

    txtNrTvsh.SetText(klientFurnitor.NrTVSH);
    txtIban.SetText(klientFurnitor.IBANKF);
    cmbObjektiva.SetSelectedIndex(cmbObjektiva.AddItem(klientFurnitor.Objektiva, klientFurnitor.IdObjektivaKosto));
    lblKrijuesi.SetText(klientFurnitor.Krijuesi);
    cmbBij.SetSelectedIndex(cmbBij.AddItem(klientFurnitor.NdermarjeBij, klientFurnitor.IdNdermarjeBij));

    if (klientFurnitor.LlojPorosie != 0)
        cmbLlojPorosie.SetValue(klientFurnitor.LlojPorosie);
    else
        cmbLlojPorosie.SetText('');    

    cbKupon.SetChecked(klientFurnitor.Kupon);    
    txtLlogDytesor.SetText(klientFurnitor.NrLlogDytesor); 
    
    if (klientFurnitor.Koordinata) {
        var koordinata = klientFurnitor.Koordinata.substring(klientFurnitor.Koordinata.indexOf('(') + 1, klientFurnitor.Koordinata.length - 1);
        koordinata = koordinata.split(' ');
        btneCaktoNeHarte.SetText(koordinata[1] + ', ' + koordinata[0]);
    }
    else
        btneCaktoNeHarte.SetText("");

    var tmpGeoms = new Array(1);
    tmpGeoms[0] = klientFurnitor.Koordinata;    

    if (klientFurnitor.KodPerfaqesuesShitje2)
        btnAgjenti2.SetText(klientFurnitor.KodPerfaqesuesShitje2);
    else
        btnAgjenti2.SetValue(null);    

    cbSpecifik.SetChecked(klientFurnitor.KlientSpecifik);
     cbFermer.SetChecked(klientFurnitor.Fermer);    
    cbAutongarkese.SetChecked(klientFurnitor.AutoNgarkese);    
    cbShitjePaTvsh.SetChecked(klientFurnitor.ShitjePaTvsh);

    if (klientFurnitor.PerqindjeAgjenti != 0)
        txtPerqindjeAgjent.SetText(klientFurnitor.PerqindjeAgjenti);    
    if (klientFurnitor.PerqindjeAgjenti2 != 0)
        txtPerqindjeAgjent2.SetText(klientFurnitor.PerqindjeAgjenti2);
    if (klientFurnitor.PerqindjeAgjenti3 != 0)
        txtPerqindjeAgjent3.SetText(klientFurnitor.PerqindjeAgjenti3);  
    
    prospekt = klientFurnitor.Prospekt;    
    txtEmailPerPajisje.SetText(klientFurnitor.EmailPerPajisje); 
    
    var idkf = klientFurnitor.IdKlientFurnitorKryesor;
    if (idkf == 0)
        btneKlientiKryesor.SetValue(null);    

    if (new Date(klientFurnitor.DteDatelindjaKF) <= new Date('1900-01-01T00:00:00'))
        dteDatelindjaKF.SetDate(null);
    else
        dteDatelindjaKF.SetDate(Utils.zeroOren(klientFurnitor.DteDatelindjaKF));  
    
    if (klientFurnitor.KodPerfaqesuesShitje3) 
        btnAgjenti3.SetValue(klientFurnitor.KodPerfaqesuesShitje3);    
    else
        btnAgjenti3.SetValue(null);    

    txtShenime.SetText(klientFurnitor.Shenime);    

    if (klientFurnitor.IDTVSH == 0) {
        cmbNivelTvsh.SetSelectedIndex(-1);
        cmbNivelTvsh.SetText('');   
    } 
    else
        cmbNivelTvsh.SetValue(klientFurnitor.IdTvsh);    

    txtEmertimFature.SetText(klientFurnitor.EmertimFature);    
    cbLlogaritKomision.SetChecked(klientFurnitor.LlogaritKomision);
    txtKodIntegrimi.SetText(klientFurnitor.KodIntegrimi);
    txtKodiISKSH.SetText(klientFurnitor.KodiISKSH);
    cbMeDogane.SetChecked(klientFurnitor.MeDogane);
    hfState.Set("geom", JSON.stringify(tmpGeoms));
    RefreshFushatShtese($('#hfId').val(), 'mod');
    callWebserviceAdresatKF(klientFurnitor.KodKlientFurnitor);
}

function zgjidhLlojeMarreveshjesh(result) {
    if (result.d)
        result = result.d;
    cblistLlojMarreveshje.UnselectAll();
    var arrayElementesh = [];
    for (var i = 0; i < result.length; i++) {
        arrayElementesh.push(result[i].IdLlojMarreveshje);
    }
    cblistLlojMarreveshje.SelectValues(arrayElementesh);
}

function SucceededCallbackKtheAutorizime(result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
}

function callWebserviceAdresatKF(name) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheAdresatKlientFurnitor"),
        data: JSON.stringify({ prefixText: name, idNdermarrje: pageState.idNdermarrje })
    }).done(SucceededCallbackAdresatKF);

}

/*
Function: SucceededCallbackAdresatKF
	
Vendosur adresat e klientit/furnitorit te zgjedhur
*/
function SucceededCallbackAdresatKF(result) {
  
    cmbLlojAdrese.SetSelectedIndex(0);
    
    for (var i = 0; i < result.length; i++) {
        var item = result[i];
        for (var j = 0; j < arradresa.length; j++) {
            if (arradresa[j].IdTipAdrese !== item.IdTipAdrese)
                continue;
            arradresa[j].Adresa = item.Adresa;
            arradresa[j].KodiPostar = item.KodiPostar;
        }
    }

    var adresaLlojiPare = $.grep(arradresa, function (e) { return e.IdTipAdrese == Utils.llojeAdresash.Biznesi; });
    txtAdresa.SetText(adresaLlojiPare[0].Adresa);
    txtKodiPostar.SetText(adresaLlojiPare[0].KodiPostar);
    ruajAdresaNeHiddenField(arradresa);
}

function ndryshoVlereAdreseKodiPostar(s, e) {
    for (var i = 0; i < arradresa.length; i++) {
        if (arradresa[i].IdTipAdrese == cmbLlojAdrese.GetValue()) {
            arradresa[i].Adresa = txtAdresa.GetText();
            arradresa[i].KodiPostar = txtKodiPostar.GetText();
        }
    }
    ruajAdresaNeHiddenField(arradresa);
}

function ruajAdresaNeHiddenField(arrAdr) {
    var adresatJoBosh = new Array();
    for (var i = 0; i < arradresa.length; i++) {
        if (arradresa[i].Adresa == "" && arradresa[i].KodiPostar == "")
            continue;
        else
            adresatJoBosh.push(arradresa[i]);
    }
    var hf2 = $("#hfAdresa");
    hf2.val(JSON.stringify(adresatJoBosh));
}


//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
   
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

function pastrofusha() {
    hfState.Set("geom", '');
    $("#hfAdresa").val("");
    lblKrijuesi.SetText($("input[id$='hfPerdoruesAktual']").val());
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    txtNipt.SetText('');
    cmbTitulli.SetSelectedIndex(0);
    txtAktiviteti.SetText('');
    cbAktiv.SetChecked(true); cbSpecifik.SetChecked(false);
    cbFermer.SetChecked(false);
    cbAutongarkese.SetChecked(false);
    cbShitjePaTvsh.SetChecked(false);
    cbKupon.SetChecked(false);
    txtTel.SetText('');
    txtFax.SetText('');
    txtLlogDytesor.SetText('');
    txtCel.SetText('');
    txtQyteti.SetValue('');
    txtEmail.SetText('');
    txtShteti.SetText('');
    txtWebpage.SetText('');
    txtEmerKerkimi.SetText('');
    txtNr2.SetText('');
    lblPershkrimMonedha.SetText('');
    txtLlogKons.SetText('');
    lblPershkrimMonedhaZ.SetText('');
    btneKategoriZbritje.SetValue(null);
    txtKategoriPerqindja.SetText('');
    btneNivelCmimi.SetValue(null);
    txtZbritjeTotale.SetText('');
    btneNivelZbritje.SetValue(null);
    cbOfertaAutomatike.SetChecked('');
    txtVleraMin.SetText('');
    cmbPrioriteti.SetText('');
    cmbMenyraTransporti.SetValue(null);
    cmbKushteDergimi.SetValue(null);
    txtLimitiParalajmerues.SetText('');
    txtLimitiBllokues.SetText('');
    btneMaturimi.SetValue(null);
    cmbKushtetPageses.SetValue(null);
    cmbMetoda.SetSelectedIndex(0);
    cmbMetoda.SetText('');
    cmbLlojAdrese.SetSelectedIndex(0);
    txtAdresa.SetText('');
    txtKodiPostar.SetText('');
    idllojadrese = 1;
    cmbAgjentShitjesh.SetValue(null);
    btnAgjenti2.SetValue(null);
    txtLicenca.SetText('');
    txtSwift.SetText('');
    txtEmriBanka.SetText('');
    txtAdresaBanka.SetText('');
    txtLlogariBankare.SetText('');
    btneGrupimi1.SetValue(null);
    btneGrupimi2.SetValue(null);
    btneGrupimi3.SetValue(null);
    txtNrTvsh.SetText('');
    txtIban.SetText('');
    cmbObjektiva.SetSelectedIndex(-1);
    cmbBij.SetSelectedIndex(-1);
    cmbLlojPorosie.SetSelectedIndex(-1);
    cmbObjektiva.SetText('');
    cmbBij.SetText('');
    cmbLlojPorosie.SetText('');
    btneCaktoNeHarte.SetText('');
    btneKlientiKryesor.SetValue(null);
    txtShenime.SetText('');
    dteDatelindjaKF.SetValue(null);
   // btneFurnitoriKryesor.SetText('');
    btnAgjenti3.SetValue(null);
    cmbAutorizimi.SetValue(null);
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    hfArkiva.Clear();
    $("#hfArkivaDokId").val("");
    txtPerqindjeAgjent2.SetText('');
    txtPerqindjeAgjent3.SetText('');
    txtPerqindjeAgjent.SetText('');
    txtEmailPerPajisje.SetText('');
    cmbNivelTvsh.SetSelectedIndex(-1);
    cmbNivelTvsh.SetText('');
    txtEmertimFature.SetText('');
    txtKodIntegrimi.SetText('');
    cbLlogaritKomision.SetChecked(false);
    cblistLlojMarreveshje.UnselectAll();
    txtKodiISKSH.SetText('');
    cbMeDogane.SetChecked(false);
    prospekt = false;
    arradresa = new Array();
    var llojeAdr = Object.keys(Utils.llojeAdresash);
    for (var j = 0; j < llojeAdr.length; j++) {
        var item = llojeAdr[j];
        arradresa.push({ IdTipAdrese: Utils.llojeAdresash[item], KodiPostar: "", Adresa: "" });
    }

    percaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));

    PastroFushatShtese();//getson
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
    var idGjuha = hfState.Get('_idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('_idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    ASPxGridView_KF.PerformCallback(idKomp + ";" + kodKonf + ";changeConfig");
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvKlientFurnitor").show();
    var idGjuha = hfState.Get('_idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('_idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function Objektiva_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniObjektivenEKostos"), 'LupaObjektivaKosto.aspx?vjenNga=Shto_Punonjes', widthLupaNivelZbritje, heightLupaNivelZbritje);
}

function Ndermarje_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniNdermarjeBij"), 'LupaNdermarjeBij.aspx?vjenNga=Shto_KF', widthLupaNivelZbritje, heightLupaNivelZbritje);
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
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblInformacion', 'tblKontakti', 'tblKontabiliteti', 'tblRegjistrime', 'tblBanka', 'tblBuxheti', 'tblFushatShtese'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C");
        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }

        if (Utils.getUrlVar('kf') == 'klient'){
            cmbLloji.SetValue('Klient');
            if (cmbBij.GetVisible() && !(hfState.Get("_meme") == true)) {
                cmbBij.SetVisible(false);
                lblBij.SetVisible(false);
                cmbLlojPorosie.SetVisible(false);
                lblLlojPorosie.SetVisible(false);
            }
        }
        else {
            cmbLloji.SetValue('Furnitor');
            cmbBij.SetVisible(false);
            lblBij.SetVisible(false);
            cmbLlojPorosie.SetVisible(false);
            lblLlojPorosie.SetVisible(false);
        }
        bejVisibleFalseTabet();
        
        ekzistonNumerAutomatik = ekzistonNumriAutomatikPerKontroll(colKontrollet, colAtrTrupi);
    }
}
function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaLlogaria");
    var hf2 = $("#hfLupaLlogKons");
    var hf3 = $("#hfLupaLlogDytesor");
    var hf4 = $("#hfLupaNivelZbritje");
    var hf5 = $("#hfLupaKushtePagese");
    var hf6 = $("#hfLupaKushteDergimi");
    var hf7 = $("#hfLupaAutorizimi");
    var hf8 = $("#hfLupaAfateMaturimi");
    var hf9 = $("#hfLupaKategoriZbritje");
    var hf10 = $("#hfLupaNivelCmimi");
    var hf11 = $("#hfLupaMenyraTransp");
    var hf12 = $("#hfLupaAgjShitje");
    var hf13 = $("#hfLupaKfkryesor");
    for (var i = 0; i < kontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it

        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
            hf7.val(colAtrTrupi[i].IdKonfigAmbjenteLupa); // kontrollet[i].split(',')[11].toString();
            continue;
        }
        if (kontrollet[i].KodKontrolli == "txtNr" || kontrollet[i].KodKontrolli == "txtNr2" || kontrollet[i].KodKontrolli == "txtNr3" || kontrollet[i].KodKontrolli == "txtNr4" || kontrollet[i].KodKontrolli == "txtNr5" || kontrollet[i].KodKontrolli == "txtNr6") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "txtLlogDytesor") {
            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbKushtetPageses") {
            hf5.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneMaturimi") {
            hf8.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneKategoriZbritje") {
            hf9.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbMenyraTransporti") {
            hf11.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneNivelCmimi") {
            hf10.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneNivelZbritje") {
            hf4.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "txtLlogKons") {
            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbAgjentShitjesh") {
            hf12.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnAgjenti2") {
            hf12.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneKlientiKryesor") {
            hf13.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btnAgjenti3") {
            hf12.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}

function bejVisibleFalseTabet() {
    PageControl.GetTab(2).SetVisible(false);
    PageControl.GetTab(6).SetVisible(false);

    //PageControl.GetTab(7).SetVisible(true);//fushatshtese
}
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("123", cmbKonfigurimi.GetText());
    ndryshoKonfigurimFushaShtese(cmbKonfigurimi.GetValue());
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("123", cmbKonfigurimi.GetText());
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];

    myFaqeCelje.changeName('Shto_KlientFurnitor.aspx?kf=' + Utils.getUrlVar('kf'), 0, hf);

    percaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}
/*
Function: EndRequestHandler
	
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar

    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_KF, "123", pastrofusha, hfTeDrejta, undefined, undefined, false);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function Autorizime_Click() {
    KPF = 0;
    var qstrAutorizimet = '?autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniAutorizimet"), 'LupaAutorizim.aspx' + qstrAutorizimet, widthLupaNivelZbritje, heightLupaNivelZbritje);
}
function AfateMaturimi_Click() {
    var hf = $("#hfLupaAfateMaturimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.AfateMaturimi_Click(hfState.Get("labelBlerjeShitjeAfateMaturimi"), queryStr, widthLupaMaturimi, heightLupaMaturimi, cmbLloji.GetText());
}
function KategoriZbritje_Click() {
    var hf = $("#hfLupaKategoriZbritje")[0];
    var queryStr = hf.value;
    myButtonClickLupa.KategoriZbritje_Click(hfState.Get("msgZgjidhKatZbritjeLupa"), queryStr, widthLupaKategoriaZbritje, heightLupaKategoriaZbritje);
}
function NivelCmimi_Click() {
    var hf = $("#hfLupaNivelCmimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.NivelCmimi_Click(hfState.Get("msgNivelCmimiPrindLupa"), queryStr, widthLupaNivelCmimi, heightLupaNivelCmimi, cmbLloji.GetText());
}
function NivelZbritje_Click() {
    var hf = $("#hfLupaNivelZbritje")[0];
    var queryStr = hf.value;
    myButtonClickLupa.NivelZbritje_Click(hfState.Get("msgZgjidhNivelZbritjeLupa"), queryStr, widthLupaNivelZbritje, heightLupaNivelZbritje);
}
function KushtetPageses_Click() {
    var hf = $("#hfLupaKushtePagese")[0];
    var queryStr = hf.value;
    myButtonClickLupa.KushtetPageses_Click(hfState.Get("msgZgjidhKushtePageseLupa"), queryStr, widthLupaKushtePagese, heightLupaKushtePagese);
}
function KushteDergimi_Click() {
    var hf = $("#hfLupaKushteDergimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.KushteDergimi_Click(hfState.Get("msgZgjidhKushteDergimiLupa"), queryStr, widthLupaKushteDergimi, heightLupaKushteDergimi);
}
function MenyraTransporti_Click() {
    var hf = $("#hfLupaMenyraTransp")[0];
    var queryStr = hf.value;
    myButtonClickLupa.MenyraTransporti_Click(hfState.Get("msgZgjidhMenyreTransportiLupa"), queryStr, widthLupaMenyreTransporti, heightLupamenyreTrasnporti);
}
function AgjentShitjesh_Click(agjenti) {
    var hfAgj = $("#hfLupaAgjShitje")[0];
    var queryStr = hfAgj.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhAgjentShitjeLupa"));
    popupUniversal.SetContentUrl('LupaAgjenteShitje.aspx?idKonfigAmbjente=' + queryStr + '&agjenti=' + agjenti);
    popupUniversal.SetSize(widthLupaMenyreTransporti, heightLupamenyreTrasnporti);
    popupUniversal.Show();
}

function kfKryesor_Click(idkf) {
    var hfAgj = $("#hfLupaKfkryesor")[0];
    var queryStr = hfAgj.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKf"));
    if (cmbLloji.GetText() == 'Klient') 
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + queryStr + '&agjenti=' + idkf + '&veprimi=' + 1);
    
    else popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + queryStr + '&agjenti=' + idkf + '&veprimi=' + 2);
    popupUniversal.SetSize(widthLupaMenyreTransporti, heightLupamenyreTrasnporti);
    popupUniversal.Show();
}

function kontrollagjent(agjenti) {
    var vleraagjentit;
    if (agjenti == '1') {
        vleraagjentit = cmbAgjentShitjesh.GetValue();

        if ((vleraagjentit == btnAgjenti2.GetValue()) && vleraagjentit != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAgjentZgjedhurNjeHere"));
            cmbAgjentShitjesh.SetValue(null);
            txtPerqindjeAgjent.SetValue("");
            return;
        }
    }
    else if (agjenti == '2') {
        vleraagjentit = btnAgjenti2.GetValue();
        if ((vleraagjentit == cmbAgjentShitjesh.GetValue()) && vleraagjentit != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAgjentZgjedhurNjeHere"));
            btnAgjenti2.SetValue(null);
            txtPerqindjeAgjent2.SetValue("");
            return;
        }
    }
    else if (agjenti == '3') {
        vleraagjentit = btnAgjenti3.GetValue();
        if ((vleraagjentit == cmbAgjentShitjesh.GetValue()) && vleraagjentit != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAgjentZgjedhurNjeHere"));
            btnAgjenti3.SetValue(null);
            txtPerqindjeAgjent3.SetValue("");
            return;
        }
    }
}

function ZbrazPerqindjeAgjenti(txtPerqindjeAgjenti) {
    Utils.ktheKontroll(txtPerqindjeAgjenti).SetText('');
    return;
}

function changedPerqindjeAgjent(agjenti) {//po
    setTimeout(function () { kontrollPerqindjeAgjent(agjenti); }, 0);
}

function kontrollPerqindjeAgjent(agjenti) {
    if (isNaN(txtPerqindjeAgjent.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerqindjaDuhetJeteNumer"));
        if (agjenti == '1')
            txtPerqindjeAgjent.SetText('0.00');
        else if (agjenti == '2')
            txtPerqindjeAgjent2.SetText('0.00');
        else if (agjenti == '3')
            txtPerqindjeAgjent3.SetText('0.00');

    }
    if (parseFloat(txtPerqindjeAgjent.GetText()) < 0 || parseFloat(txtPerqindjeAgjent.GetText()) > 100) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerqindjaVleraNdermjet"));
        if (agjenti == '1')
            txtPerqindjeAgjent.SetText('0.00');
        else if (agjenti == '2')
            txtPerqindjeAgjent2.SetText('0.00');
        else if (agjenti == '3')
            txtPerqindjeAgjent3.SetText('0.00');

    }
}

var arr = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;
var arradresa;
//var arrkodipostar = new Array();
var countadresa = 0;
var editorValues = new Object();
var idllojadrese = 1;
//var arrVlerat = new Array();
var countvlerat = 0;
var autorizime;
//pastron fushat
function Init() {
    pageState.idNdermarrje = hfState.Get('_idNdermarrje');
    pageState.idPerdoruesi = hfState.Get('_idPerdoruesi');
    changeName();
    initAdresa();
    var aut = hfState.Get("colAutorizime");
    if (aut) {
        autorizime = JSON.parse(aut);
        hfState.Remove("colAutorizime");
    }
    cmbAutorizimi = new MultiSelect({
        container: "tblInformacion",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: autorizime,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    });
}

function initAdresa() {
    arradresa = new Array();
    var llojeAdr = Object.keys(Utils.llojeAdresash);
    for (var j = 0; j < llojeAdr.length; j++) {
        var item = llojeAdr[j];
        arradresa.push({ IdTipAdrese: Utils.llojeAdresash[item], KodiPostar: "", Adresa: "" });
    }
}

function adresat() {
    //ruan te dhenat e adreses ne varesi te llojit
    var llojadrese = $.grep(arradresa, function(i){
        return i.IdTipAdrese == cmbLlojAdrese.GetValue();
    });
    txtAdresa.SetText(llojadrese[0].Adresa);
    txtKodiPostar.SetText(llojadrese[0].KodiPostar);
}

function LostFocusNrLlogarieTxtNr2(editor) {
    txtNr.SetText(txtNr2.GetText());
    txtNr4.SetText(txtNr2.GetText());
    txtNr6.SetText(txtNr2.GetText());
    txtNr7.SetText(txtNr.GetText());
    txtNr2.EndUpdate();
}

function nrLlogariChange() {
    var s = txtNr2.GetText().split(';');
    if (s.length == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheMonedheLlogSipasKodit"),
            data: JSON.stringify({ kodi: s[0] })
        }).done(function (result) {

            var mondedhaKodi = result;
        if (mondedhaKodi != null) {
            lblPershkrimMonedha.SetText(mondedhaKodi);
        }
        else {
            lblPershkrimMonedha.SetText("Nope");
        }
        });
    }
    else {
        lblPershkrimMonedha.SetText(s[2]);
    }
    txtNr2.SetText(s[0]);
}


function validateLLogariKod() {
    var s = txtNr2.GetText().split(';');
    if (s.length == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheMonedheLlogSipasKodit"),
            data: JSON.stringify({ kodi: s[0] })
        }).done(function (result) {
            var monedhaKodi = result;
        if (monedhaKodi == null) {
            txtNr2.isValid = false;
        }
        else {
            txtNr2.isValid = true;
        }
        });
    }
}

//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    arr = new Array();
    counter = 0;
    arr2 = new Array();
    counter2 = 0;
    //arrVlerat = new Array();
    countvlerat = 0;
    arrEmer = new Array();
    arrMbiemer = new Array();
    arrTel = new Array();
    arrFax = new Array();
    arrCel = new Array();
    arrEmail = new Array();
    cou1 = 0;
    cou2 = 0;
    cou3 = 0;
    cou4 = 0;
    cou5 = 0;
    cou6 = 0;
    arradresa = new Array();
    var llojeAdr = Object.keys(Utils.llojeAdresash);
    for (var j = 0; j < llojeAdr.length; j++) {
        var item = llojeAdr[j];
        arradresa.push({ IdTipAdrese: Utils.llojeAdresash[item], KodiPostar: "", Adresa: "" });
    }
}

//metodat per te hapur popupet e KPF dhe te llogarise konsoliduese dhe llogarise korresponduese       
function LlogDytesore_Click() {
    var hf = $("#hfLupaLlogDytesor")[0];
    var queryStr = hf.value;
    txtLlog.SetText('ld');
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, cmbLloji.GetText(), widthLupaLlogaria, heightLupaLlogaria);
}
function ButtonClickedLlogaria(s) {
    var queryStr = $("#hfLupaLlogaria")[0].value;
    editorLlogaria = s;
    editorPershkrimi = lblPershkrimMonedha;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get('headerPopUpText'), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function ButtonClickedParaLlogaria(s) {
    var hf = $("#hfLupaLlogDytesor")[0];
    editorGlobal = s;
    editorLlogaria = s;
    editorPershkrimi = "";
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get('headerPopUpText'), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function ButtonClickedLlogariZbritje(s) {
    var hf = $("#hfLupaLlogKons")[0];
    var queryStr = hf.value; editorGlobal = s;
    editorLlogaria = s;
    editorPershkrimi = lblPershkrimMonedhaZ;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get('headerPopUpText'), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

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
var arrEmer = new Array();
var arrMbiemer = new Array();
var arrTel = new Array();
var arrFax = new Array();
var arrCel = new Array();
var arrEmail = new Array();
var cou1 = 0;
var cou2 = 0;
var cou3 = 0;
var cou4 = 0;
var cou5 = 0;
var cou6 = 0;
var indeksi = -1;
var editorEmer;
var editorMbiemer;
var editorTel;
var editorFax;
var editorCel;
var editorEmail;
var indexCounter;
var identifikuesPerPopup = "Shto_KlientFurnitor";

function merrTeDhena() {//merren te dhenat qe ka grida
    var hidField1 = $("#hfEmer")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidfield2 = $("#hfMbiemer")[0];
    var hidField3 = $("#hfTel")[0];
    var hidField4 = $("#hfFax")[0];
    var hidField5 = $("#hfCel")[0];
    var hidField6 = $("#hfEmail")[0];
    for (i = 0; i < gvKontakti.cpNoRows; i++) {
        editorEmer = Utils.ktheKontroll('txtEmer' + i);
        editorMbiemer = Utils.ktheKontroll('txtMbiemer' + i);
        editorTel = Utils.ktheKontroll('txtTel' + i);
        editorFax = Utils.ktheKontroll('txtFax' + i);
        editorCel = Utils.ktheKontroll('txtCel' + i);
        editorEmail = Utils.ktheKontroll('txtEmail' + i);

        arrEmer[cou1] = i.toString() + ":" + editorEmer.GetValue();
        arrMbiemer[cou2] = i.toString() + ":" + editorMbiemer.GetText();
        arrTel[cou3] = i.toString() + ":" + editorTel.GetText();
        arrFax[cou4] = i.toString() + ":" + editorFax.GetText();
        arrCel[cou5] = i.toString() + ":" + editorCel.GetText();
        arrEmail[cou6] = i.toString() + ":" + editorEmail.GetText();
        cou2 = cou2 + 1;
        cou1 = cou1 + 1;
        cou5 = cou5 + 1;
        cou6 = cou6 + 1;
        cou4 = cou4 + 1;
        cou3 = cou3 + 1;
    }
    hidField1.value = arrEmer;
    hidfield2.value = arrMbiemer;
    hidField3.value = arrTel;
    hidField4.value = arrFax;
    hidField5.value = arrCel;
    hidField6.value = arrEmail;
}

//kontrollon nese jemi ne rreshtin e fundit
function TextChangedEmer(editor, key) {
    indeksi = indexCounter;
    if (key == gvKontakti.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvKontakti.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedMbiemer(editor, key) {
    indeksi = indexCounter;
    if (key == gvKontakti.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvKontakti.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedTel(editor, key) {
    indeksi = indexCounter;
    if (key == gvKontakti.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvKontakti.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedCel(editor, key) {
    indeksi = indexCounter;
    if (key == gvKontakti.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvKontakti.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedFax(editor, key) {
    indeksi = indexCounter;
    if (key == gvKontakti.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvKontakti.PerformCallback();

    }
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedEmail(editor, key) {
    indeksi = indexCounter;
    if (key == gvKontakti.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvKontakti.PerformCallback();
    }
}
//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    gvKontakti.PerformCallback(key);
}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function niveliChange() {
    var s = btneNivelCmimi.GetText().split(',');
    btneNivelCmimi.SetText(s[1]);
    if (cmbLloji.GetText() == 'Klient') {
        if (s[2] == 'Cmim Blerje') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNivelCmimiNukPerdoretKliente"));
            btneNivelCmimi.SetText('');
        }
    }
    else if (cmbLloji.GetText() == 'Furnitor') {
        if (s[2] == 'Cmim Shitje') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNivelCmimiNukPerdoretFurnitore"));
            btneNivelCmimi.SetText('');
        }
    }
}
function nrLlogariChange() {
    var s = txtNr2.GetText().split(';');
    txtNr2.SetText(s[0]);
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
        data: JSON.stringify({ kodi: txtNr2.GetText(), idNderrmarje: hfState.Get("_idNdermarrje") })
    }).done(SucceededCallbackLlog);
    lblPershkrimMonedha.SetText(s[2]);
}
function SucceededCallbackLlog(llogaria) {
    if (llogaria != null && llogaria.NrLlogari != -1) {
        lblPershkrimMonedha.SetText(llogaria.PershkrimiMonedha);
    }
}
function nrLlogariParaChange() {
    var s = txtLlogDytesor.GetText().split(';');
    txtLlogDytesor.SetText(s[0]);
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
        data: JSON.stringify({ kodi: txtLlogDytesor.GetText(), idNderrmarje: hfState.Get("_idNdermarrje") })
    }).done(SucceededCallbackLlogPara);

}
function SucceededCallbackLlogPara(llogaria) {
    if (llogaria != null && llogaria.NrLlogari != -1) {

        if (llogaria.PershkrimiMonedha != lblPershkrimMonedha.GetText()) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgMonLlogParapNjejte"));
            txtLlogDytesor.SetText('');
        }
    }
}
/*ky funksion perdoret ne rastin kur zgjidhet nje element nga lista, ne menyre qe te vendoset tek textboxi perkates vetem 
numri i llogarise, dhe jo pershkrimi apo ndonje e dhene tjeter e llogarise*/
function nrLlogariZbritjeChange() {
    var s = txtLlogKons.GetText().split(';');
    txtLlogKons.SetText(s[0]);
    $.ajax({        
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
        data: JSON.stringify({ kodi: txtLlogKons.GetText(), idNderrmarje: hfState.Get("_idNdermarrje") })
    }).done(SucceededCallbackLlogZb);
    lblPershkrimMonedhaZ.SetText(s[2]);
}
function SucceededCallbackLlogZb(llogaria) {
    if (llogaria != null && llogaria.NrLlogari != -1) {
        lblPershkrimMonedhaZ.SetText(llogaria.PershkrimiMonedha);
    }
}

/*
Function: ButtonClickBanka
	
Hap lupen me listen e bankave.
*/
function ButtonClickBanka() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhArkenBankenLupa"), 'LupaBanka.aspx?monedha=' + lblPershkrimMonedha.GetText(), 650, 600);
}

/*
Function: TextChangedBanka
	
Thirret kur ndryshojme banken e selektuar. Kur ndryshon banka ndryshon dhe monedha dhe gjendja sipas bankes se re te zgjedhur.
Shiko funksionin <SucceededCallbackBanka>.
*/
function TextChangedBanka() {
    if (txtEmriBanka.GetValue() != null && txtEmriBanka.GetValue() != "") {        
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "merrBankaSipasKodit"),
            data: JSON.stringify({ kodBanka: txtEmriBanka.GetText(), idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
        }).done(SucceededCallbackBanka);
    }
}
function SucceededCallbackBanka(result) {
    if (result.KodiBanka == null) {
        txtEmriBanka.SetText('');
        lblPershkrimiEmriBanka.SetText('');
    }
    else {
        if (result.RrugaBanka != "" && result.RrugaBanka != null) txtAdresaBanka.SetText(result.RrugaBanka + ', ' + result.QytetiBanka);
        else txtAdresaBanka.SetText(result.QytetiBanka);
        txtIban.SetText(result.IBAN);
        txtLlogariBankare.SetText(result.NrLlogariBanka);
        lblPershkrimiEmriBanka.SetText(result.EmerBanka);
    }
}
function SucceededCallbackBankaID(result) {
    if (result.KodiBanka) {
        txtEmriBanka.SetText(result.KodiBanka);
        lblPershkrimiEmriBanka.SetText(result.EmerBanka);
    }
}
function SucceededCallbackKfID(result) {
    if (result.KodKlientFurnitor)
        btneKlientiKryesor.SetText(result.KodKlientFurnitor);

}
function SucceededCallbackMaturimiID(result) {
    btneMaturimi.SetText(result.KodMaturimi);
}

function onActiveTabChanged(s, e) {

    indexModifiko = ASPxGridView_KF.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim';
            $('#hfId')[0].value = 0;
            pastrofusha();

        }
    }
    kaloTab = false; //ishte false
    percaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
    //meqe klientet financiar kane gjithmone nrllogarie

}

function percaktoMenuSipasTabit(index, hfTeDrejta, hfShtimModifikim) {

    var menuItemDraft = ASPxMenu1.GetItemByName('Draft');

    if (menuItemDraft != null) {


        //rasti i pare
        if (index == 0) {
            menuItemDraft.SetVisible(false);
        }

        else if (index > 0) {
            menuItemDraft.SetVisible(true);//jemi ne modifikim

            if (hfShtimModifikim.val() != 'modifikim')//shtim
            {
                if (hfTeDrejta.Get('ShtimDraft'))
                    menuItemDraft.SetEnabled(true);

            }
            else if (hfShtimModifikim.val() == 'modifikim')//modifikim
            {
                if (hfTeDrejta.Get('ModifikimDraft')) {
                    menuItemDraft.SetEnabled(true);
                }

                if (!prospekt)
                    menuItemDraft.SetVisible(false);//fshehim nese eshte klient financiar
            }
            else
                menuItemDraft.SetEnabled(false);
        }

    }
    //vazhdon me pjesen tjeter te butonave
    myMenu.PercaktoMenuSipasTabit(index, hfTeDrejta, hfShtimModifikim);
}

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    } catch (e) { }
}
function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
    hfState.Set("sortColumn", ASPxGridView_KF.cpSortedColumn);
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}
//thirret ne momentin qe klikohet butoni qe konfirmos fshirjen e klient\furnitoreve. Ben callback te grides, dhe ne server
//side ndodhe fshirja
function fshiKlientFurnitor() {
    //ASPxGridView_KF.PerformCallback();
}
//eshte bere me setTimeout sepse gjithesesi thirret ajax per nrauto. ne kete menyre i jep mundesi funksionit mbush() te resetoj combon perpara se te vi nrAuto nqs ka nrAuto per ate kontroll
function vendosNrAutomatik(colAtrTrupi, colKontrollet) { 
    setTimeout(function () { myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date()); }, 0);    
    //            myNrAuto.vendosNrAutomatikCelje(kontrollet, new Date());
}

function ekzistonNumriAutomatikPerKontroll(colKontrollet, colAtrTrupi){
    var idKontrolli = Utils.findFieldValueByAttribute(colKontrollet, "KodKontrolli", "IdKontrolli", "txtKodi");
    var idNrAutomatik = Utils.findFieldValueByAttribute(colAtrTrupi, "IdKontroll", "IdNrAutomatik", idKontrolli);
    return idNrAutomatik > 0;
}

function GrupiKF_Click(llojGrupi) {
    var llojikf = Utils.getUrlVar("kf");
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhgrupinkflupa"), 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + llojGrupi + '&kf=' + llojikf, 500, 500);
}

function hapLupeHarte(s, e) {
    popupUniversal.SetHeaderText('Cakto ne harte');
    popupUniversal.SetContentUrl('LupaHarta.aspx?vjenNga=celjeKlientFurnitor&idNjesia=' + $('#hfId')[0].value);
    popupUniversal.SetSize(700, 800);
    popupUniversal.Show();
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
        else {
            koordinata_x = koordinata[0];
            koordinata_y = koordinata[1];
            if ((!kontrollokoordinate(koordinata_x)) || (!kontrollokoordinate(koordinata_y)))
                alert('Formati i kordinatave duhet te jete: "x.x, y.yy" - pra te ndara me ", "');
        }
        var k = 'POINT (' + koordinata[1] + ' ' + koordinata[0] + ')';
        hfState.Set("geom", JSON.stringify(([k])));
    }
}

function kontrollokoordinate(koordinata) {
    koordinata = koordinata.split('.');
    var kontroll = true;
    if (koordinata.length != 2) {
        return false;
    }
    else
        for (var i = 0; i < koordinata.length; i++) {
            if (isNaN(koordinata[i])) {
                kontroll = false;
                break;
            }
        }
    return kontroll;
}
function TextChanged_txtKodi(s, e) {
    txtKodi2.SetText(txtKodi.GetText());
    txtKodi3.SetText(txtKodi.GetText());
    txtKodi4.SetText(txtKodi.GetText());
    txtKodi5.SetText(txtKodi.GetText());
    txtKodi6.SetText(txtKodi.GetText());
    txtKodi7.SetText(txtKodi.GetText());
}
function LostFocus_txtNr(s, e) {
    txtNr2.SetText(txtNr.GetText());
    txtNr3.SetText(txtNr.GetText());
    txtNr4.SetText(txtNr.GetText());
    txtNr5.SetText(txtNr.GetText());
    txtNr6.SetText(txtNr.GetText());
    txtNr7.SetText(txtNr.GetText());
}

function TextChanged_txtKodi3(s, e) {
    txtKodi.SetText(txtKodi3.GetText());
    txtKodi2.SetText(txtKodi3.GetText());
    txtKodi4.SetText(txtKodi3.GetText());
    txtKodi5.SetText(txtKodi3.GetText());
    txtKodi6.SetText(txtKodi3.GetText());
    txtKodi7.SetText(txtKodi3.GetText());
}
function TextChanged_txtEmertimi3(s, e) {
    txtEmertimi.SetText(txtEmertimi3.GetText());
    txtEmertimi2.SetText(txtEmertimi3.GetText());
    txtEmertimi4.SetText(txtEmertimi3.GetText());
    txtEmertimi5.SetText(txtEmertimi3.GetText());
    txtEmertimi6.SetText(txtEmertimi3.GetText());
    txtEmertimi7.SetText(txtEmertimi.GetText());
}
function LostFocus_txtNr3(s, e) {
    txtNr2.SetText(txtNr3.GetText());
    txtNr.SetText(txtNr3.GetText());
    txtNr4.SetText(txtNr3.GetText());
    txtNr5.SetText(txtNr3.GetText());
    txtNr6.SetText(txtNr3.GetText());
    txtNr7.SetText(txtNr3.GetText());
}
function TextChanged_txtEmertimi4(s, e) {
    txtEmertimi.SetText(txtEmertimi4.GetText());
    txtEmertimi2.SetText(txtEmertimi4.GetText());
    txtEmertimi3.SetText(txtEmertimi4.GetText());
    txtEmertimi5.SetText(txtEmertimi4.GetText());
    txtEmertimi6.SetText(txtEmertimi4.GetText());
    txtEmertimi7.SetText(txtEmertimi.GetText());
} 
function TextChanged_txtKodi2(s, e) {
    txtKodi.SetText(txtKodi2.GetText());
    txtKodi3.SetText(txtKodi2.GetText());
    txtKodi4.SetText(txtKodi2.GetText());
    txtKodi5.SetText(txtKodi2.GetText());
    txtKodi6.SetText(txtKodi2.GetText());
    txtKodi7.SetText(txtKodi2.GetText());
}
function TextChanged_txtEmertimi2(s, e) {
    txtEmertimi.SetText(txtEmertimi2.GetText());
    txtEmertimi3.SetText(txtEmertimi2.GetText());
    txtEmertimi4.SetText(txtEmertimi2.GetText());
    txtEmertimi5.SetText(txtEmertimi2.GetText());
    txtEmertimi6.SetText(txtEmertimi2.GetText());
    txtEmertimi7.SetText(txtEmertimi.GetText());
}
function TextChanged_txtKodi4(s, e) {
    txtKodi.SetText(txtKodi4.GetText());
    txtKodi2.SetText(txtKodi4.GetText());
    txtKodi3.SetText(txtKodi4.GetText());
    txtKodi5.SetText(txtKodi4.GetText());
    txtKodi6.SetText(txtKodi4.GetText());
    txtKodi7.SetText(txtKodi4.GetText());
}
function TextChanged_btneKategoriZbritje(sender, e) {
    var s=btneKategoriZbritje.GetText().split(';');
    btneKategoriZbritje.SetText(s[0]);
    txtKategoriPerqindja.SetText(s[2]);
}
function LostFocus_txtNr4(s, e) {
    txtNr.SetText(txtNr4.GetText());
    txtNr2.SetText(txtNr4.GetText());
    txtNr3.SetText(txtNr4.GetText());
    txtNr5.SetText(txtNr4.GetText());
    txtNr6.SetText(txtNr4.GetText());
    txtNr7.SetText(txtNr4.GetText());
}
function TextChanged_txtKodi7(s, e) {
    txtKodi.SetText(txtKodi7.GetText());
    txtKodi2.SetText(txtKodi7.GetText());
    txtKodi3.SetText(txtKodi7.GetText());
    txtKodi4.SetText(txtKodi7.GetText());
    txtKodi6.SetText(txtKodi7.GetText());
    txtKodi5.SetText(txtKodi7.GetText());
}
function TextChanged_txtEmertimi7(s, e) {
    txtEmertimi.SetText(txtEmertimi7.GetText());
    txtEmertimi2.SetText(txtEmertimi7.GetText());
    txtEmertimi3.SetText(txtEmertimi7.GetText());
    txtEmertimi4.SetText(txtEmertimi7.GetText());
    txtEmertimi6.SetText(txtEmertimi7.GetText());
    txtEmertimi5.SetText(txtEmertimi7.GetText());

}
function LostFocus_txtNr7(s, e) {
    txtNr.SetText(txtNr7.GetText());
    txtNr2.SetText(txtNr7.GetText());
    txtNr3.SetText(txtNr7.GetText());
    txtNr4.SetText(txtNr7.GetText());
    txtNr6.SetText(txtNr7.GetText());
    txtNr5.SetText(txtNr7.GetText());
}
function TextChanged_txtKodi5(s, e) {
    txtKodi.SetText(txtKodi5.GetText());
    txtKodi2.SetText(txtKodi5.GetText());
    txtKodi3.SetText(txtKodi5.GetText());
    txtKodi4.SetText(txtKodi5.GetText());
    txtKodi6.SetText(txtKodi5.GetText());
    txtKodi7.SetText(txtKodi5.GetText());
}
function TextChanged_txtEmertimi5(s, e) {
    txtEmertimi.SetText(txtEmertimi5.GetText());
    txtEmertimi2.SetText(txtEmertimi5.GetText());
    txtEmertimi3.SetText(txtEmertimi5.GetText());
    txtEmertimi4.SetText(txtEmertimi5.GetText());
    txtEmertimi6.SetText(txtEmertimi5.GetText());
    txtEmertimi7.SetText(txtEmertimi.GetText());
}
function LostFocus_txtNr5(s, e) {
    txtNr.SetText(txtNr5.GetText());
    txtNr2.SetText(txtNr5.GetText());
    txtNr3.SetText(txtNr5.GetText());
    txtNr4.SetText(txtNr5.GetText());
    txtNr6.SetText(txtNr5.GetText());
    txtNr7.SetText(txtNr5.GetText());
}

function RifreskoGride() {
    ASPxGridView_KF.PerformCallback("CustomButtonRefreshGrid");
}

function NivelZbritje_Changed() {
    var s = btneNivelZbritje.GetText().split(',');
    if (s.length > 2) {
        myMesazh.ShtoMesazhGabimi("Niveli permban karakter presje, nuk mund te lidhet!");
        btneNivelZbritje.SetSelectedIndex(-1);
        return;
    }
    btneNivelZbritje.SetText(s[1]);
}

function ASPxGridView_KF_OnColumnSorting(s, e) {
    ASPxGridView_KF.cpSortedColumn = (e.column.name + " " + (e.column.sortOrder == 1 ? "Descending" : "Ascending"));
}