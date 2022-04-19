function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaNivCm, "636", '');
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
        gvLupaNivCm.SetFocusedRowIndex(0);
        gvLupaNivCm.SelectRowOnPage(0, true);
        //btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaNivCm.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNivCm.GetVisibleRowsOnPage() - 1) {
            gvLupaNivCm.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNivCm.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNivCm.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaNivCm.GetSelectedFieldValues('IdNivelCmimi;KodNivelCmimi;PershkrimNivelCmimi;LlojiNivelCmimi;IdMonedha;', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var s = new String();    
    s = s + values[0];
    var vl = s.split(",");
    if (window.parent.identifikuesPerPopupNiveli == "Cmim Artikulli") {        
        var id = vl[0];
        var kodi = vl[1];
        var pershkrimi = vl[2];
        var niveli = new Array(kodi, pershkrimi);
        window.parent.btneNiveli.SetSelectedIndex(window.parent.btneNiveli.AddItem(niveli, id));
    }
    else if (window.parent.identikuesPerPopupPerdoruesi == "raporti") {
        window.parent.editorGlobal.SetText(vl[1]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupNiveliCmimi == "Import") {

        window.parent.editorGlobal.SetText(vl[2]);
        window.parent.editorGlobal.SetFocus(true);
    }
    
    else if (window.parent.identifikuesPerPopupNivelCmimeNga == "Shto Nivel") {
        window.parent.btneCmimRetail.SetValue(vl[2]);
        window.parent.btneCmimRetail.SetFocus(true);
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaNivCm&page=LupaNivelCmimi.aspx&idKonfigAmbjente=577';
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
        //panel.SetWidth(document.documentElement.clientWidth - 30);
        //gvLupaNivCm.SetWidth(document.documentElement.clientWidth - 30);
        Init();
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        //panel.SetWidth(document.documentElement.clientWidth - 30);
        //gvLupaNivCm.SetWidth(document.documentElement.clientWidth - 30);
    }
    catch (e) {
    }
}).trigger('resize');