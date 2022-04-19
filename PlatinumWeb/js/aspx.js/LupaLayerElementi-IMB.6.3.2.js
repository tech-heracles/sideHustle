function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLayerElementi, "10019", '');
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
        //gvLayerElementi.SetFocusedRowIndex(0);
        //gvLayerElementi.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLayerElementi.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLayerElementi.GetVisibleRowsOnPage() - 1) {
            gvLayerElementi.SetFocusedRowIndex(0);
        }
        else {
            gvLayerElementi.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLayerElementi.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLayerElementi.GetSelectedFieldValues('IDGLOBALELEMENT;KODI;PERSHKRIMI;LLOJI', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var layerelem = vl[1];

    if (window.parent.identikuesPerPopupLayerElem == 'raportLayerElement') {
        for (i = 1; i < values.length; i++) //layerelem += "," + values[i][1];
            layerelem = layerelem + "," + values[i][1];
        window.parent.editorGlobal.SetText(layerelem);
        window.parent.editorGlobal.SetFocus(true);
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLayerElementi&page=LupaLayerElementi.aspx&idKonfigAmbjente=577';
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
        gvLayerElementi.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLayerElementi.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');