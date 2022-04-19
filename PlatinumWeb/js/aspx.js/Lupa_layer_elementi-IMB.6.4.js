function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaDegaAdministrative, "714", '');
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
        //gvLupaDegaAdministrative.SetFocusedRowIndex(0);
        //gvLupaDegaAdministrative.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaDegaAdministrative.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaDegaAdministrative.GetVisibleRowsOnPage() - 1) {
            gvLupaDegaAdministrative.SetFocusedRowIndex(0);
        }
        else {
            gvLupaDegaAdministrative.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaDegaAdministrative.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaDegaAdministrative.GetSelectedFieldValues('IdDegaAdministrative;Kodi;Pershkrimi;Adresa;Aktiv;DataRegjitstrimit;IdNdermarje;IdPerdoruesi;IdKonfig;IdStatusDok;DtKrijimi;DtModifikimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var degaAdmin = vl[1];

    if (window.parent.identikuesPerPopupDegaAdministrative == 'raportDegaAdministrative') {
        for (i = 1; i < values.length; i++) //degaAdmin += "," + values[i][1];
            degaAdmin = degaAdmin + "," + values[i][1];
        window.parent.editorGlobal.SetText(degaAdmin);
        window.parent.editorGlobal.SetFocus(true);
    }
    if (window.parent.identikuesPerPopupDegaAdministrative == 'ShtoNjesiProdhimi') {
        Utils.SelectComboItem(window.parent.cmbDegeAdministrative, values[0][0], values[0][1], values[0][2]);        
        window.parent.cmbDegeAdministrative.SetFocus(true);
    }
    if (window.parent.identikuesPerPopupDegaAdministrative == 'KonfigurimDokumentash') {
        //for (i = 1; i < values.length; i++)
        //    degaAdmin = degaAdmin + "," + values[i][1];
        window.parent.editorPkShF.SetText(degaAdmin);
        window.parent.editorPkShF.SetFocus(true);
    }

    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    else if (e.item.name == "Anullo") {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaDegaAdministrative&page=LupaDegaAdministrative.aspx&idKonfigAmbjente=577';
    popFiltra.Show();
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
        gvLupaDegaAdministrative.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaDegaAdministrative.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');