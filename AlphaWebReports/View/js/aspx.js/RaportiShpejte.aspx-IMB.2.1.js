function reportToolbarItemChanged(sender, e) {
    if (sender.name.indexOf('Style') > 0) {
        $('#reportStyle').val(sender.GetText());
        $('#hfRilodo').val(true);
        reportViewer.Refresh();
        ImbReportToolbar.ResetArsyeReload();
        return;
    }
    if (sender.name.indexOf('ScaleFactor') > 0) {
        var text = sender.GetText();
        if (text == "Page Width")
            $('#Scale').val(($(window).width() / $('#reportWidth').val() * 100));
        else
            $('#Scale').val(sender.GetValue());
        $('#hfRilodo').val(true);
        rvRaporti.Refresh();
        ImbReportToolbar.ResetArsyeReload();
        return;
    }
    $('#hfRilodo').val(false);
}


function RpShpejtPageload(s, e) {
    ImbReportToolbar.OnPageLoad(e);
}

function rvRaportiEndCallback(s, e) {
    if (Utils.getUrlVar("printo") == "false") {
        var newUrl = Utils.getUrlSettingVar("printo", "0");
        window.open(newUrl, '_blank');
    }
    else {
        var exePrint = ImbReportToolbar.GetToolbarClientObject("HfState").Get("EkzekutoPrintim");
        if (Utils.getUrlVar("printo") == "true" && (exePrint || exePrint==undefined)) {
            ImbReportToolbar.GetToolbarClientObject("HfState").Set("EkzekutoPrintim", false);
            rvRaporti.Print();
        }
    }
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
});