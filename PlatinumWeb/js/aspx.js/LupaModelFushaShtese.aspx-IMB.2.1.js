function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaModele, "3055", '');
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
        gvLupaModele.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaModele.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaModele.GetVisibleRowsOnPage() - 1) {
            gvLupaModele.SetFocusedRowIndex(0);
        }
        else {
            gvLupaModele.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaModele.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaModele.GetSelectedFieldValues('KodiModeliFushaShtese', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {

    if (Utils.getUrlVar('vjenNgaRaporti') == 'true') {
        var kodi = "";
        if (values.length > 0) {
            for (i = 0; i < values.length; i++)
                kodi += values[i] + ",";

            kodi = kodi.slice(0, -1);
        }
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.SetFocus(true);
        window.parent.popupUniversal.Hide();
    }
}


var queryStr;
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
        $("#div").show();
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaModele.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaModele.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
