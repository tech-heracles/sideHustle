;
var lidhur = false;
var magazina1;
var widthLupaArtikull = 1200;
var heightLupaArtikull = 600;
var widthLupaMagazina = 600;
var heightLupaMagazina = 600;
var widthLupaKerko = 600;
var heightLupaKerko = 600;
var identifikuesPerPopupDokumentat;
var identikuesPerPopupArtikulli;
var identifikuesPerPopupMagazina;
var identifikuesMagazina;
var click = false;
var indeksiArtPerb;
var tekstiShkruar;
var keyGlobal;
var editorData;
var editorKodi;
var editorEmertimi;
var editorVlera;
var editorNjesia;
var editorMag;
var numerReshtashQeShtohen;
var colKushte;
var colAlterKusht;
var colNorma = new Array();
var arrformatevlefta = new Array();
var arrformatevleftaqk = new Array();
var arrformatevleftamon = new Array();

var llogariFK = null;
var regjQK = new dxQendraKosto();

function enter() {
    //anullon veprimin e enterit
    if (window.event.keyCode === 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

jQuery(document).ready(function () {//po
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }
    }).trigger('resize');
    Utils.resizeSplitter();
    myMesazh.eshteLupe = true;
    $(document).keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            event.preventDefault();
        }
    });
    $(window).on("load", function () {
        Init();
    });
});

function DateChanged(s, e) {//po
}

function Init() {//po
    if (typeof (isPostBack) == "undefined") {
        var hf = document.getElementById("hfKonffillestar");
        llogariFK = JSON.parse(hfState.Get('llogariFK'));
        ndryshoKonfigurimin();
        editorData = dteDtDok;
        identifikuesPerPopupDokumentat = "RegjistrimQendraKosto";
        identikuesPerPopupKlientFurnitori = "RegjistrimQendraKosto";
        identikuesPerPopupArtikulli = "RegjistrimQendraKosto";
        identifikuesPerPopupMagazina = "RegjistrimQendraKosto";
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}

function EndRequestHandler(sender, args) {
    /*
    Function: EndRequestHandler    
    Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
    Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
    */
    click = false;
    var hf = document.getElementById("status1");
    var status = hf.value;

    if (status == "true")
        timeout = setTimeout(function () {
            window.parent.popupUniversal.Hide();
        }, 1000);
}

function menu_click(s, e) {
    /*
    Function: menuClick
    
    Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
    */
    if (e.item.name === 'Ruaj') {
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
            if (window.parent.identifikuesPerPopupBanka) {
                var banka = window.parent;
                if (banka.dokRradhes < banka.dokumentat.length) {
                    banka.myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), banka.dokumentat[banka.dokRradhes], 900, 600);
                    banka.dokRradhes++;
                }
                else {
                    banka.dokRradhes = 0;
                }
            }
        }
    }
    else if (e.item.name === 'Anullo') {
        window.parent.popupUniversal.Hide();
        e.processOnServer = false;
        click = false;
    }
}

function RuajClick(s, e) {
    /*
    Function: RuajClick    
    Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
    */
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
        return;
    }
    if (isValidKoka())
        regjQK.ruajRegjQK(true, txtNrDok.GetText(), txtNrRef.GetText(), txtShenime.GetText(), 1, "LupaRegjistrimQendraKosto.aspx", false, dteDtDok.date.toDateString(), dteDtRegjistrimi.date.toDateString());
    Utils.hiqLoadingGif();
}

function ndryshoKonfigFormatNumri(formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));
}

function gjejFormatSipasMonedhes(idmonedha) {
    var formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    for (i = 0; i < formatNumri.KonfigTrupi.length; i++)
        if (formatNumri.KonfigTrupi[i].IdMonedha == idmonedha) {
            return formatNumri.KonfigTrupi[i];
        }
    return formatNumri.KonfigTrupi[0];
}

function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    myCookies.createCookie('adresa', window.location.href, 1);
}

function ButtonClickKerko(listUrl) {//po
    /*
    Function: ButtonClickKerko    
    Hap lupen e dokumentave.
    */
    var queryString = {
        veprimi: 'RegjistrimQendraKosto',
        listUrl: listUrl
    };
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetHeaderText('Zgjidh dokumentin');
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {//po
    /*
    Function: callWebserviceKonfigurimi    
    Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
    Shiko funksionin <SucceededCallbackKonfigurimi>.
    */
    try {
        var idGjuha = hfState.Get('idGjuha');
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idPerdoruesi = hfState.Get('idPerdoruesi');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: "", idObjekti: -1, shtim: true, merrFormatKursi: true, merrGjitheKonf: true, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje })
        }).done(SucceededCallbackKonfig);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }
}

function SucceededCallbackKonfig(result) {//po
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var hidField1 = $("#hfKontabilizimi");
    hidField1.val(0);
    var colKontrollet = result.colKontroll;
    var colAtrTrupi = result.colAtrTrupi;
    formatNumriZgjedhur = result.formatNumri;
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    vendosDateDefault();
    $("#dvgvFaturat").show();
    var colGrida = result.colGrida;
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    $('#HfGridCol').val(JSON.stringify(colGrida));
    regjQK.initGridQK({ ngaThirret: 'lupa', formatNr: formatNumriZgjedhur, konfigurimGride: colGrida, PershkrimiKokes: txtShenime.GetText() }, $("#hfShtimModifikim").val(), { idKoka: hfState.Get("IdKoka"), dteDtDok: dteDtDok.date.toDateString(), dteDtRegj: dteDtRegjistrimi.date.toDateString(), idKonfig: cmbKonfigurimi.GetValue() }, { idDokGjenerues: Utils.getUrlVar('idDokGjenerues'), idKonfigGjenerues: Utils.getUrlVar('idkonfig').split(';')[0], llogariFK: llogariFK });
    ndryshoKonfigFormatNumri(formatNumriZgjedhur);
}

function ndryshoKonfigurimin() {//po
    /*
    Function: ndryshoKonfigurimin    
    Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
    */
    var kodiKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente");
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
        lblKonfigurimi.SetText(pershkKonfigAmb);
    callWebserviceKonfigurimi(906, kodiKonfigAmb);
}

function ButtonClickQendraKosto(mag) {//po
    /*
    Function: SucceededCallbackNiveli
        
    Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
    Therret funksionin <ndryshoKonfigurimin>.
    */
    if (cmbLloji.GetValue() == 1)
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni qendra e kostos', 'Shto_QendraKosto.aspx?lupe=true&llojLupe=plote&vjenNga=RegjistrimQendraKosto', 900, 400);
    else myButtonClickLupa.LupaUniversal_Click('Zgjidhni skemen e kostos', 'LupaSkemaKosto.aspx?vjenNga=RegjistrimQendraKosto', 600, 400);
}

function ButtonClickObjektiva() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni objektiven e kostos', 'LupaObjektivaKosto.aspx?vjenNga=Shto_Punonjes', 600, 400);
}

function TextChangedQendraKosto() {//po
    if (cmbQendraKosto.GetText() == "")
        return;
    regjQK.vendosQKapoSkemeNeGride(cmbLloji.GetValue(), cmbQendraKosto.GetText());
}

function TextChangedObjektiva(s, e) {//po
    magazina1 = cmbObjektiva.GetText();
    if (magazina1 == "")
        return;
    regjQK.vendosObjektiveNeGride(cmbObjektiva.GetText());
}
function TextChangedPershkrimi(s, e) {//po
    regjQK.vendosPershkrimNeGride(txtShenime.GetText());
}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() === "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
            dteDtRegjistrimi.SetDate(dataSot);
        }
        catch (e) { }
    }
}

function isValidKoka() {
    /*
    Function: isValidKoka
        
    Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
    */
    if (txtNrDok.GetText() === "") {
        myMesazh.ShtoMesazhGabimi("Shenoni numrin e dokumentit!");
        return false;
    }
    else if (dteDtDok.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje date dokumenti!");
        return false;
    }
    else if (dteDtRegjistrimi.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje date regjistrimi!");
        return false;
    }
    else if (dteDtDok.date.getFullYear() != hfState.Get("ndermarrjeVit")) {
        myMesazh.ShtoMesazhGabimi("Data nuk i përket vitit ushtrimor të zgjedhur");
        return false;
    }
    else
        return true;
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function InitArtPerb() {
    keyGlobal = -1;
    numerReshtashQeShtohen = 1;
}

function SelectionChangedGridFaturat(visibleIndex) {  //po
    /*
    Function: SelectionChangedGridFaturat
    Kur ndryshojme selection-in e grides se faturave.
    */
    if (visibleIndex === -1)
        return;
    grid_faturat.GetRowValues(visibleIndex, 'IdTrupiFleteKontabel;IdLlogari;NrLlogari;EmerLlogari;KodMonedha;DK;VleftaDebiTrupiFleteKontabel', OnGridFaturatSelectionComplete);
}

function OnGridFaturatSelectionComplete(values) { //po
    if (cmbObjektiva.GetText() != "")
        regjQK.vendosObjektiveNeGride(cmbObjektiva.GetText());
    regjQK.vendosQKdheLlogariNeGride(cmbLloji.GetValue().toString() == "1" ? cmbQendraKosto.GetValue() : 0, values[1]);
}


