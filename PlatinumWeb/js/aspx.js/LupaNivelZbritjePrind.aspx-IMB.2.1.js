function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaNivZbPrind, "639", '');
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
        gvLupaNivZbPrind.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaNivZbPrind.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNivZbPrind.GetVisibleRowsOnPage() - 1) {
            gvLupaNivZbPrind.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNivZbPrind.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNivZbPrind.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}
function OnGridSelectionChanged() {
    gvLupaNivZbPrind.GetSelectedFieldValues('IdNivelZbritje;KodNivelZbritje;PershkrimNivelZbritje', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    if (window.parent.identifikuesPerPopupNiveli == "Shto Nivel") {
        window.parent.editorPrind.SetText(vl[2]);

        var listefushash = window.parent.document.getElementById("hfPrioriteteMax").value.split(',');
        for (i = 0; i < listefushash.length; i++) {
            var liste = listefushash[i].split(':')
            if (liste[0] == vl[1])
                window.parent.editorPrioriteti.SetValue(liste[1]);
        }

        window.parent.editorPrind.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupNiveli == "Modifiko Nivel") {

        if (s == 'undefined') {

            window.parent.btneEmertimPrindi.SetText();
            window.parent.cmbPrioriteti.SetSelectedIndex(0);
            window.parent.btneEmertimPrindi.SetFocus(true);

        }
        else {
            window.parent.btneEmertimPrindi.SetText(vl[2]);

            var listefushash = window.parent.document.getElementById("hfPrioriteteMax").value.split(',');
            for (i = 0; i < listefushash.length; i++) {
                var liste = listefushash[i].split(':')
                if (liste[0] == vl[1])
                    window.parent.cmbPrioriteti.SetValue(liste[1]);
            }

            window.parent.btneEmertimPrindi.SetFocus(true);
        }
    }
    else if (window.parent.identifikuesPerPopupNiveliZbritje == "Shto_KF") {
        window.parent.btneNivelZbritje.SetText(vl[2]);
        window.parent.btneNivelZbritje.SetFocus(true);
    } else if (window.parent.identifikuesPerPopupNiveliZbritje == "Import") {

        window.parent.editorGlobal.SetText(vl[1]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupNiveliZbritje == "Modifiko_KF") {
        window.parent.btneNivelZbritje.SetText(vl[2]);
        window.parent.btneNivelZbritje.SetFocus(true);
    }

    else if (window.parent.identifikuesPerPopupKodifikimin == "ShtoKartaKlientiLupaKategoriZB") {
        window.parent.VendosKategoriZbritje(vl[0], vl[1]);
    }
    else if (window.parent.identifikuesPerPopup == "Shto_KlientFurnitor") {
        window.parent.btneNivelZbritje.SetText(vl[2]);
        window.parent.btneNivelZbritje.SetFocus(true);
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaNivZbPrind&page=LupaNivelZbritjePrind.aspx&idKonfigAmbjente=577';
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
        gvLupaNivZbPrind.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNivZbPrind.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');