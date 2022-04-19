$(document).ready(function () {
    $(window).bind("load", function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    });
});

var aspxPreviewImgSrc;

function Ngarkuesi_NeNgarkimFillim() {
    btnNgarko.SetEnabled(false);
}

function Ngarkuesi_NeFileNgarkimPlotesuar(args) {
    var imgSrc = aspxPreviewImgSrc;
    if (args.isValid) {
        var date = new Date();
        imgSrc = "images/" + args.callbackData + "?dx=" + date.getTime();
    }
}

function Ngarkuesi_NeFiletNgarkimPlotesuar(args) {
    UpdateButoniNgarkim();
}

function UpdateButoniNgarkim() {
    btnNgarko.SetEnabled(ngarkuesi.GetText(0) != "");
}

function getPreviewImageElement() {
    return document.getElementById("previewImage");
}

function menu_click(s, e) {
    if (e.item.name === 'Anullo') {
        window.parent.merrImazhPerdoruesi();
        window.parent.popupUniversal.Hide();
    }
}

function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    if (hf.val() === "true") {
        window.parent.merrImazhPerdoruesi();
        window.parent.popupUniversal.Hide();
    }
}