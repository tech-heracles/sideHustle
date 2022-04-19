//;$(document).ready(function () {
//    mbushListen()
//    $(window).bind("beforeunload", function () { fshiListeFilesh(); });

//});

//function fshiListeFilesh() {

//}

//function GetTextAsHTMLLink(text, url) {
//    var html = '<a href="javascript:void(0)" onclick="ShowScreenshot(\'' + url + '\')">';
//    html += text;
//    html += '</a>';
//    return html;
//}

//function mbushListen() {
//    lstFiles.ClearItems();
//    var objektet = new Array();
   
//        if (hfMyArkiva.Contains("ArkivaSaved")) {
//            objektet = JSON.parse(hfMyArkiva.Get("ArkivaSaved"))
//            arr = objektet;
//            hfMyArkiva.Set("ArkivaUpload", arr);

//            for (var i = 0; i < objektet.length; i++) {
//                lstFiles.AddItem([GetTextAsHTMLLink(objektet[i].FileName, objektet[i].Path)], objektet[i].Path);
//            }
//        }
   
       
//}

//function ShowScreenshotWindow(evt, link) {
//    ShowScreenshot(link.href);
//    evt.cancelBubble = true;
//    return false;
//}

//function ShowScreenshot(src) {
//    var screenLeft = document.all && !document.opera ? window.screenLeft : window.screenX;
//    var screenWidth = screen.availWidth;
//    var screenHeight = screen.availHeight;
//    var zeroX = Math.floor((screenLeft < 0 ? 0 : screenLeft) / screenWidth) * screenWidth;

//    var windowWidth = 1200;
//    var windowHeight = 900;
//    var windowX = parseInt((screenWidth - windowWidth) / 2);
//    var windowY = parseInt((screenHeight - windowHeight) / 2);
//    if (windowX + windowWidth > screenWidth)
//        windowX = 0;
//    if (windowY + windowHeight > screenHeight)
//        windowY = 0;

//    windowX += zeroX;

//    var popupwnd = window.open(src, '_blank', "left=" + windowX + ",top=" + windowY + ",width=" + windowWidth + ",height=" + windowHeight + ", scrollbars=no, resizable=no", true);
//    if (popupwnd != null && popupwnd.document != null && popupwnd.document.body != null) {
//        popupwnd.document.body.style.margin = "0px";
//    }
//}