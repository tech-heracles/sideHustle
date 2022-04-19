;$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();

                break;
            default:
                break;
        }
    });
});

function changeName() {
    //myFaqeCelje.shtoHandlerSession();
    //window.parent.callWebServiceKtheInfoLart('NdryshimFjalekalimi.aspx');
    //window.parent.createCookie('adresa', 'NdryshimFjalekalimi.aspx', 1);
    //var prm = Sys.WebForms.PageRequestManager.getInstance();
    //prm.add_endRequest(myFaqeCelje.EndRequestTimer);
    myFaqeCelje.changeName('NdryshimCmimi.aspx', 0, null);
}

function EndRequestHandler(sender, args) {

}
var identikuesPerPopupArtikulli = 'NdryshimCmimi'
function ButtonClickArtikulli() {

    myButtonClickLupa.LupaUniversal_Click('Zgjidh artikullin', 'LupaArtikull.aspx', 600, 600);
}

function menu_click(s, e) {
    if (e.item.name == 'Ruaj') {
        var valid = myFaqeCelje.validim(s, e);
        if (!valid) {
            Utils.hiqLoadingGif();;
            e.processOnServer = false; click = false;
            return;
        }

        Utils.shfaqLoadingGif();;


    }

}
