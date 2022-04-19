function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaObjektivaKosto, "688", '');
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
        gvLupaObjektivaKosto.SelectRowOnPage(0, true);
        gvLupaObjektivaKosto.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaObjektivaKosto.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaObjektivaKosto.GetVisibleRowsOnPage() - 1) {
            gvLupaObjektivaKosto.SetFocusedRowIndex(0);
        }
        else {
            gvLupaObjektivaKosto.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaObjektivaKosto.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaObjektivaKosto.GetRowValues(index, 'Id;Kodi;Pershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {

        if (Utils.getUrlVar('vjenNga') == 'Shto_Punonjes') {
            window.parent.cmbObjektiva.SetText(values[1]);
            window.parent.cmbObjektiva.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'RegjistrimQendraKostoGrida') {
            window.parent.editorKodi.SetText(values[1]);
            window.parent.editorEmertimi.SetText(values[2]);
            window.parent.editorKodi.SetFocus();
        }
        else if (window.parent.identifikuesPerPopupNiveliCmimi == "Import") {
            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
        }
        else if (Utils.getUrlVar('vjenNga') === "RegjistrimQendraKostoDXDATAGRID" || window.parent.identikuesPerPopupLlogari == "RegjistrimQendraKostoDXDATAGRID") {
            window.parent.regjQK.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat(0, 0, values[0]);
        }

    }
    window.parent.popupUniversal.Hide();
}
function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvLupaObjektivaKosto.GetFocusedRowIndex());
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
        gvLupaObjektivaKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaObjektivaKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');