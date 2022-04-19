; $(document).ready(function (e) {
    PershtatAmbjentin();
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idVitNdermarrje = hfState.Get("idViti");
    myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idVitNdermarrje);
    changeName();
    if (vjenNgaRoute||meAnketa) {
        setTimeout(function () {
            $('#hfShtimModifikim').val('shtim');
            PageControl.SetActiveTabIndex(1);
        }, 10);
    }

    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblDetyra",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    })
});
var eshteLupe = false;
var meAnketa = false;
var vjenNgaRoute = false;
function PershtatAmbjentin() {
    eshteLupe = Utils.getUrlVar("eshteLupe");
    meAnketa = Utils.getUrlVar("meAnketa");
    vjenNgaRoute = Utils.getUrlVar("vjenNgaRoute");
    if (Utils.IsUndefined(meAnketa))
    {
        meAnketa = false;
    }
    if (!eshteLupe) {
        $('.header').removeClass("headerLupe");
        $(".content").removeClass("contentLupe");
    }
}

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
    myMenu.aplikoFiltra(s, e, ASPxGridView_Detyrat, "2011", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    if (eshteLupe) {
        ZgjidhClicked();
        return;
    }

    indexModifiko = index;
    lista = true;
    mbushfusha();
}

function menu_click(s, e) {
    hfState.Set('vjenNga', 'vetLupa');
    switch (e.item.name) {
        case "Ruaj":
            Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
            break;
        case "OK":
            ZgjidhClicked(s, e);

            if (!meAnketa) {//do te thote po perdoret si ambjent detyrash dhe nuk ka nevoj per postback sepse
                e.processOnServer = false;
            }
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }

    //hfShtimModifikim.val($('#hfShtimModifikim').val); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    //hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //        myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, $('#hfShtimModifikim'), $('#hfId'), PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = ASPxGridView_Detyrat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje detyre!');
    else
        ASPxGridView_Detyrat.GetRowValues(indexModifiko, 'IdDetyra;Kodi;Pershkrimi;Kategoria;Rendesia;IdNdermarrje;IdStatusDok;DtKrijimi;DtModifikimi;IdKrijues;IdModifikues', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }

    $.ajax(
	  {
            url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
            data: JSON.stringify({ idLidhese: values[0], kodLloji: 'Detyra', idPerdorues: hfState.Get('idPerdoruesi') })
	  }).done(DoneCallbackAutorizime);

    txtKodi.SetText(values[1]);
    txtPershkrimi.SetText(values[2]);
    cmbKategoria.SetValue(values[3]);
    cmbRendesia.SetValue(values[4]);

    //therret web service per te kontrolluar nese kjo detyre eshte e lidhur apo jo

    var idNdermarrje = hfState.Get("idNdermarrje");
    var idGjuha = hfState.Get("idGjuha");
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: 2011, kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        ASPxMenu1.AdjustControl();
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}



//thirret pasi eshte kthyer pergjigje nga serveri
function DoneCallbackAutorizime(result) {
    if (!result)
        return;
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
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
    txtPershkrimi.SetText('');
    var katg = hfState.Get("kategoria");

    cmbKategoria.SetValue(katg > -1 ? katg : 1);
    cmbRendesia.SetValue(1);
    cmbAutorizimi.SetValue(null);
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

/*
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/
//function OnGridSelectionChanged(e) {
//    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
//}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    ASPxGridView_Detyrat.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvDetyra").show();
    var idGjuha = hfState.Get('idGjuha');

    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    //.then(function () {
    //    lblKategoria.SetVisible(!eshteLupe);
    //    cmbKategoria.SetVisible(!eshteLupe);
    //    lblRendesia.SetVisible(!eshteLupe);
    //    cmbRendesia.SetVisible(!eshteLupe);
    //});
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
        var arrTabela = ['tblDetyra'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoDet.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
    }
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
            $("#hfLupaAutorizimi").val(colAtrTrupi[i].IdKonfigAmbjenteLupa); // kontrollet[i].split(',')[11].toString();
            continue;
        }
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, $('#hfShtimModifikim'), isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("2011", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("2011", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        // cmbKonfigurimi.SetText(hfKonffillestar.value);
        ndryshoKonfiguriminInit();
    }
    //if(eshteLupe)
    //myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $("#hfShtimModifikim"));
    //else
    //myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var items = args.get_dataItems();

    if ($("#hfStatusi").val() == "true" && eshteLupe && !meAnketa) 
        RuajDetyrePerRoute(items["hfId"], items["hfKodi"], items["hfEmertimi"]);

    if ($("#hfStatusi").val() == "true" && eshteLupe && meAnketa) 
        PageControl.SetActiveTabIndex(0);
    
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, $("#hfStatusi"), $('#hfKontrollet'), colAtrTrupi, $('#hfShtimModifikim'), $('#hfId'), indexModifiko, PageControl, ASPxGridView_Detyrat, "2011", pastrofusha, hfTeDrejta);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'), false, true);
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

var KPF = 0;
function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var qstrAutorizimet = '?autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniAutorizimet"), 'LupaAutorizim.aspx' + qstrAutorizimet, 700, 600);
}

function OnGridSelectionChangedCompletePerSkeduler(values) {
    if (!values[0])
        return;
    var combo = window.parent.ddKlienti;
    Utils.SelectComboItem(combo, values[0][0], values[0][1], values[0][2]);
    combo.SetFocus(true);
    window.parent.popupUniversal.Hide();
}

function RuajDetyrePerRoute(id,kodi,emertimi) {
    var urlParams = new URLSearchParams(window.location.search);
    var vjenNgaRoute = urlParams.get('vjenNgaRoute');
    if (vjenNgaRoute) {
        var combo = window.parent.ddKlienti;
        Utils.SelectComboItem(combo, id, kodi, emertimi);
        combo.SetFocus(true);
        window.parent.popupUniversal.Hide();
    }
}

function PageControlTabChanging(s, e) {
  var menuItem = null;

    if (meAnketa && PageControl.activeTabIndex > 1) {

        menuItem = ASPxMenu1.GetItemByName("Shto");
        if (menuItem != null)  menuItem.SetVisible(false)

        menuItem = ASPxMenu1.GetItemByName("Modifiko");
        if (menuItem != null)     menuItem.SetVisible(false)

        menuItem = ASPxMenu1.GetItemByName("Ruaj");
        if (menuItem != null)  menuItem.SetVisible(false)

        menuItem = ASPxMenu1.GetItemByName("Fshi");
        if (menuItem != null)   menuItem.SetVisible(false)

        menuItem = ASPxMenu1.GetItemByName("OK");
        if (menuItem != null) menuItem.SetVisible(true);

        ASPxMenu1.AdjustControl();
    }
    else {
        menuItem = ASPxMenu1.GetItemByName("Shto");
        if (menuItem != null)
            menuItem.SetVisible(true)
        menuItem = ASPxMenu1.GetItemByName("Fshi");
        if (menuItem != null)
            menuItem.SetVisible(true)

        myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
    }


    if (vjenNgaRoute||(meAnketa))
        indexModifiko = -1;
    else
        indexModifiko = ASPxGridView_Detyrat.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            mbushfusha();
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);
            pastrofusha();
            vjenNgaRoute = false;
        }
    }
    kaloTab = false;
  
}

function ZgjidhClicked(s, e) {
    //lupe lupe e thjeshte detyrash
    if (eshteLupe && !meAnketa) {
        //jemi duke shfrytezuar si lupe detyrash ambjentin
        ASPxGridView_Detyrat.GetSelectedFieldValues('IdDetyra;Kodi;Pershkrimi', OnGridSelectionChangedCompletePerSkeduler);

        return;
    }

    //per detyrat
    var keys = ASPxGridView_Detyrat.GetSelectedKeysOnPage()
    var detyraMeDataVlefshmerie = new Array();

    for (var k = 0; k < keys.length; k++)
    {
        detyraMeDataVlefshmerie[k] =
            {
                "DetyraID": keys[k],
                "DtFillimi": window["DtFillimi" + keys[k]].GetText(),
                "DtMbarimi": window["DtMbarimi" + keys[k]].GetText()
            }
    }

    $("#hfDataVlefshmerie").val(JSON.stringify(detyraMeDataVlefshmerie));//ruajm datat ne HF
    $('#hfKontrollet').val(window.parent.$('#hfKontrollet').val());
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function Row_DblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex);
    kaloTab = true;
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}