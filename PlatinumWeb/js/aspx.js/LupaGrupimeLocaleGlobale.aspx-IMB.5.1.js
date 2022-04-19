;function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvGrupime, "722", '');
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
        gvGrupime.SelectRowOnPage(0, true);
        gvGrupime.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvGrupime.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvGrupime.GetVisibleRowsOnPage() - 1) {
            gvGrupime.SetFocusedRowIndex(0);
        }
        else {
            gvGrupime.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvGrupime.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvGrupime.GetRowValues(index, 'Kodi;Pershkrimi;Id;IdPrindi;Prindi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var emri; emri = values[1];
        var kodi; kodi = values[0];
        if (Utils.getUrlVar('vjenNga') == 'PunonjesGlobale') {

            window.parent.cmbGlobal.SetSelectedIndex(window.parent.cmbGlobal.AddItem(values[0], values[2]));;
            try{  window.parent.cmbLocal.SetText('');}
            catch (ex) {

            }
            window.parent.cmbGlobal.SetFocus();
        }
        else if (Utils.getUrlVar('vjenNga') == 'PunonjesLocale') {
            window.parent.cmbLocal.SetSelectedIndex(window.parent.cmbLocal.AddItem(values[0], values[2]));;
            window.parent.cmbGlobal.SetSelectedIndex(window.parent.cmbGlobal.AddItem(values[4], values[3]));;

       
            window.parent.cmbLocal.SetFocus();
        } else  if (Utils.getUrlVar('vjenNga') == 'PunonjesGlobaleImp') {

            window.parent.editorGlobal.SetSelectedIndex(window.parent.editorGlobal.AddItem(values[0], values[2]));;
           
            window.parent.editorGlobal.SetFocus();
            window.parent.global = values[2];
        }
        else if (Utils.getUrlVar('vjenNga') == 'PunonjesLocaleImp') {
            window.parent.editorGlobal.SetSelectedIndex(window.parent.editorGlobal.AddItem(values[0], values[2]));;
            window.parent.editorGlobal.SetFocus();
        }
        else if (Utils.getUrlVar('vjenNga') == 'RaportiGlobal') {
            window.parent.btnGlobalBand.SetText(values[0]);
            window.parent.editorGlobalValueGrupimGlobal = values[2];
                   
            try { window.parent.btnLocalBand.SetText(''); }
            catch (ex) {

            }
        }
        else if (Utils.getUrlVar('vjenNga') == 'RaportiLocale') {
           
            window.parent.btnLocalBand.SetValue(values[2]);
            window.parent.btnLocalBand.SetText(values[0]);           
            window.parent.btnGlobalBand.SetText(values[4]);
            
            window.parent.btnLocalBand.SetFocus();
        }
        else if (window.parent.identikuesPerPopupBurimi == 'Raporti') {
            window.parent.editorGlobal.SetText(kodi);
            window.parent.editorGlobal.SetFocus(true);
        }

    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvGrupime.GetFocusedRowIndex());
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
        gvGrupime.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvGrupime.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');