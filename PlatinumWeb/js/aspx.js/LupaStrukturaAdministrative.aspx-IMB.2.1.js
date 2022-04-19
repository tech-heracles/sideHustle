function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaStruktura, "670", '');
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
        gvLupaStruktura.SetFocusedRowIndex(0);
     //   btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaStruktura.GetFocusedRowIndex();
    if (event.keyCode === 40) {
        if (currentIndex === gvLupaStruktura.GetVisibleRowsOnPage() - 1) {
            gvLupaStruktura.SetFocusedRowIndex(0);
        }
        else {
            gvLupaStruktura.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode === 38) {
        if (currentIndex === 0) {
            return;
        }
        else {
            gvLupaStruktura.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    //if (event.keyCode === 13) {
    //    OnGridSelectionChanged();

    //}
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaStruktura.GetRowValues(index,'IdStrukturaAdm;Emri', OnGridSelectionComplete);
    //gvLupaStruktura.GetRowValues(gvLupaStruktura.GetFocusedRowIndex(), 'IdStrukturaAdm;Emri', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (Utils.getUrlVar('raporti') === 'raporti') {
        var kodiDep = values[1];
        var id = values[0];
      if (Utils.getUrlVar('vjenNga') === 'Departamenti')
        window.parent.editorGlobalValue = id;
        window.parent.editorGlobal.SetText(kodiDep);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (values[0] != undefined) {
     if (Utils.getUrlVar('vjenNga') === 'Departamenti') {

            window.parent.cmbDepartamenti.SetText(values[1]);
            window.parent.cmbDepartamenti.SetFocus(true);
            window.parent.departamentiChanged();
        }
        else if (Utils.getUrlVar('vjenNga') === 'NenDepartamenti') {
            window.parent.cmbNenDepartamenti.SetText(values[1]);
            window.parent.cmbNenDepartamenti.SetFocus(true);
         window.parent.cmbNenDepartamenti.SetSelectedIndex(window.parent.cmbNenDepartamenti.AddItem(values[1], values[0]));
        }
        else if (Utils.getUrlVar('vjenNga') == "ImportDep") {
         window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
            window.parent.grupi = values[0];
        } else if (Utils.getUrlVar('vjenNga') == "ImportNenDep") {
         window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);

        }
    }
    window.parent.popupUniversal.Hide();
}



function menu_click(s, e) {
//    if (e.item.name === 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name === 'Ruaj')
//        popRuaj.Show();
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged(gvLupaStruktura.GetFocusedRowIndex());
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaStruktura&page=LupaStrukturaAdministrative.aspx&idKonfigAmbjente=577';
    popFiltra.Show();
}

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results === null)
        return "";
    else
        return results[1];
}
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaStruktura.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaStruktura.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
        