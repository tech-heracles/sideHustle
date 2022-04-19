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
});

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvAktivitetet, "802", cmbKonfigurimi.GetText());
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
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvAktivitetet, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvAktivitetet, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvAktivitetet, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvAktivitetet, indexSel);
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
    //        myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name == 'Ruaj') {
        merrTeDhena();
    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvAktivitetet.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje aktivitet!');
    else
        gvAktivitetet.GetRowValues(indexModifiko, 'IdKoka;Kodi;Emertimi;Pershkrimi;NjesiKohe;KohaPlan', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }
    txtEmertimi.SetText(values[2]);
    txtPershkrimi.SetText(values[3]);
    cmbNjesiKohe.SetValue(values[4]);
    txtKohaPlan.SetText(values[5]);
    gvTrupi.PerformCallback(values[0] + ";modifiko");
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrDataNdryshimiAktiviteti"),
        data: JSON.stringify({ idkoka: values[0] })
    }).done(SucceededCallbackData);
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "802", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
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
    txtEmertimi.SetText('');
    txtPershkrimi.SetText('');
    cmbNjesiKohe.SetSelectedIndex(0);
    txtKohaPlan.SetText('0.00');
    gvTrupi.PerformCallback(-1 + ";shto");
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrDataNdryshimiAktiviteti"),
        data: JSON.stringify({ idkoka: 0 })
    }).done(SucceededCallbackData);
    dteDateAkt.SetText(new Date().getDate() + '/' + parseInt(new Date().getMonth() + 1) + '/' + new Date().getFullYear());
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
    gvAktivitetet.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvAktiviteti").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;

function SucceededCallbackKonfig(result) {
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;

        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblAktiviteti', 'tblBurimet'];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
   // $("#dvAktiviteti").show();//$("#dvAktiviteti")[0].style.visibility = 'visible';
}

function LupaKontrollet(kontrollet, colAtrTrupi) {

}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));

    callWebserviceKonfigurimi("802", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("802", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Aktivitete.aspx', 0, hf);
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvAktivitetet, "802", pastrofusha, hfTeDrejta);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
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
var arrKodi = new Array();
var arrEmertimi = new Array();
var arrPerqindja = new Array();
var arrKostoBurimi = new Array();
var arrKosto = new Array();
var editorKodi;
var editorPershkrimi;
var editorPerqindja;
var editorKosto;
var editorKostoBurimi;
var indexCounter = 0;
var editorKoha;

function merrTeDhena() {//merren te dhenat qe ka grida
    arrKodi = new Array();
    arrEmertimi = new Array();
    arrPerqindja = new Array();
    arrKostoBurimi = new Array();
    arrKosto = new Array();
    var hidField1 = $("#hfKodi"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidfield2 = $("#hfEmertimi");
    var hidField3 = $("#hfKoha");
    var hidField4 = $("#hfKostoBurimi");
    var hidField5 = $("#hfKosto");
    for (i = 0; i < gvTrupi.cpNoRows; i++) {


        arrKodi[i] = Utils.ktheKontroll('Kodi' + i).GetText();
        arrEmertimi[i] = Utils.ktheKontroll('Emertimi' + i).GetText();
        arrPerqindja[i] = Utils.ktheKontroll('Koha' + i).GetText();
        arrKostoBurimi[i] = Utils.ktheKontroll('KostoBurimi' + i).GetText();
        arrKosto[i] = Utils.ktheKontroll('Kosto' + i).GetText();
    }
    hidField1.val(JSON.stringify(arrKodi));
    hidfield2.val(JSON.stringify(arrEmertimi))
    hidField3.val(JSON.stringify(arrPerqindja))
    hidField4.val(JSON.stringify(arrKostoBurimi))
    hidField5.val(JSON.stringify(arrKosto))
}  //kontrollon nese jemi ne rreshtin e fundit
function KeyPressBurimi(kodi, editor, key) {

    if (kodi === 13) {
        editorKodi = editor;
        editorPershkrimi = Utils.ktheKontroll('Emertimi' + key);
        editorKostoBurimi = Utils.ktheKontroll('KostoBurimi' + key);
        editorKosto = Utils.ktheKontroll('Kosto' + key);
        editorPerqindja = Utils.ktheKontroll('Koha' + key);
        editorKoha = Utils.ktheKontroll('Koha' + key);
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni burimin', 'LupaBurime.aspx?vjenNga=Shto_Aktivitete', 600, 560);
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function LostFocusBurimi(editor, editorEmer, key) {


    if (key === gvTrupi.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupi.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function ButtonClickedBurimi(editor, key) {

    editorKodi = editor;
    editorPershkrimi = Utils.ktheKontroll('Emertimi' + key);
    editorKostoBurimi = Utils.ktheKontroll('KostoBurimi' + key);
    editorKosto = Utils.ktheKontroll('Kosto' + key);
    editorPerqindja = Utils.ktheKontroll('Koha' + key);
    editorKoha = Utils.ktheKontroll('Koha' + key);
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni burimin', 'LupaBurime.aspx?vjenNga=Shto_Aktivitete', 600, 560);

}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedBurimi(editor, editorEmer, key) {
    var a = new Array();

    a = editor.GetText().toString().split(',');

    editorEmer.SetText(editor.GetSelectedItem().GetColumnText('Emertimi'));
    Utils.ktheKontroll('KostoBurimi' + key).SetText(editor.GetSelectedItem().GetColumnText('KostoPlan'));
    editor.SetText(editor.GetSelectedItem().GetColumnText('Kodi'));
    if (editor.GetText() != "")
        for (var i = 0; i < gvTrupi.cpNoRows; i++) {
            if (i != key && editor.GetText() == Utils.ktheKontroll('Kodi' + i).GetText()) {
                myMesazh.ShtoMesazhGabimi('Ky burim eshte perdorur njehere ne kete aktivitet!')
                editor.SetText('');
                editorEmer.SetText('');
                Utils.ktheKontroll('KostoBurimi' + key).SetText('0.00');
                Utils.ktheKontroll('Kosto' + key).SetText('0.00');
                break;
            }
        }
    vendosKosto(Utils.ktheKontroll('Kosto' + key), Utils.ktheKontroll('Koha' + key), Utils.ktheKontroll('KostoBurimi' + key));

    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupi.PerformCallback();
    }
}

//kontrollon nese jemi ne rreshtin e fundit
function TextChangedKoha(editor, key) {
    if (isNaN(parseFloat(editor.GetText()))) {
        myMesazh.ShtoMesazhGabimi('Koha duhet te jete numer!');
        editor.SetText('0.00');
    }
    if (parseFloat(editor.GetText()) > parseFloat(txtKohaPlan.GetText())) {
        myMesazh.ShtoMesazhGabimi('Koha e burimit nuk duhet te kaloje kohen e aktivititetit!');
        editor.SetText(parseFloat(txtKohaPlan.GetText()));
    }
    vendosKosto(Utils.ktheKontroll('Kosto' + key), Utils.ktheKontroll('Koha' + key), Utils.ktheKontroll('KostoBurimi' + key));


    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupi.PerformCallback();
    }
}

function vendosKosto(editorKosto, editorKoha, editorKostoBurim) {
    switch (cmbNjesiKohe.GetValue()) {
        case '1':
            editorKosto.SetText((parseFloat(editorKoha.GetText()) * parseFloat(editorKostoBurim.GetText()) / 3600).toFixed(2));
            break;
        case '2':
            editorKosto.SetText((parseFloat(editorKoha.GetText()) * parseFloat(editorKostoBurim.GetText()) / 60).toFixed(2));
            break;
        case '3':
            editorKosto.SetText((parseFloat(editorKoha.GetText()) * parseFloat(editorKostoBurim.GetText())).toFixed(2));
            break;
        case '4':
            editorKosto.SetText((parseFloat(editorKoha.GetText()) * parseFloat(editorKostoBurim.GetText()) * 24).toFixed(2));
            break;
    }
}
//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    gvTrupi.PerformCallback(key);
}
function ndryshoKosto() {
    for (var i = 0; i < gvTrupi.cpNoRows; i++) {
        vendosKosto(Utils.ktheKontroll('Kosto' + i), Utils.ktheKontroll('Koha' + i), Utils.ktheKontroll('KostoBurimi' + i));
    }
}
function kontrolloKoha() {
    for (var i = 0; i < gvTrupi.cpNoRows; i++) {
        if (Utils.ktheKontroll('Koha' + i).GetText() > parseFloat(txtKohaPlan.GetText())) {
            Utils.ktheKontroll('Koha' + i).SetText(parseFloat(txtKohaPlan.GetText()));
            vendosKosto(Utils.ktheKontroll('Kosto' + i), Utils.ktheKontroll('Koha' + i), Utils.ktheKontroll('KostoBurimi' + i));
            myMesazh.ShtoMesazhGabimi('Koha e disa burimeve u ndryshua qe te mos kaloje kohen e aktivitetit!');
        }

    }

}
function Active_TabChanged(s, e) {
    indexModifiko = gvAktivitetet.GetFocusedRowIndex();    
    if(mbush)
    { 
        if(indexModifiko !=-1)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else 
        {   
            mbush = false;
            $('#hfShtimModifikim').val( 'shtim'); 
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab=false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta,$('#hfShtimModifikim'));
                  
}