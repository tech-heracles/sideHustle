;

var lidhur = false;

var arrayMeMagazina = new Array();
var colMagazina;
var magazina1;
var magazina2;
var njesiaZgjedhur;
var totaletSasiveDetajimeve = new Array();

var arrMeNjesi = new Array();
var widthLupaArtikull = 700;
var heightLupaArtikull = 600;
var widthLupaKF = 600;
var heightLupaKF = 600;
var widthLupaMakro = 600;
var heightLupaMakro = 600;
var widthLupaMagazina = 600;
var heightLupaMagazina = 600;
var widthLupaPeriudha = 600;
var heightLupaPeriudha = 600;
var widthLupaKerko = 600;
var heightLupaKerko = 600;
var widthLupaDetajim = 600;
var heightLupaDetajim = 600;
var editorData;

var STR_sasiaNumer = 'Sasia duhet të jetë numer!';
//anullon veprimin e enterit
/* function enter() {
if (window.event.keyCode == 13) {
event.returnValue = false;
event.cancel = true;

}
}*/

jQuery(document).ready(function () {
    /*
Function: 
ekzekutohet sa here i behet resize faqes, dhe ben resize te grides
*/
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(145))
                return;
        }
        catch (ee) {
        }

    }).trigger('resize');
    //formoArrayKolonaGrides();

    var isLidhur = ($("input[id$='hfLidhur']").val().toLowerCase() === 'true');

    
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
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();

    });
    //jQuery("#rowed5").hideCol("txtMagazina2");        
});





var indeksi = -1;
var indexCounter;
var identifikuesPerPopupDokumentat;
var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupArtikulli;
var identifikuesPerPopupDetajime;
var identifikuesPerPopupMakro;
var identifikuesPerPopupMagazina;
var NivelCmimi; // variabel qe mban nivelin e cmimit qe i eshte caktuar klientit qe kemi zgjedhur

//var arrDetajimetSelektuara = new Array();

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimRiparime.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimRiparime.aspx', Utils.getUrlVar('id'));
       
    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}

var dtDokumentit;
/*
Function: ValidoDateDokumenti

Kontrollon nese data e dokumentit i perket periudhes aktuale
*/
function ValidoDateDokumenti() {
    var vleraLabel = lblPeriudhaAktuale.GetText();
    var periudha = vleraLabel.split("-");
    var dataDok = dteDtDok.GetText();
    periudha1 = periudha[0].split("/");
    periudha2 = periudha[1].split("/");
    dtDokumentit = dataDok.split("/");
    if (periudha1[2] != dtDokumentit[2])
        return false;
    else {
        if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
            return false;
    }
    return true;
}

/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {
    var niveli;
    if (cmbLloji.GetText() != "")
        niveli = cmbLloji.GetValue();
    else niveli = 1;
    var queryString = {
        veprimi: 'RegjistrimRiparimi',
        niveli: niveli,
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click('Zgjidh dokumentin', 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);

}

function Init() {
    if (typeof (isPostBack) == "undefined") {
        editorData = dteDtDok;
        //    callWebserviceKonfigurimi("510"); //510 = id komponente (Shto_RegjistrimMagazine.aspx)
        var hf = document.getElementById("hfKonffillestar");

        cmbKonfigurimi.SetText(hf.value);
        ndryshoKonfigurimin();
        identifikuesPerPopupDokumentat = "RegjistrimRiparime.aspx";
        identikuesPerPopupKlientFurnitori = "RegjistrimRiparime";
        identikuesPerPopupArtikulli = "RegjistrimRiparime";
        identifikuesPerPopupDetajime = "RegjistrimRiparime";
        identifikuesPerPopupMakro = "RegjistrimRiparime";
        identifikuesPerPopupMagazina = "RegjistrimRiparime"
        changeName();
        myMesazh.shtoHandler();
       
        shfaqSwap(false);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}

/*
Function: callWebserviceKonfigurimi
    
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    try {
        var idGjuha = hfState.Get('idGjuha');
      $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarje'), idGjuha: idGjuha })
        }).done(SucceededCallbackKonfig);

    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }
}
function DateChanged(s, e) {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);

    if ($('#hfShtimModifikim').val() == 'shtim' && grid_faturat.GetVisibleRowsOnPage() > 0) grid_faturat.GetRowValues(0, "DtMbarimi;Loan;Kontakti", onCallback);

}
function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}
var kushtet; var colKushte; var colAlterKusht;
var fokusi = 0; var info = false;
var colGrida;
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontrollet;
    var colAtrTrupi = result.colAtrTrupi;
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);

    if (hf.val() == "shtim") {
        $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible'; $("#divgride1")[0].style.display = '';
        $("#divgride2").show();//$("#divgride2")[0].style.visibility = 'visible'; $("#divgride2")[0].style.display = '';
        $("#divgride5").hide();//$("#divgride5")[0].style.visibility = 'hidden'; $("#divgride5")[0].style.display = 'none';
        $("#divgride6").hide();//$("#divgride6")[0].style.visibility = 'hidden'; $("#divgride6")[0].style.display = 'none';
    }
    else {
        $("#divgride1").hide();//$("#divgride1")[0].style.visibility = 'hidden'; $("#divgride1")[0].style.display = 'none';
        $("#divgride2").hide();//$("#divgride2")[0].style.visibility = 'hidden'; $("#divgride2")[0].style.display = 'none';
        $("#divgride5").show();//$("#divgride5")[0].style.visibility = 'visible'; $("#divgride5")[0].style.display = '';
        $("#divgride6").show();//$("#divgride6")[0].style.visibility = 'visible'; $("#divgride6")[0].style.display = '';
    }
    $("#dvFillim").show();//$("#dvFillim")[0].style.visibility = 'visible';
    //$("#dvFillim")[0].style.display = '';
    $("#dvFundi").show();//$("#dvFundi")[0].style.visibility = 'visible';
    //$("#dvFundi")[0].style.display = '';
    // $("#divfund1")[0].style.visibility = 'visible';
    vendosDateDefault();
    NivelCmimi = 0;

    colGrida = result.colGrida;
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    var kodniveli = result.kodniveli;
    $('#HfGridCol').val(JSON.stringify(colGrida));
    //var kontrollet = vlerat[0].split(';');
    //kushtet = vlerat[2].split(';');

    var hfMag = $("#hfLupaMagazina")[0];
    var hfrivleresim = $('#hfKontrollRivleresim');
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet)); $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }


    for (var i = 0; i < colKontrollet.length - 1; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it

        if (colAtrTrupi[i].KodKontrolli == "btneMagazina")
            hfMag.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme      

    }
    btneMagazina.SetSelectedIndex(0);
    var modinfo = 0;
    //   btnMagazina.SetEnabled(false);
    //   btnMagazina.SetText();   
    for (j = 0; j < colKushte.length; j++) {

        if (colKushte[j].Kodi == 'KR') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfrivleresim.val(true);
            else
                hfrivleresim.val(false);
        }

    }

    //formoArrayKolonaGrides();
    var isLidhur = (hfLidhur.val().toLowerCase() === 'true');
    var lloji = cmbLloji.GetText();
    shfaqSwap(false);
}

/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    if (cmbKonfigurimi.GetText().split(';').length > 1)
        lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(542, cmbKonfigurimi.GetText());
}

/*
Function: TextChangedLloji
    
Ben ndryshime ne gride ne varesi te llojit te veprimit te zgjedhur (hyrje, Dalje apo Transferim)
*/
function TextChangedLloji() {
    var lloji = cmbLloji.GetText();

    var mod;
    if ($("#hfShtimModifikim")[0].value == 'modifikim')
        mod = true;
    else mod = false;
    if (cmbLloji.GetText() != "")
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'riparime', mod);
    else
        callWebserviceNiveliNew('', 'riparime', mod);
}



function callWebserviceNiveliNew(lloji, tipi, mod) {
    try {
      $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateteNivelit"),
            data: JSON.stringify({ lloji: lloji, veprimi: tipi, mod: mod })
        }).done(SucceededCallbackNiveliNew);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }
}
function SucceededCallbackNiveliNew(colModelet) {
    cmbKonfigurimi.ClearItems();
    for (i = 0; i < colModelet.length; i++) {
        cmbKonfigurimi.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    }
    cmbKonfigurimi.SelectIndex(0);
    ndryshoKonfigurimin();
}

var colNjesiVartese;



/*
Function: SucceededCallbackNiveli
    
Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function ButtonClickMagazina() {

    var hfKl = document.getElementById("hfLupaMagazina");
    var queryStr = hfKl.value;

    popupUniversal.SetHeaderText('Zgjidh magazinen');
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr);

    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

/*
Function: TextChangedMagazina
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina() {
    if ($('#hfShtimModifikim').val() == 'shtim' && cbLoan.GetChecked())
        gvArtLoan.PerformCallback('merr');

}

var panjesi = false;

/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {


}

/*
Function: resetCountera
    
Vendos vleren 0 tek te gjithe counter-at.
*/
function resetCountera() {

}



/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    //  cmbLloji.SetText('');
    txtIMEI.SetText('');
    btneMagazina.SetValue(null);;
   // if (btneMagazina.GetItemCount() == 1)
        btneMagazina.SetSelectedIndex(0);
    txtGaranci.SetText('');
    cmbDorezuar.SetValue(3);
    cmbStatus.SetSelectedIndex(0);
    cmbDifekti.SetSelectedIndex(0);
    txtPershkrimi.SetText('');
    txtNrKontakti.SetText('');
    lblProduktGaranci.SetText('');
    txtAksesor.SetText('');
    txtIMEISwap.SetText('');
    txtProdukti.SetText('');
    $('#hfIMEI').val('');
    $('#hfAksesor').val('');
    cbLoan.SetChecked(false);

    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();

}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim" || $("input[id$='hfShtimModifikim']").val() == "riparim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
            dteDtRegjistrimi.SetDate(dataSot);
        }
        catch (e) { }

    }
}

/*
Function: isValidKoka
    
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {
   if( !Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi("Po transferohen te dhenat, shypni perseri ruaj pas disa sekondash!");
        return false;
    }
    if (dteDtDok.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje date dokumenti!");
        return false;
    }
    else if (dteDtRegjistrimi.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje date regjistrimi!");
        return false;
    }
    else if (lblProduktGaranci.GetText() == 'Ky produkt eshte jashte afatit te garancise!') {
        myMesazh.ShtoMesazhGabimi("Ky produkt eshte jashte afatit te garancise");
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
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        popMesazhQK.Show();


    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos', $('#hfUrl').val(), 900, 600);


    }
    //  Utils.hiqLoadingGif();;
    if (hf.value == "true") {

        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimRiparimi.aspx?shtim_modifikim=shtim', true);

    } else click = false;
}
var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimRiparimi.aspx?shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
}
/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name == 'Ruaj'||e.item.name == 'RuajPrint') {
        var valid = myFaqeCelje.validim(s, e);
        if (!valid) {
            Utils.hiqLoadingGif();;
            e.processOnServer = false;
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            // myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }

    else if (e.item.name == 'Draft') {
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            //myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }
    else if (e.item.name == 'Kerko') {

        myFaqeCelje.kontrolloTeDrejta('RegjistrimRiparimi.aspx', null, true);
        e.processOnServer = false;
    }
    else if (e.item.name == 'Shto') {
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    }
    else if (e.item.name == 'Fshi') {
        popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name == 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimRiparimi.aspx');
        e.processOnServer = false;
        click = false;
    }

}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('RegjistrimRiparimi.aspx?ruaj=po');

}
var click = false;
/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    if (click) {
        e.processOnServer = false; Utils.hiqLoadingGif();;
        return;
    }
    click = true;
    if (isValidKoka()) {
        if (btneMagazina.GetText() == "") {

            myMesazh.ShtoMesazhGabimi("Zgjidhni dyqanin!");
            e.processOnServer = false;
            click = false;

        }
        if ($('#hfShtimModifikim').val() == "shtim" && cbLoan.GetChecked() && gvArtLoan.GetVisibleRowsOnPage() == 0) {
            myMesazh.ShtoMesazhGabimi("Nuk ka gjendje per artikuj loan!");
            e.processOnServer = false;
            click = false;
        }
        else
            if ($('#hfShtimModifikim').val() == "shtim" && cbLoan.GetChecked() && gvArtLoan.GetVisibleRowsOnPage() > 0) {
                var kaimei = false;
                for (var i = 0; i < gvArtLoan.cpRowCount; i++) {
                    editorIMEI = Utils.ktheKontroll('txtIMEI' + i);
                    if (editorIMEI.GetText() != ""&& !kaimei) {
                        $('#hfIMEI').val(editorIMEI.GetText());
                        $('#hfAksesor').val(Utils.ktheKontroll('txtAksesor' + i).GetText());
                        $('#hfrreshti').val(i);
                        kaimei = true;
                    }
                    else if (editorIMEI.GetText() != "" && kaimei) {
                        myMesazh.ShtoMesazhGabimi("Nuk mund te zgjidhni me shume sesa nje artikull loan!");
                        e.processOnServer = false;
                        click = false;
                        return;
                    }

                }
          
         


            if (!kaimei) {
                myMesazh.ShtoMesazhGabimi("Zgjidhni IMEI e artikullit loan!");
                e.processOnServer = false;
                click = false;
            }
        }
    }

    else {
        e.processOnServer = false; click = false; Utils.hiqLoadingGif();;
    }
}

/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    $("input[id$='hfLidhur']").val(false);
    pastro();
    pastroFushatKokes(); //gvArtLoan.PerformCallback("pastro");
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    // ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
    //ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false); 
    //ASPxMenu1.GetItemByName('Fshi').SetVisible(false);// ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    hf.val("shtim"); myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    //            if (hf.value == "modifikim") 
    cmbLloji.SetText('RF');
    TextChangedLloji();
 grid_faturat.PerformCallback("pastro");//gvArtLoan.PerformCallback("pastro");
    //  ndryshoKonfigurimin(); //duhet kur klijkojme butonin shto ne rastin kur kemi hap nje dok. per modifikim
    //            jQuery("#rowed5").GridUnload("rowed5");
    //            inicializoGride();
    //            mbushGrideNgaHiddenFieldet();
    $("#ASPxSplitter1_hl").empty(); click = false;
}


/*
Function: ShfaqPeriudhen

Hap lupen e priudhave.
*/
function ShfaqPeriudhen() {
    popupUniversal.SetHeaderText('Zgjidh periudhen kontabel');
    popupUniversal.SetContentUrl('LupaPeriudhaKontabel.aspx');
    popupUniversal.SetSize(widthLupaPeriudha, heightLupaPeriudha);
    popupUniversal.Show();
}

function lostFocusPeriudha(vlera) {
    //            if (vlera != '') {
    //                callBackPanel.PerformCallback('skeme,' + vlera);
    //            }
}

function valueChangedPeriudha() {
    var vleraLabel = lblPeriudhaAktuale.GetText();
    var periudha = vleraLabel.split("-");
    var dataDok = new Date();
    dataDok = formatDate(dataDok, "dd/MM/yyyy");

    periudha1 = periudha[0].split("/");
    periudha2 = periudha[1].split("/");
    dtDokumentit = dataDok.split("/");
    if (periudha1[2] != dtDokumentit[2])
        //return false;
        dteDtDok.SetText(periudha[0]);
    else {
        if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
            dteDtDok.SetText(periudha[0]);
        else
            dteDtDok.SetText(dataDok);
    }
}
function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var kerko = false;
function KerkoClick(s, e) {
    if (btneMagazina.GetText() == "") {
        myMesazh.ShtoMesazhGabimi('Zgjidhni dyqanin!');
        return;
    }
    kerko = true;
    grid_faturat.PerformCallback();
}
function kontrolloDateGarancie(s, e) {
    if (kerko) {
        lblProduktGaranci.SetText('');
        if (grid_faturat.GetVisibleRowsOnPage() == 0 && txtIMEI.GetText() != "")
        { myMesazh.ShtoMesazhGabimi('IMEI nuk u gjet!'); gvArtLoan.PerformCallback('pastro'); }
        else if (grid_faturat.GetVisibleRowsOnPage() == 0 && txtGaranci.GetText() != "")
          {  myMesazh.ShtoMesazhGabimi('Kodi i garancise nuk u gjet!'); gvArtLoan.PerformCallback('pastro'); }
        else if (grid_faturat.GetVisibleRowsOnPage() > 0) grid_faturat.GetRowValues(0, "DtMbarimi;Loan;Kontakti", onCallback);
        kerko = false;

    }  if (grid_faturat.GetVisibleRowsOnPage() == 0)gvArtLoan.PerformCallback('pastro');
}
function onCallback(result) {

    if (result != null) {
        if (result[0] < dteDtDok.GetDate()) {
            lblProduktGaranci.SetText('Ky produkt eshte jashte afatit te garancise!');
            lblProduktGaranci.mainElement.style.color = 'red';
        }
        else {
            lblProduktGaranci.SetText('Ky produkt eshte brenda afatit te garancise!');
            lblProduktGaranci.mainElement.style.color = 'green';
        }
        if (result[1] == "Po")
            cbLoan.SetChecked(true);
        else cbLoan.SetChecked(false);
        txtNrKontakti.SetText(result[2]);
        if ($('#hfShtimModifikim').val() == 'shtim' && cbLoan.GetChecked()) gvArtLoan.PerformCallback('merr');
    else if ($('#hfShtimModifikim').val() == 'shtim' && !cbLoan.GetChecked()) gvArtLoan.PerformCallback('pastro');
    }

}

function EndCallback(s, e) {

    if (grid_faturat.GetVisibleRowsOnPage() > 0 && cbLoan.GetChecked() && gvArtLoan.GetVisibleRowsOnPage() == 0) myMesazh.ShtoMesazhGabimi('Ky produkt nuk ka gjendje per loan!');

}

function TextChangedImei(key) {  
   // gvArtLoan.SelectRows(key);
    try {
        if (Utils.ktheKontroll('txtIMEI' + key).GetText() != "") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "KontrolloEkzistonIMEI"),
                data: JSON.stringify({ detajim: Utils.ktheKontroll('txtIMEI' + key).GetText(), idartikulli: gvArtLoan.GetRowKey(key), key: key, data: dteDtDok.GetDate(), idmag: btneMagazina.GetValue() })
            }).done(SucceededCallbackIMEI);
        }

    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }
}
function SucceededCallbackIMEI(result) {
    if (result[1] == "Nuk ekziston") {
        Utils.ktheKontroll('txtIMEI' + result[0]).SetText('')
        myMesazh.ShtoMesazhGabimi('Ky serial nuk ekziston!');
    }
    if (result[1] == "Jo Artikulli") {
        Utils.ktheKontroll('txtIMEI' + result[0]).SetText('')
        myMesazh.ShtoMesazhGabimi('Ky serial nuk i perket ketij artikulli!');
    }
    if (result[1] == "Jo Loan") {
        Utils.ktheKontroll('txtIMEI' + result[0]).SetText('')
        myMesazh.ShtoMesazhGabimi('Ky serial nuk eshte serial loan!');
    }

    if (result[1] == "Jo gjendje") {
        Utils.ktheKontroll('txtIMEI' + result[0]).SetText('')
        myMesazh.ShtoMesazhGabimi('Nuk ka gjendje per kete serial!');
    }
  
}

function StatusChanged(s, e) {
    try {
       $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKaAutorizimStatusi"),
            data: JSON.stringify({ idstatusi: s.GetValue() })
        }).done(SucceededCallbackStatusi);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }

}
function SucceededCallbackStatusi(result) {
    if (!result) {
        cmbStatus.SetText('')
        myMesazh.ShtoMesazhGabimi('Nuk keni te drejta per kete status!');
        return;
    }

    shfaqSwap(true);
}
function KontrolloIMEISwap(s, e) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKaGjendjeSwap"),
            data: JSON.stringify({ detajim: s.GetText(), data: dteDtDok.GetDate(), idmag: btneMagazina.GetValue() })
        }).done(SucceededCallbackSwap);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }

}
function SucceededCallbackSwap(result) {
    if (result[0] == "Nuk ekziston") {
        txtIMEISwap.SetText('')
        myMesazh.ShtoMesazhGabimi('Ky serial nuk ekziston!');
        return;
    }
    if (result[0] == "Jo Artikulli") {
        txtIMEISwap.SetText('')
        myMesazh.ShtoMesazhGabimi('Ky serial nuk i perket asnje artikulli!');
        return;
    }
    if (result[0] == "Loan") {
        txtIMEISwap.SetText('')
        myMesazh.ShtoMesazhGabimi('Ky serial eshte serial loan!');
        return;
    }

    if (result[0] == "Jo gjendje") {
        txtIMEISwap.SetText('')
        myMesazh.ShtoMesazhGabimi('Nuk ka gjendje per kete serial!');
        return;
    }

    txtProdukti.SetText(result[1]);
}
function shfaqSwap(ndryshodorezuar) {
    if (cmbStatus.GetText() == "Zevendesuar me aparat te ri") {
        txtIMEISwap.SetVisible(true);
        lblIMEISwap.SetVisible(true);
        txtProdukti.SetVisible(true);
        lblProdukti.SetVisible(true);

    }
    else {
        txtIMEISwap.SetVisible(false);
        lblIMEISwap.SetVisible(false);
        txtProdukti.SetVisible(false);
        lblProdukti.SetVisible(false);
        //txtIMEISwap.SetText('');
        //txtProdukti.SetText('');

    }
    if (cmbStatus.GetText() == "Aparati dorezuar Klientit" && grid_Loan.GetVisibleRowsOnPage() != 0) {
        cmbDorezuar.SetEnabled(true);
      if(ndryshodorezuar)  cmbDorezuar.SetSelectedIndex(0);

    }
    else {
        cmbDorezuar.SetEnabled(false);
        if (ndryshodorezuar) cmbDorezuar.SetValue(3);
    }
}
function Click_ButtonOkQK(s, e) {
    popMesazhQK.Hide();
    myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos', $('#hfUrl').val(),900,600);
}