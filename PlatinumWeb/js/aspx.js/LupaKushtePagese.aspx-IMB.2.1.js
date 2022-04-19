function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKushtPag, "627", '');
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
        gvLupaKushtPag.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKushtPag.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKushtPag.GetVisibleRowsOnPage() - 1) {
            gvLupaKushtPag.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKushtPag.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKushtPag.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaKushtPag.GetRowValues(gvLupaKushtPag.GetFocusedRowIndex(), 'IdKoka;KodiKushtPagese', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values != null) {
        var kodi;
        kodi = values[1];
        var id;
        id = values[0];

        if (window.parent.identikuesPerPopupKushtePagese == "KushtePagese") {
            window.parent.editorKushtPagese.value = kodi;
            window.parent.arr[4][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + kodi;
        }
        else if (window.parent.identikuesPerPopupKushtePagese == "Shto_KF") {
            window.parent.cmbKushtetPageses.SetText(kodi);
        }
        else if (window.parent.identikuesPerPopupKushtePagese == "Modifiko_KF") {
            window.parent.cmbKushtetPageses.SetText(kodi);
        }
        else if (window.parent.identikuesPerPopupKushtePagese == "RegjistrimDokumentash") {
            window.parent.btnKushtPagese.SetText(kodi);
            window.parent.btnKushtPagese.SetValue(id);
            window.parent.btnKushtPagese.Focus();
        }
        else if (window.parent.identikuesPerPopupKushtePagese == "KonfigurimDokumentash") {
            window.parent.editorKP.SetValue(id);
            window.parent.editorKP.SetFocus(true);
        }
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
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

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKushtPag&page=LupaKushtePagese.aspx&idKonfigAmbjente=577';
    popFiltra.Show();
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
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKushtPag.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKushtPag.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');