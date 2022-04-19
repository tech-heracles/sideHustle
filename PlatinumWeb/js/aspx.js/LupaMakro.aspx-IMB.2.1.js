function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaMakro, "633", '');
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
        gvLupaMakro.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaMakro.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaMakro.GetVisibleRowsOnPage() - 1) {
            gvLupaMakro.SetFocusedRowIndex(0);
        }
        else {
            gvLupaMakro.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaMakro.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    //gvLupaMakro.GetSelectedFieldValues('NrLlogariKF;EmertimiKF', OnGridSelectionComplete);
    gvLupaMakro.GetRowValues(gvLupaMakro.GetFocusedRowIndex(), 'KodiKokaMakro;PershkrimiKokaMakro', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var emri; emri = values[1];
    var kodi; kodi = values[0];
    var grida = window.parent.$('#rowed5');
    var idRow = grida.getLastSel2();

    if (window.parent.identifikuesPerPopupMakro == "RegjistrimMagazine") {
     
        var index = idRow;
        var idKontrolli = "#txtKodi" + index;
        window.parent.jQuery(idKontrolli)[0].value = kodi;
        window.parent.jQuery(idKontrolli)[0].focus();
        idKontrolli = "#txtEmertimi" + index;
        window.parent.jQuery(idKontrolli)[0].value = emri;
        if (grida.getInd(index) == grida.getGridParam('reccount') - 1) {
            var index2 = index + 1;
            if (window.parent.arrayReadOnlyKolonaGrides[0] == 'True')
                be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";

            else be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";
            var datarow = { txtFshi: be, txtKategoria: "", txtKodi: "", txtEmertimi: "", txtDetajimet: "", txtMagazina: "", txtNjesia: "", txtSasia: "", txtCmimi: "", txtVlefta: "", txtMagazina2: "" };
            var su = grida.addRowData(parseInt(index) + 1, datarow);
        }
    }
    else if (window.parent.identifikuesPerPopupMakro == "RegjistrimDokumentash") {
        var index = idRow;
        var idKontrolli = "#txtKodi" + index;
        window.parent.jQuery(idKontrolli)[0].value = kodi;
        window.parent.jQuery(idKontrolli)[0].focus();
        idKontrolli = "#txtPershkrimi" + index;
        window.parent.jQuery(idKontrolli)[0].value = emri;
        if (grida.getInd(index) == grida.getGridParam('reccount') - 1) {
            var index2 = parseInt(index) + 1;
            if (window.parent.arrayReadOnlyKolonaGrides[0] == 'True')
                be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";

            else be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";
            if (window.parent.tvshkont == 1 || window.parent.tvshkont == 2)

                el3check = "<input  id ='cbTVSH" + index2 + "'  type ='checkbox' onclick='vendosVleftat(5)' onBlur='vendosVleftat(5)' onfocus='aktivizoresht(" + index2 + ")'  checked='checked' style='width: 100%'     ";
            else el3check = "<input  id ='cbTVSH" + index2 + "'  type ='checkbox' onclick='vendosVleftat(5)' onBlur='vendosVleftat(5)' onfocus='aktivizoresht(" + index2 + ")'  style='width: 100%'    ";
            if (window.parent.arrayReadOnlyKolonaGrides[11] == 'True')
            //el3check += "disabled='disabled'";
                el3check = el3check + "disabled='disabled'";
            //el3check += ">";
            el3check = el3check + ">";
            var datarow = { cmbLloji: "", txtKodi: "", txtPershkrimi: "", txtDetajimi: "", cmbNjesia: "", txtSasia: "", txtCmimi: "", txtZbritja: "", txtVleftaTVSH: "", cbTVSH: el3check, txtVlefta: "", txtFshi: be };
            var su = grida.addRowData(parseInt(index) + 1, datarow);
        }
    }
    else {
        window.parent.editorProdukti.value = kodi;
        window.parent.editorProdukti.focus();
        window.parent.editorPershkrimi.SetText(emri);
        window.parent.editorPershkrimi.SetEnabled(false);
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaMakro&page=LupaMakro.aspx&idKonfigAmbjente=577';
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
        $("#div").show();// $("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaMakro.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaMakro.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
