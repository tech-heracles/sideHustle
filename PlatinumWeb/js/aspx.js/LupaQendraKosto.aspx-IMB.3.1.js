function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaQendraKosto, "687", '');
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
        var vjenNga = Utils.getUrlVar('vjenNga');
        if (vjenNga !== "raporti" && vjenNga !== "PunonjesQK2" && vjenNga !== "RegjistrimQendraKostoGrida") {
            gvLupaQendraKosto.SelectRowOnPage(0, true);
            gvLupaQendraKosto.SetFocusedRowIndex(0);
        }
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaQendraKosto.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaQendraKosto.GetVisibleRowsOnPage() - 1) {
            gvLupaQendraKosto.SetFocusedRowIndex(0);
        }
        else {
            gvLupaQendraKosto.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaQendraKosto.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged(index) {   
    if (Utils.getUrlVar('vjenNga') !== "raporti" && Utils.getUrlVar('vjenNga') !== "PunonjesQK2") {
        var focusedRowIndex = gvLupaQendraKosto.GetFocusedRowIndex();
        if (gvLupaQendraKosto.GetSelectedRowCount() == 0 && focusedRowIndex != -1)
            gvLupaQendraKosto.SelectRowOnPage(focusedRowIndex)
        if (focusedRowIndex == -1)
            return;
    }
    if (Utils.getUrlVar('vjenNga') == 'raporti')
        gvLupaQendraKosto.GetSelectedFieldValues('Id;Kodi;Monedha;Pershkrimi;IdPrindi;Prindi', OnGridSelectionComplete);
    else
        gvLupaQendraKosto.GetRowValues(index, 'Id;Kodi;Monedha;Pershkrimi;IdPrindi;Prindi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
 
    if (values.length == 0) {
        alert('Nuk keni selektuar asnje rresht!');
        return;
    }
    if (values[0] != undefined) {
        var s = new String();
        s = s + values[0];
        var vl = s.split(",");
        var kodi = vl[1];
        if (Utils.getUrlVar('vjenNga') == 'Shto_Skema') {
            if (window.parent.editorKodi.GetText() != values[1]) {
                if (values[1] != "")
                    for (var i = 0; i < window.parent.gvTrupi.cpNoRows; i++) {
                        if (values[1] == window.parent.window['Kodi' + i].GetText()) {
                            window.parent.myMesazh.ShtoMesazhGabimi('Kjo qender eshte perdorur njehere ne kete skeme!')
                            values[1] = '';
                            values[3] = '';
                            break;
                        }
                    }

                window.parent.editorKodi.SetText(values[1]);
                window.parent.editorPershkrimi.SetText(values[3]);
            }
            window.parent.editorKodi.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Shto_Llogari') {

            window.parent.qendraKostos_TextBox.SetText(values[1]);
            window.parent.qendraKostos_TextBox.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'LupaLlogariShpejte') {

            window.parent.qendraKostos_TextBox.SetText(values[1]);
            window.parent.qendraKostos_TextBox.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'RegjistrimQendraKosto') {

            window.parent.cmbQendraKosto.SetText(values[1]);
            window.parent.cmbQendraKosto.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'PunonjesQK2') {
            window.parent.cmbQK2.SetSelectedIndex(window.parent.cmbQK2.AddItem(values[1], values[0]));;
            window.parent.cmbQK1.SetSelectedIndex(window.parent.cmbQK1.AddItem(values[5], values[4]));;
            window.parent.cmbQK2.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Raporti2') {
            window.parent.btnQenderKosto2.SetText(values[1], values[0]);
            window.parent.btnQenderKosto1.SetText(values[5], values[4]);
            window.parent.btnQenderKosto2.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Raporti1') {
            window.parent.btnQenderKosto2.SetText(values[1], values[0]);
            window.parent.btnQenderKosto2.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'RegjistrimQendraKostoGrida') {

            window.parent.editorKodi.SetText(values[1]);
            window.parent.editorEmertimi.SetText(values[3]);
            window.parent.editorKodi.SetFocus();
        }
        if (Utils.getUrlVar('vjenNga') == 'Struktura') {

            window.parent.Qendra.SetText(values[1]);
            window.parent.Qendra.SetValue(values[0]);
            window.parent.$('#hfQendra').val(values[1]);
            window.parent.Qendra.SetFocus();
        } else if (Utils.getUrlVar('vjenNga') == 'PunonjesQK2Import') {

            window.parent.editorGlobal.SetSelectedIndex(window.parent.editorGlobal.AddItem(values[1], values[0]));
            window.parent.editorGlobal.SetFocus();
       
        }
        if (Utils.getUrlVar('vjenNga') == 'raporti') {
            for (i = 1; i < values.length; i++)
                kodi = kodi + "," + values[i][1];
            window.parent.btneQenderKosto1.SetText(kodi);
            window.parent.btneQenderKosto1.SetFocus();
        }
        else if (window.parent.identifikuesPerPopupNiveliCmimi == "Import") {
            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
        }
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(gvLupaQendraKosto.GetFocusedRowIndex());
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
        gvLupaQendraKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaQendraKosto.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');