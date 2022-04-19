;

var identikuesPerPopupNenGrupeLlogari;
var identikuesPerPopupGrupeLlogari;
var identifikuesperKPF;
var identifikuesperKategoriShpenzimi;
var identifikuesPerPopup;

jQuery(document).ready(function () {
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
        Utils.resizeSplitter();
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



function enableDisableButona(gjendjaEButonave)
{
 
	btnDjathtasGjitha.SetEnabled(gjendjaEButonave);
	btnMajtaGjitha.SetEnabled(gjendjaEButonave);
	btnDjathtas1.SetEnabled(gjendjaEButonave);
	btnMajtas1.SetEnabled(gjendjaEButonave);
	btnSiper.SetEnabled(gjendjaEButonave);
	btnPoshte.SetEnabled(gjendjaEButonave);
}




function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('Shto_KonfigurimFormatImporti.aspx', 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}


function Init() {//po
    if (typeof (isPostBack) == "undefined") {
        myMenu.menuSipasTeDrejtaRegjistrimPaDraft($("input[id$='hfShtimModifikim']"), hfTeDrejta);
        var hf = document.getElementById("hfKonffillestar");
        cmbKonfigurimi.SetText(hf.value);
        ndryshoKonfigurimin();
        enableDisableButona(!hfState.Get("FormatLidhurSQL"));
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}


/*
Function: callWebserviceKonfigurimi
    
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {//po
    try {
        var idGjuha = hfState.Get('idGjuha');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
        }).done(SucceededCallbackKonfig);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }
}

var colKushte; var colAlterKusht;

function SucceededCallbackKonfig(result) {//po
    var hf = $("input[id$='hfShtimModifikim']");
    var colKontrollet = result.colKontrollet;
    var colAtrTrupi = result.colAtrTrupi;
    var arrTabela = ['tblFillim'];
    var arrPrind = ["dvFillim"];
    var hfLidhur = $("input[id$='hfLidhur']");
    //myJQGrid.SucceededCallbackKonfig(result, hf, hfLidhur, arrTabela, arrPrind, "ASPxSplitter1_");
    //myJQGrid.SucceededCallbackKonfigSlim(result, hf, hfLidhur, arrTabela, arrPrind, "ASPxSplitter1_");
    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible';
    $("#dvFillim").show();//$("#dvFillim")[0].style.visibility = 'visible';
    //$("#dvFillim")[0].style.display = '';
    var colGrida = result.colGrida;
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    $('#HfGridCol').val(JSON.stringify(colGrida));
}

/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {//po
    if (cmbKonfigurimi.GetText().split(';').length > 1)
        lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(175, cmbKonfigurimi.GetText());
}


function ndryshoKategori(s, e) {//po

    //s.PerformCallback();
    e.processOnServer = true;
    //cmbKategoria.SetValue(2);
    //var a = cmbKategoria.GetValue();
    //cmbKategoria_UpdatePanel3ET.PerformCallback();
    //  s.PerformCallback(a);
    //cmbKategoria_UpdatePanel3ET.PerformCallback();

}
/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    gvZgjedhur.UnselectAllRowsOnPage();
    gvZgjedhur.PerformCallback('pastro');
    lbxFushat.PerformCallback('pastro');
}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    //cmbKategoria.SetText('');
    //cmbKategoria.SetValue(0);
  //  cmbKategoria.SetSelectedIndex(-1);
    cmbKategoria.PerformCallback();
    txtKodi.SetText('');
    txtShenime.SetText('');
    var hf = document.getElementById("status1");
    hf.value = "false";
}

/*
Function: isValidKoka
    
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi("Po transferohen te dhenat, shypni perseri ruaj pas disa sekondash!");
        return false;
    }
    else if (txtKodi.GetText() === "") {
        myMesazh.ShtoMesazhGabimi("Shenoni kodin!");
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
    if ($("#hfShtimModifikim").val() == "modifikim")
        cmbKategoria.SetEnabled(false);

    if (hf.value === "true") {
        myFaqeCelje.kontrolloTeDrejta('Shto_KonfigurimFormatImporti.aspx?shtim_modifikim=shtim', true);
    }
    UpdateButtonState();
    Utils.hiqLoadingGif();;    
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name === 'Ruaj') {
        var isValid = myFaqeCelje.validim(s, e);
        if (isValid && isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
        }
    }
    else if (e.item.name === "Eksporto") {
        e.processOnServer = false;
        myButtonClickLupa.LupaUniversal_Click('Eksportimi', 'LupaEksportim.aspx?id=' + Utils.getUrlVar("id") + '&komponente=Shto_KonfigurimFormatImporti.aspx', 600, 400);
    }
    else if (e.item.name === 'Klono') {
    	KlonoClick(e);
    }
    else if (e.item.name === 'Shto') {
    	myFaqeCelje.kontrolloTeDrejta('Shto_KonfigurimFormatImporti.aspx?shtim_modifikim=shtim', true);
    	e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
    	popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name === 'Anullo') {
    	myFaqeCelje.kontrolloTeDrejta('KonfigurimFormatImporti.aspx');
    	e.processOnServer = false;
    	click = false;
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('KonfigurimFormatImporti.aspx?ruaj=po');

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
    if (isValidKoka()) {


    }
    else {
        e.processOnServer = false; click = false;
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
    var hf = document.getElementById("hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    
    ndryshoKonfigurimin();
    hf.value = "shtim";
    hfState.Set("FormatLidhurSQL", false);
    myMenu.menuSipasTeDrejtaRegjistrimPaDraft($("input[id$='hfShtimModifikim']"), hfTeDrejta);
    $('#ASPxSplitter1_hl').empty();
    UpdateButtonState();
}

function KlonoClick(e) {
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    ASPxMenu1.GetItemByName('Eksporto').SetVisible(false);
    var hf1 = document.getElementById("hfShtimModifikim");
    e.processOnServer = false;
    hf1.value = "klonim"; myMenu.menuSipasTeDrejtaRegjistrimPaDraft($("input[id$='hfShtimModifikim']"), hfTeDrejta);
    ndryshoKonfigurimin();
    hfState.Set("FormatLidhurSQL", false);
    enableDisableButona(true);

}
function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}


function UpdateButtonState() {

    btnDjathtasGjitha.SetEnabled(lbxFushat.GetItemCount() > 0 && !hfState.Get("FormatLidhurSQL"));
    btnMajtaGjitha.SetEnabled(gvZgjedhur.GetVisibleRowsOnPage() > 0 && !hfState.Get("FormatLidhurSQL"));
    btnDjathtas1.SetEnabled(lbxFushat.GetSelectedItems().length > 0 && !hfState.Get("FormatLidhurSQL"));
    btnMajtas1.SetEnabled(gvZgjedhur.GetSelectedRowCount() > 0 && !hfState.Get("FormatLidhurSQL"));
    btnSiper.SetEnabled(gvZgjedhur.GetFocusedRowIndex() > 0 && gvZgjedhur.GetVisibleRowsOnPage() > 0 && !hfState.Get("FormatLidhurSQL"));
    btnPoshte.SetEnabled(gvZgjedhur.GetFocusedRowIndex() >= 0 && gvZgjedhur.GetVisibleRowsOnPage() > 0 && gvZgjedhur.GetFocusedRowIndex() < gvZgjedhur.GetVisibleRowsOnPage() - 1 && !hfState.Get("FormatLidhurSQL"));
    gvZgjedhur.MakeRowVisible(gvZgjedhur.GetFocusedRowIndex());
}

function disablebtn() {
    Utils.shfaqLoadingGif();;
}

function TextChangedKategoria() {
    UpdateButtonState();
}

function TextChangedEmri(editor, field, key) {
         var a = 1;
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajFusha"),
            data: JSON.stringify({ vlera: editor.GetText(), fusha: field, index: key })
        }).done(function () {
            console.log("OK");
        })
}

function CheckedChanged(editor, field, key) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajFusha"),
            data: JSON.stringify({ vlera: editor.GetChecked(), fusha: field, index: key })
        }).done(function () {
            console.log("OK");
        })
    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }

}
function ButtonClickedSKA(editor, key) {
    editorSKA = editor;
    keySK = key;
    identifikuesPerPopupSkema = "Import";

    myButtonClickLupa.LupaUniversal_Click('Zgjidh skemen e kontabilitetit te artikullit', 'LupaSkemaKontabelArtikulli.aspx', 700, 560);

}

function ButtonClickedKF(editor, key) {
    editorKF = editor;
    identikuesPerPopupKlientFurnitori = "Import";
    switch (cmbKategoria.GetText()) {
        case "Shitje":
            url = 'LupaKlientFurnitor.aspx?veprimi=1';
            break;
        case "Blerje":
            url = 'LupaKlientFurnitor.aspx?veprimi=2';
            break;
        default:
            url = 'LupaKlientFurnitor.aspx';
            break;
    }

    myButtonClickLupa.LupaUniversal_Click('Zgjidh klient/furnitorin', url, 700, 560);
}

function ButtonClickedKF1(editor, key) {
    editorKF = editor;
    identikuesPerPopupKlientFurnitori = "Import";
    if (cmbKategoria.GetValue() == 12)
        url = 'LupaKlientFurnitor.aspx?veprimi=1';
    else
        if (cmbKategoria.GetValue() == 31)
            url = 'LupaKlientFurnitor.aspx?veprimi=2';
        else url = 'LupaKlientFurnitor.aspx';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh klient/furnitorin', url, 700, 560);
}

function ButtonClickedShitjeBlerje(editor, key) {
    editorKF = editor;
    identikuesPerPopupKlientFurnitori = "Import";
    if (cmbKategoria.GetValue() == 1)
        url = 'LupaKlientFurnitor.aspx?veprimi=1';
    else
        if (cmbKategoria.GetValue() == 2)
            url = 'LupaKlientFurnitor.aspx?veprimi=2';
        else url = 'LupaKlientFurnitor.aspx';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh klient/furnitorin', url, 700, 560);
}
function ButtonClickedKarta(editor, key) {
    editorKF = editor;
    editorGlobal = editor;
    identifikuesPerPopupKartaKlient = "Import";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh karten', 'LupaKartaKlienti.aspx', 700, 560);


}

function ButtonClickedFormatSeriali(editor,key){
    editorKF = editor;
    editorGlobal = editor;
    editorFormatSeriali = "Serial";
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni formatin e serialit", 'Shto_FormatSeriali.aspx?lupe=true', 700, 560);
}


function ButtonClickedSubjekti(editor, key, editorSubjekti) {
    editorKF = editor;
    var mesazh = '';
    identikuesPerPopupKlientFurnitori = "Import";
    if (editorSubjekti.GetText() == '') {
        myMesazh.ShtoMesazhGabimi('Zgjidhni llojin e subjektit');
    }
    else {
        switch (editorSubjekti.GetText()) {
            case "Klient":
                url = 'LupaKlientFurnitor.aspx?veprimi=1';
                mesazh = 'Zgjidh Klientin';
                break;
            case "Furnitor":
                url = 'LupaKlientFurnitor.aspx?veprimi=2';
                mesazh = 'Zgjidh Furnitorin';
                break;
            case "Llogari":
                url = 'LupaLlogaria.aspx';
                mesazh = 'Zgjidh Llogarine';
                break;
            case "Punonjes":
                url = 'LupaPunonjes.aspx?vjenNga=Import';
                mesazh = 'Zgjidh Punonjes';
                break;
        }
        myButtonClickLupa.LupaUniversal_Click(mesazh, url, 700, 560);
    }
}

function ButtonClickedKodifikim(editor, key, lloj) {
    editorGlobal = editor;
    identifikuesPerPopupKodifikimin = "Import";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh grupin e artikullit', 'LupaKodifikimArtikulli.aspx?llojKodifikimi=' + lloj, 700, 560);
}

function DetajimArtikulli_Click(s) {
    identifikuesPerPopupDetajime = "Import";
    var veprimi = "";
    editordetajimi = s;
    queryStr = '';
    myButtonClickLupa.DetajimArtikulli_Click(queryStr, 700, 560, veprimi);
}

var llojkodifikimi = 1;
function PrindKodifikim_Click(editor, key) {
    editorGlobal = editor;
    identifikuesPerPopupKodifikimin = "Import";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh prindin', 'LupaKodifikimArtikulli.aspx?llojartikulli=false&llojKodifikimi=' + llojkodifikimi, 700, 560);
}

function PrindGrupeKF_Click(editor, key) {
    editorGlobal = editor;
    var llojikf = Utils.getUrlVar("kf");
    identifikuesPerPopupKodifikimin = "Import";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh prindin',  'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + llojkodifikimi+ '&kf=' + llojikf, 700, 560);
}

function changeLlojKodifikimi(editor, field, key) {
    llojkodifikimi = editor.GetText();
    switch (llojkodifikimi) {
        case "Grupimi 1":
            llojKodifikimi = 1;
            break;
        case "Grupimi 2":
            llojKodifikimi = 2;
            break;
        case "Grupimi 3":
            llojKodifikimi = 3;
            break;
        default:
            llojKodifikimi = 0;
            break;
    }

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajFusha"),
            data: JSON.stringify({ vlera: editor.GetText(), fusha: field, index: key })
        }).done(function () {
            console.log("OK");
        })
}

function ButtonClickedMaturimi(editor, key) {
    identikuesPerPopupAfateMaturimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.AfateMaturimi_Click('Afate Maturimi','', 700, 560, llojklient);
}

var llojklient = 'Klient';
function changeKlientFurnitor(editor, key) {
    llojklient = editor.GetText();
}

function ButtonClickedEntiteti(editor, key) {
    var llojVeprimi = cmbLlojVeprimiPerfitimBuxheti.GetText();
    switch(llojVeprimi){
        case "Shitje":
        case "Blerje":
            identikuesPerPopupKlientFurnitori = "Import";
            editorKF = editor;
            myButtonClickLupa.LupaUniversal_Click('Zgjidh klient/furnitorin', 'LupaKlientFurnitor.aspx?KlientapoFurnitor=' + llojVeprimi == "Shitje" ? 'Klient' : 'Furnitor', 700, 560);      
            break;
        case "Arketim":
        case "Pagese":
            identifikuesPerPopupBanka = "Import";
            editorGlobal = editor;
            myButtonClickLupa.LupaUniversal_Click('Zgjidh arken', 'LupaBanka.aspx?arkabanka=3&arka=false', 700, 560);
            break;
        default:
            break;
    }
}

function ButtonClickedLL(editor, key) {
    if (typeof (cmbLlojiPerfitimBuxheti) == "undefined") {
        editorLL = editor;
        identifikuesPerPopupLlogari = "Import";
        myButtonClickLupa.LupaUniversal_Click('Zgjidh llogarine', 'LupaLlogaria.aspx', 700, 560);
    }
    else
    switch (cmbLlojiPerfitimBuxheti.GetText()) {
        case "Llogari":
            editorLL = editor;
            identifikuesPerPopupLlogari = "ImportPerfitimBuxheti";
            myButtonClickLupa.LupaUniversal_Click('Zgjidh llogarine', 'LupaLlogaria.aspx', 700, 560);
            break;
        case "Artikull":
            identikuesPerPopupArtikulli = "Import";
            editorGlobal = editor;
            myButtonClickLupa.LupaUniversal_Click('Zgjidh artikullin', 'LupaArtikull.aspx?klasa=3', 700, 560);
            break;
        default:
            break;
    }
}

function ButtonClickedKategoriBuxhetimi(editor, key) {
    identifikuesPerPopup = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh artikullin/analizen e buxhetimit', 'B_KategoriBuxhetimi.aspx?lupe=true', 700, 560);
}

function changeArtikull(editor, field, key) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ruajFusha"),
        data: JSON.stringify({ vlera: editor.GetText(), fusha: field, index: key })
    }).done(function () {
        console.log("OK");
    })
    //if (editor.GetText() == 'Afatshkurter') {
    //    hfState.Set("Artikull", false);       
    //}
    //else {
    //    hfState.Set("Artikull", true);
    //}
}

function ButtonClickedKategoriZbritje(editor, key) {
    identifikuesPerPopupKodifikimin = "Import";
    editorGlobal = editor;
    myButtonClickLupa.KategoriZbritje_Click('Zgjidh kategori zbritje','', 700, 560);
}

function ButtonClickedNivelCmimi(editor, key, prind) {
    identifikuesPerPopupNiveliCmimi = "Import";
    editorGlobal = editor;
    if (prind)
        myButtonClickLupa.NivelCmimi_Click('Zgjidh nivel cmimi prind', '', 700, 560, llojklient);
    else myButtonClickLupa.LupaUniversal_Click('Zgjidh nivelin', 'LupaNivelCmimi.aspx', 700, 560);
}

function ButtonClickedNivelZbritje(editor, key, prind) {
    identifikuesPerPopupNiveli = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh nivelin', 'LupaNivelZbritje.aspx', 700, 560);
}

function ButtonClickedObjektiva(editor, key, prind) {
    identifikuesPerPopupNiveliCmimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh objektiven', 'LupaObjektivaKosto.aspx', 700, 560);
}

function ButtonClickedMagazina(editor, key, prind,konf) {
    identifikuesPerPopupMagazina = "Import";
    editorGlobal = editor;
   
    if (konf != "")
        myButtonClickLupa.LupaUniversal_Click('Zgjidh magazinen', 'LupaMagazina.aspx?idKonfigAmbjente=' + konf, 700, 560);
   else myButtonClickLupa.LupaUniversal_Click('Zgjidh magazinen', 'LupaMagazina.aspx', 700, 560);
}

function ButtonClickedPunonjes(editor, key, prind) {
    identifikuesPerPopupMagazina = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh punonjesin', 'LupaPunonjes.aspx?vjenNga=Import', 700, 560);
}
function ButtonClickedQendra(editor, key, prind) {
    identifikuesPerPopupNiveliCmimi = "Import";
    editorGlobal = editor;
    if (lloji == 1)
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni qendra e kostos', 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk', 900, 600);
    else myButtonClickLupa.LupaUniversal_Click('Zgjidhni skemen e kostos', 'LupaSkemaKosto.aspx', 600, 600);
}

var grupi = "";

function ButtonClickedGrupi(editor, key, prind) {
    identikuesPerPopupGrupeLlogari = "Import";
    editorGlobal = editor;
    //grupi = editor.GetText();
    myButtonClickLupa.Grupi_Click('Zgjidh grupin', 1, 600, 600);
}

function ButtonClickedNengrupi(editor, key, prind) {
    identikuesPerPopupNenGrupeLlogari = "Import";
    editorGlobal = editor;
    if (grupi == "")
        myMesazh.ShtoMesazhGabimi('Zgjidhni grupin e llogarise!');
    else
        myButtonClickLupa.Nengrupi_Click('Zgjidh nengrupin', 1, 600, 600, grupi);
}

function ButtonClickedDep(editor, key, prind) {
    editorGlobal = editor;
    //grupi = editor.GetText();
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni departamentin', 'LupaStrukturaAdministrative.aspx?vjenNga=ImportDep', 600, 600);
}

function ButtonClickedNendep(editor, key, prind) {
    editorGlobal = editor;
    if (grupi == "")
        myMesazh.ShtoMesazhGabimi('Zgjidhni departamentin!');
    else
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni nendepartamentin', 'LupaStrukturaAdministrative.aspx?vjenNga=ImportNenDep&IdPrindi='+grupi, 600, 600);
}
function ButtonClickedQK1(editor, key, prind) {
    editorGlobal = editor;
    //grupi = editor.GetText();
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni qendren e kostos', 'Shto_QendraKosto.aspx?lupe=true&llojLupe=prind&vjenNga=PunonjesQK1Import', 900, 600);
}

function ButtonClickedQK2(editor, key, prind) {
    editorGlobal = editor;
    if (grupi == "")
        myMesazh.ShtoMesazhGabimi('Zgjidhni qendren e kostos 1!');
    else
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni qendren e kostos', 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=PunonjesQK2Import&idPrindi=' + (grupi == null ? 0 : grupi), 900, 600);
}
var global;
function ButtonClickedGlobal(editor, key, prind) {
    editorGlobal = editor;
    //grupi = editor.GetText();
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni grupimin', 'LupaGrupimeLocaleGlobale.aspx?vjenNga=PunonjesGlobaleImp', 600, 600);
}

function ButtonClickedLocal(editor, key, prind) {
    editorGlobal = editor;
    if (global == "")
        myMesazh.ShtoMesazhGabimi('Zgjidhni grupimin global!');
    else
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni grupimin', 'LupaGrupimeLocaleGlobale.aspx?vjenNga=PunonjesLocaleImp&idPrindi=' + (global == null ? 0 : global), 600, 600);
}
function ButtonClickedKpf1(editor, key, prind) {
    identifikuesperKPF = "Import";
    editorGlobal = editor;
    myButtonClickLupa.KPF_Click('Zgjidh llogarine standarte', 1, 600, 600, 1);
}

function ButtonClickedKpf2(editor, key, prind) {
    identifikuesperKPF = "Import";
    editorGlobal = editor;
    myButtonClickLupa.KPF_Click('Zgjidh llogarine standarte', 1, 600, 600, 2);
}

function ButtonClickedKpf3(editor, key, prind) {
    identifikuesperKPF = "Import";
    editorGlobal = editor;
    myButtonClickLupa.KPF_Click('Zgjidh llogarine standarte', 1, 600, 600, 3);
}

function ButtonClickedKategoriShpenzimi(editor, key, prind) {
    identifikuesperKategoriShpenzimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni kategorine e shpenzimit', 'LupaKategoriShpenzimi.aspx', 600, 600);
}

var lloji = 1;
function MerrVlere(editor, key, prind) {
    lloji = editor.GetValue();
}

function ButtonClickedNivelZbritje(editor, key) {
    identifikuesPerPopupNiveliZbritje = "Import";
    editorGlobal = editor;
    myButtonClickLupa.NivelZbritje_Click('Zgjidh nivel zbritje prind','', 700, 560);
}
var editorGlobal;
function ButtonClickedBankat(editor, key) {
    identifikuesPerPopupBanka = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh banken/arken', 'LupaBanka.aspx', 700, 560);
}

function ButtonClickedArt(editor, key) {
    identikuesPerPopupArtikulli = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh artikullin', 'LupaArtikull.aspx'+((cmbKategoria.GetValue()==93)?'?aqt=aqt':''), 700, 560);
}

function ButtonClickedGrupKf(editor, key, llojGrupi) {
    identifikuesPerPopupKodifikimin = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh grupin e klient/furnitorit', 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + llojGrupi + '&kf=' + llojklient.toLowerCase(), 700, 500);
}

function ButtonClickedRole(editor, key, prind) {
    identifikuesPerPopupNiveliCmimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh rolin', 'LupaRole.aspx?vjenNga=Importi', 600, 560);
}

function ButtonClickedStatus(editor, key, prind) {
    identifikuesPerPopupNiveliCmimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Status', 'LupaShopsHierarkiStatus.aspx?vjenNga=Importi', 600, 560);
}

function ButtonClickedLeaveReason(editor, key, prind) {
    identifikuesPerPopupNiveliCmimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Leave Reason', 'LupaShopsHierarkiLeaveReason.aspx?vjenNga=Importi', 600, 560);
}

function ButtonClickedUniform(editor, key, prind) {
    identifikuesPerPopupNiveliCmimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Uniforme', 'LupaShopsHierarkiUniform.aspx?vjenNga=Importi', 600, 560);
}

function ButtonClickedAtomjet(editor, key, llojGrupi) {
    identifikuesPerPopupAutomjet = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh automjetin', 'LupaAutomjeti.aspx', 700, 500);
}

function ButtonClickedTransportues(editor, key, llojGrupi) {
    identikuesPerPopupTransportuesi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Transportues', 'Shto_Transportues.aspx?lupe=true', 1070, 750);
}

function ButtonClickedAgjenti(editor, key, llojGrupi) {
    identikuesPerPopupAgjenteShitje = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Agjentin', 'LupaAgjenteShitje.aspx?', 700, 500);
}

function SelectedIndexChangedNenkategoria(editor, key, s, e) {
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdoruesi = hfState.Get('idPerdoruesi');    
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKonfigurimetENivelit"),
        data: JSON.stringify({ kodNiveli: editor.GetText(), idKategori: cmbKategoria.GetValue(), idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje, idGjuha: hfState.Get('idGjuha') })
    }).done(SucceededCallbackNiveli);
}

function SucceededCallbackNiveli(colModelet) {
    var kodKonfig = txtVleraLlojDokumenti.GetText();
    txtVleraLlojDokumenti.ClearItems();
    for (i = 0; i < colModelet.length; i++) {        
        txtVleraLlojDokumenti.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente);
        if (colModelet[i].KodKonfigAmbjente == kodKonfig)
            txtVleraLlojDokumenti.SetText(colModelet[i].KodKonfigAmbjente);
    }
}

function SelectedIndexChangedLlojDokumenti(editor, key, s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheBuxhetimViteSipasKonfigurimit"),
        data: JSON.stringify({ kodKonfigAmbjente: txtVleraLlojDokumenti.GetText() })
    }).done(SucceededCallbackLlojDokumenti);
}

function SucceededCallbackLlojDokumenti(results) {
    var value = cmbViteBuxheti.GetValue();
    cmbViteBuxheti.ClearItems();
    var viti = results.viti;
    var nrvitesh = results.nrVitesh;
    cmbViteBuxheti.AddItem([""], 0);
    cmbViteBuxheti.AddItem([viti], viti);
    for (var i = 1; i < nrvitesh; i++) {
        cmbViteBuxheti.AddItem([viti + i], viti + i);
    }
    if (cmbViteBuxheti.FindItemByValue(value))
        cmbViteBuxheti.SetValue(value);
    else
        cmbViteBuxheti.SetSelectedIndex(0);
}


function ButtonClickedPerdorues(editor, key) {
    identifikuesPerPopupPerdorues = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh perdoruesin', 'LupaPerdorues.aspx', 700, 560);
}

function ButtonClickedNjesiProdhimi(editor, key) {
    identifikuesPerPopupNjesiProdhimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Njesi Prodhimi', 'LupaNjesiProdhimi.aspx', 700, 560);
}

function Autorizimi_Click(editor, key) {
    identifikuesPerPopupAutorizimi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Autorizimin', 'LupaAutorizim.aspx', 700, 560);
}