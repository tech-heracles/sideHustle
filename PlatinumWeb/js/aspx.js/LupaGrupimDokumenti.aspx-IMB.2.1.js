function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaGrupDokumenti, "2013", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
    myMesazh.shtoHandler();
    try {
        myFaqeCelje.shtoHandlerSession();

        gvLupaGrupDokumenti.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}
var faqe = 'kodifikime';
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaGrupDokumenti.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaGrupDokumenti.GetVisibleRowsOnPage() - 1) {
            gvLupaGrupDokumenti.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrupDokumenti.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaGrupDokumenti.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function zgjidhElement() {
        OnGridSelectionChanged();
}

function OnGridSelectionChanged() {
    gvLupaGrupDokumenti.GetSelectedFieldValues('IdGrupimKoka;Kodi;Pershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var s = new String();
    s = s + values[1];
    var vl = s.split(",");
    var kodi = '';
    for (var i = 0; i < values.length; i++) {
        if (kodi == '')
            kodi = values[i][1];
        else
            kodi = kodi + ',' + values[i][1];
    }
    window.parent.editorGlobal.SetText(kodi);
    window.parent.editorGlobal.SetFocus(true);
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
    if (e.item.name == "OK") {
        e.processOnServer = false;
        zgjidhElement();
    }
    else if (e.item.name = "Anullo") {
        window.parent.popupUniversal.Hide();
    }
}

$(window).load(function () {
    try {
        $("#div").show();
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaGrupDokumenti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaGrupDokumenti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');


//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaGrupDokumenti&page=LupaGrupimDokumenti.aspx');
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
    