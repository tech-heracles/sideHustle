;
$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                try { window.parent.rifresko = true; } catch (e) { }
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    try { window.parent.rifresko = true; } catch (e) { }
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
    });

});
var indexModifiko = 0;
// kur faqja lodohet
function Init() {

    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    mbushFusha();

}
function mbushFusha() {

    txtKodi.SetText(Utils.getUrlVar("kodi") || '');
    txtPershkrimi.SetText(Utils.getUrlVar("pershkrimi") || '');
    $('#hfId').val(Utils.getUrlVar("gid") || 0);
    RefreshFushatShtese($('#hfId').val(), 'mod');
}
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    //pa konfigurim p.s. kur te duhet le te shtohet
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    // cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("3049", cmbKonfigurimi.GetText());
    ndryshoKonfigurimFushaShtese(cmbKonfigurimi.GetValue());
}

function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, undefined, "3049", undefined, hfTeDrejta, undefined, undefined);

}

function menu_click(s, e) {

    switch (e.item.name) {
        case "Anullo":
            window.parent.popupUniversal.Hide();
            e.processOnServer = false;
            break;
        case "Ruaj":
            if (!ASPxClientEdit.ValidateGroup("entries")) {
                myMesazh.ShtoMesazhGabimi("Kontrolloni fushat ne gride!");
                e.processOnServer = false;
                return;
            }
            Utils.shfaqLoadingGif();;
            ruajFushaShtese();
            break;
        default:
            e.processOnServer = false;
            throw new Error("Menu item " + e.item.name + " eshte i pa trajtuar!")
    }
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblFushatShtese'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C");
        RefreshFushatShtese(Utils.getUrlVar("gid"), 'mod');

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

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}