function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaAfatMaturimi, "606", '');
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
        gvLupaAfatMaturimi.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAfatMaturimi.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaAfatMaturimi.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaAfatMaturimi.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaAfatMaturimi.GetVisibleRowsOnPage() - 1) {
            gvLupaAfatMaturimi.SetFocusedRowIndex(0);
        }
        else {
            gvLupaAfatMaturimi.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaAfatMaturimi.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    //gvLupaAfatMaturimi.GetSelectedFieldValues('NrLlogariKF;EmertimiKF', OnGridSelectionComplete);
    gvLupaAfatMaturimi.GetRowValues(gvLupaAfatMaturimi.GetFocusedRowIndex(), 'KodMaturimi;PershkrimMaturimi;IdMaturimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {

    var emri; emri = values[1];
    var kodi; kodi = values[0];
    var id; id = values[2];

    if (window.parent.identikuesPerPopupAfateMaturimi == "RegjistrimDokumentash") {
        window.parent.btnMaturimi.SetValue(id);
        window.parent.btnMaturimi.SetText(kodi);
        window.parent.btnMaturimi.Focus();
    }
    else if (window.parent.identikuesPerPopupAfateMaturimi == "KonfigurimDokumentash") {
        window.parent.editorM.SetValue(id);
        window.parent.editorM.SetFocus(true);
    }
     else if (window.parent.identikuesPerPopupAfateMaturimi == "Import") {
         window.parent.editorGlobal.SetValue(id);
         window.parent.editorGlobal.SetFocus(true);
     }
     else if (window.parent.identikuesPerPopupAfateMaturimi == "raportklientfurnitor") {
         window.parent.editorGlobal.SetText(kodi);
         window.parent.editorGlobal.SetFocus(true);
    }
    else {
        window.parent.btneMaturimi.SetText(kodi);
        window.parent.btneMaturimi.SetFocus(true);
    }
    window.parent.popupUniversal.Hide();
}


function menu_click(s, e) {
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
    //    if (e.item.name == 'Filtra')
    //        popZgjidhFiltrin.Show();
    //    if (e.item.name == 'Ruaj')
    //        popRuaj.Show();
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {

    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(750, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaAfatMaturimi&page=LupaAfateMaturimi.aspx&idKonfigAmbjente=577');
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