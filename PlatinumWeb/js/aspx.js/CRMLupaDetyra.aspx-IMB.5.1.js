; function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaDetyrat, "2012", '');
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
        $('#hfKontrollet').val(window.parent.$('#hfKontrollet').val());
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        // gvLupaAnketa.SetFocusedRowIndex(0);

    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaDetyrat.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaDetyrat.GetVisibleRowsOnPage() - 1) {
            gvLupaDetyrat.SetFocusedRowIndex(0);
        }
        else {
            gvLupaDetyrat.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaDetyrat.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {

    gvLupaDetyrat.GetSelectedFieldValues('IdDetyra;Kodi', OnGridSelectionChangedComplete);
    
}
function OnGridSelectionChangedComplete(values)
{
    var id = '';
    id = values[0][0];
    kodi = values[1];
    
    window.parent.ddKlienti.SetValue(id);
    //window.parent.ddKlienti.SetText(kodi);
    window.parent.ddKlienti.SetFocus(true);
    window.parent.popupUniversal.Hide();
}



function menu_click(s, e) {

    hfState.Set('vjenNga', 'vetLupa');
    switch (e.item.name) {
        case "OK":
            $('#hfKontrollet').val(window.parent.$('#hfKontrollet').val());
            OnGridSelectionChanged();
            e.processOnServer = false;
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
        default:
            break;
    }
}
function EndRequestHandler(sender, args) {//po

    setTimeout(function () {
        window.parent.popupUniversal.Hide();
       
    }, 2000);
}
//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {

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
        //   panel.SetWidth(document.documentElement.clientWidth - 20);
        //  gvLupaAnketa.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        //  panel.SetWidth(document.documentElement.clientWidth - 20);
        // gvLupaAnketa.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
