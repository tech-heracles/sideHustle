function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKonfig, "673", '');
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
        gvLupaKonfig.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKonfig.GetFocusedRowIndex();
    if (event.keyCode === 40) {
        if (currentIndex === gvLupaKonfig.GetVisibleRowsOnPage() - 1) {
            gvLupaKonfig.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKonfig.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode === 38) {
        if (currentIndex === 0) {
            return;
        }
        else {
            gvLupaKonfig.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode === 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    if (Utils.getUrlVar('vjenNgaRaporti') != 'true')
        gvLupaKonfig.GetRowValues(gvLupaKonfig.GetFocusedRowIndex(), 'Id;Kodi', OnGridSelectionComplete);
    else
        gvLupaKonfig.GetSelectedFieldValues('Id;Kodi;', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var grida = window.parent.$('#rowed5');
    var idRow = 0;
    if (grida["getLastSel2"] != undefined)
        idRow = grida.getLastSel2();

    if ((typeof (window.parent.vjenNgaKokeApoTrup) !== "undefined") && window.parent.vjenNgaKokeApoTrup == true) {
        if (Utils.getUrlVar('vjenNga') === "Grupi") {
            window.parent.btneGrupi.SetText(values[1]);
            window.parent.LostFocusObjekti('txtGrupi', window.parent.btneGrupi);
            window.parent.$('#txtGrupi' + idRow).focus();
        }
        else if (Utils.getUrlVar('vjenNga') === 'Titulli') {
            window.parent.btneTitulli.SetText(values[1]);
            window.parent.LostFocusObjekti('txtTitulli', window.parent.btneTitulli);
            window.parent.$('#txtTitulli' + idRow).focus();
        }
        else if (Utils.getUrlVar('vjenNga') === "Kapitulli") {
            window.parent.btneKapitulli.SetText(values[1]);
            window.parent.LostFocusObjekti('txtKapitulli', window.parent.btneKapitulli);
            window.parent.$('#txtKapitulli' + idRow).focus();
        }
    }
    else {
        if (Utils.getUrlVar('vjenNgaRaporti') == 'true') {
            var kontrolli;
            if (Utils.getUrlVar('vjenNga') === 'Kapitulli')
                kontrolli = window.parent.btneKapitulli;
            else if (Utils.getUrlVar('vjenNga') === 'Titulli')
                kontrolli = window.parent.btneProgrami;
            var kodet = Utils.ktheVleratESelektuaraTeBashkuara(values, 1, ",")
            kontrolli.SetText(kodet);
        }
        else if (Utils.getUrlVar('vjenNga') === 'Grupi') {
            window.parent.$('#txtGrupi' + idRow).val(values[1]);
            window.parent.$('#txtGrupi' + idRow).focus();
        }
        else if (Utils.getUrlVar('vjenNga') === 'Titulli') {
            window.parent.$('#txtTitulli' + idRow).val(values[1]);
            window.parent.$('#txtTitulli' + idRow).focus();
        }
        else if (Utils.getUrlVar('vjenNga') === 'Kapitulli') {
            window.parent.$('#txtKapitulli' + idRow).val(values[1]);
            window.parent.$('#txtKapitulli' + idRow).focus();
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKonfig&page=LupaStrukturaAdministrative.aspx&idKonfigAmbjente=577';
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
        gvLupaKonfig.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonfig.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');