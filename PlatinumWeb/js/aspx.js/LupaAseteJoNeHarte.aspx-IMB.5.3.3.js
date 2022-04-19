function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaAsete, "2027", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload', function () {
    $.ajax({
        url: Utils.getServerApiUrl("Autorizime", "fshiGrideNgaSessioniLupa"),
        data: JSON.stringify({ })
    }).done(Utils.fshiSessionFailCheck);
});

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        //gvLupaAsete.SelectRowOnPage(0, true);
        //gvLupaAsete.SetFocusedRowIndex(0);
        //btnOk.Focus();
    }
    catch (err) {
        console.log(err);
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaAsete.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaAsete.GetVisibleRowsOnPage() - 1) {
            gvLupaAsete.SetFocusedRowIndex(0);
        }
        else {
            gvLupaAsete.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaAsete.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    //gvMakro.GetSelectedFieldValues('NrLlogariKF;EmertimiKF', OnGridSelectionComplete);  gvLupaAsete.GetFocusedRowIndex(),
    gvLupaAsete.GetSelectedFieldValues('IdSeriali;SerialiKod;KodArtikulli;PershkrimArtikulli;IdKodifikimi;IdMagazina;IdArtikulli;KodKodifikimi;IdKokaMag;IdTrupDok;KodiMagazina;NrDokumenti;gidPrindi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {

        var IdSeriali = values[0][0];
        var SerialiKod = values[0][1];
        var KodArtikulli = values[0][2];
        var PershkrimArtikulli = values[0][3];
        var IdKodifikimi = values[0][4];
        var IdMagazina = values[0][5];
        var IdArtikulli = values[0][6];
        var KodKodifikimi = values[0][7];
        var IdKokaMag = values[0][8];
        var IdTrupDok = values[0][9];
        var KodiMagazina = values[0][10];
        var gidPrindi = values[0][12];

        window.parent.varSettings.editim.objLidhjeGisWeb.KODI = KodArtikulli;
        window.parent.varSettings.editim.objLidhjeGisWeb.PERSHKRIMI = PershkrimArtikulli;
        window.parent.varSettings.editim.objLidhjeGisWeb.IDMAGAZINA = IdMagazina;
        window.parent.varSettings.editim.objLidhjeGisWeb.IDKODIFIKIMI = IdKodifikimi;
        window.parent.varSettings.editim.objLidhjeGisWeb.IDARTIKULLI = IdArtikulli;
        window.parent.varSettings.editim.objLidhjeGisWeb.IDSERIALI = IdSeriali;
        window.parent.varSettings.editim.objLidhjeGisWeb.IDKOKADOK = IdKokaMag;
        window.parent.varSettings.editim.objLidhjeGisWeb.IDTRUPIDOKLIDHES = IdTrupDok;
        window.parent.varSettings.editim.objLidhjeGisWeb.SERIALKOD = SerialiKod;
        window.parent.varSettings.editim.objLidhjeGisWeb.KODKODIFIKIMI = KodKodifikimi;
        window.parent.varSettings.editim.objLidhjeGisWeb.DTMODIFIKIMI = new Date();
        window.parent.varSettings.editim.objLidhjeGisWeb.gidPrindi = gidPrindi;
        window.parent.varSettings.editim.objLidhjeGisWeb.kodPrindi = KodiMagazina;
        $.extend(window.parent.varSettings.editim.objLidhjeGisWebAfishim, window.parent.varSettings.editim.objLidhjeGisWeb);
        window.parent.objSelektuarNgaLupa = true;
        window.parent.Ext.getCmp('ConnButtonId').enable();
        window.parent.Ext.getCmp('DeleteConnButtonId').enable();
        window.parent.plotesoTeDhenaPaneliUpdateInsert();
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
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    //document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaAsete&page=LupaMagazina.aspx&idKonfigAmbjente=577';
    //popFiltra.Show();
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
        $("#div").show();// $("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAsete.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaAsete.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');