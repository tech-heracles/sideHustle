function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaPasqFin.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaPasqFin.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaPasqFin.GetVisibleRowsOnPage() - 1) {
            gvLupaPasqFin.SetFocusedRowIndex(0);
        }
        else {
            gvLupaPasqFin.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaPasqFin.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    //gvLupaPasqFin.GetSelectedFieldValues('PershkrimiZerit', OnGridSelectionComplete);
    gvLupaPasqFin.GetRowValues(gvLupaPasqFin.GetFocusedRowIndex(), 'PershkrimiZerit', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var txtpasqyrefinanciare;
    txtpasqyrefinanciare = '';

    //        for (var i = 0; i < values.length; i++) {
    //            if (txtpasqyrefinanciare == '')
    //                txtpasqyrefinanciare = values[values.length - 1];
    //            else
    //                txtpasqyrefinanciare = txtpasqyrefinanciare + ',' + values[i];
    //        }

    txtpasqyrefinanciare = values;
    window.parent.editorGlobal.SetText(txtpasqyrefinanciare);
    window.parent.editorGlobal.Focus();

    window.parent.popupUniversal.Hide();
}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaPasqFin.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaPasqFin.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
