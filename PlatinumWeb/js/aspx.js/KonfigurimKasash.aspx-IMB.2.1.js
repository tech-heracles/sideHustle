
function changeLlojKase(lloji) {
    switch (lloji.toString()) {
        case "-1":
            pnlBosh.SetVisible(true);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(false);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(false);
            break;
        case "0":
         case "7":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(true);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(true);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(false);
            break;
        case "1":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(true);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(true);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(false);
            break;
        case "2":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(true);
            pnlgrida.SetVisible(true);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(false);
            break;
        case "3":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(true);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(true);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(false);
            break;
        case "4":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(true);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(true);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(false);
            break;
        case "5":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(true);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(true);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(false);
            break;
        case "6":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(true);
            pnlBNTAClass.SetVisible(true);
            pnlPeshore.SetVisible(false);
            break;
        case "8":
            pnlBosh.SetVisible(false);
            pnlIVA.SetVisible(false);
            pnlAED.SetVisible(false);
            pnlCKVNOKI.SetVisible(false);
            pnlPKP.SetVisible(false);
            pnlGEKOS.SetVisible(false);
            pnlBTN.SetVisible(false);
            pnlgrida.SetVisible(false);
            pnlBNTAClass.SetVisible(false);
            pnlPeshore.SetVisible(true);
            break;
    }
    txtUrl.SetText('');
    txtIPKase.SetText('');
}

function onSelectionChangedLlojKase(lloji) {
    changeLlojKase(lloji);
    gvNiveleTVSHIVA.PerformCallback(-1);
}

var arr = new Array();
var indexModifiko;
var eshteVeprimFshirje = false;
var kodiPerTuFshire = '';
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = false;

//perdoret per te dalluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;

function merrTeDhena() {
    var hidField7 = $("#hfVlerat");
    var hidfiled = $("#hfFile");

    for (var i = 0; i < gvNiveleTVSHIVA.cpNoRows; i++) {

        editorKodi = Utils.ktheKontroll('lblKodi' + i);
        editorKasa = Utils.ktheKontroll('txtKasa' + i);
        arr[i] = new Array();
        arr[i][0] = i;
        arr[i][1] = editorKodi.GetText();
        arr[i][2] = editorKasa.GetText();
    }

    hidField7.val(JSON.stringify(arr));

    switch (cmbLlojiKases.GetValue()) {
        case "-1":

            break;
        case "0":
        case "7":
            hidfiled.val(ucIVA.GetText());
            break;
        case "1":
            hidfiled.val(ucAED.GetText());
            break;
        case "2":
            hidfiled.val(ucBTN.GetText());
            break;
        case "3":
            hidfiled.val(ucCKV.GetText());
            break;
        case "4":
            hidfiled.val(ucPKP.GetText());
            break;
        case "5":
            hidfiled.val(ucGEKOS.GetText());
            break;
        case "6":
            break;
        case "8":
            hidfiled.val(ucPeshore.GetText());
            break;
    }
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
    $(window).on('load', function () {
        Init();
    });

});

function Init() {
    changeName();
    IndexChangedLlojiKasePeshore(cmbLlojObjekti.GetValue());
    cmbLlojiKases.SetText('');
}

function menu_click(s, e) {
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    if (e.item.name == 'Shto') {
        hfId.val(0);
    }
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, false, indexModifiko);
    if (e.item.name == 'Ruaj') {
        merrTeDhena();
    }
    if (e.item.name == 'Fshi') {
        gvKasat.GetRowValues(gvKasat.GetFocusedRowIndex(), "Kodi", onSuccedeedFshirje);
        eshteVeprimFshirje = true;
    }
}

function onSuccedeedFshirje(result) {
    eshteVeprimFshirje = true;
    kodiPerTuFshire = result;
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function pastrofusha() {
    txtKodi.SetText('');
    txtUrl.SetText('');
    txtIPKase.SetText('');
    txtSkema.SetText('');
    cmbLlojiKases.SetSelectedIndex(-1);
    changeLlojKase("-1");
}

function shkruajFile() {
    var file = $('#hfKasa').val().split('&&');
    //kontrollohet nqs file eshte bosh; nqs eshte bosh atehere funksioni qe kerkohet nuk eshte i mundur per kasen qe eshte zgjedhur.
    if (file != "") {
        var fso = new ActiveXObject("Scripting.FileSystemObject");
        var index = file[0].lastIndexOf('\\');
        var direktoria = file[0].substring(0, index);
        //kontrollohet nqs ekziston direktoria; nqs ekziston vazhdohet me shkrimin e skedarit, perndryshe del mesazhi qe direktoria nuk ekziston
        if (fso.FolderExists(direktoria)) {

            if (fso.FileExists(file[0])) {

                s = fso.OpenTextFile(file[0], 2, false);
            }
            else {
                var s = fso.CreateTextFile(file[0], false);
            }
            var text = file[1].split('||');
            for (var i = 0; i < text.length; i++)
                s.WriteLine(text[i]);
            s.Close();
            if (file.length > 2) {
                fso.CopyFile(file[0], file[2]);
                if (file[3] == "False") {
                    fso.DeleteFile(file[0]);
                }
            }
            myMesazh.ShtoMesazhSuksesi('Veprimi u krye me sukses!');
        }
        else myMesazh.ShtoMesazhGabimi('Direktoria nuk ekziston!');
    }
    else myMesazh.ShtoMesazhGabimi('Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!');
}

function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    if (hf.val() === "true" && eshteVeprimFshirje && kodiPerTuFshire !== '') {
        if (localStorage.getItem("kase_key" + hfState.Get('idNdermarrje')) == kodiPerTuFshire)
            localStorage.removeItem("kase_key" + hfState.Get('idNdermarrje'));
        kodiPerTuFshire = '';
        eshteVeprimFshirje = false;
    }
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerPas(sender, args, hf, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKasat, "533", hfTeDrejta);
    eshteVeprimFshirje = false;
    if ($('#hfKasa').val() !== "")
        shkruajFile();
}

function changeName() {  
    myFaqeCelje.changeName('KonfigurimKasash.aspx', 0, null);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));    
}

//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    mbushfusha();
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvKasat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi("Duhet te zgjidhni nje konfigurim kase!");
    else
        gvKasat.GetRowValues(indexModifiko, 'IdKonfigurimi;Kodi;Vlera;Lloji;Skema', OnGetRowValuesMod);
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    PageControl.SetActiveTabIndex(1);
    cmbLlojiKases.SetValue(values[2]);
    txtKodi.SetText(values[1]);
    cmbLlojObjekti.SetValue(values[3]);
    IndexChangedLlojiKasePeshore(values[3], values[2]);
    //changeLlojKase(values[2]);
    txtSkema.SetText(values[4]);
    gvNiveleTVSHIVA.PerformCallback(values[0]);
     $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheVleratEkonfigurimitSipaasLlojitKases"),
        data: JSON.stringify({ idkonfigurimi: values[0], Lloji: cmbLlojiKases.GetText() })
    }).done(SucceededCallbacKonfigurimiKases);
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
}

function SucceededCallbacKonfigurimiKases(result) {
    txtUrl.SetText(result.url);
    txtIPKase.SetText(result.IP);
    switch (result.lloji) {
        case "0":
        case "7": //IVA
            ucIVA.SetText(result.pathFile);
            cbKasaMeShifraDhjetore.SetChecked(JSON.parse(result.meShifraDhjetore.toString().toLowerCase()));
            rbKasa.SetChecked(JSON.parse(result.kaseApoPrinter.toString().toLowerCase()));
            rbPrinter.SetChecked(!rbKasa.GetChecked());
            cbPrintoNrFature.SetChecked(JSON.parse(result.printoNrFature.toString().toLowerCase()));
            cbPrintoNeServerIVA.SetChecked(JSON.parse(result.printoNeServer.toString().toLowerCase()));
            if (result.cmimMonedheDyte != "0") {
                cbPrintoTotalDheNeMon.SetChecked(true);
                cmbMonedha.SetEnabled(true);
            }
            else
                cbPrintoTotalDheNeMon.SetChecked(false);
            cmbMonedha.SetValue(result.cmimMonedheDyte);
            rbKuponTatimor.SetChecked(JSON.parse(result.kuponTatimor.toString().toLowerCase()));
            rbFatureTatimore.SetChecked(!rbKuponTatimor.GetChecked());
            if (result.nrKopjesh != "False") 
                cbPrintoNrKopjeFature.SetChecked(true);
            else
                cbPrintoNrKopjeFature.SetChecked(false);
            if (result.nrKopjeshKthimi != "0") {
                cbPrintoKopjeTeKthimeve.SetChecked(true);
                txtNrKopjeKthimesh.SetEnabled(true);
            }
            else
                cbPrintoKopjeTeKthimeve.SetChecked(false);
            txtNrKopjeKthimesh.SetText(result.nrKopjeshKthimi);
            cbPrintoManualishtNgaKasa.SetChecked(JSON.parse(result.printomanualisht.toString().toLowerCase()));
            cbPrintoPershkrim2IVA.SetChecked(JSON.parse(result.printoPershkrim2.toString().toLowerCase()));
            break;
        case "1": //AED
            ucAED.SetText(result.pathFile);
            txtPorta.SetText(result.port);
            if (result.chius == "1")
                cbMbyllCdoFature.SetChecked(true);
            else
                cbMbyllCdoFature.SetChecked(false);
            if (result.nrKopjesh != "False") 
                cbPrintoNrKopjeFatureAED.SetChecked(true);
            else
                cbPrintoNrKopjeFatureAED.SetChecked(false);
            cbPrintoBarkod.SetChecked(JSON.parse(result.printoBarKod.toString().toLowerCase()));
            cbRuajKopje.SetChecked(JSON.parse(result.ruajKopje.toString().toLowerCase()));
            cbKasaMeShifraDhjetoreAED.SetChecked(JSON.parse(result.meShifraDhjetore.toString().toLowerCase()));
            cbPrintoKodArtikulli.SetChecked(JSON.parse(result.printoKodArtikulli.toString().toLowerCase()));
            cbPrintoNeServerAED.SetChecked(JSON.parse(result.printoNeServer.toString().toLowerCase()));
            cbPrintoPershkrim2AED.SetChecked(JSON.parse(result.printoPershkrim2.toString().toLowerCase()));
            break;
        case "2": //BTN
            ucBTN.SetText(result.pathFile);
            cbKasaMeShifraDhjetoreBTN.SetChecked(JSON.parse(result.meShifraDhjetore.toString().toLowerCase()));
            rbKasaBTN.SetChecked(JSON.parse(result.kaseApoPrinter.toString().toLowerCase()));
            rbPrinterBTN.SetChecked(!rbKasaBTN.GetChecked());
            cbPrintoNrFatureBTN.SetChecked(JSON.parse(result.printoNrFature.toString().toLowerCase()));
            cbPrintoNeServerBTN.SetChecked(JSON.parse(result.printoNeServer.toString().toLowerCase()));
            if (result.cmimMonedheDyte != "0") {
                cbPrintoTotalDheNeMonBTN.SetChecked(true);
                cmbMonedhaBTN.SetEnabled(true);
            }
            else
                cbPrintoTotalDheNeMonBTN.SetChecked(false);
            cmbMonedhaBTN.SetValue(result.cmimMonedheDyte);
            rbKuponTatimorBTN.SetChecked(JSON.parse(result.kuponTatimor.toString().toLowerCase()));
            rbFatureTatimoreBTN.SetChecked(!rbKuponTatimorBTN.GetChecked());
            if (result.nrKopjesh != "False") 
                cbPrintoNrKopjeFatureBTN.SetChecked(true);
            else
                cbPrintoNrKopjeFatureBTN.SetChecked(false);
            if (result.nrKopjeshKthimi != "0") {
                cbPrintoKopjeTeKthimeveBTN.SetChecked(true);
                txtNrKopjeKthimeshBTN.SetEnabled(true);
            }
            else
                cbPrintoKopjeTeKthimeveBTN.SetChecked(false);
            txtNrKopjeKthimeshBTN.SetText(result.nrKopjeshKthimi);
            cbPrintoPershkrim2BTN.SetChecked(JSON.parse(result.printoPershkrim2.toString().toLowerCase()));
            break;
        case "3": //CKVNOKI
            txtPortaCom.SetText(result.comPort);
            cbKasaMeShifraDhjetoreCKVNOKI.SetChecked(JSON.parse(result.meShifraDhjetore.toString().toLowerCase()));
            txtBoudRate.SetText(result.boudRate);
            cbPrintoNrFatureCKVNOKI.SetChecked(JSON.parse(result.printoNrFature.toString().toLowerCase()));
            cbMeSkedar.SetChecked(JSON.parse(result.meSkedar.toString().toLowerCase()));
            cbPrintoNeServerCKV.SetChecked(JSON.parse(result.printoNeServer.toString().toLowerCase()));
            ucCKV.SetText(result.pathFile);
            cbPrintoPershkrim2CKV.SetChecked(JSON.parse(result.printoPershkrim2.toString().toLowerCase()));
            break;
        case "4": //PKP
            ucPKP.SetText(result.pathFile);
            cbKasaMeShifraDhjetorePKP.SetChecked(JSON.parse(result.meShifraDhjetore.toString().toLowerCase()));
            cbPrintoNrFaturePKP.SetChecked(JSON.parse(result.printoNrFature.toString().toLowerCase()));
            cbPrintoNeServerPKP.SetChecked(JSON.parse(result.printoNeServer.toString().toLowerCase()));
            if (result.cmimMondheDyte != "0") {
                cbPrintoTotalDheNeMonPKP.SetChecked(true);
                cmbMonedhaPKP.SetEnabled(true);
            }
            else
                cbPrintoTotalDheNeMonPKP.SetChecked(false);
            cmbMonedhaPKP.SetValue(result.cmimMondheDyte);
            if (result.nrKopjesh != "False") 
                cbPrintoNrKopjeFaturePKP.SetChecked(true);
            else
                cbPrintoNrKopjeFaturePKP.SetChecked(false);
            if (result.nrKopjeshKthimi != "0") 
                cbPrintoKopjeTeKthimevePKP.SetChecked(true);
            else
                cbPrintoKopjeTeKthimevePKP.SetChecked(false);
            txtNrKopjeKthimeshPKP.SetText(result.nrKopjeshKthimi);
            cbPrintoPershkrim2PKP.SetChecked(JSON.parse(result.printoPershkrim2.toString().toLowerCase()));
            break;
        case "5": //GEKOS
            ucGEKOS.SetText(result.pathFile);
            txtPassOperator.SetText(result.operatorPass);
            cmbGjuha.SetValue(result.gjuha);
            txtKodiTVSH.SetText(result.nivelDefaultTvsh);
            cbPrintoNeServerGEKOS.SetChecked(JSON.parse(result.printoNeServer.toString().toLowerCase()));
            if (result.nrKopjesh != "False") 
                cbPrintoNrKopjeFatureGEKOS.SetChecked(true);
            else
                cbPrintoNrKopjeFatureGEKOS.SetChecked(false);
            cbKasaMeShifraDhjetoreGEKOS.SetChecked(JSON.parse(result.meShifraDhjetore.toString().toLowerCase()));
            cbPrintoNrFatureGEKOS.SetChecked(JSON.parse(result.printoNrFature.toString().toLowerCase()));
            cbPrintoPershkrim2GEKOS.SetChecked(JSON.parse(result.printoPershkrim2.toString().toLowerCase()));
            break;
        case "6": //BNTAClass
            txtPortaComBNTAClass.SetText(result.comport);
            cbKasaMeShifraDhjetoreBNTAClass.SetChecked(JSON.parse(result.meShifraDhjetore.toString().toLowerCase()));
            txtBoudRateBNTAClass.SetText(result.boudrate);
            cbPrintoNeServerBNTAclass.SetChecked(JSON.parse(result.printoNeServer.toString().toLowerCase()));
            cbPrintoPershkrim2BNTAclass.SetChecked(JSON.parse(result.printoPershkrim2.toString().toLowerCase()));
            break;
        case "8":
            ucPeshore.SetText(result.pathFile);
            break;
    }
}

function tabsActiveTabChanged(s, e) {
    indexModifiko = gvKasat.GetFocusedRowIndex();
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

function onNdryshimFokusi() {
    if (PageControl.GetActiveTabIndex() == 0)
        mbush = true;
}

function IndexChangedLlojiKasePeshore(lloj, llojKase) {
    changeLlojKase("-1");
    if (lloj == "1") {
        lblLlojiKases.SetText("Lloji i Kases");
        lblUrl.SetText("Url e Kases");
        lblSkema.SetVisible(false);
        txtSkema.SetVisible(false);
    }
    else {
        lblLlojiKases.SetText("Lloji i Peshores");
        lblUrl.SetText("Url e Peshores");
        lblSkema.SetVisible(true);
        txtSkema.SetVisible(true);
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheTipeStandarteKasashPeshoreshSipasLlojit"), data: JSON.stringify({ lloji: lloj })
    }).done(function (result) {
        SucceededCallbackLlojeKasash(result);
        if (llojKase) {
            changeLlojKase(llojKase);
            cmbLlojiKases.SetValue(llojKase);
        }
    });
}

function SucceededCallbackLlojeKasash(result) {
    if (result && result !== "") {
        cmbLlojiKases.ClearItems();
        //var llojet = JSON.parse(result);
        for (var i = 0; i < result.length; i++) {
            var item = result[i];
            cmbLlojiKases.AddItem(item["Lloji"], item["Vlera"]);
        }
    }
    else {
        myMesazh.ShtoMesazhGabimi("Ndodhi nje gabim gjate marrjes se llojeve te " + (cmbLlojObjekti.GetValue() == "1" ? "kases" : "peshores") + "!");
    }
}
function CheckedChanged_cbPrintoTotalDheNeMon(s, e) {
    if(cbPrintoTotalDheNeMon.GetChecked()==true)
        cmbMonedha.SetEnabled(true);        
    else{ cmbMonedha.SetEnabled(false); 
        cmbMonedha.SetSelectedIndex(0);}
}

function CheckedChanged_cbPrintoKopjeTeKthimeve(s, e) {
    if(cbPrintoKopjeTeKthimeve.GetChecked()==true)

        txtNrKopjeKthimesh.SetEnabled(true);    
    
    else {txtNrKopjeKthimesh.SetEnabled(false);  txtNrKopjeKthimesh.SetText(0);
    }
}

function CheckedChanged_cbPrintoTotalDheNeMonBTN(s, e) {
    if(cbPrintoTotalDheNeMonBTN.GetChecked()==true)

        cmbMonedhaBTN.SetEnabled(true);    
    
    else{ cmbMonedhaBTN.SetEnabled(false); cmbMonedhaBTN.SetSelectedIndex(0);}
}


function CheckedChanged_cbPrintoKopjeTeKthimeveBTN(s, e) {
    if(cbPrintoKopjeTeKthimeveBTN.GetChecked()==true)

        txtNrKopjeKthimeshBTN.SetEnabled(true);

    else {
        txtNrKopjeKthimeshBTN.SetEnabled(false); txtNrKopjeKthimeshBTN.SetText(0);
    }
}
function CheckedChanged_cbPrintoTotalDheNeMonPKP(s, e) {
    if(cbPrintoTotalDheNeMonPKP.GetChecked()==true)

        cmbMonedhaPKP.SetEnabled(true);        else{ cmbMonedhaPKP.SetEnabled(false); cmbMonedhaPKP.SetSelectedIndex(0);}
}


function CheckedChanged_cbPrintoKopjeTeKthimevePKP(s, e) {
    if(cbPrintoKopjeTeKthimevePKP.GetChecked()==true)

        txtNrKopjeKthimeshPKP.SetEnabled(true);   
    
    else {txtNrKopjeKthimeshPKP.SetEnabled(false);  txtNrKopjeKthimeshPKP.SetText(0);
    }
}


function Click_btnRaportiX(s, e) {
    if(cmbLlojiKases.GetValue()=='5')
    {
        e.processOnServer = false;
        lblMsgbox2.SetText('Deshironi te printoni raportin ditor X?');
        $('#hfPyetje').val('X');
        popFshi2.Show();
    } 
}

function Click_btnRaportiZ(s, e) {
    if(cmbLlojiKases.GetValue()=='5')
    {
        e.processOnServer = false;
        lblMsgbox2.SetText('Deshironi te printoni raportin ditor Z?');
        $('#hfPyetje').val('Z');
        popFshi2.Show();
    }
    else if(cmbLlojiKases.GetValue()=='1') {e.processOnServer = false; popupMbyllKase.Show();}
}

function Click_btnPlu(s, e) {
    if(cmbLlojiKases.GetValue()=='5')
        e.processOnServer = false;
    $('#hfPyetje').val('PLU');
    popFshi2.Show();
    lblMsgbox2.SetText('Deshironi te fshini listen e PLU?');
}

function dergoNeKase(s, e) {
    //var myKasaJson = JSON.parse(hfKasa.val());
    var hfKasa = $('#hfKasaNew');
    var urlKase = txtUrl.GetText();
    $.ajax({
        crossDomain: true,
        data: hfKasa.val(),
        url: urlKase
    }).done(function (res) {
        if (res)
            myMesazh.ShtoMesazhSuksesi(hfState.Get("msgFaturaDerguaKaseFiskaleSukses"));
    }).fail(function (res) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDownloadPrograminEKasesTeMenu"));
        if (console && console.error) {
            console.error("Server Kasa responded:", res);
        }
    });
    // Denisi
    hfKasa.val(""); //pastrojme hf-ne
}