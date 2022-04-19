function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaSkemaKosto, "689", '');
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
        gvLupaSkemaKosto.SelectRowOnPage(0, true);
        gvLupaSkemaKosto.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaSkemaKosto.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaSkemaKosto.GetVisibleRowsOnPage() - 1) {
            gvLupaSkemaKosto.SetFocusedRowIndex(0);
        }
        else {
            gvLupaSkemaKosto.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaSkemaKosto.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaSkemaKosto.GetRowValues(index, 'IdKoka;Kodi;Pershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {

        if (Utils.getUrlVar('vjenNga') == 'Shto_Llogari') {

            window.parent.qendraKostos_TextBox.SetText(values[1]);
            window.parent.qendraKostos_TextBox.SetFocus();
        }

        if (Utils.getUrlVar('vjenNga') == 'LupaLlogariShpejte') {

            window.parent.qendraKostos_TextBox.SetText(values[1]);
            window.parent.qendraKostos_TextBox.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Struktura') {

            window.parent.Qendra.SetText(values[1]);
            window.parent.Qendra.SetValue(values[0]);            
            window.parent.$('#hfQendra').val(values[1]);
            window.parent.Qendra.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'RegjistrimQendraKosto') {

            window.parent.cmbQendraKosto.SetText(values[1]);
            window.parent.cmbQendraKosto.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'RegjistrimQendraKostoGrida') {

            window.parent.editorKodi.SetText(values[1]);
            window.parent.editorEmertimi.SetText(values[2]);

            window.parent.editorKodi.SetFocus();
        } else if (window.parent.identifikuesPerPopupNiveliCmimi == "Import") {

            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
        }
    }
    window.parent.popupUniversal.Hide();
}
function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvLupaSkemaKosto.GetFocusedRowIndex());
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
        gvLupaSkemaKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaSkemaKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');