; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
//
//perdoren per hapjen e lupes se klientit
var widthLupaKlient = 850;
var heightLupaKlient = 550;
var identikuesPerPopupKlientFurnitori;
var identifikuesPerPopupKodifikimin;
var multiselect = false;

var llojiPolitikes = undefined;
var isvalidLimite = false;
var key = undefined;

var grideLimiti;

$(document).ready(function () {
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
    changeName();

});

function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Kartat, "2017", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    if (!hfState.Get("celjeShpejte")) {
        indexModifiko = index;
        lista = true;
        mbushfusha();
    }
    else {
        indexModifiko = ASPxGridView_Kartat.GetFocusedRowIndex();
        if (indexModifiko == -1)
            myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje karte!');
        else
            ASPxGridView_Kartat.GetRowValues(indexModifiko, 'IdKarta;Kodi;Emri;Targa;Shoferi', OnGridSelectionComplete);
    }
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Kartat, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Kartat, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Kartat, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Kartat, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    switch (e.item.name) {
        case "Ruaj":
            grideLimiti.SaveCurrentValues();
            var data = grideLimiti.GetData();
            if (!IsValidGrideLimiti(data)) {
                e.processOnServer = false;
                PageControl.SetActiveTabIndex(2);
                myMesazh.ShtoMesazhGabimi("Ka gabime ne gride!");
                return;
            }
            hfState.Set("limitet", JSON.stringify(data));
            break;
        case "OK":
            e.processOnServer = false;
            indexModifiko = ASPxGridView_Kartat.GetFocusedRowIndex();
            if (indexModifiko == -1)
                myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje karte!');
            else
                ASPxGridView_Kartat.GetRowValues(indexModifiko, 'IdKarta;Kodi;Emri;Targa;Shoferi', OnGridSelectionComplete);
            break;
    }

    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, vendosKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

function OnGridSelectionComplete(values) {
    var kodi = values[1];
    var id = values[0];
    var emri = values[2];
    Utils.SelectComboItem(window.parent.cmbKarta, id, kodi, emri);
    window.parent.txtTarga2.SetText(values[3]);
    window.parent.txtShoferi.SetText(values[4]);
    window.parent.cmbKarta.SetFocus(true);
    window.parent.cmbKarta.RaiseTextChanged();
    window.parent.popupUniversal.Hide();
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = ASPxGridView_Kartat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje karte!');
    textChangedKlienti
    ASPxGridView_Kartat.GetRowValues(indexModifiko, 'IdKarta;Kodi;Emri;Email;Kontakt;Aktiv;Aktiv;IdPolitike;IdKategoriZB;Klienti;Aktiv;Targa;Shoferi;Qyteti;Datelindja;IdQyteti;Adresa;Departamenti;Modeli;Id;IdMenyrePagese', OnGetRowValuesMod); // Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $('#hfId').val(values[0]);
    txtKodi.SetText(values[1]);

    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }

    txtEmri.SetText(values[2]);
    txtEmail.SetValue(values[3]);
    txtKontakt.SetValue(values[4]);
    cbAktiv.SetChecked(values[5]);

    //if (values[9] != null) {
    //    var kf = new Array(values[9], values[10]);
    //    cmbKlienti.SetSelectedIndex(cmbKlienti.AddItem(kf, values[6]));
    //}
    //else
    //    cmbKlienti.SetSelectedIndex(-1);

    if (values[9] != null) {
        cmbKlienti.SetText(values[9]);
    }
    else
        cmbKlienti.SetSelectedIndex(-1);

    if (values[7] !== "" && values[7] !== null) {
        cmbPolitike.SetValue(values[7]);
    }
    else
        cmbPolitike.SetSelectedIndex(-1);

    if (values[8] !== "" && values[8] !== null && values[8] !== 0 && values[8] !== -1)
        cmbKategori.SetValue(values[8]);
    else
        cmbKategori.SetSelectedIndex(-1);

    txtTarga.SetValue(values[11]);
    txtShoferi.SetValue(values[12]);
    dteDitelindja.SetDate(values[14]);

    if (values[15] !== "" && values[15] !== null)
        cmbQyteti.SetValue(values[15]);
    else
        cmbQyteti.SetSelectedIndex(-1);

    txtAdresa.SetText(values[16]);

    txtDepartamenti.SetText(values[17]);
    txtModeli.SetText(values[18]);
    txtId.SetText(values[19]);
    txtMenyrePagese.SetValue(values[20]);

    MerrLimitetSipasKartes(values[0]);

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }

    callWebServicePikeKarte(values[0]);
    enableKategoriZbritje();
}

function MerrLimitetSipasKartes(idKarte) {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "MerrLimitet"),
        data: JSON.stringify({ idKarta: idKarte })
    }).done(MbushGride);
}

function MbushGride(dataSource) {
    grideLimiti.SetDataSource(dataSource);
    Utils.hiqLoadingGif();
}

function callWebServicePikeKarte(idKarte) {
    var idNdermarrje = hfState.Get('_idNdermarrje');
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "kthePikeDheLidhjeKarte"),
        data: JSON.stringify({ idkarta: idKarte, idNdermarrje: idNdermarrje })
    }).done(function (result) { SucceededCallbackPike(result, idKarte) });
}

function SucceededCallbackPike(result, idKarte) {
    if (result == null)
        return;

    if (idKarte != $('#hfId').val())
        return;

    cmbPolitike.SetEnabled(!result.iLidhur);
    txtGjendjePike.SetEnabled(!result.iLidhur);
    txtGjendjePike.SetText(result.pike);

    if (cmbPolitike.GetValue() > 0 && result.pike > 0)
        callWebserviceKategoriDhuratash(cmbPolitike.GetValue());
    else
        txtStatusi.SetText("Ju nuk fitoni dhurate!");
}

function callWebserviceKategoriDhuratash(idPolitike) {
    try {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTrupiPolitikeKarte"),
            data: JSON.stringify({ idPolitike: idPolitike })
        }).done(SucceededCallbacKategoriDhuratash);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

function SucceededCallbacKategoriDhuratash(result) {
    var piketotal = parseInt(txtGjendjePike.GetText());

    if (result != null) {
        cmbDhurata = new Array();
        for (i = 0; i < result.length; i++) {
            if (piketotal < result[0].Pike) {
                txtStatusi.SetText("Ju nuk fitoni dhurate");
                return;
            }

            if (piketotal >= result[i].Pike) {
                cmbDhurata.push("Ju fitoni dhurate: " + result[i].Pike.toString());
            }
        }

        txtStatusi.SetText(cmbDhurata.pop());
    }
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtKodi.SetEnabled(true);
    cmbPolitike.SetEnabled(true);
    txtEmri.SetText('');
    txtEmail.SetText('');
    txtKontakt.SetText('');
    txtTarga.SetText('');
    txtShoferi.SetText('');
    txtGjendjePike.SetText('');
    txtStatusi.SetText('');
    cbAktiv.SetChecked(true);
    cmbPolitike.SetText('');
    cmbKategori.SetValue('');    
    cmbKlienti.SetValue('');
    txtDepartamenti.SetText('');
    txtModeli.SetText('');
    txtId.SetText('');
    txtMenyrePagese.SetValue(-1);
    txtMenyrePagese.SetText('');

    aktivizoFusha(colKontrollet, colAtrTrupi, false);

    if (grideLimiti) {
        grideLimiti.Clean(new DevExpress.data.ArrayStore([Utils.CloneObject(defaultObject)]));
    }

    dteDitelindja.SetDate(new Date());
    cmbQyteti.SetSelectedIndex(-1);
    txtAdresa.SetText('');
    isvalid = false;
    txtKodi.SetEnabled(true);
    cmbPolitike.SetEnabled(true);
}

/* 
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/
function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('_idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('_idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);

    ASPxGridView_Kartat.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvKarta").show();
    var idGjuha = hfState.Get('_idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('_idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    vendosKonfig(result);
}

function vendosKonfig(result) {
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblKarta'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C", 1);

        var columnsKonfigKoka = result.colGrida.filter(function (item) { return item.GridKokaEmri == 'gvLimiti' });
        DevExpress.localization.locale(hfState.Get("_idGjuha") == 0 ? "al" : "en");
        KonfiguroGrideLimiti(columnsKonfigKoka);
    }
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf6 = $("#hfLupaLlog");
    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli == "cmbLlog")
            hf6.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("2017", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("2017", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_KartaKlienti.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/

function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Kartat, "2017", pastrofusha, hfTeDrejta, undefined, resultkonf, true);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

var KPF;

function Klienti_Click() {
    multiselect = $("#hfKushti").val();
    var queryStr = "1";
    identikuesPerPopupKlientFurnitori = "ShtoKartaKlienti";
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, "Klient", widthLupaKlient, heightLupaKlient);
}

function textChangedKlienti(s, e) {
    if (isNaN(cmbKlienti.GetValue())) {
        btneKlienti.SetText('');
        btneKlienti.Focus();
        return;
    }
}

function Kategori_Click() {
    var queryStr = "1";
    identifikuesPerPopupKodifikimin = "ShtoKartaKlienti";
    myButtonClickLupa.KategoriZbritje_Click(hfState.Get("headerZgjidhKategoriZbritje"), queryStr, widthLupaKlient, heightLupaKlient);
}

function textChangedKategoria(s, e) {
    if (isNaN(cmbKategori.GetValue())) {
        cmbKategori.SetText('');
        cmbKategori.Focus();
        return;
    }
}
function textChangedPolitika(s, e) {
    if (cmbPolitike.GetSelectedItem() != null) {
        enableKategoriZbritje();
    }
}

function enableKategoriZbritje() {
    var politika = cmbPolitike.GetText();
    if (politika != "") {
        var str = politika.split(';');
        llojiPolitikes = str.length > 1 ? str[1].trim() : undefined;
        if (llojiPolitikes == 'me pike') {
            cmbKategori.SetValue('');
            cmbKategori.SetEnabled(false);
            if (grideLimiti) {
                var data = grideLimiti.GetData();
                for (var i = 0; i < data.length; i++) {
                    data[i].IdKokaKategoriZbritje = 0;
                    data[i].ZbritjaAnalitike = null;
                }
                grideLimiti.Refresh();
            }
        }
        else
            cmbKategori.SetEnabled(true);
    }
}


function activeTabsChanged(s, e) {
    indexModifiko = ASPxGridView_Kartat.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            mbushfusha();
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);
            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

//Limitet



var defaultObject;

function IsValidGrideLimiti(data) {
    for (var i = 0; i < data.length; i++) {
        if (data[i].IdArtikull === 0 &&
            (data[i].LimitSasi !== 0 ||
                data[i].LimitVlere !== 0 ||
                data[i].IdNivelZbritje !== 0 ||
            data[i].DtFillimi !== new Date(new Date().setHours(12, 0, 0, 0)).toISOString() ||
            data[i].DtMbarimi !== new Date(new Date("12/31/9999").setHours(12, 0, 0, 0)).toISOString()
            )) {
            return false;
        }
        if (data[i].IdArtikull !== 0 &&
            ((data[i].LimitSasi === 0 && data[i].LimitVlere === 0) ||
                (data[i].LimitSasi !== 0 && data[i].LimitVlere !== 0) ||
                data[i].DtFillimi === null ||
                data[i].DtMbarimi === null
            )) {
            return false;
        }
    }
    return true;
}

function KonfiguroGrideLimiti(columnsKonfig) {
    defaultObject = { IdLimiti: 0, IdKarta: 0, Lloji: 0, IdArtikull: 0, Kodi: null, IdKodifikimi1: 0, LimitSasi: 0, LimitVlere: 0, IdNivelZbritje: 0, ZbritjaAnalitike: null, DtFillimi: new Date(new Date().setHours(12, 0, 0, 0)), DtMbarimi: new Date(new Date("12/31/9999").setHours(12, 0, 0, 0)) }
    var dataSource = hfState.Get("Limitet");
    var ds = dataSource ? dataSource : [Utils.CloneObject(defaultObject)];

    grideLimiti = new myDxDataGrid("dxDataGrid_Limiti", {
        dataSource: ds,
        focusStateEnabled: false,
        editing: {
            mode: "cell",
            allowUpdating: true,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        searchPanel: { visible: false },
        showRowLines: true,
        sorting: { mode: "none" },
        addDeleteRowCommand: true,
        onRowRemoving: OnRowRemoving,
        onRowUpdated: OnRowUpdated,
        onEditingStart: OnEditingStart,
        onCellClick: OnCellClick,
        onContentReady: function (e) {
            grideLimiti.ShtoRreshtBosh(defaultObject, "Kodi");
            grideLimiti.SetLastFocusedCell();
        }
    });


    grideLimiti.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides

    grideLimiti.AddCustomOptionToColumns(["DtFillimi", "DtMbarimi"], "dataType", "date");
    grideLimiti.AddCustomOptionToColumns(["DtFillimi", "DtMbarimi"], "format", "dd/MM/yyyy");
    grideLimiti.AddCustomOptionToColumns(["DtFillimi"], "setCellValue", OnSetCellValueDtFillimi);
    grideLimiti.AddCustomOptionToColumns(["DtMbarimi"], "setCellValue", OnSetCellValueDtMbarimi);
    grideLimiti.AddDataSourceToColumn("Lloji", $.parseJSON(hfState.Get("lloji")), "Lloji", "Label", false);
    grideLimiti.AddCustomOptionToColumns(["Lloji"], "setCellValue", OnSetCellValueLloji);
    grideLimiti.AddAutocompleteToColumn("Kodi", "IdArtikull", "IdKodifikimi1", MerrArtikullOseGrupim, undefined, "Id", "Kodi", "IdKodifikimi", HapLupeArtikulli, OnValueChangedKodi);
    grideLimiti.AddAutocompleteToColumn("ZbritjaAnalitike", "IdNivelZbritje", undefined, MerrKategoriZbritje, undefined, "Id", "Kodi", undefined, HapLupeNivelZbritje);
}

function OnSetCellValueDtFillimi(newData, value, currentRowData) {
    value.setHours(value.getHours() + 2)
    value = value.toISOString();
    if (currentRowData.IdArtikull != 0) {
        var data = grideLimiti.GetData();
        for (var i = 0; i < data.length; i++) {
            if (rowIndex != i
                && (data[i].IdArtikull == currentRowData.IdArtikull || data[i].IdKodifikimi1 == currentRowData.IdArtikull)
                && (value >= data[i].DtFillimi && value <= data[i].DtMbarimi)) {
                newData.DtFillimi = null;
                grideLimiti.Refresh();
                myMesazh.ShtoMesazhGabimi(hfState.Get("KartaKlienti.EkzistonArtikullGrupArtikull"));
                return;
            }
        }
    }
    newData.DtFillimi = value;
    grideLimiti.Refresh();
}

function OnSetCellValueDtMbarimi(newData, value, currentRowData) {
    value.setHours(value.getHours() + 2)
    value = value.toISOString();
    if (currentRowData.IdArtikull != 0) {
        var data = grideLimiti.GetData();
        for (var i = 0; i < data.length; i++) {
            if (rowIndex != i
                && (data[i].IdArtikull == currentRowData.IdArtikull || data[i].IdKodifikimi1 == currentRowData.IdArtikull)
                && (value >= data[i].DtFillimi && value <= data[i].DtMbarimi)) {
                newData.DtMbarimi = null;
                grideLimiti.Refresh();
                myMesazh.ShtoMesazhGabimi(hfState.Get("KartaKlienti.EkzistonArtikullGrupArtikull"));
                return;
            }
        }
    }
    newData.DtMbarimi = value;
    grideLimiti.Refresh();
}

function OnSetCellValueLloji(newData, value, currentRowData) {
    if (value != currentRowData.Lloji) {
        newData.Lloji = value;
        newData.IdArtikull = 0;
        newData.IdKodifikimi1 = 0;
        newData.Kodi = null;
        grideLimiti.Refresh();
    }
}

function MerrArtikullOseGrupim(value, rowIndex, perAcList) {
    var deferred = new jQuery.Deferred();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", grideLimiti.GetData()[rowIndex].Lloji == 0 ? "MerrArtikujAutoComplete" : "MerrKodifikim1AutoComplete"),
        data: JSON.stringify({ kodi: value ? value : "" }),
        success: function (result) {
            if (!perAcList) {
                var objekti = result.filter(function (item) { return item.Kodi.toLowerCase() == value.toLowerCase() })[0];
                if (!objekti) return;
                grideLimiti.VendosVleraNeDataSource(rowIndex, "Kodi", "IdArtikull", "IdKodifikimi1", result[0], "Kodi", "Id", "IdKodifikimi");
                grideLimiti.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}

function OnValueChangedKodi(rowIndex, objekti) {
    if (objekti.Id != null) {
        var data = grideLimiti.GetData();
        if (data[rowIndex].IdArtikull != 0) {
            for (var i = 0; i < data.length; i++) {
                if (rowIndex != i
                    && (data[i].IdArtikull == objekti.Id || data[i].IdKodifikimi1 == objekti.Id)
                    && (data[rowIndex].DtFillimi >= data[i].DtFillimi
                        || data[rowIndex].DtFillimi <= data[i].DtMbarimi
                        || data[rowIndex].DtMbarimi >= data[i].DtFillimi
                        || data[rowIndex].DtMbarimi <= data[i].DtMbarimi)) {
                    data[rowIndex].DtFillimi = null;
                    data[rowIndex].DtMbarimi = null;
                    grideLimiti.Refresh();
                    myMesazh.ShtoMesazhGabimi(hfState.Get("KartaKlienti.EkzistonArtikullGrupArtikull"));
                    return;
                }
            }
        }
    }
}

function MerrKategoriZbritje(value, rowIndex, perAcList) {
    var deferred = new jQuery.Deferred();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "MerrNivelZbritjeAutoComplete"),
        data: JSON.stringify({ kodi: value ? value : "" }),
        success: function (result) {
            if (!perAcList) {
                var objekti = result.filter(function (item) { return item.Kodi.toLowerCase() == value.toLowerCase() })[0];
                if (!objekti) return;
                grideLimiti.VendosVleraNeDataSource(rowIndex, "ZbritjaAnalitike", "IdNivelZbritje", undefined, result[0], "Kodi", "Id", undefined);
                grideLimiti.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}

function OnRowUpdated(e) {
    switch (Object.keys(e.data)[0]) {
        case "DtFillimi":
            if (e.data["DtFillimi"] == null) {
                myMesazh.ShtoMesazhGabimi("Data e fillimit nuk duhet te jete bosh!");
            }
            break;
        case "DtMbarimi":
            if (e.data["DtMbarimi"] == null) {
                myMesazh.ShtoMesazhGabimi("Data e mbarimit nuk duhet te jete bosh!");
            }
            break;
        case "LimitSasi":
            if (e.key["LimitSasi"] != 0 && e.key["LimitVlere"] != 0) {
                e.key["LimitVlere"] = 0;
            }
            else if (e.key["LimitSasi"] == 0 && e.key["LimitVlere"] == 0) {
                myMesazh.ShtoMesazhGabimi("Duhet te plotesoni nje nga limitet!");
            }
            break;
        case "LimitVlere":
            if (e.key["LimitSasi"] != 0 && e.key["LimitVlere"] != 0) {
                e.key["LimitSasi"] = 0;
            }
            else if (e.key["LimitSasi"] == 0 && e.key["LimitVlere"] == 0) {
                myMesazh.ShtoMesazhGabimi("Duhet te plotesoni nje nga limitet!");
            }
            break;
        case "Kodi":
            if (e.data["Kodi"] == "") {
                myMesazh.ShtoMesazhGabimi("Kodi i limiti nuk duhet te jete bosh!");
            }
    }
}

function OnRowRemoving(e) {
    if (new Date(e.data.DtFillimi) <= new Date() && e.data.IdLimiti !== undefined && e.data.IdLimiti !== 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("KartaKlienti.LimitiNukMundTeFshihet"));
        e.cancel = true;
    }
}

function OnEditingStart(e) {
    e.component.columnOption("ZbritjaAnalitike", "allowEditing", cmbKategori.GetEnabled());
    if (!e.parentType == "dataRow")
        return;

    if (e.column.dataField === "Kodi" || e.column.dataField === "Lloji" || e.column.dataField === "LimitSasi" || e.column.dataField === "LimitVlere" || e.column.dataField === "ZbritjaAnalitike") {
        e.cancel = (new Date(e.data.DtFillimi) <= new Date() && e.data.IdLimiti !== 0);
    }

    if (e.column.dataField === "DtFillimi") {
        e.cancel = (e.data.DtFillimi != null && new Date(e.data.DtFillimi) <= new Date() && e.data.IdLimiti !== 0);
    }

    if (e.column.dataField === "DtMbarimi") {
        e.cancel = (e.data.DtMbarimi != null && new Date(e.data.DtMbarimi) <= new Date() && e.data.IdLimiti !== 0);
    }
}

function OnCellClick(e) {
    rowIndex = e.rowIndex;
}

var rowIndex;

function HapLupeNivelZbritje(index) {
    var queryStr = "1";
    rowIndex = index;
    identifikuesPerPopupKodifikimin = "ShtoKartaKlientiLupaKategoriZB";
    myButtonClickLupa.NivelZbritje_Click(hfState.Get("headerZgjidhKategoriZbritje"), queryStr, widthLupaKlient, heightLupaKlient);
}

function VendosKategoriZbritje(idKokaKategoriZbritje, kategoriZbritje) {
    var dataSource = grideLimiti.GetData();
    dataSource[rowIndex].IdNivelZbritje = idKokaKategoriZbritje;
    dataSource[rowIndex].ZbritjaAnalitike = kategoriZbritje;
    grideLimiti.Refresh();
    grideLimiti.Grida.closeEditCell();
}

function HapLupeArtikulli(index) {
    rowIndex = index;

    var dataSource = grideLimiti.GetData();
    var queryStr = "1";
    if (dataSource[rowIndex].Lloji == 0) {
        identifikuesPerPopupKodifikimin = "ShtoKartaKlientiLupaArtikull";
        myButtonClickLupa.ButtonClickArtikulli(hfState.Get("headerZgjidhArtikull"), queryStr, widthLupaKlient, heightLupaKlient);

    } else {
        identifikuesPerPopupKodifikimin = "ShtoKartaKlientiLupaGrupimArtikulli";
        var llojartikulli = false;
        var llojKodifikimi = 1;
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + llojKodifikimi + '&llojartikulli=' + llojartikulli + '&idKonfigAmbjente=' + queryStr, widthLupaKlient, heightLupaKlient);
    }
}

function VendosArtikull(idArtikulli, kodArtikulli, kodifikimi1Artikulli) {
    var data = grideLimiti.GetData();

    for (var i = 0; i < data.length; i++) {
        if (rowIndex != i
            && (data[i].IdArtikull == idArtikulli || data[i].IdKodifikimi1 == kodifikimi1Artikulli)
            && (data[rowIndex].DtFillimi >= data[i].DtFillimi
                || data[rowIndex].DtFillimi <= data[i].DtMbarimi
                || data[rowIndex].DtMbarimi >= data[i].DtFillimi
                || data[rowIndex].DtMbarimi <= data[i].DtMbarimi)) {
            data[rowIndex].DtFillimi = null;
            data[rowIndex].DtMbarimi = null;
            grideLimiti.Refresh();
            myMesazh.ShtoMesazhGabimi(hfState.Get("KartaKlienti.EkzistonArtikullGrupArtikull"));
        }
    }

    data[rowIndex].IdArtikull = idArtikulli;
    data[rowIndex].Kodi = kodArtikulli;
    data[rowIndex].IdKodifikimi1 = kodifikimi1Artikulli;
    grideLimiti.Refresh();
    grideLimiti.Grida.closeEditCell();
}

function VendosKodifikim1Artikulli(idKodifikimi, kodKodifikimi) {
    var data = grideLimiti.GetData();

    for (var i = 0; i < data.length; i++) {
        if (rowIndex != i
            && (data[i].IdArtikull == idKodifikimi || data[i].IdKodifikimi1 == idKodifikimi)
            && (data[rowIndex].DtFillimi >= data[i].DtFillimi
                || data[rowIndex].DtFillimi <= data[i].DtMbarimi
                || data[rowIndex].DtMbarimi >= data[i].DtFillimi
                || data[rowIndex].DtMbarimi <= data[i].DtMbarimi)) {
            data[rowIndex].DtFillimi = null;
            data[rowIndex].DtMbarimi = null;
            grideLimiti.Refresh();
            myMesazh.ShtoMesazhGabimi(hfState.Get("KartaKlienti.EkzistonArtikullGrupArtikull"));
        }
    }

    data[rowIndex].IdArtikull = idKodifikimi;
    data[rowIndex].Kodi = kodKodifikimi;
    data[rowIndex].IdKodifikimi1 = idKodifikimi;
    grideLimiti.Refresh();
    grideLimiti.Grida.closeEditCell();
}