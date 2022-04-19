function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaAutorizim, "611", '');
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
        // gvLupaAutorizim.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaAutorizim.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaAutorizim.GetVisibleRowsOnPage() - 1) {
            gvLupaAutorizim.SetFocusedRowIndex(0);
        }
        else {
            gvLupaAutorizim.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaAutorizim.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaAutorizim.GetSelectedFieldValues('KodiAutorizim', OnGridSelectionComplete);
    // gvLupaAutorizim.GetRowValues(gvLupaAutorizim.GetFocusedRowIndex(), 'KodiAutorizim', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var autorizim;
    autorizim = '';

    for (var i = 0; i < values.length; i++) {
        if (autorizim == '')
            autorizim = values[i];
        else
            autorizim = autorizim + ',' + values[i];
    }
    if (window.parent.identifikuesPerPopupAutorizimi == "Import") {
        SetEditorText(window.parent.editorGlobal, autorizim);
    }
    else if (window.parent.identikuesPerPopupKushtePagese == "KushtePagese") {
        if (window.parent.grida == true) {
            SetEditorText(window.parent.txtAutorizimi, autorizim);
        }
        else {
            SetEditorText(window.parent.txtAutorizimi, autorizim);
        }
    }
    else if (window.parent.identikuesPerPopupLlogari == "AgjenteShitje") {
        SetEditorText(window.parent.cmbAutorizimi, autorizim);
    }
    else if (window.parent.identifikuesPerPopupAutorizimet == "GrupimDokumentash") {
        SetEditorText(window.parent.cmbAutorizimi, autorizim);
    }
    else if (window.parent.identifikuesPerPopupAutorizime == 'KonfigurimDokumentash')
        SetEditorText(window.parent.editorAutorizime, autorizim);
    else if (window.parent.identifikuesPerPopupAutorizime == 'KonfigurimDokumentashLupaAutorizim') {
        window.parent.identifikuesPerPopupAutorizime = 'KonfigurimDokumentash';
        SetEditorText(window.parent.cmbAutorizimi, autorizim);
    }
    else {
        if (window.parent.KPF == 1) {
            SetEditorText(window.parent.editorAutorizime1, autorizim);
        }
        else if (window.parent.KPF == 2) {

            SetEditorText(window.parent.editorAutorizime2, autorizim);
        }
        else if (window.parent.KPF == 3) {
            SetEditorText(window.parent.editorAutorizime3, autorizim);
        }
        else if (window.parent.KPF == 0) {
            SetEditorText(window.parent.cmbAutorizimi, autorizim);
        }
        else {
            SetEditorText(window.parent.editorAutorizime, autorizim);
        }
    }


    window.parent.popupUniversal.Hide();
}

function SetEditorText(editor, text) {
    editor.SetText(text);

    if (editor.Focus)
        editor.Focus();
    else if (editor.SetFocus)
        editor.SetFocus(true);
}

function menu_click(s, e) {
    hfState.Set('vjenNga', 'vetLupa');
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaAutorizim&page=LupaAutorizim.aspx&idKonfigAmbjente=577';
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
        gvLupaAutorizim.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAutorizim.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
