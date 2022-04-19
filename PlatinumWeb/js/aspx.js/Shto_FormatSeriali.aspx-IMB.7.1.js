; //variabla per te kaluar nga nje reshti i grides tek tjetri
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
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_FormatSeriali, "3061", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click ose per te selektuar ne lupe
function OnGridDoubleClick(index) {
    if (!hfState.Get("lupe")) {
        indexModifiko = index;
        lista = true;
        mbushfusha();
    }
    else
        ZgjidhRreshtaNgaLupa();
}
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_FormatSeriali, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_FormatSeriali, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_FormatSeriali, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_FormatSeriali, indexSel);
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
    if (hfState.Get("lupe")) {
        switch (e.item.name) {
            case "OK":
                e.processOnServer = false;
                ZgjidhRreshtaNgaLupa();
                break;
            case 'Anullo':
                window.parent.popupUniversal.Hide();
            default:
                break;
        }
    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = ASPxGridView_FormatSeriali.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje serial!');
    else {
        ASPxGridView_FormatSeriali.GetRowValues(indexModifiko, 'idFormatSerialesh;kodFormatSerialesh;pershkrimFormatSerialesh;kategoriFormatSerialesh;Aktiv', OnGetRowValuesMod);
        Utils.shfaqLoadingGif();
    }
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {   
    if ($('#hfShtimModifikim').val() == "shtim")
        return;    
    var id = $('#hfShtimModifikim').val() == "modifikim" ? values[0] : 0;
    $('#hfId').val(id);
    txtKodiFormat.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {        
        txtKodiFormat.SetEnabled(false);
    }
    txtPershkrimFormat.SetText(values[2]);
    gvSeriali.PerformCallback(values[0] + ';modifiko');
    cmbKategoriaFormat.SetValue(values[3]);
    cbAktiv.SetChecked(values[4]);

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');

    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "3061", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: id, idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, id) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {

    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
    aktivizoGride();
}

function aktivizoGride() {
    if ($('#hfLidhur').val() == 'True')
        gvSeriali.SetEnabled(false);
    else gvSeriali.SetEnabled(true);
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodiFormat.SetText('');
    txtPershkrimFormat.SetText('');
    cmbKategoriaFormat.SetSelectedIndex(-1);
    cbAktiv.SetChecked(true);
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    gvSeriali.PerformCallback('0;pastro');
    gvSeriali.SetEnabled(true);
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(false);
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
    ASPxGridView_FormatSeriali.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvBurimi").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi, arrTabela;
function SucceededCallbackKonfig(result) {
    if (result !== "" && result !== null) {
        if (hfState.Get("lupe")) { // nqs ambienti eshte hapur si lupe, nuk duhen kontrollet dhe tabela e kontrolleve, sepse perdoret vetem griga (lista e formateve)
            colKontrollet = []
            arrTabela = [];
        }
        else {
            colKontrollet = result.colKontrollet;
            arrTabela = ['tblFormatet'];
        }
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;

        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');

        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
}



function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("3061", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("3061", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_FormatSeriali.aspx', 0, hf);
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_FormatSeriali, "3061", pastrofusha, hfTeDrejta);
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

function Active_TabChanged(s, e) {
    indexModifiko = ASPxGridView_FormatSeriali.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));

}

var arrKodi = new Array();
var editorKodi;
var indexCounter = 0;


function merrTeDhena() {//merren te dhenat qe ka grida
    arrKodi = new Array();
    var hidField1 = $("#hfKodi"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    for (i = 0; i < gvSeriali.cpNoRows; i++) {
        arrKodi[i] = eval('Kodi' + i).GetText();  
    }
    hidField1.val(JSON.stringify(arrKodi));
  
}  

//kontrollon nese jemi ne rreshtin e fundit
function LostFocusSeriali(editor, key) {
    if (key === gvSeriali.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvSeriali.PerformCallback();
    }
}

//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    gvSeriali.PerformCallback(key);
}

function ZgjidhRreshtaNgaLupa() {
    ASPxGridView_FormatSeriali.GetSelectedFieldValues('idFormatSerialesh;kodFormatSerialesh;pershkrimFormatSerialesh;kategoriFormatSerialesh', OnGridSelectionComplete);
}

function OnGridSelectionComplete(value) {
    if (value.length == 0) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje format seriali!");
        return;
    }
    hide = true;
    var id = ''; var kodi = ''; var pershkrimi = ''; var kategoriSeriali = '';
    if (value.length > 1) {
        for (i = 0; i < value.length - 1; i++) {
            var values = value[i];

            id = id + values[0] + ",";
        }
        values = value[value.length - 1];

        id = id + values[0];
    }
    else {

        values = value[0];
        id = values[0];
        kodi = values[1];
        pershkrimi = values[2];
        kategoriSeriali = values[3];
    }

    if (value.length > 1) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te zgjidhni me shume se nje format seriali!");
        hide = false;
    }
    else {
        if (window.parent.editorFormatSeriali == "Serial") {
            window.parent.editorGlobal.SetValue(values[0]);
            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus();
        }
        else {
            if (window.parent.cmbFormatSeriali.GetText() != '') {
                window.parent.cmbFormatSeriali.SetText('');
                window.parent.cmbFormatSeriali.SetSelectedIndex(-1);
            }
            window.parent.cmbFormatSeriali.SetValue(values[0]);
            window.parent.cmbFormatSeriali.SetText(values[1]);
            window.parent.cmbFormatSeriali.Focus(true);
        }
    }

    if (hide) {
        window.parent.popupUniversal.Hide();
    }
}