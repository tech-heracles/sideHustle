function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaRole, "683", '');
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
        gvLupaRole.SelectRowOnPage(0, true);
        gvLupaRole.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaRole.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaRole.GetVisibleRowsOnPage() - 1) {
            gvLupaRole.SetFocusedRowIndex(0);
        }
        else {
            gvLupaRole.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaRole.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaRole.GetRowValues(index, 'IdRoli;KodRoli;PershkrimRoli', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {


    if (values[0] != undefined) {
        if (Utils.getUrlVar('vjenNga') == 'Perdoruesi') {
            if (window.parent.editorKodi.GetText() != values[1])
                window.parent.editorKodi.SetText(values[1]);
            window.parent.editorKodi.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Importi') {
            if (window.parent.editorGlobal.GetText() != values[1])
                window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus();
        }

    }

    window.parent.popupUniversal.Hide();
}







function menu_click(s, e) {
   
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvLupaRole.GetFocusedRowIndex());
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
        gvLupaRole.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaRole.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');