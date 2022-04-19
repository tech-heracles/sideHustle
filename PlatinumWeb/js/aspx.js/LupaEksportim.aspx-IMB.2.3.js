function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        enabled();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

function EndRequestHandler(sender, args) {

}

function menu_click(s, e) {
    if (e.item.name == "OK") {
        myFaqeCelje.validim(s, e);
        if (rbTipi.GetValue() == "CSV" && (txtSimboliNdares.GetText() == "" && !cbSimboliNdares.GetChecked())) {
            myMesazh.ShtoMesazhGabimi('Shenoni karakterin ndares!');
            e.processOnServer = false;
            return;
        }
        Exporto.DoClick();
        //if (e.processOnServer)
        //    setTimeout(function () {
        //        window.parent.popupUniversal.Hide();
        //    }, 2000);
    }
    else window.parent.popupUniversal.Hide();
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
}).trigger('resize');

function enabled() {
    if (rbTipi.GetValue() == "XLS" || rbTipi.GetValue() == "XLSX") {
        txtSimboliNdares.SetText('');
        txtSimboliNdares.SetEnabled(false);
        cbSimboliNdares.SetChecked(false);
        cbSimboliNdares.SetEnabled(false);
        txtEmerSheet.SetEnabled(true);
    }
    else {
        txtEmerSheet.SetText('');
        txtSimboliNdares.SetEnabled(true);
        cbSimboliNdares.SetEnabled(true);
        txtEmerSheet.SetEnabled(false);
    }
}