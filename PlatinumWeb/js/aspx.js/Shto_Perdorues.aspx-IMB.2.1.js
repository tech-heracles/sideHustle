;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e roleve ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var roletNew;
//metodat per te hapur faqen e modifikimit me double click
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_ListPerdoruesit, "100", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}


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
                break;
            default:
                break;
        }
    });
});

function ShowHideAmbjentMobile() {
    if (cmbAmbjenti.GetSelectedItem() && cmbAmbjenti.GetSelectedItem().text == 'Mobile')
        cmbAmbjentiMobile.SetEnabled(true);
    else {
        cmbAmbjentiMobile.SetEnabled(false);
        cmbAmbjentiMobile.SetSelectedIndex(-1);
    }
}
/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    hfState.Set("dergoEmailNdryshimRoli", false);
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    if (hfShtimModifikim.val() != "modifikim") {
        password_TextBox.validationGroup = "entries";
        konfirmo_Textbox.validationGroup = "entries";
    }
    else {
        password_TextBox.validationGroup = "aaa";
        konfirmo_Textbox.validationGroup = "aaa";
    }
    if (cmbAmbjenti.GetSelectedItem() && cmbAmbjenti.GetSelectedItem().text == 'Mobile' && cmbAmbjentiMobile.GetSelectedItem().text == '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get('msgambjentindefaultmodulimobile'));
        e.processOnServer = false;
        return;
    }
  
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
   
    if (e.item.name == "Ruaj" && hfShtimModifikim.val() == "modifikim") {
        if (!myFaqeCelje.validim(s, e))
            return;
        try {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRoletSipasPerdoruesit"),
                data: JSON.stringify({ idPerdoruesi: hfId.val() })
            }).done(SucceededCallbackRoletPerd);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        }
            e.processOnServer = false;
            //setTabRoleVisibility(true);
    }
    if (hfShtimModifikim.val() == "shtim") {
        lblPasswordieksistues.SetVisible(false);
        txtPasswordieksistues.SetVisible(false);
        //cbPassPerkohshem.SetChecked(true);
        cbKycurMobile.SetChecked(true);
        AktivizoFushatEPasswordit();
        cbShfaqNjoftime.SetChecked(false);
        // setTabRoleVisibility(true);
    }
}
function AktivizoFushatEPasswordit()
{
    if (hfState.Get("gjeneroPassword") == true) {
        password_TextBox.SetText(hfState.Get("passwordiGjeneruar"));
        konfirmo_Textbox.SetText(hfState.Get("passwordiGjeneruar"));
        password_TextBox.SetEnabled(false);
        konfirmo_Textbox.SetEnabled(false);
    }
}

//merr te dhenat e rreshtit te selektuar
//function mbushfusha() {
//    mbush = false;
//    $('#hfShtimModifikim')[0].value = "modifikim";
//    indexModifiko = grid_ListPerdoruesit.GetFocusedRowIndex();
//    if (indexModifiko == -1)
//        myMesazh.ShtoMesazhGabimi(hfState.Get('msgPerdoruesitZgjidhniNjePerdorues'));
//    else
//        grid_ListPerdoruesit.GetRowValues(indexModifiko, 'IdPerdorues;IdQyteti;EmriPerdorues;MbiemriPerdorues;PerdoruesAktiv;PerdoruesUsername;PerdoruesTel;PerdoruesFax;PerdoruesEmail;PerdoruesAdresa;IdGjuha;IdAmbjent;PassIPerkohshem;Kycur;KONTROLLOPASSWORD;SHFAQDTPRINTIMI;KycurMobile;SHFAQPERDORUESMENU;SHFAQMESAZHEPOPUP;IdAmbjentMobile;ShopCode;ShopName;DealerName;UseriCRM;UserEtopUP;IDETopUp;TypeOfDevice;SalesRepMobileNumber;SalesRepMPesaMSISDN;Gjinia;SalesRepStartDateVod;SalesRepTrainingStart;SalesRepStartDateShop;SalesRepMaternityLeaveStart;LeaveDateVod;LeaveDateShop;MaternityLeaveEndDate;TrainingEndDate;CommentsRetailSales;AccountExecutive;IDNumber;IsInsured;CommentsRetailOpSpecialist;RegionalSupervisor;RetailSalesAccountExecutive;RetailSalesAreaManager;Birthdate;SiteCode;District;ShopMainCode;Latitude;Longitude;PershkrimStatusi;PershkrimLeaveReason;PershkrimUniform;Shenime;StatusAprovimi;NjoftimEmailAprovim', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
//}

function mbushfusha() {
    $('#hfShtimModifikim')[0].value = "modifikim";
    if ($('#hfIndexId').val() != '-1' && $('#hfIndexId').val() != '') {
        mbush = true;
        indexModifiko = parseInt($('#hfIndexId').val());
        kaloTab = true;
    }
    else {
        mbush = false;
        indexModifiko = grid_ListPerdoruesit.GetFocusedRowIndex();
    }
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje perdorues!');
    else {
        SelektoPerdorues(indexModifiko);
    }
}

var arrayRoletSelektuar = new Array();

function setTabRoleVisibility(visible)
{
    PageControl.GetTab(3).SetVisible(visible);
}
function SelektoPerdorues(indexi) {
    LoadingPanel.Show();
    grid_ListPerdoruesit.GetRowValues(indexi, 'IdPerdorues;IdQyteti;EmriPerdorues;MbiemriPerdorues;PerdoruesAktiv;PerdoruesUsername;PerdoruesTel;PerdoruesFax;PerdoruesEmail;PerdoruesAdresa;IdGjuha;IdAmbjent;PassIPerkohshem;Kycur;KONTROLLOPASSWORD;SHFAQDTPRINTIMI;KycurMobile;SHFAQPERDORUESMENU;SHFAQMESAZHEPOPUP;IdAmbjentMobile;ShopCode;ShopName;DealerName;UseriCRM;UserEtopUP;IDETopUp;TypeOfDevice;SalesRepMobileNumber;SalesRepMPesaMSISDN;Gjinia;SalesRepStartDateVod;SalesRepTrainingStart;SalesRepStartDateShop;SalesRepMaternityLeaveStart;LeaveDateVod;LeaveDateShop;MaternityLeaveEndDate;TrainingEndDate;CommentsRetailSales;AccountExecutive;IDNumber;IsInsured;CommentsRetailOpSpecialist;RegionalSupervisor;RetailSalesAccountExecutive;RetailSalesAreaManager;Birthdate;SiteCode;District;ShopMainCode;Latitude;Longitude;PershkrimStatusi;PershkrimLeaveReason;PershkrimUniform;Shenime;StatusAprovimi;NjoftimEmailAprovim;IDKONFIGKASA', function (values) {
        if ($('#hfShtimModifikim').val() == "shtim")
            return;
       //setTabRoleVisibility(hfState.Get("MMRT") == "Jo" || hfState.Get("PerdoruesUsername") != values[5]);//nese po hapet perdoruesi te behet i padukshem tabi role
        $('#hfId').val(values[0]);
        emri_TextBox.SetText(values[2]);
        txtEmri2.SetText(values[2]);
        mbiemri_TextBox.SetText(values[3]);
        txtMbiemri2.SetText(values[3]);
        username_TextBox.SetText(values[5]);
        aktiv_CheckBox.SetChecked(values[4]);
        email_TextBox.SetText(values[8]);
        adresa_TextBox.SetText(values[9]);
        cmbGjuha.SetValue(values[10]);
        if (values[11] != null) 
            cmbAmbjenti.SetValue(values[11]);
        else cmbAmbjenti.SetSelectedIndex(0);

        if (values[1] != -1)
            qyteti_ASPxComboBox.SetValue(values[1]);
        else qyteti_ASPxComboBox.SetValue(null);
        tel_TextBox.SetText(values[6]);
        fax_TextBox.SetText(values[7]);
        //lblPasswordieksistues.SetVisible(true);
        //txtPasswordieksistues.SetVisible(true);
        ASPxGridView_Autorizimet.PerformCallback(indexModifiko);
        password_TextBox.SetText('');
        txtPasswordieksistues.SetText('');
        konfirmo_Textbox.SetText('');
        cbPassPerkohshem.SetChecked(values[12]);
        txtPasswordieksistues.SetEnabled(!values[12]);
        cbKycur.SetChecked(values[13]);
        kontrolloPassword_CheckBox.SetChecked(values[14]);
        dtPrintimi_CheckBox.SetChecked(values[15]);
        cbKycurMobile.SetChecked(values[16]);
        shfaqPerdoruesMenu_CheckBox.SetChecked(values[17]);
        cbShfaqNjoftime.SetChecked(values[18]);
        if (values[19] != null)
            cmbAmbjentiMobile.SetValue(values[19]);
        else cmbAmbjentiMobile.SetSelectedIndex(0);

        txtKodiDyqanit.SetText(values[20]);
        txtShopName.SetText(values[21]);
        txtDealer.SetText(values[22]);
        txtUserCRM.SetText(values[23]);
        txtUserEtopUP.SetText(values[24]);
        txtIDETopUp.SetText(values[25]);
        txtTypeDeviceSalesRep.SetText(values[26]);
        txtSalesRepMobile.SetText(values[27]);
        txtSalesRepMPesaMSISDN.SetText(values[28]);

        if (values[29] != null || values[29] != "")
            cmbGender.SetValue(values[29]);
        else cmbGender.SetSelectedIndex(0);
        dteSalesRepStartDate.SetDate(values[30]);
        if (values[30] != null)
            dteSalesRepStartDate.SetEnabled(false);
        else {
            var id = Utils.findInArray(colKontrollet, function (item) { return item.KodKontrolli == "dteSalesRepStartDate"; }).IdKontrolli;
            dteSalesRepStartDate.SetEnabled(Utils.findInArray(colAtrTrupi, function (item) { return item.IdKontroll == id; }).Enabled);
        }
        dteSalesRepTrainingDate.SetDate(values[31]);
        dteSalesRepStartDateShop.SetDate(values[32]);
        dteSalesRepStartMaternityLeave.SetDate(values[33]);
        dteLeaveDateVodafoneVod.SetDate(values[34]);
        if (values[34] != null)
            dteLeaveDateVodafoneVod.SetEnabled(false);
        else {
            var id = Utils.findInArray(colKontrollet, function (item) { return item.KodKontrolli == "dteLeaveDateVodafoneVod"; }).IdKontrolli;
            dteLeaveDateVodafoneVod.SetEnabled(Utils.findInArray(colAtrTrupi, function (item) { return item.IdKontroll == id; }).Enabled);
        }
        dteLeaveDateShop.SetDate(values[35]);
        dteMaternityLeaveEndDate.SetDate(values[36]);
        dteTrainingEndDate.SetDate(values[37]);
        txtComRetSal.SetText(values[38]);
        txtAccountExecutive.SetText(values[39]);
        txtIDNumber.SetText(values[40]);
        if (values[41] != null || values[41] != "")
            cmbIsInsured.SetValue(values[41]);
        else cmbIsInsured.SetSelectedIndex(0);
        txtComROS.SetText(values[42]);
        txtRegSup.SetText(values[43]);
        txtRetailSAE.SetText(values[44]);
        txtRetailSAM.SetText(values[45]);
        dteBirthday.SetDate(values[46]);
        txtSiteCode.SetText(values[47]);
        txtDistrict.SetText(values[48]);
        txtShopMainCode.SetText(values[49]);
        txtLatitude.SetText(values[50]);
        txtLongitude.SetText(values[51]);
        if (values[52] != null) {
            var status = cmbStatus.FindItemByText(values[52]);
            cmbStatus.SetSelectedIndex(status != null ? status.index : -1);
        }
        else cmbStatus.SetSelectedIndex(-1);

        if (values[53] != null) {
            var leaveReason = cmbLeaveReason.FindItemByText(values[53]);
            cmbLeaveReason.SetSelectedIndex(leaveReason != null ? leaveReason.index : -1);
        }
        else cmbLeaveReason.SetSelectedIndex(-1);

        if (values[54] != null) {
            var uniform = cmbUniform.FindItemByText(values[54]);
            cmbUniform.SetSelectedIndex(uniform != null ? uniform.index : -1);
        }
        else cmbUniform.SetSelectedIndex(-1);

        txtShenime.SetText(values[55]);
        if (values[56] != null)
            cmbStatusAprovimi.SetValue(values[56]);
        else cmbStatusAprovimi.SetSelectedIndex(0);

        cbNjoftimEmailAprovim.SetChecked(values[57]);
        cmbKonfigKase.SetValue(values[58]);
        gvRolet.PerformCallback(indexModifiko);
        //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
            data: JSON.stringify({ idkomp: "100", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
        }).done(function (result) { SucceededCallbackLidhur(result, values[0]); });
        if (kaloTab) {
            PageControl.SetActiveTabIndex(1);
            myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
        }
    }
    );
}
//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
   
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}


//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    var hfKontrollet = $('#hfKontrolletNrAutom')[0];
    var kontrollet = $('#hfKontrolletNrAutom')[0].value.split(';');

   
    aktiv_CheckBox.SetChecked(true);
    qyteti_ASPxComboBox.SetValue(null); cmbGjuha.SetSelectedIndex(0);
    lblPasswordieksistues.SetVisible(false);
    txtPasswordieksistues.SetVisible(false);
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    ASPxGridView_Autorizimet.PerformCallback(-1);
    gvRolet.PerformCallback(-1);
    cmbAmbjenti.SetText('');
    cmbAmbjentiMobile.SetText('');
    emri_TextBox.SetText('');
    mbiemri_TextBox.SetText('');
    txtEmri2.SetText('');
    txtMbiemri2.SetText('');
    username_TextBox.SetText('');
    email_TextBox.SetText('');
    adresa_TextBox.SetText('');
    tel_TextBox.SetText('');
    fax_TextBox.SetText('');
    password_TextBox.SetText('');
    if (hfShtimModifikim.val() == "shtim") {
        lblPasswordieksistues.SetVisible(false);
        txtPasswordieksistues.SetVisible(false);
    }
    txtPasswordieksistues.SetText('');
    konfirmo_Textbox.SetText('');
    cbKycur.SetChecked(false);
    cbPassPerkohshem.SetChecked(false);
    cbKycurMobile.SetChecked(true);
    kontrolloPassword_CheckBox.SetChecked(true);
    cbShfaqNjoftime.SetChecked(false);
    //shfaqPerdoruesMenu_CheckBox.SetChecked(false);
    setTimeout(AktivizoFushatEPasswordit, 10);
    txtKodiDyqanit.SetText('');
    txtShopName.SetText('');
    txtDealer.SetText('');
    txtUserCRM.SetText('');
    txtIDETopUp.SetText('');
    txtUserEtopUP.SetText('');
    txtTypeDeviceSalesRep.SetText('');
    txtSalesRepMobile.SetText('');
    txtSalesRepMPesaMSISDN.SetText('');
    cmbGender.SetValue('');
    dteSalesRepStartDate.SetText('');
    dteSalesRepTrainingDate.SetText('');
    dteSalesRepStartDateShop.SetText('');
    dteSalesRepStartMaternityLeave.SetText('');
    dteLeaveDateVodafoneVod.SetText('');
    dteLeaveDateShop.SetText('');
    dteMaternityLeaveEndDate.SetText('');
    dteTrainingEndDate.SetText('');
    txtComRetSal.SetText('');
    txtAccountExecutive.SetText('');
    txtIDNumber.SetText('');
    cmbIsInsured.SetValue('');
    txtComROS.SetText('');
    txtRegSup.SetText('');
    txtRetailSAE.SetText('');
    txtRetailSAM.SetText('');
    dteBirthday.SetText('');
    txtSiteCode.SetText('');
    txtDistrict.SetText('');
    txtShopMainCode.SetText('');
    txtLatitude.SetText('');
    txtLongitude.SetText('');
    cmbStatus.SetValue('');
    cmbLeaveReason.SetValue('');
    cmbUniform.SetValue('');
    txtShenime.SetText('');
    cmbStatusAprovimi.SetValue('');
    cbNjoftimEmailAprovim.SetChecked(false);
    cmbKonfigKase.SetValue(0);
}

function SucceededCallbackNrAutomatik(result) {
    var vlerat = result.split(',');
    kontrolli = Utils.ktheKontroll(vlerat[0]);
    kontrolli.SetText(vlerat[1]);
    hfVleraNrAutom.Set(vlerat[2], vlerat[1]);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    grid_ListPerdoruesit.PerformCallback(idKomp + ";" + kodKonf);
    gvRolet.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvPerdorues").show();
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
function SucceededCallbackRoletPerd(result)
{
    if (result.toString() !== roletNew.toString()) {
        Utils.hiqLoadingGif();
        popUpConfirm.Show();
        hfState.Set("dergoEmailNdryshimRoli", true);
    }
    else 
        btnKonfirmo.DoClick();

}

function SucceedCallbackRoli(result) {
    roletNew = new Array(result.length);
    var rol = '';
    for (var i = 0; i < result.length; i++) {
        roletNew[i] = result[i][1];
        rol = rol + result[i][1] + ',';
        if (result[i][0] == false) {
            myMesazh.ShtoMesazhGabimi(hfState.Get('msgPerdoruesitRolJoAktiv'));
            gvRolet.UnselectRowsByKey(result[i][1]);
        }
    }
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        colKushte = result.colKushte;
        colAlterKusht = result.colAlterKusht;
        resultkonf = result;
        var hiqkontakt = false;
        var hiqrole = false;
        for (j = 0; j < colKushte.length; j++) {

            if (colKushte[j].Kodi == "SHTR") {
                if (colAlterKusht[j].Alternativa == "Jo") {
                    PageControl.GetTab(3).SetVisible(false);
                    hiqrole = true;
                }
                else
                    PageControl.GetTab(3).SetVisible(true);

            }
            if (colKushte[j].Kodi == "SHTK") {
                if (colAlterKusht[j].Alternativa == "Jo") {
                    PageControl.GetTab(2).SetVisible(false);
                    hiqkontakt = true;
                }
                else
                    PageControl.GetTab(2).SetVisible(true);

            }
        }
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblPerdoruesi', 'tblKontakti', 'tblRolet'];
        if (hiqkontakt)
            arrTabela = ['tblPerdoruesi', 'tblPerdoruesi', 'tblRolet'];
        else
             arrTabela = ['tblPerdoruesi', 'tblKontakti', 'tblRolet'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
        if (hfMod.val() == "shtim") {
            lblPasswordieksistues.SetVisible(false);
            txtPasswordieksistues.SetVisible(false);
        }
    }

    //$("#dvPerdorues").show();//$("#dvPerdorues")[0].style.visibility = 'visible';
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    ShowHideAmbjentMobile();    
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("100", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("100", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

//function changeName() {
//    $('#hfId').val(0);
//    var hf = $("#hfKonffillestar")[0];
//    myFaqeCelje.changeName('Shto_Perdorues.aspx',0, hf);
//    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
//}
function changeName() {
    var id = parseInt($('#hfId').val());
    if (isNaN(id))
        id = 0;
    $('#hfId').val(id);
    var idTabi = (id == 0) ? 0 : 1;
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Perdorues.aspx', id, hf);
    PageControl.SetActiveTabIndex(idTabi);
    myMenu.PercaktoMenuSipasTabit(idTabi, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    //var hfRedirect = $('#hfRedirect');
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    //            indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, grid_ListPerdoruesit, "100")
    //if (hfRedirect.val() == "true")
    //    setTimeout(function () {
    //        window.location = 'Default.aspx';
    //    }, 5000)
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, grid_ListPerdoruesit, "100", pastrofusha, hfTeDrejta);
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

function OnBeginCallback(s, e) {
    if (e.command == "CUSTOMCALLBACK")
        LoadingPanel.Show();
}
function OnEndCallback(s, e) {
    LoadingPanel.Hide();
    if (therritWsLidhur) {
        therritWsLidhur = false;
        PlatinumWeb.wsfunc.eshteLidhur("100", cmbKonfigurimi.GetText(), $('#hfId').val(), SucceededCallbackLidhur);
    }
}

var therritWsLidhur = false;

function OnSelectionChanged(s, e) {
    gvRolet.GetSelectedFieldValues('AktivRoli;IdRoli', function (result) {
        try {
            window.parent.SessionTimeout.sendKeepAlive();
        } catch (e) { }
        var rol = '';
        for (var i = 0; i < result.length; i++) {
            
            rol = rol + result[i][1] + ',';
            if (result[i][0] == false) {
                myMesazh.ShtoMesazhGabimi('Ky rol nuk eshte aktiv');

                gvRolet.UnselectRowsByKey(result[i][1]);
            }
        }
    });
}

function kontrolloPassword(s, e) {
    // var valid = password_TextBox.GetIsValid();
    //if (!valid) {
    var gjatesiPass = s.GetValue();
    if (hfMinGjatesiPassword !== 'undefined') {
        if (hfMinGjatesiPassword.Contains('GjatesiMinPass')) {
            var gjatesiMinPass = hfMinGjatesiPassword.Get('GjatesiMinPass');
        }
    }
    if(gjatesiPass.length < gjatesiMinPass)
    {
        e.isValid = false;
        e.errorText = hfState.Get('msgPerdoruesitMinGjatesiPassword') + gjatesiMinPass + hfState.Get('msgPerdoruesitMinKarakterePass');
        myMesazh.ShtoMesazhGabimi(hfState.Get('msgPerdoruesitMinGjatesiPassword') + gjatesiMinPass + hfState.Get('msgPerdoruesitMinKarakterePass'));
    }
     
}

function disablePassEkzistues(s, e) {
    if (cbPassPerkohshem.GetChecked()) {
        txtPasswordieksistues.SetEnabled(false);
        AktivizoFushatEPasswordit();
    }
    else
        txtPasswordieksistues.SetEnabled(true);
}
function Active_TabChanged(s, e) {

    indexModifiko = grid_ListPerdoruesit.GetFocusedRowIndex();
    if (s.GetActiveTabIndex() == 0) {
        $('#hfIndexId').val('');
    }
    if(mbush)
    { 
        if(indexModifiko !=-1)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else 
        {   
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim'; 
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab=false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function myButtonClickLupaShopsHierarkiLeaveReason(s, e) {
    e.processOnServer = false;
    popupUniversal.SetSize(500, 500);
    popupUniversal.SetHeaderText(hfState.Get('msgZgjidhArsyeLargimi'));
    popupUniversal.SetContentUrl('LupaShopsHierarkiLeaveReason.aspx?vjenNga=Perdoruesi');
    popupUniversal.Show();
}


function myButtonClickLupaShopsStatus(s, e) {
    e.processOnServer = false;
    popupUniversal.SetSize(500, 500);
    popupUniversal.SetHeaderText(hfState.Get('msgStatus'));
    popupUniversal.SetContentUrl('LupaShopsHierarkiStatus.aspx?vjenNga=Perdoruesi');
    popupUniversal.Show();
}

function myButtonClickLupaShopsUniform(s, e) {
    e.processOnServer = false;
    popupUniversal.SetSize(500, 500);
    popupUniversal.SetHeaderText(hfState.Get('msgUniform'));
    popupUniversal.SetContentUrl('LupaShopsHierarkiUniform.aspx?vjenNga=Perdoruesi');
    popupUniversal.Show();
}
