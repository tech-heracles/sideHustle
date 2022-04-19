function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaNrAutomatik.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaNrAutomatik.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNrAutomatik.GetVisibleRowsOnPage() - 1) {
            gvLupaNrAutomatik.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNrAutomatik.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNrAutomatik.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    //gvLupaNrAutomatik.GetSelectedFieldValues('IdNrAutom;KodiNrAutom;EmertimiNrAutom', OnGridSelectionComplete);
    gvLupaNrAutomatik.GetRowValues(gvLupaNrAutomatik.GetFocusedRowIndex(), 'IdNrAutom;KodiNrAutom;EmertimiNrAutom', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    //        var s = new String();
    //        s += values[0];
    //        var vl = s.split(",");

    window.parent.editorNrAutomatik.SetValue(values[0]);
    window.parent.editorNrAutomatik.SetText(values[1]);
    window.parent.editorNrAutomatik.Focus();

    window.parent.popupUniversal.Hide();
}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNrAutomatik.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNrAutomatik.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');