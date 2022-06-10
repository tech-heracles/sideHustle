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
var widthLupaViti = 600, heightLupaViti = 600;
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Ndermarrjet, "129", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Ndermarrjet, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Ndermarrjet, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Ndermarrjet, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Ndermarrjet, indexSel);
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
    if (e.item.name === 'Ruaj') {
        myMesazh.vendosClient();
    }
    else if (e.item.name === "NdryshoLlojLicence") {
        popLlojLicence.Show();
        e.processOnServer = false;
        cmbLicenca.SetVisible(false);
        lblLicenca1.SetVisible(false);
        ButtonOk2.SetVisible(false);
    }
}

function PoClick(s, e) {//po
    popLlojLicence.Show();
    //e.processOnServer = false;
    ButtonOk3.SetVisible(false);
}

function JoClick(s, e) {    
    //e.processOnServer = true;
    Utils.shfaqLoadingGif();;
    btnVazhdoRuajtje.DoClick();
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = ASPxGridView_Ndermarrjet.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(MsgZgjidhNdermarrje.Get("MsgAdministrimZgjidhNdermarrje"));
    else ASPxGridView_Ndermarrjet.GetRowValues(indexModifiko, 'IdNdermarrje;NdermarrjeKodi;NdermarrjePershkrimi;NdermarrjeVendi;NdermarrjeNipt;NdermarrjeMonedha;NdermarrjeMonedhaPershkrimi;NdermarrjeQyteti;NdermarrjeQytetiPershkrimi;NdermarrjeTel;NdermarrjeFax;NdermarrjeEMail;NdermarrjeLicenca;NdermarrjeKodiFiskal;IdViti;NrTvsh;KodViti;Prind;IdPrindi;IdGrupi;Mema;Grupi;LimitiShitjes;OwnShop;Logu;Raportuesi;RaportuesKodi;KodiLicenca;Fiskalizim;Kodbiznesi', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheQytetePerNdermarrje"),
        data: JSON.stringify({idNdermarrje: values[0] })
    }).done(function (result) { SucceededCallbackQytete(result, values[7], values[8]) });

    $('#hfId')[0].value = values[0];
    kodi_TextBox.SetText(values[1]);
    pershkrimi_TextBox.SetText(values[2]);
    nipt_TextBox.SetText(values[4]);
    txtNrTvsh.SetText(values[15]);
    licenca_TextBox.SetText(values[12]);
    kodi_fiskal_TextBox.SetText(values[13]);
    txtViti.SetText(values[16]);//hiqet elementi me te njejtin pershkrim dhe shtohet elementi i ri me id e re
    txtViti.RemoveItem(txtViti.GetSelectedIndex());
    txtViti.SetSelectedIndex(txtViti.AddItem(values[16], values[14]));
    monedha_ASPxComboBox.SetText(values[6]);
    monedha_ASPxComboBox.RemoveItem(monedha_ASPxComboBox.GetSelectedIndex());
    monedha_ASPxComboBox.SetSelectedIndex(monedha_ASPxComboBox.AddItem(values[6], values[5]));
    email_TextBox.SetText(values[11]);
    vendi_TextBox.SetText(values[3]);
    cbPrind.SetChecked(values[17]);
    cbOwnShop.SetChecked(values[23]);
    cbLogu.SetChecked(values[24]);
    if (values[20] !== -1 && values[20]!=null)
        cmbMeme.SetSelectedIndex(cmbMeme.AddItem(values[20], values[18]));
    else cmbMeme.SetValue(null);
    if (values[21] !== -1)
        txtGrupi.SetSelectedIndex(txtGrupi.AddItem(values[21], values[19]));
    else
        txtGrupi.SetValue(null);
    txtLimitiShitjes.SetText(values[22]);
    tel_TextBox.SetText(values[9]);
    fax_TextBox.SetText(values[10]);
    cmbRaportuesi.SetValue(values[25]);
    cmbRaportuesi.SetText(values[26]);
    txtKodLicenca.SetText(values[27]);
    cbfiskalizimi.SetChecked(values[28])
    txtkodbiznesi.SetText(values[29]);
    PageControl.GetTab(3).SetVisible(false);
    ucEmerSkedari.SetVisible(true);
    lblCertifikata.SetVisible(true);
    upload.SetVisible(true);
    pastro.SetVisible(true);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMeTvshNdermarrje"),
        data: JSON.stringify({ idndermarje: $('#hfId').val() })
    }).done(SucceededCallbackMeTvshNdermarrja);
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "129", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrImazh"),
        data: JSON.stringify({ idndermarje: values[0] })
    }).done(SucceededCallbackImazhi);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SucceededCallbackMeTvshNdermarrja(values) {
    cbMeTvsh.SetChecked(values)
}
function SucceededCallbackQytete(result,vleraQytetit,qytetiDefault) {

    Utils.MbushCombo(qyteti_ASPxComboBox, result.colQyt, "IdQyteti", "EmriQyteti");
    if (vleraQytetit !== -1)
        qyteti_ASPxComboBox.SetText(qytetiDefault);
    else qyteti_ASPxComboBox.SetValue(null);
}

function SucceededCallbackQyteteDefault(result) {
    Utils.MbushCombo(qyteti_ASPxComboBox, result.colQyt, "IdQyteti", "EmriQyteti");
    qyteti_ASPxComboBox.SetValue(null);
    localStorage.setItem('qyteteDefault', JSON.stringify(result));
}

function SucceededCallbackImazhi(result) {
    if (!ASPxGridView_Ndermarrjet.InCallback())
        btnShfaqImazh.DoClick();
    // ppp.SetValue(result);            
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
   
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }

    //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    kodi_TextBox.SetText('');
    pershkrimi_TextBox.SetText('');
    nipt_TextBox.SetText('');
    txtLimitiShitjes.SetText('0');
    txtNrTvsh.SetText('');
    licenca_TextBox.SetText('');
    kodi_fiskal_TextBox.SetText('');
    monedha_ASPxComboBox.SetValue(null);
    txtViti.SetValue(null);
    email_TextBox.SetText('');
    vendi_TextBox.SetText('');
    if (localStorage.getItem('qyteteDefault') != null) {
        var qyteteDefault = JSON.parse(localStorage.getItem('qyteteDefault')).colQyt;
        Utils.MbushCombo(qyteti_ASPxComboBox, qyteteDefault, "IdQyteti", "EmriQyteti");
        qyteti_ASPxComboBox.SetValue(null);
    } else {
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "ktheQytetePerNdermarrje"),
            data: JSON.stringify({ idNdermarrje: -1 })
        }).done(function (result) { SucceededCallbackQyteteDefault(result) });
    }
    tel_TextBox.SetText('');
    fax_TextBox.SetText('');
    cbPrind.SetChecked(false); cbOwnShop.SetChecked(false); cbLogu.SetChecked(false);
    cmbMeme.SetValue(null);
    txtGrupi.SetValue(null);
    cmbRaportuesi.SetSelectedIndex(-1);
    cmbRaportuesi.SetValue(null);
    txtKodLicenca.SetText('');
    PageControl.GetTab(3).SetVisible(true);
    ucEmerSkedari.SetVisible(false);
    lblCertifikata.SetVisible(false);
    upload.SetVisible(false);
    pastro.SetVisible(false);
    endCallback();
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    cmbLlojNdermarje.SetEnabled(true);
    cmbNdermarjet.SetEnabled(true);
    cbfiskalizimi.SetChecked(false);
    txtkodbiznesi.SetText('');
    ucEmerSkedari.ClearText();
}

function endCallback() {
    if (PageControl.GetActiveTabIndex() == 4) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "merrImazh"),
            data: JSON.stringify({ idndermarje: $('#hfId').val() })
        }).done(SucceededCallbackImazhi);
    }
    else {
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function mbyllModal() {
    $(".dialog-pajisje").css("display", "none");
    $(".bg-pajisje").css("display", "none");
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
    ASPxGridView_Ndermarrjet.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvNdermarja").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;
//        var colAlterKusht;
//        var colKushte;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        //                var colGrida = result[2];
        //                colKushte = result[3];
        //                colAlterKusht = result[4];
        //                var kodniveli = result[5];
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblNdermarja', 'tblKontakti', 'tblModel'];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
        //                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //                ASPxGridView_Ndermarrjet.PerformCallback("129" + ";" + cmbKonfigurimi.GetText());
        if ($('#hfShtimModifikim').val() == "shtim" ){
            ucEmerSkedari.SetVisible(false);
            lblCertifikata.SetVisible(false);
            upload.SetVisible(false);
            pastro.SetVisible(false);
        }

    }

  //  $("#dvNdermarja").show();//$("#dvNdermarja")[0].style.visibility = 'visible';
}
//        function SucceededCallbackKonfigurimi(result) {
//            if (result != "") {
//                var vlerat = '';
//                resultkonf = result;
//                vlerat = result.split('*');
//                var kontrollet = vlerat[0].split(';');
//                var hf = $('#hfKontrollet')[0];
//                var hfLidhur = $("#hfLidhur")[0];
//                var hfMod = $('#hfShtimModifikim')[0];
//                var arrTabela = ['tblNdermarja', 'tblKontakti'];
//                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
////                ASPxGridView_Ndermarrjet.PerformCallback("129" + ";" + cmbKonfigurimi.GetText());
//            } $("#dvNdermarja")[0].style.visibility = 'visible';
//        }

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //            var hfLidhur = $("#hfLidhur")[0];
    //            myFaqeCelje.aktivizoFusha(vlerat, hfMod, isLidhur, '#ASPxPageControl1_');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("129", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("129", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
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
    changeName();
});

function changeName() {
    //   aspxPreviewImgSrc = previewImage.ImageUrl;
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Ndermarrje.aspx', 0, hf);
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
    //            indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Ndermarrjet, "129")
    if (cmbRaportuesi.IsVisible() && (hfState.Contains('idNdermERe') && hfState.Contains('kodNdermERe')) && (hfState.Get("idNdermERe") !== "" && hfState.Get("kodNdermERe") !== "")) {
        var idNdermRe = hfState.Get("idNdermERe");
        var ndermKodi = hfState.Get("kodNdermERe");
        cmbRaportuesi.AddItem(ndermKodi, idNdermRe);
        hfState.Set("idNdermERe", "");
        hfState.Set("kodNdermERe", "");
    }
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Ndermarrjet, "129", pastrofusha, hfTeDrejta);
}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
    if (Utils.IsNullOrEmpty(txtLimitiShitjes.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniLimitinShitjesKupon"));
        e.processOnServer = false;
        return;
    }
    if (txtLimitiShitjes.GetText() < 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLimitiShitjesKuponNegative")); 
        e.processOnServer = false;
        return;
    }
    if (isNaN(parseFloat(txtLimitiShitjes.GetText()))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLimitiShitjesKuponNumer"));
        e.processOnServer = false;
        return;
    }
        
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var editorViti;
var grida;
function Viti_Click() {//thiret popup i grupe banke
    myButtonClickLupa.LupaUniversal_Click('Zgjidh vitin', 'LupaViti.aspx', widthLupaViti, heightLupaViti);

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
//        var aspxPreviewImgSrc = getPreviewImageElement().src;
var aspxPreviewImgSrc;

function Ngarkuesi_NeNgarkimFillim() {
    btnNgarko.SetEnabled(false);
}
function Ngarkuesi_NeFileNgarkimPlotesuar(args) {
    var imgSrc = aspxPreviewImgSrc;
    if (args.isValid) {
        var date = new Date();
        imgSrc = "images/" + args.callbackData + "?dx=" + date.getTime();
    }
    //   previewImage.SetImageUrl(imgSrc);
    //            getPreviewImageElement().src = imgSrc;
}

function Ngarkuesi_NeFiletNgarkimPlotesuar(args) {
    UpdateButoniNgarkim();
}
function UpdateButoniNgarkim() {
    btnNgarko.SetEnabled(ngarkuesi.GetText(0) != "");

}
function shfaqPopupPerPajisjetElektronike(s, e) {
    if($("#ASPxPageControl1_ucEmerSkedari_TextBox0_FakeInput").length > 0){
        var str = $("#ASPxPageControl1_ucEmerSkedari_TextBox0_FakeInput").val();
        var inputExtension = str.substring(str.indexOf('.') + 1);

        if (inputExtension == "p12") {
            var opsionMbyllje = hfState.Get("MenuItemMbyll");
            var popUpOptions = { prependSelector: "body", dialogClass: "dialog-pajisje", contentClass: "tabele-pajisje", titulli: "Njoftim", text: { mbyll: opsionMbyllje } };
            Utils.ndertoPopupPerPajisjetElektronike(popUpOptions, "test");
        }
    }
    
   
}
function getPreviewImageElement() {
    return document.getElementById("previewImage");
}
function tabsActiveTabChanged(s, e) {
    indexModifiko = ASPxGridView_Ndermarrjet.GetFocusedRowIndex();
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
    if (e.tab.index !== 0 && $('#hfSuperUser').val() == 'True')
        ASPxMenu1.GetItemByName('NdryshoLlojLicence').SetVisible(true);
    else ASPxMenu1.GetItemByName('NdryshoLlojLicence').SetVisible(false);

    if (e.tab.index == 4)
        ASPxMenu1.GetItemByName('HiqLogo').SetVisible(true);
    else ASPxMenu1.GetItemByName('HiqLogo').SetVisible(false);
}
function Grupi_Click() {//thiret popup i grupe banke

    myButtonClickLupa.LupaUniversal_Click('Zgjidh grupin e ndermarrjes', 'LupaGrupNdermarrje.aspx', 600, 600);
}

function Prind_SelectIndexChanged(s, e) {

    if (s.GetText() != '') {
        cbPrind.SetEnabled(false); //cbOwnShop.SetEnabled(true);
        cbPrind.SetChecked(false);
        cmbLlojNdermarje.SetEnabled(false);
        cmbNdermarjet.SetEnabled(false);
    }
    else {
        cbPrind.SetEnabled(true); //cbOwnShop.SetEnabled(false);
        cmbLlojNdermarje.SetEnabled(true);
        cmbNdermarjet.SetEnabled(true);

    }
}

function Prind_CheckedChanged(s, e) {

    cmbMeme.SetEnabled(!s.GetChecked());
    if (s.GetChecked()) {
        cmbMeme.SetText('');
        cbOwnShop.SetEnabled(false);
        cbOwnShop.SetChecked(false);
    } else {
        cbOwnShop.SetEnabled(true);
    }
}