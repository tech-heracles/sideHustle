var indexSel = 0;                                           //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexModifiko;                                          //indexi i zgjedhur per modifikim
var mbush = false;                                          //perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var kaloTab = false;                                        //perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var lista = true;                                           //perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var nrWsRruges = 0;                                         //perdoret per te kontrolluar pergjigjet e kthyera nga webservices
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;   //perdoren per konfigurimin e ambjentit
var btnFiltrat;
var idKomponenteGISWorkspace = 3046;                        

jQuery(document).ready(function () {
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

function Init() {
    changeName();
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_GISWorkspace, idKomponenteGISWorkspace.toString(), cmbKonfigurimi.GetText());
}

function gridFocusRowCanged(s, e) {
    mbush = true;
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

function tabsActiveTabChanged(s, e) {
    indexModifiko = ASPxGridView_GISWorkspace.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko !== -1) {
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

/*Function: Poshte_click  perdoret per te selektuar rreshtin me poshte
Parameters: e-eventi*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_GISWorkspace, indexSel);
}

/*Function: Lart_click  perdoret per te selektuar rreshtin me lart
Parameters: e-eventi*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_GISWorkspace, indexSel);
}

/*Function: Fillim_click  perdoret per te shkuar ne fillim te faqes                
Parameters: e-eventi*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_GISWorkspace, indexSel);
}

/*Function:   Fund_click    perdoret per te shkuar ne fund te faqes
Parameters: e-eventi*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_GISWorkspace, indexSel);
}

/*Function: menu_click    perdoret per veprimet e menuse ne javascript
Parameters: e-eventi*/
function menu_click(s, e) {
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); 
    var ruajbuxhetet = false; //i here per i here
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, undefined, indexModifiko, pastrofusha, vendosKonfig, resultkonf, colKontrollet, aktivFusha, colAtrTrupi);
    if (e.item.name == 'Ruaj') {
        if (nrWsRruges !== 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
            e.processOnServer = false;
            return;
        }
    }
    else if (e.item.name == "Rifresko") {
        Utils.shfaqLoadingGif();;
    }
}

function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = ASPxGridView_GISWorkspace.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
    else
        ASPxGridView_GISWorkspace.GetRowValues(indexModifiko, 'id_workspace;workspace_name;IDNDERMARJE;NDERMARJEKODI;NDERMARJEPERSHK;GEOURL;GEOUSER;GEOPASSWORD;DSDATABASE;DSPORT;DSUSER;WSFUNCTIONS;WSDEFAULT;WSGUIDING;WSPUBLIC', OnGetRowValuesMod);
}

function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $('#hfId').val(values[0]);
    txtWorkspace.SetText(values[1]);
    cmbNdermarrje.SetValue(values[2]);
    cmbNdermarrje.SetText(values[3]);
    txtUrl.SetText(values[5]);
    txtUsername.SetText(values[6]);
    txtPassword.SetText(values[7]);
    txtDatabase.SetText(values[8]);
    txtPortDB.SetText(values[9]);
    txtUserDB.SetText(values[10]);
    txtPasswordDB.SetText('');
    txtKonfirmoPasswordDB.SetText('');
    txtWSFunksionesh.SetText(values[11]);
    txtWSDefault.SetText(values[12]);
    txtWSOrientues.SetText(values[13]);
    txtWSPublik.SetText(values[14]);
    setEnabledPassObject(false);
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: idKomponenteGISWorkspace.toString(), kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

/*Function: SucceededCallbackLidhur     therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes */
function SucceededCallbackLidhur(result, idObjekti) {   
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    var hf = $('#hfKontrollet')[0]; 
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

/*Function: pastrofusha     pastron fushat per shtim dhe ben aktive fushat */
function pastrofusha() {    
    txtWorkspace.SetText('');
    cmbNdermarrje.SetText('');
    txtUrl.SetText('');
    txtUsername.SetText('');
    txtPassword.SetText('');
    txtDatabase.SetText('');
    txtPortDB.SetText('');
    txtUserDB.SetText('');
    txtPasswordDB.SetText('');
    txtKonfirmoPasswordDB.SetText('');
    txtWSFunksionesh.SetText('');
    txtWSDefault.SetText('');
    txtWSOrientues.SetText('');
    txtWSPublik.SetText('');
    setEnabledPassObject(true);
    $('#hfLidhur').val('False');
    aktivFusha(colKontrollet, colAtrTrupi, false);
}

function setEnabledPassObject(value) {
    lblPasswordDB.SetEnabled(value);
    txtPasswordDB.SetEnabled(value);
    lblKonfirmoPasswordDB.SetEnabled(value);
    txtKonfirmoPasswordDB.SetEnabled(value);
}

function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    ASPxGridView_GISWorkspace.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvWorkspace").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    vendosKonfig(result);
}

function vendosKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var colGrida = result.colGrida;
        colKushte = result.colKushte;
        colAlterKusht = result.colAlterKusht;
        var kodniveli = result.kodniveli;
        var konfLlojRreshti = result.konfLlojRreshti;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C");
    }
}

function aktivFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoNdermarrje() {
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    callWebserviceKonfigurimi(idKomponenteGISWorkspace, cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(idKomponenteGISWorkspace, cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('GISWorkspace.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));
}

/*Function: EndRequestHandler    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.*/
function EndRequestHandler(sender, args) {  
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); 
    var hfId = $('#hfId');
    var hfFshirje = $('#hfFshirje');
    var hfVeprimeObj = $("#hfVeprimeObj").val();

    if (hf.val() == 'true' && hfShtimModifikim.val() != 'modifikim' && hfVeprimeObj == '')
        cmbNdermarrje.RemoveItem(cmbNdermarrje.GetSelectedIndex());

    if (hfVeprimeObj != '')
    {
        JSON.parse(hfVeprimeObj).map(function (ndermarrje, index) {
            cmbNdermarrje.AddItem([ndermarrje.NdermarrjeKodi, ndermarrje.NdermarrjePershkrimi], parseInt(ndermarrje.IdNdermarrje));
        });
        $('#hfVeprimeObj').val('');
    }
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_GISWorkspace, idKomponenteGISWorkspace.toString(), pastrofusha, hfTeDrejta, vendosKonfig, resultkonf);
   
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

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    }
    catch (e) { }
};