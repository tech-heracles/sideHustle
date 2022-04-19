function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaGrLlog, "618", '');
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
        gvLupaGrLlog.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaGrLlog.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaGrLlog.GetVisibleRowsOnPage() - 1) {
            gvLupaGrLlog.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrLlog.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaGrLlog.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaGrLlog.GetRowValues(gvLupaGrLlog.GetFocusedRowIndex(), 'IdGrupiLlogaria;PershkrimiGrupiLlogaria', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (window.parent.identikuesPerPopupGrupeLlogari == "Shto_Llogari") {
        if (window.parent.grida == true) {
            window.parent.editorGrupi.SetValue(values[0]);
            window.parent.editorGrupi.SetText(values[1]);
            window.parent.editorGrupi.Focus();
            window.parent.editorValues["Grupi"] = values[0];
            window.parent.editorValues1["Grupi"] = values[1];
        }
        else {
            window.parent.cmbGrupi.SetText(values[1]);
            window.parent.cmbGrupi.SetFocus(true);
            window.parent.editorValues["Grupi"] = values[0];
            window.parent.editorValues1["Grupi"] = values[1];
        }
    }
    else if (window.parent.identikuesPerPopupGrupeLlogari == "LlogariShpejte") {
        window.parent.cmbGrupi.SetValue(values[0]);
        window.parent.cmbGrupi.SetText(values[1]);
        window.parent.cmbGrupi.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupGrupeLlogari == "Import") {
        window.parent.editorGlobal.SetText(values[1]);
        window.parent.editorGlobal.SetFocus(true);
        window.parent.grupi = values[1];
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

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText(hfState.Get("headerPopUZgjidhFiltrin"));
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaKlientFurnitor&page=LupaKlientFurnitor.aspx&idKonfigAmbjente=577');
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
        gvLupaGrLlog.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaGrLlog.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');