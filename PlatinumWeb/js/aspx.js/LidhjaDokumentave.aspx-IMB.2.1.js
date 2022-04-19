; var identikuesPerPopupLlogari;
var identikuesPerPopupKlientFurnitori;

//array-t per griden e dok kryesore
var arrNiveli = new Array();
var arrNrDokumenti = new Array();
var arrDtDokumenti = new Array();
var arrVleftaPaLikujduar = new Array();
var arrMonedha = new Array();
var arrKursi = new Array();

//array-t per griden e dok vartes
var arrNiveli1 = new Array();
var arrNrDokumenti1 = new Array();
var arrDtDokumenti1 = new Array();
var arrVleftaPaLikujduar1 = new Array();
var arrMonedha1 = new Array();
var arrKursi1 = new Array();

//editoret per griden e dok kryesore
var editorNiveli;
var editorNrDokumenti;
var editorDtDokumenti;
var editorVleftaPaLikujduar;
var editorVlefta;
var editorMonedha;
var editorKursi;

//editoret per griden e dok vartes
var editorNiveli1;
var editorNrDokumenti1;
var editorDtDokumenti1;
var editorVleftaPaLikujduar1;
var editorVlefta1;
var editorMonedha1;
var editorKursi1;

var counter = 0;
var counter1 = 0;

var totaliKryesor = 0;
var totalilidhes = 0;

$(document).ready(function () {
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }
    }).trigger('resize');
    Utils.resizeSplitter();
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

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('LidhjaDokumentave.aspx', 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {
    var queryString = {
        veprimi: 'LidhjaDokumentave',
        listUrl: listUrl
    };
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 700, 560);
}

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    ndryshoKonfigFormatNumri(formatNumri);

    vendosKonfigFormatNumri();
    return;
}

function ndryshoKonfigFormatNumri(formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));
    if (monedha == 0) {
        Utils.setFormatNumri(txtTotali, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
        Utils.setFormatNumri(txtTotali2, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
        Utils.setFormatNumri(txtDiferenca, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
        return;
    }
    for (i = 0; i < formatNumri.KonfigTrupi.length; i++)
        if (formatNumri.KonfigTrupi[i].IdMonedha == monedha) {
            Utils.setFormatNumri(txtTotali, formatNumri.KonfigTrupi[i].ShifraPasPresjesVlefta);
            Utils.setFormatNumri(txtTotali2, formatNumri.KonfigTrupi[i].ShifraPasPresjesVlefta);
            Utils.setFormatNumri(txtDiferenca, formatNumri.KonfigTrupi[i].ShifraPasPresjesVlefta);
        }
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtTotali);
    Utils.formatoTextBox(txtTotali2);
    Utils.formatoTextBox(txtDiferenca);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtTotali);
    Utils.unFormatoTextBox(txtTotali2);
    Utils.unFormatoTextBox(txtDiferenca);
}

function Init() {
    identikuesPerPopupLlogari = "LidhjaDokumentave";
    identikuesPerPopupKlientFurnitori = "LidhjaDokumentave";
    identikuesPerPopupLlojDokumenti = "LidhjaDokumentave";
    identifikuesPerPopupDokumentat = "LidhjaDokumentave";
    var hf = $("input[id$='hfKonffillestar']")[0];
    konfigurimi_ComboBox.SetText(hf.value.split(';')[0]);
    lblKonfigurimi.SetText(hf.value.split(';')[1]);
    callWebserviceKonfigurimi("217", konfigurimi_ComboBox.GetText());
    changeName();

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    myMesazh.shtoHandler();
}

function DateChanged(s, e) {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {//po
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dtDokumenti_DateEdit.GetDate());
}

function vendosDateDefault() {
    try {
        var hfPeriudheObj = window.parent.lexoHfPeriudhe();
        dtDokumenti_DateEdit.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
        var dataSot = Utils.zeroOren(new Date());
        dtRegjistrimi_DateEdit.SetDate(dataSot);
    }
    catch (e) {
    }
}

/*
Function: callWebserviceKonfigurimi
theret web servicin per te mare konfigurimin
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('_idGjuha');
$.ajax({	  
	    url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
	    data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('_idNdermarrje'), idGjuha: idGjuha })
	}).done(SucceededCallbackKonfig);
}

//perdoret per te vendosur kontrollet ne nje tabele
function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var colKushte, colAlterKusht;
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblTotal"];
    var arrPrind = ["dvFillim", "dvTotal", "dvTotal"]
    var colKontrollet = result.colKontrollet;
    var colAtrTrupi = result.colAtrTrupi;
    var colGrida = result.colGrida;
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    var kodniveli = result.kodniveli;
    var konfLlojRreshti = result.konfLlojRreshti;

    formatNumriZgjedhur = result.formatNumriZgjedhur;
    formatKursi = result.formatKursi;
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    if (hfShtimModifikim.value != "modifikim")
    vendosDateDefault();
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if (hf.val() == "shtim" || hf.val() === "klonim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
        setVisibleFalseFushaLlogariteseNeShtim(true);
    }
    else if (hf.val() == "modifikim")
    {
        setVisibleFalseFushaLlogariteseNeShtim(false);
    }

    var hidField1 = $("#hfMeKontabilizim")[0];
    hidField1.value = 0;
    for (j = 0; j < colKushte.length; j++) {
        switch (colKushte[j].Kodi) {
            case "GJK":
                if (colAlterKusht[j].Alternativa == 'Jo') {
                    hidField1.value = 0;
                }
                else
                    if (colAlterKusht[j].Alternativa == "Direkt")
                        hidField1.value = 1;
                    else
                        hidField1.value = 2;
                break;
            case "NFHNDK":
                hfState.Set("NFHNDK", colAlterKusht[j].Alternativa == "Po");
                break;
        }
    }
    ruajFormatetNeGride(formatNumriZgjedhur, formatKursi);
    $("#dvFillim").show();//$("#dvFillim")[0].style.visibility = 'visible'; $("#dvFillim")[0].style.display = '';
    $("#dvTotal").show();//$("#dvTotal")[0].style.visibility = 'visible'; $("#dvTotal")[0].style.display = '';
    $("#gridak").show();//$("#gridak")[0].style.visibility = 'visible'; $("#gridak")[0].style.display = '';
    $("#gridal").show();//$("#gridal")[0].style.visibility = 'visible'; $("#gridal")[0].style.display = '';
}

function setVisibleFalseFushaLlogariteseNeShtim(isVisible) {
    lblTotali.SetVisible(isVisible)
    txtTotali.SetVisible(isVisible)
    lblTotali2.SetVisible(isVisible)
    txtTotali2.SetVisible(isVisible);
    lblDiferenca.SetVisible(isVisible)
    txtDiferenca.SetVisible(isVisible);
}

///kur ndryshon konfigurimi therret web service per konfigurimin e ri
function ndryshoKonfigurimin() {
    if (konfigurimi_ComboBox.GetText().split(';').length > 1)
        lblKonfigurimi.SetText(konfigurimi_ComboBox.GetText().split(';')[1]);
    konfigurimi_ComboBox.SetText(konfigurimi_ComboBox.GetText().split(';')[0]);
    callWebserviceKonfigurimi("217", konfigurimi_ComboBox.GetText());
}

//kur kthehemi pas nje postbacku
function EndRequestHandler(sender, args) {
    // This code is executed after a successful async. postback

    var statusRuajtje = $("input[id$='hfStatusRuajtje']")[0];
    if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {
        popMesazhQK.Show();
        if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {
            lblmesazhqendra.SetText(hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"));
        }
    }
    if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val(), 900, 600);
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));
    }
    if (statusRuajtje.value == "true") {
        myFaqeCelje.kontrolloTeDrejta('LidhjaDokumentave.aspx?shtim_modifikim=shtim', true);
    }
    else click = false;
}

function JopopupClick(s, e) {
    if ($('#hfUrlVDK').val() != '')
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));

    if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh' && $('#hfUrlVDK').val() != '') {
        popMesazhQK.Show(); lblmesazhqendra.SetText(hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"));
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        if ($('#hfUrlVDK').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
            $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", "")); if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');
        }
    }
}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
    if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh' && $('#hfUrlmag').val() != '') {
        popMesazhQK.Show(); lblmesazhqendra.SetText(hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"));
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        if ($('#hfUrlVDK').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
            $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", "")); if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');

        }
    }
}

function hapPopUp(s, e) {
    if ($('#hfUrlVDK').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", "")); if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');
    }
}

function PastroClick() {
    pastro();
    nrLidhje_TextBox.SetText("");
    txtTotali.SetText("");
    txtTotali2.SetText("");
    klientFurnitor_ButtonEdit.SetValue(null);
    var hf = $("input[id$='hfShtimModifikim']");
    hf.val("shtim");
    //grid_dokKryesor.UnselectAllRowsOnPage();
    grid_dokKryesor.PerformCallback('unselect;shtim');
    grid_dokLidhes.PerformCallback('unselect');
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaRegjistrimPaDraft(hf, hfTeDrejta);
    popKuadruar.Hide();
    ndryshoKonfigurimin();
    $('#ASPxSplitter1_hl').empty();
    click = false;
}

var click = false; //perdoret qe perdoruesi te mos shtype dyhere ruaj

///veprimet e menuse
function menu_click(s, e) {
    if (click) {
        e.processOnServer = false;
    }
    else {
        click = true;
        if (e.item.name == 'Ruaj') {
            Utils.shfaqLoadingGif();;
            merrTeDhena();
            merrTeDhena1();
            begincallbackKrye();
            if (isValid()) {
                pastro();
            }
            else {
                e.processOnServer = false;
                click = false;
            }
        }
        else if (e.item.name == 'Ruaj si draft') {
        }
        else if (e.item.name == 'Fshi') {
            popFshi.Show(); e.processOnServer = false; click = false;
        }
        else if (e.item.name == 'Kerko') {
            myFaqeCelje.kontrolloTeDrejta("ListaLidhjaDokumentave.aspx", null, true); e.processOnServer = false; click = false;
        }
        else if (e.item.name == 'Shto') {
            myFaqeCelje.kontrolloTeDrejta('LidhjaDokumentave.aspx?shtim_modifikim=shtim', true);
            e.processOnServer = false;
        }
        else if (e.item.name == 'Anullo') {
            myFaqeCelje.kontrolloTeDrejta("ListaLidhjaDokumentave.aspx");
            e.processOnServer = false;
            click = false;
        }
        else if (e.item.name == 'QendraKosto') {
            click = false;
        }
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("ListaLidhjaDokumentave.aspx?ruaj=po");
}

/*
Function: OnGridSelectionChanged

Therritet kur ndryshon selection-i i grides se dokumentave kryesore. 
dhe ben ndryshimet e vlerave sipas kushteve
Shiko funksionin <OnGridSelectionComplete>.
*/
function OnGridSelectionChanged(s, e) {
    var selcountkryesor = grid_dokKryesor.GetSelectedRowCount();
    var selcountlidhes = grid_dokLidhes.GetSelectedRowCount();
    if (selcountkryesor > 1 && selcountlidhes > 1) {   //nuk lejohen qe te lidhen shume dokumenta kryesore me shume dokumenta lidhes
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokNukMundTeLidhniShumeMeShumeDok"));
        grid_dokKryesor.UnselectRowOnPage(e.visibleIndex);
        return;
    }
    if (e.visibleIndex !== -1) {   //nqs eshte selektuar te pakten nje 
        var editorvleftaPalikuiduar = Utils.ktheKontroll('lblVleftaPaLikujduar' + e.visibleIndex);         //vlera e palikuiduar e rreshtit te selektuar
        var shumadokkryesor = 0;
        var shumadoklidhesfatura = 0;
        var shumadoklidhesmbetur = 0;
        for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {          //gjendet shuma e dokumentave te tjere kryesore
            if (grid_dokKryesor._isRowSelected(i) && i !== e.visibleIndex) {
                editorVlefta = Utils.ktheKontroll('txtVlefta' + i);
                //shumadokkryesor += parseFloat(editorVlefta.GetText());
                shumadokkryesor = shumadokkryesor + parseFloat(editorVlefta.GetText());
            }
        }
        for (var j = 0; j < grid_dokLidhes.cpNoRows; j++) {    //gjendet shuma e lidhes te dokumentave lidhes   si dhe shuma e vleres se palikuiduar te dokumentave lidhes
            if (grid_dokLidhes._isRowSelected(j)) {
                var editorvleftaPalikuiduarlid = Utils.ktheKontroll('lblVleftaPaLikujduarLidhes' + j);
                editorVleftaMonDok = Utils.ktheKontroll('txtVleftaMonBazeLidhes' + j);
                shumadoklidhesfatura += parseFloat(editorVleftaMonDok.GetText());
                shumadoklidhesfatura = shumadoklidhesfatura + parseFloat(editorVleftaMonDok.GetText());
                for (var k = 0; k < countkvkmk; k++)   //kv pjestim kmk qe ruhet ne nje vektor kur selektojme nje rresht te dokumentave lidhes
                    if (kvkmk[k][0] == j) {
                        //shumadoklidhesmbetur += parseFloat(editorvleftaPalikuiduarlid.GetText() * kvkmk[k][1]);
                        shumadoklidhesmbetur = shumadoklidhesmbetur + parseFloat(editorvleftaPalikuiduarlid.GetText() * kvkmk[k][1]);
                        break;
                    }

            }
        }
        editorVlefta = Utils.ktheKontroll('txtVlefta' + e.visibleIndex);   //kontrolli te cilin do llogarisim
        ///vlera e dokumentit kryesor ne rastin 1 me shume eshte sa minimalja midis vleres se saj te mbetur dhe shumes se dokumentave lidhes
        /// ne rastin shume me 1 eshte minimalja midis vleres se saj te mbetur dhe diferences midis vleres se dokumentit lidhes - shumen e dokumentave te tjere kryesore
        if (selcountlidhes === 0)  //nqs nuk ka asnje dokument lidhes te selektuar e leme vleren sa vlefta e palikuiduar
            editorVlefta.SetText(parseFloat(editorvleftaPalikuiduar.GetText()));
        else  //nqs diferenca midis shumes se dokumentave te tjere kryesor dhe dokumentave lidhes eshte 0 ose negative 
            //      shikohet nese dokumenti lidhes ka akoma vlere te palikuiduar per te plotesuar vleren e palikur te ketij dokumenti     
            //prn vendoset zero
            //ky rast ndodh vetem kur kemi lidhen shume me 1
            if (Math.round(shumadoklidhesfatura - shumadokkryesor, 6) <= 0) {
                if (Math.round(shumadoklidhesmbetur - shumadokkryesor, 6) >= 0) {
                    if (parseFloat(editorvleftaPalikuiduar.GetText()) <= shumadoklidhesmbetur - shumadokkryesor) //nqs vlera e palikuiduar e dokumentit eshte me e vogel se vlera e mbetur e dok lidhes
                        editorVlefta.SetText(parseFloat(editorvleftaPalikuiduar.GetText()));                 //atehere vendoset vlera e palikuiduar
                    else editorVlefta.SetText(parseFloat(shumadoklidhesmbetur - shumadokkryesor));            //prn vlera qe ka mbetur nga dokumenti lidhes pa u likuiduar
                }
                else
                    editorVlefta.SetText(0.00);
            }
            else  //nqs shuma e dok lidhes eshte me e madhe se shuma e dok kryesor atehere krahasohet me vleren e palikuiduar dhe vendoset vlera me e vogel
                if (parseFloat(editorvleftaPalikuiduar.GetText()) <= shumadoklidhesfatura - shumadokkryesor)
                    editorVlefta.SetText(parseFloat(editorvleftaPalikuiduar.GetText()));
                else editorVlefta.SetText(parseFloat(shumadoklidhesfatura - shumadokkryesor));
        //thirret ky funksion per te vendosur vleren e re te shtuar apo te hequr tek dokumenti lidhes
        TextChangedVlefta(editorVlefta, e.visibleIndex);
    }
    merrTeDhena();    ///merren te dhenat e plotesuara per te mos humbur vlerat kur ben callback
    merrTeDhena1();
    grid_dokKryesor.GetSelectedFieldValues('IdKlientFurnitori;IdMonedha', OnGridSelectionComplete);
}

function begincallback() {
    var hidField11 = $("#hfVlefta1");
    var arrVlefta = new Array();
    for (var j = 0; j < grid_dokLidhes.cpNoRows; j++) {
        if (grid_dokLidhes._isRowSelected(j)) {
            id = grid_dokLidhes.GetRowKey(j);
            editorVlefta1 = Utils.ktheKontroll('txtVleftaLidhes' + j);
            arrVlefta.push({ id: id, vlefta: editorVlefta1.GetText() });            
        }
    }
    hidField11.val(JSON.stringify(arrVlefta));
}

function begincallbackKrye(s, e) {
    var hidField4 = $("#hfVlefta");
    var arrVlefta = new Array();
    for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {
        if (grid_dokKryesor._isRowSelected(i)) {
            id = grid_dokKryesor.GetRowKey(i);
            editorVlefta = Utils.ktheKontroll('txtVlefta' + i);
            arrVlefta.push({ id: id, vlefta: editorVlefta.GetText() });
        }
    }
    hidField4.val(JSON.stringify(arrVlefta));
}

var monedha = 0;
/*
Function: OnGridSelectionChangedLidhes

Therritet kur ndryshon selection-i i grides se dokumentave vartes. 
Therret funksionin <vendosTotalet>.
*/
function OnGridSelectionComplete(values) {
    if ((values == undefined || values == '')) //kur unselecton rreshtin e selektuar me pare
    {
        //nqs vjen nga ndryshimi i klientit tek koka ndrysho dhe griden kryesore
        if (!klienti)
            grid_dokKryesor.PerformCallback('unselect');
        grid_dokLidhes.PerformCallback('unselect');
    }
    else {
        if (values.length == 1) {
            grid_dokLidhes.PerformCallback(values[0][0] + ';select');
            grid_dokKryesor.PerformCallback(values[0][0] + ';select');
        }
    }
    if (values.length == 0)
        monedha = 0;
    else
        monedha = values[0][1];    
        
    ruajFormatetNeGride();
    klienti = false;
    vendosTotalet();
}

function EndCallback() {
    formatoFushaDevi();
    if (grid_dokKryesor.GetAutoFilterEditor("EmertimiKf").GetValue() != klientFurnitor_ButtonEdit.GetValue()) {
        ///ristarton array e kvkmk per klientin e ri
        kvkmk = new Array();
        countkvkmk = 0;
    }
}

var klienti = false;
//kur ndryshon klienti tek koka
function KlientFurnitoriChanged() {
    if (isNaN(klientFurnitor_ButtonEdit.GetValue())) {
        klientFurnitor_ButtonEdit.SetText('');
        klientFurnitor_ButtonEdit.Focus();
        return;
    }
    var id = klientFurnitor_ButtonEdit.GetValue();
    klienti = true;
    grid_dokKryesor.AutoFilterByColumn("IdKlientFurnitori", id);
    grid_dokKryesor.UnselectAllRowsOnPage();
}

/*
Function: OnGridSelectionChangedLidhes

Therritet kur ndryshon selection-i i grides se dokumentave vartes. 
Therret funksionin <vendosTotalet>.
*/
var index = 0;
function OnGridSelectionChangedLidhes(s, e) {
    var selcountkryesor = grid_dokKryesor.GetSelectedRowCount(); grid_dokLidhes.GetSelectedFieldValues('IdDokumenti', OnGridSelectionCompletelidh);
    var selcountlidhes = grid_dokLidhes.GetSelectedRowCount();
    if (selcountkryesor > 1 && selcountlidhes > 1) {      //nuk lejohen shume dokumenta kryesore te lidhur me shume dokumenta lidhes
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokNukMundTeLidhniShumeMeShumeDok"));
        grid_dokLidhes.UnselectRowOnPage(e.visibleIndex);
        return;
    }
    if (e.visibleIndex !== -1) { //rasti kur nuk eshte e selektuar asnje
        index = e.visibleIndex;
        var monedhakryesor = Utils.ktheKontroll('lblMonedha' + grid_dokKryesor.cpNoPage * 15);
        for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {          //gjendet shuma e dokumentave te tjere kryesore
            if (grid_dokKryesor._isRowSelected(i)) {
                monedhakryesor = Utils.ktheKontroll('lblMonedha' + i);
                break;
            }
        }

        var monedhalidhes = Utils.ktheKontroll('lblMonedhaLidhes' + e.visibleIndex);
        var datelidhes = Utils.ktheKontroll('lblDtDokumentiLidhes' + e.visibleIndex);
        editorMonDokkryesor = Utils.ktheKontroll("txtVleftaMonBazeLidhes" + e.visibleIndex);
        var editorvleftaPalikuiduar = Utils.ktheKontroll('lblVleftaPaLikujduarLidhes' + e.visibleIndex);
        var editorkursi = Utils.ktheKontroll('lblKursiLidhes' + e.visibleIndex);   //llogaritet kv pjestim kmk per kete dokument
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "merrKVpjesetimKMK"),
            data: JSON.stringify({ monedhakryesor: monedhakryesor.GetText(), monedhalidhes: monedhalidhes.GetText(), date: datelidhes.GetText(), kursdoklidhes: editorkursi.GetText(), idNdermarrje: hfState.Get('_idNdermarrje') })
        }).done(SucceededCallbackSelLidhes);
    }
    else vendosTotalet();
}

var kvkmk = new Array();
var countkvkmk = 0;
function SucceededCallbackSelLidhes(result) {
    var editorvleftaPalikuiduar = Utils.ktheKontroll('lblVleftaPaLikujduarLidhes' + index);
    kvkmk[countkvkmk] = new Array();
    kvkmk[countkvkmk][0] = index;
    kvkmk[countkvkmk][1] = result;
    countkvkmk++;
    var shumadokkryesor = 0;
    var shumadoklidhesfatura = 0;
    var shumadokmbetur = 0;
    var selcountkryesor = grid_dokKryesor.GetSelectedRowCount();
    for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {      //shuma e vlerave te lidhes se dokumentave kryesor dhe shuma e vlerave te palikuiduar te dokumentave kryesor
        if (grid_dokKryesor._isRowSelected(i)) {
            editorVlefta = Utils.ktheKontroll('txtVlefta' + i);
            editorVleftaPaLikujduar = Utils.ktheKontroll('lblVleftaPaLikujduar' + i);
            //shumadokkryesor += parseFloat(editorVlefta.GetText());
            shumadokkryesor = shumadokkryesor + parseFloat(editorVlefta.GetText());
            //shumadokmbetur += parseFloat(editorVleftaPaLikujduar.GetText());
            shumadokmbetur = shumadokmbetur + parseFloat(editorVleftaPaLikujduar.GetText());
        }
    }
    for (var j = 0; j < grid_dokLidhes.cpNoRows; j++) {    // shuma e dokumentave lidhes
        if (grid_dokLidhes._isRowSelected(j) && j !== index) {
            editorVleftaMonDok = Utils.ktheKontroll('txtVleftaMonBazeLidhes' + j);
            //shumadoklidhesfatura += parseFloat(editorVleftaMonDok.GetText());
            shumadoklidhesfatura = shumadoklidhesfatura + parseFloat(editorVleftaMonDok.GetText());
        }
    }
    editorVleftaMonDok = Utils.ktheKontroll('txtVleftaMonBazeLidhes' + index);
    editorVleftalidhes = Utils.ktheKontroll('txtVleftaLidhes' + index);
    ///vlera e dokumentit lidhes per rastin 1 me shume llogaritet si minimalja midis vleres se saj te mbetur dhe diferences midis vleres se dokumentit kryesor ne monedhe dokumenti lidhes me shumen e dokumentave te tjere lidhes 
    /// per rastin shume me nje si minimalja midis vleres se saj te mbetur dhe shumes se vlerave te dokumentave kryesor ne monedhe dokumenti lidhes
    if (selcountkryesor === 0) {      //kur nuk eshte i zgjedhur asnje dokument kryesor   lihet vlera e mbetur
        editorVleftaMonDok.SetText(parseFloat(result * editorvleftaPalikuiduar.GetText()));
        editorVleftalidhes.SetText(parseFloat(editorvleftaPalikuiduar.GetText()));
    }      /// rasti kur diferenca midis shumes se dokumentave kryesor dhe dokumentave lidhes eshte 0
        //shikohet nqs ka mbetur me vlere e palikuiduar nga dokumenti kryesor dhe merret minimalja midis vleres se mbetur te ketij dokumenti lidhes me vleren e mbetur pa u likuiduar te dokumentit kryesor
        // ky rast ndodh vetem kur kemi lidhje 1 me shume
    else if (Math.round(shumadokkryesor - shumadoklidhesfatura, 6) <= 0) {
        if (Math.round(shumadokmbetur - shumadoklidhesfatura) >= 0) {
            if (parseFloat(result * editorvleftaPalikuiduar.GetText()) <= shumadokmbetur - shumadoklidhesfatura) {
                editorVleftaMonDok.SetText(parseFloat(result * editorvleftaPalikuiduar.GetText()));
                editorVleftalidhes.SetText(parseFloat(editorvleftaPalikuiduar.GetText()));
            }
            else {
                editorVleftaMonDok.SetText(parseFloat((shumadokmbetur - shumadoklidhesfatura)));
                editorVleftalidhes.SetText(parseFloat((shumadokmbetur - shumadoklidhesfatura) / result));
            }
        }
        else {
            editorVleftaMonDok.SetText(parseFloat(0));
            editorVleftalidhes.SetText(parseFloat(0));
        }
    }
    else if (parseFloat(result * editorvleftaPalikuiduar.GetText()) <= shumadokkryesor - shumadoklidhesfatura) {
        editorVleftaMonDok.SetText(parseFloat(result * editorvleftaPalikuiduar.GetText()));
        editorVleftalidhes.SetText(parseFloat(editorvleftaPalikuiduar.GetText()));
    }
    else {
        editorVleftaMonDok.SetText(parseFloat(shumadokkryesor - shumadoklidhesfatura));
        editorVleftalidhes.SetText(parseFloat((shumadokkryesor - shumadoklidhesfatura) / result));
    }
    //perdoret per te shtuar apo hequr vleren e dokumentit te selektuar tek dokumenti kryesor
    TextChangedVleftaLidhes(editorVleftalidhes, index);
    vendosTotalet();
}

/*
Function: TextChangedVlefta

Therritet kur ndryshon vlefta e lidhjes per nje dokument te grides se dokumentave kryesore. 
Validon vleften (nuk duhet te jete > se vlera e palikujduar) dhe therret funksionin <vendosTotalet>.
*/
function TextChangedVlefta(editor, key) {
    editorVleftaPaLikujduar = Utils.ktheKontroll('lblVleftaPaLikujduar' + key);
    editorKursi = Utils.ktheKontroll('lblKursi' + key);
    var vleftapalikujduar = parseFloat(editorVleftaPaLikujduar.GetText());
    var vlefta = parseFloat(editor.GetText());
    if (parseFloat(vlefta) > parseFloat(vleftapalikujduar)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokNukMundTeVendosniVlereMeTeMadheSeVleraEPalikujduar"));
        editor.SetText(vleftapalikujduar);
        vlefta = vleftapalikujduar;
    }
    if (!grid_dokKryesor._isRowSelected(key)) {  //nqs reshti eshte c'selektuar lere vleren sa vlera e palikuiduar
        editor.SetText(vleftapalikujduar);
        vlefta = vleftapalikujduar;
    }
    var shumadokkryesor = 0;

    for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {    //shuma e dok kryesor
        if (grid_dokKryesor._isRowSelected(i)) {
            editorVlefta = Utils.ktheKontroll('txtVlefta' + i);
            //shumadokkryesor += parseFloat(editorVlefta.GetText());
            shumadokkryesor = shumadokkryesor + parseFloat(editorVlefta.GetText());
        }
    }
    var selcountkryesor = grid_dokKryesor.GetSelectedRowCount();
    var selcountlidhes = grid_dokLidhes.GetSelectedRowCount();
    if (selcountkryesor > 1 || (selcountkryesor === 1 && selcountlidhes === 1)) {  //ndodh vetem kur kemi rastin shume me 1 per te llogaritur vleren e dok lidhes sa shumen e dok kryesor
        for (var j = 0; j < grid_dokLidhes.cpNoRows; j++) {                   //ose nje me nje per ti barazuar vlerat
            if (grid_dokLidhes._isRowSelected(j)) {
                editorVleftaMonDok = Utils.ktheKontroll('txtVleftaMonBazeLidhes' + j);
                editorVleftaPaLikujduar1 = Utils.ktheKontroll('lblVleftaPaLikujduarLidhes' + j);
                editorVlefta1 = Utils.ktheKontroll('txtVleftaLidhes' + j);
                var kvpjestimkmk = 1;
                for (var k = 0; k < countkvkmk; k++) {
                    if (kvkmk[k][0] === j) {
                        kvpjestimkmk = kvkmk[k][1];
                        break;
                    }
                }

                if (shumadokkryesor <= 0) {  //nqs shuma e dok kryesor 0 vendose 0
                    editorVlefta1.SetText(parseFloat(0));
                    editorVleftaMonDok.SetText(parseFloat(0));
                }
                else if (shumadokkryesor >= parseFloat(editorVleftaPaLikujduar1.GetText() * kvpjestimkmk)) {  //vendos minimalen midis vleres se palikuiduar te dokumentit lidhes dhe shumes e dokumentave kryesor
                    editorVlefta1.SetText(parseFloat(editorVleftaPaLikujduar1.GetText()));
                    editorVleftaMonDok.SetText(parseFloat(editorVleftaPaLikujduar1.GetText() * kvpjestimkmk));
                    shumadokkryesor -= parseFloat(editorVleftaPaLikujduar.GetText());
                }
                else {
                    editorVlefta1.SetText(parseFloat(shumadokkryesor / kvpjestimkmk));
                    editorVleftaMonDok.SetText(parseFloat(shumadokkryesor));
                    shumadokkryesor = 0;
                }
            }
        }
        vendosTotalet();
    }
}

var editorMonDokkryesor;
/*
Function: TextChangedVleftaLidhes

Therritet kur ndryshon vlefta e lidhjes per nje dokument te grides se dokumentave vartes. 
Validon vleften (nuk duhet te jete > se vlera e palikujduar) dhe therret funksionin <vendosTotalet>.
*/
var vlefta1 = 0;

function TextChangedVleftaLidhes(editor1, key1) {
    editorVleftaPaLikujduar1 = Utils.ktheKontroll('lblVleftaPaLikujduarLidhes' + key1);
    editorKursi1 = Utils.ktheKontroll('lblKursiLidhes' + key1);
    var vleftapalikujduar1 = parseFloat(editorVleftaPaLikujduar1.GetText());
    vlefta1 = parseFloat(editor1.GetText());
    if (parseFloat(vlefta1) > parseFloat(vleftapalikujduar1)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokNukMundTeVendosniVlereMeTeMadheSeVleraEPalikujduar"));
        editor1.SetText(vleftapalikujduar1);
        vlefta1 = vleftapalikujduar1;
    }
    if (!grid_dokLidhes._isRowSelected(key1))         //rasti kur eshte c'selektuar vendoset vlera e palikuiduar
    {
        vlefta1 = vleftapalikujduar1;
        editor1.SetText(vleftapalikujduar1);
    }

    var monedhakryesor = Utils.ktheKontroll('lblMonedha' + grid_dokKryesor.cpNoPage * 15);
    for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {          //gjendet shuma e dokumentave te tjere kryesore
        if (grid_dokKryesor._isRowSelected(i)) {
            monedhakryesor = Utils.ktheKontroll('lblMonedha' + i);
            break;
        }
    }
    var monedhalidhes = Utils.ktheKontroll('lblMonedhaLidhes' + key1);
    var datelidhes = Utils.ktheKontroll('lblDtDokumentiLidhes' + key1);
    editorMonDokkryesor = Utils.ktheKontroll("txtVleftaMonBazeLidhes" + key1)       //llogaritet kv/kmk
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrKVpjesetimKMK"),
        data: JSON.stringify({ monedhakryesor: monedhakryesor.GetText(), monedhalidhes: monedhalidhes.GetText(), date: datelidhes.GetText(), kursdoklidhes: editorKursi1.GetText(), idNdermarrje: hfState.Get('_idNdermarrje') })
    }).done(SucceededCallbackVleftaMonDokKryesor);
}

function SucceededCallbackVleftaMonDokKryesor(result) {
    var vleftaMonBaze1 = parseFloat(vlefta1 * result);
    editorMonDokkryesor.SetText(vleftaMonBaze1);
    var shumadoklidhesfatura = 0;
    for (var j = 0; j < grid_dokLidhes.cpNoRows; j++) {     //shuma e dokumentave lidhes
        if (grid_dokLidhes._isRowSelected(j)) {
            editorVleftaMonDok = Utils.ktheKontroll('txtVleftaMonBazeLidhes' + j);
            //shumadoklidhesfatura += parseFloat(editorVleftaMonDok.GetText());
            shumadoklidhesfatura = shumadoklidhesfatura + parseFloat(editorVleftaMonDok.GetText());
        }
    }
    var selcountkryesor = grid_dokKryesor.GetSelectedRowCount();
    var selcountlidhes = grid_dokLidhes.GetSelectedRowCount();
    if (selcountlidhes > 1 || (selcountkryesor === 1 && selcountlidhes === 1)) {   //rasti kur eshte 1 me shume per te vendosur vleren e dokumentit kryesor sa shume e dok lidhes 
        // dhe nje me nje per ti barazuar vlerat
        for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {
            if (grid_dokKryesor._isRowSelected(i)) {
                editorVleftaPaLikujduar = Utils.ktheKontroll('lblVleftaPaLikujduar' + i);
                editorVlefta = Utils.ktheKontroll('txtVlefta' + i);
                if (shumadoklidhesfatura <= 0) {
                    editorVlefta.SetText(parseFloat(0));
                }           ///llogaritet vlera si minimalja midis shumes se dokumentave lidhes dhe vleres se palikuiduar
                if (shumadoklidhesfatura >= parseFloat(editorVleftaPaLikujduar.GetText())) {
                    editorVlefta.SetText(parseFloat(editorVleftaPaLikujduar.GetText()));
                    shumadoklidhesfatura -= parseFloat(editorVleftaPaLikujduar.GetText());
                }
                else {
                    editorVlefta.SetText(parseFloat(shumadoklidhesfatura));
                    shumadoklidhesfatura = 0;
                }
            }
        }
    }
    vendosTotalet();
}

/*
Function: pastro

Pastron array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    arrIdDok = new Array();
    arrNiveli = new Array();
    arrNrDokumenti = new Array();
    arrDtDokumenti = new Array();
    arrVleftaPaLikujduar = new Array();
    arrMonedha = new Array();
    arrKursi = new Array();
    arrNiveli1 = new Array();
    arrNrDokumenti1 = new Array();
    arrDtDokumenti1 = new Array();
    arrVleftaPaLikujduar1 = new Array();
    arrMonedha1 = new Array();
    arrKursi1 = new Array();
    monedha = 0;
    counter = 0;
    counter1 = 0;
}

/*
Function: vendosTotalet

Llogarit dhe vendos totalet te textboxet poshte gridave.
*/
function vendosTotalet() {//  llogarit totalet ne monedhe baze dhe nxjerr diferences qe do gjeneroje diference nga kurset
    var tot1 = 0;
    for (var k = 0; k < grid_dokKryesor.cpNoRows; k++) {
        if (grid_dokKryesor._isRowSelected(k)) {
            editorVlefta = Utils.ktheKontroll('txtVlefta' + k);
            editorDtDokumenti = Utils.ktheKontroll('lblDtDokumenti' + k);
            editorDtAzhornimi = Utils.ktheKontroll('lblDtAzhornimi' + k);
            editorKursAzhornimi = Utils.ktheKontroll('lblKursAzhornimi' + k);
            editorKursi = Utils.ktheKontroll('lblKursi' + k);

            if (Date.parseLocale(editorDtAzhornimi.GetText(), "dd/MM/yyyy") < Date.parseLocale(editorDtDokumenti.GetText(), "dd/MM/yyyy"))
                //tot1 += parseFloat(editorVlefta.GetText() * editorKursi.GetText());
                tot1 = tot1 + parseFloat(editorVlefta.GetText() * editorKursi.GetText());
            else //tot1 += parseFloat(editorVlefta.GetText() * editorKursAzhornimi.GetText());
                tot1 = tot1 + parseFloat(editorVlefta.GetText() * editorKursAzhornimi.GetText());
        }
    }

    var tot2 = 0;
    for (var l = 0; l < grid_dokLidhes.cpNoRows; l++) {
        if (grid_dokLidhes._isRowSelected(l)) {
            editorVlefta1 = Utils.ktheKontroll('txtVleftaLidhes' + l);
            editorKursi1 = Utils.ktheKontroll('lblKursiLidhes' + l);
            //tot2 += parseFloat(editorVlefta1.GetText() * editorKursi1.GetText());
            tot2 = tot2 + parseFloat(editorVlefta1.GetText() * editorKursi1.GetText());
        }
    }
    txtTotali.SetText(parseFloat(tot1));
    txtTotali2.SetText(parseFloat(tot2));
    txtDiferenca.SetText(parseFloat(tot2 - tot1).toFixed(6));
}

/*
Function: merrTeDhena

Merr te dhenat nga grida e dokumentave kryesore dhe i vendos neper hiddenfield-e per ti perdorur ne server side.
*/
function merrTeDhena() {
    pastro();
    totaliKryesor = 0;
    var hidField1 = $("#hfNiveli");
    var hidField2 = $("#hfNrDokumenti");
    var hidField3 = $("#hfVleftaPaLikujduar");
    var hidField5 = $("#hfDtDokumenti");
    var hidField6 = $("#hfMonedha");
    var hidField7 = $("#hfKursi");
    var hidFieldIdDok = $("#hfIdDokKryesor");

    for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {
        if (grid_dokKryesor._isRowSelected(i)) {
            editorNiveli = Utils.ktheKontroll('lblNiveli' + i);
            editorNrDokumenti = Utils.ktheKontroll('lblNrDokumenti' + i);
            editorDtDokumenti = Utils.ktheKontroll('lblDtDokumenti' + i);
            editorVleftaPaLikujduar = Utils.ktheKontroll('lblVleftaPaLikujduar' + i);
            editorMonedha = Utils.ktheKontroll('lblMonedha' + i);
            editorKursi = Utils.ktheKontroll('lblKursi' + i);
            editorDtAzhornimi = Utils.ktheKontroll('lblDtAzhornimi' + i);
            editorKursAzhornimi = Utils.ktheKontroll('lblKursAzhornimi' + i);
            arrIdDok[counter] = grid_dokKryesor.GetRowKey(i);
            arrNiveli[counter] = editorNiveli.GetText();
            arrNrDokumenti[counter] = editorNrDokumenti.GetText();
            arrDtDokumenti[counter] = editorDtDokumenti.GetText();
            arrVleftaPaLikujduar[counter] = editorVleftaPaLikujduar.GetText();
            arrMonedha[counter] = editorMonedha.GetText();
            if (Date.parseLocale(editorDtAzhornimi.GetText(), "dd/MM/yyyy") > Date.parseLocale(editorDtDokumenti.GetText(), "dd/MM/yyyy"))
                arrKursi[counter] = editorKursAzhornimi.GetText();
            else
                arrKursi[counter] = editorKursi.GetText();

            totaliKryesor = mbledhje(totaliKryesor, parseFloat(editorVlefta.GetText()));
            //counter += 1;
            counter = counter + 1;
        }
    }
    //grid_dokKryesor.GetSelectedFieldValues('IdDokumenti', OnGridSelectionCompletedok);
    hidFieldIdDok.val(JSON.stringify(arrIdDok));
    hidField1.val(JSON.stringify(arrNiveli));
    hidField2.val(JSON.stringify(arrNrDokumenti));
    hidField3.val(JSON.stringify(arrVleftaPaLikujduar));
    hidField5.val(JSON.stringify(arrDtDokumenti));
    hidField6.val(JSON.stringify(arrMonedha));
    hidField7.val(JSON.stringify(arrKursi));
    unformatoFushaDevi();
}

//function OnGridSelectionCompletedok(values) {
//    $('#hfIdDokKryesor').val(JSON.stringify(values));
//}

function OnGridSelectionCompletelidh(values) {
    $('#hfIdDokLidhes').val(JSON.stringify(values));
}

/*
Function: merrTeDhena1

Merr te dhenat nga grida e dokumentave vartes dhe i vendos neper hiddenfield-e per ti perdorur ne server side.
*/
function merrTeDhena1() {
    pastro();
    totalilidhes = 0; grid_dokLidhes.GetSelectedFieldValues('IdDokumenti', OnGridSelectionCompletelidh);
    var hidField8 = $("#hfNiveli1");
    var hidField9 = $("#hfNrDokumenti1");
    var hidField10 = $("#hfVleftaPaLikujduar1");
    var hidField12 = $("#hfDtDokumenti1");
    var hidField13 = $("#hfMonedha1");
    var hidField14 = $("#hfKursi1");
    var arridlidhes = new Array();
    var arrid = new Array();
    if ($('#hfIdDokLidhes').val() != "")
        arrid = JSON.parse($('#hfIdDokLidhes').val());
    for (var j = 0; j < grid_dokLidhes.cpNoRows; j++) {
        if (grid_dokLidhes._isRowSelected(j)) {
            editorNiveli1 = Utils.ktheKontroll('lblNiveliLidhes' + j);
            editorNrDokumenti1 = Utils.ktheKontroll('lblNrDokumentiLidhes' + j);
            editorDtDokumenti1 = Utils.ktheKontroll('lblDtDokumentiLidhes' + j);
            editorVleftaPaLikujduar1 = Utils.ktheKontroll('lblVleftaPaLikujduarLidhes' + j);
            editorVlefta1 = Utils.ktheKontroll('txtVleftaLidhes' + j);
            editorMonedha1 = Utils.ktheKontroll('lblMonedhaLidhes' + j);
            editorKursi1 = Utils.ktheKontroll('lblKursiLidhes' + j);
            arrNiveli1[counter1] = editorNiveli1.GetText();
            arrNrDokumenti1[counter1] = editorNrDokumenti1.GetText();
            arrDtDokumenti1[counter1] = editorDtDokumenti1.GetText();
            arrVleftaPaLikujduar1[counter1] = editorVleftaPaLikujduar1.GetText();
            arrMonedha1[counter1] = editorMonedha1.GetText();
            arrKursi1[counter1] = editorKursi1.GetText();
            arridlidhes[counter1] = arrid[counter1];
            totalilidhes = mbledhje(totalilidhes, parseFloat(editorVlefta1.GetText()));
            counter1 = counter1 + 1;
        }
    }
    hidField8.val(JSON.stringify(arrNiveli1));
    hidField9.val(JSON.stringify(arrNrDokumenti1));
    hidField10.val(JSON.stringify(arrVleftaPaLikujduar1));
    hidField12.val(JSON.stringify(arrDtDokumenti1));
    hidField13.val(JSON.stringify(arrMonedha1));
    hidField14.val(JSON.stringify(arrKursi1));
    $('#hfIdDokLidhes').val(JSON.stringify(arridlidhes));
}

/*
Function: isValid

kontrollon vlefshmerine e dokumentit te lidhjes (a ka numer, a ka te pakten nje dokument kryesor, a ka te pakten nje dokument vartes)
*/
function isValid() {
    if (nrLidhje_TextBox.GetText() == "") {
        myMesazh.ShtoMesazhGabimi( hfState.Get("msgLidhjaDokShenoNrElidhjes"));
        return false;
    }
    else if (grid_dokKryesor._getSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi( hfState.Get("msgLidhjaDokZgjidhTePaktenNjeDokKryesor"));
        return false;
    }
    else if (dtDokumenti_DateEdit.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokZgjidh1DateDokumenti"));
        return false;
    }
    else if (dtRegjistrimi_DateEdit.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokZgjidh1DateRegjistrimi"));
        return false;
    }
    else if (grid_dokLidhes._getSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokZgjidh1DokumentLidhes"));
        return false;
    }
    else {
        //kontrollohet nese veprimi nuk eshte i kuadruar
        // kuadrimi ndodh nqs shuma e dokumentave kryesor eshte = me shumen e vlerave te dokumentave lidhes ne monedhe dokumenti kryesor
        var shumadokkryesor = 0;
        var shumadoklidhesfatura = 0;
        for (var i = 0; i < grid_dokKryesor.cpNoRows; i++) {
            if (grid_dokKryesor._isRowSelected(i)) {
                editorVlefta = Utils.ktheKontroll('txtVlefta' + i);
                shumadokkryesor = shumadokkryesor + parseFloat(editorVlefta.GetText());
            }
        }
        for (var j = 0; j < grid_dokLidhes.cpNoRows; j++) {
            if (grid_dokLidhes._isRowSelected(j)) {
                editorVleftaMonDok = Utils.ktheKontroll('txtVleftaMonBazeLidhes' + j);
                shumadoklidhesfatura = shumadoklidhesfatura + parseFloat(editorVleftaMonDok.GetText());
            }
        }
        if (shumadokkryesor.toFixed(2) == shumadoklidhesfatura.toFixed(2))
            return true;
        else {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgLidhjaDokVeprimiNukEshteIKuadruar"));
            return false;
        }
    }
}

/*
Function: RuajClick

Therret funksionet <merrTeDhena> dhe <merrTeDhena1> per te marre te dhenat e dokumentave dhe per te bere ruajtjen e lidhjes
*/
function RuajClick(s, e) {
    merrTeDhena();
    merrTeDhena1();
    begincallbackKrye();
    if (isValid()) {
        pastro();
    }
}

function shfaqKlientetFurnitoret() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhKF"), 'LupaKlientFurnitor.aspx', 800, 560);
}