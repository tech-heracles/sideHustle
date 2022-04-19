 function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKatZbr, "620", '');
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

        gvLupaKatZbr.SetFocusedRowIndex(0); gvLupaKatZbr.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaKatZbr.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKatZbr.GetVisibleRowsOnPage() - 1) {
            gvLupaKatZbr.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKatZbr.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKatZbr.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}
function OnGridSelectionChanged() {
    gvLupaKatZbr.GetSelectedFieldValues('IdKokaKategoriZbritje;KodKategoriZbritje;Zbritja', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    if (window.parent.identifikuesPerPopupKodifikimin == "Shto_KlientFurnitor") {
        window.parent.btneKategoriZbritje.SetText(vl[1]);
        window.parent.txtKategoriPerqindja.SetText(vl[2]);
        window.parent.btneKategoriZbritje.SetFocus(true);

    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "Modifiko_KF") {
        window.parent.btneKategoriZbritje.SetText(vl[1]);

        window.parent.btneKategoriZbritje.SetFocus(true);

    } else if (window.parent.identifikuesPerPopupKodifikimin == "Import") {
        window.parent.editorGlobal.SetText(vl[1]);

        window.parent.editorGlobal.SetFocus(true);

    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "ShtoKartaKlienti") {

        window.parent.cmbKategori.SetText(vl[1]);
        window.parent.cmbKategori.SetFocus(true);
    }
    //else if (window.parent.identifikuesPerPopupKodifikimin == "ShtoKartaKlientiLupaKategoriZB") {
    //    parent.UZgjodhKategoriaNGaLupa = true;
    //    parent.IdKategoriZbritjeZGjedhurNgaLupa = vl[0];
    //    parent.gvLimiti.BatchEditEndEditing.FireEvent(parent.gvLimiti);
    //}
    
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
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaKatZbr&page=LupaKategoriZbritje.aspx&idKonfigAmbjente=577');
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
        gvLupaKatZbr.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKatZbr.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');