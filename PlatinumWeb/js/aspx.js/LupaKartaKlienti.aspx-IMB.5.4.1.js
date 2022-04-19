;
function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKartaKlienti, "3034", '');
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
        gvLupaKartaKlienti.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKartaKlienti.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKartaKlienti.GetVisibleRowsOnPage() - 1) {
            gvLupaKartaKlienti.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKartaKlienti.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKartaKlienti.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaKartaKlienti.GetSelectedFieldValues( 'IdKarta;Kodi;Emri;Targa;Shoferi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {

    var kodi = values[0][1];
    var id = values[0][0];
    var emri = values[0][2];
    switch (window.parent.identifikuesPerPopupKartaKlient) {
        case "raportkartaklient":
            if (values.length > 1) {
                for (i = 1; i < values.length; i++)
                    kodi = kodi + "," + values[i][1];
            }
            window.parent.editorGlobal.SetText(kodi);
            window.parent.editorGlobal.SetFocus();
            break;
        case "Import":
            window.parent.editorKF.SetText(kodi);
            window.parent.editorKF.SetFocus(true);
            break;
        default:
            Utils.SelectComboItem(window.parent.cmbKarta, id, kodi, emri);
            window.parent.txtTarga2.SetText(values[0][3]);
            window.parent.txtShoferi.SetText(values[0][4]);
            window.parent.cmbKarta.SetFocus(true);
    }
    ////Utils.SelectComboItem(window.parent.cmbKarta, id, kodi, emri);
    ////window.parent.txtTarga2.SetText(values[0][3]);
    ////window.parent.txtShoferi.SetText(values[0][4]);
    //var item = window.parent.cmbKarta.FindItemByValue(id);
    //window.parent.cmbKarta.SetSelectedItem(item);
    window.parent.popupUniversal.Hide();
    ////window.parent.cmbKarta.SetFocus(true);
    
}


function menu_click(s, e) {
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            window.parent.popupUniversal.Hide();
            break;
        case "Shto":
            e.processOnServer = false;
            window.parent.myButtonClickLupa.LupaUniversal_Click("Shto Karte Klienti", 'Shto_KartaKlienti.aspx?celjeShpejte=po&veprimi=shtim', 900, 600);
            break;
        default:
            e.processOnServer = false;
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKartaKlienti&page=LupaBanka.aspx&idKonfigAmbjente=577';
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
        //$("#div")[0].style.visibility = 'visible';
        $("#div").show();
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKartaKlienti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKartaKlienti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');