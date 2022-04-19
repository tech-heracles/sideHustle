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
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKokaKategoriZbritje, "417", cmbKonfigurimi.GetText());
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
    $(window).on('load', function () {
        Init();
    });
});

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKokaKategoriZbritje, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKokaKategoriZbritje, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKokaKategoriZbritje, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKokaKategoriZbritje, indexSel);
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
    //            myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name == 'Ruaj')
        merrTeDhena(s, e);

}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvKokaKategoriZbritje.GetFocusedRowIndex();
    if (indexModifiko == -1)
        //        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje Kategori Zbritje!');
        myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgZgjidhKategori"));
    else
        gvKokaKategoriZbritje.GetRowValues(indexModifiko, 'IdKokaKategoriZbritje;KodKategoriZbritje;PershkrimKategoriZbritje;Zbritja;IdMonedha', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi2.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi2.SetEnabled(false);
    }
    txtPershkrimi2.SetText(values[2]);
    txtZbritja.SetText(values[3]);
    cmbMonedha.SetValue(values[4]);
    lista = true;
    gvTrupiKategoriZbritje.PerformCallback(indexModifiko + ':' + $('#hfShtimModifikim').val());

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarje = hfState.Get('idNdermarje');

    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "417", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarje, idGjuha: idGjuha })
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
    txtKodi2.SetText('');
    txtPershkrimi2.SetText('');
    txtZbritja.SetText('0.00');
    cmbMonedha.SetSelectedIndex(0);
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    $("#hfNrRreshta")[0].value = '';
    gvTrupiKategoriZbritje.PerformCallback("-1:" + $('#hfShtimModifikim').val());
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
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    gvKokaKategoriZbritje.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvKategoria").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({       
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblNenkategoria'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
    //$("#dvKategoria").show();
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("417", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("417", cmbKonfigurimi.GetText());
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_KategoriZbritje.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi")[0];
    var hfKontrollet = $('#hfKontrollet')[0];
    var hfShtimModifikim = $('#hfShtimModifikim')[0]; //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId')[0];  //hidden fieldi qe ruan id  e rreshtit te selektuar
    //  indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKokaKategoriZbritje, "417")
    if (hf.value == "true") {
        if (hfShtimModifikim.value !== "modifikim") {
            //                    aktivizoFusha(hfKontrollet.value);

            window.mbush = false;
            hfShtimModifikim.value = "shtim"; //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            hfId.value = 0; //hidden fieldi qe ruan id  e rreshtit te selektuar
            indexModifiko = -1; //indexi i reshtit te selektuar      
            pastrofusha();
            hf.value = "false";

            if (PageControl.GetActiveTabIndex() != 0)
            { PageControl.SetActiveTabIndex(1); myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim')); }
            SucceededCallbackKonfig(resultkonf);

            gvKokaKategoriZbritje.PerformCallback("417" + ";" + cmbKonfigurimi.GetText());
            gvKokaKategoriZbritje.ClearFilter();
        }
        else {
            hf.value = "false";
            gvKokaKategoriZbritje.PerformCallback("417" + ";" + cmbKonfigurimi.GetText());
            gvKokaKategoriZbritje.ClearFilter();
            mbushfusha();
            PageControl.SetActiveTabIndex(1); myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));

        }
    }
    else myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
    Utils.hiqLoadingGif();;

}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
//pastron fushat
function Init() {
    changeName();

}
var arrDateFillimi = new Array();
var arrDateMbarimi = new Array();
var arrVleraMin = new Array();
var arrVleraMax = new Array();
var arrLloji = new Array();
var arrZbritja = new Array();
var cou1 = 0;

var indeksi = -1;
var editorDateFillimi;
var editorDateMbarimi;
var editorVleraMin;
var editorVleraMax;
var editorLloji;
var editorZbritja;
var indexCounter = 0;
function merrTeDhena(s, e) {//merren te dhenat qe ka grida
    arrDateFillimi = new Array();
    arrDateMbarimi = new Array();
    arrVleraMin = new Array();
    arrVleraMax = new Array();
    arrLloji = new Array();
    arrZbritja = new Array();
    arrPrioriteti = new Array();
    cou1 = 0;
    indexCounter = 0;
    var hidField1 = $("#hfDateFillimi")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidfield2 = $("#hfDateMbarimi")[0];
    var hidField3 = $("#hfVleraMin")[0];
    var hidField4 = $("#hfVleraMax")[0];
    var hidField5 = $("#hfLloji")[0];
    var hidField6 = $("#hfZbritja")[0];
    var hidField7 = $("#hfPrioriteti")[0];
    var vleratRreshtit = "";
    var arrRreshtat = new Array();

    for (i = 0; i < gvTrupiKategoriZbritje.cpNoRows; i++) {
        editorDateFillimi = Utils.ktheKontroll('dteDateFillimi' + i);
        editorDateMbarimi = Utils.ktheKontroll('dteDateMbarimi' + i);
        editorVleraMin = Utils.ktheKontroll('txtVleraMin' + i);
        editorVleraMax = Utils.ktheKontroll('txtVleraMax' + i);
        editorLloji = Utils.ktheKontroll('cmbLloji' + i);
        editorZbritja = Utils.ktheKontroll('txtZbritja' + i);
        editorPrioriteti = Utils.ktheKontroll('cmbPrioriteti' + i);
        if (editorDateFillimi.GetValue() != null)
            arrDateFillimi[cou1] = i.toString() + ":" + myFormatDate.formatDate(editorDateFillimi.GetValue(), 'dd/MM/yyyy');
        else
            arrDateFillimi[cou1] = i.toString() + ":";
        if (editorDateMbarimi.GetValue() != null)
            arrDateMbarimi[cou1] = i.toString() + ":" + myFormatDate.formatDate(editorDateMbarimi.GetValue(), 'dd/MM/yyyy');
        else
            arrDateMbarimi[cou1] = i.toString() + ":";
        arrVleraMin[cou1] = i.toString() + ":" + editorVleraMin.GetText();
        arrVleraMax[cou1] = i.toString() + ":" + editorVleraMax.GetText();
        arrLloji[cou1] = i.toString() + ":" + editorLloji.GetValue();
        arrZbritja[cou1] = i.toString() + ":" + editorZbritja.GetText();
        arrPrioriteti[cou1] = i.toString() + ":" + editorPrioriteti.GetValue();
        if (editorDateFillimi.GetValue() != null && editorDateMbarimi.GetValue() != null) {
            var vleratRreshtitTeRi = myFormatDate.formatDate(editorDateFillimi.GetValue(), 'dd/MM/yyyy') + myFormatDate.formatDate(editorDateMbarimi.GetValue(), 'dd/MM/yyyy') + editorVleraMin.GetText() + editorVleraMax.GetText();

            for (j = 0; j < arrRreshtat.length; j++) {
                if (arrRreshtat[j] == vleratRreshtitTeRi) {
                    //                    myMesazh.ShtoMesazhGabimi("Nuk lejohen dy rreshta njelloj");
                    myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgRreshtaNjelloj"));
                    e.processOnServer = false;
                }
            }

            vleratRreshtit = myFormatDate.formatDate(editorDateFillimi.GetValue(), 'dd/MM/yyyy') + myFormatDate.formatDate(editorDateMbarimi.GetValue(), 'dd/MM/yyyy') + editorVleraMin.GetText() + editorVleraMax.GetText();
            arrRreshtat[cou1] = vleratRreshtit;
        }
        //cou1 += 1;
        cou1 = cou1 + 1;
    }

    hidField1.value = arrDateFillimi;
    hidfield2.value = arrDateMbarimi;
    hidField3.value = arrVleraMin;
    hidField4.value = arrVleraMax;
    hidField5.value = arrLloji;
    hidField6.value = arrZbritja;
    hidField7.value = arrPrioriteti;
}
function TextChangedPrioriteti(editor, key) {
    var hidField1 = $("#hfNrRreshta")[0];
    var hidField = $("#hfPrioritetiPara")[0];
    var vlerapara = 0;
    var listefushash = hidField.value.split(",");
    for (i = 0; i < hidField1.value; i++) {
        var liste = listefushash[i].split(":");
        if (liste[0] == key) {

            vlerapara = liste[1];
        }
    }
    editorPrioriteti = Utils.ktheKontroll('cmbPrioriteti' + key);
    var vlerapas = editorPrioriteti.GetValue();
    if (vlerapara < vlerapas) {

        for (i = 0; i < hidField1.value; i++) {
            if (i != key) {
                editorPrioriteti = Utils.ktheKontroll('cmbPrioriteti' + i);
                if (editorPrioriteti.GetValue() <= vlerapas && editorPrioriteti.GetValue() > vlerapara)
                    editorPrioriteti.SetValue(parseFloat(editorPrioriteti.GetValue()) - 1);
            }
        }


    }
    else if (vlerapara > vlerapas) {

        for (i = 0; i < hidField1.value; i++) {
            if (i != key) {
                editorPrioriteti = Utils.ktheKontroll('cmbPrioriteti' + i);
                if (editorPrioriteti.GetValue() >= vlerapas && editorPrioriteti.GetValue() < vlerapara)
                    editorPrioriteti.SetValue(parseFloat(editorPrioriteti.GetValue()) + 1);
            }
        }
    }
    ruajPrioritete();
    if (key == gvTrupiKategoriZbritje.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupiKategoriZbritje.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedDateFillimi(editor, key) {
    editorDateFillimi = Utils.ktheKontroll('dteDateFillimi' + key);
    editorDateMbarimi = Utils.ktheKontroll('dteDateMbarimi' + key);
    //alert(editorDateMbarimi.GetDate());
    if (editorDateMbarimi.GetDate() != null && editorDateFillimi.GetDate() > editorDateMbarimi.GetDate()) {
        //        myMesazh.ShtoMesazhGabimi('Data e fillimit nuk mund te jete me e madhe se data e mbarimit');
        myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgDataGabim"));

        editorDateFillimi.SetDate(editorDateMbarimi.GetDate())
    }
    if (key == gvTrupiKategoriZbritje.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupiKategoriZbritje.PerformCallback();
    }
}

//kontrollon nese jemi ne rreshtin e fundit
function TextChangedDateMbarimi(editor, key) {
    editorDateFillimi = Utils.ktheKontroll('dteDateFillimi' + key);
    editorDateMbarimi = Utils.ktheKontroll('dteDateMbarimi' + key);
    if (editorDateFillimi.GetDate() > editorDateMbarimi.GetDate()) {
        //        myMesazh.ShtoMesazhGabimi('Data e fillimit nuk mund te jete me e madhe se data e mbarimit');
        myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgDataGabim"));
        editorDateMbarimi.SetDate(editorDateFillimi.GetDate())
    }
    if (key == gvTrupiKategoriZbritje.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupiKategoriZbritje.PerformCallback();
    }
}
function TextChangedVleraMin(editor, key) {
    var vleraMin = editor.GetText();
    var vleraMax = window["txtVleraMax" + key].GetText();
    var cmbLlojiValue = window["cmbLloji" + key].GetValue();
    switch (cmbLlojiValue) {
        case "1": //perqindje
            if (isNaN(vleraMin) || parseInt(vleraMin) < 0 || parseInt(vleraMin) > 100) {
                ////        myMesazh.ShtoMesazhGabimi('Vlera Minimum duhet te jete numer.');
                myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgVleraMinimumPerqindje")); //TODO ALDA - "Vlera minimum duhet nga 0 ne 100 per llojin perqindje"
                editor.SetText('0.00');
                return;
            }
            break;
        case "2": //vlere
            if (isNaN(vleraMin) || parseInt(vleraMin) < 0) {
                ////        myMesazh.ShtoMesazhGabimi('Vlera Minimum duhet te jete numer.');
                myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgVleraMinimumVlere"));//TODO ALDA - "Vlera minimum duhet me e madhe se 0 per llojin vlere" //
                editor.SetText('0.00');
                return;
            }
            break;
        default:
            myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgLlojKategorie")); //TODO ALDA - "Lloj i panjohur kategorie" //
            editor.SetText('0.00');
            return;
    }
    
    if (key == gvTrupiKategoriZbritje.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupiKategoriZbritje.PerformCallback();
    }
}
function TextChangedVleraMax(editor, key) {
    var vleraMin = window["txtVleraMin" + key].GetText();
    var vleraMax = editor.GetText();
    var cmbLlojiValue = window["cmbLloji" + key].GetValue();
    switch (cmbLlojiValue) {
        case "1": //perqindje
            if (isNaN(vleraMax) || parseInt(vleraMin) > parseInt(vleraMax) || parseInt(vleraMax) > 100) {
                //        myMesazh.ShtoMesazhGabimi('Vlera Maximum duhet te jete numer.');
                myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgVleraMaximumPerqindje")); //TODO ALDA - "Vlera maximum duhet nga min ne 100 per llojin perqindje"//
                editor.SetText('0.00');
                return;
            }
            break;
        case "2": //vlere
            if (isNaN(vleraMin) || parseInt(vleraMin) > parseInt(vleraMax)) {
                ////        myMesazh.ShtoMesazhGabimi('Vlera Minimum duhet te jete numer.');
                myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgVleraMaximumVlere"));//TODO ALDA - "Vlera maximum duhet me e madhe se min per llojin vlere" //
                editor.SetText('0.00');
                return;
            }
            break;
        default:

            myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgLlojKategorie")); //TODO ALDA - "Lloj i panjohur kategorie" //
            editor.SetText('0.00');
            return;
    }
    
    editorVleraMin = Utils.ktheKontroll('txtVleraMin' + i);
    editorVleraMax = Utils.ktheKontroll('txtVleraMax' + i);


    if (key == gvTrupiKategoriZbritje.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupiKategoriZbritje.PerformCallback();
    }
}
function TextChangedLloji(editor, key) {
    var txtBoxZbritja = "txtZbritja" + key; //nderton variablin ne varesi te rreshtit te grides 
    if (Utils.HiqPresjet((eval(txtBoxZbritja)).GetText()) > 100 && editor.GetText() != "Vlere") {
        myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgZbritjaMaximum100"));
        (eval(txtBoxZbritja)).SetText('0.00');
    }
    if (key == gvTrupiKategoriZbritje.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupiKategoriZbritje.PerformCallback();
    }
}
function TextChangedZbritja(editor, key) {
    var cmbLloji = "cmbLloji" + key; //nderton variablin ne varesi te rreshtit te grides 
    if (isNaN(editor.GetText())) {
        //        myMesazh.ShtoMesazhGabimi('Zbritja duhet te jete numer.');
        myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgZbritjaNumer"));
        editor.SetText('0.00');
    }
    if (parseFloat(editor.GetText()) < 0) {
        //        myMesazh.ShtoMesazhGabimi('Zbritja duhet te jete numer pozitiv.');
        myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgZbritjaPozitive"));
        editor.SetText('0.00');
    }
    if (parseFloat(editor.GetText()) > 100 && (eval(cmbLloji)).GetText() != "Vlere") {
        //        myMesazh.ShtoMesazhGabimi('Zbritja duhet te jete numer pozitiv.');
        myMesazh.ShtoMesazhGabimi(hfMsgZbritje.Get("MsgZbritjaMaximum100"));
        editor.SetText('0.00');
    }
    if (key == gvTrupiKategoriZbritje.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupiKategoriZbritje.PerformCallback();
    }
}
//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    gvTrupiKategoriZbritje.PerformCallback(key);
    var hidField = $("#hfNrRreshta")[0];
    if (key < hidField.value);
    {
        hidField.value = parseFloat(hidField.value) - 1;

    }
}
function enable() {
    var hidField = $("#hfNrRreshta")[0];
    for (i = 0; i < gvTrupiKategoriZbritje.cpNoRows; i++) {
        if (i < hidField.value) {
            editorPrioriteti = Utils.ktheKontroll('cmbPrioriteti' + i);
            editorPrioriteti.SetEnabled(true);
        }
        else {
            editorPrioriteti = Utils.ktheKontroll('cmbPrioriteti' + i);
            editorPrioriteti.SetEnabled(false);
        }
    }


} function ruajPrioritete() {
    var arrayListPri = new Array();
    var count = 0;
    var hidField = $("#hfPrioritetiPara")[0];
    var hidField1 = $("#hfNrRreshta")[0];
    for (i = 0; i < hidField1.value; i++) {

        editorPrioriteti = Utils.ktheKontroll('cmbPrioriteti' + i);

        arrayListPri[count] = i.toString() + ":" + editorPrioriteti.GetValue();
        //count += 1;
        count = count + 1;
    }
    hidField.value = arrayListPri;

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

function activeTabChanged(s, e) {
    indexModifiko = gvKokaKategoriZbritje.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val( 'shtim');
            $('#hfId')[0].value = 0; pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function EndCallback_gvTrupiKategoriZbritje(s, e) {
    if($('#hfShtimModifikim')[0].value=='modifikim' && lista)
    {$('#hfNrRreshta')[0].value=gvTrupiKategoriZbritje.cpNoRows -1; lista=false;}
    enable();
    ruajPrioritete();
	
}