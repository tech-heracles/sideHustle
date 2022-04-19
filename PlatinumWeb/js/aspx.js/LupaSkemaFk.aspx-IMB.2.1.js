function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaSkemaFK, "644", '');
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
        gvLupaSkemaFK.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaSkemaFK.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaSkemaFK.GetVisibleRowsOnPage() - 1) {
            gvLupaSkemaFK.SetFocusedRowIndex(0);
        }
        else {
            gvLupaSkemaFK.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaSkemaFK.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
        window.parent.popupUniversal.Hide();
    }
}

function OnGridSelectionChanged() {
    //gvLupaSkemaFK.GetSelectedFieldValues('Kodi;Monedha', OnGridSelectionComplete);
    gvLupaSkemaFK.GetRowValues(gvLupaSkemaFK.GetFocusedRowIndex(), 'IdKokaSkemaFK;KodiKokaSkemaFK', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var id = ''; 
    id = values[0];
    var kodi = ''; 
    kodi = values[1];
    window.parent.btneSkemaFK.SetText(kodi);
    window.parent.lostFocusSkemaKontabel(kodi);
}


function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged(); 
        window.parent.popupUniversal.Hide();
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaSkemaFK&page=LupaSkemaFk.aspx&idKonfigAmbjente=577';
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
        gvLupaSkemaFK.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaSkemaFK.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');