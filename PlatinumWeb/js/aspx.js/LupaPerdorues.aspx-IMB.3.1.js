function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaPerdorues, "682", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload', function () {
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaPerdorues.SelectRowOnPage(0, true);
        gvLupaPerdorues.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaPerdorues.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaPerdorues.GetVisibleRowsOnPage() - 1) {
            gvLupaPerdorues.SetFocusedRowIndex(0);
        }
        else {
            gvLupaPerdorues.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaPerdorues.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaPerdorues.GetRowValues(index, 'IdPerdorues;EmriPerdorues;MbiemriPerdorues;PerdoruesUsername;PerdoruesEmail', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {

        if (Utils.getUrlVar('vjenNga') == 'Perdoruesi') {
            if (window.parent.editorKodi.GetText() != values[3]) {

                window.parent.editorKodi.SetText(values[3]);
                window.parent.editorEmail.SetText(values[4]);

            }
            window.parent.editorKodi.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Agjenti') {

            if (Utils.getUrlVar("arsye") == "zgjidhDrejtor") {
                window.parent.cmbDrejtori.SetValue(values[0]);
                window.parent.cmbDrejtori.SetFocus(true);
            } else {

                if (window.parent.btnPerdoruesi.GetText() != values[3]) {

                    window.parent.btnPerdoruesi.SetText(values[3]);
                    // window.parent.editorEmail.SetText(values[4]);

                }
                window.parent.btnPerdoruesi.SetFocus();
            }
        }
        if (Utils.getUrlVar('vjenNga') == 'KonfEmailImport') {
            window.parent.editorKodi.SetText(values[3]);
            window.parent.editorKodi.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Deleguesi') {
            if (window.parent.editorKodi.GetText() != values[3]) {
                window.parent.editorKodi.SetText(values[3]);
            }
            window.parent.editorKodi.SetFocus();
        }
        else if (window.parent.identikuesPerPopupPerdoruesi == "raporti") {
            window.parent.editorGlobal.SetText(values[3]);
            window.parent.editorGlobal.SetFocus(true);
        } else if (window.parent.indentifikuesPerPopupKonfigurimet == "Mag") {
            window.parent.editorGlobal.SetText(values[3]);
            window.parent.editorGlobal.SetFocus(true);
        }
    }
    window.parent.popupUniversal.Hide();
}
function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvLupaPerdorues.GetFocusedRowIndex());
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
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
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaPerdorues.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaPerdorues.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');