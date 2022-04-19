;
var editmode = false;
var indexEdit = -1;
var indexModifiko;

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

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}


//thirret ne loading te faqes
function Init() {
    changeName();
    editmode = false;
    indexEdit = -1;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
}
//thirret kur ndodh nje event ne server postback
function EndRequestHandler(sender, args) {

}
//shfaq emrin e komponentes tek faqja
function changeName() {
    myFaqeCelje.changeNameRegjistrime('KonfigurimeQK.aspx',0);

}

//veprimet e menuse
function menu_click(s, e) {//po
   
}

function changeRadio(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrPershkrimMenyreMesazhi"),
        data: JSON.stringify({ idmenyre: s.GetValue(), idGjuha: hfState.Get("idGjuha") })
    }).done(SucceededCallbackData);
}
function SucceededCallbackData(result) {
    lblMetodePershkrimi.SetText(result);

}
