 function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('Shto_KushtDergimi.aspx',0);
    window.parent.createCookie('adresa', 'Shto_KushtDergimi.aspx', 1);
}