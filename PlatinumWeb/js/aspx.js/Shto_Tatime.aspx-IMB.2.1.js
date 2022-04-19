;
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
// kontrollon nese ekziston filtri
function checkText(s, e) {
    myMenu.checkText(s, e);
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
    pastro();
});

var btnFiltrat;
//aplikon filtrin
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvTatimet, "706", cmbKonfigurimi.GetText());
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvTatimet, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvTatimet, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvTatimet, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvTatimet, indexSel);
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
}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvTatimet.GetFocusedRowIndex();
    if (indexModifiko === -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje komponente!');
    else
        gvTatimet.GetRowValues(indexModifiko, 'IdTatime;Data;Min;Max;Norma;Menyra;Model', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() === "modifikim")
        $('#hfModel').val(values[6]);
    else $('#hfModel').val(2);
    dteDateAkt.SetDate(values[1]);
    txtMin.SetText(values[2]);
    txtMax.SetText(values[3]);
    txtNorma.SetText(values[4]);
    cmbMenyra.SetValue(values[5]);

    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "706", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
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
    var hf = $('#hfKontrollet'); //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    dteDateAkt.SetDate(Utils.zeroOren(new Date()));
    txtMin.SetText('0');
    txtMax.SetText('0');
    txtNorma.SetText('0');
    cmbMenyra.SetSelectedIndex(0);
    var hf = $('#hfKontrollet');
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    $('#hfModel').val(2);
    llogaritmin();
}
function llogaritmin() {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMin"),
        data: JSON.stringify({ date: dteDateAkt.GetDate() })
    }).done(SuccededCallbackMin);
}
function SuccededCallbackMin(result) {
    txtMin.SetText(result);
    if (result !== "0")
        txtMin.SetEnabled(false);
    else txtMin.SetEnabled(true);

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
    gvTatimet.PerformCallback(idKomp + ";" + kodKonf);
}

//thirret kur hapet faqja ne fillim
function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvTatime").show();
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
        //            var kodniveli = result[5];
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblData', 'tblTatime'];
        //            myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C", 0);
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C", 0);
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C", 0);
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C", 0);
    }

    //$("#dvTatime").show();//$("#dvTatime")[0].style.visibility = 'visible'; $("#dvTatime")[0].style.display = '';
}
//    function SucceededCallbackKonfigurimi(result) {
//        if (result !== "") {
//            resultkonf = result;
//            var vlerat = '';
//            vlerat = result.split('*');
//            var kontrollet = vlerat[0].split(';');
//            niveli = vlerat[3];
//            var hf = $('#hfKontrollet')[0];
//            var hfLidhur = $("#hfLidhur")[0];
//            var hfMod = $('#hfShtimModifikim')[0];
//            var arrTabela = ['tblData','tblTatime'];
//            myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C", 0);
//            gvTatimet.PerformCallback("706" + ";" + cmbKonfigurimi.GetText());
//        } $("#dvTatime")[0].style.visibility = 'visible'; $("#dvTatime")[0].style.display = '';
//    }

// aktivizon fushat
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
//thirret kur lodohet faqja
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("706", cmbKonfigurimi.GetText());
}
//thirret kur ndryshon konfigurimi
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("706", cmbKonfigurimi.GetText());
}
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
// ndryshon emrin e faqes
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Tatime.aspx',0, hf);
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
    //        indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvTatimet, "706")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvTatimet, "706", pastrofusha, hfTeDrejta);
    $('#hfModel').val(2);
}

//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    changeName();

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
   
function Active_TabChanged(s, e) {
    indexModifiko = gvTatimet.GetFocusedRowIndex();
                      
    if(mbush)
    { 
        if(indexModifiko !=-1)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else {  
            mbush = false;
            $('#hfShtimModifikim').val( 'shtim'); 
            $('#hfId').val( 0); 
            $('#hfModel').val(2);
            pastrofusha();
        }
    }
    kaloTab=false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));  
}