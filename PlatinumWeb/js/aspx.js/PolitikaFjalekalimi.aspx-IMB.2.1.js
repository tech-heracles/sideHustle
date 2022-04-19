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

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('PolitikaFjalekalimi.aspx', 0);
    window.parent.createCookie('adresa', 'PolitikaFjalekalimi.aspx', 1);
}

function ndryshoHistorikun(s, e) {
    if (!cbRuajHistorikun.GetChecked())
        spinDiteHistoriku.SetEnabled(false);
    else
        spinDiteHistoriku.SetEnabled(true);
}

function ndryshoSkadiminPass(s, e) {
    if (!cbPassSkadon.GetChecked())
        sePassSkadonPas.SetEnabled(false);
    else
        sePassSkadonPas.SetEnabled(true);
}

function ndryshoBllokiminUserit(s, e) {
    if (!cbBllokoUser.GetChecked()) {
        seBllokoPas.SetEnabled(false);
        seMaxSession.SetEnabled(false);
    }
    else {
        seBllokoPas.SetEnabled(true);
        seMaxSession.SetEnabled(true);
    }
}


