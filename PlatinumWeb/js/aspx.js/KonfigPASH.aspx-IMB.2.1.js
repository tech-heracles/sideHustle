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
//perdoret per te marre te dhenat e nje rreshti per tia kaluar ato faqes se modifikimit  
var widthLupaLogaria = 1100, heightLupaLlogaria = 560;

var identifikuesperKategoriShpenzimi = "KonfigPASH";

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKonfigPASH, "227", "");
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
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKonfigPASH, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKonfigPASH, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKonfigPASH, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKonfigPASH, indexSel);
}
/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var grida = $('#' + hfState.Get('lastselgrid'))
    var idRresht = grida.getLastSel2();
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, false);
    if (e.item.name === 'Ruaj') {
        $("#" + hfState.Get('lastselgrid')).jqGrid('saveRow', idRresht, null, 'clientArray', {}, ruajTeDhena);
        $('#HiddenFieldZerat').val(JSON.stringify(colTrupat));
        pastro();

    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvKonfigPASH.GetFocusedRowIndex();
    if (indexModifiko === -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje konfigurim!');
    else
        gvKonfigPASH.GetRowValues(indexModifiko, 'IdPasqyresFin;KodiPasqyresFin;EmertimiPasqyresFin;Metoda', OnGetRowValuesMod);
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    kodi_ASPxTextBox.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim")
        kodi_ASPxTextBox.SetEnabled(false);
    else
        kodi_ASPxTextBox.SetEnabled(true);
    metoda_ASPxComboBox.SetText(values[3]);
    emertimi_ASPxTextBox.SetText(values[2]);
    grida.setLastSel2(1);
    hfState.Set('lastselgrid', "ASPxPageControl1_rowed5");
    hfState.Set('lastniveli', 1);
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrKonfigurimPasqyre"),
        data: JSON.stringify({ id: values[0] })
    }).done(SucceededCallbackPasqyre);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
var colTrupat = new Array();
//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackPasqyre(result) {
    colTrupat = result;
    inicializogridasipasLlojit();
}
function inicializogridasipasLlojit() {
    jQuery("#ASPxPageControl1_rowed5").jqGrid('GridUnload', "ASPxPageControl1_rowed5");
    jQuery("#ASPxPageControl1_rowed6").jqGrid('GridUnload', "ASPxPageControl1_rowed6");
    jQuery("#ASPxPageControl1_rowed7").jqGrid('GridUnload', "ASPxPageControl1_rowed7");
    jQuery("#ASPxPageControl1_rowed8").jqGrid('GridUnload', "ASPxPageControl1_rowed8");
    if (Utils.getUrlVar('lloji') === 'Pash') {
        inicializoGride("ASPxPageControl1_rowed5", $('#form1').width() - 50, 1, "PASH");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed5", $('#form1').width() - 50, "PASH");
    }
    else if (Utils.getUrlVar('lloji') === 'PashOJF') {
        inicializoGride("ASPxPageControl1_rowed5", $('#form1').width() - 50, 1, "PASHOJF");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed5", $('#form1').width() - 50, "PASHOJF");
    }
    else if (Utils.getUrlVar('lloji') === 'Buxhetor') {
        inicializoGride("ASPxPageControl1_rowed5", $('#form1').width() - 50, 1, "Buxhetor");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed5", $('#form1').width() - 50, "Buxhetor");
    }
    else if (Utils.getUrlVar('lloji') === 'Bilanc' || Utils.getUrlVar('lloji') === 'BilancOJF') {
        inicializoGride("ASPxPageControl1_rowed5", $('#form1').width() - 50, 1, "Aktive");
        inicializoGride("ASPxPageControl1_rowed6", $('#form1').width() - 50, 1, "Detyrime");
        inicializoGride("ASPxPageControl1_rowed7", $('#form1').width() - 50, 1, "Kapitali");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed5", $('#form1').width() - 50, "Aktive");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed6", $('#form1').width() - 50, "Detyrime");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed7", $('#form1').width() - 50, "Kapitali");
    }
    else {
        inicializoGride("ASPxPageControl1_rowed5", $('#form1').width() - 50, 1, "Llogarite Cash");
        inicializoGride("ASPxPageControl1_rowed6", $('#form1').width() - 50, 1, "Fluks Shfrytezimi");
        inicializoGride("ASPxPageControl1_rowed7", $('#form1').width() - 50, 1, "Fluks Investues");
        inicializoGride("ASPxPageControl1_rowed8", $('#form1').width() - 50, 1, "Fluks Financiar");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed5", $('#form1').width() - 50, "Llogarite Cash");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed6", $('#form1').width() - 50, "Fluks Shfrytezimi");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed7", $('#form1').width() - 50, "Fluks Investues");
        mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed8", $('#form1').width() - 50, "Fluks Financiar");
    }
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    var grida = $('#' + hfState.Get('lastselgrid'));
    kodi_ASPxTextBox.SetEnabled(true);
    kodi_ASPxTextBox.SetText('');
    metoda_ASPxComboBox.SetText('');
    emertimi_ASPxTextBox.SetText('');
    colTrupat = new Array();
    grida.setLastSel2(1);
    hfState.Set('lastselgrid', "ASPxPageControl1_rowed5");
    hfState.Set('lastniveli', 1);
    inicializogridasipasLlojit();
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

function changeName() {
    myFaqeCelje.changeName('KonfigPASH.aspx?lloji=' + Utils.getUrlVar('lloji'), 0, null);
    if (Utils.getUrlVar('lloji') === 'Pash' || Utils.getUrlVar('lloji') === 'PashOJF') {
        PageControl.GetTab(3).SetVisible(false);
        PageControl.GetTab(4).SetVisible(false);
        PageControl.GetTab(5).SetVisible(false);
        PageControl.GetTab(2).SetText("Pash");
        metoda_ASPxComboBox.SetVisible(false);
        lblMetoda.SetVisible(false);
    }
    else if (Utils.getUrlVar('lloji') === 'BilancOJF') {
        PageControl.GetTab(2).SetText("Aktive");
        PageControl.GetTab(3).SetText('Detyrime');
        PageControl.GetTab(4).SetText('Aktive Neto');
        PageControl.GetTab(5).SetVisible(false);
        PageControl.GetTab(6).SetVisible(false);
        metoda_ASPxComboBox.SetVisible(false);
        lblMetoda.SetVisible(false);
    }
    else if (Utils.getUrlVar('lloji') === 'Buxhetor') {
        PageControl.GetTab(3).SetVisible(false);
        PageControl.GetTab(4).SetVisible(false);
        PageControl.GetTab(5).SetVisible(false);
        PageControl.GetTab(6).SetVisible(false);
        PageControl.GetTab(2).SetText("Buxhetor");
        metoda_ASPxComboBox.SetVisible(false);
        lblMetoda.SetVisible(false);
    }
    else if (Utils.getUrlVar('lloji') === 'Bilanc') {
        PageControl.GetTab(3).SetText('Detyrime');
        PageControl.GetTab(4).SetText('Kapitali');
        PageControl.GetTab(5).SetVisible(false);
        PageControl.GetTab(2).SetText("Aktive");
        metoda_ASPxComboBox.SetVisible(false);
        lblMetoda.SetVisible(false);
    }
    else PageControl.GetTab(6).SetVisible(false);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));

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
    indexModifiko = myFaqeCelje.EndRequestHandlerPas(sender, args, hf, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKonfigPASH, "227", hfTeDrejta)
}


//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {

    var hf = $("#hfStatusi")[0];
    hf.value = "false";
}
//    //buxhetet
function ShtoBuxhet1(editor, edgjendja, eddiff, key) {
    myBuxhet.ShtoBuxhet1Pas(editor, edgjendja, eddiff, key, merrbuxhete());
}
function ShtoBuxhet2(editor, edgjendja, eddiff, key) {
    myBuxhet.ShtoBuxhet2Pas(editor, edgjendja, eddiff, key, merrbuxhete());
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal1(editor, edgjendja, eddiff) {
    myBuxhet.ShtoTotal1Pas(editor, edgjendja, eddiff, merrbuxhete());
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal2(editor, edgjendja, eddiff) {
    myBuxhet.ShtoTotal2Pas(editor, edgjendja, eddiff, merrbuxhete());
}
function valido(s, e) {
    var activeTabIndex = PageControl.GetActiveTab().index;
    for (var i = 1; i < 2; i++) {
        PageControl.SetActiveTab(PageControl.GetTab(i));
        var isvalid = ASPxClientEdit.ValidateGroup("entries");
        if (isvalid === false) {
            e.processOnServer = false;
            PageControl.SetActiveTab(PageControl.GetTab(i));
            Utils.hiqLoadingGif();;
            myMenu.PercaktoMenuSipasTabit(i, hfTeDrejta, $('#hfShtimModifikim'));
            break;
        }
        else
            PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex));
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function BeginCallback(s, e) {

    if (e.command === 'APPLYFILTER' && btnFiltrat !== undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}
function GetRowValuesZeratBuxheti() {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    var pershkrimi = "";
    var datarow = $('#' + hfState.Get('lastselgrid')).getRowData(idRresht);
    if (datarow.cmbLloji === undefined || datarow.cmbLloji.search('cmbLloji') === -1 || datarow.cmbGjendja !== undefined) {
        PageControl.SetActiveTab(PageControl.GetTab(2));
        myMesazh.ShtoMesazhGabimi("Zgjidhni njerin nga zerat");
    }
    else {

        pershkrimi = jQuery('#txtPershkrimi' + hfState.Get('lastselgrid') + idRresht).val();
        zeri_buxheti_ASPxLabel.SetText(pershkrimi);
        if (pershkrimi === "") {
            PageControl.SetActiveTab(PageControl.GetTab(2));
            myMesazh.ShtoMesazhGabimi("Zgjidhni njerin nga zerat");
        }
        mbushbuxhete();
    }
}
function merrbuxhete() {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    var newid = merrNewId(hfState.Get('lastselgrid'), idRresht);
    var coltrupgrida = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PrindiZerit == merrPershkrimPrindi(hfState.Get('lastselgrid')) && item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && item.NiveliZerit == gjenNivel(hfState.Get('lastselgrid')); })
                   .Select(function (item) { return item; });
    if (coltrupgrida.Count() <= newid) {
        newid = colTrupat.length;
        colTrupat[newid] = new Object();
        colTrupat[newid].OColLlogarite = new Array();
        colTrupat[newid].OColBuxhetet = new Array();
        inicializobuxhete(newid);
        colTrupat[newid].IdTrupi = 0;
        colTrupat[newid].IdKoka = 0;
        colTrupat[newid].PershkrimiZerit = jQuery('#txtPershkrimi' + hfState.Get('lastselgrid') + idRresht).val();
        colTrupat[newid].PrindiZerit = merrPershkrimPrindi(hfState.Get('lastselgrid'));
        colTrupat[newid].LlojiZerit = merrLlojZeri(hfState.Get('lastselgrid'));
        return colTrupat[newid].OColBuxhetet;
    }
    return coltrupgrida.ElementAt(newid).OColBuxhetet;
}
function mbushbuxhete() {
    var colbuxh = merrbuxhete();
    for (var i = 0; i < colbuxh.length; i++) {
        Utils.ktheKontroll('labelGjendja' + i).SetText(colbuxh[i].Gjendja);
        Utils.ktheKontroll('textboxBuxh1' + i).SetText(colbuxh[i].Buxheti_1);
        Utils.ktheKontroll('textboxBuxh2' + i).SetText(colbuxh[i].Buxheti_2);
        Utils.ktheKontroll('labelDiff1' + i).SetText(colbuxh[i].Diferenca_1);
        Utils.ktheKontroll('labelDiff2' + i).SetText(colbuxh[i].Diferenca_2);
    }
}

//hfState.Set('lastselgrid', "ASPxPageControl1_rowed5");
//hfState.Set('lastprindi', "");
//hfState.Set('lastniveli', 1);
//var grida = $('#' + hfState.Get('lastselgrid'));
//var idRresht = grida.setLastSel2(1);
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var IdKonfigAmbjenteLupat = new Array();
var arrayIdKolonaSubGrides = new Array();
var arrayPershkrimiKolonaSubGrides = new Array();
var arrayVisibleKolonaSubGrides = new Array();
var arrayWidthKolonaSubGrides = new Array();
var arrayReadOnlyKolonaSubGrides = new Array();
var arrayRenditjeKolonaSubGrides = new Array();

jQuery(document).ready(function () {
    formoArrayKolonaGrides();
    formoArrayKolonaSubGrides();
    inicializoGride("ASPxPageControl1_rowed5", $('#form1').width() - 50, 1);
    mbushGrideNgaHiddenFieldi("", 1, "ASPxPageControl1_rowed5");
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

function inicializoGride(gridaid, width, nivel, lloji) {
    var idRresht = $('#' + gridaid).getLastSel2();
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5]];
    var arrayModel = [
                    { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo, custom_value: myJQGrid.myValueCombo } },
                    { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodiZeri, custom_value: myJQGrid.myValueTextBox } },
                    { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
                    { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myelemTotal, custom_value: myJQGrid.myValueCombo } },
                    { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], editable: true, edittype: 'custom', editoptions: { custom_element: myelemShfaq, custom_value: myJQGrid.myValueCombo } },
                     { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButon, custom_value: myvalueFshi } },
    ]


    var gridParams = {
        emergride: '#' + gridaid,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        widthi: width,
        caption: "Niveli ",
        niveli: nivel,
        afterSaveFunc: ruajTeDhena,
        lloji: lloji,
        subgrid: true,
        inicializoSubGride: inicializoGride,
        mbushSubGridenRreshtit: mbushGrideNgaHiddenFieldi,

        arrayRenditjeKolonaGridesName: "arrayRenditjeKolonaGrides",
        hidegrid: true,
        selektorDivgride2: "#form1",
        selektorDivgride3: '#form1',
        lostFocusKoloneFundit: lostFocusKoloneFundit,


    };
    return myJQGrid.initGride(gridParams);

}
function inicializobuxhete(id) {
    var muajt = ["Totali", "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor"];
    for (var i = 0; i < 13; i++) {
        colTrupat[id].OColBuxhetet[i] = new Object();
        colTrupat[id].OColBuxhetet[i].IdBuxheti = 0;
        colTrupat[id].OColBuxhetet[i].IdLlojBuxheti = 0;
        colTrupat[id].OColBuxhetet[i].IdLidhese = 0;
        colTrupat[id].OColBuxhetet[i].Buxheti_1 = 0;
        colTrupat[id].OColBuxhetet[i].Buxheti_2 = 0;
        colTrupat[id].OColBuxhetet[i].Diferenca_1 = 0;
        colTrupat[id].OColBuxhetet[i].Diferenca_2 = 0;
        colTrupat[id].OColBuxhetet[i].Gjendja = 0;
        colTrupat[id].OColBuxhetet[i].Muaj = muajt[i];
    }


}
function ruajTeDhena(id, response) {
    var newid = merrNewId(hfState.Get('lastselgrid'), id);
    var dataRow = $('#' + hfState.Get('lastselgrid')).getRowData(id);
    if (dataRow.txtPershkrimi === undefined) {
        ruajLlogari(newid, dataRow);
        return;
    }
    var coltrupgrida = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PrindiZerit == merrPershkrimPrindi(hfState.Get('lastselgrid')) && item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && item.NiveliZerit == gjenNivel(hfState.Get('lastselgrid')); })
                   .Select(function (item) { return item; });
    if (coltrupgrida.Count() <= newid) {
        newid = colTrupat.length;
        colTrupat[newid] = new Object();
        colTrupat[newid].OColLlogarite = new Array();
        colTrupat[newid].OColBuxhetet = new Array();
        inicializobuxhete(newid);
        colTrupat[newid].IdTrupi = 0;
        colTrupat[newid].IdKoka = 0;
    }
    else {
        newid = colTrupat.indexOf(coltrupgrida.ElementAt(newid));
        coltrupgrida = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PrindiZerit == colTrupat[newid].PershkrimiZerit && item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && (item.NiveliZerit - 1) == colTrupat[newid].NiveliZerit; })
                   .Select(function (item) { return item; });
        for (var i = 0; i < coltrupgrida.Count() ; i++)
            coltrupgrida.ElementAt(i).PrindiZerit = dataRow.txtPershkrimi;
    }

    colTrupat[newid].PershkrimiZerit = dataRow.txtPershkrimi;
    colTrupat[newid].KodZeri = dataRow.txtKodiZeri;
    colTrupat[newid].ShfaqBij = (dataRow.cbShfaqBij == "Po") ? true : false;
    colTrupat[newid].PrindiZerit = merrPershkrimPrindi(hfState.Get('lastselgrid'));
    colTrupat[newid].NiveliZerit = gjenNivel(hfState.Get('lastselgrid'));
    colTrupat[newid].LlojiZerit = merrLlojZeri(hfState.Get('lastselgrid'));
    colTrupat[newid].GjeneroTotal = (dataRow.cbTotali == "") ? 0 : (dataRow.cbTotali == "Lart") ? 1 : 2; //.search('checked') == -1) ? false : true;

}
function fshiTeDhena(grid, id) {
    var newid = merrNewId(grid, id);
    var dataRow = $('#' + grid).getRowData(id);
    if (dataRow.txtPershkrimi === undefined) {
        var coltrupsipasllojit = JSLINQ(colTrupat)
                    .Where(function (item) { return item.PershkrimiZerit == merrPershkrimPrindi(grid) && item.LlojiZerit == merrLlojZeri(grid) && item.NiveliZerit == (gjenNivel(grid) - 1); })
                   .Select(function (item) { return item; });
        var colLlogarigrida = coltrupsipasllojit.First().OColLlogarite;
        if (colLlogarigrida.length > newid) {
            colLlogarigrida.splice(newid, 1);
        }
        return;
    }

    if (dataRow.txtPershkrimi !== "") {
        coltrupsipasllojit = JSLINQ(colTrupat)
                    .Where(function (item) { return item.PershkrimiZerit == dataRow.txtPershkrimi && item.LlojiZerit == merrLlojZeri(grid) && item.NiveliZerit == gjenNivel(grid); })
                   .Select(function (item) { return item; });
        fshitrup(coltrupsipasllojit.First(), merrLlojZeri(grid));
    }
    else {
        coltrupsipasllojit = JSLINQ(colTrupat)
                    .Where(function (item) { return item.PrindiZerit == merrPershkrimPrindi(grid) && item.LlojiZerit == merrLlojZeri(grid) && item.NiveliZerit == gjenNivel(grid); })
                   .Select(function (item) { return item; });
        var ind = colTrupat.indexOf(coltrupsipasllojit.ElementAt(newid));
        if (ind !== -1)
            colTrupat.splice(ind, 1);
    }
}
function fshitrup(trupi, lloji) {
    var coltrupgrida = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PrindiZerit == trupi.PershkrimiZerit && item.LlojiZerit == lloji && (item.NiveliZerit - 1) == trupi.NiveliZerit; })
                   .Select(function (item) { return item; });
    for (var i = 0; i < coltrupgrida.Count() ; i++) {
        fshitrup(coltrupgrida.ElementAt(i), lloji);
    }
    var newid = colTrupat.indexOf(trupi);
    if (newid !== -1) colTrupat.splice(newid, 1);
}
function ruajLlogari(newid, dataRow) {
    var coltrupsipasllojit = JSLINQ(colTrupat)
                    .Where(function (item) { return item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && item.PershkrimiZerit == merrPershkrimPrindi(hfState.Get('lastselgrid')) && item.NiveliZerit == (gjenNivel(hfState.Get('lastselgrid')) - 1) })
                   .Select(function (item) { return item; });
    var colLlogarigrida = coltrupsipasllojit.First().OColLlogarite;
    if (colLlogarigrida.length <= newid) {

        newid = colLlogarigrida.length;
        colLlogarigrida[newid] = new Object();
        colLlogarigrida[newid].IdLlogariaTrupi = 0;
        colLlogarigrida[newid].IdTrupi = 0;
        colLlogarigrida[newid].IdPerdoruesi = 0;
    }

    colLlogarigrida[newid].IdLlogaria = dataRow.txtKodi;
    colLlogarigrida[newid].Emertimi = dataRow.txtPershkrimiLlogaria;
    colLlogarigrida[newid].Gjendja = dataRow.cmbGjendja;
    colLlogarigrida[newid].Shenja = dataRow.cmbShenja;
    colLlogarigrida[newid].ShfaqBij = (dataRow.cbShfaqBijSub == "Po") ? true : false;
    colLlogarigrida[newid].Lloji = dataRow.cmbLloji;

}
//krijon elementin buton per fshirjen gjate editimit te reshtit
function myElemButon() {//po
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] === 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, hfState.Get('lastselgrid'), lostFocusKoloneFundit, true);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, hfState.Get('lastselgrid'), lostFocusKoloneFundit, true);
}
function myvalueFshi(elem, operation, value) {//po
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] === 'True')
        return myJQGrid.myValueButtonFshi(true, idRresht, hfState.Get('lastselgrid'), true);
    else
        return myJQGrid.myValueButtonFshi(false, idRresht, hfState.Get('lastselgrid'), true);

}
function myelemCombo(value) {//po
    return myElemCom(value, 1);
}
function myElemPershkrimi(value) {//po
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = arrayReadOnlyKolonaGrides[2];
    return myJQGrid.myElemEmertimi(value, disabled, hfState.Get('lastselgrid') + idRresht, arrayIdKolonaGrides[2], eksitonzeri);

}
function myElemKodiZeri(value) {//po
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemEmertimi(value, disabled, hfState.Get('lastselgrid') + idRresht, arrayIdKolonaGrides[1], eksitonzeri);

}
function eksitonzeri() {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    if ($("#txtPershkrimi" + hfState.Get('lastselgrid') + idRresht).val() === "")
        return;
    var per = "";
    var ndryshuar = false;
    var newid = merrNewId(hfState.Get('lastselgrid'), idRresht);
    var coltrupgrida = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PrindiZerit == merrPershkrimPrindi(hfState.Get('lastselgrid')) && item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && item.NiveliZerit == gjenNivel(hfState.Get('lastselgrid')); })
                   .Select(function (item) { return item; });
    if (coltrupgrida.Count() > newid) {
        newid = colTrupat.indexOf(coltrupgrida.ElementAt(newid));
        if (newid !== -1) {
            ndryshuar = true;
            per = colTrupat[newid].PershkrimiZerit;
            colTrupat[newid].PershkrimiZerit = "";
        }
    }
    coltrupgrida = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PershkrimiZerit == $("#txtPershkrimi" + hfState.Get('lastselgrid') + idRresht).val() })
                   .Select(function (item) { return item; });
    if (coltrupgrida.Count() !== 0) {
        myMesazh.ShtoMesazhGabimi("Eksiton nje ze me kete emertim. Ju lutem shenoni nje emertim tjeter!");
        $("#txtPershkrimi" + hfState.Get('lastselgrid') + idRresht).val("");
    }
    if (ndryshuar)
        colTrupat[newid].PershkrimiZerit = per;

}
function myelemTotal(value) {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = false;
    var arrayOptions = new Array(3);
    var objTmp = new Object();
    objTmp.value = "0";
    objTmp.text = "";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "1";
    objTmp.text = "Lart";
    arrayOptions[1] = objTmp;
    objTmp = new Object();
    objTmp.value = "2";
    objTmp.text = "Poshte";
    arrayOptions[2] = objTmp;
    if (arrayReadOnlyKolonaGrides[3] === 'True') {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[3], hfState.Get('lastselgrid') + idRresht, null, arrayOptions, disabled);
}
function myelemShfaq(value) {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = false;
    var arrayOptions = new Array(2);
    var objTmp = new Object();
    objTmp.value = "true";
    objTmp.text = "Po";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "false";
    objTmp.text = "Jo";
    arrayOptions[1] = objTmp;
    if (arrayReadOnlyKolonaGrides[4] === 'True') {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[4], hfState.Get('lastselgrid') + idRresht, null, arrayOptions, disabled);
}
function lostFocusKoloneFundit() {     //po 
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    $('#' + hfState.Get('lastselgrid')).jqGrid('saveRow', idRresht, null, 'clientArray', {}, ruajTeDhena);
    var idRow = $('#' + hfState.Get('lastselgrid')).lostFocusKoloneFundit();
    $('#' + hfState.Get('lastselgrid')).setLastSel2(idRow);

}

function mbushGrideNgaHiddenFieldi(pershkrimi, niveli, gridaemer, widthi, lloji) {
    grida = $("#" + gridaemer);
    var idRresht = grida.getLastSel2();

    if (colTrupat.length > 0) {
        var coltrupsipasllojit = JSLINQ(colTrupat)
                   .Where(function (item) { return item.LlojiZerit == lloji && item.NiveliZerit == niveli })
                   .Select(function (item) { return item; });
        grida.setLastSel2(1);
        grida.delRowData(1);
        grida.delRowData(2);
        idRresht = 1;
       var lloj = 'Zeri';
        var be;
        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True')
            be = myJQGrid.myValueButtonFshi(true, idRresht, (gridaemer), true);
        else
            be = myJQGrid.myValueButtonFshi(false, idRresht, (gridaemer), true);
        for (var i = 0; i < coltrupsipasllojit.Count() ; i++) {
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True')
                be = myJQGrid.myValueButtonFshi(true, idRresht, (gridaemer), true);
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, (gridaemer), true);
            if (niveli !== 1 && coltrupsipasllojit.ElementAt(i).PrindiZerit !== pershkrimi)
                continue;
            else {
                var kodi = (coltrupsipasllojit.ElementAt(i).KodZeri == undefined) ? "" : coltrupsipasllojit.ElementAt(i).KodZeri;
                var emertimi = (coltrupsipasllojit.ElementAt(i).PershkrimiZerit == undefined) ? "" : coltrupsipasllojit.ElementAt(i).PershkrimiZerit;
                var tot = (coltrupsipasllojit.ElementAt(i).GjeneroTotal == undefined) ? "" : (coltrupsipasllojit.ElementAt(i).GjeneroTotal == 0) ? "" : (coltrupsipasllojit.ElementAt(i).GjeneroTotal == 1) ? "Lart" : "Poshte";
                var bij = (coltrupsipasllojit.ElementAt(i).ShfaqBij == undefined) ? "" : (coltrupsipasllojit.ElementAt(i).ShfaqBij == true) ? "Po" : "Jo";
                var datarow = { cmbLloji: lloj, txtKodiZeri: kodi, txtPershkrimi: emertimi, cbTotali: tot, cbShfaqBij: bij, txtFshi: be };
                var su;
                if (emertimi !== "") {
                    su = grida.addRowData(parseInt(idRresht), datarow);
                    idRresht = idRresht + 1;
                    grida.setLastSel2(idRresht);
                }
            }

        }
        var colLlogari = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PershkrimiZerit == pershkrimi && item.LlojiZerit == lloji; })
                   .Select(function (item) { return item.OColLlogarite; });
        if (colLlogari.items.length > 0 && colLlogari.FirstOrDefault().length > 0) {
            grida.GridUnload(grida);
            inicializoSubGride(gridaemer, niveli, widthi);
            mbushSubGrideNgaHiddenFieldi(pershkrimi, gridaemer);
            return;
        }
        datarow = { cmbLloji: "", txtPershkrimi: "", cbTotali: "", txtFshi: be };
        su = $(grida).addRowData(parseInt(idRresht), datarow);
    }
}


function formoArrayKolonaGrides() {
    if ($("#hfKolonaGride").val() === undefined || $("#hfKolonaGride").val() === "")
        return;

    myJQGrid.formArrayKolGrides($("#hfKolonaGride"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, false, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}
//formon array per konfigurimin e sub grides
function formoArrayKolonaSubGrides() {
    if ($("#hfKolonaSubGride").val() === undefined || $("#hfKolonaSubGride").val() === "")
        return;

    myJQGrid.formArrayKolGrides($("#hfKolonaSubGride"), arrayIdKolonaSubGrides, arrayPershkrimiKolonaSubGrides, arrayVisibleKolonaSubGrides, arrayReadOnlyKolonaSubGrides, false, arrayWidthKolonaSubGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaSubGrides);
}

//ndryshon imazhin e butonit fshi kur mausi i vete siper
function ndryshoImazhin(nr, index) {
    myJQGrid.ndryshoImazhin(nr, index);
}
function renditKolonatGrides() {
    jQuery("#ASPxPageControl1_rowed5").remapColumns(arrayRenditjeKolonaGrides);
}
function fshiClicked(emergride, index) {//po
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    $("#" + hfState.Get('lastselgrid')).jqGrid('saveRow', idRresht, null, 'clientArray', {}, ruajTeDhena);
    lastselgrid = "ASPxPageControl1_rowed5";
    grida.setLastSel2(1);
    fshiTeDhena(emergride, index);
    $("#" + emergride).collapseSubGridRow(index);
    if ($("#" + emergride).getGridParam('reccount') > 1)
        $("#" + emergride).delRowData(index);
    else
        if (emergride === "ASPxPageControl1_rowed5" || emergride === "ASPxPageControl1_rowed6" || emergride === "ASPxPageControl1_rowed7" || emergride === "ASPxPageControl1_rowed8") {
            jQuery("#" + emergride).GridUnload("#" + emergride);
            inicializoGride(emergride, $('#form1').width() - 50, 1, merrLlojZeri(emergride));
        }
        else {
            $("#" + merrEmerPrindi(emergride)).collapseSubGridRow(merrIndexPrindi(emergride));
        }

    return false;
}
function merrEmerPrindi(emergride) {
    var prind = "";
    var arr = emergride.split('_t');
    for (var i = 0; i < arr.length - 2; i++)
        //prind += arr[i] + "_t";
        prind = prind + arr[i] + "_t";
    if (prind === "")
        prind = merrgridenkryesore(emergride);
    return prind;
}
function merrIndexPrindi(emergride) {
    var index = 1;
    var arr = emergride.split('_t');
    arr[0] = arr[0].replace(merrgridenkryesore(emergride) + '_', "")
    if (arr.length >= 2)
        index = arr[arr.length - 2].replace("_", "");
    return index;
}
function gjenNivel(emergride) {
    var arr = emergride.split('_t');
    return arr.length;
}
function merrgridenkryesore(emergride) {
    var arr = emergride.split('_t');
    var gridekryesore = arr[0].split('_')[0] + '_' + arr[0].split('_')[1];
    return gridekryesore;
}
function merrLlojZeri(emergride) {
    var gridakryesore = merrgridenkryesore(emergride);

    if (Utils.getUrlVar('lloji') === 'Pash')
        var llojzeri = 'PASH';
    else if (Utils.getUrlVar('lloji') === 'PashOJF')
        var llojzeri = 'PASHOJF';
    else if (Utils.getUrlVar('lloji') === 'Buxhetor')
        var llojzeri = 'Buxhetor';
    else if (Utils.getUrlVar('lloji') === 'Bilanc' || Utils.getUrlVar('lloji') === 'BilancOJF') {
        if (gridakryesore === "ASPxPageControl1_rowed5") llojzeri = 'Aktive';
        else if (gridakryesore === "ASPxPageControl1_rowed6") llojzeri = 'Detyrime';
        else llojzeri = 'Kapitali';
    } else {
        if (gridakryesore === "ASPxPageControl1_rowed5") llojzeri = 'Llogarite Cash';
        else if (gridakryesore === "ASPxPageControl1_rowed6") llojzeri = 'Fluks Shfrytezimi';
        else if (gridakryesore === "ASPxPageControl1_rowed7") llojzeri = 'Fluks Investues';
        else llojzeri = 'Fluks Financiar';
    }
    return llojzeri;
}

function changeGrid(gridid, llojgride, id) {
    var nivel = gjenNivel(gridid);
    var widthi = $('#form1').width() - 50 - ((nivel - 1) * 25);
    var lloj = $('#cmbLloji' + gridid + id).val();
    if (nivel === 1 && (lloj === "2" || lloj === "3" || lloj === "4")) {
        myMesazh.ShtoMesazhGabimi("Te niveli i pare lejohet vetem lloji Zeri!");
        $('#cmbLloji' + gridid + id).val(1);
        return;
    }
 
    if (lloj === "1" && llojgride === 2) {
        var coltrupsipasllojit = JSLINQ(colTrupat)
                   .Where(function (item) { return item.LlojiZerit == merrLlojZeri(gridid) && item.PershkrimiZerit == merrPershkrimPrindi(gridid) && item.NiveliZerit == (gjenNivel(hfState.Get('lastselgrid')) - 1) })
                    .Select(function (item) { return item; });
        coltrupsipasllojit.First().OColLlogarite = new Array();
        $("#" + gridid).GridUnload("#" + gridid);

        inicializoGride(gridid, widthi, nivel, merrLlojZeri(gridid));
        $("#" + gridid).jqGrid('setSelection', 1);

        $('#cmbLloji' + gridid + 1).val(lloj); $('#cmbLloji' + gridid + 1).focus();
    }
  
   
    else if (((lloj === "2" || lloj === "3") || lloj === "4" )  && llojgride === 1) {
        var coltrupgrida = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PrindiZerit == merrPershkrimPrindi(gridid) && item.LlojiZerit == merrLlojZeri(gridid) && item.NiveliZerit == gjenNivel(gridid); })
                   .Select(function (item) { return item; });
        for (var i = 0; i < coltrupgrida.Count() ; i++) {
            fshitrup(coltrupgrida.ElementAt(i), merrLlojZeri(gridid));
        }
        $("#" + gridid).GridUnload("#" + gridid);
        inicializoSubGride(gridid, nivel, widthi);
        $("#" + gridid).jqGrid('setSelection', 1);
        $('#cmbLloji' + gridid + 1).val(lloj); $('#cmbLloji' + gridid + 1).focus();
    }
    else {
        jQuery("#txtKodi" + gridid + id).val('');
        jQuery("#txtPershkrimiLlogaria" + gridid + id).val('');
    }
}

//inicializon griden sipas te dhenave te ruajtura ne database per llogarite dhe ka funksione  per editimin e reshtave
function inicializoSubGride(subgrid_table_id, niveli, width) {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var arrayPershkrime = [arrayPershkrimiKolonaSubGrides[0], arrayPershkrimiKolonaSubGrides[1], arrayPershkrimiKolonaSubGrides[2], arrayPershkrimiKolonaSubGrides[3], arrayPershkrimiKolonaSubGrides[4], arrayPershkrimiKolonaSubGrides[5], arrayPershkrimiKolonaSubGrides[6]];
    var arrayModel = [
            { name: arrayIdKolonaSubGrides[0], index: arrayIdKolonaSubGrides[0], width: arrayWidthKolonaSubGrides[0], hidden: arrayVisibleKolonaSubGrides[0], editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboS, custom_value: myJQGrid.myValueCombo } },
            { name: arrayIdKolonaSubGrides[1], index: arrayIdKolonaSubGrides[1], width: arrayWidthKolonaSubGrides[1], hidden: arrayVisibleKolonaSubGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myelemKodi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaSubGrides[2], index: arrayIdKolonaSubGrides[2], width: arrayWidthKolonaSubGrides[2], hidden: arrayVisibleKolonaSubGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimiLlogaria, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaSubGrides[3], index: arrayIdKolonaSubGrides[3], width: arrayWidthKolonaSubGrides[3], hidden: arrayVisibleKolonaSubGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboGjendja, custom_value: myJQGrid.myValueCombo } },
            { name: arrayIdKolonaSubGrides[4], index: arrayIdKolonaSubGrides[4], width: arrayWidthKolonaSubGrides[4], hidden: arrayVisibleKolonaSubGrides[4], editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboShenja, custom_value: myJQGrid.myValueCombo } },
            { name: arrayIdKolonaSubGrides[5], index: arrayIdKolonaSubGrides[5], width: arrayWidthKolonaSubGrides[5], hidden: arrayVisibleKolonaSubGrides[5], editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboShfaqBijSub, custom_value: myJQGrid.myValueCombo } },
             { name: arrayIdKolonaSubGrides[6], index: arrayIdKolonaSubGrides[6], width: arrayWidthKolonaSubGrides[6], hidden: arrayVisibleKolonaSubGrides[6], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButon, custom_value: myvalueFshi } }
    ]

    var gridParams = {
        emergride: '#' + subgrid_table_id,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        widthi: width,
        caption: "Llogarite ",
        niveli: niveli,
        afterSaveFunc: ruajTeDhena,
        emerEditorKodi: arrayIdKolonaSubGrides[1],
        subgrid: false,


        arrayRenditjeKolonaGridesName: "arrayRenditjeKolonaSubGrides",
        hidegrid: true,
        selektorDivgride2: "#form1",
        selektorDivgride3: '#form1',
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        autocompleteList: [
            { emerEditor: arrayIdKolonaSubGrides[1], selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
        ]

    };
    return myJQGrid.initGride(gridParams);
}

//krijon elementin combo per llojin kur reshti editohet
function myelemComboS(value) {
    return myElemCom(value, 2);
}
function myElemCom(value, lloj) {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    var disabled = false;
    var arrayOptions = new Array(3);
    if (Utils.getUrlVar('lloji') === 'Pash')
        var arrayOptions = new Array(4);
    var objTmp = new Object();
    objTmp.value = "1";
    objTmp.text = "Zeri";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "2";
    objTmp.text = "Llogari";
    arrayOptions[1] = objTmp;
    objTmp = new Object();
    objTmp.value = "3";
    objTmp.text = "Llogari standarte";
    arrayOptions[2] = objTmp;
   
    if (Utils.getUrlVar('lloji') === 'Pash') {
        objTmp = new Object();
        objTmp.value = "4";
        objTmp.text = "Kategori Shpenzimi";
        arrayOptions[3] = objTmp;
    }
    
  
    if (arrayReadOnlyKolonaGrides[0] === 'True') {
        disabled = true;
    }
    if (lloj === 1 && value === "")
        value = "Zeri";
    else if (lloj === 2 && value === "")
        value = "Llogari";
    else if (lloj === 4 && value === "")
        value = "Kategori Shpenzimi";
    var arrParam = new Array(3);
    arrParam[0] = hfState.Get('lastselgrid');
    arrParam[1] = lloj;
    arrParam[2] = idRresht;
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[0], hfState.Get('lastselgrid') + idRresht, changeGrid, arrayOptions, disabled, arrParam);
}
function keyPressKodi(event) {     //po      
    callWebserviceKodi();
}
/*
Function: callWebserviceKodi
    
Sugjeron listen e artikujve, llogarive ose makrove kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackKodi>.
*/
function callWebserviceKodi() {//po
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    var vlera = $('#txtKodi' + hfState.Get('lastselgrid') + idRresht).val();
    var kategoria = $('#cmbLloji' + hfState.Get('lastselgrid') + idRresht);
    if (kategoria.val() === "3") { //Artikull
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeKPFsh"),
            data: JSON.stringify({ infixText: vlera, idNdermarrje: hfState.Get("_idNdermarrje"), idPerdoruesi: hfState.Get("_idPerdoruesi") })
        }).done(SucceededCallbackKodi);

        return;
    }
    if (kategoria.val() === "2") { //Llogari
        $.ajax({
            pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish"),
            data: JSON.stringify({ infixText: vlera, pershk: 1, idNdermarrje: hfState.Get("_idNdermarrje"), idPerdoruesi: hfState.Get("_idPerdoruesi") })
        }).done(SucceededCallbackKodi);
        return;
    }
}
/*
Function: SucceededCallbackKodi
    
Sugjeron listen e artikujve, llogarive ose makrove kur shkruajme te kodi.
*/
function SucceededCallbackKodi(result) {//po
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + hfState.Get('lastselgrid') + idRresht);
}
//krijon elementin text per kodin kur reshti editohet
function myelemKodi(value, options) {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = arrayReadOnlyKolonaSubGrides[1];
    return myJQGrid.myElemKodi(value, options, disabled, hfState.Get('lastselgrid') + idRresht, arrayIdKolonaSubGrides[1], ButtonClickKodi, keyPressKodi, changeFunc, false, lostFocusKoloneFundit);
}
function kaPrind() {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    if (hfState.Get('lastselgrid') === merrgridenkryesore(hfState.Get('lastselgrid')))
        return true;
    var prind = merrEmerPrindi(hfState.Get('lastselgrid'));
    var index = merrIndexPrindi(hfState.Get('lastselgrid'));

    var pershkrimi = $("#" + prind).getTekstQelize('txtPershkrimi', index, prind + index);

    if (pershkrimi === "") {
        myMesazh.ShtoMesazhGabimi('Ju lutem jepni emertimin e zerit prind');
        $("#" + hfState.Get('lastselgrid')).jqGrid('saveRow', idRresht, null, 'clientArray', {});
        $('#' + prind).setSelection(index, true);
        return false;
    }
    return true;
}
function myelemComboShfaqBijSub(value) {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    var disabled = false;
    var arrayOptions = new Array(2);
    var objTmp = new Object();
    objTmp.value = "true";
    objTmp.text = "Po";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "false";
    objTmp.text = "Jo";
    arrayOptions[1] = objTmp;
    if (arrayReadOnlyKolonaSubGrides[5] === 'True') {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaSubGrides[5], hfState.Get('lastselgrid') + idRresht, null, arrayOptions, disabled);
}
function merrPershkrimPrindi(emergride) {
    if (emergride === merrgridenkryesore(emergride))
        return "";
    var prind = merrEmerPrindi(emergride);
    var index = merrIndexPrindi(emergride);
    var pershkrimi = $("#" + prind).getTekstQelize('txtPershkrimi', index, prind + index);

    return pershkrimi;
}
//perdoret per te kontrolluar nese ekziston e njejta llogari tek ky ze dhe me te njejten gjendje me zerat e tjere
function eksistonLLogNeKetePrind(lloj, prind, index, kod, tipi, gjendje) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eksitonLLogNeKetePrind"),
        data: JSON.stringify({ col: colTrupat, lloj: lloj, prind: prind, index: index, kod: kod, tip: tipi, gjendje: gjendje, idndermarje: hfState.Get("_idNdermarrje") })
    }).done(SucceededCallbackPrindi);
}
//funksioni qe kthen pergjigjen e web serverit
function SucceededCallbackPrindi(result) {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    var eksiton = result[0];
    if (eksiton) {
        var kodi = "#txtKodi" + hfState.Get('lastselgrid') + idRresht;
        var emerArt = '#' + 'txtPershkrimiLlogaria' + hfState.Get('lastselgrid') + idRresht;
        myMesazh.ShtoMesazhGabimi(result[1]);
        $(kodi).val('');
        $(emerArt).val('');
    }
}
//funksioni per hapjen e popupit te llogarive
function ButtonClickKodi() {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    editorGlobal = $('#txtKodi' + hfState.Get('lastselgrid') + idRresht);
    editorPershkrimillogaria = $('#txtPershkrimiLlogaria' + hfState.Get('lastselgrid') + idRresht);

    if (!kaPrind())
        return;

    var lloji = $('#cmbLloji' + hfState.Get('lastselgrid') + idRresht).val();
    if (lloji === "2") {
        popupUniversal.SetHeaderText('Zgjidh llogarine');
        popupUniversal.SetContentUrl('LupaLlogaria.aspx?vjennga=KonfigPash');
        popupUniversal.SetSize(widthLupaLogaria, heightLupaLlogaria);
        popupUniversal.Show();

    }
    else if (lloji === "3") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidh llogarine standarte', 'LupaKpf.aspx?id=1&vjennga=KonfigPash', widthLupaLogaria, heightLupaLlogaria);
    }

    else if (lloji === "4") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidh Kategori Shpenzimi', 'LupaKategoriShpenzimi.aspx', 680, 600);
    }
    return false;
}
//funksioni per ruajtjen e ndryshimeve te llogarive ne array
function changeKodi() {
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    var newid = merrNewId(hfState.Get('lastselgrid'), idRresht);
    lastprindi = merrPershkrimPrindi(hfState.Get('lastselgrid'));
    var coltrupsipasllojit = JSLINQ(colTrupat)
                     .Where(function (item) { return item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && item.PershkrimiZerit == merrPershkrimPrindi(hfState.Get('lastselgrid')) && item.NiveliZerit == (gjenNivel(hfState.Get('lastselgrid')) - 1) })
                    .Select(function (item) { return item; });
    var colLlogarigrida = coltrupsipasllojit.First().OColLlogarite;
    if (colLlogarigrida.length <= newid) {

        newid = colLlogarigrida.length;
    }

    eksistonLLogNeKetePrind(merrLlojZeri(hfState.Get('lastselgrid')), lastprindi, newid, $("#txtKodi" + hfState.Get('lastselgrid') + idRresht).val(), $("#cmbLloji" + hfState.Get('lastselgrid') + idRresht + " option:selected").text(), $("#cmbGjendja" + hfState.Get('lastselgrid') + idRresht + " option:selected").text());
}


// krijon elementin textbox per pershkrimin  llogaria kur rreshti eshte ne editim
function myElemPershkrimiLlogaria(value) {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = arrayReadOnlyKolonaSubGrides[2];
    return myJQGrid.myElemEmertimi(value, disabled, hfState.Get('lastselgrid') + idRresht, arrayIdKolonaSubGrides[2]);
}
//krijon elementin combo per gjendjen kur reshti editohet
function myelemComboGjendja(value) {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = false;
    var arrayOptions = new Array(3);
    var objTmp = new Object();
    objTmp.value = "1";
    objTmp.text = "Gjithmone";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "2";
    objTmp.text = "Debi";
    arrayOptions[1] = objTmp;
    objTmp = new Object();
    objTmp.value = "3";
    objTmp.text = "Kredi";
    arrayOptions[2] = objTmp;
    if (arrayReadOnlyKolonaSubGrides[3] === 'True') {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaSubGrides[3], hfState.Get('lastselgrid') + idRresht, changeKodi, arrayOptions, disabled);
}

//krijon elementin combo per shenjen kur reshti editohet
function myelemComboShenja(value) {
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    var disabled = false;
    var arrayOptions = new Array(2);
    var objTmp = new Object();
    objTmp.value = "1";
    objTmp.text = "Pozitive";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "2";
    objTmp.text = "Negative ";
    arrayOptions[1] = objTmp;

    if (arrayReadOnlyKolonaSubGrides[4] === 'True') {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaSubGrides[4], hfState.Get('lastselgrid') + idRresht, null, arrayOptions, disabled);
}

function renditKolonatGrides() {
    jQuery("#ASPxPageControl1_rowed5").remapColumns(arrayRenditjeKolonaGrides);
}

function renditKolonatSubGrides() {
    jQuery("#ASPxPageControl1_rowed5").remapColumns(arrayRenditjeKolonaSubGrides);
}

function selectFunc(event, ui, emerfushe, id, kod) { //po
    var idRresht = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    if (id !== undefined && kod !== undefined && id !== "" && kod !== "") {
        $(emerfushe).val(kod);
        KtheVleraKPF(id);
        return false;
    }
    var lloji = $('#cmbLloji' + hfState.Get('lastselgrid') + idRresht);
    if (ui.item !== null) {
        $(emerfushe).val(ui.item.label);
        if (lloji.val() === "3") KtheVleraKPF(ui.item.value);
        else if (lloji.val() === "2") { //Llogar
            KtheVleraLlogMeID(ui.item.value);
        }
        return false;
    }
}
function changeFunc(event, ui, emerKodi, index) {//po
    var emerfushe = '#' + emerKodi + index;
    if (ui === null || ui.item === null) {
        var lloji = $('#cmbLloji' + index);
        if ($(emerfushe).val() !== "") {
            if (lloji.val() === "3") { //Artikull                 
                if (event.type === 'change') {
                    ktheVleraKPFMeKod($(emerfushe).val());
                }
                return;
            }
            if (lloji.val() === "2") { //Llogari
                KtheVleraLlogMeKod($(emerfushe).val());
                return;
            }
        }
    }
}

function KtheVleraKPF(idja) {//po
    var idRow = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraKPF"),
        data: JSON.stringify({ idja: idja, rreshti: idRow })
    }).done(SucceededCallbackKPF);
}
function ktheVleraKPFMeKod(kodi) {//po
    var idRow = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraKPFMeKod"),
        data: JSON.stringify({ kodi: kodi, rreshti: idRow, idNdermarrje: hfState.Get("_idNdermarrje"), idPerdoruesi: hfState.Get("_idPerdoruesi") })
    }).done(SucceededCallbackKPF);
}

function KtheVleraLlogMeID(idja) {//po
    var idRow = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogMeIDRow"),
        data: JSON.stringify({ idja: idja, rreshti: idRow })
    }).done(SucceededCallbackLlog);
}
function KtheVleraLlogMeKod(kodi) {//po
    var idRow = $('#' + hfState.Get('lastselgrid')).getLastSel2();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraLlogMeKodRow"),
        data: JSON.stringify({ kodi: kodi, rreshti: idRow, idNdermarrje: hfState.Get("_idNdermarrje") })
    }).done(SucceededCallbackLlog);
}

function SucceededCallbackKPF(result) {//po
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRow = grida.getLastSel2();
    if (result !== null) {
        var KPF = result[1];
        var kodi = "#txtKodi" + hfState.Get('lastselgrid') + idRow;
        var emerArt = '#' + 'txtPershkrimiLlogaria' + hfState.Get('lastselgrid') + idRow;
        if (KPF === null || KPF === undefined || KPF.IdKPF === -1) {
            myMesazh.ShtoMesazhGabimi("Kjo llogari standarte nuk ekziston!");
            grida.setTekstQelize('txtKodi', idRow, '', false, hfState.Get('lastselgrid') + idRow);
            grida.setTekstQelize('txtPershkrimiLlogaria', idRow, '', false, hfState.Get('lastselgrid') + idRow);
            return;
        }
        grida.setTekstQelize('txtKodi', idRow, KPF.KodiKPF, false, hfState.Get('lastselgrid') + idRow);
        grida.setTekstQelize('txtPershkrimiLlogaria', idRow, KPF.EmertimiKPF, false, hfState.Get('lastselgrid') + idRow);

        var newid = merrNewId(hfState.Get('lastselgrid'), idRow);
        lastprindi = merrPershkrimPrindi(hfState.Get('lastselgrid'));
        var coltrupsipasllojit = JSLINQ(colTrupat)
                     .Where(function (item) { return item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && item.PershkrimiZerit == merrPershkrimPrindi(hfState.Get('lastselgrid')) && item.NiveliZerit == (gjenNivel(hfState.Get('lastselgrid')) - 1) })
                    .Select(function (item) { return item; });
        var colLlogarigrida = coltrupsipasllojit.First().OColLlogarite;
        if (colLlogarigrida.length <= newid) {

            newid = colLlogarigrida.length;
        }
        eksistonLLogNeKetePrind(merrLlojZeri(hfState.Get('lastselgrid')), lastprindi, newid, KPF.KodiKPF, "Llogari standarte", $("#cmbGjendja" + hfState.Get('lastselgrid') + idRow + " option:selected").text());

        return;
    }
}

function SucceededCallbackLlog(result) {//po
    var grida = $('#' + hfState.Get('lastselgrid'));
    var idRresht = grida.getLastSel2();
    if (result !== null) {
        var llogaria = result[1];
        var kodi = "#txtKodi" + hfState.Get('lastselgrid') + idRresht;
        var emerArt = '#' + 'txtPershkrimiLlogaria' + hfState.Get('lastselgrid') + idRresht;
        if (llogaria === null || llogaria === undefined || llogaria.IdLlogari === -1) {
            myMesazh.ShtoMesazhGabimi("Kjo llogari nuk eksiton!");
            grida.setTekstQelize('txtKodi', idRresht, '', false, hfState.Get('lastselgrid') + idRresht);
            grida.setTekstQelize('txtPershkrimiLlogaria', idRresht, '', false, hfState.Get('lastselgrid') + idRresht);

            return;
        }
        if (llogaria !== null && llogaria.IdLlogari !== -1) {
            grida.setTekstQelize('txtKodi', idRresht, llogaria.NrLlogari, false, hfState.Get('lastselgrid') + idRresht);
            grida.setTekstQelize('txtPershkrimiLlogaria', idRresht, llogaria.EmerLlogari1, false, hfState.Get('lastselgrid') + idRresht);

            lastprindi = merrPershkrimPrindi(hfState.Get('lastselgrid'));
            var newid = merrNewId(hfState.Get('lastselgrid'), idRresht);
            lastprindi = merrPershkrimPrindi(hfState.Get('lastselgrid'));
            var coltrupsipasllojit = JSLINQ(colTrupat)
                     .Where(function (item) { return item.LlojiZerit == merrLlojZeri(hfState.Get('lastselgrid')) && item.PershkrimiZerit == merrPershkrimPrindi(hfState.Get('lastselgrid')) && item.NiveliZerit == (gjenNivel(hfState.Get('lastselgrid')) - 1) })
                    .Select(function (item) { return item; });
            var colLlogarigrida = coltrupsipasllojit.First().OColLlogarite;
            if (colLlogarigrida.length <= newid) {

                newid = colLlogarigrida.length;
            }
            eksistonLLogNeKetePrind(merrLlojZeri(hfState.Get('lastselgrid')), lastprindi, newid, llogaria.NrLlogari, "Llogari", $("#cmbGjendja" + hfState.Get('lastselgrid') + idRresht + " option:selected").text());
            return;
        }
    }
}
function merrNewId(gride, id) {
    var datarow = $('#' + gride).getDataIDs();
    for (var i = 0; i < datarow.length; i++)
        if (datarow[i] == id)
            return i;

}
//perdoret per te mbushur griden me te dhenat e ruajtura tek hidden fieldi per llogarite

function mbushSubGrideNgaHiddenFieldi(pershk, grida) {
    var be;
    var grid = $('#' + hfState.Get('lastselgrid'));
    var idRresht = 1;
    if (colTrupat.length > 0) {
        grid.setLastSel2(1);
        $("#" + grida).jqGrid('clearGridData');
        var colLlogari = JSLINQ(colTrupat)
                   .Where(function (item) { return item.PershkrimiZerit == pershk && item.LlojiZerit == merrLlojZeri(grida); })
                   .Select(function (item) { return item.OColLlogarite; });
        for (var i = 0; i < colLlogari.ElementAt(0).length; i++) {
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True')
                be = myJQGrid.myValueButtonFshi(true, idRresht, grida, true);
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, grida, true);

            var lloj = (colLlogari.ElementAt(0)[i].Lloji == undefined) ? "" : colLlogari.ElementAt(0)[i].Lloji;
            var kod = (colLlogari.ElementAt(0)[i].IdLlogaria == undefined) ? "" : colLlogari.ElementAt(0)[i].IdLlogaria;
            var emertimi = (colLlogari.ElementAt(0)[i].Emertimi == undefined) ? "" : colLlogari.ElementAt(0)[i].Emertimi;
            var gjen = (colLlogari.ElementAt(0)[i].Gjendja == undefined) ? "" : colLlogari.ElementAt(0)[i].Gjendja;
            var shenj = (colLlogari.ElementAt(0)[i].Shenja == undefined) ? "" : colLlogari.ElementAt(0)[i].Shenja;
            var bij = (colLlogari.ElementAt(0)[i].ShfaqBij == undefined) ? "" : (colLlogari.ElementAt(0)[i].ShfaqBij == true) ? "Po" : "Jo";

            var datarow = { cmbLloji: lloj, txtKodi: kod, txtPershkrimiLlogaria: emertimi, cmbGjendja: gjen, cmbShenja: shenj, cbShfaqBijSub: bij, txtFshi: be };

            if (kod !== "") {
                $("#" + grida).addRowData(parseInt(idRresht), datarow);
                idRresht = idRresht + 1;
                $("#" + grida).setLastSel2(idRresht);
            }
        }
        be = "<input id='butonFshi" + grida + idRresht + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + idRresht + ")' onmouseout='ndryshoImazhin(0," + idRresht + ")'  src='images/square-icon.png' onclick='fshiClicked(" + "&quot;" + grida + "&quot;" + "," + idRresht + ")'/>";
        datarow = { txtFshi: be };
        $("#" + grida).addRowData(parseInt(idRresht), datarow);
    }
}
function Active_TabChanged(s, e) {
    if (PageControl.GetActiveTab().index == 6)
        GetRowValuesZeratBuxheti();

    indexModifiko = gvKonfigPASH.GetFocusedRowIndex();
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