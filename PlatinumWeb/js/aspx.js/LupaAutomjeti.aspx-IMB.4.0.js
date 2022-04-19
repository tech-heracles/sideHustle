function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaAutomjet, "609", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAutomjet.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAutomjet.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).on('unload', function () {
});

function Init() {
    try {
        if (window.parent.window.location.href.search('LupaAutomjeti.aspx') != -1)
        { ASPxMenu1.GetItemByName('Shto').SetVisible(false); }
        myFaqeCelje.shtoHandlerSession();
        gvLupaAutomjet.SetFocusedRowIndex(0);
        gvLupaAutomjet.SelectRowOnPage(0, true);
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaAutomjet.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaAutomjet.GetVisibleRowsOnPage() - 1) {
            gvLupaAutomjet.SetFocusedRowIndex(0);
        }
        else {
            gvLupaAutomjet.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaAutomjet.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaAutomjet.GetSelectedFieldValues('IdAutomjeti;NrShasie;Targa;ModelAutomjeti;PershkrimModeli;VitProdhimi;Kilometra;KodMotorri;IdStatusDok;IdKlienti;Klienti;IdNdermarrje;IdKrijues;IdPerdorues;DtKrijimi;DtModifikimi;KodKlientFurnitor;KodModeli', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values.length == 0 || !values[0]) {
        alert("Selektoni nje rresht");
        return;
    }
    if (window.parent.identifikuesPerPopupAutomjet == 'Shto_RegjistrimDokumentash') {
        window.parent.btneAutomjeti.SetValue(values[0][0]);
        window.parent.btneAutomjeti.SetText(values[0][1]);
        window.parent.txtTarga.SetText(values[0][2]);
        if (window.parent.btnKlienti.GetText() == '') {
            Utils.SelectComboItem(window.parent.btnKlienti, values[0][9], values[0][16], values[0][10]);
            window.parent.callWebserviceKF(values[0][9], false);
        }
        window.parent.btneAutomjeti.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupAutomjet == 'Shto_RegjistrimMagazine') {
        window.parent.btneAutomjeti.SetValue(values[0][0]);
        window.parent.btneAutomjeti.SetText(values[0][1]);
        window.parent.txtTarga.SetText(values[0][2]);
        if (window.parent.btneKlientFurnitori.GetText() == '')
        Utils.SelectComboItem(window.parent.btneKlientFurnitori, values[0][9], values[0][16], values[0][10]);
        window.parent.btneAutomjeti.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupAutomjet == 'ShtoVeprimBanka') {
        window.parent.btneAutomjet.SetValue(values[0][0]);
        window.parent.btneAutomjet.SetText(values[0][1]);
        window.parent.txtTarga.SetText(values[0][2]);
        window.parent.btneAutomjet.SetFocus(true);
    }

    else if (window.parent.identifikuesPerPopupAutomjet == 'raportAuto') {
        var auto = values[0][1];
        for (i = 1; i < values.length; i++)
            auto = auto + "," + values[i][1];
        window.parent.editorGlobal.SetText(auto);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupAutomjet == 'Import') {
        window.parent.editorGlobal.SetText(values[0][1]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopup == "PerfitimBuxheti") {
        window.parent.btnDoktori.SetSelectedIndex(window.parent.btnDoktori.AddItem(values[0][1], values[0][0]));
        window.parent.btnDoktori.SetFocus(true);
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
        case "Shto":
        e.processOnServer = false;
        var klienti = Utils.getUrlVar('klienti');
        switch (window.parent.identifikuesPerPopupAutomjet) {
            case "Shto_RegjistrimDokumentash":
                if (typeof (klienti) != "undefined" && klienti != "undefined" && typeof (window.parent.btnKlienti) != "undefined")
                    klienti = window.parent.btnKlienti.GetValue();
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "ruajNeSessionURLLupaShpejte"),
                    data: JSON.stringify({ url: window.location.href, lupa: 'LupaAutomjetShpejte' })
                }).done(Succeded);
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("btnShtoAutomjet"), 'LupaAutomjetShpejte.aspx?klienti=' + klienti, 900, 600);
                return;
            default:
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "ruajNeSessionURLLupaShpejte"),
                    data: JSON.stringify({ url: window.location.href, lupa: 'LupaAutomjetShpejte' })
                }).done(Succeded);
                return;
        }
        break;
    }
}

function Succeded() {
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("btnShtoAutomjet"), 'LupaAutomjetShpejte.aspx', 900, 600);
}