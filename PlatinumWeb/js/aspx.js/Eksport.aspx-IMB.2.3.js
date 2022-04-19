;


jQuery(document).ready(function () {//po
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
        }
    }).trigger('resize');

    $(window).on('load', function () {
        Init();
        //Utils.resizeSplitter();
    });

    
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
});

function aplikoFiltra(s, e) {
    try {
        if (cmbEmer.GetValue() == 0) {
            pastroFushatKokes();
            visibleKontrolle(false);
            return;
        }
        if (!isNaN(cmbEmer.GetValue())) {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "MerrKonfigurimExporti"),
                data: JSON.stringify({ id: cmbEmer.GetValue() })
            }).done(SucceededCallbackExport);
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimeGjateTransferimit"));
    }
}

function SucceededCallbackExport(result) {
    cmbFormati.ClearItems();
    for (i = 0; i < result[1].length; i++)
        cmbFormati.AddItem(result[1][i].Kodi, result[1][i].IdKoka); //AddItem(teksti, vlera);
    cmbFiltri.ClearItems();
    for (i = 0; i < result[2].length; i++)
        cmbFiltri.AddItem(result[2][i].Kodi, result[2][i].Id); //AddItem(teksti, vlera);
    cmbKategoria.SetValue(result[0].Kategoria);
    if (result[0].Lloji == 2 && !hfState.Get("superuser"))
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(false);
    else
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(true);
    if (result[0].Kategoria == 1 || result[0].Kategoria == 2) {
        cbMerrDokTeMod.SetChecked(result[0].MerrDokTeModifikuar);
        cbMerrDokTeFshire.SetChecked(result[0].MerrDokTeFshire);
    }
    else {
        cbMerrDokTeMod.SetVisible(false);
        cbMerrDokTeFshire.SetVisible(false);
    }

    if (result[0].Lloji == 1) { //file
        if (result[1][0].Kategori === "Flex Cube") {
            lblFiltriData.SetVisible(true);
            dtFiltriDates.SetVisible(true);
            var date = result[0].DatePerFiltrim;
            date = new Date(date);
            var dt = date.getDate();
            var m = date.getMonth();
            var year = date.getFullYear();
            var dateStr = dt + "/" + (m + 1) + "/" + year;
            if (dateStr === '1/1/1')
                dtFiltriDates.SetText('');
            else dtFiltriDates.SetValue(date);
        }
        cmbLlojEksporti.SetValue(1);
        cmbLlojEksporti.SetText("File");
        visibleKontrolle(false);
        rbTipi.SetValue(result[0].Tipi);
        cmbFormati.SetValue(result[0].Formati);
        txtEmerSheet.SetText(result[0].EmerSheet);
        enabled();
        txtEmerSkedari.SetText(result[0].EmerSkedari);
        if (result[0].Filtri != 0) {
            cmbFiltri.SetValue(result[0].Filtri); //gvExport.PerformCallback('Filter;' + cmbFiltri.GetValue()) 
        }
        else {
            cmbFiltri.SetText(''); // gvExport.PerformCallback('Filter;0') 
        }
        if (cmbFiltri.FindItemByText(cmbFiltri.GetInputElement().value) == null) {
            btnRuaj.SetEnabled(true);
            btnFshi.SetEnabled(false);
        }
        else {
            btnRuaj.SetEnabled(false);
            btnFshi.SetEnabled(true);
        }
    }
    else { //sql
        cmbLlojEksporti.SetValue(2);
        cmbLlojEksporti.SetText("SQL");
        visibleKontrolle(false);
        cmbFormati.SetValue(result[0].Formati);
        txtFormatDestinacion.SetText(result[0].FormatDestinacion);
        txtUrlDestinacion.SetText(result[0].UrlDestinacion);
        txtNdermDestinacion.SetText(result[0].NdermarrjeDestinacion);
        txtEmerTabKoka.SetText(result[0].EmerTabKoka);
        txtEmerTabTrupi.SetText(result[0].EmerTabTrupi);
        if (result[0].Kategoria == 45)
            txtEmerTabRec.SetText(result[0].EmerTabRec);
        if (result[0].Filtri != 0) {
            cmbFiltri.SetValue(result[0].Filtri); //gvExport.PerformCallback('Filter;' + cmbFiltri.GetValue()) 
        }
        else {
            cmbFiltri.SetText(''); // gvExport.PerformCallback('Filter;0') 
        }
    }
    btnPastro.DoClick();
}

function MerrFiltraFormati() {
    btnPastro.DoClick();
    if (cmbFormati.GetValue() !== null) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "MerrFiltraExporti"),
            data: JSON.stringify({ id: cmbFormati.GetValue() })
        }).done(SucceededCallbackFiltraExport);
        txtEmerSkedari.SetText(cmbFormati.GetText());
        txtEmerSheet.SetText(cmbFormati.GetText());
    }
}

function SucceededCallbackFiltraExport(result) {
    cmbFiltri.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbFiltri.AddItem(result[i].Kodi, result[i].Id);
    gvExport.ClearFilter();
}

function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('Eksport.aspx', 0); } catch (e) { }
    //myCookies.createCookie('adresa', window.location.href, 1);
}
var eksportNgaDok = false;
function Init() {//po
    if (typeof (isPostBack) == "undefined") {
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        $("#divgride1").show();
        $("#dvFillim").show();
        enabled();
        visibleKontrolle(false);
        if (cmbKategoria.GetSelectedItem()) {
            txtEmerSkedari.SetText(cmbFormati.GetSelectedItem().text);
            txtEmerSheet.SetText(cmbFormati.GetSelectedItem().text);
        }
        
        if (Utils.getUrlVar("idDok") != "" && Utils.getUrlVar("idDok") != "undefined" || Utils.getUrlVar("idkat") != "undefined") {
            cmbKategoria.SetValue(Utils.getUrlVar("idkat"));
            eksportNgaDok = true;
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "MerrFormatImportiSipasKategorise"),
                data: JSON.stringify({ idkategoria: cmbKategoria.GetValue() })
            }).done(SucceededCallback);
            if (Utils.getUrlVar("idDok") != "" && Utils.getUrlVar("idDok") != "undefined")
                gvExport.SelectRows();
        }

    }
}

/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {


}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    cmbKategoria.SetText('');
    cmbEmer.SetText('');
    rbTipi.SetValue('XLS');
    cmbFormati.ClearItems();
    cmbFormati.SetText('');
    cmbFiltri.ClearItems();
    txtEmerSheet.SetText('');
    txtEmerSkedari.SetText('');
    cmbFiltri.SetText('');
    dtFiltriDates.SetText('');
    txtFormatDestinacion.SetText('');
    txtUrlDestinacion.SetText('');
    txtNdermDestinacion.SetText('');
    txtEmerTabKoka.SetText('');
    txtEmerTabTrupi.SetText('');
    txtEmerTabRec.SetText('');
    cbMerrDokTeMod.SetChecked(false);
    cbMerrDokTeFshire.SetChecked(false);
    enabled();
    var hf = document.getElementById("status1");
    hf.value = "false";
    //percaktoVisibleMenu();
}

/*
Function: isValidKoka
    
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    else if (cmbEmer.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoEmrin"));
        return false;
    }
    else if (cmbKategoria.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoKategori"));
        return false;
    }
    else if (cmbFormati.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoFormatin"));
        return false;
    }
    else return true;
}


/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = document.getElementById("status1");

    click = false;
    if (hf.value === "true") {
        myFaqeCelje.kontrolloTeDrejta('Eksport.aspx', true);
    }
    if (hf.value == "export" && !eksportNgaDok) {
        if (cmbFiltri.GetSelectedIndex() != -1)
            gvExport.PerformCallback('Filter;' + cmbFiltri.GetValue())
        else gvExport.PerformCallback('Filter;' + 0);
    }
    if (hf.value == "pastro" && !eksportNgaDok) {
        gvExport.PerformCallback('pastro');
    }
    Utils.hiqLoadingGif();;
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name === 'Ruaj') {
        myFaqeCelje.validim(s, e);
        if (cmbLlojEksporti.GetValue() == 2 && (txtEmerTabKoka.GetText() == "" || ((cmbKategoria.GetValue() == 1 || cmbKategoria.GetValue() == 0 || cmbKategoria.GetValue() == 2 || cmbKategoria.GetValue() == 3 || cmbKategoria.GetValue() == 4 || cmbKategoria.GetValue() == 6 || cmbKategoria.GetValue() == 45) && txtEmerTabTrupi.GetText() == ""))) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTabelatEksportit"));
            e.processOnServer = false;
            return;
        }
        if (!isValidKoka()) {
            e.processOnServer = false;
            return;
        }
        Utils.shfaqLoadingGif();
        RuajClick(s, e);
    }
    else if (e.item.name === "Eksporto") {        
        if (cmbLlojEksporti.GetValue() == 1)
            e.processOnServer = false;
        else
            Utils.shfaqLoadingGif();
        switch (cmbLlojEksporti.GetValue()) {
            case "2":
                if (gvExport.GetSelectedRowCount() == 0) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKaRreshtaPerEksport"));
                    return;
                }
                if (txtEmerTabKoka.GetText() == "" || ((cmbKategoria.GetValue() == 1 || cmbKategoria.GetValue() == 0 || cmbKategoria.GetValue() == 2 || cmbKategoria.GetValue() == 3 || cmbKategoria.GetValue() == 4 || cmbKategoria.GetValue() == 6 || cmbKategoria.GetValue() == 45) && txtEmerTabTrupi.GetText() == "")) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTabelatEksportit"));
                    return;
                }
                break;
            case "1":
                if (rbTipi.GetValue() == "XLS" && gvExport.GetSelectedRowCount() == 0) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKaRreshtaPerEksport"));
                    return;
                }
                if (txtEmerSkedari.GetText() == '') {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhEmerSkedar"));
                    return;
                }
                if (gvExport.cpNoRows == 0) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgSkaRreshtaNeGrid"));
                    return;
                }
                break;
        }
        if (cmbLlojEksporti.GetValue() == 1)
            btnExporto.DoClick();
        else
            e.processOnServer = true;
    }
    else if (e.item.name === 'Ngarko') {
        //hfState.Set("idQueryString", "");
        Utils.shfaqLoadingGif();
        if ((cmbLlojEksporti.GetValue() == 1 && cmbFormati.GetText() == "" && cmbKategoria.GetText() !== 'Flex Cube') || (cmbLlojEksporti.GetValue() == 2 && cmbKategoria.GetText() !== "Eksport Demesh" && cmbFormati.GetText() == "")) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhFormatin"));
            e.processOnServer = false;
            return;
        }
        if (cmbKategoria.GetText() === 'Flex Cube' && (dtFiltriDates.GetValue() == null || dtFiltriDates.GetValue() == "")) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosDaten"));  // 
            e.processOnServer = false;
            return;
        }
        if (cmbLlojEksporti.GetValue() == 2 && cmbKategoria.GetText() == "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhKategorine")); //
            e.processOnServer = false;
            return;
        }
        if (cmbLlojEksporti.GetValue() === 2) {
            var url = Utils.getServerApiUrl("Rregjistrime", "ktheIdSuperKategori");
            $.ajax({
                url: url,
                data: JSON.stringify({ idKategoria: cmbKategoria.GetValue() })
            }).done(function (result) {
                //Nese result eshte 2 eshte Regjistrim pra ka koke dhe trup dhe mund te kete dhe receptura
                if (result === "2" && (txtEmerTabKoka.GetText() === "" || txtEmerTabTrupi.GetText() === "")) {
                    if (cmbKategoria.GetText() === "Ekzekutim Prodhimi" && txtEmerTabRec.GetText() === "") {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniFushat"));
                        e.processOnServer = false;
                        return;
                    }
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniFushatETabelave"));
                    e.processOnServer = false;
                    return;
                }
                    //Nese result eshte 1 ath eshte Celje dhe ka vetem koke
                else if (result === "1" && txtEmerTabKoka.GetText() === "") {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniFushatETabelesKoke"));
                    e.processOnServer = false;
                    return;
                }
            });
        }
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('Eksport.aspx', true);
    }
    else if (e.item.name === 'Fshi') {
        popFshi.Show();
        e.processOnServer = false;
    }
}

function SucceededCallbackExportAutomatik(result) {
    alert(result);
}

var click = false;

/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/
function RuajClick(s, e) {
    if (click) {
        e.processOnServer = false;
        return;
    }
    click = true;
}

/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    pastroFushatKokes();
    pastro();
    var hf = document.getElementById("hfShtimModifikim");
    gvExport.ClearFilter();
    hf.value = "shtim";
    //$('#ASPxSplitter1_hl').empty();
}
function TextChangedFormati() {
    txtEmerSkedari.SetText(cmbFormati.GetText());
    txtEmerSheet.SetText(cmbFormati.GetText());
}
function TextChangedKategoria() {
    txtEmerSkedari.SetText(cmbFormati.GetText());
    if (cmbLlojEksporti.GetValue() == 1) {
        try {
            gvExport.ClearFilter();
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "MerrFormatImportiSipasKategorise"),
                data: JSON.stringify({ idkategoria: cmbKategoria.GetValue() })
            }).done(SucceededCallback);
            if (cmbKategoria.GetText() == 'Flex Cube') {
                lblFiltriData.SetVisible(true);
                dtFiltriDates.SetVisible(true);
                txtEmerSheet.SetText('detail');
                var date = new Date();
                var dt = date.getDate();
                var m = date.getMonth();
                var year = date.getFullYear();
                var dateStr = dt + "/" + (m + 1) + "/" + year;
                dtFiltriDates.SetText(dateStr);
            }
            else {
                lblFiltriData.SetVisible(false);
                dtFiltriDates.SetVisible(false);
                dtFiltriDates.SetValue(null);
            }
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimeGjateTransferimit")); 
        }
    }
    else {
        gvExport.ClearFilter();
        if (cmbKategoria.GetText() !== 'Eksport Demesh') {
            lblFormati.SetVisible(true);
            cmbFormati.SetVisible(true);
            cmbFormati.SetText('');
            gvExport.ClearFilter();
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "MerrFormatImportiSipasKategorise"),
                data: JSON.stringify({ idkategoria: cmbKategoria.GetValue() })
            }).done(SucceededCallback);
        }
        else {
            lblFormati.SetVisible(false);
            cmbFormati.SetVisible(false);
            cmbFormati.SetText('');
        }
    }
}

function SucceededCallback(result) {
    vlerat = result;
    cmbFormati.ClearItems();
    for (i = 0; i < vlerat.length; i++)
        cmbFormati.AddItem(vlerat[i].Kodi, vlerat[i].IdKoka);
    cmbFormati.SelectIndex(0);
    if (eksportNgaDok) {
        cmbKategoria.SetText(Utils.getUrlVar("kategori").replace(/\+/g, " "));
        cmbFormati.SetSelectedItem(cmbFormati.FindItemByText(Utils.getUrlVar("formati").replace(/\+/g, " ")));
    }
    txtEmerSkedari.SetText(cmbFormati.GetSelectedItem().text);
    txtEmerSheet.SetText(cmbFormati.GetSelectedItem().text);
    if (!eksportNgaDok)
        MerrFiltraFormati();
}

function ButtonClickedFormati(editor, key) {
    editorLL = editor;
    identifikuesPerPopupFormati = "Eksport";
    var idkat = 0;
    if (cmbKategoria.GetText() != '') {
        idkat = cmbKategoria.GetValue() != null ? cmbKategoria.GetValue() : Utils.getUrlVar("idkat");
    }
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Format Exporti', 'LupaFormatImporti.aspx?id=' + idkat, 700, 560);
}

function enabled() {
    if (cmbLlojEksporti.GetValue() == 1) {
        if (rbTipi.GetValue() == "XLS" || rbTipi.GetValue() == "XLSX") {
            txtEmerSheet.SetEnabled(true);
        }
        else {
            txtEmerSheet.SetText('');
            txtEmerSheet.SetEnabled(false);
        }
    }
}

function TextChangedLlojEksporti(mbushKategori) {
    if (cmbLlojEksporti.GetText() == 'SQL' && !hfState.Get("superuser"))
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(false);
    else
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(true);
    //gvExport.UnselectRows();
    //gvExport.ClearFilter();
    gvExport.PerformCallback('pastro');
    visibleKontrolle(mbushKategori);
}

function visibleKontrolle(mbushKategori) {
    if (cmbLlojEksporti.GetText() == 'SQL') {
        if (mbushKategori) {
            cmbKategoria.ClearItems();
            cmbKategoria.AddItem("", -1);
            cmbKategoria.AddItem("Eksport Demesh", 0);
            cmbKategoria.AddItem("Shitje", 1);
            cmbKategoria.AddItem("Blerje", 2);
            cmbKategoria.AddItem("Veprime Arka", 3);
            cmbKategoria.AddItem("Veprime Banka", 4);
            cmbKategoria.AddItem("Magazina", 6);
            cmbKategoria.AddItem("Ekzekutim Prodhimi", 45);
            cmbKategoria.AddItem("Klient/Furnitor", 12);
            cmbKategoria.AddItem("Artikuj afatshkurter/afatgjate", 13);
            cmbKategoria.AddItem("Llogari", 14);
            cmbKategoria.AddItem("Grupet e Artikujve", 67);
            cmbKategoria.AddItem("Receptura", 133);
            cmbKategoria.AddItem("Inventarizimi", 135);
            cmbKategoria.AddItem("Inventarizimi afatgjate", 136);
        }
        lblTipi.SetVisible(false);
        rbTipi.SetVisible(false);
        if (cmbKategoria.GetText() == "Eksport Demesh") {
            lblFormati.SetVisible(false);
            cmbFormati.SetVisible(false);
        }
        else {
            lblFormati.SetVisible(true);
            cmbFormati.SetVisible(true);
        }

        lblEmerSheet.SetVisible(false);
        txtEmerSheet.SetVisible(false);
        lblEmerSkedari.SetVisible(false);
        txtEmerSkedari.SetVisible(false);
        lblFiltri.SetVisible(true);
        cmbFiltri.SetVisible(true);
        btnRuaj.SetVisible(true);
        btnFshi.SetVisible(true);
        lblFormatDestinacion.SetVisible(true);
        txtFormatDestinacion.SetVisible(true);
        txtUrlDestinacion.SetVisible(true);
        lblUrlDestinacion.SetVisible(true);
        txtNdermDestinacion.SetVisible(true);
        lblNdermDestinacion.SetVisible(true);
        txtEmerTabKoka.SetVisible(true);
        lblEmerTabKoka.SetVisible(true);
        if (cmbKategoria.GetValue() == 1 || cmbKategoria.GetValue() == 0 || cmbKategoria.GetValue() == 2 || cmbKategoria.GetValue() == 3 || cmbKategoria.GetValue() == 4 || cmbKategoria.GetValue() == 6 || cmbKategoria.GetValue() == 45 || cmbKategoria.GetValue() == 135 || cmbKategoria.GetValue() == 136) {
            txtEmerTabTrupi.SetVisible(true);
            lblEmerTabTrupi.SetVisible(true);
        }
        else {
            txtEmerTabTrupi.SetVisible(false);
            lblEmerTabTrupi.SetVisible(false);
        }
        if (cmbKategoria.GetText() == "Ekzekutim Prodhimi") {
            txtEmerTabRec.SetVisible(true);
            lblEmerTabRec.SetVisible(true);
            lblEmerTabTrupi.SetText("Emri i tabeles se produkteve");
        }
        else {
            txtEmerTabRec.SetVisible(false);
            lblEmerTabRec.SetVisible(false);
            lblEmerTabTrupi.SetText("Emri i tabeles se trupit");
        }
    }
    else {
        lblTipi.SetVisible(true);
        rbTipi.SetVisible(true);
        lblFormati.SetVisible(true);
        cmbFormati.SetVisible(true);
        lblEmerSheet.SetVisible(true);
        txtEmerSheet.SetVisible(true);
        lblEmerSkedari.SetVisible(true);
        txtEmerSkedari.SetVisible(true);
        lblFiltri.SetVisible(true);
        cmbFiltri.SetVisible(true);
        btnRuaj.SetVisible(true);
        btnFshi.SetVisible(true);
        lblFormatDestinacion.SetVisible(false);
        txtFormatDestinacion.SetVisible(false);
        txtUrlDestinacion.SetVisible(false);
        lblUrlDestinacion.SetVisible(false);
        txtNdermDestinacion.SetVisible(false);
        lblNdermDestinacion.SetVisible(false);
        txtEmerTabKoka.SetVisible(false);
        lblEmerTabKoka.SetVisible(false);
        txtEmerTabTrupi.SetVisible(false);
        lblEmerTabTrupi.SetVisible(false);
        txtEmerTabRec.SetVisible(false);
        lblEmerTabRec.SetVisible(false);
        txtEmerTabRec.SetVisible(false);
        lblEmerTabRec.SetVisible(false);
        lblEmerTabTrupi.SetText("Emri i tabeles se trupit");

        if (mbushKategori) {
            cmbKategoria.ClearItems();
            MerrKategoritePerImport();
        }
    }
    if (cmbKategoria.GetValue() == 1 || cmbKategoria.GetValue() == 2) {
        cbMerrDokTeMod.SetVisible(true);
        cbMerrDokTeFshire.SetVisible(true);
    }
    else {
        cbMerrDokTeMod.SetChecked(false);
        cbMerrDokTeMod.SetVisible(false);
        cbMerrDokTeFshire.SetChecked(false);
        cbMerrDokTeFshire.SetVisible(false);
    }
    if (cmbLlojEksporti.GetText() == 'SQL' && !hfState.Get("superuser"))
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(false);
    else
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(true);
}

function MerrKategoritePerImport() {
    var idNdermarrje = hfState.Get("_idNdermarrje");
    var idViti = hfState.Get("_idViti");
    var idPerdoruesi = hfState.Get("_idPerdoruesi");
    
    try {
        gvExport.ClearFilter();
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "MerrKategoritePerImport"),
            data: JSON.stringify({ idNdermarrje: idNdermarrje, idViti: idViti, idPerdoruesi: idPerdoruesi, komponente: "Eksport.aspx"})
        }).done(SucceededCallbackKategorite);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState("msgGabimeGjateTransferimit"));   
    }
}

function SucceededCallbackKategorite(result) {
    var kategorite = result;
    for (var i = 0; i < kategorite.length; i++) {
        cmbKategoria.AddItem(kategorite[i].Pershkrimi, kategorite[i].IdKategori);
    }
}
function BeginCallBackGrida(s, e)
{
    Utils.shfaqLoadingGif();
}

function EndCallbackGrida(s, e) {
    visibleKontrolle(false);
    //gvExport.ClearSelection();
    Utils.hiqLoadingGif();
}

function Selected_IndexChanged(s, e) {
    if (s.FindItemByText(s.GetInputElement().value) != null) {
        btnRuaj.SetEnabled(false); btnFshi.SetEnabled(true);
        //gvExport.PerformCallback('Filter;'+s.GetValue());
        gvExport.PerformCallback('NdryshoFiltrinSipasCombos;' + s.GetValue())
    } else if (s.GetText() == '') {
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(false);
    }
}
function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function Callback_Error(s, e) {
    myMesazh.ShtoMesazhGabimi(e.message); 
    e.handled=true;
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}