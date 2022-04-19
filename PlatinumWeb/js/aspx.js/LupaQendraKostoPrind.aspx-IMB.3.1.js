function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaQendraKosto, "686", '');
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
        gvLupaQendraKosto.SelectRowOnPage(0, true);
        gvLupaQendraKosto.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaQendraKosto.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaQendraKosto.GetVisibleRowsOnPage() - 1) {
            gvLupaQendraKosto.SetFocusedRowIndex(0);
        }
        else {
            gvLupaQendraKosto.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaQendraKosto.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaQendraKosto.GetRowValues(index, 'Id;Kodi;Monedha', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {

        if (Utils.getUrlVar('vjenNga') == 'PunonjesQK1') {

            window.parent.cmbQK1.SetSelectedIndex(window.parent.cmbQK1.AddItem(values[1], values[0]));
            window.parent.cmbQK2.SetText('');
            window.parent.cmbQK1.SetFocus();
        } else if (Utils.getUrlVar('vjenNga') == 'PunonjesQK1Import') {

            window.parent.editorGlobal.SetSelectedIndex(window.parent.editorGlobal.AddItem(values[1], values[0]));
            window.parent.editorGlobal.SetFocus();
            window.parent.grupi = values[0];
        }
        else if (Utils.getUrlVar('vjenNga') == 'Raporti') {
            window.parent.btnQenderKosto1.SetText(values[1]);
            window.parent.btnQenderKosto2.SetText('');
            window.parent.btnQenderKosto1.SetFocus();
        }
        else {
            if (window.parent.cmbPrindi.GetText() != values[1]) {

                window.parent.cmbPrindi.SetText(values[1]);
                window.parent.cmbMonedha.SetText(values[2]);
                window.parent.cmbMonedha.SetEnabled(false);
            }
            window.parent.cmbPrindi.SetFocus();
        }
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvLupaQendraKosto.GetFocusedRowIndex());
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
        gvLupaQendraKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaQendraKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');