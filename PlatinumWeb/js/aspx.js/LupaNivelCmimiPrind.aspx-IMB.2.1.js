function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaNivCmPrind, "637", '');
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

        gvLupaNivCmPrind.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaNivCmPrind.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNivCmPrind.GetVisibleRowsOnPage() - 1) {
            gvLupaNivCmPrind.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNivCmPrind.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNivCmPrind.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}
function OnGridSelectionChanged() {
    gvLupaNivCmPrind.GetSelectedFieldValues('IdNivelCmimi;KodNivelCmimi;PershkrimNivelCmimi;LlojiNivelCmimi;IdMonedha;BrutoNetoNivelCmimi;KodMonedha;KodBrutoNeto', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");

    if (window.parent.identifikuesPerPopupNiveli == "Modifiko Nivel") {

        if (s == 'undefined') {

            window.parent.cmbLloji.SetSelectedIndex(0);
            window.parent.cmbMonedha.SetSelectedIndex(1);
            window.parent.btneEmertimPrindi.SetText();
            window.parent.cmbPrioriteti.SetSelectedIndex(0);
            window.parent.btneEmertimPrindi.SetFocus(true);
            window.parent.cmbLloji.SetEnabled(true);
            window.parent.cmbBrutoNeto.SetSelectedIndex(0); window.parent.enable();
        }
        else {
            var hf = window.parent.document.getElementById("hfPrindi");
            hf.value = vl[1];
            //window.parent.btneEmertimPrindi.SetText(vl[2]);
            Utils.ShtoNeseNukGjendetDheSelektoCombo(window.parent.btneEmertimPrindi, vl[0], new Array(vl[1], vl[2], vl[3] == 0 ? 'Cmim Shitje' : 'Cmim Blerje', vl[6], vl[7]));
            window.parent.Selected_IndexChanged();
            window.parent.cmbLloji.SetValue(vl[3]);
            window.parent.cmbMonedha.SetValue(vl[4]);
            window.parent.cmbBrutoNeto.SetValue(vl[5]);
            var listefushash = window.parent.document.getElementById("hfPrioriteteMax").value.split(',');
            for (i = 0; i < listefushash.length; i++) {
                var liste = listefushash[i].split(':')
                if (liste[0] == vl[1])
                    window.parent.cmbPrioriteti.SetValue(liste[1]);
            }
            window.parent.cmbLloji.SetEnabled(false);
            window.parent.btneEmertimPrindi.SetFocus(true);
            window.parent.enable();
        }
    }
    else if (window.parent.identifikuesPerPopupNiveliCmimi == "Shto_KF") {

        window.parent.btneNivelCmimi.SetText(vl[2]);
        window.parent.btneNivelCmimi.SetFocus(true);
    } 
    else if (window.parent.identifikuesPerPopupNiveliCmimi == "Import") {

        window.parent.editorGlobal.SetText(vl[2]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupNiveliCmimi == "Modifiko_KF") {
        window.parent.btneNivelCmimi.SetText(vl[2]);
        window.parent.btneNivelCmimi.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopup == "Shto_KlientFurnitor") {
        window.parent.btneNivelCmimi.SetText(vl[2]);
        window.parent.btneNivelCmimi.SetFocus(true);
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
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaNivCmPrind&page=LupaNivelCmimiPrind.aspx&idKonfigAmbjente=577';
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
        gvLupaNivCmPrind.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNivCmPrind.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
