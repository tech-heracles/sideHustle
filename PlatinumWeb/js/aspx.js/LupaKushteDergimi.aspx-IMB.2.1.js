function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKushtDerg, "626", '');
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

        gvLupaKushtDerg.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKushtDerg.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKushtDerg.GetVisibleRowsOnPage() - 1) {
            gvLupaKushtDerg.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKushtDerg.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKushtDerg.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}
function OnGridSelectionChanged() {
    gvLupaKushtDerg.GetRowValues(gvLupaKushtDerg.GetFocusedRowIndex(), 'KodiKushtDergimi;PershkrimiKushtDergimi;IdKushtDergimi', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    if (values.length != 0) {
        if (window.parent.identikuesPerPopupKushteDergimi == "Modifiko_KF") {
            window.parent.cmbKushteDergimi.SetText(values[0]);
        }
        else if (window.parent.identikuesPerPopupKushteDergimi == "Shto_KF") {
            window.parent.cmbKushteDergimi.SetText(values[0]);
        }
        else if (window.parent.identikuesPerPopupKushteDergimi == "RegjistrimDokumentash") {
            window.parent.btnKushtDergimi.SetText(values[0]);
            window.parent.btnKushtDergimi.SetValue(values[2]);
            window.parent.btnKushtDergimi.Focus();
        }
        else if (window.parent.identikuesPerPopupKushteDergimi == "KonfigurimDokumentash") {
            window.parent.editorKD.SetValue(values[2]);
            window.parent.editorKD.SetFocus(true);
        }
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKushtDerg&page=LupaKushteDergimi.aspx&idKonfigAmbjente=577';
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
        gvLupaKushtDerg.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKushtDerg.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
