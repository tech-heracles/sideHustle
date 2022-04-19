$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                btnRuaj.DoClick();
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
function onloadNdryshimFjalekalimi() {
        try {
            //duhet t'i bej refresh footerit te spliterit ne faqen kryesore qe te ngarkohen keto te dhena
            Utils.SetOrRefreshSplitterPaneContentUrl("Footer", "FooterPanelInfo.aspx");
        } catch (e) {}

    }
function changeName() {
    //myFaqeCelje.shtoHandlerSession();
    //window.parent.callWebServiceKtheInfoLart('NdryshimFjalekalimi.aspx');
    //window.parent.createCookie('adresa', 'NdryshimFjalekalimi.aspx', 1);
    //var prm = Sys.WebForms.PageRequestManager.getInstance();
    //prm.add_endRequest(myFaqeCelje.EndRequestTimer);
    myFaqeCelje.changeName('NdryshimFjalekalimi.aspx', 0, null);
}

function capitaliseFirstLetter() {
    var perdorues = document.getElementById("lblPerdorues");
    return perdorues.charAt(0).toUpperCase() + string.slice(1);
}

function kontrolloPassword(s, e) {
    // var valid = password_TextBox.GetIsValid();
    //if (!valid) {
    var gjatesiPass = s.GetValue();
    if (hfGjatesiMinPassword !== 'undefined') {
        if (hfGjatesiMinPassword.Contains('GjatesiMinPass')) {
            var gjatesiMinPass = hfGjatesiMinPassword.Get('GjatesiMinPass');
        }
    }
    if (gjatesiPass.length < gjatesiMinPass) {
        e.isValid = false;
        e.errorText = hfState.Get('msgPerdoruesitMinGjatesiPassword') + ' ' + gjatesiMinPass + ' ' + hfState.Get('msgPerdoruesitMinKarakterePass');
        myMesazh.ShtoMesazhGabimi(hfState.Get('msgPerdoruesitMinGjatesiPassword') + ' ' + gjatesiMinPass + ' ' + hfState.Get('msgPerdoruesitMinKarakterePass'));
    }

}

function EndRequestHandler(sender, args) {
    if (hfPassPerkohshem.Contains('PassPerkohshem') || hfSkaduarPass.Contains('PassSkaduar')) {
        if (hfPassPerkohshem.Get('PassPerkohshem') === 'Po' || hfSkaduarPass.Get('PassSkaduar') === 'Po') {
            onloadNdryshimFjalekalimi();
        }
    }
}