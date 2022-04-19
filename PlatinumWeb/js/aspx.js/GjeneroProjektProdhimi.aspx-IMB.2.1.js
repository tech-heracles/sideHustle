;
var lidhur = false;

var widthLupaArtikull = 900;
var heightLupaArtikull = 650;
var widthLupaKodifikim = 600;
var heightLupaKodifikim = 600;

var widthLupaKerko = 900;
var heightLupaKerko = 600;

var editorData;

var identifikuesPerPopupDokumentat;
var identikuesPerPopupArtikulli;
var identifikuesPerPopupMagazina;
var identifikuesPerPopupKodifikimin;

function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('GjeneroProjektProdhimi.aspx',0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

jQuery(document).ready(function () {//po   
    //formGridColsArray();
    //inicializoGride();
    //mbushGrideNgaHiddenFieldet();
    
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }
        if ($('#divgride2').width() != null) {
            myJQGrid.fixGridWidth($('#rowed5'), $('#divgride2'));
        }
    }).trigger('resize');
    $(document).keydown(function (e) {//po
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
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });

    
});

function Init() {//po
    if (typeof (isPostBack) == "undefined") {
        var HfPeriudheObj = window.parent.lexoHfPeriudhe();
        //alert(HfPeriudheObj.fillimiPeriudha);
        dteDtNga.SetDate(new Date(HfPeriudheObj.FillimiPeriudha));
        //    callWebserviceKonfigurimi("807"); //807 = id komponente (Shto_Ekzekutim.aspx)
        //var hf = document.getElementById("hfKonffillestar");
        //cmbKonfigurimi.SetText(hf.value);
        ndryshoKonfigurimin();
        identifikuesPerPopupKodifikimin = "GjeneroProjekt";
        identifikuesPerPopupDokumentat = "GjeneroProjekt";
        identikuesPerPopupKlientFurnitori = "GjeneroProjekt";
        identikuesPerPopupArtikulli = "GjeneroProjekt";
        identifikuesPerPopupMagazina = "GjeneroProjekt";
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);

    }
}

function inicializoGride() {//po
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    var classes = '';
    if (lidhur === true)
        classes = 'uigray';
    var arrayPershkrime = [ arrayPershkrimiKolonaGrides[0],  arrayPershkrimiKolonaGrides[1],  arrayPershkrimiKolonaGrides[2],  arrayPershkrimiKolonaGrides[3],  arrayPershkrimiKolonaGrides[4],
                            arrayPershkrimiKolonaGrides[5],  arrayPershkrimiKolonaGrides[6],  arrayPershkrimiKolonaGrides[7],  arrayPershkrimiKolonaGrides[8],  arrayPershkrimiKolonaGrides[9],
                            arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13], arrayPershkrimiKolonaGrides[14],
                            arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17], arrayPershkrimiKolonaGrides[18], arrayPershkrimiKolonaGrides[19],
                            arrayPershkrimiKolonaGrides[20], arrayPershkrimiKolonaGrides[21], arrayPershkrimiKolonaGrides[22]
                           ];
    var arrayModel = [
            { name: arrayIdKolonaGrides[0],  index: arrayIdKolonaGrides[0],  width: arrayWidthKolonaGrides[0],  hidden: arrayVisibleKolonaGrides[0],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjenero, custom_value: myvalueCheck } },
            { name: arrayIdKolonaGrides[1],  index: arrayIdKolonaGrides[1],  width: arrayWidthKolonaGrides[1],  hidden: arrayVisibleKolonaGrides[1],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKoka, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[2],  index: arrayIdKolonaGrides[2],  width: arrayWidthKolonaGrides[2],  hidden: arrayVisibleKolonaGrides[2],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemNrProjekti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[3],  index: arrayIdKolonaGrides[3],  width: arrayWidthKolonaGrides[3],  hidden: arrayVisibleKolonaGrides[3],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodartikulli, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[4],  index: arrayIdKolonaGrides[4],  width: arrayWidthKolonaGrides[4],  hidden: arrayVisibleKolonaGrides[4],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[5],  index: arrayIdKolonaGrides[5],  width: arrayWidthKolonaGrides[5],  hidden: arrayVisibleKolonaGrides[5],  classes: classes, sortable: false, editable: true, edittype: 'custom',  editoptions: { custom_element: myelemSasiAkt, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[6],  index: arrayIdKolonaGrides[6],  width: arrayWidthKolonaGrides[6],  hidden: arrayVisibleKolonaGrides[6],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemData, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[7],  index: arrayIdKolonaGrides[7],  width: arrayWidthKolonaGrides[7],  hidden: arrayVisibleKolonaGrides[7],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjeresi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[8],  index: arrayIdKolonaGrides[8],  width: arrayWidthKolonaGrides[8],  hidden: arrayVisibleKolonaGrides[8],  classes: classes, sortable: false, editable: true, edittype: 'custom',  editoptions: { custom_element: myelemGjatesi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[9],  index: arrayIdKolonaGrides[9],  width: arrayWidthKolonaGrides[9],  hidden: arrayVisibleKolonaGrides[9],  classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiPor, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiPer, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKodKlienti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemEmerKlienti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemNrUrdher, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdMag, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdNjesi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdArti, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKlienti, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[18], index: arrayIdKolonaGrides[18], width: arrayWidthKolonaGrides[18], hidden: arrayVisibleKolonaGrides[18], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemShenime, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[19], index: arrayIdKolonaGrides[19], width: arrayWidthKolonaGrides[19], hidden: arrayVisibleKolonaGrides[19], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[20], index: arrayIdKolonaGrides[20], width: arrayWidthKolonaGrides[20], hidden: arrayVisibleKolonaGrides[20], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemDetajim1, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[21], index: arrayIdKolonaGrides[21], width: arrayWidthKolonaGrides[21], hidden: arrayVisibleKolonaGrides[21], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemDetajim2, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[22], index: arrayIdKolonaGrides[22], width: arrayWidthKolonaGrides[22], hidden: arrayVisibleKolonaGrides[22], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];
    var myGridParams = {
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: lidhur,
        widthi: $('#divgride2').width(),
        mosshtorresht: true,
        lostFocusKoloneFundit: lostFocusKoloneFundit,

        konfigToolbar: {
            identifikuesNrRreshta: "GjeneroProjektProdhimi",
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            ruajKolonatEGrides: ruajKolonatEGrides
        }
    };
  return myJQGrid.initGride(myGridParams);
}
/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(grida, formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosVleraDefaultNeGride(grida);
    return;
}
/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtSasiAktuale', 1);
    grida.setVlereDefault('txtGjeresiPorositur', 0);
    grida.setVlereDefault('txtGjatesiPorositur', 0);
    grida.setVlereDefault('txtSasiaPorositur', 0);
    grida.setVlereDefault('txtSasiPermase', 0);
    return;
}

function ndryshoKonfigFormatNumri(grida, formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));
    grida.setShifraPasPresjes('txtSasiAktuale', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtGjeresiPorositur', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtGjatesiPorositur', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaPorositur', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiPermase', formatNumri.ShifraPasPresjesSasia);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtSasiAktuale', idRreshti);
        grida.formatoQelize('txtGjeresiPorositur', idRreshti);
        grida.formatoQelize('txtGjatesiPorositur', idRreshti);
        grida.formatoQelize('txtSasiaPorositur', idRreshti);
        grida.formatoQelize('txtSasiPermase', idRreshti);
    }
}


/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {//po
    var queryString = {
        veprimi: 'GjeneroProjekt',
        listUrl: listUrl
    };
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDokumentin"));
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
}



function kerko() {
    grid_faturat.PerformCallback();
    nvFatura.ExpandAll();
}


/*
Function: callWebserviceKonfigurimi
    
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {//po
    var idGjuha = hfState.Get('idGjuha');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    if ($('#hfShtimModifikim').val() != 'modifikim')
    {
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheGrupimDokumentashNderm"), data: JSON.stringify({ kodkonfig: kodKonf, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi }) }).done(Utils.SucceededCallbackGrupimDokumentash);
    }
}

function DateChanged(s, e) {//po
    if (dteDtNga.GetDate() > dteDtDeri.GetDate()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDataNgaNukDuhetMeEMadheSeDataDeri"));
        dteDtNga.SetDate(dteDtDeri.GetDate());
    }
}

function ButtonClickArtikulli() {
    var hf = $("#hfKodi");
    var queryStr = hf.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&prodhimporosi=po', widthLupaArtikull, heightLupaArtikull);
}
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var colKushte; var colAlterKusht; var colGrida;
var colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {//po

    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', 'tblFund'];
    var arrPrind = ["dvFillim", "dvFundi"];
    colKontrollet = result.colKontrollet;
    colAtrTrupi = result.colAtrTrupi;
    var hidField2 = $("#hfFD");
    hidField2.val(0);
    var grida = $('#rowed5');
      var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
      $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible';
      $("#dvFillim").show();//$("#dvFillim")[0].style.visibility = 'visible';
    //$("#dvFillim")[0].style.display = '';
      $("#dvFundi").show();//$("#dvFundi")[0].style.visibility = 'visible';
    //$("#dvFundi")[0].style.display = '';
      $("#dvbtnKerko").show();//$("#dvbtnKerko")[0].style.visibility = 'visible';

    $("#dvgvFaturat").show()//$("#dvgvFaturat")[0].style.visibility = 'visible'; $("#dvgvFaturat")[0].style.display = '';

    colGrida = result.colGrida;
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    formatNumriZgjedhur = result.formatNumriZgjedhur;
    $('#HfGridCol').val(JSON.stringify(colGrida));

    var hfKl = $("#hfLupaKlientFurnitor");
    var hfKodi = $("#hfKodi");
    var hf6 = $("#hfLupaKodifikim1");
    var hf7 = $("#hfLupaKodifikim2");

    for (var i = 0; i < colKontrollet.length - 1; i++) {

        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (colKontrollet[i].KodKontrolli === "btneKlienti")
            hfKl.value = colAtrTrupi[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme
        else
            if (colKontrollet[i].KodKontrolli === "btneArtikulli")
                hfKodi.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen e magazines ne forme      
        if (colKontrollet[i].KodKontrolli == "btneGrup") {
            hf6.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btneNenGrup") {
            hf7.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
    for (j = 0; j < colKushte.length; j++) {

        if (colKushte[j].Kodi == 'ZKDP') {

            hidField2.val(colKushte[j].Vlera);

        }
    }

    grida.jqGrid('GridUnload', "rowed5");

    formGridColsArray();
    grida.setLastSel2(-1);
    grida = $('#rowed5');

    ruajFormatetNeGride(grida, formatNumriZgjedhur);

    inicializoGride();
    mbushGrideNgaHiddenFieldet();
}

function IndexChangedFurnitori(s, e) {//po
    if (isNaN(btneKlienti.GetValue())) {
        btneKlienti.SetText('');
        btneKlienti.Focus();
        return;
    }
}

function Furnitori_Click() {
    var hf = $("#hfLupaKlientFurnitor");
    var queryStr = hf.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhKlientin"), 'LupaKlientFurnitor.aspx?veprimi=1&idKonfigAmbjente=' + queryStr, widthLupaArtikull, heightLupaArtikull);
}

var editorGlobal;
function ButtonClickGrup() {
    var hf = $("#hfLupaKodifikim1");
    var queryStr = hf.val();
    editorGlobal = btneGrup;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=1&llojartikulli=false&idKonfigAmbjente=' + queryStr, widthLupaKodifikim, heightLupaKodifikim);
}

function ButtonClickNenGrup() {
    var hf = $("#hfLupaKodifikim2");
    editorGlobal = btneNenGrup;
    var queryStr = hf.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojKodifikimi=2&llojartikulli=false&idKonfigAmbjente=' + queryStr, widthLupaKodifikim, heightLupaKodifikim);
}

/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {//po
    var kodi = cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente");
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
        lblKonfigurimi.SetText(pershkKonfigAmb);
    callWebserviceKonfigurimi(807, kodi);
}

/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    InitArtPerb();

    grid_faturat.PerformCallback();
    arrPlanifikime = new Array();
}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    dteDtDeri.SetDate(new Date());
    var date = new Date(), y = date.getFullYear(), m = date.getMonth();
    var firstDay = new Date(y, m, 1);
    dteDtNga.SetDate(firstDay);
    btneArtikulli.SetValue(null);
    btneGrup.SetValue(null);
    btneNenGrup.SetValue(null);
    btneKlienti.SetValue(null);
    btneNrUrdherShitje.SetValue(null);
    cmbGrup1.SetValue(null);
    cmbGrup2.SetValue(null); cmbGrup3.SetValue(null);
    var hf = document.getElementById("status1");
    hf.value = "false";
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
    else return true;
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = document.getElementById("status1");
    if (hf.value == "konvertuar") {
        popKonvertuar.Show();
        return;
    }
    if (hf.value === "true") {
        myFaqeCelje.kontrolloTeDrejta('GjeneroProjektProdhimi.aspx', true);
    }
    else
        click = false;
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name === 'Ruaj') {
        myFaqeCelje.validim(s, e);

        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;

        }
    }
    else if (e.item.name === 'Pastro') {
        myFaqeCelje.kontrolloTeDrejta('GjeneroProjektProdhimi.aspx', true);
        e.processOnServer = false;
    }
}

var click = false;

/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/
function RuajClick(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
        return;
    }
    click = true;
    if (isValidKoka()) {
        // $('#hfPlanifikime').val(JSON.stringify(arrPlanifikime));
        grida.jqGrid('saveRow', idRresht, false, 'clientArray');
        grida.setLastSel2(0);
        merrTeDhenaArt();
        //  $('#gridDataObject').val(JSON.stringify(coliArtPerb));
    }
    else {
        e.processOnServer = false;
        click = false;
        Utils.hiqLoadingGif();
    }
}

/*
Function: PastroClick
Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    pastroFushatKokes();
    pastro();
    ndryshoKonfigurimin();
    click = false;
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var colNorma = new Array();
var keyGlobal;
var editorKodi;

function InitArtPerb() {
    keyGlobal = -1;
    colNorma = new Array();
}

var reshta = 0;

function merrTeDhenaArt(s, e) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');

    var gridIds = grida.jqGrid('getDataIDs');


    var rreshtaTeGrides = grida.jqGrid('getRowData');

    for (i = 0; i < rreshtaTeGrides.length; i++) {

      
        rreshtaTeGrides[i].cbGjenero = $('#cbGjenero' + gridIds[i]).attr("checked") ? true : false;
        grida.setTekstQelize('cbGjenero', gridIds[i], $('#cbGjenero' + gridIds[i]).attr("checked") ? true : false);
    }
    rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
 
}

/*
Function: SelectionChangedGridFaturat
Kur ndryshojme selection-in e grides se faturave.
*/
function SelectionChangedGridFaturat(visibleIndex) { //po
    visibleIndex = grid_faturat.GetFocusedRowIndex();
    if (visibleIndex == -1 || !grid_faturat.IsRowSelectedOnPage(visibleIndex))
        return;
    grid_faturat.GetRowValues(visibleIndex, 'IdDokumenti', OnGridFaturatSelectionComplete);
}

var arrPlanifikime = new Array();
function OnGridFaturatSelectionComplete(values) { //po
    if (!kontrolloPlanifikim(values)) {
        arrPlanifikime.push(values);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "MerrProjekteTeGjeneruara"),
            data: JSON.stringify({ id: values })
        }).done(SucceededCallbackMbushGride);
        // gvGjenerimi.PerformCallback(-1 + ';shtoReshta;' + values);
    }
}
function SucceededCallbackMbushGride(result) { //pati 
    var colTrup = result;
    var grida = $("#rowed5");
    var gridIds = grida.jqGrid('getDataIDs');
    var index;
    if (gridIds.length > 0) index = parseFloat(gridIds[gridIds.length - 1]) + 1;
    else index = 1;
    //   jQuery("#rowed5").jqGrid('saveRow', lastsel2, false, 'clientArray');
    var idkoka, nrprojekti, kodart, pershkart, sasiakt, data, gjeresi, gjatesi, sasipor, sasipermase, kodklienti, emerklienti, nrurdhershitje, idmag, idnjesi, idart, idklienti, shenime, idtrupi, detajim1, detajim2;
    for (var i = 0; i < colTrup.length; i++) {
        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || lidhur)
            be = myJQGrid.myValueButtonFshi(true, index, "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, index, "#rowed5");
        //onChange='ruajCheck(&quot;" + grida + "&quot;," + lastsel2 + ")'  onChange='ruajCheck(&quot;" + grida + "&quot;," + lastsel2 + ")' 
        var el3check = "<input  id ='cbGjenero" + index + "'  type ='checkbox'   checked='checked' style='width: 100%'  />   ";
        //   var el3check2 = "<input  id ='cbGjenero" + lastsel2 + "'  type ='checkbox'   style='width: 100%'  />   ";
        idkoka = colTrup[i].IdKoka;
        nrprojekti = colTrup[i].NrProjekti;
        kodart = colTrup[i].KodArtikulli;
        pershkart = colTrup[i].PershkrimArtikull;
        data = new Date(colTrup[i].Data).format('dd/MM/yyyy');
        kodklienti = colTrup[i].KodKlienti;
        emerklienti = colTrup[i].EmerKlienti;
        nrurdhershitje = colTrup[i].NrUrdherShitje;
        idmag = colTrup[i].IdMag;
        idnjesi = colTrup[i].IdNjesi;
        idart = colTrup[i].IdArtikulli;
        idklienti = colTrup[i].IdKlienti;
        shenime = colTrup[i].Shenime;
        idtrupi = colTrup[i].IdTrupiShitje;
        sasiakt = colTrup[i].SasiAktuale == null ? "" : colTrup[i].SasiAktuale;
        gjeresi = colTrup[i].GjeresiPorositur == null ? "" : colTrup[i].GjeresiPorositur;
        gjatesi = colTrup[i].GjatesiPorositur == null ? "" : colTrup[i].GjatesiPorositur;
        sasipor = colTrup[i].SasiaPorositur == null ? "" : colTrup[i].SasiaPorositur;
        sasipermase = colTrup[i].SasiPermase == null ? "" : colTrup[i].SasiPermase;
        detajim1 = colTrup[i].KodDetajim1 == null ? "" : colTrup[i].KodDetajim1;
        detajim2 = colTrup[i].KodDetajim2 == null ? "" : colTrup[i].KodDetajim2;

        var datarow = {
            cbGjenero: el3check, txtIdKoka: idkoka, txtNrProjekti: nrprojekti, txtKodArtikulli: kodart, txtPershkrimArtikull: pershkart, txtSasiAktuale: sasiakt, txtData: data,
            txtGjeresiPorositur: gjeresi, txtGjatesiPorositur: gjatesi, txtSasiaPorositur: sasipor, txtSasiPermase: sasipermase, txtKodKlienti: kodklienti, txtEmerKlienti: emerklienti,
            txtNrUrdherShitje: nrurdhershitje, txtIdMag: idmag, txtIdNjesi: idnjesi, txtIdArtikulli: idart, txtIdKlienti: idklienti, txtShenime: shenime, txtIdTrupiShitje: idtrupi,
            txtDetajim1: detajim1, txtDetajim2: detajim2, txtFshi: be
        };
        var su = grida.jqGrid('addRowData', parseInt(index), datarow);
        grida.setTekstQelize('txtSasiAktuale', index, sasiakt);
        grida.setTekstQelize('txtGjeresiPorositur', index, gjeresi);
        grida.setTekstQelize('txtGjatesiPorositur', index, gjatesi);
        grida.setTekstQelize('txtSasiaPorositur', index, sasipor);
        grida.setTekstQelize('txtSasiPermase', index, sasipermase);
        index = index + 1;
    }




}
function myvalueCheck(elem) {// gridid + "&quot;"onChange='ruajCheck(&quot;" + lastselgrid + "&quot;," + lastsel2 + ")' onChange='ruajCheck(&quot;" + lastselgrid + "&quot;," + lastsel2 + ")'  
    var idRresht = $('#rowed5').getLastSel2();
    if (elem[0].firstChild.checked === true)
        el3check = "<input  id ='cbGjenero" + idRresht + "'  type ='checkbox'  checked='checked' style='width: 100%'   ";
    else el3check = "<input  id ='cbGjenero" + idRresht + "'  type ='checkbox'    style='width: 100%'   ";

    el3check += ">";
    return el3check;
}
function kontrolloPlanifikim(id) {
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    var editorkodi = '';
    for (i = 0; i < idTe.length; i++) {
     editorkodi=   grida.getTekstQelize('txtIdKoka', idTe[i]);
       

        if (editorkodi == id) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyDokumentProdhimMePorosiEshteZgjedhurNjeHere"));
            return true;
        }
    }

    return false;
}
var grida = $('#rowed5');
var idRresht = grida.setLastSel2(0);
function ruajKolonatEGrides(grida) {
    var idGjuha = hfState.Get('idGjuha');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idViti = hfState.Get("idViti");
    var idGride = colGrida[0].IdKoka;
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi);
}
function formGridColsArray() {//po                       

    var IdKonfigAmbjenteLupat = [];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}
function changegjenero() {
}
function myelemGjenero(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[0];
    return myJQGrid.myElemCheckBox(value, disabled, '', idRresht, arrayIdKolonaGrides[0], changegjenero);
}
function myelemIdKoka(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1]);
}
function myelemNrProjekti(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[2];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[2]);
}

function myElemKodartikulli(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[3];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[3]);
}
function myelemPershkrimi(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[4];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[4]);
}
function myelemSasiAkt(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[5] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtSasiAktuale", onKeyDown: keyup, onFocusout: changedSasiAkt });
}
/*
Function: myelemDtFillimi

Nderton nje dateedit ku vendoset data
*/
function myelemData(value) {
    var idRresht = $('#rowed5').getLastSel2();
    if (value == "")
        value = new date();

    disabled = arrayReadOnlyKolonaGrides[6];

    return myJQGrid.myelemData(value, disabled, idRresht, 'txtData', undefined, keyup);

}
function myelemGjeresi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[7] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtGjeresiPorositur", onKeyDown: keyup, onFocusout: changedSasiAkt });
}
function myelemGjatesi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[8] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtGjatesiPorositur", onKeyDown: keyup, onFocusout: changedSasiAkt });
}
function myelemSasiPor(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[9] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtSasiaPorositur", onKeyDown: keyup, onFocusout: changedSasiAkt });
}
function myelemSasiPer(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[10] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtSasiPermase", onKeyDown: keyup, onFocusout: changedSasiPer });
}
function myelemKodKlienti(value) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[11];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[11]);
}
function myelemEmerKlienti(value) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[12];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[12]);
}
function myelemNrUrdher(value) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[13];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[13]);
}
function myelemIdMag(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[14];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[14]);
}
function myelemIdNjesi(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[15];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[15]);
}
function myelemIdArti(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[16];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[16]);
}
function myelemIdKlienti(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[17];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[17]);
}
function myelemShenime(value) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[18];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[18]);
}
function myelemIdTrupi(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[19];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[19]);
}
function myelemDetajim1(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[20];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[20]);
}
function myelemDetajim2(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[21];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[21]);
}
function myValueButtonFshi(elem, operation, value) {
    var idRresht = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
    else
        return myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");

}

function myElemButtonFshi() {//po
    var idRresht = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);


}
function keyup() {
}
function lostFocusKoloneFundit() {     //po 
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
   grida.jqGrid('saveRow', window.idRresht, null, 'clientArray');
    var ids = grida.jqGrid('getDataIDs');
    var index = grida.jqGrid('getInd', idRresht);

    if (ids[index] == undefined) {
        return;
    }

    idRresht = grida.lostFocusKoloneFundit();
    if (($('#' + arrayIdKolonaGrides[0] + idRresht).attr("disabled") == 'disabled')) {
        $('#txtSasiAktuale' + idRresht).focus();
        $('#txtSasiAktuale' + idRresht).blur();
        $('#txtSasiAktuale' + idRresht).focus();
    }
}
function mbushGrideNgaHiddenFieldet() {
    var grida = jQuery("#rowed5");
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    grida.jqGrid('clearGridData');
}

function fshiClicked(index) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (grida.jqGrid('getGridParam', 'reccount') > 1) {
        myJQGrid.fshiClicked(index, '#rowed5', inicializoGride);
    }
    else {

        grida.jqGrid('clearGridData'); window.idRresht = grida.setLastSel2(0);

    }

}
function ndryshoImazhin(nr, index) {
    var id = "butonFshi" + index;
    if (document.getElementById(id) != null) {
        if (nr == 1)
            document.getElementById(id).src = "images/blue-square-icon.png";
        else if (nr == 0)
            document.getElementById(id).src = "images/square-icon.png";
    }
}
function changedSasiAkt() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorSasia =grida.getTekstQelize( 'txtSasiAktuale' , idRresht);

    var gjeresi;
    if (editorSasia == "" || isNaN(editorSasia)) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaAktualeDuhetTeJeteNr"));
        sasia = grida.getVlereDefault('txtSasiAktuale');
        grida.setTekstQelize('txtSasiAktuale', idRresht, sasia);
        $('#txtSasiAktuale' + idRresht).focus();
       
    }
    else
        if (editorSasia == "0") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaAktualeNukMundTeJeteZero"));
            sasia = grida.getVlereDefault('txtSasiAktuale');
            grida.setTekstQelize('txtSasiAktuale', idRresht, sasia);
            $('#txtSasiAktuale' + idRresht).focus();
        }

}
function changedSasiPer() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorSasia = grida.getTekstQelize('txtSasiPermase', idRresht);
  

    var gjeresi;
    if (editorSasia == "" || isNaN(editorSasia)) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaPermaseDuhetTeJeteNumer"));
        sasia = grida.getVlereDefault('txtSasiPermase');
        grida.setTekstQelize('txtSasiPermase', idRresht, sasia);
        $('#txtSasiPermase' + idRresht).focus();
    }
    else
        if (editorSasia == "0") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaPermaseNukMundTeJeteZero"));
            sasia = grida.getVlereDefault('txtSasiPermase');
            grida.setTekstQelize('txtSasiPermase', idRresht, sasia);
            $('#txtSasiPermase' + idRresht).focus();
        }
    grida.setTekstQelize('txtSasiAktuale',idRresht,editorSasia*grida.getTekstQelize('txtGjeresiPorositur' , idRresht)*grida.getTekstQelize('txtGjatesiPorositur' , idRresht));

}
function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Click_ButtonOk5(s, e) {
    popKonvertuar.Hide();
    Utils.shfaqLoadingGif();
}
function Click_ButtonCancel5(s, e) {
    popKonvertuar.Hide();
    click = false;
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}
function SelectionChanged_grid_faturat(s, e) {
    SelectionChangedGridFaturat(e.visibleIndex);
}