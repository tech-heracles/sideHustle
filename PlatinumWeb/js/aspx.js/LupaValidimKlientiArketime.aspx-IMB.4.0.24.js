



$(window).load(function () {//po
    Init();
});


function Init() {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    myMesazh.shtoHandler();
}

function EndRequestHandler(sender, args) {//po
    if ($('#hfStatus').val() == "True") {
        var TeDhenaKlienti = JSON.parse($("#hfTeDhenaKlienti").val());
        parent.VendosTeDhenaPerKlienteNgaBRM(TeDhenaKlienti);
    }
    Utils.hiqLoadingGif();
}
function menu_click(s, e) {

}

function btnValidoClick() {

    Utils.shfaqLoadingGif();;
}
