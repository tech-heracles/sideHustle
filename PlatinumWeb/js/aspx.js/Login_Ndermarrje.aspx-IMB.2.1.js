
function SetSplitterPaneContentUrl(pane, contentUrl) {
    //            var parentWindow = window.parent;
    //            var paneContent = parentWindow.splitter.GetPaneByName(pane);
    //            // paneContent.SetContentUrl(contentUrl);
    //            // ASPxClientSplitterPane.RefreshContentUrl()   
    //            var e = Math.floor(Math.random() * 100000).toString();
    //            contentUrl = 'FooterPanelInfo.aspx?' + e;
    //            paneContent.SetContentUrl(contentUrl);
    //            paneContent.RefreshContentUrl();
}

function RedirectWindow(url) {
    var parentWindow = window.parent;
    parentWindow.RedirectWindow(url);
}

function OnGridDoubleClick(s,e) {
    btnOk.DoClick();
    //            grid.GetRowValues(index, 'IDNDERMARJE;NDERMARJEKODI;VITI;IDNDERVITI', OnGetRowValues);
}
function OnGetRowValues(values) {
    // window.location = 'Default.aspx?id=' + values[0] + '&kodi=' + values[1] + '&viti=' + values[2] + '&idnderviti=' + values[3];
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else
        var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}
$(document).ready(function () {
    Utils.shfaqLoadingGif();
    $(window).on('load', function () {
        Init();
        Utils.hiqLoadingGif();
        Utils.AppendLiveHelperChat(hfState.Get("chatAktiv"), hfState.Get("chatLink"), hfState.Get("chatPortHttp"), hfState.Get("chatPortHttps"));
    });
});
function Init(s, e) {
    btnOk.Focus();
    Utils.setApplicationLanguageId(hfState.Get("idGjuha"));
    //if (top.location == self.location) {
    //    createCookie('adresa', 'Login_Ndermarrje.aspx', 1);
    //    window.location = "FaqeKryesore.aspx";
    //}
}


function setSize() {
    var defaultWidth = 700;
    var width = document.body.clientWidth;

    if (width < 700) {
        grid.SetWidth(width);
        $(".content").width(width);
       // ResizePager();
    }
    else {

        if (grid.GetWidth() < defaultWidth)
            grid.SetWidth(defaultWidth);
        if ($(".content").width() < defaultWidth)
            $(".content").width(defaultWidth);
    }

}
function ResizePager() {
    setTimeout(function () {
        $("#grid_ListLoginNdermarrje_DXPagerBottom").css({ "min-width": '150px' });
    }, 10)
}
window.onresize = function () {
    setSize();
}