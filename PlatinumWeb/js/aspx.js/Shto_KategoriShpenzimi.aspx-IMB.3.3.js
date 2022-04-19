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
var identifikuesperKPF = "Shto_KategoriShpenzimi.aspx";
var identifikuesperKategoriShpenzimi = "KategoriShpenzimi";
var editorPrind;
var editorNiveli;

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
    $(window).on('load', function () {
        Init();
    });
});

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKategoria, "230", cmbKonfigurimi.GetText());
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKategoria, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKategoria, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKategoria, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKategoria, indexSel);
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
   // myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, true, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name == 'Ruaj') {
        pastro();
    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvKategoria.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje kategori!');
    else
        gvKategoria.GetRowValues(indexModifiko, 'Id;Kodi;Pershkrimi;IdPrindi;NivelKategorie;KodPrindi;PershkrimPrind;NivelPrind;KategoriAktive', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi.SetText(values[1]);
    txtEmertimi.SetText(values[2]);
    if (values[3] != null)
        Utils.SelectComboItem(btneEmertimPrindi, values[3], [values[5].toString(), values[6].toString(), values[4].toString()]);
    else btneEmertimPrindi.SetText('');
    txtNiveli.SetText(values[4]);
    cbKatAktive.SetChecked(values[8]);
    Utils.hiqLoadingGif();;
    if ($('#hfShtimModifikim').val() == 'klonim')
        txtKodi.SetEnabled(true);
    else
        txtKodi.SetEnabled(false);
    //  gvBuxheti.PerformCallback(indexModifiko);
    btneKapitulli.SetValue(null);
    merrBuxhete();
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function merrBuxhete()
{
    $.ajax({
        url: Utils.getServerApiUrl("Celje", "merrBuxhete"), 
        data: JSON.stringify({ idqk: $('#hfId').val(), idNderviti: hfState.Get('idNdermarrjeVit'), idllojbuxheti: 18, idviti: cmbViti.GetValue(), eshteprojekt: cmbLlojBuxheti.GetValue() == 1 }),
        pritPergjigje: true
    }).done(function (result) {
        //some instrutions
        SucceededCallbackBuxhet(result);
    }).fail(function (err) {
        console.log(err);
    });
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    txtKodi.SetEnabled(true);
    btneEmertimPrindi.SetValue(null);
    txtNiveli.SetText('1');
    cmbViti.SetVisible(false);
    lblViti.SetVisible(false);
    cmbLlojBuxheti.SetValue(0);
    gvBuxheti.PerformCallback(-1);
    cmbNdryshimi.ClearItems();
    dteDateAkt.SetDate(new Date());
    btneKapitulli.SetValue(null);
    cbKatAktive.SetChecked(true);

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

    gvKategoria.PerformCallback(idKomp + ";" + kodKonf);
    gvBuxheti.PerformCallback(idKomp + ";" + kodKonf);
}

var resultkonf;
var colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    vendosKonfig(result);
}

function vendosKonfig(result) {
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblKategoria'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C", 1);//  hfLidhur, undefined, false);
    }
}

function LupaKontrollet(kontrollet, colAtrTrupi) {

}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_KategoriShpenzimi.aspx',0, null);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[0]);
    callWebserviceKonfigurimi("230", cmbKonfigurimi.GetText());
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
   // indexModifiko = myFaqeCelje.EndRequestHandlerPas(sender, args, hf, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKategoria, "230", hfTeDrejta)
      indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKategoria, "230", pastrofusha, hfTeDrejta);
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
var arr = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;
//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    arr = new Array();
    counter = 0;
    arr2 = new Array();
    counter2 = 0;
    var hf = $("#hfStatusi")[0];
    hf.value = "false";
    arrVlerat = new Array();
    countvlerat = 0;
}

//buxhetet
function ruajBuxhet() {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidFieldShenime = $("#hfBuxhetiShenime")[0];
    myBuxhet.ruajBuxhet(hidField, hidField2, hidFieldShenime);
}
function ShtoBuxhet1(editor, edgjendja, eddiff, key) {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter = myBuxhet.ShtoBuxhet1(editor, edgjendja, eddiff, key, hidField, counter);
}
function ShtoBuxhet2(editor, edgjendja, eddiff, key) {
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter2 = myBuxhet.ShtoBuxhet2(editor, edgjendja, eddiff, key, hidField2, counter2);
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal1(editor, edgjendja, eddiff) {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter = myBuxhet.ShtoTotal1(editor, edgjendja, eddiff, hidField, counter);
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal2(editor, edgjendja, eddiff) {
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter2 = myBuxhet.ShtoTotal2(editor, edgjendja, eddiff, hidField2, counter2);
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

function activeTabChanged(s, e) {
    indexModifiko = gvKategoria.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim';
            $('#hfId')[0].value = 0; pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function prindClick() {
    editorPrind = btneEmertimPrindi;
    editorNiveli = txtNiveli;
    KategoriShpenzimi_Click();
}

function KategoriShpenzimi_Click() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni kategorine e shpenzimit', 'LupaKategoriShpenzimi.aspx', 680, 600);
}
var resultkonf;
var colKontrollet, colAtrTrupi;


function ndryshoPrindi(s, e) {
    var niveli = s.GetSelectedItem().GetColumnText("NivelKategorie");
    txtNiveli.SetText(parseInt(niveli) + 1);
}

function onChangedViti(s, e) {
    merrBuxhete();
}

function onChangedLlojBuxheti(s, e) {
    lblViti.SetVisible(cmbLlojBuxheti.GetSelectedIndex() != 0);
    cmbViti.SetVisible(cmbLlojBuxheti.GetSelectedIndex() != 0);
    merrBuxhete();
}
function EndCallBackGridaBuxheti(s, e) {
    var vleraKapitullit = gvBuxheti["cpKapitulli"];
    if (vleraKapitullit) {
        if (vleraKapitullit != 0)
            btneKapitulli.SetValue(vleraKapitullit);
        else btneKapitulli.SetValue(null);
        delete gvBuxheti["cpKapitulli"];
    }

}
function SucceededCallbackBuxhet(result) {
    cmbNdryshimi.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbNdryshimi.AddItem(result[i], result[i]); //AddItem(teksti, vlera);
    cmbNdryshimi.SetSelectedIndex(0);
    if (cmbNdryshimi.GetText() !== "")
        dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));
    else dteDateAkt.SetDate(new Date());
    gvBuxheti.PerformCallback();//$('#hfId').val(), dteDateAkt.GetDate() PATI: Po i heq nga parametrat sepse nuk perdoren dhe kur jane null, japin error.
  
}
function ndryshoDate() {
    dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));
    gvBuxheti.PerformCallback();
}
function ButtonClickKapitulliNgaKoka() {
    vjenNgaKokeApoTrup = true;

    myButtonClickLupa.LupaUniversal_Click("Zgjidhni Kapitullin", 'LupaKonfigUrdherPagesa.aspx?vjenNga=Kapitulli&idkonfigambjente=' + 0, 600 , 600);
}
function LostFocusObjekti() {
    
}

function Init() {    
    ndryshoKonfigurimin();
}