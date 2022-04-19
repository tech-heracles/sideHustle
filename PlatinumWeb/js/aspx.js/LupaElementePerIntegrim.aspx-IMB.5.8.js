function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaElementePerIntegrim, "607", '');
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
        gvLupaElementePerIntegrim.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaElementePerIntegrim.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaElementePerIntegrim.GetVisibleRowsOnPage() - 1) {
            gvLupaElementePerIntegrim.SetFocusedRowIndex(0);
        }
        else {
            gvLupaElementePerIntegrim.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaElementePerIntegrim.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaElementePerIntegrim.GetSelectedFieldValues('IdElementi;Kodi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {

    if (values.length == 0) {
        window.parent.popupUniversal.Hide();
        return;
    }
    var id = values[0][0];
    var kodi = values[0][1];

    window.parent.btneKodiPerIntegrim.SetValue(id);
    window.parent.btneKodiPerIntegrim.SetText(kodi);
    window.parent.btneKodiPerIntegrim.SetFocus(true);
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaElementePerIntegrim&page=LupaElementePerIntegrim.aspx&idKonfigAmbjente=577';
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
        gvLupaElementePerIntegrim.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaElementePerIntegrim.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');