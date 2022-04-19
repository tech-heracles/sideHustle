function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKodbari, "654", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload',function () {
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaKodbari.SetFocusedRowIndex(0);
        gvLupaKodbari.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
    }
}
document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKodbari.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKodbari.GetVisibleRowsOnPage() - 1) {
            gvLupaKodbari.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKodbari.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKodbari.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaKodbari.GetSelectedFieldValues('IdArtikulli;IdKodbari;KodArtikulli;Kodbari;Pershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodbari = vl[3];

    if (window.parent.identikuesPerPopupKodbari == 'raportKodbari') {
        for (i = 1; i < values.length; i++) //kodbari += "," + values[i][3];
            kodbari = kodbari + "," + values[i][3];
        window.parent.editorGlobal.SetText(kodbari);
        window.parent.editorGlobal.SetFocus(true);
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKodbari&page=LupaKodbari.aspx&idKonfigAmbjente=577';
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
        gvLupaKodbari.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKodbari.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
