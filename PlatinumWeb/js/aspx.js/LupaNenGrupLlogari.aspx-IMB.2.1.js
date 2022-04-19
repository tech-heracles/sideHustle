function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaNGrupLlog, "635", '');
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
        gvLupaNGrupLlog.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaNGrupLlog.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNGrupLlog.GetVisibleRowsOnPage() - 1) {
            gvLupaNGrupLlog.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNGrupLlog.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNGrupLlog.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function OnGridSelectionChanged() {
    gvLupaNGrupLlog.GetRowValues(gvLupaNGrupLlog.GetFocusedRowIndex(), 'IdNenGrupiLlogaria;PershkrimiNenGrupiLlogaria', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    //        var nengrupi;
    //        nengrupi = '';
    //        
    //            for (var i = 0; i < values.length; i++) {
    //                if (nengrupi == '')
    //                    nengrupi = values[i];
    //                else
    //                    nengrupi = nengrupi + ',' + values[i];
    //            }

    if (window.parent.identikuesPerPopupNenGrupeLlogari == "Shto_Llogari") {
        if (window.parent.grida == true) {
            window.parent.editorNenGrupi.SetValue(values[0]);
            window.parent.editorNenGrupi.SetText(values[1]);
            window.parent.editorNenGrupi.Focus();
            window.parent.editorValues["Nengrupi"] = values[0];
            window.parent.editorValues1["Nengrupi"] = values[1];
        }
        else {
            window.parent.cmbNengrupi.SetText(values[1]);
            window.parent.cmbNengrupi.SetFocus(true);
        }
    }
    else if (window.parent.identikuesPerPopupGrupeLlogari == "LlogariShpejte") {
        window.parent.cmbNengrupi.SetValue(values[0]);
        window.parent.cmbNengrupi.SetText(values[1]);
        window.parent.cmbNengrupi.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupNenGrupeLlogari == "Import") {
        window.parent.editorGlobal.SetText(values[1]);
        window.parent.editorGlobal.SetFocus(true);
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaNGrupLlog&page=LupaNengrupLlogari.aspx&idKonfigAmbjente=577';
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
        $("#div").show();// $("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNGrupLlog.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNGrupLlog.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');