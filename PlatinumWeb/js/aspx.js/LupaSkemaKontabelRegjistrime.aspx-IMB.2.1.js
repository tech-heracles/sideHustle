function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaSkemaKontRegj.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaSkemaKontRegj.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaSkemaKontRegj.GetVisibleRowsOnPage() - 1) {
            gvLupaSkemaKontRegj.SetFocusedRowIndex(0);
        }
        else {
            gvLupaSkemaKontRegj.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaSkemaKontRegj.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function OnGridSelectionChanged() {
    gvLupaSkemaKontRegj.GetRowValues(gvLupaSkemaKontRegj.GetFocusedRowIndex(), 'IdSkemeKont;KodSkemeKont', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var s = new String();
    var sK = new String();
    //s += values[0];
    s = s + values[0];
    //sK += values[1];
    sK = sK + values[1];
    var vl = s.split(",");
    var vlSK = sK.split(",");

    //   window.parent.btnSkemaKontabelRegjistrime.SetText(vlSK[0]);
    //window.parent.getElementById('hfSkemaKontabelRegjistrime').Value = vl[0];
    // document.parentWindow.parent.document.getElementById('ASPxCallbackPanel1$hfSkemaKontabelRegjistrime').value = vl[0];
    window.parent.$("input[id$='hfSkemaKontabelRegjistrime']").val(vl[0]);
    window.parent.editorGlobal.SetText(vlSK[0]);
    window.parent.editorGlobal.SetFocus(true);

    window.parent.popupUniversal.Hide();


}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaSkemaKontRegj.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaSkemaKontRegj.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');