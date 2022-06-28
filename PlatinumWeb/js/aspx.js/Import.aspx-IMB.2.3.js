;
var pageState = {
    gridImporti: null,
    gridRowIndex: -1,
    gridActive: null,
    isUpdateCanceled: true,
    updatedId: -1,
    updatedIndex: -1,
    dataSourceTrupi: null,
    dataSourceReceptura: null,
    editedIMPORTTRUPISHITJE: -1,
    objectRowsFshi: { kokaFshi: new Array(), trupiFshi: new Array(), recepturaFshi: new Array() },
    keyExpr: "Id",
    imported: false
};

jQuery(document).ready(function () {//po
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
                break;
            default:
                break;
        }
    });
});

$(window).on('resize', function () {//po
    try {
        if (Utils.isGridResized())//if (!Utils.resizeSplitter(300))
            return;
    }
    catch (ee) {
    }
}).trigger('resize');

$(window).on('load', function () {
    Init();
    DevExpress.localization.locale(hfState.Get("_idGjuha") == 0 ? "al" : "en");
    Utils.resizeSplitter();
});

function aplikoFiltra(s, e) {
    try {
        enabledmenu();
        if (cmbEmer.GetValue() == 0) {
            pastroFushatKokes();
            enabled();
            return;
        }
        var emri = cmbEmer.GetValue();
        if (emri && !isNaN(emri)) {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "MerrKonfigurimImporti"),
                data: JSON.stringify({ id: emri })
            }).done(SucceededCallbackImport);
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimit"));
    }
}

function SucceededCallbackImport(result) {
    try { $("#hfIdKonfigImporti").val(result[0].Id); } catch (e) { }
    if (result[0].Id !== 0) {
        cmbFormati.ClearItems();
        for (i = 0; i < result[1].length; i++)
            cmbFormati.AddItem(result[1][i].Kodi, result[1][i].IdKoka); //AddItem(teksti, vlera);
        cmbKategoria.SetValue(result[0].Kategoria);
        rbTipi.SetValue(result[0].Tipi);
        if (result[0].Tipi == "SQL" && !hfState.Get("superuser"))
            ASPxMenu1.GetItemByName("Ruaj").SetEnabled(false);
        else
            ASPxMenu1.GetItemByName("Ruaj").SetEnabled(true);
        if (result[0].Tipi == "SQL") {
            txtEmerTabKoka.SetVisible(true);
            txtEmerTabKokaHistorik.SetVisible(true);
            lblEmerTabKoka.SetVisible(true);
            lblEmerTabKokaHistorik.SetVisible(true);
            txtEmerTabTrupi.SetVisible(true);
            txtEmerTabTrupiHistorik.SetVisible(true);
            btnPastroTabelat.SetVisible(true);
            lblEmerTabTrupi.SetVisible(true);
            lblEmerTabTrupiHistorik.SetVisible(true);
            cbTePaImportuara.SetVisible(true);
            txtNrDokumentash.SetVisible(true);
            lblNrDokumentash.SetVisible(true);
            txtEmerTabKoka.SetText(result[0].EmerTabKoka);
            txtEmerTabKokaHistorik.SetText(result[0].EmerTabKokaHistorik);
            txtEmerTabTrupi.SetText(result[0].EmerTabTrupi);
            txtEmerTabTrupiHistorik.SetText(result[0].EmerTabTrupiHistorik);
            cbTePaImportuara.SetChecked(result[0].MerrTePaImportuara);
            txtNrDokumentash.SetValue(result[0].NrDokumentash);
            if (cmbKategoria.GetValue() == "1" || cmbKategoria.GetValue() == "2") {
                cbRimerrTeImportuara.SetChecked(result[0].RimerrTeImportuara);
                if (cmbKategoria.GetValue() != "2") {
                    cbPermbledhese.SetChecked(result[0].GjeneroFaturePermbledhese);
                    cbTransferoFatura.SetChecked(result[0].TransferoFatura);
                }
            }
            if (result[0].Kategoria == 45) {
                txtEmerTabRec.SetText(result[0].EmerTabRec);
                txtEmerTabRecHistorik.SetText(result[0].EmerTabRecHistorik);
            }
        }
        else {
            txtEmerTabKoka.SetVisible(false);
            txtEmerTabKokaHistorik.SetVisible(false);
            lblEmerTabKoka.SetVisible(false);
            lblEmerTabKokaHistorik.SetVisible(false);
            txtEmerTabTrupi.SetVisible(false);
            txtEmerTabTrupiHistorik.SetVisible(false);
            btnPastroTabelat.SetVisible(false);
            lblEmerTabTrupi.SetVisible(false);
            lblEmerTabTrupiHistorik.SetVisible(false);
            txtEmerTabKoka.SetText('');
            txtEmerTabKokaHistorik.SetText('');
            txtEmerTabTrupi.SetText('');
            txtEmerTabTrupiHistorik.SetText('');
            cbPermbledhese.SetChecked(false);
            cbTransferoFatura.SetChecked(false);
            cbTePaImportuara.SetVisible(false);
            cbTePaImportuara.SetChecked(false);
            txtNrDokumentash.SetVisible(false);
            lblNrDokumentash.SetVisible(false);
            txtNrDokumentash.SetValue(null);
            cbRimerrTeImportuara.SetVisible(false);
            cbRimerrTeImportuara.SetChecked(true);
        }
        cmbFormati.SetValue(result[0].Formati);
        txtEmerSheet.SetText(result[0].EmerSheet);
        enabled();
        cbDergoMeEmail.SetChecked(result[0].DergoMeEmail);
        btnKonfiguroEmail.SetEnabled(cbDergoMeEmail.GetChecked());
    }
}

function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('Import.aspx', 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

function enabledmenu() {
    if (hfTeDrejta.Get('Shtim') == true && cmbEmer.GetSelectedIndex() == -1) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }
    else if (hfTeDrejta.Get('Modifikim') == true && cmbEmer.GetSelectedIndex() != -1) {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }
    else {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
    }
}

function Init() {
    if (typeof (isPostBack) == "undefined") {
        enabledmenu();
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        $("#divgride1").show();
        $("#dvFillim").show();
        enabled();
        PlotesoTeDhenaPerImport();
    }
}

function PlotesoTeDhenaPerImport() {
    if (!window.parent.pageState.teDhenaImporti) {
        ASPxMenu1.GetItemByName('Anullo').SetVisible(false);
        return;
    }
    myMesazh.eshteLupe = true;
    TextChangedKategoria(window.parent.pageState.teDhenaImporti.idKategoria);

    window.parent.popupUniversal.OnCloseButtonClick = function () { MbyllPopUpImporti(); };
}

function MbyllPopUpImporti() {
    myMesazh.closeAll();
    window.parent.popupUniversal.Hide();
    if (pageState.imported)
        window.parent.RifreskoGride();
}
/*
Function: pastro

Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    enabled();
}

/*
Function: pastroFushatKokes

Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    cmbKategoria.SetText('');
    cmbEmer.SetText('');
    rbTipi.SetValue('XLS');
    cmbFormati.SetText('');
    txtEmerSheet.SetText('');
    ucEmerSkedari.ClearText('');
    cmbFormati.ClearItems();
    txtEmerTabKoka.SetText('');
    txtEmerTabKokaHistorik.SetText('');
    txtEmerTabTrupi.SetText('');
    txtEmerTabTrupiHistorik.SetText('');
    txtEmerTabRec.SetText('');
    txtEmerTabRecHistorik.SetText('');
    cbDergoMeEmail.SetChecked(false);
    $("#hfKaVleraTeImportuara").val("False");
    enabled();
    var hf = document.getElementById("status1");
    hf.value = "false";
    cbTePaImportuara.SetChecked(false);
    txtNrDokumentash.SetValue(null);
    cbRimerrTeImportuara.SetChecked(true);
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
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoEmer"));
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
        myFaqeCelje.kontrolloTeDrejta('Import.aspx', true);
    }

    var emraTabHistoriku = JSON.parse($("#tabHistoriku").val());
    if (emraTabHistoriku) {
        txtEmerTabKokaHistorik.SetText(emraTabHistoriku.EmerTabKokaHistorik);
        txtEmerTabTrupiHistorik.SetText(emraTabHistoriku.EmerTabTrupiHistorik);
        txtEmerTabRecHistorik.SetText(emraTabHistoriku.EmerTabRecHistorik);
    }

    Utils.hiqLoadingGif();
}

var raportgab = false;
/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name === 'Ruaj') {
        if (rbTipi.GetValue() == "SQL") {

            if (txtEmerTabKoka.GetText() == '') { //|| txtEmerTabTrupi.GetText()==''
                e.processOnServer = false;
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniEmratETabelave"));
                return;
            }
            if (!hfState.Get("superuser")) {
                e.processOnServer = false;
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniTeDrejta"));
                return;
            }
        }
        myFaqeCelje.validim(s, e);

        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            //myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }
    else if (e.item.name === "Importo") {
        if (!pageState.gridImporti || pageState.gridImporti.GetData().length <= 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("MsgSkaRreshtPerKontroll"));
            return;
        }

        e.processOnServer = false;
        if (rbTipi.GetValue() === "SQL") {
            Utils.shfaqLoadingGif();
            kontrolloImportoTeDhenaGridImporti(true);
            raportgab = true;
            return;
        }

        if ($("#hfKaVleraTeImportuara").val() == 'True') {
            myMesazh.ShtoMesazh({
                text: hfState.Get("msgDeshironiTeMbishkruaniVlerat"),
                type: "confirm",
                modal: true,
                idGjuha: hfState.Get('idGjuha'),
                okClick: function () {
                    DoMenuClickPostBack(true, s, e);
                },
                cancelClick: function () {
                    DoMenuClickPostBack(false, s, e);
                }
            });
        }
        else {
            Utils.shfaqLoadingGif();
            $("#hfMbishkruajVleraImporti").val(false);
            kontrolloImportoTeDhenaGridImporti(true);
        }
        raportgab = true;
    }
    else if (e.item.name === "Kontrollo") {
        e.processOnServer = false;

        if (!pageState.gridImporti || pageState.gridImporti.GetData().length <= 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("MsgSkaRreshtPerKontroll"));
            return;
        }

        raportgab = true;
        Utils.shfaqLoadingGif();
        kontrolloImportoTeDhenaGridImporti(false);

        if (rbTipi.GetValue() === "SQL")
            return;
    }
    else if (e.item.name === "ListaGabimeve") {
        e.processOnServer = false;
        if (raportgab == false && !cbTransferoFatura.GetChecked())
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKontrolloGabimet"));

        else
            window.open("RaportiShpejte.aspx?emriReal=gabimeImporti&printo=0&Sesioni=false&db=jo&scopeID=" + Utils.getUrlVar("scopeID"));
    }
    else if (e.item.name === 'Ngarko') {
        raportgab = false;
        e.processOnServer = false;
        if (cmbFormati.GetText() == "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhFormatin"));
            return;
        }
        if (rbTipi.GetValue() !== "SQL" && ucEmerSkedari.GetText() == '') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhSkedarin"));
            return;
        }

        if (rbTipi.GetValue() === "SQL") {
            var url = Utils.getServerApiUrl("Rregjistrime", "ktheIdSuperKategori");
            Utils.shfaqLoadingGif();
            $.ajax({
                url: url,
                data: JSON.stringify({ idKategoria: cmbKategoria.GetValue() })
            }).done(function (result) {
                Utils.shfaqLoadingGif();
                //Nese result eshte 2 eshte Regjistrim pra ka koke dhe trup dhe mund te kete dhe receptura
                if (result === "2" && (txtEmerTabKoka.GetText() === "" || txtEmerTabTrupi.GetText() === "")) {
                    if (cmbKategoria.GetText() === "Ekzekutim Prodhimi" && txtEmerTabRec.GetText() === "") {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniFushat"));
                        e.processOnServer = false;
                        Utils.hiqLoadingGif();
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
                    Utils.hiqLoadingGif();
                    return;
                }
                callWebServiceKtheDataSourceDheFormatImporti();
            });
        }
        else
            ucEmerSkedari.Upload();
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('Import.aspx', true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
        popFshi.Show();
        e.processOnServer = false;
    }
    else if (e.item.name === 'Anullo') {
        e.processOnServer = false;
        MbyllPopUpImporti();
    }
    enabledmenu();
}
function DoMenuClickPostBack(mbishkruajVlera, s, e) {
   $("#hfMbishkruajVleraImporti").val(mbishkruajVlera);
    $("#hfKaVleraTeImportuara").val('False');
    Utils.shfaqLoadingGif();
    kontrolloImportoTeDhenaGridImporti(true);
    //__doPostBack(s.name, "CLICK:" + e.item.index);
}
function SucceededCallbackImportAutomatik(result) {
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
    pastro();
    pastroFushatKokes();
    var hf = document.getElementById("hfShtimModifikim");

    raportgab = false;
    hf.value = "shtim";
    $('#ASPxSplitter1_hl').empty();
}

function KlonoClick(e) {
    var hf1 = document.getElementById("hfShtimModifikim");
    e.processOnServer = false;
}

function TextChangedKategoria(idKategoria) {
    if (cmbKategoria.GetValue() === "1") {
        //rbTipi.GetItem(3).SetEnabled(true);
        cbPermbledhese.SetVisible(true);
        cbPermbledhese.SetEnabled(true);
        if (rbTipi.GetValue() == "SQL") {
            cbTransferoFatura.SetVisible(true);
            cbTransferoFatura.SetEnabled(true);
        }
        else {
            cbTransferoFatura.SetVisible(false);
            cbTransferoFatura.SetEnabled(false);
        }
    }
    else
        if (cmbKategoria.GetValue() !== "1" && cmbKategoria.GetValue() !== "2") {
            //rbTipi.GetItem(3).SetEnabled(false);
            cbPermbledhese.SetVisible(false);
            cbPermbledhese.SetEnabled(false);
            cbTransferoFatura.SetVisible(false);
            cbTransferoFatura.SetEnabled(false);
        }
    try {
        $.ajax({
            pritPergjigje: true,
            showLoading:true,
            url: Utils.getServerApiUrl("Rregjistrime", "MerrFormatImportiSipasKategorise"),
            data: JSON.stringify({ idkategoria: idKategoria ? idKategoria : cmbKategoria.GetValue() })
        }).done(function (result) { SucceededCallback(result, idKategoria); });
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimit"));
    }
}

function SucceededCallback(result, idKategoria) {
    if (idKategoria)
        cmbKategoria.SetValue(idKategoria);

    vlerat = result;
    cmbFormati.ClearItems();
    for (i = 0; i < vlerat.length; i++)
        cmbFormati.AddItem(vlerat[i].Kodi, vlerat[i].IdKoka);
    cmbFormati.SelectIndex(0);
    if (cmbKategoria.GetText() == 'Perdoruesit') {
        cbDergoMeEmail.SetVisible(true);
        btnKonfiguroEmail.SetVisible(true);
    }

    if (window.parent.pageState.teDhenaImporti) {
        cmbFormati.SetSelectedItem(cmbFormati.FindItemByText(window.parent.pageState.teDhenaImporti.formatImporti));
        rbTipi.SetValue(window.parent.pageState.teDhenaImporti.tipi);
    }

    enabled();
}

function ButtonClickedFormati(editor, key) {
    editorLL = editor;
    identifikuesPerPopupFormati = "Import";
    var idkat = 0;
    if (cmbKategoria.GetText() != '')
        idkat = cmbKategoria.GetValue();
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Format Importi', 'LupaFormatImporti.aspx?id=' + idkat, 700, 560);
}
function TextChangedFormati() {
    enabled();
}

function enabled() {
    pageState.keyExpr = "Id";
    fshiGrideImporti();
    if (cmbKategoria.GetValue() === "1") {
        cbPermbledhese.SetVisible(true);
        cbPermbledhese.SetEnabled(true);
        if (rbTipi.GetValue() == "SQL") {
            cbTransferoFatura.SetVisible(true);
            cbTransferoFatura.SetEnabled(true);
        }
        else {
            cbTransferoFatura.SetVisible(false);
            cbTransferoFatura.SetEnabled(false);
        }
    }
    else
        if (cmbKategoria.GetValue() !== "1") {
            cbPermbledhese.SetVisible(false);
            cbPermbledhese.SetEnabled(false);
            cbTransferoFatura.SetVisible(false);
            cbTransferoFatura.SetEnabled(false);
        }
    if (rbTipi.GetValue() == "XLS" || rbTipi.GetValue() == "XLSX") {
        txtEmerSheet.SetEnabled(true);
        cmbFormati.SetEnabled(true);
        ucEmerSkedari.SetVisible(true);
        lblEmerSkedari.SetVisible(true);
        txtEmerTabKoka.SetVisible(false);
        txtEmerTabKokaHistorik.SetVisible(false);
        lblEmerTabKoka.SetVisible(false);
        lblEmerTabKokaHistorik.SetVisible(false);
        txtEmerTabTrupi.SetVisible(false);
        txtEmerTabTrupiHistorik.SetVisible(false);
        btnPastroTabelat.SetVisible(false);
        lblEmerTabTrupi.SetVisible(false);
        lblEmerTabTrupiHistorik.SetVisible(false);
        txtEmerTabRec.SetVisible(false);
        txtEmerTabRecHistorik.SetVisible(false);
        lblEmerTabRec.SetVisible(false);
        lblEmerTabRecHistorik.SetVisible(false);
        lblEmerTabTrupi.SetText("Emri i tabeles se trupit");
        lblEmerTabTrupiHistorik.SetText("Emri i tabeles se historikut te trupit");
        cbTePaImportuara.SetChecked(false);
        cbTePaImportuara.SetVisible(false);
        txtNrDokumentash.SetValue(null);
        txtNrDokumentash.SetVisible(false);
        lblNrDokumentash.SetVisible(false);
        cbRimerrTeImportuara.SetChecked(true);
        cbRimerrTeImportuara.SetVisible(false);
    }
    else if (rbTipi.GetValue() == "CSV") {
        txtEmerSheet.SetText('');
        txtEmerSheet.SetEnabled(false);
        cmbFormati.SetEnabled(true);
        ucEmerSkedari.SetVisible(true);
        lblEmerSkedari.SetVisible(true);
        txtEmerTabKoka.SetVisible(false);
        txtEmerTabKokaHistorik.SetVisible(false);
        lblEmerTabKoka.SetVisible(false);
        lblEmerTabKokaHistorik.SetVisible(false);
        txtEmerTabTrupi.SetVisible(false);
        txtEmerTabTrupiHistorik.SetVisible(false);
        btnPastroTabelat.SetVisible(false);
        lblEmerTabTrupi.SetVisible(false);
        lblEmerTabTrupiHistorik.SetVisible(false);
        txtEmerTabRec.SetVisible(false);
        txtEmerTabRecHistorik.SetVisible(false);
        lblEmerTabRec.SetVisible(false);
        lblEmerTabRecHistorik.SetVisible(false);
        lblEmerTabTrupi.SetText("Emri i tabeles se trupit");
        lblEmerTabTrupiHistorik.SetText("Emri i tabeles se historikut te trupit");
        cbTePaImportuara.SetChecked(false);
        cbTePaImportuara.SetVisible(false);
        txtNrDokumentash.SetValue(null);
        txtNrDokumentash.SetVisible(false);
        lblNrDokumentash.SetVisible(false);
        cbRimerrTeImportuara.SetChecked(true);
        cbRimerrTeImportuara.SetVisible(false);
    }
    else if (rbTipi.GetValue() == "SQL") {
        pageState.keyExpr = "IDIMPORTSHITJE";
        cmbFormati.SetEnabled(true);
        txtEmerSheet.SetEnabled(false);
        txtEmerSheet.SetText('');
        ucEmerSkedari.SetVisible(false);
        lblEmerSkedari.SetVisible(false);
        txtEmerTabKoka.SetVisible(true);
        txtEmerTabKokaHistorik.SetVisible(true);
        lblEmerTabKoka.SetVisible(true);
        lblEmerTabKokaHistorik.SetVisible(true);
        cbTePaImportuara.SetVisible(true);
        txtNrDokumentash.SetVisible(true);
        lblNrDokumentash.SetVisible(true);
        cbRimerrTeImportuara.SetVisible(false);
        if (cmbKategoria.GetValue() == 1 || cmbKategoria.GetValue() == 2 || cmbKategoria.GetValue() == 3 || cmbKategoria.GetValue() == 4 || cmbKategoria.GetValue() == 6 || cmbKategoria.GetValue() == 45 || cmbKategoria.GetValue() == 136 || cmbKategoria.GetValue() == 135 || cmbKategoria.GetValue() == 177) {
            if (cmbKategoria.GetValue() == 1 || cmbKategoria.GetValue() == 2)
                cbRimerrTeImportuara.SetVisible(true);
            txtEmerTabTrupi.SetVisible(true);
            txtEmerTabTrupiHistorik.SetVisible(true);
            btnPastroTabelat.SetVisible(true);
            lblEmerTabTrupi.SetVisible(true);
            lblEmerTabTrupiHistorik.SetVisible(true);
        }
        else {
            txtEmerTabTrupi.SetVisible(false);
            txtEmerTabTrupiHistorik.SetVisible(false);
            btnPastroTabelat.SetVisible(true);
            lblEmerTabTrupi.SetVisible(false);
            lblEmerTabTrupiHistorik.SetVisible(false);
        }
        if (cmbKategoria.GetText() == "Ekzekutim Prodhimi") {
            txtEmerTabRec.SetVisible(true);
            txtEmerTabRecHistorik.SetVisible(true);
            lblEmerTabRec.SetVisible(true);
            lblEmerTabRecHistorik.SetVisible(true);
            lblEmerTabTrupi.SetText("Emri i tabeles se produkteve");
            lblEmerTabTrupiHistorik.SetText("Emri i tabeles se historikut te produkteve");
        }
        else {
            txtEmerTabRec.SetVisible(false);
            txtEmerTabRecHistorik.SetVisible(false);
            lblEmerTabRec.SetVisible(false);
            lblEmerTabRecHistorik.SetVisible(false);
            lblEmerTabTrupi.SetText("Emri i tabeles se trupit");
            lblEmerTabTrupiHistorik.SetText("Emri i tabeles se historikut te trupit");
        }
    }
    var eshtePerdorues = (cmbKategoria.GetText() == "Perdoruesit");
    cbDergoMeEmail.SetVisible(eshtePerdorues);
    btnKonfiguroEmail.SetVisible(eshtePerdorues);
    btnKonfiguroEmail.SetEnabled(cbDergoMeEmail.GetChecked());
}

function rbTipiIndexChanged(s, e) {
    if (s.GetValue() == "SQL" && !hfState.Get("superuser"))
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(false);
    else
        ASPxMenu1.GetItemByName("Ruaj").SetEnabled(true);
    enabled();
}

function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();
}

function closing(s, e) {
    popupUniversal.SetContentUrl('');
}


function PoClick(s, e) {
    if (pageState.gridActive && pageState.gridRowIndex > -1) {
        pageState.gridActive.DeleteRow(pageState.gridRowIndex);
        pageState.gridActive = null;
        pageState.gridRowIndex = -1;
    }
}

function JoClick(s, e) {
    return;
}

/*
Function: HapLupaKonfigurimEmail
Hap lupen e per konfigurimin e e-mail.
*/
function HapLupaKonfigurimEmail(s, e) {
    e.processOnServer = false;
    var IdKonfigImporti = $("#hfIdKonfigImporti").val();
    popupUniversal.SetHeaderText('Konfiguro e-mail');
    popupUniversal.SetContentUrl('LupaKonfigurimEmailImport.aspx?idKonfigImporti=' + IdKonfigImporti);
    popupUniversal.SetSize(750, 500);
    popupUniversal.Show();
}

function cbDergoMeEmail_CheckedChanged(s, e) {
    var IdKonfigImporti = $("#hfIdKonfigImporti").val();
    if (IdKonfigImporti == "" || IdKonfigImporti == "0") {
        cbDergoMeEmail.SetChecked(false);
        myMesazh.ShtoMesazhGabimi("Duhet te ruani templatin me pare!");
        return;
    }
    btnKonfiguroEmail.SetEnabled(s.GetChecked());
}

function ucEmerSkedariFileUploadComplete(s, e) {
    callWebServiceKtheDataSourceDheFormatImporti();
}

//Fshin gride e importit ne momentin qe ndryshon kategoria, konfigurimi ose behet pastrimi i ambientit
function fshiGrideImporti() {
    pageState.gridImporti = null;
    $("#dxDataGrid_Importi").remove();
    $("<div id='dxDataGrid_Importi' class ='noUndoGrida'>").appendTo($("#data-grid-importi"));
    $("#dxDataGrid_Importi").dxDataGrid({});
}

//Shfaqe pyetjen ne momentin qe perdoruesi do te fshije nje rresht nga gridat
function DeleteRow(grida, rowIndex) {
    pageState.gridActive = grida;
    pageState.gridRowIndex = rowIndex;
    myMesazh.ShtoPyetje("Doni ta fshini kete rresht?", false);
}

//Therret metoden e webservices qe kthen te dhenat per import dhe konfigurimin e grides
function callWebServiceKtheDataSourceDheFormatImporti() {
    $.ajax({
        url: Utils.getServerApiUrl("Importi", "KtheDataSourceDheFormatImporti"),
        data: JSON.stringify({
            idKategoria: cmbKategoria.GetValue(),
            kategoria: cmbKategoria.GetText(),
            formati: cmbFormati.GetValue(),
            lloji: rbTipi.GetValue(),
            emerTabeleKoke: txtEmerTabKoka.GetText(),
            emerTabeleTrupi: txtEmerTabTrupi.GetText(),
            emerTabeleRec: txtEmerTabRec.GetText(),
            transferoFatura: cbTransferoFatura.GetChecked(),
            tePaImportuara: cbTePaImportuara.GetChecked(),
            emerSheet: txtEmerSheet.GetText(),
            rimerrTeImportuara: cbRimerrTeImportuara.GetChecked(),
            nrDokumentash: txtNrDokumentash.GetValue()
        })
    }).done(SucceededCallbackDataSourceDheFormatImporti);
}

//Therritet pasi kthehet pergjigja e webservices qe kthen te dhenat e importit dhe konfigurimet e grides
function SucceededCallbackDataSourceDheFormatImporti(result) {
    var mesazh = result.mesazh;
    var dataSource = result.dataSource;
    var columnsKonfig = result.columnsKonfig;
    Utils.hiqLoadingGif();
    if (!mesazh.Status) {
        myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
        return;
    }

    bejGatiGrideImporti(columnsKonfig, dataSource);
}

//Ben gati griden kryesore te importit
function bejGatiGrideImporti(columnsKonfig, dataSource) {

    if (rbTipi.GetValue() == "SQL") {
        pageState.gridImporti = new myDxDataGrid("dxDataGrid_Importi", {
            customData: { dataSource: Utils.CloneObject(dataSource) },
            dataSource: new Array(),
            keyExpr: pageState.keyExpr,
            addEditRowCommand: true,
            addDeleteRowCommand: true,
            deleteMethod: DeleteRow,
            columnAutoWidth: true,
            showRowLines: true,
            headerFilter: { visible: true },
            columns: [
                { dataField: "ID", caption: "Employee Id" },
            ],
            scrolling: { useNative: true },
            paging: { pageSize: 10 },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20, 30],
                showInfo: true
            },
            formatoVleraNumerike: false,
            onRowPrepared: vendosStileRreshti,
            editing: {
                mode: "form",
                texts: {
                    confirmDeleteMessage: '',
                    validationCancelChanges: '',
                    saveRowChanges: 'Ruaj',
                    cancelRowChanges: "Anullo"
                },
                form: {
                    colCount: 4,
                    onContentReady: shtoEventsNeRuajDheAnullo
                }
            },
            onEditingStart: vendosTeDhenaRreshtiNePageState,
            onRowRemoved: fshiRreshtNgaGridaImportit
        });
       
    }
    else {
        pageState.gridImporti = new myDxDataGrid("dxDataGrid_Importi", {
            customData: { dataSource: Utils.CloneObject(dataSource) },
            dataSource: new Array(),
            keyExpr: pageState.keyExpr,
            addEditRowCommand: true,
            addDeleteRowCommand: true,
            deleteMethod: DeleteRow,
            columnAutoWidth: true,
            showRowLines: true,
            scrolling: { useNative: true },
            paging: { pageSize: 10 },
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20, 30],
                showInfo: true
            },
            formatoVleraNumerike: false,
            onRowPrepared: vendosStileRreshti,
            editing: {
                mode: "form",
                texts: {
                    confirmDeleteMessage: '',
                    validationCancelChanges: '',
                    saveRowChanges: 'Ruaj',
                    cancelRowChanges: "Anullo"
                },
                form: {
                    colCount: 4,
                    onContentReady: shtoEventsNeRuajDheAnullo
                }
            },
            onEditingStart: vendosTeDhenaRreshtiNePageState,
            onRowRemoved: fshiRreshtNgaGridaImportit
        });
    }
   

    var fushaTrupiLength = columnsKonfig.fushaTrupi.length;
    pageState.gridImporti.Grida.option("dataSource", pageState.gridImporti.Options.customData.dataSource.koka); // vendos dataSource grides
    pageState.gridImporti.SetColumnsFromConfig(columnsKonfig.fushaKoke); //percakton kolonat e grides﻿
    pageState.gridImporti.SetFormEditingFieldsFromColumns(fushaTrupiLength > 0 ? "Koka e dokumentit" : ""); //percakton fushat sipas kolonave

    if (rbTipi.GetValue() == "SQL") {
        shtoMasterDetailKoka(columnsKonfig, fushaTrupiLength);
        shtoGrideTrupiNeEditForm(columnsKonfig, fushaTrupiLength);
    }
}

//Ruaj modifikimet e bera ne gridat e edit formes dhe grides kryesore ne momentin qe i jap Ruaj formes
function ruajTeDhenaRreshtiNgaForma() {
    var gridaRecepturaEdited = $(".gridaRecepturaDiv").find(".dx-datagrid.dx-gridbase-container");
    if (gridaRecepturaEdited) {
        for (var i = 0; i < gridaRecepturaEdited.length; i++) {
            var gridRec = $(gridaRecepturaEdited[i]).first().parent().dxDataGrid("instance");
            gridRec.saveEditData();
        }
    }

    var gridaTrupiEdited = $("#gridaTrupiDiv").find(".dx-datagrid.dx-gridbase-container").first().parent().dxDataGrid("instance");
    if (gridaTrupiEdited)
        gridaTrupiEdited.saveEditData();

    pageState.gridImporti.SaveCurrentValues();
    ruajTeDhenaNeSourceGride(pageState.gridImporti.Grida);
}

//Bej update sourcet e gridave (kryesore, trupit dhe recepturave) qe perdoren per shfaqje
//Therras funksionin per modifikimin e te dhenave ne databaze
function ruajTeDhenaNeSourceGride(grida) {
    pageState.isUpdateCanceled = true;
    var uFshiKoka = updateOriginalDataSource();
    var detailRowIndex = pageState.updatedIndex + 1;
    var cellMasterElement = grida.getCellElement(detailRowIndex, 0);
    if (cellMasterElement) {
        var $detailCell = cellMasterElement.parent().find(".dx-master-detail-cell");
        var dataGrid = $detailCell.find(".dx-datagrid").first().parent().dxDataGrid("instance");

        if (dataGrid) {
            var dsTrupi = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.trupi);
            var dataSource = dsTrupi.filter(function (row) { return row[pageState.keyExpr] == pageState.updatedId; });
            dataGrid.option("dataSource", dataSource);
            dataGrid.refresh();
        }
    }

    if (pageState.gridImporti.Grida.option("masterDetail").enabled)
        pageState.gridImporti.Grida.expandRow(pageState.gridImporti.Grida.getKeyByRowIndex(pageState.updatedIndex));

    if (!uFshiKoka && rbTipi.GetValue() == "SQL")
        callWebServiceModifikoDokumentaNeTabeleTemporale();
}

//Bej update gjithe datasourcet origjinale te grides (datasourcet ruhen ne opsionet e grides kryesore "gridImporti.Options.customData.dataSource")
function updateOriginalDataSource() {
    var uFshiKoka = false;
    if (pageState.dataSourceReceptura) {
        pageState.objectRowsFshi.trupiFshi = pageState.objectRowsFshi.trupiFshi.concat(pageState.dataSourceTrupi.filter(function (row) { return pageState.dataSourceReceptura["ds_" + row.IDIMPORTTRUPISHITJE].length <= 0; }));
        pageState.dataSourceTrupi = pageState.dataSourceTrupi.filter(function (row) { return pageState.dataSourceReceptura["ds_" + row.IDIMPORTTRUPISHITJE].length > 0; });
    }

    if (pageState.dataSourceTrupi && pageState.dataSourceTrupi.length <= 0) {
        pageState.gridImporti.DeleteRow(pageState.updatedIndex);
        pageState.gridImporti.Options.customData.dataSource.koka = pageState.gridImporti.Options.customData.dataSource.koka.filter(function (row) { return row[pageState.keyExpr] != pageState.updatedId; });
        uFshiKoka = true;
    }

    var dsTrupi = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.trupi);
    if (dsTrupi) {
        var dsTrupiTemp = dsTrupi.filter(function (row) { return row[pageState.keyExpr] != pageState.updatedId; });
        dsTrupiTemp = dsTrupiTemp.concat(pageState.dataSourceTrupi);
        pageState.gridImporti.Options.customData.dataSource.trupi = dsTrupiTemp;
    }

    var dsReceptura = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.receptura);
    if (dsReceptura) {
        var dsRecepturaTemp = dsReceptura.filter(function (row) { return row[pageState.keyExpr] != pageState.updatedId; });
        dsRecepturaTemp = dsRecepturaTemp.concat(merrArrayNgaObjektDsReceptura(pageState.dataSourceReceptura));
        pageState.gridImporti.Options.customData.dataSource.receptura = dsRecepturaTemp;
    }
    return uFshiKoka;
}

//Therret webservicen qe ben modifikimin e te dhenave qe jane bere update, ne databaze ne tabelat temporale
function callWebServiceModifikoDokumentaNeTabeleTemporale() {
    if ((txtEmerTabKoka.GetText() != "" && txtEmerTabKokaHistorik.GetText() == "") || 
        (txtEmerTabTrupi.GetText() != "" && txtEmerTabTrupiHistorik.GetText() == "") || 
        (txtEmerTabRec.GetText() != "" && txtEmerTabRecHistorik.GetText() == "")) {
        myMesazh.ShtoMesazhGabimi("Nuk ekzistojne tabelat e historikut! Ju lutem riruani templaten e importit dhe ringarkoni griden!");
        return;
    }

    var koka = pageState.gridImporti.Options.customData.dataSource.koka.filter(function (row) { return row[pageState.keyExpr] == pageState.updatedId; });
    var trupi = pageState.gridImporti.Options.customData.dataSource.trupi ? pageState.gridImporti.Options.customData.dataSource.trupi.filter(function (row) { return row[pageState.keyExpr] == pageState.updatedId; }) : new Array();
    var receptura = pageState.gridImporti.Options.customData.dataSource.receptura ? pageState.gridImporti.Options.customData.dataSource.receptura.filter(function (row) { return row[pageState.keyExpr] == pageState.updatedId; }) : new Array();
    var objKokaDheRecepturaModifikim = { koka: koka, receptura: receptura };

    $.ajax({
        url: Utils.getServerApiUrl("Importi", "ModifikoDokumentaNeTabeleTemporale"),
        data: JSON.stringify({
            objRowsKokaDheRecepturaModifikim: objKokaDheRecepturaModifikim,
            objRowsTrupiModifikim: trupi,
            objectRowsFshi: pageState.objectRowsFshi,
            formati: cmbFormati.GetValue(),
            idKategoria: cmbKategoria.GetValue(),
            emerTabeleKoke: txtEmerTabKoka.GetText(),
            emerTabeleTrupi: txtEmerTabTrupi.GetText(),
            emerTabeleRec: txtEmerTabRec.GetText()
        })
    }).done(function (result) {
        if (result.Status)
            myMesazh.ShtoMesazhSuksesi("Modifikimi/Fshirja u krye me sukses!");
        else
            myMesazh.ShtoMesazhGabimi("Modifikimi/Fshirja deshtoi! Ju lutem ringarkoni griden.");
    });

    pageState.objectRowsFshi.kokaFshi = new Array();
    pageState.objectRowsFshi.trupiFshi = new Array();
    pageState.objectRowsFshi.recepturaFshi = new Array();
}

//Anulloj gjithe ndryshimet e bera ne edit mode ne forme
function anulloTeDhenaNeSourceGride(grida) {
    pageState.gridImporti.Grida.cancelEditData();
    if (pageState.gridImporti.Grida.option("masterDetail").enabled)
        pageState.gridImporti.Grida.expandRow(pageState.gridImporti.Grida.getKeyByRowIndex(pageState.updatedIndex));

    pageState.objectRowsFshi.kokaFshi = new Array();
    pageState.objectRowsFshi.trupiFshi = new Array();
    pageState.objectRowsFshi.recepturaFshi = new Array();
}

//Vendos ne page state te dhenat per rreshtin qe po modifikoj ne griden kryesore
function vendosTeDhenaRreshtiNePageState(rowInfo) {
    var dsTrupi = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.trupi);
    var dsReceptura = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.receptura);

    if (dsTrupi)
        pageState.dataSourceTrupi = dsTrupi.filter(function (row) { return row[pageState.keyExpr] == rowInfo.data[pageState.keyExpr]; });
    if (dsReceptura) {
        var recepturaDok = dsReceptura.filter(function (row) { return row[pageState.keyExpr] == rowInfo.data[pageState.keyExpr]; });
        pageState.dataSourceReceptura = merrObjektDsReceptura(recepturaDok);
    }

    pageState.isUpdateCanceled = false;
    pageState.updatedId = rowInfo.data[pageState.keyExpr];
    pageState.updatedIndex = pageState.gridImporti.Grida.getRowIndexByKey(rowInfo.key);
}

//Fshij gjithe te dhenat e lidhura me rreshtin qe fshihet nga grida kryesore e importit
function fshiRreshtNgaGridaImportit(rowInfo) {
    pageState.objectRowsFshi.kokaFshi.push(rowInfo.data);

    var dsTrupi = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.trupi);

    pageState.gridImporti.Options.customData.dataSource.koka = pageState.gridImporti.Options.customData.dataSource.koka.filter(function (row) { return row[pageState.keyExpr] != rowInfo.data[pageState.keyExpr]; });

    if (dsTrupi) {
        var dsTrupiTemp = dsTrupi.filter(function (row) { return row[pageState.keyExpr] != rowInfo.data[pageState.keyExpr]; });

        pageState.objectRowsFshi.trupiFshi = pageState.objectRowsFshi.trupiFshi.concat(dsTrupi.filter(function (row) { return row[pageState.keyExpr] == rowInfo.data[pageState.keyExpr]; }));
        pageState.gridImporti.Options.customData.dataSource.trupi = dsTrupiTemp;
    }
    fshiRecepturaNgaGridaTrupiImporti(rowInfo, pageState.keyExpr);

    if(rbTipi.GetValue() == "SQL")
        callWebServiceModifikoDokumentaNeTabeleTemporale();
}

//Krijoj nje template per griden e trupit dhe ia bashkangjis rreshtave te grides kryesore te importit
function krijoTemplatePerMasterDetailTrupi(container, options, columnsKonfig) {
    var dsTrupi = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.trupi);
    var dataSource = dsTrupi.filter(function (row) { return row[pageState.keyExpr] == options.data[pageState.keyExpr]; });
    var gridaMasterDetail = krijoTemplateGrideMasterDetail(container, columnsKonfig.fushaTrupi, "IDIMPORTTRUPISHITJE", dataSource, false, undefined, "Trupi i dokumentit", false);

    //nese importi permban dhe receptura, shtojme griden e recepturave brenda rreshtave te trupit
    if (columnsKonfig.fushaRec.length > 0) {
        var recepturaMasterDetail = {
            enabled: true,
            template: function (container, options) { krijoTemplatePerMasterDetailReceptura(container, options, columnsKonfig, false); }
        };
        gridaMasterDetail.Grida.option('masterDetail', recepturaMasterDetail);
        gridaMasterDetail.Grida.option('onToolbarPreparing', shtoButonExpandAll);
    }

    gridaMasterDetail.GridWidget.appendTo(container);
}

//Krijoj nje template per griden e recepturave dhe ia bashkangjis rreshtave te grides se trupit
function krijoTemplatePerMasterDetailReceptura(container, options, columnsKonfig, allowEditing) {
    var dsRec = allowEditing ? pageState.dataSourceReceptura : Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.receptura);
    var dataSource = allowEditing ? dsRec["ds_" + options.data.IDIMPORTTRUPISHITJE] : dsRec.filter(function (row) { return row.IDIMPORTTRUPISHITJE == options.data.IDIMPORTTRUPISHITJE; });
    var gridaMasterDetail = krijoTemplateGrideMasterDetail(container, columnsKonfig.fushaRec, "IDIMPORTRECEPTURA", dataSource, allowEditing, "cell", "Recepturat", true);
    gridaMasterDetail.GridWidget.appendTo(container);
}

//Krijoj nje objekt gride sipas rastit (trup apo recepture), 
//dhe ia bashkangjis si master detail rreshtave te grides kryesore kur eshte trup, 
//dhe rreshtave te trupit kur eshte recepture
function krijoTemplateGrideMasterDetail(container, columns, keyExpr, dataSource, allowUpdating, editingMode, masterCaption, isRecepturat) {
    $("<div>")
        .addClass("master-detail-caption")
        .text(masterCaption)
        .appendTo(container);
    var gridContainer = $("<div " + (allowUpdating ? "class='gridaRecepturaDiv'" : "") + ">");
    var masterDetailGrid = new myDxDataGrid(gridContainer, {
        dataSource: dataSource,
        keyExpr: keyExpr,
        columnAutoWidth: true,
        searchPanel: { visible: false },
        showRowLines: true,
        addDeleteRowCommand: allowUpdating,
        deleteMethod: DeleteRow,
        formatoVleraNumerike: false,
        scrolling: { useNative: true },
        editing: {
            allowUpdating: allowUpdating,
            mode: editingMode,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        onRowExpanded: function (e) { e.element.find(".dx-virtual-row").hide(); },
        onRowPrepared: vendosStileRreshti,
        onRowUpdating: function (options) {
            if (keyExpr == "IDIMPORTRECEPTURA") {
                options.newData = $.extend({}, options.oldData, options.newData);
                var arrayDsReceptura = merrArrayNgaObjektDsReceptura(pageState.dataSourceReceptura);
                for (var i = 0; i < arrayDsReceptura.length; i++) {
                    if (arrayDsReceptura[i].IDIMPORTRECEPTURA == options.oldData.IDIMPORTRECEPTURA)
                        arrayDsReceptura[i] = $.extend({}, arrayDsReceptura[i], options.newData);
                }
                pageState.dataSourceReceptura = merrObjektDsReceptura(arrayDsReceptura);
            }
            pageState.editedIMPORTTRUPISHITJE = options.newData.IDIMPORTTRUPISHITJE;
        },
        onRowUpdated: function (rowInfo) {
            if (keyExpr == "IDIMPORTTRUPISHITJE")
                pageState.dataSourceTrupi = rowInfo.component.option("dataSource");
        },
        onRowRemoved: function (rowInfo) {
            if (keyExpr == "IDIMPORTRECEPTURA")
                pageState.objectRowsFshi.recepturaFshi.push(rowInfo.data);
        }
    }, true);
    masterDetailGrid.SetColumnsFromConfig(columns);
    return masterDetailGrid;
}

//Funksion qe krijon nje template per griden e trupit e cila shfaqet kur grida e importit eshte ne edit mode
function krijoTemplateGrideTrupi(data, element, columnsKonfig) {
    var gridContainer = $("<div id='gridaTrupiDiv'>");
    var gridaTrupImporti = new myDxDataGrid(gridContainer, {
        dataSource: pageState.dataSourceTrupi,
        keyExpr: "IDIMPORTTRUPISHITJE",
        addDeleteRowCommand: true,
        deleteMethod: DeleteRow,
        columnAutoWidth: true,
        showRowLines: true,
        searchPanel: { visible: false },
        formatoVleraNumerike: false,
        scrolling: { useNative: true },
        editing: {
            mode: "batch",
            allowUpdating: true,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        onRowPrepared: vendosStileRreshti, //vendosim stil te alternuar te rreshtave dhe sipas gabimeve
        onContentReady: function (e) { e.component.element().find(".dx-virtual-row").hide(); }, //fsheh nje rresht bosh qe del kur ngarkohet grida
        onRowUpdated:   function (rowInfo) { pageState.dataSourceTrupi = rowInfo.component.option("dataSource"); }, //kur behen update vlerat ne griden e trupit, ruhen ne pageState
        onRowExpanded:  function (e) { e.element.find(".dx-virtual-row").hide(); },             //fsheh nje rresht bosh qe del kur behet expand grida
        onRowRemoved:   function (rowInfo) {
            fshiRecepturaNgaGridaTrupiImporti(rowInfo, "IDIMPORTTRUPISHITJE"); //ne momentin qe fshihet nje rresht nga trupi i dokumentit, duhet te fshihen nga datasource dhe recepturat
            pageState.objectRowsFshi.trupiFshi.push(rowInfo.data); //vendosim ne pagestate rreshtat e fshire
        },
        onToolbarPreparing: function (e) { e.toolbarOptions.items = new Array(); } //fshehim butonat default ruaj te grides kur eshte ne batch edit
    }, true);

    //nese importi permban dhe receptura, shtojme griden e recepturave brenda rreshtave te trupit
    if (columnsKonfig.fushaRec.length > 0) {
        gridaTrupImporti.Grida.option('masterDetail', {
            enabled: (columnsKonfig.fushaRec.length > 0),
            template: function (container, options) { krijoTemplatePerMasterDetailReceptura(container, options, columnsKonfig, true); }
        });
        gridaTrupImporti.Grida.option('onToolbarPreparing', shtoButonExpandAll);
    }

    gridaTrupImporti.SetColumnsFromConfig(columnsKonfig.fushaTrupi);
    gridaTrupImporti.GridWidget.appendTo(element); //i bashkangjis edit formes griden e trupit
    $("#gridaTrupiDiv").width($("#gridaTrupiDiv").parent().width());
}

//Merr gjithe recepturat e dokumentit duke i ndare ne array te vecanta sipas rreshtave te trupit
function merrObjektDsReceptura(recepturaDok) {
    var objektDsReceptura = {};
    for (var i = 0; i < recepturaDok.length; i++) {
        if (!objektDsReceptura["ds_" + recepturaDok[i].IDIMPORTTRUPISHITJE])
            objektDsReceptura["ds_" + recepturaDok[i].IDIMPORTTRUPISHITJE] = new Array();
        objektDsReceptura["ds_" + recepturaDok[i].IDIMPORTTRUPISHITJE].push(recepturaDok[i]);
    }
    return objektDsReceptura;
}

//Merr si array gjithe recepturat e dokumentit qe kane qene ruajtur te ndara sipas rreshtave te trupit, ne nje array te vetem
function merrArrayNgaObjektDsReceptura(dsReceptura) {
    var tempArray = Object.keys(dsReceptura).map(function (key) {
        return dsReceptura[key];
    });
    var arrayDsReceptura = new Array();
    for (var i = 0; i < tempArray.length; i++) {
        arrayDsReceptura = arrayDsReceptura.concat(tempArray[i]);
    }
    return arrayDsReceptura;
}

//Fshin recepturat nga dataSource i grides (ne momentin qe fshihet nje rresht nga grida e trupit ose nga grida e recepturave)
function fshiRecepturaNgaGridaTrupiImporti(rowInfo, key) {
    var dsReceptura = Utils.CloneObject(pageState.gridImporti.Options.customData.dataSource.receptura);
    if (dsReceptura) {
        pageState.objectRowsFshi.recepturaFshi = pageState.objectRowsFshi.recepturaFshi.concat(dsReceptura.filter(function (row) { return row[key] == rowInfo.data[key]; }));
        var dsRecepturaTemp = dsReceptura.filter(function (row) { return row[key] != rowInfo.data[key]; });
        if (pageState.dataSourceReceptura)
            delete pageState.dataSourceReceptura["ds_" + rowInfo.data[key]];
        pageState.gridImporti.Options.customData.dataSource.receptura = dsRecepturaTemp;
    }
}

//Therritet webservice qe ben kontrollin/importin e te dhenave te grides
function kontrolloImportoTeDhenaGridImporti(importo) {
    $.ajax({
        url: Utils.getServerApiUrl("Importi", "KontrolloImportoTeDhenaGridImporti"),
        data: JSON.stringify({
            rreshtaImporti:  merrDataSourceGridePerImport(),
            idKonfig: cmbEmer.GetValue() ? cmbEmer.GetValue() : 0,
            idKategoria: cmbKategoria.GetValue(),
            kategoria: cmbKategoria.GetText(),
            formati: cmbFormati.GetValue(),
            lloji: rbTipi.GetValue(),
            transferoFatura: cbTransferoFatura.GetChecked(),
            tePaImportuara: cbTePaImportuara.GetChecked(),
            emerTabeleKoke: txtEmerTabKoka.GetText(),
            emerTabeleTrupi: txtEmerTabTrupi.GetText(),
            emerTabeleRec: txtEmerTabRec.GetText(),
            mbishkruajVleratEMeparshme: $("#hfMbishkruajVleraImporti").val(),
            permbledhese: cbPermbledhese.GetChecked(),
            importo: importo
        })
    }).done(function (result) {
        kontrolloImportoTeDhenaGridImportiDone(result);
    });
}
var filteredValues;
function merrFilteredRows() {
    const filterExpr = pageState.gridImporti.Grida.getCombinedFilter(true);
    pageState.gridImporti.Grida
        .getDataSource()
        .store()
        .load({ filter: filterExpr })
        .done(function(values){
            filteredValues = values;
        });

}
//Kthen gjithe datasourcen e grides rresht per rresht sipas nivelit me te ulet (receptura, trupi dhe koka vendosen ne nje rresht)
function merrDataSourceGridePerImport() {
    merrFilteredRows();
    var ds = pageState.gridImporti.Options.customData.dataSource;
    ds.koka = filteredValues;
    var obj = new Array();
    var vazhdoReceptura = false;
    if (ds.receptura && ds.receptura.length > 0) {
        for (var i = 0; i < ds.receptura.length; i++) {
            var receptura = ds.receptura[i];
            for (j = 0; j < ds.trupi.length; j++) {
                var trupi = ds.trupi[j];
                if (trupi.IDIMPORTTRUPISHITJE == receptura.IDIMPORTTRUPISHITJE) {
                    receptura = $.extend({}, receptura, trupi);
                    for (var h = 0; h < ds.koka.length; h++) {
                        var koka = ds.koka[h];
                        if (koka[pageState.keyExpr] == trupi[pageState.keyExpr]) {
                            receptura = $.extend({}, receptura, koka);
                            vazhdoReceptura = true;

                        }
                    }
                }
            }
            if (vazhdoReceptura == true) {
                obj.push(receptura);
                vazhdoReceptura = false;
            }
        }
        return obj;
    }
    var vazhdoTrupi = false;
    if (ds.trupi && ds.trupi.length > 0) {
        for (j = 0; j < ds.trupi.length; j++) {
            var trupi = ds.trupi[j];
            for (var h = 0; h < ds.koka.length; h++) {
                var koka = ds.koka[h];
                if (koka[pageState.keyExpr] == trupi[pageState.keyExpr]) {
                    trupi = $.extend({}, trupi, koka);
                    vazhdoTrupi = true;
                }
                
            }
            if (vazhdoTrupi) {
                obj.push(trupi);
                vazhdoTrupi = false;
            }
        }
        return obj;
    }

    return ds.koka;
}

//Therritet ne momentin qe kthen pergjigje webservice qe kontrollon/importon te dhenat e grides
function kontrolloImportoTeDhenaGridImportiDone(result) {
    if (!result.mesazh.Status) {
        myMesazh.ShtoMesazhGabimi(result.mesazh.PershkrimMesazhi);

        var hf = document.getElementById("status1");
        hf.value = result.status1;
        $("#hfKaVleraTeImportuara").val(result.kaVleraTeImportuara);

        evidentoRreshtaJoOk(result.rreshtaJoOk, result.status1);
        return;
    }

    if (result.status1 == "import") {
        pageState.gridImporti.Options.customData.dataSource.koka = new Array();
        pageState.gridImporti.Options.customData.dataSource.trupi = new Array();
        pageState.gridImporti.Options.customData.dataSource.receptura = new Array();
        pageState.gridImporti.Grida.option("dataSource", pageState.gridImporti.Options.customData.dataSource.koka);
        pageState.gridImporti.Refresh();
        pageState.imported = true;
    }
    else
        hiqGabimeNgaSourceGride();

    myMesazh.ShtoMesazhSuksesi(result.mesazh.PershkrimMesazhi);
}

//Evidentoj rreshtat qe kane patur gabime gjate kontrolli ose importit dhe i caktoj nese kane patur gabime ose jo, ne datasourcet origjinale te grides.
function evidentoRreshtaJoOk(rreshtaJoOk, status) {
    //kur kryhet importi, nuk eshte e nevojshme te ngjyrosim rreshtat, po mjafton te shfaqim rreshtat e paimportuar
    if (status == "import") { 
        pageState.gridImporti.Options.customData.dataSource.koka = rreshtaJoOk.koka;
        pageState.gridImporti.Options.customData.dataSource.trupi = rreshtaJoOk.trupi;
        pageState.gridImporti.Options.customData.dataSource.receptura = rreshtaJoOk.receptura;
        pageState.gridImporti.Grida.option("dataSource", pageState.gridImporti.Options.customData.dataSource.koka);
        pageState.gridImporti.Refresh();
        return;
    }

    var dsGridaImporti = pageState.gridImporti.GetData();
    if (rreshtaJoOk && rreshtaJoOk.koka && rreshtaJoOk.koka.length > 0)
        dsGridaImporti.map(function (item) { item.MeGabime = (rreshtaJoOk.koka.filter(function (row) { return row[pageState.keyExpr] == item[pageState.keyExpr]; }).length > 0); });


    var dsGridaTrupi = pageState.gridImporti.Options.customData.dataSource.trupi;
    if (rreshtaJoOk && rreshtaJoOk.trupi && rreshtaJoOk.trupi.length > 0)
        dsGridaTrupi.map(function (item) { item.MeGabime = (rreshtaJoOk.trupi.filter(function (row) { return row.IDIMPORTTRUPISHITJE == item.IDIMPORTTRUPISHITJE; }).length > 0); });

    var dsGridaReceptura = pageState.gridImporti.Options.customData.dataSource.receptura;
    if (rreshtaJoOk && rreshtaJoOk.receptura && rreshtaJoOk.receptura.length > 0)
        dsGridaReceptura.map(function (item) { item.MeGabime = (rreshtaJoOk.receptura.filter(function (row) { return row.IDIMPORTRECEPTURA == item.IDIMPORTRECEPTURA; }).length > 0); });

    pageState.gridImporti.Refresh();
}

//Heq gabimet nga rreshtat e grides nese mesazhi i kontrollit eshte i suksesshem
function hiqGabimeNgaSourceGride() {
    var dsGridaImporti = pageState.gridImporti.GetData();
    if (dsGridaImporti && dsGridaImporti.length > 0)
        dsGridaImporti.map(function (item) { item.MeGabime = false; });

    var dsGridaTrupi = pageState.gridImporti.Options.customData.dataSource.trupi;
    if (dsGridaTrupi && dsGridaTrupi.length > 0)
        dsGridaTrupi.map(function (item) { item.MeGabime = false; });

    var dsGridaReceptura = pageState.gridImporti.Options.customData.dataSource.receptura;
    if (dsGridaReceptura && dsGridaReceptura.length > 0)
        dsGridaReceptura.map(function (item) { item.MeGabime = false; });

    pageState.gridImporti.Refresh();
}

//Shtoj ne griden e elementit "e" butonin expand/collapse all, per te hapur/mbyllur rreshtat e grides
function shtoButonExpandAll(e) {
    var dataGrid = e.component;

    e.toolbarOptions.items.unshift({
        location: "before",
        widget: "dxButton",
        options: {
            text: "Expand All",
            width: 136,
            onClick: function (e) {
                var expanding = e.component.option("text") === "Expand All";
                dataGrid.option("masterDetail.autoExpandAll", expanding);
                e.component.option("text", expanding ? "Collapse All" : "Expand All");
            }
        }
    });
}

//Mbivendos eventet e klikimit default te Save dhe Cancel te edit form
function shtoEventsNeRuajDheAnullo(e) {
    setTimeout(function () {
        var form = e.element;
        var btnAnullo = form.parent().find("[aria-label='Anullo']");
        var btnRuaj = form.parent().find("[aria-label='Ruaj']");
        btnRuaj.parent().css("margin-right","50%");
        btnRuaj.dxButton("instance").option("onClick", function () {
            ruajTeDhenaRreshtiNgaForma();
        });
        btnAnullo.dxButton("instance").option("onClick", function () {
            anulloTeDhenaNeSourceGride(e.component);
        });
    }, 0);
}

//Shtoj si master detail te grides kryesore te importit, griden e trupit
function shtoMasterDetailKoka(columnsKonfig, fushaTrupiLength) {    
    if (fushaTrupiLength > 0) {
        var trupiMasterDetail = {
            enabled: true,
            template: function (container, options) { krijoTemplatePerMasterDetailTrupi(container, options, columnsKonfig); }
        };
        pageState.gridImporti.Grida.option('masterDetail', trupiMasterDetail);
        pageState.gridImporti.Grida.option('onToolbarPreparing', shtoButonExpandAll);
    }
}

//Shtoj griden e trupit ne edit form te grides kryesore te importit
function shtoGrideTrupiNeEditForm(columnsKonfig, fushaTrupiLength) {
    if (fushaTrupiLength > 0) {
        pageState.gridImporti.Grida.option('editing.form.items').push({
            colCount: 4,
            colSpan: 4,
            itemType: "group",
            caption: "Trupi i dokumentit",
            template: function (data, element) { krijoTemplateGrideTrupi(data, element, columnsKonfig); }
        });
    }
}

//Vendos ngjyra te alternuara ne rreshtat e grides dhe nese ka gabime vendos te kuqe
function vendosStileRreshti(row) {
    if (row.rowType == 'data') {
        if (row.key % 2 == 1)
            row.rowElement.css("background", row.data.MeGabime ? "#ffe6e6" : "#ffffff");
        else
            row.rowElement.css("background", row.data.MeGabime ? "#ffcccc" : "#f5f5f5");
    }
}

function KontrolloNrDokumentash(s, e) {
    if (isNaN(s.GetText()) || s.GetText() <= 0) {
        s.SetValue(null);
    }
}
function PastroTabelatTemporare(s, e) {
    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: "Jeni i sigurt per te fshire te gjithe te dhenat e tabelave te importit?",
        modal: true,
        idGjuha: hfState.Get("idGjuha"),
        okClick: fshiTabelaTemporare,
        cancelClick: JoClick
    });
}
function fshiTabelaTemporare() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Importi", "PastroTabelatTemporare"),
        data: JSON.stringify({ tabelaKoka: txtEmerTabKoka.GetText(), tabelaTrupi: txtEmerTabTrupi.GetText(), tabelaKokaHistorik: txtEmerTabKokaHistorik.GetText(), tabelaTrupiHistorik: txtEmerTabTrupiHistorik.GetText() })
    }).done(SucceededCallbackPastroTabelatTemporare);
}
function SucceededCallbackPastroTabelatTemporare() {
    myMesazh.ShtoMesazhSuksesi("Tabelat u pastruan me sukses!");
}