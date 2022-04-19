function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvPlanifikime, "810", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload', function () {
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvPlanifikime.SelectRowOnPage(0, true);
        gvPlanifikime.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvPlanifikime.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvPlanifikime.GetVisibleRowsOnPage() - 1) {
            gvPlanifikime.SetFocusedRowIndex(0);
        }
        else {
            gvPlanifikime.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvPlanifikime.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvPlanifikime.GetRowValues(index, 'NrDok;DtDok;IdKokaPlanifikim', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var emri = values[1];
        var kodi = values[0];
        var id = values[2];
        if (Utils.getUrlVar('vjenNga') == 'SkedulimProdhimi') {
            window.parent.btnProjekti.SetText(kodi);
            window.parent.$('#hfPrioriteti').val(id);
            window.parent.btnProjekti.SetFocus(true);
        }
        else if (Utils.getUrlVar('vjenNga') == 'SkedulimProdhimiGrida') {
            var grida = window.parent.$('#rowed5');
            var idRow = grida.getLastSel2();
            var index = idRow;
            var idKontrolli = "#txtProjekti" + index;
            window.parent.$(idKontrolli).val(kodi);
            window.parent.$("#txtIdProjekti" + index).val(id);
            window.parent.merrArtikullPlanifikimi(id, index);
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
        OnGridSelectionChanged(gvPlanifikime.GetFocusedRowIndex());
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvPlanifikime&page=LupaBurime.aspx&idKonfigAmbjente=577');
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
        gvPlanifikime.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvPlanifikime.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');