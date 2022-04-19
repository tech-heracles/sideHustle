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
    myMenu.aplikoFiltra(s, e, gvSkema, "178", cmbKonfigurimi.GetText());
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
    else {
        gvSkema.GetRowValues(indexModifiko, 'IdKoka;Kodi;Emertimi;Nr;Njoftim;Formula;DergoEmailPasAprovimitFinal;EmailAprovimi', OnGetRowValuesMod);
        Utils.shfaqLoadingGif();;
    }
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    txtKodi.SetText(values[1]);
    txtEmertimi.SetText(values[2]);
    txtNr.SetText(values[3]);
    cmbFormula.SetValue(values[5]);
    cbNjoftim.SetChecked(values[4]);
    cbDergo.SetChecked(values[6]);
    txtEmail.SetText(values[7]);
    txtEmail.SetEnabled(cbDergo.GetChecked());
    gvTrupi.PerformCallback(values[0] + ";modifiko");

    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "178", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {

    if (idObjekti.toString() !== $('#hfId').val()) {
        if (gvTrupi.InCallback())
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
    txtEmertimi.SetText('');
    txtNr.SetText('0');
    cmbFormula.SetSelectedIndex(0);
    cbNjoftim.SetChecked(false);
    cbDergo.SetChecked(false);
    txtEmail.SetText('');
    gvTrupi.PerformCallback(-1 + ";shto");

    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

function gvTrupiBeginCallback(s, e) {
    Utils.shfaqLoadingGif();;
    merrTeDhena();
}

function gvTrupiEndCallback(s, e) {
    Utils.hiqLoadingGif();;
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
        var arrTabela = ['tblSkema'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
    // $("#dvAktiviteti").show();//$("#dvAktiviteti")[0].style.visibility = 'visible';
}

function LupaKontrollet(kontrollet, colAtrTrupi) {

}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    if (gvTrupi.InCallback())
        Utils.shfaqLoadingGif();;
    txtEmail.SetEnabled(cbDergo.GetChecked());
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("178", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("178", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_SkemaWorkFlow.aspx', 0, hf);
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvSkema, "178", pastrofusha, hfTeDrejta);
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
var arrPerdoruesi = new Array();
var arrLloji = new Array();
var arrNiveli = new Array();
var arrVleraLimit = new Array();
var arrPaLimit = new Array();
var arrEmail = new Array();
var arrDelegimi = new Array();
var editorPerdoruesi;
var editorLloji;
var editorNiveli;
var editorVleraLimit;
var editorPaLimit;
var editorEmail;
var editorDelegimi;


var indexCounter = 0;

function merrTeDhena() {//merren te dhenat qe ka grida
    arrPerdoruesi = new Array();
    arrLloji = new Array();
    arrNiveli = new Array();
    arrVleraLimit = new Array();
    arrPaLimit = new Array();
    arrEmail = new Array();
    arrDelegimi = new Array();
    arrLlojGrupimi = new Array();
    arrGrupkf = new Array();
    arrNivelApr = new Array();
    arrDite = new Array();
    arrPershkrimi = new Array();
    var hidField1 = $("#hfPerdoruesi"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidfield2 = $("#hfLloji");
    var hidField3 = $("#hfNiveli");
    var hidField4 = $("#hfVleraLimit");
    var hidField5 = $("#hfPaLimit");
    var hidField6 = $("#hfEmail");
    var hidField7 = $("#hfDelegimi");
    var hidField8 = $("#hfLlojGrupimi");
    var hidField9 = $("#hfGrupKf");
    var hidField10 = $("#hfNiveliApr");
    var hidField11 = $("#hfDite");
    var hidFiled31 = $("#hfPershkrimi")
    for (i = 0; i < gvTrupi.cpNoRows; i++) {
        arrPerdoruesi[i] = Utils.ktheKontroll('Perdoruesi' + i).GetText();
        arrLloji[i] = Utils.ktheKontroll('Lloji' + i).GetValue();
        arrNiveli[i] = Utils.ktheKontroll('Niveli' + i).GetText();
        arrPershkrimi[i] = Utils.ktheKontroll('Pershkrimi' + i).GetText();
        try {
            arrVleraLimit[i] = Utils.ktheKontroll('VleraLimit' + i).GetText();
        }
        catch (epx) {
            arrVleraLimit[i] = 0;
        }
        arrPaLimit[i] = Utils.ktheKontroll('Modifiko' + i).GetChecked();
        arrEmail[i] = Utils.ktheKontroll('Email' + i).GetText();
        arrDelegimi[i] = Utils.ktheKontroll('Deleguesi' + i).GetText();
        try {
            arrLlojGrupimi[i] = Utils.ktheKontroll('LlojiGrupi' + i).GetValue();
        }
        catch (epx) {
            arrLlojGrupimi[i] = 1;
        }
        try {
            arrGrupkf[i] = Utils.ktheKontroll('Grupi' + i).GetText();
        }
        catch (exp) {
            arrGrupkf[i] = "";
        }
        try {
            arrNivelApr[i] = Utils.ktheKontroll('NiveliApr' + i).GetValue();
        }
        catch (exp) {
            arrNivelApr[i] = 0;
        }
        arrDite[i] = Utils.ktheKontroll('Dite' + i).GetValue();
    }
    hidField1.val(JSON.stringify(arrPerdoruesi));
    hidfield2.val(JSON.stringify(arrLloji));
    hidField3.val(JSON.stringify(arrNiveli));
    hidField4.val(JSON.stringify(arrVleraLimit));
    hidField5.val(JSON.stringify(arrPaLimit));
    hidField6.val(JSON.stringify(arrEmail));
    hidField7.val(JSON.stringify(arrDelegimi));
    hidField8.val(JSON.stringify(arrLlojGrupimi));
    hidField9.val(JSON.stringify(arrGrupkf));
    hidField10.val(JSON.stringify(arrNivelApr));
    hidField11.val(JSON.stringify(arrDite));
    hidFiled31.val(JSON.stringify(arrPershkrimi))
}  //kontrollon nese jemi ne rreshtin e fundit
function KeyPressPerdoruesi(kodi, editor, key) {
    editorLloji = Utils.ktheKontroll('Lloji' + key);
    var vler = Utils.ktheKontroll(editor).GetInputElement().value;

    tekstiShkruar = vler;
    if (editorLloji.GetValue() == 1) {
        indeksiArtPerb = key;
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullOseMakro2"),
            data: JSON.stringify({ prefixText: vler, count: 0, contextKey: "Perdorues", klasa: 0 })
        }).done(SucceededCallbackKodi);
    }
    else if (editorLloji.GetValue() == 2) {
        indeksiArtPerb = key;
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullOseMakro2"),
            data: JSON.stringify({ prefixText: vler, count: 0, contextKey: "Rol", klasa: 0 })
        }).done(SucceededCallbackKodi);
    }


    if (event.keyCode == 13) {
        editorKodi = editor;
        editorKodi = document.getElementById(editor);
        editorEmail = Utils.ktheKontroll('Email' + key);
        keyGlobal = key;
        if (editorLloji != null) {
            if (editorLloji.GetSelectedItem().text == "Perdorues") {
                myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPerdoruesin"), 'LupaPerdorues.aspx?vjenNga=Perdoruesi', 600, 560);
            }
            else if (editorLloji.GetSelectedItem().text == "Rol") {
                myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhRolin"), 'LupaRole.aspx?vjenNga=Perdoruesi', 600, 560);
            }
        }
        event.returnValue = false;
        event.cancel = true;
    }

}

function callWebservice(name, key) {
    merrTeDhena()
    editorLloji = Utils.ktheKontroll('Lloji' + key);
    if (editorLloji.GetValue() == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheEmailPerdoruesi"),
            data: JSON.stringify({ prefixText: name, perdorues: arrPerdoruesi, lloji: arrLloji, key: key })
        }).done(SucceededCallbackPerdoruesiemail);
    }
    else {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "kontrolloKaAdresaroli"),
            data: JSON.stringify({ prefixText: name, perdorues: arrPerdoruesi, lloji: arrLloji, key: key })
        }).done(SucceededCallbackRoli);
    }
}

function SucceededCallbackPerdoruesiemail(result) {
    editorEmail = Utils.ktheKontroll('Email' + indeksPerEmerLlogarie);
    editorPerdoruesi = Utils.ktheKontroll('Perdoruesi' + indeksPerEmerLlogarie);
    if (result[0] != null && result[1] == "") {
        if (result[0].PerdoruesEmail == "" && cbNjoftim.GetChecked())
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerdoruesEmail"));
        TextChangedEmail(editorEmail, indeksPerEmerLlogarie);
        editorEmail.SetText(result[0].PerdoruesEmail);
    }
    else if (result[0] != null && result[1] != "") {
        editorEmail.SetText('');
        editorPerdoruesi.SetText('');
        myMesazh.ShtoMesazhGabimi(result[1]);
    }

    if (result[2] != null && result[2] != "") {

        myMesazh.ShtoMesazhGabimi(result[2].toString());
        editorPerdoruesi.SetText('');
    }
    if (indeksPerEmerLlogarie == gvTrupi.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }


}
function SucceededCallbackRoli(result) {
    editorEmail = Utils.ktheKontroll('Email' + indeksPerEmerLlogarie);
    editorPerdoruesi = Utils.ktheKontroll('Perdoruesi' + indeksPerEmerLlogarie);
    if (result[0] != null) {
        if (result[1] == true && cbNjoftim.GetChecked())
            myMesazh.ShtoMesazhGabimi(result[2]);
        else if (result[1] == false && result[2] != "")
        { editorPerdoruesi.SetText(''); myMesazh.ShtoMesazhGabimi(result[2]); }
    }
    else { editorPerdoruesi.SetText(''); myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyRolNukEkziston")); }
    if (indeksPerEmerLlogarie == gvTrupi.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }

}
//kontrollon nese jemi ne rreshtin e fundit
function ButtonClickedPerdoruesi(editor, key) {

    editorKodi = editor;
    editorLloji = Utils.ktheKontroll('Lloji' + key);
    editorEmail = Utils.ktheKontroll('Email' + key);
    keyGlobal = key;
    if (editorLloji != null) {
        if (editorLloji.GetSelectedItem().text == "Perdorues") {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPerdoruesin"), 'LupaPerdorues.aspx?vjenNga=Perdoruesi', 600, 560);
        }
        else if (editorLloji.GetSelectedItem().text == "Rol") {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhRolin"), 'LupaRole.aspx?vjenNga=Perdoruesi', 600, 560);
        }
    }

}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedPerdoruesi(editor, key) {
    var value = Utils.ktheKontroll(editor).GetText(); indeksPerEmerLlogarie = key;
    if (value != '')
        callWebservice(value, key);
    else {
        Utils.ktheKontroll('Email' + key).SetText('');

    }

    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1; merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
function KeyPressDeleguesi(kodi, editor, key) {

    if (kodi === 13) {
        editorKodi = editor;

        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPerdoruesin"), 'LupaPerdorues.aspx?vjenNga=Deleguesi', 600, 560);
    }
}

//kontrollon nese jemi ne rreshtin e fundit
function ButtonClickedDeleguesi(editor, key) {

    editorKodi = editor;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhRolin"), 'LupaPerdorues.aspx?vjenNga=Deleguesi', 600, 560);

}
var identifikuesPerPopupKodifikimin;
var keyGlobal;
function ButtonClickedGrupi(editor, key) {
    identifikuesPerPopupKodifikimin = "Skema"
    editorGlobal = editor;
    keyGlobal = key;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Grupin e klientit', 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + Utils.ktheKontroll('LlojiGrupi' + key).GetValue() + '&kf=klient', 600, 560);

}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedDeleguesi(editor, key) {
    var deleguesi = editor.GetText();
    if (deleguesi == Utils.ktheKontroll('Perdoruesi' + key).GetText() && Utils.ktheKontroll('Lloji' + key).GetValue() == 1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPunonjesJoNjejtePerDelegim"));
        editor.SetText('');
        return;
    }
    merrTeDhena();
    indeksPerEmerLlogarie = key;
    if (deleguesi != "")
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheEmailPerdoruesi"),
            data: JSON.stringify({ prefixText: deleguesi, perdorues: arrPerdoruesi, lloji: arrLloji, key: key })
        }).done(SucceededCallbackDelegues);
    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvTrupi.PerformCallback();
    }
}
function SucceededCallbackDelegues(result) {
    editorPerdoruesi = Utils.ktheKontroll('Deleguesi' + indeksPerEmerLlogarie);
    if (result[0] != null) {
        if (result[0].PerdoruesEmail == "" && cbNjoftim.GetChecked())
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDeleguesEmail"));
    }
    else { editorPerdoruesi.SetText(''); myMesazh.ShtoMesazhGabimi(hfState.Get("msgDelegues")); }
    if (indeksPerEmerLlogarie == gvTrupi.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }

}
function TextChangedLloji(editor, field, key) {
    keyGlobal = key;
    editorLloji = editor;
    editorPerdoruesi = Utils.ktheKontroll('Perdoruesi' + key);
    editorEmail = Utils.ktheKontroll('Email' + key);
    editorEmail.SetText('');
    if (editorLloji.GetValue() == 1)
        editorEmail.SetEnabled(true);
    else editorEmail.SetEnabled(false);
    editorPerdoruesi.SetText(''); Utils.ktheKontroll('Perdoruesi' + key).ClearItems();
    if (key === gvTrupi.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1; merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
function TextChangedLlojiGrupi(editor, field, key) {
    keyGlobal = key;
    Utils.ktheKontroll(field).SetText('');
    if (parseFloat(Utils.ktheKontroll('VleraLimit' + key).GetText()) == 0) {
        Utils.ktheKontroll('NiveliApr' + key).SetEnabled(false);
        Utils.ktheKontroll('NiveliApr' + key).SetSelectedIndex(0);
    }
    if (key === gvTrupi.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1; merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
function TextChangedNiveli(editor, key) {
    Utils.ktheKontroll('NiveliApr' + key).ClearItems();
    Utils.ktheKontroll('NiveliApr' + key).AddItem('Aprovuar', 0);
    for (i = parseFloat(Utils.ktheKontroll('Niveli' + key).GetValue()) + 1; i <= 20; i++) {
        Utils.ktheKontroll('NiveliApr' + key).AddItem(i + '', i);
    }
    Utils.ktheKontroll('NiveliApr' + key).SetSelectedIndex(0);
    if (key === gvTrupi.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1; merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    gvTrupi.PerformCallback(key);
}


var tekstiShkruar = "";

function SucceededCallbackKodi(result) {


    var cmbKodi = Utils.ktheKontroll('Perdoruesi' + indeksiArtPerb);
    var cmbLloji = Utils.ktheKontroll('Lloji' + indeksiArtPerb)
    cmbKodi.BeginUpdate();
    cmbKodi.ClearItems();
    if (result != null && result.length != 0) {
        for (i = 0; i < result.length; i++) {
            if (cmbLloji.GetValue() == 1)
                cmbKodi.AddItem([result[i].PerdoruesUsername, result[i].EmriPerdorues + " " + result[i].MbiemriPerdorues], result[i].IdPerdorues); //AddItem(kodi, id);
            else cmbKodi.AddItem([result[i].KodRoli, result[i].PershkrimRoli], result[i].IdRoli); //AddItem(kodi, id); 
        }
    }
    cmbKodi.EndUpdate();
    if (cmbLloji.GetValue() != 0)
        cmbKodi.SetText(tekstiShkruar);
    else cmbKodi.SetText('');
    cmbKodi.ShowDropDown();

}

function TextChangedVleraLimit(editor, key) {
    if (isNaN(editor.GetText()) || editor.GetText() == "") {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraLimitNumer"));
        editor.SetText(0);
    }
    if (parseFloat(editor.GetText()) != 0) {
        Utils.ktheKontroll('NiveliApr' + key).SetEnabled(true);

    }
    else if (Utils.ktheKontroll('Grupi' + key).GetText() == "") {
        Utils.ktheKontroll('NiveliApr' + key).SetEnabled(false);
        Utils.ktheKontroll('NiveliApr' + key).SetSelectedIndex(0);
    }
}
function TextChangedGrupi(editor, key) {
    Utils.ktheKontroll('Grupi' + key).SetText('')
    if (parseFloat(Utils.ktheKontroll('VleraLimit' + key).GetText()) == 0) {
        Utils.ktheKontroll('NiveliApr' + key).SetEnabled(false);
        Utils.ktheKontroll('NiveliApr' + key).SetSelectedIndex(0);
    }
}

function TextChangedEmail(editor, key) {
    if (editor.GetText() != "") {
        var emailRegEx = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,9}$/i;
        if (editor.GetText().search(emailRegEx) == -1) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAdresaEmailEPavlefshme"));
        }

    }

}
function Active_TabChanged(s, e) {
    indexModifiko = gvSkema.GetFocusedRowIndex();
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
function TextChangedVleraDite(editor, key) {
    if (isNaN(editor.GetText()) || editor.GetText() == "") {
        editor.SetFocus();
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDitetDuhenNumerike"));
        editor.SetText(0);
    }
    if (parseFloat(editor.GetText()) != 0) {
        Utils.ktheKontroll('NiveliApr' + key).SetEnabled(true);

    }
    else if (Utils.ktheKontroll('Grupi' + key).GetText() == "") {
        Utils.ktheKontroll('NiveliApr' + key).SetEnabled(false);
        Utils.ktheKontroll('NiveliApr' + key).SetSelectedIndex(0);
    }
}