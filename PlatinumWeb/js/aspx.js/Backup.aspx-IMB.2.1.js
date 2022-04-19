

function LostFocus() {
    gvNdermarjet.PerformCallback('nder');
    gvNdermarjet2.PerformCallback();
    pastro();
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
    $(window).on('load', function () {
        Init();
    });

});

function Init() {
    myMesazh.shtoHandler(); myFaqeCelje.shtoHandlerSession();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    changeName();
}
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('Backup.aspx',0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function EndRequestHandler(sender, args) {
    if (status)
        tmNdermarje.SetEnabled(false);
    pastro();
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

    if (gvNdermarjet.cpNoRows > 10 * (gvNdermarjet.cpNoPage + 1)) {
        for (l = 10 * gvNdermarjet.cpNoPage; l < 10 * (gvNdermarjet.cpNoPage + 1); l++) {

            gvNdermarjet.SelectRowOnPage(l, !gvNdermarjet.IsRowSelectedOnPage(l));

        }
    }
    else {
        for (l = 10 * gvNdermarjet.cpNoPage; l < gvNdermarjet.cpNoRows; l++) {

            gvNdermarjet.SelectRowOnPage(l, !gvNdermarjet.IsRowSelectedOnPage(l));


        }
    }
}
function KlikoTeGjitha() {
    gvNdermarjet.SelectAllRowsOnPage();

}
function HiqTeGjitha() {
    gvNdermarjet.UnselectAllRowsOnPage();


}
var arrSel2 = new Array();
var arrUnSel2 = new Array();
var count2 = 0;
var count21 = 0;
function SelectionChange2(s, e) {


}
function kundert2() {
    if (gvNdermarjet2.cpNoRows > 10 * (gvNdermarjet2.cpNoPage + 1)) {
        for (l = 10 * gvNdermarjet2.cpNoPage; l < 10 * (gvNdermarjet2.cpNoPage + 1); l++) {
            gvNdermarjet2.SelectRowOnPage(l, !gvNdermarjet2.IsRowSelectedOnPage(l));
        }
    }
    else {
        for (l = 10 * gvNdermarjet2.cpNoPage; l < gvNdermarjet2.cpNoRows; l++) {
            gvNdermarjet2.SelectRowOnPage(l, !gvNdermarjet2.IsRowSelectedOnPage(l));
        }
    }
}
function KlikoTeGjitha2() {
    gvNdermarjet2.SelectAllRowsOnPage();

}
function HiqTeGjitha2() {
    gvNdermarjet2.UnselectAllRowsOnPage();

}


function Item_Click(s, e) {
    Utils.shfaqLoadingGif();; 
    if (prbNdermarje.GetPosition() > 0 && prbNdermarje.GetPosition() < 100)
    {
        myMesazh.ShtoMesazhGabimi('Ju lutem prisni te mbaroje backupi');
        e.cancel = true; status = false;
    }
    else
    { tmNdermarje.SetEnabled(true); prbNdermarje.SetPosition(0); status = true; }
}



function Tick_Position(s, e) {
    if(prbNdermarje.GetPosition()<98)
    prbNdermarje.SetPosition(prbNdermarje.GetPosition()+1);
}
function Init_tmNdermarje(s, e) {
    tmNdermarje.SetEnabled(false); 
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}