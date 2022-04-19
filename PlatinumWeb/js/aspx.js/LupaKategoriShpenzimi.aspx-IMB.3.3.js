function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKategoria, "690", '');
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
        if (window.parent.identikuesPerPopupKategoriShpenzimi != 'RaportiMultiSelect') {
            gvKategoria.SelectRowOnPage(0, true);
            gvKategoria.SetFocusedRowIndex(0);
        }
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvKategoria.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvKategoria.GetVisibleRowsOnPage() - 1) {
            gvKategoria.SetFocusedRowIndex(0);
        }
        else {
            gvKategoria.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvKategoria.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (window.parent.identikuesPerPopupKategoriShpenzimi == 'RaportiMultiSelect')
        gvKategoria.GetSelectedFieldValues('Id;Kodi;', OnGridMultiSelectionComplete);
    else if (index != -1)
        gvKategoria.GetRowValues(index, 'Id;Kodi;Pershkrimi;NivelKategorie', OnGridSelectionComplete);
    
}

function OnGridSelectionComplete(values) {
    if (window.parent.pageState != undefined && window.parent.pageState.identifikuesPopUp == 'RegjistrimDokumentash') {
        //var index = window.parent.lastsel2;
        var grida = window.parent.$('#rowed5');
        var idRow = grida.getLastSel2();
        var index = idRow;
        var idKontrolli = "#txtKategoriShpenzimi" + index;
        var idKontrollPershkrimni = '#txtPershkrimiKatShpenzimi' + index;
        //var kodi;
        //kodi = vl[0];
        //var idArt = values[0][4];
        //window.parent.selectFunc(null, null, idKontrolli, idArt, kodi);
       
        window.parent.$(idKontrolli).val(values[1]);
        window.parent.$(idKontrollPershkrimni).val(values[2]);
      //  window.parent.$(idKontrolli).change();
        window.parent.$(idKontrolli).focus();
        //window.parent.editorGlobal.SetText(values[1]);
        //window.parent.editorGlobal.SetFocus(true);
    }
    else
    if (window.parent.identifikuesperKategoriShpenzimi == "Import") {
        window.parent.editorGlobal.SetText(values[1]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesperKategoriShpenzimi == "KategoriShpenzimi") {
        //window.parent.editorPrind.SetText(values[2]);
        Utils.SelectComboItem(window.parent.editorPrind, values[0], [values[1], values[2], values[3].toString()]);
        window.parent.editorNiveli.SetText(parseInt(values[3]) + 1);
        window.parent.editorPrind.SetValue(values[0]);
        window.parent.editorPrind.SetText(values[1]);
        window.parent.editorPrind.SetFocus(true);
    }

   
    else if (window.parent.identikuesPerPopupKategoriShpenzimi == 'Raporti') {
        window.parent.editorGlobal.SetText(values[1]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesperKategoriShpenzimi == "KonfigPASH") {
        window.parent.editorGlobal.val(values[1]);
        window.parent.editorPershkrimillogaria.val(values[2]);
        window.parent.editorGlobal.focus();
    }

    else if (values[0] != undefined) {
        window.parent.cmbKategori.SetText(values[1]);
        window.parent.cmbKategori.SetFocus();

    }
    window.parent.popupUniversal.Hide();
}

function OnGridMultiSelectionComplete(values) {
    var vlerat = Utils.ktheVleratESelektuaraTeBashkuara(values, 1, ",");
    window.parent.editorGlobal.SetText(vlerat);
    window.parent.editorGlobal.SetFocus(true);
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvKategoria.GetFocusedRowIndex());
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
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
        gvKategoria.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvKategoria.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');