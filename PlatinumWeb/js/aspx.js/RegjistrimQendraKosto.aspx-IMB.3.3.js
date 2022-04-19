; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;
var idgjenerues;
var indexModifiko;

$(document).ready(function () {
    $("#progressBar").hide();
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

function OnGetRowValues(values) {
    var hidField1 = $("#hfVeprimi")[0];
    id = gvRegjQK.GetRowKey(indexModifiko);

    numur = values[1];
    idgjenerues = values[2];
    focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_RegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&id=' + gvRegjQK.GetRowKey(gvRegjQK.GetFocusedRowIndex()) + '&numer=' + values[1] + '&indexrow=' + indexModifiko);
}
function clickExport(e) {
    if (gvRegjQK.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvRegjQK, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvRegjQK, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvRegjQK, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvRegjQK, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvRegjQK, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hidField1 = $("#hfVeprimi")[0];
    myMenu.menu_click_regjistrime(s, e, "Shto_RegjistrimQendraKosto.aspx?shtim_modifikim=shtim", 'Shto_RegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&id=' + gvRegjQK.GetRowKey(gvRegjQK.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko);

    if (e.item.name === 'Klono') {
        if (id === null)
            myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
        else if (idgjenerues != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMunTeKlononi1QKTeGjeneruar"));
            e.processOnServer = false;
        }
        else
            myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimQendraKosto.aspx?shtim_modifikim=klonim&id=' + gvRegjQK.GetRowKey(gvRegjQK.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko);
        e.processOnServer = false;
    }
    else if (e.item.name === "Rivleresim") {
        if (gvRegjQK.GetSelectedRowCount() == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        }
        else {
            $("#progressBar").show();
            ProgressBar1.startTask();
        } e.processOnServer = false;
    }
}

var mbush = false;

function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        var hidField1 = $("#hfVeprimi")[0];
        myMenu.ShikoClick(editor, 'Shto_RegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&id=' + gvRegjQK.GetRowKey(gvRegjQK.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko);
    } 
    else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
var focuschange = false;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvRegjQK.GetFocusedRowIndex();
    //         if (indexModifiko == -1)
    //            myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje fature shitje/blerje!');
    //        else
    gvRegjQK.GetRowValues(indexModifiko, 'IdKoka;NrDok;IdGjenerues', OnGetRowValues);
    focuschange = true;

}
function changeName() {

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RegjistrimQendraKosto.aspx', 0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler(); $('#dvMenu').show();//[0].style.visibility = 'visible';
}

function OnError(message, context) {
    if (message == "Session TimeOut")
        ndryshoUrlFrame(Paths.defaultLoginPath);
}

function selection(index) {
    hf = $("#hfReshtaTeSelektuar")[0];
    hf.value = gvRegjQK.GetSelectedKeysOnPage();
}
function OnGridSelectionCompleteMultiSelect(values) {


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

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("907", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvRegjQK.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}
function onTaskRunning() {
    window.parent.SessionTimeout.sendKeepAlive();
}
function onTaskDone(s,e) {
    if (ProgressBar1.getValue() == 100) {
        myMesazh.ShtoMesazhSuksesi("Rillogaritja perfundoi me sukses");
        gvRegjQK.UnselectRows();

    }
    else {
        var extraData = ProgressBar1.getExtraData();
        if (extraData)
            myMesazh.ShtoMesazhInformues("Rillogaritja ndaloi tek" + ' ' + ProgressBar1.getValue() + ' %' + extraData);
        else
            myMesazh.ShtoMesazhInformues("Rillogaritja ndaloi tek " + ' ' + ProgressBar1.getValue() + ' %');
    }
    //ASPxMenu1.GetItemByName('Stop').SetVisible(false);
    setTimeout(function () { $("#progressBar").hide(); }, 2000);
}

function onTaskError() {
    myMesazh.ShtoMesazhGabimi("Ndodhi nje gabim gjate rillogaritjes!");
}