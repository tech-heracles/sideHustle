;

//anullon veprimin e enterit
function enter() {
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;

    }
}

jQuery(document).ready(function () {//po
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(300))
                return;
        }
        catch (ee) {
        }
    }).trigger('resize');    
    Utils.resizeSplitter();
});

function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    var lloji = Utils.getUrlVar('lloji');
    try { window.parent.callWebServiceKtheInfoLart('ImportWK.aspx?lloji=' + lloji, 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

function Init() {//po
    if (typeof (isPostBack) == "undefined") {
        changeName();
        $("#divgride1").show();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}

/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    gvImport.UnselectAllRowsOnPage();
    gvImport.PerformCallback('pastro');
}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    var hf = document.getElementById("status1");
    hf.value = "false";
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = document.getElementById("status1");
    click = false;
    if (hf.value === "true") {
        var lloji = Utils.getUrlVar('lloji');
        myFaqeCelje.kontrolloTeDrejta('ImportWK.aspx?lloji=' + lloji, true);
    }
    if (hf.value == "import") {
        gvImport.PerformCallback("imp");
    }
    Utils.hiqLoadingGif();;
}

var raportgab = false;
/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
   if (e.item.name === "Importo") {
        if (gvImport.cpNoRows == 0) {
            myMesazh.ShtoMesazhGabimi(hfTeDrejta.Get("msgSkaRreshtaPerImport"));
            e.processOnServer = false;
            return;
        }
        Utils.shfaqLoadingGif();;
        raportgab = true;
    }
    else if (e.item.name === "Kontrollo") {
        if (gvImport.cpNoRows == 0) {
            myMesazh.ShtoMesazhGabimi(hfTeDrejta.Get("MsgSkaRreshtaGridaPerKontroll")); 
            e.processOnServer = false;
            return;
        }
        raportgab = true;
        Utils.shfaqLoadingGif();;
    }
    else if (e.item.name === "ListaGabimeve") {
        e.processOnServer = false;
        if (raportgab == false)
            myMesazh.ShtoMesazhGabimi(hfTeDrejta.Get("msgKontrolliPerGabimet"));
        else
            window.open("RaportiShpejte.aspx?emriReal=gabimeImporti&printo=0&Sesioni=false&db=jo&scopeID=" + Utils.getUrlVar("scopeID"));

    }
    else if (e.item.name === 'Ngarko') {
        raportgab = false;
        e.processOnServer = false;
        gvImport.PerformCallback('ngarko');
    }
    else if (e.item.name === 'Shto') {
        var lloji = Utils.getUrlVar('lloji');
        myFaqeCelje.kontrolloTeDrejta('ImportWK.aspx?lloji=' + lloji, true);
        e.processOnServer = false;
    }
}

var click = false;
/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    pastro();
    pastroFushatKokes();
    var hf = document.getElementById("hfShtimModifikim");

    raportgab = false;
    hf.value = "shtim";
    $('#ASPxSplitter1_hl').empty();
}
function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Callback_Error(s, e) {
    myMesazh.ShtoMesazhGabimi(e.message);
    e.handled = true;
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');}