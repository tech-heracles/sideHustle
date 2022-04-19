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


function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvPikeShitjeFurnizimi, "524", cmbKonfigurimi.GetText());
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



$(document).ready(function () {
    changeName();
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
});
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvPikeShitjeFurnizimi, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvPikeShitjeFurnizimi, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvPikeShitjeFurnizimi, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvPikeShitjeFurnizimi, indexSel);
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
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);

}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvPikeShitjeFurnizimi.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPikeShitjeFurnDuhetTeZgjidhni1PikeShitjeFurn"));
    else
        gvPikeShitjeFurnizimi.GetRowValues(indexModifiko, 'IdPikeShitjeFurnizimi;Kodi;Pershkrimi;Adresa;Aktiv;DateRegjistrimi;IdDegeAdministrative;ShitjeFurnizimi;Koordinata', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }
    txtPershkrimi.SetText(values[2]);
    txtAdresa.SetText(values[3]);
    //        if (values[7] == true)
    //            cmbShitjeFurnizim.SetText('Pike Shitje');
    //        else cmbShitjeFurnizim.SetText('Pike Furnizimi');
    cbAktiv.SetChecked(values[4]);
    cmbDegeAdministrative.SetValue(values[6]);
    if ($('#hfShtimModifikim').val() == 'klonim') {
        var today = new Date();
        dteDtRegjistrimi.SetDate(today);
    }
    else
        dteDtRegjistrimi.SetDate(values[5]);
    if (values[8] != null && values[8] != "") {
        var koordinata = values[8].substring(values[8].indexOf('(') + 1, values[8].length - 1);
        koordinata = koordinata.split(' ');
        btneCaktoNeHarte.SetText(koordinata[1] + ', ' + koordinata[0]);
    }
    else
        btneCaktoNeHarte.SetText("");
    var tmpGeoms = new Array(1);
    tmpGeoms[0] = values[8];
    hfState.Set("geom", JSON.stringify(tmpGeoms));
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "524", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
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
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    hfState.Set("geom", '');
    cmbDegeAdministrative.SetText('');
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    txtAdresa.SetText('');
    cbAktiv.SetChecked(true);
    btneCaktoNeHarte.SetText('');
    dteDtRegjistrimi.SetDate(new Date());
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
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
    gvPikeShitjeFurnizimi.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvPika").show();
    var idGjuha = hfState.Get('idGjuha');
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
        //                var kodniveli = result[5];
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblPikaShitjeFurnizimi'];
        //                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
        if (hfMod.val() == "shtim") {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
    }
    // $("#dvPika").show();//$("#dvPika")[0].style.visibility = 'visible';
}

//    function SucceededCallbackKonfigurimi(result) {
//        if (result != "") {
//            resultkonf = result;
//            var vlerat = '';
//            vlerat = result.split('*');
//            var kontrollet = vlerat[0].split(';');
//            Lupa(kontrollet);
//            var hf = $('#hfKontrollet')[0];
//            var hfLidhur = $("#hfLidhur")[0];
//            var hfMod = $('#hfShtimModifikim')[0];
//            var arrTabela = ['tblPikaShitjeFurnizimi'];
//            myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
//            gvPikeShitjeFurnizimi.PerformCallback("524" + ";" + cmbKonfigurimi.GetText());
//        } $("#dvPika")[0].style.visibility = 'visible';
//    }
function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
    //            myNrAuto.vendosNrAutomatikCelje(kontrollet, new Date());
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    //        var hf6 = $("#hfLupaAutorizimi")[0];
    //        for (var i = 0; i < kontrollet.length - 1; i++) {
    //            if (kontrollet[i].split(',')[0] == "cmbAutorizimi")
    //                hf6.value = kontrollet[i].split(',')[11].toString();
    //        }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //            var hfLidhur = $("#hfLidhur")[0];
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("524", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("524", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_PikeShitjeFurnizimi.aspx?sf=' + Utils.getUrlVar('sf'), 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
    if (Utils.getUrlVar('sf') == 'shitje')
        PageControl.GetTab(1).SetText(hfState.Get("pikeShitjeTab"));
    else PageControl.GetTab(1).SetText(hfState.Get("msgPikeFurnizimi"));
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
    //            indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvPikeShitjeFurnizimi, "524")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvPikeShitjeFurnizimi, "524", pastrofusha, hfTeDrejta);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var KPF;
function Autorizime_Click() {
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, 400, 400);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function hapLupeHarte(s, e) {
    popupUniversal.SetHeaderText('Cakto ne harte');
    popupUniversal.SetContentUrl('LupaHarta.aspx?idNjesia=' + $('#hfId')[0].value);
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
                kontroll= false;
                break;
            }
           

            //if (!isNaN(koordinata[i])) continue;
            //else {
            //    return false;
            //    break;
            //}
        }
    return kontroll;
}
function Active_TabChanged(s, e) {
    indexModifiko = gvPikeShitjeFurnizimi.GetFocusedRowIndex();    
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