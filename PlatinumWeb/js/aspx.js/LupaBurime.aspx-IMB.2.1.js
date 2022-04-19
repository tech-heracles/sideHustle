function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaBurimet, "675", '');
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
        gvLupaBurimet.SelectRowOnPage(0, true);
        gvLupaBurimet.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaBurimet.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaBurimet.GetVisibleRowsOnPage() - 1) {
            gvLupaBurimet.SetFocusedRowIndex(0);
        }
        else {
            gvLupaBurimet.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaBurimet.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaBurimet.GetRowValues(index, 'Kodi;Emertimi;KostoPlan;IdBurimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var emri; emri = values[1];
        var kodi; kodi = values[0];
        var kosto; kosto = values[2];
        if (Utils.getUrlVar('vjenNga') == 'Shto_Aktivitete') {
            if (window.parent.editorKodi.GetText() != kodi) {
                if (kodi != "")
                    for (var i = 0; i < window.parent.gvTrupi.cpNoRows; i++) {
                        if (kodi == window.parent.window['Kodi' + i].GetText()) {
                            window.parent.myMesazh.ShtoMesazhGabimi('Ky burim eshte perdorur njehere ne kete aktivitet!')
                            kodi = '';
                            emri = '';
                            kosto = '0.00';
                            break;
                        }
                    }

                window.parent.editorKodi.SetText(kodi);
                window.parent.editorPershkrimi.SetText(emri);
                window.parent.editorKostoBurimi.SetText(kosto);
                window.parent.vendosKosto(window.parent.editorKosto, window.parent.editorKoha, window.parent.editorKostoBurimi);
            }
            window.parent.editorKodi.SetFocus();
        }
        else if (Utils.getUrlVar('vjenNga') == 'Shto_Ekzekutim') {
            var grida =window.parent. $("#rowed6");
            var idRow = grida.getLastSel2();
            var index = idRow;
            var idKontrolli = "#txtKodiArtikullR" + index;
          

            window.parent.selectFunca(null, null, idKontrolli, values[3], kodi);
            window.parent.$(idKontrolli).focus();
           
           // window.parent.editorNjesia.SetText("Ore");
        }
       else if (Utils.getUrlVar('vjenNga') == 'SkedulimProdhimi') {
            window.parent.btnBurimi.SetText(kodi);
            window.parent.btnBurimi.SetFocus(true);
           // window.parent.editorNjesia.SetText("Ore");
       }
       else if (Utils.getUrlVar('vjenNga') == 'SkedulimProdhimiGrida') {
           var grida = window.parent.$('#rowed5');
           var idRow = grida.getLastSel2();
           var index = idRow;
           var idKontrolli = "#txtBurimi" + index;


           window.parent.selectFunc(null, null, idKontrolli, values[3], kodi);
           window.parent.$(idKontrolli).focus();
           
           // window.parent.editorNjesia.SetText("Ore");
       }
       else if (window.parent.identikuesPerPopupBurimi == 'Raporti') {
           window.parent.editorGlobal.SetText(kodi);
           window.parent.editorGlobal.SetFocus(true);
       }
        //                if (window.parent.identifikuesPerPopupMagazina === "RegjistrimMagazine") {
        //                    if (window.parent.identifikuesMagazina === 'Mag1') {
        //                        window.parent.btneMagazina.SetText(kodi);
        //                        window.parent.btneMagazina.SetFocus();
        //                        window.parent.TextChangedMagazina();
        //                    }
        //                    else {
        //                        window.parent.btneMagazina2.SetText(kodi);
        //                        window.parent.btneMagazina2.SetFocus();
        //                        window.parent.TextChangedMagazina2();
        //                    }
        //                }
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
        OnGridSelectionChanged(gvLupaBurimet.GetFocusedRowIndex());
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaBurimet&page=LupaBurime.aspx&idKonfigAmbjente=577');
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
        gvLupaBurimet.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaBurimet.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');