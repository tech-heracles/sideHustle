function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaFormatImport, "680", '');
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
        //window.parent.popupUniversal.AdjustSize();
        gvLupaFormatImport.SetFocusedRowIndex(0);
        gvLupaFormatImport.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaFormatImport.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaFormatImport.GetVisibleRowsOnPage() - 1) {
            gvLupaFormatImport.SetFocusedRowIndex(0);
        }
        else {
            gvLupaFormatImport.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaFormatImport.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaFormatImport.GetRowValues(gvLupaFormatImport.GetFocusedRowIndex(), 'Kodi;IdKoka', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    switch (window.parent.identifikuesPerPopupFormati) {
        case "Import":
            window.parent.cmbFormati.SetValue(values[1]);
            window.parent.cmbFormati.SetText(values[0]);
            window.parent.cmbFormati.Focus();
            window.parent.TextChangedFormati();
            break;
        case "Eksport":
            window.parent.cmbFormati.SetValue(values[1]);
            window.parent.cmbFormati.SetText(values[0]);
            window.parent.cmbFormati.Focus();
            window.parent.MerrFiltraFormati();
            window.parent.TextChangedFormati();
            break;
        default:
            break;
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
        case "Eksporto":
            e.processOnServer = false;
            EksportoClick();
            break;
        default:
            break;
    }
}

function EksportoClick() {
    if (gvLupaFormatImport.GetSelectedRowCount() !== 1) {
        myMesazh.ShtoMesazhGabimi("Duhet te zgjidhni nje format!");
        return;
    }
    gvLupaFormatImport.GetRowValues(gvLupaFormatImport.GetFocusedRowIndex(), 'IdKoka', HapLupeEksporto);
}

function HapLupeEksporto(values) {
    myButtonClickLupa.LupaUniversal_Click('Eksportimi', 'LupaEksportim.aspx?id=' + values, 600, 400);   
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaFormatImport&page=LupaFormatImporti.aspx&idKonfigAmbjente=577';
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
        gvLupaFormatImport.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaFormatImport.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize'); 