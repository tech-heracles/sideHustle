;
function EPaySlip() {


    var krijoMenuEpaySlip = function (eshteHomePage, shtoAnnualBonus) {

        var html = htmlHomePage = "";
        var $ul = $('#ulMenu');
        var $ulHomePage = $('#myHomePage');


        if (eshteHomePage) {
            var width = window.document.body.clientWidth;
            if (shtoAnnualBonus) {
                htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href='RaportiEPaySlip.aspx?idraporti=285&windowWidth=" + width + "&radButon=0&dateNga=" + dtdoknga.value + "&dateDeri=" + dtdokderi.value + "&idFiltri=0&Filtro=false&vjenNga=epayslip'><img class='ui-li-thumb' src='CRM/AlphaWeb.png'><h2>MEMO of Annual Bonus</h2></a></li>";
            }
            htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href='RaportiEPaySlip.aspx?idraporti=281&windowWidth=" + width + "&radButon=0&dateNga=" + dtdoknga.value + "&dateDeri=" + dtdokderi.value + "&idFiltri=0&Filtro=false&vjenNga=epayslip'><img class='ui-li-thumb' src='CRM/AlphaWeb.png'><h2 style='font-size:0.85em;'>MEMO of Annual Declaration</h2></a></li>";
            htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href='RaportiEPaySlip.aspx?idraporti=291&windowWidth=" + width + "&radButon=0&dateNga=" + dtdoknga.value + "&dateDeri=" + dtdokderi.value + "&idFiltri=0&Filtro=false&vjenNga=epayslip'><img class='ui-li-thumb' src='CRM/AlphaWeb.png'><h2>PaySlip</h2></a></li>";
        }
        $ulHomePage.html(htmlHomePage);
    };
    var logoutUrl = function () {
        return Utils.getServerUrlHost() + '/' + Paths.epaySlipLoginPath+'?arsye=logout';
    }
    return {
        krijoMenu: krijoMenuEpaySlip,
        logoutPath: logoutUrl
    }
}