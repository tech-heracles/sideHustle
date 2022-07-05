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
var btnFiltrat;
var colKontrollet, colAtrTrupi;
var resultkonf;
var identifikuesPerPopupBanka = "GjendjeArkeDitore";

function changeName() {
    var hf = $("#hfKonfillestar")[0];
    myFaqeCelje.changeName('GjendjeArkeDitore.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
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
                break;
            default:
                break;
        }
    });
    changeName();
});

function callWebserviceKonfigurimi(idKomp, kodKonf) { 
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvGjendjeArke").show();
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    }).done(SucceededCallbackKonfig);
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(301, cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(301, cmbKonfigurimi.GetText());
}

function EndRequestHandler(sender, args) {

    var hf = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvGjendjeDitore, "20045", pastrofusha, hfTeDrejta);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvGjendjeDitore, "20045", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function menu_click(s, e) {

    hfShtimModifikim = $('#hfShtimModifikim'); 
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;

        var hf = $('#hfKontrollet');

        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblGjendja'];

        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvGjendjeDitore.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("MsgBlerjeShitjeAfatMaturimi"));
    else
        gvGjendjeDitore.GetRowValues(indexModifiko, 'IdGjendjeDitore;Arka;Data;Vlera', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();
}

function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    cmbArka.SetText(values[1]);
    dteData.SetDate(values[2]);
    txtVlera.SetText(values[3]);
    
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }

    aktivizoFusha(colKontrollet, colAtrTrupi, false);

    if ($('#hfShtimModifikim').val() == "modifikim") {
        cmbArka.SetEnabled(false);
        dteData.SetEnabled(false);
    }
}

function activeTabChanged(s, e) {
    indexModifiko = gvGjendjeDitore.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);
            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function pastrofusha() {
    cmbArka.SetSelectedIndex(-1);
    cmbArka.SetValue('');
    dteData.SetDate(new Date());
    txtVlera.SetText('0');
    $('#hfShtimModifikim').val() == 'shtim' ? cmbArka.SetEnabled(true) : cmbArka.SetEnabled(false);
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

function Arka_Click() {

    var header = hfState.Get("msgZgjidhniArken");
    myButtonClickLupa.LupaUniversal_Click(header, 'LupaBanka.aspx?arka=false', 796, 585);
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
function hapRaport() {
    var myParams = [];
    myParams.push("idraporti=" + 173);
    myParams.push("idmod=" + 2)
    myParams.push("windowWidth=" + $(window).width());
    myParams.push("Filtro=" + false);
    var ngaCRM = Utils.getUrlVar("vjenNga");
    if (ngaCRM != "undefined")
        myParams.push("vjenNga=" + ngaCRM);

    window.open((173 == 292 ? "CRMHarte.aspx?" : "Raporti.aspx?") + myParams.join('&'), "_blank");
}