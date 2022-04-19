function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('Shto_MenyreTransporti.aspx',0);
    window.parent.createCookie('adresa', 'Shto_MenyreTransporti.aspx', 1);
}