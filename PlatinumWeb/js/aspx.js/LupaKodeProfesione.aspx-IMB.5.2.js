;function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvProfesione, "724", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload', function () {
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvProfesione.SelectRowOnPage(0, true);
        gvProfesione.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvProfesione.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvProfesione.GetVisibleRowsOnPage() - 1) {
            gvProfesione.SetFocusedRowIndex(0);
        }
        else {
            gvProfesione.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvProfesione.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvProfesione.GetRowValues(index, 'Kodi;Pershkrimi;Id', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var emri; emri = values[1];
        var kodi; kodi = values[0];
        var kosto; kosto = values[2];
        if (Utils.getUrlVar('vjenNga') == 'PunonjesProfesione') {

            window.parent.cmbKodeProfesione.SetText(kodi);

            window.parent.cmbKodeProfesione.SetFocus();
        }
       
        else if (window.parent.identikuesPerPopupBurimi == 'Raporti') {
            window.parent.editorGlobal.SetText(kodi);
            window.parent.editorGlobal.SetFocus(true);
        }

    }
    window.parent.popupUniversal.Hide();
}
function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvProfesione.GetFocusedRowIndex());
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
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
        gvProfesione.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvProfesione.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');