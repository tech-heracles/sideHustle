;
var pageState = false;

function LostFocus(s, e) {
    gvRivleresim.PerformCallback(s.GetValue());
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

    switch ((e.item.name)) {
        case 'Rivleresim':
            e.processOnServer = false;
            if (gvRivleresim2.GetVisibleRowsOnPage() == 0) {
                myMesazh.ShtoMesazhInformues(hfState.Get("msgDuhetTeKaloniArtikujtQeDoniTeLlogarisniTekGridaPoshte"));
                return;
            }
            if (cmbLloji.GetValue() == "False") {
                for (l = 0 ; l < gvRivleresim2.cpNoRows; l++) {

                    var idkodi = gvRivleresim2.GetRowKey(l);
                    if (!hfSeriale.Contains(idkodi + '_' + 0) || hfSeriale.Get(idkodi + '_' + 0) == "[]")
                    { myMesazh.ShtoMesazhInformues(hfState.Get("msgKaArtikujPaSerialeNeGride")); return; }

                }
            }
            s.GetItemByName('Stop').SetVisible(true);
            Utils.shfaqLoadingGif();;
            ProgressBar1.startTask();
            break;
        case 'Stop':
            e.processOnServer = false;
            myMesazh.ShtoMesazhInformues(hfState.Get("msgRillogaritjaUNdaluaTek") + ' ' + ProgressBar1.getValue() + ' %');
            s.GetItemByName('Stop').SetVisible(false);
            ProgressBar1.stopTask();
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
    if (ProgressBar1.getValue() == 100)
        myMesazh.ShtoMesazhSuksesi(hfState.Get("msgRillogaritjaPerfundoiMeSukses"));
    else {
        var extraData = JSON.parse(ProgressBar1.getExtraData());
        if (extraData.Tipi === 0)
            myMesazh.ShtoMesazhGabimi(extraData.PershkrimMesazhi);
        else if (extraData.Tipi != 0 &&  extraData.PershkrimMesazhi) 
            myMesazh.ShtoMesazhInformues(hfState.Get("msgRillogaritjaUNdaluaTek") + ' ' + ProgressBar1.getValue() + ' % ' + extraData.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhInformues(hfState.Get("msgRillogaritjaUNdaluaTek") + ' ' + ProgressBar1.getValue() + ' % ');
        //myMesazh.ShtoMesazhInformues(hfState.Get("msgRillogaritjaUNdaluaTek") + ' ' + ProgressBar1.getValue() + ' %');
    }
    ASPxMenu1.GetItemByName('Stop').SetVisible(false);
}
function onTaskError() {
    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhi1GabimGjateRillogaritjes"));
}

function Init() {
    myMesazh.shtoHandler(); myFaqeCelje.shtoHandlerSession();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler); changeName();
}
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('RillogaritjeAmortizimi.aspx', 0); } catch (e) { }
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
function SelectionChange(s, e) {


}
function kundert() {

    if (gvRivleresim.cpNoRows > 10 * (gvRivleresim.cpNoPage + 1)) {
        for (l = 10 * gvRivleresim.cpNoPage; l < 10 * (gvRivleresim.cpNoPage + 1) ; l++) {

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
        for (l = 10 * gvRivleresim2.cpNoPage; l < 10 * (gvRivleresim2.cpNoPage + 1) ; l++) {
            gvRivleresim2.SelectRowOnPage(l, !gvRivleresim2.IsRowSelectedOnPage(l));
        }
    }
    else {
        for (l = 10 * gvRivleresim2.cpNoPage; l < gvRivleresim2.cpNoRows; l++) {
            gvRivleresim2.SelectRowOnPage(l, !gvRivleresim2.IsRowSelectedOnPage(l));
        }
    }
}
function endcallback() {
    if (cmbLloji.GetValue() == "False") {
        if (gvRivleresim2.cpNoRows > 10 * (gvRivleresim2.cpNoPage + 1)) {
            for (l = 10 * gvRivleresim2.cpNoPage; l < 10 * (gvRivleresim2.cpNoPage + 1) ; l++) {
                var idkodi = gvRivleresim2.GetRowKey(l);
                if (hfSeriale.Contains(idkodi + '_' + 0) && hfSeriale.Get(idkodi + '_' + 0) != "[]")
                    Utils.ktheKontroll('Serial' + l).SetText("Me serial"); else Utils.ktheKontroll('Serial' + l).SetText("Pa serial");

            }
        }
        else {
            for (l = 10 * gvRivleresim2.cpNoPage; l < gvRivleresim2.cpNoRows; l++) {
                var idkodi = gvRivleresim2.GetRowKey(l);
                if (hfSeriale.Contains(idkodi + '_' + 0) && hfSeriale.Get(idkodi + '_' + 0) != "[]")
                    Utils.ktheKontroll('Serial' + l).SetText("Me serial"); else Utils.ktheKontroll('Serial' + l).SetText("Pa serial");
            }
        }
    }
}
function hiqTeSelectuara() {
    for (l = 0 ; l < gvRivleresim2.cpNoRows; l++) {
        if (gvRivleresim2.IsRowSelectedOnPage(l)) {
            var idkodi = gvRivleresim2.GetRowKey(l);
            if (hfSeriale.Contains(idkodi + '_' + 0))
                hfSeriale.Remove(idkodi + '_' + 0);
        }
    }
}
function KlikoTeGjitha2() {
    gvRivleresim2.SelectAllRowsOnPage();

}
function HiqTeGjitha2() {
    gvRivleresim2.UnselectRows();

}
var editor
function ButtonClickKodi(emer, key) {
    var idkodi = gvRivleresim2.GetRowKey(key);
    editor = Utils.ktheKontroll(emer);
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniSerialet"), 'LupaSeriale.aspx?vjennga=rillogaritja&magazina=0&id=' + idkodi + '&lastsel=' + 0 + '&mecope=true&sasia=' + 100000000000 + '&data=' + (new Date()) + '&iddokmag=0', 900, 600);

}
function KeyPressKodi(emer, key, e) {
    if (e.htmlEvent.which == 13) {
        var serial = Utils.ktheKontroll(emer).GetText();
        var idkodi = gvRivleresim2.GetRowKey(key);

        if (serial != "" && serial != "Me serial" && serial != "Pa serial") {


            var arrayMeSeriale = new Array();

            $.ajax({   
              pritPergjigje: true,
              url: Utils.getServerApiUrl("Rregjistrime", "CelSerialNqsNukEkziston"),
              data: JSON.stringify({ kodserial: serial, idndermarje: hfState.Get('idNdermarrje'), idperdoruesi: hfState.Get('idPerdoruesi'), idartikulli: idkodi, lastsel: 0, serialeekzistuese: hfSeriale.Contains(idkodi + '_' + 0) ? hfSeriale.Get(idkodi + '_' + 0) : "", serialeteperdoruraNeKeteFature: arrayMeSeriale, celnqsnukekziston: false, magazina: "", sasishuma: 0, iddokmodmagazine: 0 })
            }).done(SucceededCallbackSerial)

        }
        Utils.ktheKontroll(emer).SetText("");

    }
}
function LostFocusKodi(emer, key) {
    var idkodi = gvRivleresim2.GetRowKey(key);
    if (hfSeriale.Contains(idkodi + '_' + 0) && hfSeriale.Get(idkodi + '_' + 0) != "[]")
        Utils.ktheKontroll(emer).SetText("Me serial"); else Utils.ktheKontroll(emer).SetText("Pa serial");
}
function SucceededCallbackSerial(result) {
    if (result[2] != "") {
        myMesazh.ShtoMesazhGabimi(result[2]);
        return;
    }

    if (hfSeriale.Contains(result[0] + '_' + result[4])) {
        hfSeriale.Set(result[0] + '_' + result[4], result[1]);

    }
    else hfSeriale.Add(result[0] + '_' + result[4], result[1]);


}