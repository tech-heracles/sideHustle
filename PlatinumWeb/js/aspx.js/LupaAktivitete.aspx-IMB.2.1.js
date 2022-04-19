function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaAktivitetet, "676", '');
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
        gvLupaAktivitetet.SelectRowOnPage(0, true);
        gvLupaAktivitetet.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaAktivitetet.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaAktivitetet.GetVisibleRowsOnPage() - 1) {
            gvLupaAktivitetet.SetFocusedRowIndex(0);
        }
        else {
            gvLupaAktivitetet.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaAktivitetet.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
if(index!=-1)
    gvLupaAktivitetet.GetRowValues(index, 'Kodi;Emertimi;KohaPlan;NjesiKohe;IdKoka', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var emri; emri = values[1];
        var kodi; kodi = values[0];
        var koha; koha = values[2];
        if (Utils.getUrlVar('vjenNga') == 'Shto_Artikull.aspx') {
            var grida = window.parent.$('#rowed5');
            var idRresht = grida.getLastSel2();
            var index = idRresht;
            var idKontrolli = "#txtKodi" + index;
            var kodi;
            kodi = values[0];

            window.parent.selectFunc(null, null, idKontrolli, values[4], kodi);
            window.parent.$(idKontrolli).focus();
//            if (window.parent.editorKodi.GetText() != kodi) {

//                window.parent.editorKodi.SetText(kodi);
//                window.parent.editorEmertimi.SetText(emri);
//                window.parent.editorNjes.SetText(values[3] == 1 ? 'sec' : values[3] == 2 ? 'min' : values[3] == 3 ? 'ore' : 'dite');
//                window.parent.kontrollo(window.parent.keyGlobal);
//                window.parent.arr[1][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + kodi;
//                window.parent.arr[2][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + emri;
//                window.parent.arr[5][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + kodi;
//            }
//            window.parent.editorKodi.SetFocus();
        } else if (Utils.getUrlVar('vjenNga') == 'SkedulimProdhimi') {
            var grida = window.parent.$('#rowed5');
            var idRresht = grida.getLastSel2();
            var index = idRresht;
            var idKontrolli = "#txtAktiviteti" + index;
            var kodi;
            kodi = values[0];

            window.parent.selectFunc2(null, null, idKontrolli, values[4], kodi);
            window.parent.$(idKontrolli).focus();

        }
        else if (window.parent.identikuesPerPopupAktiviteti == 'Raporti') {
            window.parent.editorGlobal.SetText(kodi);
            window.parent.editorGlobal.SetFocus(true);
        }

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
        OnGridSelectionChanged(gvLupaAktivitetet.GetFocusedRowIndex());
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaAktivitetet&page=LupaAktivitete.aspx&idKonfigAmbjente=577');
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
        gvLupaAktivitetet.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAktivitetet.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');