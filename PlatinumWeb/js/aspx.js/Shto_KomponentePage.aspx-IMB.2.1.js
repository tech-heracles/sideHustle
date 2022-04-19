; var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var widthLupaLlogaria = 900, heightLupaLlogaria = 600;

pageState = {
    cmbNdryshimiPreviousValue: ''
}
// kontrollon nese ekziston filtri
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
//aplikon filtrin
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKomponentePage, "703", cmbKonfigurimi.GetText());
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
    changeName();
    window.addEventListener("beforeunload", function (e) {
        if ($("#hfKaNdryshimeNeGride").val() == "True") {
            var text = hfState.Get("msgListeKompNdryshimeteParuajtura");
            e.returnValue = text;
            return text;
        }
    })
    pageState.cmbNdryshimiPreviousValue = cmbNdryshimi.GetValue()
});

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKomponentePage, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKomponentePage, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKomponentePage, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKomponentePage, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {

    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //        myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    switch (e.item.name) {
        case "RuajList":
            Utils.shfaqLoadingGif();;
            break;
        case "Default":
            e.processOnServer = false;
            popFshi.Show();
            ButtonOk.SetVisible(false);
            lblMsgbox.SetText(hfState.Get("msgFshikomponenteEkzistuese"));
            break;
        case "Fshi":
            ButtonOk2.SetVisible(false);
            lblMsgbox.SetText(hfState.Get("msgPyetjeFshirje"));

            break;
        case "Aktivizo":
            hapPopUp(e);
    }
}

function hapPopUp(e) {
    var str = {
        dtAktivizimiKomp: dteDateAkt.GetDate().format('MM/dd/yyyy'),
        lloji: Utils.getUrlVar("lloji") == 'true' ? 1 : 0,
        vjenNga: "KomponentePage"
    };
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni Punonjesit", "LupaPunonjes.aspx?" + Utils.KonvertoObjectQueryString(str), 800, 600);
    e.processOnServer = false;
}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvKomponentePage.GetFocusedRowIndex();
    if (indexModifiko === -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniKomponente"));
    else
        gvKomponentePage.GetRowValues(indexModifiko, 'IdKomponentePage;Kodi;Pershkrimi;Njesi;ParamKodi;ParamEmri;Formula;IdLlogDebi;IdLlogKredi;Aktivizimi;Tipi;LlogDebi;LlogKredi;Model;ParamNjesi;AplikoPageMuaji;AplikoDiteMuaji;ShfaqDefault;IdGrupKomponente;LejoModVlere;GrupKomponente;LlogaritGjithmone;Shenime', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() === "modifikim")
        $('#hfModel').val(values[13]);
    else $('#hfModel').val(2);
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }
    txtPershkrimi.SetText(values[2]);
    cmbNjesia.SetValue(values[3]);
    txtParamKodi.SetText(values[4]);
    txtParamEmri.SetText(values[5]);
    txtFormula.SetText(values[6]);
    if (values[11] !== "" && values[11] !== null)
        cmbLlogDebi.SetSelectedIndex(cmbLlogDebi.AddItem(values[11], values[7]));
    else cmbLlogDebi.SetText('');
    if (values[12] !== "" && values[12] !== null)
        cmbLlogKredi.SetSelectedIndex(cmbLlogKredi.AddItem(values[12], values[8]));
    else cmbLlogKredi.SetText('');
    if (values[9] === true)
        cmbAktivizimi.SetValue('True');
    else cmbAktivizimi.SetValue('False');
    cmbTipi.SetValue(values[10]);
    cmbNjesiaParam.SetValue(values[14]);
    cbAplikoPage.SetChecked(values[15]);
    cbAplikoDite.SetChecked(values[16]);
    cbShfaqDefault.SetChecked(values[17]);
    cbLejoModVlere.SetChecked(values[19]);
    cbLlogaritGjithmone.SetChecked(values[21]);
    txtShenime.SetText(values[22]);
    if (values[20] !== "" && values[20] !== null)
        cmbGrup.SetSelectedIndex(cmbGrup.AddItem(values[20], values[18]));
    else cmbGrup.SetText('');
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "703", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
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
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    cmbNjesia.SetSelectedIndex(0); cmbNjesiaParam.SetSelectedIndex(0);
    txtParamKodi.SetText('');
    txtParamEmri.SetText('');
    txtFormula.SetText('');;
    cmbLlogDebi.SetText('');
    cmbLlogKredi.SetText('');
    cbAplikoPage.SetChecked(false);
    cbAplikoDite.SetChecked(false);
    cbShfaqDefault.SetChecked(false);
    cbLejoModVlere.SetChecked(false);
    cbLlogaritGjithmone.SetChecked(true);
    cmbGrup.SetText('');
    cmbAktivizimi.SetSelectedIndex(0);
    cmbTipi.SetSelectedIndex(0);
    txtShenime.SetText('');
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    aktivizofushaNjesia();

    $('#hfModel').val(2);
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

//thiret konfigurimi kur e ndryshojme ate
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    gvKomponentePage.PerformCallback(idKomp + ";" + kodKonf);
}

//thirret kur hapet faqja ne fillim
function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvKomponente").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblAktivizimi', 'tblKomponente'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C", 0);
    }
}


//mbush hidden fieldet me konfigurimet e lupes
function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaLlogariDebi");
    var hf2 = $("#hfLupaLlogariKredi");

    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli === "cmbLlogDebi")
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
        else
            if (kontrollet[i].KodKontrolli === "cmbLlogKredi")
                hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
    }
}
// aktivizon fushat
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //        myFaqeCelje.aktivizoFusha(vlerat, hfMod, hfLidhur, '#ASPxPageControl1_');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    aktivizofushaNjesia();
}
//thirret kur lodohet faqja
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("703", cmbKonfigurimi.GetText());
}
//thirret kur ndryshon konfigurimi
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("703", cmbKonfigurimi.GetText());
}
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}
// ndryshon emrin e faqes
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_KomponentePage.aspx?lloji=' + Utils.getUrlVar('lloji'), 0, hf);
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
    //        indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKomponentePage, "703")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKomponentePage, "703", pastrofusha, hfTeDrejta);
    aktivizofushaNjesia();
    //ne rastin qe nuk ka ndryshime te paruajtura ndryshojem dt
    if (!($("#hfKaNdryshimeNeGride").val() == "True")) cmbNdryshimiChanged();
    $('#hfModel').val(2);
}

//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    changeName();

}
function Grupim_Click() {
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni Grupin", 'LupaGrupKomponente.aspx', 600, 600);
}
//hap lupen e llogarive
function Llogari_Click(nr) {//thirret popup i llogarive
    if (nr === 1)
        var hf = $("#hfLupaLlogariDebi");
    else
        hf = $("#hfLupaLlogariKredi");
    var queryStr = hf.val() + "&komponentenga=Shto_KomponentePage" + nr;
    myButtonClickLupa.ButtonClickLlogaria('Zgjidh Llogarine', queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
//validon te dhenat
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
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
//heq veprimin e enterit
function enter() {
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}
//aktivizon fushat kur kalojme ne njesine formule dhe anasjelltas
function aktivizofushaNjesia() {
    var njesia = cmbNjesia.GetText();
    var kodKomp = txtKodi.GetText();
    cmbNjesia.SetEnabled(njesia != "Tab" && kodKomp != "PRN");
    if (njesia === 'Nr' || njesia === 'Tab') {
        txtParamKodi.SetText('');
        txtParamEmri.SetText('');
        cmbNjesiaParam.SetSelectedIndex(0);
        txtFormula.SetText('');
        txtParamKodi.SetEnabled(false);
        txtParamEmri.SetEnabled(false);
        txtFormula.SetEnabled(false);
        cmbNjesiaParam.SetEnabled(false);
    }
    else if (kodKomp == "PRN") {
        txtParamKodi.SetEnabled(false);
        txtParamEmri.SetEnabled(false);
        cmbNjesiaParam.SetEnabled(false);
    }
    else {
        txtParamKodi.SetEnabled(true);
        txtParamEmri.SetEnabled(true);
        txtFormula.SetEnabled(true);
        cmbNjesiaParam.SetEnabled(true);
    }
}

function Active_TabChanged(s, e) {
    indexModifiko = gvKomponentePage.GetFocusedRowIndex();

    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim';
            $('#hfId')[0].value = 0;
            $('#hfModel').val(2);
            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim')); aktivizofushaNjesia();
}
function cmbNdryshimiChanged(s, e) {
    if ($("#hfKaNdryshimeNeGride").val() == "True") {
        myMesazh.ShtoMesazh({
            text: hfState.Get("msgListeKompNdryshimeteParuajturaNdryshimDate"),
            type: "confirm",
            modal: true,
            idGjuha: hfState.Get('idGjuha'),
            okClick: function () {
                pageState.cmbNdryshimiPreviousValue = cmbNdryshimi.GetValue();
                $("#hfKaNdryshimeNeGride").val("False");
                vendosDateAktivizimi();
            },
            cancelClick: function () {
                cmbNdryshimi.SetValue(pageState.cmbNdryshimiPreviousValue);
            }
        });
    } else {
        vendosDateAktivizimi();
    }
   

}

function vendosDateAktivizimi() {
    var selectedItem = cmbNdryshimi.GetText();
    if (selectedItem == "" || selectedItem == undefined) return;

    var dateArray = selectedItem.split('/');
    var data = new Date(dateArray[1] + '/' + dateArray[0] + '/' + dateArray[2]);
    dteDateAkt.SetDate(data);
    gvKomponentePage.PerformCallback('ndryshodate');
}