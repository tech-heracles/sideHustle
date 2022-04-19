; var editorGlobal;
var widthLupaLlogaria = 400;
var heightLupaLlogaria = 400;
var widthLupaKategoriaZbritje = 600;
var heightLupaKategoriaZbritje = 600;
var widthLupaNivelCmimi = 600;
var heightLupaNivelCmimi = 600;
var widthLupaNivelZbritje = 600;
var heightLupaNivelZbritje = 600;
var widthLupaAutorizime = 600, heightLupaAutorizime = 600;
var widthLupaAgjentShitje = 600, heightLupaAgjentShitje = 600;
var identifikuesPerPopupKodifikimin = "Shto_KlientFurnitor";
var identifikuesPerPopupLlogari = "KlientFurnitor";
var identifikuesPerPopupNiveliCmimi = "Shto_KF";
var identifikuesPerPopupNiveliZbritje = "Shto_KF";
var identikuesPerPopupKushtePagese = "Shto_KF";
var identikuesPerPopupKushteDergimi = "Shto_KF";
var identikuesPerPopupMenyraTransporti = "Shto_KF";
var identikuesPerPopupAgjenteShitje = "Shto_KF";
var identikuesPerPopupAfateMaturimi = "Shto_KlientFurnitor";
var identifikuesPerPopupBanka = "Shto_KlientFurnitor";
var heightLupaAutorizime = 600;

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    //  hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    //  hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    if (e.item.name === 'Ruaj') {
        Utils.shfaqLoadingGif();;
        valido(s, e);
    }
    else if (e.item.name == 'Arkiva') {
        ButtonClickArkiva();
        e.processOnServer = false;
    }
    else
        if (e.item.name === 'Anullo') {
            if (pageState.vjenNga == undefined) {
                $.ajax({
                  url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLLupaShpejte"),
                 data: JSON.stringify({ lupa: 'LupaKlientShpejte' })
                }).done(Succeded);
                //window.history.go(lengjth - window.history.length - 1);
                return;
            }
            window.parent.popupUniversal.Hide();
        }
}

function ButtonClickArkiva() {

    var idKf = $('#hfId')[0].value;
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=ListaShpejte&veprimi=kf&kf=' + pageState.lloji + '&idDok=' + idKf
        //+ '&shtim_modifikim=' + pageState.veprimi
        );
    popupUniversal.Show();

}
function AgjentShitjesh_Click() {
    var hf = $("#hfLupaAgjShitje")[0];
    var queryStr = hf.value + '&agjenti=1';
    myButtonClickLupa.AgjentShitjesh_Click(hfState.Get("msgZgjidhAgjentShitjeLupa"), queryStr, widthLupaAgjentShitje, heightLupaAgjentShitje);
}
function niveliChange() {
    var s = btneNivelCmimi.GetText().split(',');
    btneNivelCmimi.SetText(s[1]);
    if (pageState.eshteKlient) {
        if (s[2] === 'Cmim Blerje') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNivelCmimiNukPerdoretKliente"));
            btneNivelCmimi.SetText('');
        }
    } else if (pageState.lloji === 'furnitor') {
        if (s[2] === 'Cmim Shitje') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNivelCmimiNukPerdoretFurnitore"));
            btneNivelCmimi.SetText('');
        }
    }
}
function ButtonClickedParaLlogaria(s) {
    var hf = $("#hfLupaLlogDytesor")[0];
    editorGlobal = s;
    editorLlogaria = s;
    editorPershkrimi = "";
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria('Zgjidh Llogarine', queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
function SucceededCallbackLlogPara(llogaria) {
    if (llogaria != null && llogaria.NrLlogari != -1) {

        if (llogaria.PershkrimiMonedha != lblPershkrimMonedha.GetText()) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgMonLlogParapNjejte"));
            txtLlogDytesor.SetText('');
        }
    }
}
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}
function nrLlogariParaChange() {
    var s = txtLlogDytesor.GetText().split(';');
    txtLlogDytesor.SetText(s[0]);
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
        data: JSON.stringify({ kodi: txtLlogDytesor.GetText(), idNderrmarje: hfState.Get("idNdermarrje")})
    }).done(SucceededCallbackLlogPara);
}
var resultkonf;
var colKontrollet, colAtrTrupi;

function SucceededCallbackKonfig(result) {
    $("#dvKlientFurnitor").show();//$("#dvKlientFurnitor")[0].style.visibility = 'visible';
    //$("#dvKlientFurnitor")[0].style.display = '';
    if (result !== "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, $('#hfShtimModifikim'), '', arrTabela, "ASPxPanel");
        if (pageState.veprimi !== 'modifikim') {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
        if (pageState.eshteKlient) {

            if (cmbBij.GetVisible() && !($('#hfMeme').val() == 'True')) {
                cmbBij.SetVisible(false);
                lblBij.SetVisible(false);
                cmbLlojPorosie.SetVisible(false);
                lblLlojPorosie.SetVisible(false);
            }

        } else {
            cmbBij.SetVisible(false);
            lblBij.SetVisible(false);
            cmbLlojPorosie.SetVisible(false);
            lblLlojPorosie.SetVisible(false);
        }

        if (cmbLlojAdrese.GetSelectedIndex() == -1)
            cmbLlojAdrese.SetSelectedIndex(0);
        if (cmbTitulli.GetSelectedIndex() == -1)
            cmbTitulli.SetSelectedIndex(0);
    }
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaLlogaria");
    var hf4 = $("#hfLupaNivelZbritje");
    var hf9 = $("#hfLupaKategoriZbritje");
    var hf10 = $("#hfLupaNivelCmimi");
    var hf12 = $("#hfLupaAgjShitje");
    for (var i = 0; i < kontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it

        //        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
        //            hf7.val(colAtrTrupi[i].IdKonfigAmbjenteLupa); // kontrollet[i].split(',')[11].toString();
        //            continue;
        //        }
        if (kontrollet[i].KodKontrolli == "txtNr2") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        } if (kontrollet[i].KodKontrolli == "btneKategoriZbritje") {
            hf9.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        } if (kontrollet[i].KodKontrolli == "btneNivelCmimi") {
            hf10.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "btneNivelZbritje") {
            hf4.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbAgjentShitjesh") {
            hf12.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}

function KategoriZbritje_Click() {
    var hf = $("#hfLupaKategoriZbritje")[0];
    myButtonClickLupa.KategoriZbritje_Click(hfState.Get("msgZgjidhKatZbritjeLupa"), hf.value, widthLupaKategoriaZbritje, heightLupaKategoriaZbritje);
}

function NivelCmimi_Click() {
    var hf = $("#hfLupaNivelCmimi")[0];
    myButtonClickLupa.NivelCmimi_Click(hfState.Get("msgNivelCmimiPrindLupa"), hf.value, widthLupaNivelCmimi, heightLupaNivelCmimi, pageState.lloji);
}

function NivelZbritje_Click() {
    var hf = $("#hfLupaNivelZbritje")[0];
    myButtonClickLupa.NivelZbritje_Click(hfState.Get("msgZgjidhNivelZbritjeLupa"), hf.value, widthLupaNivelZbritje, heightLupaNivelZbritje);
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("173", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("173", cmbKonfigurimi.GetText());
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.shtoHandlerSession();
    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
}

var arr = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;
var arradresa=new Array();
var arrkodipostar = new Array();
var countadresa = 0;
var editorValues = new Object();
var idllojadrese = 1;
var arrVlerat = new Array();
var countvlerat = 0;
/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi")[0];
    //    var hfKontrollet = $('#hfKontrollet')[0];
    //    var hfShtimModifikim = $('#hfShtimModifikim')[0]; //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    //    var hfId = $('#hfId')[0];  //hidden fieldi qe ruan id  e rreshtit te selektuar
    //    //            indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_KF, "123")
    //    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_KF, "123", pastrofusha);
    if (hf.value == "true") {
        if (pageState.vjenNga == undefined || pageState.vjenNga == "undefined") {
            $.ajax({               
               url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLLupaShpejte"),
                data: JSON.stringify({ lupa: 'LupaKlientShpejte' })
            }).done(Succeded);
            // window.history.go(lengjth - window.history.length - 1);
            return;
        }
        var grida = window.parent.$("#rowed5");
        var idRow = grida.getLastSel2();
        var rreshtaTeGrides = grida.jqGrid('getRowData');
        var rreshtILire = 0;
        for (i = 0; i < rreshtaTeGrides.length; i++) {
            if (rreshtaTeGrides[i].undefined !== "") {

                if (pageState.vjenNga === "ShtoVeprimBanka") {
                    if (rreshtaTeGrides[i].txtSubjekti.toString().search('value') != -1) {
                        if (window.parent.$('#txtSubjekti' + idRow).val() == "" && window.parent.$('#txtLloji' + idRow).val() == 7 && pageState.eshteKlient) {
                            rreshtILire = idRow;
                            break;
                        }
                        if (window.parent.$('#txtSubjekti' + idRow).val() == "" && window.parent.$('#txtLloji' + idRow).val() == 8 && pageState.lloji === 'furnitor') {
                            rreshtILire = idRow;
                            break;
                        }
                    }
                    else if (rreshtaTeGrides[i].txtSubjekti == "" && (rreshtaTeGrides[i].txtLloji == "Klient") && pageState.eshteKlient) {
                        rreshtILire = grida.jqGrid('getDataIDs')[i];
                        break;
                    }
                    else if (rreshtaTeGrides[i].txtSubjekti == "" && (rreshtaTeGrides[i].txtLloji == "Furnitor") && pageState.lloji === 'furnitor') {
                        rreshtILire = grida.jqGrid('getDataIDs')[i];
                        break;
                    }
                    else if (rreshtaTeGrides[i].txtSubjekti == "" && (rreshtaTeGrides[i].txtLloji == "")) {
                        rreshtILire = grida.jqGrid('getDataIDs')[i];
                        var rreshti = grida.getRowData(rreshtILire);
                        rreshti.txtLloji = pageState.eshteKlient ? 'Klient' : 'Furnitor';
                        rreshti.txtVleraMonBaze = 0;
                        rreshti.txtVleraArketuar = 0;
                        rreshti.txtZbritja = 0;
                        rreshti.txtKreditet = 0;
                        if (window.parent.Utils.getUrlVar('lloji') === "terheqje" || window.parent.Utils.getUrlVar('lloji') === "pagese")
                            rreshti.txtDebiKredi = 'Debi'
                        else rreshti.txtDebiKredi = 'Kredi'
                        if (window.parent.pershkrimi_Memo.GetText() == "")
                            rreshti.txtPershkrimi = "";
                        else rreshti.txtPershkrimi = window.parent.pershkrimi_Memo.GetText();
                        //                        rreshti.txtVlefta = 1;
                       grida.jqGrid('setRowData', rreshtILire, rreshti);
                        var rreshtitjeter = parseFloat(rreshtILire) + 1;
                        if (window.parent.arrayReadOnlyKolonaGrides[window.parent.arrayReadOnlyKolonaGrides.length - 1] == 'True')
                            be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png'  onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                        else
                            be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                        var datarow = { txtFshi: be };
                        var su = grida.jqGrid('addRowData', parseInt(rreshtitjeter), datarow);
                        break;
                    }
                }
                else if (pageState.vjenNga === "Shto_VeprimeKF") {
                    if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1) {
                        if (window.parent.$('#txtKodi' + idRow).val() == "") {
                            rreshtILire = idRow;
                            break;

                        }
                    }
                    else if (rreshtaTeGrides[i].txtKodi == "" && (rreshtaTeGrides[i].cmbDebiKredi != "")) {
                        rreshtILire = grida.jqGrid('getDataIDs')[i];
                        break;
                    }
                    else if (rreshtaTeGrides[i].txtKodi == "" && (rreshtaTeGrides[i].cmbDebiKredi == "")) {
                        rreshtILire = grida.jqGrid('getDataIDs')[i];
                        var rreshti = grida.getRowData(rreshtILire);

                        rreshti.txtVlefta = 1;
                        rreshti.txtVleftaMon = 1;
                        rreshti.dteData = window.parent.dteDtDok.GetText()
                        rreshti.txtPershkrim = window.parent.txtPershkrimi.GetText();

                        grida.jqGrid('setRowData', rreshtILire, rreshti);
                        var rreshtitjeter = parseFloat(rreshtILire) + 1;
                        if (window.parent.arrayReadOnlyKolonaGrides[window.parent.arrayReadOnlyKolonaGrides.length - 1] == 'True')
                            be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png'  onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                        else
                            be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                        var datarow = { txtFshi: be };
                        var su = grida.jqGrid('addRowData', parseInt(rreshtitjeter), datarow);
                        break;
                    }
                }
            }
        }
        if (rreshtILire != 0)
            if (pageState.vjenNga === "ShtoVeprimBanka")
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
                    data: JSON.stringify({ kodi: txtKodi.GetText(), rreshti: rreshtILire, data: window.parent.data_DateEdit.GetDate() })
                }).done(window.parent.SucceededCallbackVleraKodi);
            else if (pageState.vjenNga === "Shto_VeprimeKF")
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
            data: JSON.stringify({ kodi: txtKodi.GetText(), rreshti: rreshtILire, data: window.parent.dteDtDok.GetDate() })
        }).done(window.parent.SucceededCallbackVleraKodi);
        hf.value = "false";
        window.parent.popupUniversal.Hide();
    }
    Utils.hiqLoadingGif();;
}

function Succeded(result) {
    window.location = result;
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

var identifikuesPerPopupLlogari = "KlientFurnitor";

/*
Function: ButtonClickBanka
    
Hap lupen me listen e bankave.
*/
function ButtonClickBanka() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhArkenBankenLupa"), 'LupaBanka.aspx?monedha=' + lblPershkrimMonedha.GetText(), 700, 600);
}

/*
Function: TextChangedBanka
    
Thirret kur ndryshojme banken e selektuar. Kur ndryshon banka ndryshon dhe monedha dhe gjendja sipas bankes se re te zgjedhur.
Shiko funksionin <SucceededCallbackBanka>.
*/
function TextChangedBanka() {
    if (txtEmriBanka.GetValue() != null && txtEmriBanka.GetValue() != "") {
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Konfigurime", "merrBankaSipasKodit"), data: JSON.stringify({ kodBanka: txtEmriBanka.GetText(), idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi }) }).done(SucceededCallbackBanka);
    }
}

function SucceededCallbackBanka(result) {
    txtEmriBanka.SetText(result.KodiBanka);
    lblPershkrimiEmriBanka.SetText(result.EmerBanka);
}

function SucceededCallbackBankaID(result) {
    txtEmriBanka.SetText(result.KodiBanka);
    lblPershkrimiEmriBanka.SetText(result.EmerBanka);
}
function Ndermarje_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniNdermarjeBij"), 'LupaNdermarjeBij.aspx?vjenNga=Shto_KF', widthLupaAutorizime, heightLupaAutorizime);
}
var KPF;
function Autorizime_Click() {
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}

var lengjth = 0;
var pageState = {};
function Init() {
    window.parent.popupUniversal.UpdatePosition();
    lengjth = window.history.length;
    changeName();
    initAdresa();
    //hf2.value = arrkodipostar;
    cmbLlojAdrese.SetSelectedIndex(0);
    var kodi = hfState.Get('Kodi');
    pageState.veprimi = $('#hfShtimModifikim').val();
    pageState.eshteKlient = Utils.getUrlVar("kf") === 'klient';
    pageState.lloji = Utils.getUrlVar("kf");
    pageState.vjenNga = Utils.getUrlVar("vjenNga");
    pageState.idNdermarrje = hfState.Get('idNdermarrje');
    pageState.idPerdoruesi = hfState.Get('idPerdoruesi');
    if (kodi !== "" && kodi != undefined && kodi !== 'undefined' && kodi != null) {
        if (pageState.veprimi === 'modifikim' || pageState.veprimi === 'klonim') {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Celje", "ktheKFSipasKodit"), data: JSON.stringify({ kodi: kodi, idNdermarrje: pageState.idNdermarrje })
            }).done(KonfiguroVleraFillestareModifikim);
        }
    }

}
function initAdresa() {
    arradresa = new Array();
    var llojeAdr = Object.keys(Utils.llojeAdresash);
    for (var j = 0; j < llojeAdr.length; j++) {
        var item = llojeAdr[j];
        arradresa.push({ IdTipAdrese: Utils.llojeAdresash[item], KodiPostar: "", Adresa: "" });

    }
}
function adresat() {
    //ruan te dhenat e adreses ne varesi te llojit
    var editor = Utils.ktheKontroll('txtAdresa');
    var editorA = Utils.ktheKontroll('cmbLlojAdrese');
    var editorPosta = Utils.ktheKontroll('txtKodiPostar');
    var llojadrese = $.grep(arradresa, function(i){
        return i.IdTipAdrese == cmbLlojAdrese.GetValue()});
    txtAdresa.SetText(llojadrese[0].Adresa);
    txtKodiPostar.SetText(llojadrese[0].KodiPostar);
}

function KonfiguroVleraFillestareModifikim(result) {
    if (result != null)
    {
        $('#hfdateHapje')[0].value = result.data;
        mbushFushat(result.kf);
    }
        
}


function mbushFushat(values) {
    initAdresa();
    $('#hfId')[0].value = values.IdKlientFurnitor;
    if (pageState.veprimi === "modifikim")
        txtKodi.SetEnabled(false);
  //if (pageState.veprimi !== "klonim")
        txtKodi.SetText(values.KodKlientFurnitor);
    txtNipt.SetText(values.NiptiKF);
    if (values.IdLlogari != null)
        Utils.SelectComboItem(txtNr2, values.IdLlogari, values.NrLLogKlientFurnitor);
    if (values.IdKatZbritje != null)
        btneKategoriZbritje.SetText(values.KodKatZbritje);
    // Utils.SelectComboItem(btneKategoriZbritje, values.IdKatZbritje, values.KodKatZbritje);
    //if (values.)
    //cmbLlojAdrese.SetSelectedIndex(cmbNjesia1.AddItem(artikulli.PershkrimNjesia1, artikulli.Njesi1Artikulli));
    txtAdresa.SetText(values.AdresaBanka);
    //  txtKodiPostar.SetText(values)
    if (values.EmriBanka != null)
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrBankaSipasId"),
        data: JSON.stringify({ idbanka: values.EmriBanka })
    }).done(SucceededCallbackBankaID);
    //  txtEmriBanka.SelectComboItem(txtEmriBanka, values.IDBANKF, values.LlogariBankareKF);
    txtTel.SetText(values.TelKF);
    txtEmerKerkimi.SetText(values.EmerKerkimiKF);
    txtEmertimi.SetText(values.EmertimiKF);
    lblPershkrimMonedha.SetText(values.Monedha);
    txtKategoriPerqindja.SetText(values.PerqindjeKatZbritje);
    if (values.IdNivelCmimi != null)
        btneNivelCmimi.SetText(values.PershkrimNivelCmimi);
    if (values.IdLlogariDytesore != null)
        txtLlogDytesor.SetText(values.NrLlogDytesor);
    //  Utils.SelectComboItem(btneNivelCmimi,values.IdNivelCmimi, values.PershkrimNivelCmimi);
    if (values.PershkrimNivelZbritje != null)
        btneNivelZbritje.SetText(values.PershkrimNivelZbritje);
    //  Utils.SelectComboItem(btneKategoriZbritje,values.ZbritjeAnalitike, values.ZbritjeAnalitike);
    if (values.Idgrupim1kf != null)
        btneGrupimi1.SetText(values.Grupim1KF);
    if (values.Idgrupim2kf != null)
        btneGrupimi2.SetText(values.Grupim2KF);
    if (values.Idgrupim3kf != null)
        btneGrupimi3.SetText(values.Grupim3KF);
    // Utils.SelectComboItem(btneGrupimi, values.Idgrupim1kf, values.Grupim1KF);
    txtEmail.SetText(values.EmailKF);
    txtQyteti.SetText(values.EmriQytetitKF);
    cmbAgjentShitjesh.SetText(values.KodPerfaqesuesShitje);
    txtLimitiParalajmerues.SetText(values.LimitParalajmerues);
    txtLimitiBllokues.SetText(values.LimitBllokues);
    txtAktiviteti.SetText(values.AktivitetiKF);    
    cmbTitulli.SetValue(values.TitulliKF);
    if(values.IdMetoda != -1)
        cmbMetoda.SetValue(values.IdMetoda);
    txtKodiISKSH.SetText(values.KodiISKSH);
    cbMeDogane.SetChecked(values.MeDogane);

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');

    $.ajax({
    pritPergjigje: true,
    url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
    data: JSON.stringify({ idkomp: "123", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values.IdKlientFurnitor, idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values.IdKlientFurnitor) });
    callWebserviceAdresatKF(values.KodKlientFurnitor);
    txtEmertimFature.SetText(values.EmertimFature);

}
function callWebserviceAdresatKF(name) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheAdresatKlientFurnitor"), data: JSON.stringify({ prefixText: name, idNdermarrje: hfState.Get('idNdermarrje')})
    }).done(SucceededCallbackAdresatKF);
}
function SucceededCallbackAdresatKF(result) {
    cmbLlojAdrese.SetSelectedIndex(0);

    for (var i = 0; i < result.length; i++) {
        var item = result[i];
        for (var j = 0; j < arradresa.length; j++) {
            if (arradresa[j].IdTipAdrese !== item.IdTipAdrese)
                continue;
            arradresa[j].Adresa = item.Adresa;
            arradresa[j].KodiPostar = item.KodiPostar;
        }
    }

    var adresaLlojiPare = $.grep(arradresa, function (e) { return e.IdTipAdrese == Utils.llojeAdresash.Biznesi; });
    txtAdresa.SetText(adresaLlojiPare[0].Adresa);
    txtKodiPostar.SetText(adresaLlojiPare[0].KodiPostar);

    var hf1 = $("#hfAdresa");
    hf1.val(JSON.stringify(arradresa));
}

function ndryshoVlereAdreseKodiPostar(s, e) {
    for (var i = 0; i < arradresa.length; i++) {
        if (arradresa[i].IdTipAdrese == cmbLlojAdrese.GetValue()) {
            arradresa[i].Adresa = txtAdresa.GetText();
            arradresa[i].KodiPostar = txtKodiPostar.GetText();
        }
    }

    var hf2 = $("#hfAdresa");
    hf2.val(JSON.stringify(arradresa));
}
//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
   
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}
function aktivFusha(colKontrollet, colAtrTrupi, isLidhur) {
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, $('#hfShtimModifikim'), isLidhur, '#ASPxPageControl1_');

}

var editorLlogaria
function LostFocusNrLlogarieTxtNr2(editor) {
    txtNr2.EndUpdate();
}

function nrLlogariChange() {
    var s = txtNr2.GetText().split(';');
    if (s.length == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheMonedheLlogSipasKodit"),
            data: JSON.stringify({ kodi: s[0] })
        }).done(function (result) {

            var mondedhaKodi = result;
            if (mondedhaKodi != null) {
                lblPershkrimMonedha.SetText(mondedhaKodi);
            } else {
                lblPershkrimMonedha.SetText("Nope");
            }
        });
    }
    else {
        lblPershkrimMonedha.SetText(s[2]);
    }
    txtNr2.SetText(s[0]);
}
function validateLLogariKod() {
    var s = txtNr2.GetText().split(';');
    if (s.length == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheMonedheLlogSipasKodit"),
            data: JSON.stringify({ kodi: s[0] })
        }).done(function (result) {
            var monedhaKodi = result;
            txtNr2.isValid = monedhaKodi != null;
        });
    }
}
     


function ButtonClickedLlogaria(s) {
    var hf = $("#hfLupaLlogaria")[0];
    editorGlobal = s;
    editorLlogaria = s;
    editorPershkrimi = lblPershkrimMonedha;
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function valido(s, e) {
    myFaqeCelje.validim(s, e);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function nrLlogariChange() {
    var s = txtNr2.GetText().split(';');
    txtNr2.SetText(s[0]);
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
        data: JSON.stringify({ kodi: txtNr2.GetText(), idNderrmarje: hfState.Get("idNdermarrje") })
    }).done(SucceededCallbackLlog);
    lblPershkrimMonedha.SetText(s[2]);
}

function SucceededCallbackLlog(llogaria) {
    if (llogaria != null && llogaria.NrLlogari != -1) {
        lblPershkrimMonedha.SetText(llogaria.PershkrimiMonedha);
    }
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}

var grida
function GrupiKF_Click(llojGrupi) {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhgrupinkflupa"), 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + llojGrupi + '&kf=' + pageState.lloji, 700, 500);
}
function txtEmertimiTextChanged(s, e) {
    var tmp = Utils.hiqEnter(s.GetText());
    s.SetText(tmp);
    txtEmertimi.SetText(tmp);
    //txtEmertimi3.SetText(tmp);
    //txtEmertimi4.SetText(tmp);
    //txtEmertimi5.SetText(tmp);
    //txtEmertimi6.SetText(tmp);
    //txtEmertimi7.SetText(tmp);
}