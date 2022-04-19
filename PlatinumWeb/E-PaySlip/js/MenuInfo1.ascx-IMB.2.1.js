function poBtnClick(s, e) {
    myMesazh.Po(s, e);
}

function poBtnInit(s, e) {
    if ((typeof (mesazhList) !== "undefined") && mesazhList.GetText().split('?').length == 1)
        btnPo.SetVisible(false);
}

function joBtnClick(s, e) {
    myMesazh.Jo(s, e);
}

function joBtnInit(s, e) {
    if ((typeof (mesazhList) !== "undefined") && mesazhList.GetText().split('?').length == 1)
        btnJo.SetVisible(false);
}

function hlCloseClick(s, e) {
    mesazhList.SetSelectedIndex(-1);
    btnPo.SetVisible(false); btnJo.SetVisible(false);
    hlClose.SetVisible(false);
}

function mesazhListSelectedIndexChanged(s, e) {
    mesazhList.SetSelectedIndex(-1);
}