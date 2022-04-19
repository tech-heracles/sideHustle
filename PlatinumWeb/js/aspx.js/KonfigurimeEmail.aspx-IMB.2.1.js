function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('KonfigurimeEmail.aspx',0);
    window.parent.createCookie('adresa', 'KonfigurimeEmail.aspx', 1);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(myMesazh.EndRequestTimer);
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