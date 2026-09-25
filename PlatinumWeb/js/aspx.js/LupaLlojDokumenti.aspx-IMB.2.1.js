function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaLlojDok, "630", '');
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
        gvLupaLlojDok.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaLlojDok.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaLlojDok.GetVisibleRowsOnPage() - 1) {
            gvLupaLlojDok.SetFocusedRowIndex(0);
        }
        else {
            gvLupaLlojDok.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaLlojDok.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaLlojDok.GetRowValues(gvLupaLlojDok.GetFocusedRowIndex(), 'LlojDokKodi;LlojDokPershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {

    if (window.parent.identikuesPerPopupLlojDokumenti == "RaporteKontabiliteti") {
        window.parent.editorGlobal.SetText(values[0]);
    }
    else if (window.parent.identikuesPerPopupLlojDokumenti == "LidhjaDokumentave") {
        window.parent.llojDokumenti_ButtonEdit.SetText(values[0]);
        window.parent.llojDokumenti_ButtonEdit.Focus();
    }
    else if (window.parent.identikuesPerPopupLlojDokumenti == "raportllojdokumenti") {
        window.parent.editorGlobal.SetText(values[0]);
        window.parent.editorGlobal.Focus();
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaLlojDok&page=LupaLlojDokumenti.aspx&idKonfigAmbjente=577';
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
        gvLupaLlojDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaLlojDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
 