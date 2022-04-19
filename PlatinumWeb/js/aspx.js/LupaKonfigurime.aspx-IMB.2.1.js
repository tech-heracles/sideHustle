function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKonfig, "623", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaKonfig.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaKonfig.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKonfig.GetVisibleRowsOnPage() - 1) {
            gvLupaKonfig.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKonfig.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKonfig.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function OnGridSelectionChanged() {
    gvLupaKonfig.GetSelectedFieldValues('IdKonfigAmbjente;KodKonfigAmbjente', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var s = new String();
    var sK = new String();
    //s += values[0];
    s = s + values[0][1];
    //sK += values[1];
    sK = sK + values[0][1];
    var vl = s.split(",");
    var vlSK = sK.split(",");
    var kodi = vl[0];
    if (values.length > 1) {
        for (i = 1; i < values.length; i++)
            kodi = kodi + "," + values[i][1];


        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.SetFocus(true);
    }
    else {



        window.parent.editorGlobal.SetText(vlSK[0]);
        window.parent.editorGlobal.SetFocus(true);
    }
    //window.parent.getElementById('hfSkemaKontabelRegjistrime').Value = vl[0];



    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKonfig&page=LupaKonfigurime.aspx&idKonfigAmbjente=577';
    popFiltra.Show();
}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonfig.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonfig.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
