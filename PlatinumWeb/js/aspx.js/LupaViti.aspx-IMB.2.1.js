; $(window).on('unload',function () {
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaViti.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaViti.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaViti.GetVisibleRowsOnPage() - 1) {
            gvLupaViti.SetFocusedRowIndex(0);
        }
        else {
            gvLupaViti.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaViti.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    //gvLupaViti.GetSelectedFieldValues('Kodi;Monedha', OnGridSelectionComplete);
    gvLupaViti.GetRowValues(gvLupaViti.GetFocusedRowIndex(), 'KodiViti', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var kodi = ''; kodi = values;

    if (window.parent.grida == true) {
        window.parent.editorViti.SetText(kodi);
        window.parent.editorViti.SetFocus(true);
    }
    else {
        window.parent.txtViti.SetText(kodi);
        window.parent.txtViti.SetFocus(true);
    }
    window.parent.popupUniversal.Hide();
}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaViti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaViti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');