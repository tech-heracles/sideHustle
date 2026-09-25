
function SetSplitterPaneContentUrl(pane, contentUrl) {
    //            // ASPxClientSplitterPane.RefreshContentUrl()   
}

function RedirectWindow(url) {
    var parentWindow = window.parent;
    parentWindow.RedirectWindow(url);
}

function OnGridDoubleClick(s,e) {
    btnOk.DoClick();
}
function OnGetRowValues(values) {
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
}


function setSize() {
    var defaultWidth = 700;
    var width = document.body.clientWidth;

    if (width < 700) {
        grid.SetWidth(width);
        $(".content").width(width);
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
function updateSessionStorage() {
    sessionStorage.setItem("filterNdermarrje", sessionStorage.getItem("filterNdermarrje") == null ? JSON.stringify([]) : sessionStorage.getItem("filterNdermarrje"))
    checkIfSessionStorageExistsAndupdateSessionStorage(hfState.Get("organization"), $("#grid_ListLoginNdermarrje_DXFREditorcol1_I").val(), $("#grid_ListLoginNdermarrje_DXFREditorcol3_I").val());
}
function checkIfSessionStorageExistsAndupdateSessionStorage(organization, ndermarrje, viti) {
    console.log(hfState.Get("organization"))
    if (sessionStorage.getItem("filterNdermarrje") != null) {
        var obj = JSON.parse(sessionStorage.getItem("filterNdermarrje"));
        if (obj.length == 0) {
            obj.push({ organization: organization, ndermarrje: ndermarrje, viti: viti });
            sessionStorage.setItem("filterNdermarrje", JSON.stringify(obj))
            return;
        }
        for (var i = 0; i < obj.length; i++) {
            if (obj[i].organization == organization) {
                obj[i].ndermarrje = ndermarrje;
                obj[i].organization = organization;
                obj[i].viti = viti;
                sessionStorage.setItem("filterNdermarrje", JSON.stringify(obj));
            }
            else {
                obj.push({ organization: organization, ndermarrje: ndermarrje, viti: viti });
                sessionStorage.setItem("filterNdermarrje", JSON.stringify(obj))
            }
            
        }
    }
}
function setSessionValue() {
    var obj = JSON.parse(sessionStorage.getItem("filterNdermarrje"));
    var org = hfState.Get("organization");
    for (var i = 0; i < obj.length; i++) {
        if (org == obj[i].organization) {
            ASPxTextBoxNdermarrja.SetText(obj[i].ndermarrje);
            ASPxTextBoxViti.SetText(obj[i].viti);
        }
    }
    var filterButton = document.getElementById("filterButton");
    filterButton.click();
}
