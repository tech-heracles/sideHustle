;
var indexModifiko;
var indexSel = 0;
var levizNgaShigjetat = false;
var id;
var numur;
var kodi;
var focuschange = false;
var identifikuesPyetje;
function OnGetRowValues(values) {
    id = values[0];
    numur = values[1]; focuschange = false;
    kodi = numur;
    if (mbush) myMenu.ShikoClick(editor, 'KonfigurimDokumentash.aspx?idsuperkat=' + Utils.getUrlVar('idsuperkat') + '&id=' + values[0] + '&numur=' + values[1] + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');

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
    changeName();
});

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKonfigAmbjentesh, "", "");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKonfigAmbjentesh, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKonfigAmbjentesh, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKonfigAmbjentesh, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKonfigAmbjentesh, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    if (e.item.name == "Shiko") {
        indexModifiko = gvKonfigAmbjentesh.GetFocusedRowIndex();
        e.processOnServer = false;
        gvKonfigAmbjentesh.GetRowValues(indexModifiko, 'IdKonfigAmbjente;KodKonfigAmbjente', OnGetRowValues);
        mbush = true;
    }
    if (e.item.name === 'Klono') {
        myMenu.ShikoClick(e, 'KonfigurimDokumentash.aspx?idsuperkat=' + Utils.getUrlVar('idsuperkat') + '&id=' + id + '&numur=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=klonim');
    }
    if (e.item.name === "Default") {
        e.processOnServer = false;
        //popFshi.Show();

        identifikuesPyetje = 'Default';
        myMesazh.ShtoPyetje(hfState.Get("msgPyetjeFshirjeKonfigurime"), true);
        //ButtonOk.SetVisible(false);

    }
    if (e.item.name === "Fshi") {

        e.processOnServer = false;
        identifikuesPyetje = 'Fshi';
        myMesazh.ShtoPyetje(hfState.Get("msgPyetjeFshirjeKonfigurime"), true);
        // ButtonOk2.SetVisible(false);
    }
    else
        myMenu.menu_click_regjistrime(s, e, "KonfigurimDokumentash.aspx?idsuperkat=" + Utils.getUrlVar('idsuperkat') + '&shtim_modifikim=shtim', 'KonfigurimDokumentash.aspx?idsuperkat=' + Utils.getUrlVar('idsuperkat') + '&id=' + id + '&numur=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');

}
var mbush = false;
function OnGridDoubleClick(e, index) {
//    if (!focuschange) {
//        myMenu.ShikoClick(editor, 'KonfigurimDokumentash.aspx?idsuperkat=' + Utils.getUrlVar('idsuperkat') + '&id=' + id + '&numur=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
//    } else {
//        indexModifiko = index;
//        mbush = true;
    //    }
    indexModifiko = index;
    gvKonfigAmbjentesh.GetRowValues(indexModifiko, 'IdKonfigAmbjente;KodKonfigAmbjente', OnGetRowValues);
    mbush = true;
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvKonfigAmbjentesh.GetFocusedRowIndex();

    gvKonfigAmbjentesh.GetRowValues(indexModifiko, 'IdKonfigAmbjente;KodKonfigAmbjente', OnGetRowValues);
    focuschange = true;
}
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    myFaqeCelje.changeNameRegjistrime('KonfigDokumentash.aspx?idsuperkat=' + Utils.getUrlVar('idsuperkat',0),0);
    myCookies.createCookie('adresa', window.location.href, 1);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    $('#dvMenu').show();//[0].style.visibility = 'visible';
}
function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}



function EndRequestHandler(sender, args) {
    if ($('#hfshfaq').val() === "shfaq") {
        $('#hfshfaq').val(''); 
        identifikuesPyetje = 'FshiMeVeprime';
        var pyetje = $('#pyetjeFshiKonfig').val();
        $('#pyetjeFshiKonfig').val('');
        myMesazh.ShtoPyetje(pyetje, true);
    }
    Utils.hiqLoadingGif();

}
    
function PoClick(s, e) {
    switch (identifikuesPyetje) {
        case "Default":
            Utils.shfaqLoadingGif();
            ButtonOk2.DoClick();
            break;
        case "Fshi":
            Utils.shfaqLoadingGif();
            ButtonOk.DoClick();
            break;
        case "FshiMeVeprime":
            Utils.shfaqLoadingGif();
            ASPxButton1.DoClick();
    }
}

function JoClick(s, e) {
    return;
}