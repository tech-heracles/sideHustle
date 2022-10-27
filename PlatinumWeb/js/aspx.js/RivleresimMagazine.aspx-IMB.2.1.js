; var identifikuesPerPopupMagazina = "Rivleresim";
var identikuesPerPopupArtikulli = "Rivleresim";
var pageState = false;
window.parent.open("https://rivlersimi.alpha.al/Prezantohu.aspx", "_blank");
window.parent.location.reload();
function ButtonClickMagazina() {
    // var hfKl = document.getElementById("hfLupaMagazina");
    // var queryStr = hfKl.value;
    myButtonClickLupa.ButtonClickMagazina(hfState.Get("msgZgjidhMagazinen"), '', 720, 650);
}
function ButtonClickArtikuj() {
    //  var hfKl = document.getElementById("hfLupaMagazina");
    // var queryStr = hfKl.value;
    myButtonClickLupa.ButtonClickArtikulli(hfState.Get("headerPopUpZgjidhArtikullin"), 0, 800, 600);
}
function LostFocus() {
    gvRivleresim.PerformCallback('mag');
    gvRivleresim2.PerformCallback();
    pastro();
}
jQuery(document).ready(function () {
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
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
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });
    
});

function menuClick(s, e) {
    //    if (prbRivleresim.GetPosition() > 0 && prbRivleresim.GetPosition() < 100) {
    //        myMesazh.ShtoMesazhGabimi('Ju lutem prisni te mbaroje rivleresimi');
    //        e.cancel = true;
    //        status = false;
    //    }
    //    else {
    //Utils.shfaqLoadingGif();;
    //        tmRivleresim.SetEnabled(true);
    //        prbRivleresim.SetVisible(true);
    //        prbRivleresim.SetPosition(0);
    switch ((e.item.name)) {
        case 'Rivleresim':
            e.processOnServer = false;
            if (gvRivleresim2.GetVisibleRowsOnPage() == 0) {
                myMesazh.ShtoMesazhInformues(hfState.Get("msgRivleresimMagDuhetTeKaloniArtikujtQeDoniTeRivleresoniTekGridaPoshte"));
                return;
            }
            s.GetItemByName('Rivleresim').SetVisible(false);
            if (s.GetItemByName('Riruaj'))
                s.GetItemByName('Riruaj').SetVisible(false);
            s.GetItemByName('Stop').SetVisible(true);
            Utils.shfaqLoadingGif("#ASPxSplitter1");
            hfState.Set("Veprimi", 'Rivleresim');
            ProgressBar1.startTask();
            break;
        case 'Riruaj':
            e.processOnServer = false;
            if (gvRivleresim2.GetVisibleRowsOnPage() == 0) {
                myMesazh.ShtoMesazhInformues(hfState.Get("msgRivleresimMagDuhetTeKaloniArtikujtQeDoniTeRivleresoniTekGridaPoshte"));
                return;
            }
            s.GetItemByName('Rivleresim').SetVisible(false);
            s.GetItemByName('Riruaj').SetVisible(false);
            s.GetItemByName('Stop').SetVisible(true);
            Utils.shfaqLoadingGif("#ASPxSplitter1");
            hfState.Set("Veprimi", 'Riruaj');
            ProgressBar1.startTask();
            break;
        case 'Stop':
            e.processOnServer = false;
            s.GetItemByName('Rivleresim').SetVisible(true);
            s.GetItemByName('Riruaj').SetVisible(true);
            s.GetItemByName('Stop').SetVisible(false);
            ProgressBar1.stopTask();
            gvRivleresim.PerformCallback();
            gvRivleresim2.PerformCallback();
            var extraData = ProgressBar1.getExtraData();
            if (extraData)
                myMesazh.ShtoMesazhInformues(hfState.Get("msgRivleresimMagazineUNdaluaTek") + ' ' + ProgressBar1.getValue() + ' %' + extraData);
            else
                myMesazh.ShtoMesazhInformues(hfState.Get("msgRivleresimMagazineUNdaluaTek") + ' ' + ProgressBar1.getValue() + ' %');
            Utils.hiqLoadingGif("#ASPxSplitter1");
            break;
    }
    //    }
}
function menuInit(s, e) {
    s.GetItemByName('Stop').SetVisible(false);
}
function onTaskRunning() {
    window.parent.SessionTimeout.sendKeepAlive();
}
function onTaskDone() {
    if (ProgressBar1.getValue() == 100) {
        myMesazh.ShtoMesazhSuksesi(hfState.Get("msgRivleresimMagazinePerfundoiMeSukses"));
        Utils.hiqLoadingGif("#ASPxSplitter1");
        gvRivleresim.PerformCallback();
        gvRivleresim2.PerformCallback();
    }
    else {
        var extraData = ProgressBar1.getExtraData();
        var today = new Date();
        if (today.getHours() < 18)
            myMesazh.ShtoMesazhInformues("Ju lutem provoni pas ores 18: 00");
        else {
            var mesazhInformues = hfState.Get("msgRivleresimMagazineUNdaluaTek") + ' ' + ProgressBar1.getValue() + ' % ';
            if (extraData)
                mesazhInformues = mesazhInformues + extraData;
            myMesazh.ShtoMesazhInformues(mesazhInformues);
        }
        
        Utils.hiqLoadingGif("#ASPxSplitter1");
        gvRivleresim.PerformCallback();
        gvRivleresim2.PerformCallback();
    }
    ASPxMenu1.GetItemByName('Rivleresim').SetVisible(true);
    ASPxMenu1.GetItemByName('Riruaj').SetVisible(true);
    ASPxMenu1.GetItemByName('Stop').SetVisible(false);

}

function onTaskError() {
    myMesazh.ShtoMesazhGabimi(hfState.Get("msgRivleresimMagazineNdodhi1Gabim"));
    Utils.hiqLoadingGif("#ASPxSplitter1");
    gvRivleresim.PerformCallback();
    gvRivleresim2.PerformCallback()
}

//function timerTick(s, e) {
//    prbRivleresim.SetPosition(prbRivleresim.GetPosition());
//}

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
//    if (status)
//        tmRivleresim.SetEnabled(false);
//    pastro();
}

var arrSel = new Array();
var arrUnSel = new Array();
var count = 0;
var count1 = 0;
function pastro() {
    arrSel = new Array();
    arrUnSel = new Array();
    count = 0;
    count1 = 0;
}

function gvRivleresimRowDblClick(s, e) {
    
}

function kundert() {

    if (gvRivleresim.cpNoRows > 10 * (gvRivleresim.cpNoPage + 1)) {
        for (l = 10 * gvRivleresim.cpNoPage; l < 10 * (gvRivleresim.cpNoPage + 1); l++) {

            gvRivleresim.SelectRowOnPage(l, !gvRivleresim.IsRowSelectedOnPage(l));

        }
    }
    else {
        for (l = 10 * gvRivleresim.cpNoPage; l < gvRivleresim.cpNoRows; l++) {

            gvRivleresim.SelectRowOnPage(l, !gvRivleresim.IsRowSelectedOnPage(l));


        }
    }
}
function KlikoTeGjitha() {
    gvRivleresim.SelectAllRowsOnPage();

}
function HiqTeGjitha() {
    gvRivleresim.UnselectRows();


}
var arrSel2 = new Array();
var arrUnSel2 = new Array();
var count2 = 0;
var count21 = 0;
function SelectionChange2(s, e) {


}
function kundert2() {
    if (gvRivleresim2.cpNoRows > 10 * (gvRivleresim2.cpNoPage + 1)) {
        for (l = 10 * gvRivleresim2.cpNoPage; l < 10 * (gvRivleresim2.cpNoPage + 1); l++) {
            gvRivleresim2.SelectRowOnPage(l, !gvRivleresim2.IsRowSelectedOnPage(l));
        }
    }
    else {
        for (l = 10 * gvRivleresim2.cpNoPage; l < gvRivleresim2.cpNoRows; l++) {
            gvRivleresim2.SelectRowOnPage(l, !gvRivleresim2.IsRowSelectedOnPage(l));
        }
    }
}

function KlikoTeGjitha2() {
    gvRivleresim2.SelectAllRowsOnPage();
}

function HiqTeGjitha2() {
    gvRivleresim2.UnselectRows();
}

function dtePeriudhaChanged(s, e) {
    if (dtePeriudhaDeri.GetDate() < dtePeriudhaNga.GetDate()) {
        myMesazh.ShtoMesazhInformues(hfState.Get("MsgDataGabim"));
        dtePeriudhaDeri.SetDate(dtePeriudhaNga.GetDate());
    }
}