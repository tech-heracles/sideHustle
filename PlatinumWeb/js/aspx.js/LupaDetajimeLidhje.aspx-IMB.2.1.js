function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaDetajime, "697", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
$(window).on('unload', function () {
    window.parent.gvLupaDetArtRegj.PerformCallback();
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaDetajime.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaDetajime.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaDetajime.GetVisibleRowsOnPage() - 1) {
            gvLupaDetajime.SetFocusedRowIndex(0);
        }
        else {
            gvLupaDetajime.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaDetajime.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
var queryStr;
function menu_click(s, e) {
    switch (e.item.name) {
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
        case 'Ruaj':
            if (gvLupaDetajime.GetSelectedRowCount() == 0) {
                e.processOnServer = false;
                myMesazh.ShtoMesazhGabimi('Nuk keni zgjedhur asnje detajim per te lidhur me artikullin!');
            }
            break;
    }
}

function Succeded() {
    queryStr = 'kodArtikulli=' + Utils.getUrlVar('kodArtikulli') + '&veprimi=' + Utils.getUrlVar('veprimi') + '&lloji=' + Utils.getUrlVar('lloji');
    window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaDetajime&page=LupaDetajimeLidhje.aspx&idKonfigAmbjente=577');
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
        gvLupaDetajime.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaDetajime.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');