;
var lidhur = false;
var widthLupaArtikull = 1200;
var heightLupaArtikull = 600;

var widthLupaKF = 800;
var heightLupaKF = 600;

var widthLupaMagazina = 600;
var heightLupaMagazina = 600;

var widthLupaKerko = 600;
var heightLupaKerko = 600;

var widthLupaDetajim = 600;
var heightLupaDetajim = 560;

var editorData;
var tabPressed = false;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'PlanifikimProdhimi'
};


var pageState = {
    dataGrid: null,
    rreshtIndex: null,
    identifikuesPyetje: null,
    queryString: null,
    kategoriDetajimi: null,
    kodDetajimi: null
};

jQuery(document).ready(function () {
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);    
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
        }
    }).trigger('resize');
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
            case 9: //tab
                tabPressed = true;
                break;
            default:
                break;
        }
    });


    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });
    $(document).on('click',function(){
      tabPressed = false;
    });
});

var identifikuesPerPopupDokumentat;
var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupArtikulli;

var identifikuesPerPopupMagazina;


function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_Planifikim.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_Planifikim.aspx', Utils.getUrlVar('id'));
    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function vendosNrAutomatik(colAtrTrupi, colKontrollet) {//po
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}


/*
Function: ButtonClickKerko

Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {//po
    var queryString = {
        veprimi: 'Planifikim',
        listUrl: listUrl
    };
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDokumentin"));
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?'+ Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
}

function Init() {
    if (typeof (isPostBack) == "undefined") {
        editorData = dteDtDok;
        //    callWebserviceKonfigurimi("804"); //804 = id komponente (Shto_Planifikim.aspx)
        var hf = document.getElementById("hfKonffillestar");
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");

        cmbKonfigurimi.SetText(hf.value);
        ndryshoKonfigurimin();
        identifikuesPerPopupDokumentat = "Planifikim";
        identikuesPerPopupKlientFurnitori = "Planifikim";
        identikuesPerPopupArtikulli = "Planifikim";
        identifikuesPerPopupMagazina = "Planifikim";
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}


function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}


/*
Function: callWebserviceKonfigurimi

Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    var idPerdoruesi = hfState.Get('idPerdorues');
    var idNdermarrje = hfState.Get('idNdermarrje');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    if ($('#hfShtimModifikim').val() != 'modifikim')
    {
        $.ajax({
            pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheGrupimDokumentashNderm"),
            data: JSON.stringify({ kodkonfig: kodKonf, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi })
        }).done(Utils.SucceededCallbackGrupimDokumentash);
    }

}
function DateChanged(s, e) {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);

}
var colKushte; var colAlterKusht;

function SucceededCallbackKonfig(result) {//po
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontrollet;
    var colAtrTrupi = result.colAtrTrupi;
    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    vendosDateDefault();
    
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    var colGrida = result.colGrida;
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hfKl = $("#hfLupaKlientFurnitor")[0];
    var hfMag = $("#hfLupaMagazina")[0];
    var hfLupaNjesiProdhimi = $("#hfLupaNjesiProdhimi")[0];
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet)); $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() === "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
        CreateDataGrid(colGrida);
    } else if ($("input[id$='hfShtimModifikim']").val() === "modifikim") {
        callWebserviceDokumentPlanifikimProdhimi();
    }

    for (var i = 0; i < colKontrollet.length - 1; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (colAtrTrupi[i].KodKontrolli === "btneKlientFurnitori")
            hfKl.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme
        else
            if (colAtrTrupi[i].KodKontrolli === "btneMagazina")
                hfMag.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme
            else if (colAtrTrupi[i].KodKontrolli === "cmbNjesiProdhimi")
                hfLupaNjesiProdhimi.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString();
    }

    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}


/*
Function: ndryshoKonfigurimin

Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
      //  lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") +': ' + pershkKonfigAmb);
    callWebserviceKonfigurimi(804, cmbKonfigurimi.GetText());
}

/*
Function: SucceededCallbackNiveli

Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function ButtonClickMagazina() {

    var hfKl = document.getElementById("hfLupaMagazina");
    var queryStr = hfKl.value;

    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr);

    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

/*
Function: TextChangedMagazina

Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina() {
   
    var dataSource = pageState.dataGrid.GetData();
    for (i = 0; i < dataSource.length; i++) {
        dataSource[i].IdMag = btneMagazina.GetValue();
    }
    pageState.dataGrid.Refresh();
}


/*
Function: ButtonClickFurnitori

Hap lupen e klienteve/furnitoreve.
*/
function ButtonClickFurnitori() {//po
    var hfKl = document.getElementById("hfLupaKlientFurnitor");
    var queryStr = hfKl.value;
    popupUniversal.SetHeaderText(hfState.Get("headerZgjidhKlientFurnitorin"));
    popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=1&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
}

/*
* Function: KlientFurnitoriChanged
* Therret funksionin < callWebserviceKF > per te marre vlerat e klientit / furnitorit te zgjedhur
* Nese nuk eshte zgjedhur ndonje klient/furnitor ekzistues fshin textin dhe venod fokusin te kontroli
*/
function KlientFurnitoriChanged() {
    if (isNaN(btneKlientFurnitori.GetValue())) {
        btneKlientFurnitori.SetText('');
        btneKlientFurnitori.Focus();
        return;
    }
}

/*
Function: pastroFushatKokes

Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    btneKlientFurnitori.SetValue(null);
    btneMagazina.SetValue(null);
    if (btneMagazina.GetItemCount() == 1)
        btneMagazina.SetSelectedIndex(0);
    txtNrDok.SetText('');
    txtShenime.SetText('');

    cmbNjesiProdhimi.SetValue(null);
    cmbGrup1.SetValue(null);
    cmbGrup2.SetValue(null);
    cmbGrup3.SetValue(null);
    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();
    pastroPageState();
}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() === "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            dteAfatiKohor.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
            dteDtRegjistrimi.SetDate(dataSot);
        }
        catch (e) { }

    }
}


function isValidKoka() {
    ///<summary> metode per te validuar te dhenat e kokes</summary>

    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }

    else if (txtNrDok.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    else if (dteDtDok.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    else if (dteDtRegjistrimi.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    else if (btneKlientFurnitori.GetText() != '' && btneKlientFurnitori.GetSelectedItem() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKFnukEkziston"));
        return false;
    }
    else return true;
}


/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = document.getElementById("status1");
    click = false;
    if (hf.value === "true") {

        myFaqeCelje.kontrolloTeDrejta('Shto_Planifikim.aspx?shtim_modifikim=shtim', true);

    } else click = false;
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name === 'Ruaj') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif(); 
            click = false;
            e.processOnServer = false;
            return;
        }
        //myFaqeCelje.valido(s, e);
        var valid = myFaqeCelje.validim(s, e);
        if (!valid) {
            Utils.hiqLoadingGif();
            e.processOnServer = false;
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            //myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }

    else if (e.item.name === 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('Planifikimi.aspx', null, true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('Shto_Planifikim.aspx?shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeFshireDok"));
            Utils.hiqLoadingGif(); 
            click = false;
            e.processOnServer = false;
            return;
        }
        popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name === 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('Planifikimi.aspx');
        e.processOnServer = false;
        click = false;
    }

}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('Planifikimi.aspx?ruaj=po');

}
var click = false;
/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
        return;
    }
    click = true;
    if (isValidKoka()) {
        merrTeDhenatArtPerberes();

    }
    else {
        e.processOnServer = false;
        click = false;
        Utils.hiqLoadingGif();
    }
}

/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionin <pastroFushatKokes>.
*/
function PastroClick() {
    $("input[id$='hfAutorizimi']").val(true);
    pastroFushatKokes();
  
    var hf = $("#hfShtimModifikim"); hf.val("shtim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    ndryshoKonfigurimin();

    $('#ASPxSplitter1_hl').empty();
    click = false;
}

/*
 * Function: merrTeDhenatArtPerberes
 * Pergatit datasourcin e grides dhe e vendos ne hidden field qe ta aksesojme ne server side per ta ruajtur ne DB. 
 */ 
function merrTeDhenatArtPerberes() {
    var gridDataObject = $('#gridDataObject');
    pageState.dataGrid.SaveCurrentValues();
    var dataSourceGride = pageState.dataGrid.GetData();
    var dataSourceRuajtje = new Array();
    dataSourceGride.forEach(function (item, i) {
        if (i != dataSourceGride.length - 1) {
            dataSourceRuajtje[i] = new Object();
            dataSourceRuajtje[i].IdTrupiPlanifikim = 0;
            dataSourceRuajtje[i].KodiArtikull = item.KodiArtikull != undefined ? item.KodiArtikull : "";
            dataSourceRuajtje[i].Shenime = item.Shenime != undefined ? item.Shenime : "";
            dataSourceRuajtje[i].IdMag = item.IdMag != undefined ? item.IdMag : 0;
            dataSourceRuajtje[i].Sasia = item.Sasia != undefined ? item.Sasia : 1;
            dataSourceRuajtje[i].IdNjesia = item.IdNjesia != undefined ? item.IdNjesia : 0;
            dataSourceRuajtje[i].Detajim1 = item.IdDetajim1 != undefined ? item.IdDetajim1 : 0;
            dataSourceRuajtje[i].Detajim2 = item.IdDetajim2 != undefined ? item.IdDetajim2 : 0;
            dataSourceRuajtje[i].KodDetajim1 = item.Detajim1 != null ? item.Detajim1 : "";
            dataSourceRuajtje[i].KodDetajim2 = item.Detajim2 != null ? item.Detajim2 : "";
            dataSourceRuajtje[i].Gjeresi = item.Gjeresi != undefined ? item.Gjeresi : 1;
            dataSourceRuajtje[i].Gjatesi = item.Gjatesi != undefined ? item.Gjatesi : 1;
            dataSourceRuajtje[i].SasiPermase = item.SasiPermase != undefined ? item.SasiPermase : 1;
            dataSourceRuajtje[i].IdUrdherPorosi = item.IdUrdherPorosi != 0 ? item.IdUrdherPorosi : 0;
        }
    });
    
    gridDataObject.val(JSON.stringify(dataSourceRuajtje));
}


function ButtonClickNjesiProdhimi(s, e) {
    identifikuesPerPopupNjesiProdhimi = "Planifikim";
    var hfNjesiProdhimi = document.getElementById("hfLupaNjesiProdhimi");
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Njesi Prodhimi', 'LupaNjesiProdhimi.aspx?idKonfigAmbjente=' + hfNjesiProdhimi.value, 700, 560);
}

function TextChangedNjesiProdhimi(s, e) {
    if (isNaN(cmbNjesiProdhimi.GetValue())) {
        cmbNjesiProdhimi.SetText('');
        cmbNjesiProdhimi.Focus();
        return;
    }
}

/*
* Function: callWebserviceDokumentPlanifikimProdhimi
* Therret webservice per te marre trupin e dokumentit te planifikimit te nga DB
* dhe therret funksionin <VendosDokumentPlanifikimProdhimi>
*/ 

function callWebserviceDokumentPlanifikimProdhimi() {
    var idDokumenti = Utils.getUrlVar("id");
    if (!(idDokumenti && idDokumenti > 0))
        return;

    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("PlanifikimProdhimi", "MerrDokumentPlanifikimProdhimi"),
        data: JSON.stringify({ idDokumenti: idDokumenti })
    }).done(VendosDokumentPlanifikimProdhimi);
}

/*
* Function: VendosDokumentPlanifikimProdhimi
* Pergatit datasourcin e grides dhe therret funksionin <CreateDataGrid> per incializimin e saj.
*/
function VendosDokumentPlanifikimProdhimi(result) {
    var dataSourceGride = new Array();
    result.forEach(function (item) {
        var artikulli = item.artikulli;
        if (artikulli.Njesi1Artikulli == artikulli.Njesi2Artikulli) {
            item.trupiPlanifikim.Njesite = [{ IdNjesia: artikulli.Njesi1Artikulli, Njesia: artikulli.PershkrimNjesia1 }];
            item.trupiPlanifikim.Njesia = artikulli.PershkrimNjesia1;
        }
        else {
            item.trupiPlanifikim.Njesite = [{ IdNjesia: artikulli.Njesi1Artikulli, Njesia: artikulli.PershkrimNjesia1 }, { IdNjesia: artikulli.Njesi2Artikulli, Njesia: artikulli.PershkrimNjesia2 }];

            if (item.trupiPlanifikim.IdNjesia == artikulli.Njesi1Artikulli)
                item.trupiPlanifikim.Njesia = artikulli.PershkrimNjesia1;
            else item.trupiPlanifikim.Njesia = artikulli.PershkrimNjesia2;
        }

        item.trupiPlanifikim.IdDetajim1 = item.trupiPlanifikim.Detajim1;
        item.trupiPlanifikim.Detajim1 = item.detajim1.KodDetajimArtikulli;
        item.trupiPlanifikim.IdDetajim2 = item.trupiPlanifikim.Detajim2;
        item.trupiPlanifikim.Detajim2 = item.detajim2.KodDetajimArtikulli;

        dataSourceGride.push(item.trupiPlanifikim);
    });
    CreateDataGrid(JSON.parse($('#HfGridCol').val()), dataSourceGride);
}

/*
 * Function: CreateDataGrid
 * Inicializon griden e trupit te dokumentit.
 */
CreateDataGrid = function (columnsKonfig, dataSource) {
    var disabled = $("input[id$='hfLidhur']").val() == 'True' ? true : false;
    var magazinat = JSON.parse(hfState.Get("colMagPlanifikimi"));
    defaultObject = {
        IdTrupiPlanifikim: 0, IdArtikulli: 0, KodiArtikull: null, PershkrimArtikull: null, Shenime: null, IdMag: magazinat[0].IdNjesiAdministrative, IdUrdherPorosi: 0,
        Sasia: 1, IdNjesia: 0, Njesia: null, IdDetajim1: 0, Detajim1: null, IdDetajim2: 0, Detajim2: null, Gjeresi: 1, Gjatesi: 1, SasiPermase: 1
    };
    var ds = dataSource ? dataSource : [Utils.CloneObject(defaultObject)];
    pageState.dataGrid = new myDxDataGrid("PlanifikimTrupiDataGrid", {
        dataSource: ds,
        focusStateEnabled: false,
        searchPanel: { visible: false },
        showRowLines: true,
        disabled: disabled,
        addDeleteRowCommand: true,
        sorting: { mode: "none" },
        editing: {
            mode: "cell",
            allowUpdating: true,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        onCellPrepared: function (cell) {
            if (!cell.column.allowEditing)
                cell.cellElement.addClass("dxeDisabled_MetropolisBlue");
        },
        onContentReady: function () {
            pageState.dataGrid.ShtoRreshtBosh(defaultObject, "KodiArtikull");
        }
    });

    pageState.dataGrid.SetColumnsFromConfig(columnsKonfig);
    pageState.dataGrid.AddCustomOptionToColumns(["PershkrimArtikull"], "allowEditing", false);
    pageState.dataGrid.AddCustomOptionToColumns(["IdNjesia"], "calculateDisplayValue", "Njesia");
    pageState.dataGrid.AddCustomOptionToColumns(["IdNjesia"], "dataType", "string");
    pageState.dataGrid.AddCustomOptionToColumns(["IdNjesia"], 'editCellTemplate', function (container, options) {
        container.dxSelectBox({
            dataSource: options.data.Njesite,
            displayExpr: "Njesia",
            valueExpr: "IdNjesia",
            value: options.value,
            tabindex: 1,
            focusStateEnabled: true,
            selected: false,
            onValueChanged: function (x) {
                options.setValue(x.value, options.data.Njesite.filter(function (item) { return item.IdNjesia == x.value; })[0].Njesia);
            }
        });
    });
  
    var autoCompleteColumnsArt = [{ dataField: "label", capField: "Kodi", width: 4 }, { dataField: "desc", capField: "Pershkrimi", width: 8 }];
    pageState.dataGrid.AddAutocompleteToColumn("KodiArtikull", "IdArtikulli", "PershkrimArtikull", merrArtikull, "Zgjidhni artikullin...", "value", "label", "desc", HapLupeArtikulli, onValueChangedArtikulli, autoCompleteColumnsArt);
    pageState.dataGrid.AddDataSourceToColumn("IdMag", magazinat, "IdNjesiAdministrative", "Kodi", false);
    pageState.dataGrid.AddAutocompleteToColumn("Detajim1", "IdDetajim1", undefined, merrDetajim1, "Detajim 1...", "value", "label", undefined, HapLupeDetajimi1);
    pageState.dataGrid.AddAutocompleteToColumn("Detajim2", "IdDetajim2", undefined, merrDetajim2, "Detajim 2...", "value", "label", undefined, HapLupeDetajimi2);
};


function ShtoPyetjeCeljeDetajim(lloji, idRreshti, kategoria, kodDetajimi) {
    if (lloji == 1)
        pageState.identifikuesPyetje = 'detajimi1';
    else pageState.identifikuesPyetje = 'detajimi2';
    pageState.rreshtIndex = idRreshti;
    pageState.kategoriDetajimi = kategoria;
    pageState.kodDetajimi = kodDetajimi;
    myMesazh.ShtoPyetje(hfState.Get("msgDetajimiVendosurNukEkzistonDoniTaCelni"));
    return;
}

function PoClick(s, e) {

    var dataSource = pageState.dataGrid.GetData();
    var idRresht = pageState.rreshtIndex;
    switch (pageState.identifikuesPyetje) {

        case 'detajimi1':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            pageState.queryString = 'vjenNga=Shto_Planifikim&kodArtikulli=' + dataSource[idRresht].KodiArtikull + '&lloji=1&veprimi=' + pageState.kategoriDetajimi + '&kodDet=' + pageState.kodDetajimi;
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + pageState.queryString, 900, 600);
            break;
        case 'detajimi2':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            pageState.queryString = 'vjenNga=Shto_Planifikim&kodArtikulli=' + dataSource[idRresht].KodiArtikull + '&lloji=2&veprimi=' + pageState.kategoriDetajimi + '&kodDet=' + pageState.kodDetajimi;
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + pageState.queryString, 900, 600);
            break;
        case 'detajimi1Lidhje':
            Utils.shfaqLoadingGif();
            RuajLidhjeDetajim(1, idRresht, dataSource);
            break;
        case 'detajimi2Lidhje':
            Utils.shfaqLoadingGif();
            RuajLidhjeDetajim(2, idRresht, dataSource);
            break;
        default:
            alert('Pyetje e panjohur');
            pageState.identifikuesPyetje = null;
            break;
    }
}

function JoClick(s, e) {
    var dataSource = pageState.dataGrid.GetData();
    var idRresht = pageState.rreshtIndex;
    switch (pageState.identifikuesPyetje) {
       
        case 'detajimi1':
        case 'detajimi1Lidhje':
            dataSource[idRresht].Detajim1 = null;    
            pageState.identifikuesPyetje = null;
            break;
        case 'detajimi2':
        case 'detajimi2Lidhje':
            dataSource[idRresht].Detajim2 = null;
            pageState.identifikuesPyetje = null;
            break;
        default:
            alert('Pyetje e panjohur');
            pageState.identifikuesPyetje = null;
            break;
    }
    pageState.dataGrid.Refresh();
}

function pastroPageState() {
    pageState.identifikuesPyetje = null;
    pageState.queryString = null;
    pageState.rreshtIndex = null;
    pageState.kategoriDetajimi = null;
}

function HapLupeDetajimi1(rowIndex) {
    HapLupeDetajim(rowIndex, 1);
}

function HapLupeDetajimi2(rowIndex) {
    HapLupeDetajim(rowIndex, 2);
}

/*
 * Function: HapLupeDetajimi
 * Hap lupen e detajimeve
 */ 
function HapLupeDetajim(rowIndex, lloji) {
    pageState.rreshtIndex = rowIndex;
    var dataSource = pageState.dataGrid.GetData();
    var idArt = dataSource[rowIndex].IdArtikulli == null ? 0 : dataSource[rowIndex].IdArtikulli;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDetajimArtikulli"));
    popupUniversal.SetContentUrl("LupaDetajimArtikulliRegjistrim.aspx?idArtikulli=" + idArt + "&lloji=" + lloji + "&vjenNga=Shto_Planifikim");
    popupUniversal.SetSize(widthLupaDetajim, heightLupaDetajim);
    popupUniversal.Show();
}

function merrDetajim1(value, rowIndex, perAcList) {
    return merrDetajim(value, rowIndex, perAcList, 1);
}

function merrDetajim2(value, rowIndex, perAcList) {
   return merrDetajim(value, rowIndex, perAcList, 2);
}

/*
 * Function: merrDetajim
 * Merr listen e detajimeve per autocomplete
 */ 
function merrDetajim(value, rowIndex, perAcList, detajim1ose2) {

    var deferred = new jQuery.Deferred();
    var wsData, wsURL, dataSource;
    dataSource = pageState.dataGrid.GetData();
    if (!dataSource[rowIndex].KodiArtikull)
        return;
    wsData = {
        infixText: value, art: dataSource[rowIndex].KodiArtikull, lloji: detajim1ose2, idNdermarrje: hfState.Get('idNdermarrje'), idPerdoruesi: hfState.Get('idPerdorues'), merrPerberesit: false,
        date: dteDtDok.GetDate(), sipasGjendjes: false, detajimi1: value, mag: pageState.dataGrid.GetColumnDataSourceItem("IdMag", dataSource[rowIndex].IdMag).Kodi
    };
    wsURL = Utils.getServerApiUrl("Rregjistrime", "ktheListeDetajimeshArtikulliNew");
    pageState.rreshtIndex = rowIndex;
    $.ajax({
        pritPergjigje: true,
        url: wsURL,
        data: JSON.stringify(wsData),
        success: function (result) {
            
            if (!perAcList) {
                var objekti = result.autocomplete.filter(function (item) { return item.label.toLowerCase() == value.toLowerCase() || item.desc.toLowerCase() == value.toLowerCase(); })[0];
                if (!objekti) {
                    pageState.dataGrid.Grida.focus(pageState.dataGrid.Grida.getCellElement(rowIndex, detajim1ose2 == 1 ? "Detajim1" : "Detajim2"));
                    if (!result.meDetajim)
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikullpaDetajim"));

                    else if (result.ekzistonDetajim) {
                       
                        if (result.kategoriDet == 3 || result.kategoriDet == 4) {
                            RuajLidhjeDetajim(detajim1ose2, rowIndex, dataSource);
                        }
                        else {
                            if (detajim1ose2 == 1) {
                                pageState.identifikuesPyetje = 'detajimi1Lidhje';
                                dataSource[rowIndex].Detajim1 = result.infixText;
                            }
                            else {
                                pageState.identifikuesPyetje = 'detajimi2Lidhje';
                                dataSource[rowIndex].Detajim2 = result.infixText;
                            }
                            pageState.dataGrid.Refresh();
                            myMesazh.ShtoPyetje(hfState.Get("msgDetajimJoLidhur"));
                        }
                    }
                    else
                        switch (result.kategoriDet) {
                            case 0:
                                myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtSkaKategoriPerDetajim"));
                                break;
                            case 1:
                            case 2:
                                if (detajim1ose2 == 1)
                                    dataSource[rowIndex].Detajim1 = result.infixText;
                                else
                                    dataSource[rowIndex].Detajim2 = result.infixText;
                                pageState.dataGrid.Refresh();
                                ShtoPyetjeCeljeDetajim(detajim1ose2, rowIndex, result.kategoriDet, result.infixText);
                                break;
                            case 3:
                                if (Date.parseLocale(result.infixText, "dd/MM/yyyy") == null || result.infixText.length != 10)
                                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgKodiDateSkadenceDuhetFormat"));
                                else CelDheLidhDetajimMeArtikull(dataSource[rowIndex].IdArtikulli, detajim1ose2, result.kategoriDet, 3, result.infixText);
                                break;
                            case 4:                               
                                CelDheLidhDetajimMeArtikull(dataSource[rowIndex].IdArtikulli, detajim1ose2, result.kategoriDet, 1, result.infixText);
                                break;
                        }
                }
                else {
                    if (detajim1ose2 == 1)
                        pageState.dataGrid.VendosVleraNeDataSource(rowIndex, "Detajim1", "IdDetajim1", undefined, result.autocomplete[0], "label", "value");
                    else pageState.dataGrid.VendosVleraNeDataSource(rowIndex, "Detajim2", "IdDetajim2", undefined, result.autocomplete[0], "label", "value");
                    pageState.dataGrid.Refresh();
                }
            }
            else
                deferred.resolve(result.autocomplete);
        }
    });
    return deferred.promise();
}

/*
 * Function: RuajLidhjeDetajim
 * Therret webservice lidhjen e detajimit
 */

function RuajLidhjeDetajim(llojDet, idRreshti, dataSource) {

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "RuajLidhjeDetajim"),
        data: JSON.stringify({
            kodartikulli: dataSource[idRreshti].KodiArtikull, kodi: llojDet == 1 ? dataSource[idRreshti].Detajim1 : dataSource[idRreshti].Detajim2,
            idNdermarrje: hfState.Get('idNdermarrje'), lloji: llojDet, idPerdorues: hfState.Get('idPerdorues')
        })
    }).done(function (result) {
        Utils.hiqLoadingGif();
        if (result.Status)
            myMesazh.ShtoMesazhInformues(result.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhSesioni(result);
    });
}

/*
 * Function: CelDheLidhDetajimMeArtikull
 * Therret webservice per celjen dhe lidhjen e detajimit
 */ 

function CelDheLidhDetajimMeArtikull(idArtikulli, detajimPareApoDyte, kategoria, llojDetajimi, kodDetajimi) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "CelDheLidhDetajimMeArtikull"),
        showLoading: true,
        data: JSON.stringify({
            idArtikulli: idArtikulli,
            detajimPareApoDyte: detajimPareApoDyte,
            llojDetajimi: llojDetajimi,
            kategoriDetajimi: kategoria,
            kodDetajimi: kodDetajimi
        })
    }).done(function (result) {        
        if (result.mesazhi.Status) {
            myMesazh.ShtoMesazhInformues(result.mesazhi.PershkrimMesazhi);
            vendosDetajim(result.detajimi, detajimPareApoDyte);
        }
        else
            myMesazh.ShtoMesazhSesioni(result.mesazhi);
    });
}

/*
 * Function: vendosDetajim
 * Vendos ne gride Detajimin e zgjedhur nga lupa.
 */ 
function vendosDetajim(detajimi, lloji) {
    var dataSource = pageState.dataGrid.GetData();
    if (lloji == 1) {
        dataSource[pageState.rreshtIndex].IdDetajim1 = detajimi.IdDetajimArtikulli;
        dataSource[pageState.rreshtIndex].Detajim1 = detajimi.KodDetajimArtikulli;
    }
    else {
        dataSource[pageState.rreshtIndex].IdDetajim2 = detajimi.IdDetajimArtikulli;
        dataSource[pageState.rreshtIndex].Detajim2 = detajimi.KodDetajimArtikulli;
    }
    pageState.dataGrid.Refresh();
    pageState.dataGrid.Grida.closeEditCell();
    pastroPageState();
}

/*
 * Function: merrArtikull
 * Merr listen e artkujve per autocomplete.
 */ 

function merrArtikull(value, rowIndex, perAcList) {
    var deferred = new jQuery.Deferred();
    var wsData, wsURL;
    wsData = { infixText: value ? value : "", pershk: 1, grup: "", idNdermarrje: hfState.Get('idNdermarrje'), idPerdoruesi: hfState.Get('idPerdorues'), artikujTeShitshem: true, merrVetemAfatgjate: false, merrSipasDetajimit: false, klasa: "" };
    wsURL = Utils.getServerApiUrl("Rregjistrime", "ktheACListeArtikujshKodPershkKodbarFull");
    $.ajax({
        pritPergjigje: true,
        url: wsURL,
        data: JSON.stringify(wsData),
        success: function (result) {
            
            if (!perAcList) {
                var objekti = result.filter(function (item) { return item.label.toLowerCase() == value.toLowerCase() || item.desc.toLowerCase() == value.toLowerCase(); })[0];
                if (!objekti) return;
                pageState.dataGrid.VendosVleraNeDataSource(rowIndex, "KodiArtikull", "IdArtikulli", "PershkrimArtikull", result[0], "label", "value", "desc", onValueChangedArtikulli);
                pageState.dataGrid.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}

/*
 * Function: HapLupeArtikulli
 * Hap lupen e artikujve nga fusha Kodi e grides.
 */
function HapLupeArtikulli(rowIndex) {
    pageState.rreshtIndex = rowIndex;
    popupUniversal.SetHeaderText(hfState.Get("roundPanelZgjidhArtikullin"));
    popupUniversal.SetContentUrl('LupaArtikull.aspx?klasa=0');
    popupUniversal.SetSize(widthLupaArtikull, heightLupaArtikull);
    popupUniversal.Show();
    pageState.dataGrid.Grida.focus(pageState.dataGrid.Grida.getCellElement(rowIndex, "KodiArtikull"));
}
/*
 * Function: vendosArt
 * Vendos ne gride artikullin e zgjedhur nga lupa.
 */
function vendosArt(artikulli, rowIndex) { 
    var dataSource = pageState.dataGrid.GetData();
    dataSource[rowIndex].KodiArtikull = null; 
    if (!kontrolloArt(artikulli)) {
        resetRresht(dataSource, rowIndex);
        return;
    }
    dataSource[rowIndex].KodiArtikull = artikulli.KodArtikulli;
    dataSource[rowIndex].PershkrimArtikull = artikulli.PershkrimArtikulli;
    dataSource[rowIndex].IdArtikulli = artikulli.IdArtikulli;
    var njesite;
    if (artikulli.Njesi1Artikulli == artikulli.Njesi2Artikulli)
        njesite = [{ IdNjesia: artikulli.Njesi1Artikulli, Njesia: artikulli.PershkrimNjesia1 }];
    else njesite = [{ IdNjesia: artikulli.Njesi1Artikulli, Njesia: artikulli.PershkrimNjesia1 }, { IdNjesia: artikulli.Njesi2Artikulli, Njesia: artikulli.PershkrimNjesia2 }];
    dataSource[rowIndex].IdNjesia = artikulli.Njesi1Artikulli;
    dataSource[rowIndex].Njesia = artikulli.PershkrimNjesia1;
    dataSource[rowIndex].Njesite = njesite;
    pageState.dataGrid.Refresh();
}

/*
 * Function: kontrolloArt
 * Kontrollon artikullin qe po shtohet ne gride.
 */
function kontrolloArt(artikulli) {
    if (artikulli.Klasa != 5 && artikulli.Klasa != 6) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyArtikullNukIPerketKlasesProdhimOsePProces"));
        return false;
    }

    return true;
}

/*
 * Function: onValueChangedArtikulli
 * Therritet kur ndryshon teksti i fushes Kodi ne gride
 */
function onValueChangedArtikulli(rowIndex, result) {
    if (!result.objekti)
        return;
    var artikulli = result.objekti;
    vendosArt(artikulli, rowIndex);
}

/*
 * Function: resetRresht
 * Pastron rreshtin qe po plotesohet nese nuk kalohen kontrollet
 */ 
function resetRresht(dataSource, rowIndex) {
    dataSource[rowIndex].IdArtikulli = null;
    dataSource[rowIndex].KodiArtikull = null;
    dataSource[rowIndex].PershkrimArtikull = null;
    pageState.dataGrid.Refresh();
}