function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaNjesiVartese, "674", '');
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
        //gvLupaNjesiVartese.SelectRowOnPage(0, true);
        //gvLupaNjesiVartese.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaNjesiVartese.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNjesiVartese.GetVisibleRowsOnPage() - 1) {
            gvLupaNjesiVartese.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNjesiVartese.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNjesiVartese.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    //gvMakro.GetSelectedFieldValues('NrLlogariKF;EmertimiKF', OnGridSelectionComplete);  gvLupaNjesiVartese.GetFocusedRowIndex(),
    gvLupaNjesiVartese.GetSelectedFieldValues('Kodi;Pershkrimi;IdNjesiVartese', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        var emri; emri = values[0][1];
        var kodi; kodi = values[0][0];
        var idDege = values[0][2];


        if (window.parent.identifikuesPerPopupMagazina === "RegjistrimMagazine") {
            if (window.parent.identifikuesMagazina === 'Mag1') {
                window.parent.btneMagazina.SetSelectedIndex(window.parent.btneMagazina.AddItem(new Array(kodi, emri), idDege));
                window.parent.btneMagazina.SetFocus();
                window.parent.TextChangedMagazina();
            }
            else {
                window.parent.btneMagazina2.SetSelectedIndex(window.parent.btneMagazina2.AddItem(new Array(kodi, emri), idDege));
                //window.parent.btneMagazina2.SetText(kodi + " (" + emri + ")");
                window.parent.btneMagazina2.SetFocus();
                window.parent.TextChangedMagazina2();
            }
        }


        if (window.parent.identifikuesPerPopupMagazina === "raportmagazina") {
            if (window.parent.identifikuesMagazina === 'Mag1') {

                if (values.length > 1) {
                    for (i = 1; i < values.length; i++)
                        kodi = kodi + "," + values[i][0];
                }

                window.parent.txtBtnnjesiVartese1.SetText(kodi);
                window.parent.txtBtnnjesiVartese1.SetFocus();
                
            }
            else {
                window.parent.txtBtnnjesiVartese2.SetText(kodi);
                window.parent.txtBtnnjesiVartese2.SetFocus();
            }

        }
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaNjesiVartese&page=LupaNjesiVartese.aspx&idKonfigAmbjente=577';
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
        gvLupaNjesiVartese.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNjesiVartese.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');