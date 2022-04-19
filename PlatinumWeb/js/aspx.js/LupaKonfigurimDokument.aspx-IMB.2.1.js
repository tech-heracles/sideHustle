function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();

        gvLupaKonfigDok.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKonfigDok.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKonfigDok.GetVisibleRowsOnPage() - 1) {
            gvLupaKonfigDok.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKonfigDok.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKonfigDok.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    //gvLupaKonfigDok.GetRowValues(gvLupaKonfigDok.GetFocusedRowIndex(), 'IdKonfigAmbjente;KodKonfigAmbjente', OnGridSelectionComplete);
    gvLupaKonfigDok.GetSelectedFieldValues('IdKonfigAmbjente;KodKonfigAmbjente', OnGridSelectionComplete);
}

var hide = true;

function OnGridSelectionComplete(value) {
    hide = true;
    var kodi = '';
    var id = '';
    if (value.length > 1) {
        for (i = 0; i < value.length - 1; i++) {
            var values = value[i];
            //kodi += values[1] + "-";
            kodi = kodi + values[1] + "-";
            //id += values[0] + "-";
            id = id + values[0] + "-";
        }
        values = value[value.length - 1];
        //kodi += values[1];
        kodi = kodi + values[1];
        //id += values[0];
        id = id + values[0];
    }
    else {
        values = value[0];
        kodi = values[1];
        id = values[0];
    }
    window.parent.editorLupa.value = kodi;
    window.parent.editorLupa.focus();

    window.parent.popupUniversal.Hide();
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonfigDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonfigDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
