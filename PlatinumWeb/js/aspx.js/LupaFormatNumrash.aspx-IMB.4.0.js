function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvFormati, "912", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).load(function () {
    try {        
        gvFormati.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        gvFormati.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).on('unload',function () {
});

function Init() {
    try {
        if (window.parent.window.location.href.search('LupaFormatNumrash.aspx') != -1)
        { ASPxMenu1.GetItemByName('Shto').SetVisible(false); }
        myFaqeCelje.shtoHandlerSession();
        gvFormati.SetFocusedRowIndex(0);
        gvFormati.SelectRowOnPage(0, true);
    }
    catch (err) {
    }
}
document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvFormati.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvFormati.GetVisibleRowsOnPage() - 1) {
            gvFormati.SetFocusedRowIndex(0);
        }
        else {
            gvFormati.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvFormati.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvFormati.GetRowValues(gvFormati.GetFocusedRowIndex(), 'IdFormatKonfig;Kodi;Emertimi;IdKategoria;IdNdermarrja;DtModifikimi;Kategoria;IdStatusDok;IdKrijuesi;DtKrijimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values.length == 0) {
        alert(hfState.Get("msgSelektoniNjeRresht"));      
        return;
    }
    if (window.parent.identifikuesPerPopupFormatNr == 'KonfigurimDokumentash') {
        window.parent.cmbFormatNumri.SetValue(values[0]);
        window.parent.cmbFormatNumri.SetText(values[1]);        
        window.parent.cmbFormatNumri.SetFocus(true);
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}