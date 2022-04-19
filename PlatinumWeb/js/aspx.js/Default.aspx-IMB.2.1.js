
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}
$(document).ready(function () {
    onloadDefault();
});
function onloadDefault() {
        //var newMenu = Menu_IMB.prototype.krijoMenuNeDocReady("body");
        //if (window.parent.splitter.GetPaneByName('paneKryesor').GetContentUrl() != 'Default.aspx')
        //    window.parent.splitter.GetPaneByName('paneKryesor').SetContentUrl('Default.aspx');
        myCookies.readCookie('adresa', 'Default.aspx', 1)
        try {
            parent.window.btn.DoClick();     //kam zgjedhur ndermarrjen dhe eshte caktuar periudha aktuale, jane ne sesion
            // myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim');
            //duhet t'i bej refresh footerit te spliterit ne faqen kryesore qe te ngarkohen keto te dhena
            Utils.SetOrRefreshSplitterPaneContentUrl("Footer", "FooterPanelInfo.aspx");
        } catch (e) {
        }    
    //  parent.window.document.getElementById('Button1').click();

}
function openHelpWindow(s, e, helpUrl) {
    window.open(helpUrl, "_blank");
}
      