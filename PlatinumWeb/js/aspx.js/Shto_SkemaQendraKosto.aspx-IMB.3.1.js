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
    myMenu.aplikoFiltra(s, e, gvSkema, "904", cmbKonfigurimi.GetText());
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvSkema, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvSkema, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvSkema, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvSkema, indexSel);
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
    indexModifiko = gvSkema.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSkemaQKZgjidhniNjeSkeme"));
    else
        gvSkema.GetRowValues(indexModifiko, 'IdKoka;Kodi;Pershkrimi', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
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
    txtPershkrimi.SetText(values[2]);
    gvTrupi.PerformCallback(values[0] + ";modifiko");
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "904", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
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
    //        aktivizoFusha(hf.value);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    gvTrupi.PerformCallback(-1 + ";shto");
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
    gvSkema.PerformCallback(idKomp + ";" + kodKonf);
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
        var arrTabela = ['tblSkema', ];
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
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("904", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("904", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_SkemaQendraKosto.aspx',0, hf);
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvSkema, "904", pastrofusha, hfTeDrejta);
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

var editorKodi;
var editorPershkrimi;
var editorPerqindja;

var indexCounter = 0;

function merrTeDhena() {//merren te dhenat qe ka grida
    arrKodi = new Array();
    arrEmertimi = new Array();
    arrPerqindja = new Array();
  
    var hidField1 = $("#hfKodi"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidfield2 = $("#hfEmertimi");
    var hidField3 = $("#hfKoha");

    for (i = 0; i < gvTrupi.cpNoRows; i++) {


        arrKodi[i] = Utils.ktheKontroll('Kodi' + i).GetText();
        arrEmertimi[i] = Utils.ktheKontroll('Emertimi' + i).GetText();
        arrPerqindja[i] = Utils.ktheKontroll('Perqindja' + i).GetText();
      
    }
    hidField1.val(JSON.stringify(arrKodi));
    hidfield2.val(JSON.stringify(arrEmertimi))
    hidField3.val(JSON.stringify(arrPerqindja))
}  //kontrollon nese jemi ne rreshtin e fundit
function KeyPressQendra(kodi, editor, key) {

    if (kodi === 13) {
        editorKodi = editor;
        editorPershkrimi = Utils.ktheKontroll('Emertimi' + key);
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgSkemaQKZgjidhQK"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Shto_Skema', 900, 560);
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function LostFocusQendra(editor, editorEmer, key) {


    if (key === gvTrupi.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupi.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function ButtonClickedQendra(editor, key) {

    editorKodi = editor;
    editorPershkrimi = Utils.ktheKontroll('Emertimi' + key);
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgSkemaQKZgjidhQK"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Shto_Skema', 900, 560);
  
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedQendra(editor, editorEmer, key) {
    var a = new Array();

    a = editor.GetText().toString().split(',');

    editorEmer.SetText(editor.GetSelectedItem().GetColumnText('Pershkrimi'));
    editor.SetText(editor.GetSelectedItem().GetColumnText('Kodi'));
    if (editor.GetText() != "")
        for (var i = 0; i < gvTrupi.cpNoRows; i++) {
            if (i != key && editor.GetText() == Utils.ktheKontroll('Kodi' + i).GetText()) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgSkemaQKKjoQKEshtePerdorur1HereNeKeteSkeme"))
                editor.SetText('');
                editorEmer.SetText('');
                Utils.ktheKontroll('Perqindja' + key).SetText('0.00');
                break;
            }
        }
   
    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupi.PerformCallback();
    }
}

//kontrollon nese jemi ne rreshtin e fundit
function TextChangedPerqindja(editor, key) {
    if (isNaN(parseFloat(editor.GetText()))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerqindjaDuhetJeteNumer"));
        editor.SetText('0.00');
    }
    if (parseFloat(editor.GetText()) > 100 || parseFloat(editor.GetText())<0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSkemaQKPerqindjaDuhetNumerMidis0Dhe100"));
        editor.SetText('0.00');
    }
   
   if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupi.PerformCallback();
    }
}

//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    gvTrupi.PerformCallback(key);
}


function Active_TabChanged(s, e) {
    indexModifiko = gvSkema.GetFocusedRowIndex();    
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