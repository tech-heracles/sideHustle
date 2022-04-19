function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvDetajimArtikulli, "614", '');
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
        gvDetajimArtikulli.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvDetajimArtikulli.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvDetajimArtikulli.GetVisibleRowsOnPage() - 1) {
            gvDetajimArtikulli.SetFocusedRowIndex(0);
        }
        else {
            gvDetajimArtikulli.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvDetajimArtikulli.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvDetajimArtikulli.GetSelectedFieldValues('KodDetajimArtikulli;KategoriDetajimi;PershkrimDetajimArtikulli', OnGridSelectionComplete);
    //gvDetajimArtikulli.GetRowValues(gvDetajimArtikulli.GetFocusedRowIndex(), 'KodDetajimArtikulli;PershkrimDetajimArtikulli', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var kodi = '';

    for (var i = 0; i < values.length; i++) {
        if (kodi == '')
            kodi = values[i][0];
        else
            kodi = kodi + ',' + values[i][0];
    }



    var pershkrimi = '';

    for (var i = 0; i < values.length; i++) {
        if (pershkrimi == '')
            pershkrimi = values[i][2];
        else
            pershkrimi = pershkrimi + ',' + values[i][2];
    }


    if (window.parent.identifikuesPerPopupDetajime == "RegjistrimDokumentash") {
        window.parent.editorDetajimi.value = kodi;
        window.parent.arr[3][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + kodi;
        window.parent.popupUniversal.Hide();
    }
    else if (window.parent.identifikuesPerPopupDetajime == "raportDetajime") {
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.Focus();
        window.parent.popupUniversal.Hide();
    }
    else if (window.parent.identifikuesPerPopupDetajime == "raportDetajimePershkrim") {
        window.parent.editorGlobal.SetText(pershkrimi);
        window.parent.editorGlobal.Focus();
        window.parent.popupUniversal.Hide();
    }
    else if (window.parent.identifikuesPerPopupDetajime == "Import") {
        window.parent.editordetajimi.SetText(kodi);
        window.parent.editordetajimi.Focus();
        window.parent.popupUniversal.Hide();
        //$.ajax({
        //    url: Utils.getServerApiUrl("Konfigurime", "kontrolloDetajimePerVeprime"),
        //    data: JSON.stringify({ detajimereja: kodi, detajimevjetra: Utils.getUrlVar('detajime'), kodartikulli: Utils.getUrlVar('kodArtikulli') })
        //}).done(SuccededDetajime);
    }
    else {//kur thirret nga Shto_Artikull dhe Modifiko_Artikull
        window.parent.editordetajimi.SetText(kodi);
        window.parent.editorKategoriDetajimi.SetValue(values[0][1]);
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "kontrolloDetajimePerVeprime"),
            data: JSON.stringify({ detajimereja: kodi, detajimevjetra: Utils.getUrlVar('detajime'), kodartikulli: Utils.getUrlVar('kodArtikulli'), idNdermarrje:  hfState.Get('idNdermarrje') })
        }).done(SuccededDetajime);
    }
}

function SuccededDetajime(result) {
    if (result != "") {
        window.parent.editordetajimi.SetText(Utils.getUrlVar('detajime'));
        myMesazh.ShtoMesazhGabimi(result);
    }
    else
        window.parent.popupUniversal.Hide();
}

var queryStr;
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
        case 'Shto':
            e.processOnServer = false;
            if (window.parent.identifikuesPerPopupDetajime == 'RegjistrimDokumentash') {
                queryStr = 'vjenNga=Shto_RegjistrimDokumentash&kodArtikulli=' + Utils.getUrlVar('kodArtikulli') + '&veprimi=' + Utils.getUrlVar('veprimi') + '&lloji=' + Utils.getUrlVar('lloji');
                window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
            }
            else if (window.parent.identikuesPerPopupArtikulli == 'RegjistrimMagazine') {
                queryStr = 'vjenNga=Shto_RegjistrimMagazina&kodArtikulli=' + Utils.getUrlVar('kodArtikulli') + '&veprimi=' + Utils.getUrlVar('veprimi') + '&lloji=' + Utils.getUrlVar('lloji');
                window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?' + queryStr, 900, 600);
            }
            else {
                //window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Detajim', 'LupaDetajimShpejte.aspx?kodArtikulli=' + Utils.getUrlVar('kodArtikulli') + '&veprimi=' + Utils.getUrlVar('veprimi'),900,600);
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionURL"),
                    data: JSON.stringify({ url: window.location.href })
                }).done(Succeded);
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
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvDetajimArtikulli&page=LupaDetajime.aspx&idKonfigAmbjente=577');
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
        gvDetajimArtikulli.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvDetajimArtikulli.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');