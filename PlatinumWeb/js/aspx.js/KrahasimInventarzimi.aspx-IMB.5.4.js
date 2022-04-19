; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvEkzistuese";

var pageState = {
    RegjistrimInventarizimiPageId: '',
    Init:function(){
        this.RegjistrimInventarizimiPageId = Utils.getUrlVar('RegjistrimInventarizimiPageId')
    }
};

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
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
    $(window).on('load', function () {
        Init();
    });
});

var btnFiltrat;
///aplikon filtrat ne gride
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val('Filtra');
    if (grida == "gvEkzistuese")
        myMenu.aplikoFiltra(s, e, gvEkzistuese, "", '');
    else if (grida == "gvPerbashket")
        myMenu.aplikoFiltra(s, e, gvPerbashket, "", '');
    else if (grida == "gvMagTjeter")
        myMenu.aplikoFiltra(s, e, gvMagTjeter, "", '');
    else if (grida == "gvJoEkzistues")
        myMenu.aplikoFiltra(s, e, gvJoEkzistues, "", '');

}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvEkzistuese, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvEkzistuese, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvEkzistuese, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvEkzistuese, indexSel);
}

//kur ndryshon tabin del nga editimi
function ndryshimTabi(tab) {

   
    if (tab.GetText() == "Art. Program") {
        grida = "gvEkzistuese";
        try {
            ASPxMenu1.GetItemByName('ListaGabimeve').SetEnabled(false);
        } catch (ee) { }
        ASPxMenu1.GetItemByName('Gjenero').SetEnabled(true);
    }
    else if (tab.GetText() == "Art. e perbashket") {
        grida = "gvPerbashket";
        try {
            ASPxMenu1.GetItemByName('ListaGabimeve').SetEnabled(true);
        } catch (ee) { }
        if (Utils.getUrlVar('lloj') == 'agj')
            ASPxMenu1.GetItemByName('Gjenero').SetEnabled(false);
        else ASPxMenu1.GetItemByName('Gjenero').SetEnabled(true);
    }
    else if (tab.GetText() == "Art. magazine tjeter"||tab.GetText()=="Art jo ne mag") {
        grida = "gvMagTjeter";
        try {
            ASPxMenu1.GetItemByName('ListaGabimeve').SetEnabled(true);
        } catch (ee) { }
        if (Utils.getUrlVar('lloj') == 'agj')
            ASPxMenu1.GetItemByName('Gjenero').SetEnabled(true);
        else ASPxMenu1.GetItemByName('Gjenero').SetEnabled(false);
    }
    else if (tab.GetText() == "Art. jo ne program") {
        grida = "gvJoEkzistues";
        try {
            ASPxMenu1.GetItemByName('ListaGabimeve').SetEnabled(true);
        } catch (ee) { }
        ASPxMenu1.GetItemByName('Gjenero').SetEnabled(false);
    }
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

function EndRequestHandler(sender, args) {
    ndryshimTabi(PageControl.GetActiveTab());
    if ($('#hfRaport').val() == "po") {
        window.open("RaportiShpejte.aspx?emriReal=gabimeImporti&printo=0&Sesioni=false&db=jo&vjen=krahasimi&scopeID=" + Utils.getUrlVar("scopeID"));

        $('#hfRaport').val('jo');
    }
    
}
function Init() {
    pageState.Init();
    changeName();
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
///shfaq emrin e faqes
function changeName() {
    ndryshimTabi(PageControl.GetActiveTab());
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('KrahasimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), 0, null);

}


var editorKodi;


///verprimet e menu clickut

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');

    if (e.item.name === 'Anullo') {
        e.processOnServer = false;
        myFaqeCelje.kontrolloTeDrejta('RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj'));
    }
    else if (e.item.name === 'Gjenero') {
        e.processOnServer = false;
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloNqsKaTeDhenaNeGride"),
            data: JSON.stringify({ index: PageControl.GetActiveTab().index, llojiNv: Utils.getUrlVar('lloj'), idNdermarrjeVit: hfState.Get('idNdermarrjeVit') })
        }).done(function (result) {
            if (result) {
                if (PageControl.GetActiveTab().index != 2)
                    myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=dalje' + '&id=' + 0 + '&numer=' + 0 + '&indexrow=' + 0 + '&shtim_modifikim=inventarizim&niveli=' + $('#hfNiveli').val() + '&konfigurim=' + $('#hfKonfigurimi').val() + '&fsh=po&mag=' + btneMagazina.GetValue() + '&dtdok=' + dteDtDok.GetDate().toDateString() + '&tab=' + PageControl.GetActiveTab().index + '&llojinv=' + Utils.getUrlVar('lloj') + '&RegjistrimInventarizimiPageId=' + pageState.RegjistrimInventarizimiPageId);
                else
                    myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=dalje' + '&id=' + 0 + '&numer=' + 0 + '&indexrow=' + 0 + '&shtim_modifikim=inventarizim&niveli=' + $('#hfNiveli2').val() + '&konfigurim=' + $('#hfKonfigurimi2').val() + '&fsh=po&mag=' + btneMagazina.GetValue() + '&dtdok=' + dteDtDok.GetDate().toDateString() + '&tab=' + PageControl.GetActiveTab().index + '&llojinv=' + Utils.getUrlVar('lloj') + '&RegjistrimInventarizimiPageId=' + pageState.RegjistrimInventarizimiPageId);
            }
            else {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKaArtikujTePerbashket"));
                return;
            }
        });        
    }
}

function EndCallbackGrida(s, e) {
    enable();
    if (PageControl.GetActiveTab().index == 3) ASPxMenu1.GetItemByName('Gjenero').SetEnabled(false);
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("552", cmbKonfigurimi.GetText());
}

