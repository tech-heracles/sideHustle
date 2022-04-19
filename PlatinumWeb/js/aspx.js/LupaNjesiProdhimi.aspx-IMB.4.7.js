function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaNjesiProdhimi, "600", '');
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
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNjesiProdhimi.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNjesiProdhimi.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).on('unload', function () {
});

function Init() {
    try {
        if (window.parent.window.location.href.search('LupaNjesiProdhimi.aspx') != -1)
        { ASPxMenu1.GetItemByName('Shto').SetVisible(false); }
        myFaqeCelje.shtoHandlerSession();
        gvLupaNjesiProdhimi.SetFocusedRowIndex(0);
        gvLupaNjesiProdhimi.SelectRowOnPage(0, true);
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaNjesiProdhimi.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNjesiProdhimi.GetVisibleRowsOnPage() - 1) {
            gvLupaNjesiProdhimi.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNjesiProdhimi.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNjesiProdhimi.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaNjesiProdhimi.GetRowValues(gvLupaNjesiProdhimi.GetFocusedRowIndex(), 'IdNjesiProdhimi;Kodi;Pershkrimi;DtRegjistrimi;IdDegeAdministrative;Adresa;Aktiv;Shenime;DtModifikimi;IdPerdoruesi;IdKrijuesi;IdNdermarje;IdStatusdok;KodDegeAdministrative;PershkrimDegeAdministrative', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    switch (window.parent.identifikuesPerPopupNjesiProdhimi) {
        case "Ekzekutim":
        case "Planifikim":
            window.parent.cmbNjesiProdhimi.SetValue(values[0]);
            window.parent.cmbNjesiProdhimi.SetText(values[1]);
            break;
        case "Import":
            window.parent.editorGlobal.SetText(values[1]);
            break;
        case "raportNjesiProdhimi":
            window.parent.editorGlobal.SetValue(values[0]);
            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
            break;
        case "KonfigurimDokumentash":
            window.parent.editorNjProdhimi.SetValue(values[0]);
            window.parent.editorNjProdhimi.SetText(values[1]);
            window.parent.editorNjProdhimi.SetFocus(true);
            break;
    }
    if (window.parent.identifikuesPerPopupNjesiProdhimi == 'ShtoNjesiProdhimi') {
        Utils.SelectComboItem(window.parent.cmbDegeAdministrative, values[0][0], values[0][1], values[0][2]);
        window.parent.cmbDegeAdministrative.SetFocus(true);
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