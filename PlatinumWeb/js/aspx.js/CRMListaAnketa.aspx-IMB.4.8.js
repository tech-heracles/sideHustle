; $(document).ready(function (e) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idViti = hfState.Get("idViti");
    myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idViti);

});


function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
var focuschange = false;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvCRMListaAnketa, "2007", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
    try {

        myFaqeCelje.shtoHandlerSession();
        $('#hfKontrollet').val(window.parent.$('#hfKontrollet').val());
        // gvCRMListaAnketa.SetFocusedRowIndex(0);

    }
    catch (err) {
    }
}
var indexModifiko;
document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvCRMListaAnketa.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvCRMListaAnketa.GetVisibleRowsOnPage() - 1) {
            gvCRMListaAnketa.SetFocusedRowIndex(0);
        }
        else {
            gvCRMListaAnketa.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvCRMListaAnketa.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {

}

/*
Function: menu_click
perdoret per veprimet e menuse ne javascript
Parameters: e-eventi
*/

function menu_click(s, e) {
    if (e.item.name == "Shiko") {
        indexModifiko = gvCRMListaAnketa.GetFocusedRowIndex();
        e.processOnServer = false;
        gvCRMListaAnketa.GetRowValues(indexModifiko, 'IdKokaAnketa', OnGetRowValues);
        mbush = true;
    }
    else if(e.item.name==="Klono")
    {
        myFaqeCelje.kontrolloTeDrejta('CRMAnketa.aspx?shtim_modifikim=klonim&id=' + gvCRMListaAnketa.GetRowKey(gvCRMListaAnketa.GetFocusedRowIndex()));
        e.processOnServer = false;
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('CRMAnketa.aspx?shtim_modifikim=shtim');
        e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
        popFshi.Show(); e.processOnServer = false;
    }
}

var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        var hidField1 = $("#hfVeprimi")[0];
        myMenu.ShikoClick(editor, 'CRMAnketa.aspx?shtim_modifikim=modifikim&id=' + gvCRMListaAnketa.GetRowKey(gvCRMListaAnketa.GetFocusedRowIndex())) ;
        //+ '&numer=' + numur + '&indexrow=' + indexModifiko
    }
    else {
        indexModifiko = index;
        mbush = true;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {

}

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}

var editor;
var focuschange = false;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvCRMListaAnketa.GetFocusedRowIndex();
    gvCRMListaAnketa.GetRowValues(indexModifiko, 'IdKokaAnketa', OnGetRowValues);
    focuschange = true;
}
function OnGetRowValues(values) {  
    id = gvCRMListaAnketa.GetRowKey(gvCRMListaAnketa.GetFocusedRowIndex());
    focuschange = false;
    if (mbush) myFaqeCelje.kontrolloTeDrejta('CRMAnketa.aspx?shtim_modifikim=modifikim&id=' + id);
}

function clickExport(e) {
    if (gvCRMListaAnketa.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function Row_DblClick(s, e) {
    OnGridDoubleClick(e,e.visibleIndex); 
}