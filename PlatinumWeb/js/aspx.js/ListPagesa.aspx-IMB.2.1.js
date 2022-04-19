; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;

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
    changeName();
});
$(window).on("load", function () {

          pageState.regjisDokNukKeniAsnjeDokTeZgjedhur = hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"); hfState.Remove("regjisDokNukKeniAsnjeDokTeZgjedhur");
});
   
var pageState = {
            regjisDokNukKeniAsnjeDokTeZgjedhur : null,
            regjisDokZgjidhDokPerTeBashkengjitur : null
}

function ButtonClickArkiva(id) {//id e rreshtit te selektuar
    if (id === null) {
        myMesazh.ShtoMesazhGabimi(pageState.regjisDokNukKeniAsnjeDokTeZgjedhur);
        return;
    }
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);

    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=listpagesa' + '&idDok=' + gvListPagesa.GetRowKey(gvListPagesa.GetFocusedRowIndex()));
    popupUniversal.Show();
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('ListPagesa.aspx',0, hf);

    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}
function OnGetRowValues(values) {

    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_ListPagesa.aspx?shtim_modifikim=modifikim&numer=' + values[1] + '&indexrow=' + indexModifiko + '&id=' + gvListPagesa.GetRowKey(gvListPagesa.GetFocusedRowIndex()));
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvListPagesa, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvListPagesa, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvListPagesa, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvListPagesa, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvListPagesa, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {

    myMenu.menu_click_regjistrime(s, e, "Shto_ListPagesa.aspx?shtim_modifikim=shtim&id=0", 'Shto_ListPagesa.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvListPagesa.GetRowKey(gvListPagesa.GetFocusedRowIndex()));
    switch (e.item.name) {
        case 'Klono':
            if (id === null)
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
            else
                myFaqeCelje.kontrolloTeDrejta('Shto_ListPagesa.aspx?shtim_modifikim=klonim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvListPagesa.GetRowKey(gvListPagesa.GetFocusedRowIndex()));
            e.processOnServer = false;
            break;
        case 'Fshi':
            if (gvListPagesa.GetSelectedRowCount() == 0)
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjMagMesazhZgjidhniNje"));
            else
                myMesazh.ShtoPyetje(hfState.Get("msgJuKeniZgjedhur") + gvListPagesa.GetSelectedRowCount() + hfState.Get("msgRreshta") + hfState.Get("labelAdministrimiMsgJeniSigurt"));
            e.processOnServer = false;
            break;
        case "Arkiva":
            e.processOnServer = false;
            ButtonClickArkiva(id);
            break;
        default:
            break;
    }
}
var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {

        myMenu.ShikoClick(editor, 'Shto_ListPagesa.aspx?shtim_modifikim=modifikim&numer=' + numur + '&indexrow=' + indexModifiko + '&id=' + gvListPagesa.GetRowKey(gvListPagesa.GetFocusedRowIndex()));
    } else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvListPagesa.GetFocusedRowIndex();

    gvListPagesa.GetRowValues(indexModifiko, 'IdKoka;NrDok', OnGetRowValues);
    focuschange = true;
}
function BeginCallback(s, e) {

    if (e.command === 'APPLYFILTER' && btnFiltrat !== undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("709", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    gvListPagesa.PerformCallback(idKomp + ";" + kodKonf);
}
function EndRequestHandler(sender, args) {
}

function PoClick() {
    Utils.shfaqLoadingGif();
    FshiDokument();
}

function JoClick() {
    return;
}

function FshiDokument() {
    gvListPagesa.GetSelectedFieldValues('IdKoka', function (result) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("ListPagesa", "FshiDokument"),
            data: JSON.stringify({ ids: result, guidString: hfState.Get("guidString") })
        }).done(SuccededCallbackDelete)
    });
}

function SuccededCallbackDelete(result) {
    Utils.hiqLoadingGif();
    result.deletedKeys.forEach(function (key, index) {
        gvListPagesa.DeleteRowByKey(key);
    });
    if (result.mesazhSukses != "")
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
}