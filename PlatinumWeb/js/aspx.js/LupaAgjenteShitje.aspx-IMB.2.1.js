function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaAgjShitje, "607", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
    myFaqeCelje.shtoHandlerSession();
    if (Utils.getUrlVar("vjenNga") != "Agjenti")
        gvLupaAgjShitje.SetFocusedRowIndex(0);
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaAgjShitje.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaAgjShitje.GetVisibleRowsOnPage() - 1) {
            gvLupaAgjShitje.SetFocusedRowIndex(0);
        }
        else {
            gvLupaAgjShitje.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaAgjShitje.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaAgjShitje.GetSelectedFieldValues('IdAgjentShitje;KodiAgjentShitje;EmriAgjentShitje', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {

    var agjent;
    agjent = values[0][1];
    var idagjent;
    idagjent = values[0][0];
    emriagjent = values[0][2];
    if (window.parent.identikuesPerPopupAgjenteShitje == "Shto_KF") {
        //window.parent.cmbAgjentShitjesh.SetText(agjent);
        //window.parent.cmbAgjentShitjesh.Focus();
        var editorAgjenti;
        if (Utils.getUrlVar("agjenti") == "1") {
            editorAgjenti = window.parent.cmbAgjentShitjesh;
            if (window.parent.btnAgjenti2 != undefined && window.parent.btnAgjenti3 != undefined)
            if ((window.parent.btnAgjenti2.GetText() == agjent) || (window.parent.btnAgjenti3.GetText() == agjent)) {
                myMesazh.ShtoMesazhGabimi("Ky agjent është zgjedhur një herë!");
                return;
            }
        }
        else if (Utils.getUrlVar("agjenti") == "2") {
            editorAgjenti = window.parent.btnAgjenti2;
            if (window.parent.cmbAgjentShitjesh != undefined && window.parent.btnAgjenti3 != undefined)
            if ((window.parent.cmbAgjentShitjesh.GetText() == agjent) ||(window.parent.btnAgjenti3.GetText() == agjent)){
                myMesazh.ShtoMesazhGabimi("Ky agjent është zgjedhur një herë!");
                return;
            }
        }

        else if (Utils.getUrlVar("agjenti") == "3") {
            editorAgjenti = window.parent.btnAgjenti3;
            if (window.parent.cmbAgjentShitjesh != undefined && window.parent.btnAgjenti2 != undefined)
            if ((window.parent.cmbAgjentShitjesh.GetText() == agjent) || (window.parent.btnAgjenti2.GetText() == agjent)) {
                myMesazh.ShtoMesazhGabimi("Ky agjent është zgjedhur një herë!");
                return;
            }
        }
        Utils.SelectComboItem(editorAgjenti, idagjent, agjent);
        editorAgjenti.Focus();
    }
    else if (window.parent.identikuesPerPopupAgjenteShitje == "RegjistrimDokumentash") {
        var editorAgjenti
        if (Utils.getUrlVar("agjenti") == "1") {
            editorAgjenti = window.parent.btnAgjenti;
            if (window.parent.btnAgjenti2.GetValue() == idagjent || window.parent.btnAgjenti3.GetValue() == idagjent) {
                myMesazh.ShtoMesazhGabimi("Ky agjent është zgjedhur një herë!");
                return;
            }
        }
        else if (Utils.getUrlVar("agjenti") == "2") {
            editorAgjenti = window.parent.btnAgjenti2;
            if (window.parent.btnAgjenti.GetValue() == idagjent || window.parent.btnAgjenti3.GetValue() == idagjent) {
                myMesazh.ShtoMesazhGabimi("Ky agjent është zgjedhur një herë!");
                return;
            }
        }
        else {
            editorAgjenti = window.parent.btnAgjenti3;
            if (window.parent.btnAgjenti.GetValue() == idagjent || window.parent.btnAgjenti2.GetValue() == idagjent) {
                myMesazh.ShtoMesazhGabimi("Ky agjent është zgjedhur një herë!");
                return;
            }
        }
        editorAgjenti.SetText(agjent);
        editorAgjenti.SetValue(idagjent);
        editorAgjenti.Focus();
        window.parent.changeRadio(Utils.getUrlVar("agjenti"));
    }
    else if (window.parent.identikuesPerPopupAgjenteShitje == "KonfigurimDokumentash") {
        window.parent.editorAGJ.SetValue(idagjent);
        window.parent.editorAGJ.SetFocus(true);
    } else if (window.parent.identikuesPerPopupAgjenteShitje == "Router") {
   
        // Utils.ShtoNeseNukGjendetDheSelektoCombo(window.parent.btnAgjenti, idagjent, agjent, emriagjent);
        Utils.ShtoNeseNukGjendetDheSelektoCombo(window.parent.btnAgjenti, idagjent, new Array(values[0][1], values[0][2]));
       // new Array(value[0][1], value[0][2])
        window.parent.btnAgjenti.SetValue(idagjent);
        window.parent.btnAgjenti.SetFocus(true);
        window.parent.skeduler.PerformCallback();
    }
    else if (Utils.getUrlVar("vjenNga") == "Agjenti") {
        window.parent.cmbDrejtori.SetValue(idagjent);
        window.parent.cmbDrejtori.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupAgjenteShitje == "Import") {
        window.parent.editorGlobal.SetText(agjent);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identikuesPerPopupPerdoruesi == "raporti") {
        if (values.length > 1) {
            for (i = 1; i < values.length; i++)
                agjent = agjent + "," + values[i][1];
        }
        window.parent.editorGlobal.SetText(agjent);
        window.parent.editorGlobal.SetFocus(true);
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaAgjShitje&page=LupaAgjenteShitje.aspx&idKonfigAmbjente=577';
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
        gvLupaAgjShitje.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAgjShitje.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');