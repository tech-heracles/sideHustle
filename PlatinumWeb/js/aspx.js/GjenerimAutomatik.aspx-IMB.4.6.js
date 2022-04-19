; 
var pageState = false;

jQuery(document).ready(function () {
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }

    }).trigger('resize');
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
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });

    
});

function menuClick(s, e) {
   
    switch ((e.item.name)) {
        case "Pastro":
            cmbLlojDate.SetSelectedIndex(2);
            e.processOnServer = false;
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dtePeriudhaNga.SetDate(hfPeriudheObj.fillimiPeriudha);
            dtePeriudhaDeri.SetDate(hfPeriudheObj.mbarimiPeriudha);
            gvGjenerimi.PerformCallback('pastro');
      
            break;
        case 'Gjenero':
            
            Utils.shfaqLoadingGif();;
           
            break;
      
    }
    //    }
}


function Init() {
    myMesazh.shtoHandler(); myFaqeCelje.shtoHandlerSession();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler); changeName();
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('GjenerimAutomatik.aspx', 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

function EndRequestHandler(sender, args) {
    Utils.hiqLoadingGif();;
 
}


function SelectionChange(s, e) {
       gvGjenerimi2.PerformCallback('select');

}

function KlikoTeGjitha() {
    gvGjenerimi.SelectAllRowsOnPage();

}
function HiqTeGjitha() {
    gvGjenerimi.UnselectRows();


}
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined) {
        lblKonfigurimi.SetText(pershkKonfigAmb);
       
    }
    
    callWebserviceKonfigurimi("546", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {

}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    gvGjenerimi.PerformCallback(idKomp + ";" + kodKonf);
 
}

function kerko() {
    gvGjenerimi.PerformCallback('kerko');
  
}
function FshiClicked(key) { //po
 
    gvGjenerimi2.PerformCallback(key);
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}