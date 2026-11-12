; // Rivleresimi i inventarit: zgjidhen magazina + periudha, shfaqen artikujt me levizje,
// zgjidhen artikujt dhe ProgressBar1 (RunTask ne server) i rivlereson dite pas dite.
var pageState = false;

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
    myMesazh.shtoHandler(); myFaqeCelje.shtoHandlerSession();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler); changeName();
}
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('RivleresimMagazine.aspx', 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function EndRequestHandler(sender, args) {
}

function shfaqItem(s, emer, vizibel) {
    var item = s.GetItemByName(emer);
    if (item)
        item.SetVisible(vizibel);
}

function menuInit(s, e) {
    shfaqItem(s, 'Stop', false);
    shfaqItem(s, 'Riruaj', false); // veprim i vjeter qe kjo faqe nuk e ka
}

function menuClick(s, e) {
    switch (e.item.name) {
        case 'Rivleresim':
            e.processOnServer = false;
            nisRivleresimin();
            break;
        case 'Stop':
            e.processOnServer = false;
            ndalRivleresimin();
            break;
    }
}

function shfaqArtikujt() {
    if (!dtePeriudhaNga.GetDate() || !dtePeriudhaDeri.GetDate()) {
        myMesazh.ShtoMesazhInformues(hfState.Get("msgZgjidhPeriudhen"));
        return;
    }
    gvRivleresim.PerformCallback('shfaq');
}

function filtriNdryshoi() {
    // lista e shfaqur i perket filtrit te meparshem: pastrohet qe te mos rivleresohet periudha e gabuar
    if (gvRivleresim.GetVisibleRowsOnPage() > 0)
        gvRivleresim.PerformCallback('pastro');
}

function dtePeriudhaChanged(s, e) {
    if (dtePeriudhaNga.GetDate() && dtePeriudhaDeri.GetDate() && dtePeriudhaDeri.GetDate() < dtePeriudhaNga.GetDate()) {
        myMesazh.ShtoMesazhInformues(hfState.Get("MsgDataGabim"));
        dtePeriudhaDeri.SetDate(dtePeriudhaNga.GetDate());
    }
    filtriNdryshoi();
}

function gvRivleresimEndCallback(s, e) {
    if (s.cpMesazh) {
        myMesazh.ShtoMesazhInformues(s.cpMesazh);
        s.cpMesazh = null;
    }
}

var rivleresimNePune = false;
function nisRivleresimin() {
    if (rivleresimNePune)
        return;
    if (gvRivleresim.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhInformues(hfState.Get("msgZgjidhArtikujt"));
        return;
    }
    if (!confirm(hfState.Get("msgKonfirmoRivleresimin").replace('{0}', gvRivleresim.GetSelectedRowCount())))
        return;
    rivleresimNePune = true;
    shfaqItem(ASPxMenu1, 'Rivleresim', false);
    shfaqItem(ASPxMenu1, 'Stop', true);
    lblProgresi.SetText('');
    Utils.shfaqLoadingGif();
    ProgressBar1.startTask();
}

function ndalRivleresimin() {
    // ndalimi behet ne server pasi mbaron dita qe po rivleresohet; onTaskDone jep mesazhin
    shfaqItem(ASPxMenu1, 'Stop', false);
    ProgressBar1.stopTask();
}

function lexoExtraData() {
    var extra = ProgressBar1.getExtraData();
    if (!extra)
        return null;
    try { return JSON.parse(extra); } catch (ex) { return { Status: null, Mesazh: extra }; }
}

function onTaskRunning() {
    try { window.parent.SessionTimeout.sendKeepAlive(); } catch (e) { }
    var extra = lexoExtraData();
    if (extra && extra.Mesazh)
        lblProgresi.SetText(extra.Mesazh);
}

function perfundoUI() {
    rivleresimNePune = false;
    Utils.hiqLoadingGif();
    shfaqItem(ASPxMenu1, 'Rivleresim', true);
    shfaqItem(ASPxMenu1, 'Stop', false);
}

function onTaskDone() {
    var extra = lexoExtraData();
    var mesazh = extra && extra.Mesazh ? extra.Mesazh : '';
    if (extra && extra.Status === true)
        myMesazh.ShtoMesazhSuksesi(mesazh);
    else if (extra && extra.Status === false)
        myMesazh.ShtoMesazhGabimi(mesazh);
    else
        myMesazh.ShtoMesazhInformues(hfState.Get("msgRivleresimiUNdal") + ' ' + ProgressBar1.getValue() + ' % ' + mesazh);
    lblProgresi.SetText(mesazh);
    perfundoUI();
}

function onTaskError() {
    myMesazh.ShtoMesazhGabimi(hfState.Get("msgRivleresimiGabim"));
    perfundoUI();
}
