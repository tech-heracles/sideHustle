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
    prm.add_endRequest(EndRequestHandler); changeName()
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('Restore.aspx', 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function EndRequestHandler(sender, args) {
    if (valid == true)
        myMesazh.ShtoMesazhSuksesi(text);
    else myMesazh.ShtoMesazhGabimi(text);
}
var text, valid
function kot(s, e) {
    valid = e.isValid;

    text = e.callbackData;
}