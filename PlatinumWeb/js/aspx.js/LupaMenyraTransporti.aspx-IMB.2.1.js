function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaMenTransp, "634", '');
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
        gvLupaMenTransp.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaMenTransp.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaMenTransp.GetVisibleRowsOnPage() - 1) {
            gvLupaMenTransp.SetFocusedRowIndex(0);
        }
        else {
            gvLupaMenTransp.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaMenTransp.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}
function OnGridSelectionChanged() {
    gvLupaMenTransp.GetRowValues(gvLupaMenTransp.GetFocusedRowIndex(), 'KodiMenyreTransporti;PershkrimiMenyreTransporti;IdMenyreTransporti', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    if (values.length != 0) {
        if (window.parent.identikuesPerPopupMenyraTransporti == "Modifiko_KF") {
            window.parent.cmbMenyraTransporti.SetText(values[0]);
        }
        else if (window.parent.identikuesPerPopupMenyraTransporti == "Shto_KF") {
            window.parent.cmbMenyraTransporti.SetText(values[0]);
        }
        else if (window.parent.identikuesPerPopupMenyraTransporti == "RegjistrimDokumentash") {
            window.parent.btnMenyreTransporti.SetText(values[0]);
            window.parent.btnMenyreTransporti.SetValue(values[2]);
            window.parent.btnMenyreTransporti.Focus();
        }
        else if (window.parent.identikuesPerPopupMenyraTransporti == "KonfigurimDokumentash") {
            window.parent.editorMT.SetValue(values[2]);
            window.parent.editorMT.SetFocus(true);
        }
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaMenTransp&page=LupaMenyraTransporti.aspx&idKonfigAmbjente=577';
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
        gvLupaMenTransp.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaMenTransp.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');