
var identikuesPerPopupTransportuesi = "RegjistrimDokumentash";
$(window).load(function () {
    try {
        Init();
        ASPxPanel1.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        //document.getElementById("menu").style.width = document.documentElement.clientWidth - 50;
        ASPxPanel1.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
}).trigger('resize');
/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    if (e.item.name == 'Ruaj') {
        Utils.shfaqLoadingGif();;
        valido(s, e);
    }
    if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}


function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("915", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("915", cmbKonfigurimi.GetText());
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.shtoHandlerSession();
    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    $("#hfRuaj").val("Ruaj");
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function Init() {
    window.parent.popupUniversal.UpdatePosition();
    lengjth = window.history.length;
    changeName();
}

function valido(s, e) {
    myFaqeCelje.validim(s, e);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
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

function SucceededCallbackKonfig(result) {
    $("#dvTransp").show();//$("#dvTransp")[0].style.visibility = 'visible';
    //$("#dvTransp")[0].style.display = '';
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        hfMod.val('shtim');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPanel");


    }
}

//function LupaKontrollet(kontrollet, colAtrTrupi) {
//    var hf1 = $("#hfLupaGrupi");
//    var hf2 = $("#hfLupaNengrupi");
//    var hf3 = $("#hfLupaKpf1");
//    for (var i = 0; i < kontrollet.length; i++) {

//        //id e konfigurimit te lupes vendoset ne hidden field
//        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
//        if (kontrollet[i].KodKontrolli == "cmbGrupi") {
//            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
//            continue;
//        }
//        if (kontrollet[i].KodKontrolli == "cmbNengrupi") {
//            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
//            continue;
//        }
//        if (kontrollet[i].KodKontrolli == "cmbKpf1") {
//            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
//            continue;
//        }
//    }
//}



/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    if (hf.val() == "true") {
        //        if (hfShtimModifikim.val() != "modifikim") {
        window.mbush = false;
        $('#hfShtimModifikim').val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
        //hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
        //indexModifiko = -1; //indexi i reshtit te selektuar  
        var emertimi = txtTransportues.GetText();
        // var emertimi = $('#hfEmertimi').value;
        pastrofusha();
        //InitiComboKategoriaLloji();
        hf.val("false");


        switch (window.parent.identikuesPerPopupTransportuesi) {

            case "RegjistrimDokumentash":
                var idNdermarrje = window.parent.hfState.Get('idNdermarrje');
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Konfigurime", "ktheKodDheIdTransportuesi"),
                    data: JSON.stringify({ kodTransportues: emertimi, idNdermarrje: idNdermarrje })
                }).done(SucceededCallbackTransportues);
        }
    }
    Utils.hiqLoadingGif();;
}

function SucceededCallbackTransportues(result) {
    if (result !== null && typeof (result) !== undefined) {
        var emertimi = result.emertimi;
        var id = result.id;
        window.parent.btnTransportues.SetValue(id);
        window.parent.btnTransportues.SetText(emertimi);
        window.parent.btnTransportues.Focus(true);
        window.parent.popupUniversal.Hide();
    }
}


//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {

    txtTransportues.SetText('');
    txtNIPTTransp.SetText('');
    txtAdresaTransp.SetText('');
    txtTelTransp.SetText('');
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //        var hfLidhur = $("#hfLidhur")[0];
    //        myFaqeCelje.aktivizoFusha(vlerat, hfMod, hfLidhur, '#ASPxPageControl1_');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function Succeded(result) {
    window.location = result;
}

