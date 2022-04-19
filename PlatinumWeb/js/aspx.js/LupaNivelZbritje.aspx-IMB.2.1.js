function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvNivelZbritje, "638", '');
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
        gvNivelZbritje.SetFocusedRowIndex(0); gvNivelZbritje.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvNivelZbritje.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvNivelZbritje.GetVisibleRowsOnPage() - 1) {
            gvNivelZbritje.SetFocusedRowIndex(0);
        }
        else {
            gvNivelZbritje.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvNivelZbritje.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvNivelZbritje.GetSelectedFieldValues('IdNivelZbritje;KodNivelZbritje;PershkrimNivelZbritje;', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    if (window.parent.identifikuesPerPopupNiveli == "Zbritje Analitike") {
        window.parent.btneNiveli.SetText(vl[2]);
    }
    else if (window.parent.identikuesPerPopupPerdoruesi == "raporti") {
        window.parent.btnNivelZbritje.SetText(vl[1]);
        window.parent.btnNivelZbritje.SetFocus(true);
    }
    if (window.parent.identifikuesPerPopupNiveli == "Import") {
        window.parent.btneNiveli.SetText(vl[1]);
    }
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvNivelZbritje&page=LupaNivelZbritje.aspx&idKonfigAmbjente=577';
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
        gvNivelZbritje.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvNivelZbritje.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
