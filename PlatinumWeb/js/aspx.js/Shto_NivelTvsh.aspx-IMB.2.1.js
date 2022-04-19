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
var widthLupaLogaria = 800, heightLupaLlogaria = 600;
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvShtoNivelTvsh, "520", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
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
        container: "tblTaksa",
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

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvShtoNivelTvsh, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvShtoNivelTvsh, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvShtoNivelTvsh, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvShtoNivelTvsh, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvShtoNivelTvsh.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgTaksaDuetTeZgjidhniNjeTakse"));
    else gvShtoNivelTvsh.GetRowValues(indexModifiko, 'IdTaksa;KodTaksa;Pershkrimi;NormaPerqindje;LlogariDebi;LlogariKredi;IdLlojTakse;Njesia;IdNivelAutorizimi;Aktiv;TakseNdermarje;LlogariDogane;EPerjashtuar;AplikoTvshNeFleteDoganore;FurnizimeZero;ShitjePaTvshTaksa', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
} identifikuesPerPopupLlogari = "Taksa";
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi.SetText(values[1]);
    txtPershkrimi.SetText(values[2]);
    txtPerqindja.SetText(values[3]);
    cmbLlogDebi.SetText(values[4]);
    cmbLlogKredi.SetText(values[5]);
    cmbLloji.SetValue(values[6]);
    if (values[6] == 1)
    { cmbLlogDog.SetVisible(true); lblLlogDog.SetVisible(true); }
    else { cmbLlogDog.SetVisible(false); lblLlogDog.SetVisible(false); }
    cmbNjesia.SetValue(values[7])
    cmbAutorizimi.SetText(values[8]);
    cbAktiv.SetChecked(values[9]);
    cbTakseNdermarje.SetChecked(values[10]);
    cmbLlogDog.SetText(values[11]);
    cbPerjashtuar.SetChecked(values[12]);
    cbAplikoTvshNeFleteDoganore.SetChecked(values[13]);
    enableAplikoTVSHNeFleteDoganoreSipasLlojTakse();
    cbFurnizimeZero.SetChecked(values[14]);
    cbShitjePaTvshTaksa.SetChecked(values[15]);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrTipinEPerjashtimit"),
        data: JSON.stringify({ idTakse: values[0] })
    }).done(SuksesTipiPerjashtimi);


    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'Taksa', idPerdorues: hfState.Get('idPerdorues') })
    }).done(SuksesAutorizimi);

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "520", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function SuksesAutorizimi(result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
    else cmbAutorizimi.SetValue(null);
}
function SuksesTipiPerjashtimi(result) {
    if (result.tipiIPerjashtimit == "")
        cmbTipiPerjashtimit.SetText("");
    else
        cmbTipiPerjashtimit.SetText(result.tipiIPerjashtimit);
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
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    txtPerqindja.SetText('');
    cmbLlogDebi.SetText('');
    cmbLlogKredi.SetText('');
    cmbLlogDog.SetText('');
    cmbLloji.SetSelectedIndex(0);
    cmbNjesia.SetSelectedIndex(0);
    cbAktiv.SetChecked(true);
    cbShitjePaTvshTaksa.SetChecked(false);
    cbTakseNdermarje.SetChecked(false);
    cbPerjashtuar.SetChecked(false);
    cbAplikoTvshNeFleteDoganore.SetChecked(false);
    cbFurnizimeZero.SetChecked(false);
    cmbAutorizimi.SetValue(null);
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    SelectedIndexChangedLloji(1);
    cmbLlogDog.SetVisible(true);
    lblLlogDog.SetVisible(true);
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
    gvShtoNivelTvsh.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $("#dvNiveli").show();
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;
//        var colAlterKusht;
//        var colKushte;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        //                var colGrida = result[2];
        //                colKushte = result[3];
        //                colAlterKusht = result[4];
        var kodniveli = result.kodniveli;
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblTaksa'];
        //            myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }

   // $("#dvNiveli").show();//$("#dvNiveli")[0].style.visibility = 'visible';
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //        var hfLidhur = $("#hfLidhur")[0];
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    enableAplikoTVSHNeFleteDoganoreSipasLlojTakse();
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("520", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("520", cmbKonfigurimi.GetText()); 
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_NivelTvsh.aspx', 0, hf);
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
    //        indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvShtoNivelTvsh, "520")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvShtoNivelTvsh, "520", pastrofusha, hfTeDrejta);
}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}


function Autorizime_Click() {
    KPF = 0;
    var queryStr = 0 + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, 600, 600);

}

function Llogari_Click() {//thirret popup i llogarive
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx', widthLupaLogaria, heightLupaLlogaria);

}
function SelectedIndexChangedLloji(key) {//kur ndryshon texti tek kolona Autorizimeve
    indeksi = key;

    if (cmbLloji.GetValue() != 3) {
        if ($('#hfShtimModifikim').val() == 'modifikim')
            cmbNjesia.SetEnabled(false);
        cmbNjesia.SetText(hfState.Get("cmbItemBlerjeShitjeperqindje"));
    }
    else cmbNjesia.SetEnabled(true);
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
var llogdebi, llogkredi, llogdogane;

function Value_Changed(s, e) {
    if (cmbLloji.GetValue() == 3) {
        llogdebi = cmbLlogDebi.GetText();
        cmbLlogDebi.SetText('');
        llogkredi = cmbLlogKredi.GetText();
        cmbLlogKredi.SetText('');
        llogdogane = cmbLlogDog.GetText();
        cmbLlogDog.SetText(''); cmbLlogDog.SetVisible(false); lblLlogDog.SetVisible(false);
    }
    else {
        cmbLlogDebi.SetText(llogdebi);
        cmbLlogKredi.SetText(llogkredi);
        cmbLlogDog.SetText(llogdogane);
        cmbLlogDog.SetVisible(true);
        lblLlogDog.SetVisible(true);
        cbAplikoTvshNeFleteDoganore.SetValue(false);
    }
    enableAplikoTVSHNeFleteDoganoreSipasLlojTakse();
}

function Active_TabChanged(s, e) {
    indexModifiko = gvShtoNivelTvsh.GetFocusedRowIndex();    
    if(mbush)
    { 
        if(indexModifiko !=-1)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else 
        {   
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim'; 
            $('#hfId')[0].value = 0; pastrofusha();
        }
    }
    kaloTab=false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
                  
}

function enableAplikoTVSHNeFleteDoganoreSipasLlojTakse() {
    (cmbLloji.GetText() == 'Taksa doganore') ? cbAplikoTvshNeFleteDoganore.SetEnabled(true) : cbAplikoTvshNeFleteDoganore.SetEnabled(false);
}