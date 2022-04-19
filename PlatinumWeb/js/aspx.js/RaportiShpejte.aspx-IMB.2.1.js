function rvRaportiEndCallback(s, e) {
    if (Utils.getUrlVar("printo") == "false") {
        var newUrl = Utils.getUrlSettingVar("printo", "0");
        window.open(newUrl, '_blank');
    }
    else {
        var exePrint = hfState.Get("EkzekutoPrintim");
        if (Utils.getUrlVar("printo") == "true" && (exePrint || exePrint==undefined)) {
            hfState.Set("EkzekutoPrintim", false);
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
                break;
            default:
                break;
        }
    });
});


function InitReportViewer(s, e) {
    DevExpress.Report.Preview.AsyncExportApproach = false;
    RaporteUtils.InitReportViewer(s, e);
    rvRaportiEndCallback(s, e);
}

function CustomizeMenuActions(s, e) {
    RaporteUtils.CustomizeMenuActions(s, e);
}

function ASPxCallbackPanel1_BeginCallback(s, e) {
    RaporteUtils.DeactivateViewer();
}

window.onbeforeunload = function (s, e) {
    rvRaporti.Close();
};
