function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaQendraKostoPlote, "696", '');
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
   //     gvLupaQendraKostoPlote.SelectRowOnPage(0, true);
        gvLupaQendraKostoPlote.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaQendraKostoPlote.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaQendraKostoPlote.GetVisibleRowsOnPage() - 1) {
            gvLupaQendraKostoPlote.SetFocusedRowIndex(0);
        }
        else {
            gvLupaQendraKostoPlote.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaQendraKostoPlote.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (gvLupaQendraKostoPlote.GetFocusedRowIndex() == 0)
        gvLupaQendraKostoPlote.SelectRowOnPage(0)
    if (gvLupaQendraKostoPlote.GetFocusedRowIndex() == -1)
        return;
    gvLupaQendraKostoPlote.GetSelectedFieldValues('Id;Kodi;Monedha;Pershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var s = new String();
        s = s + values[0];
        var vl = s.split(",");
        var kodi = vl[1];
       
        if (Utils.getUrlVar('vjenNga') == 'raporti') {
            for (i = 1; i < values.length; i++)
                kodi = kodi + "," + values[i][1];
            window.parent.btneQenderKosto1.SetText(kodi);
            window.parent.btneQenderKosto1.SetFocus();
        }
       
    }

    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvLupaQendraKostoPlote.GetFocusedRowIndex());
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
        gvLupaQendraKostoPlote.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaQendraKostoPlote.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');