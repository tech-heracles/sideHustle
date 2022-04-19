; $(document).ready(function (e) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idVitNdermarrje = hfState.Get("idVitNdermarrje");
    myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idVitNdermarrje);
    $(window).on('load', function () {
        Init();
    });
});





function changeName() {//po
    myFaqeCelje.shtoHandlerSession();

}


function Init() {//po
    if (typeof (isPostBack) == "undefined") {
        myMenu.menuSipasTeDrejtaRegjistrimPaDraft($("input[id$='hfShtimModifikim']"), hfTeDrejta);
        var hf = document.getElementById("hfKonffillestar");
        cmbKonfigurimi.SetText(hf.value);
        ndryshoKonfigurimin();
        UpdateButtonState();
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
    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible';
    $("#dvFillim").show();//$("#dvFillim")[0].style.visibility = 'visible';
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
    callWebserviceKonfigurimi(2002, cmbKonfigurimi.GetText());
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
    $('#hfShtimModifikim').val('shtim')
    txtKodi.SetText('');
    txtKodi.SetEnabled(true);
    txtShenime.SetText('');
    dteDtFillimi.SetText('');
    dteDtMbarimi.SetText('');
    var hf = document.getElementById("status1");
    hf.value = "false";
}

/*
Function: isValidKoka
    
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka(s,e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi("Po transferohen te dhenat, shypni perseri ruaj pas disa sekondash!");
        return false;
    }
    else if (txtKodi.GetText() === "") {
        myMesazh.ShtoMesazhGabimi("Shenoni kodin!");
        return false;
    }
    else if (dteDtMbarimi.GetDate() < dteDtFillimi.GetDate()) {
        myMesazh.ShtoMesazhGabimi("Data e mbarimit duhet te jete me e madhe se data e fillimit!");
        return false;
    }
    else if (dteDtMbarimi.GetText() == dteDtFillimi.GetText()) {
        myMesazh.ShtoPyetje("Data e mbarimit eshte e njejte me daten e fillimit. Doni te vazhdoni?", false);
        e.processOnServer = false;
        click = false;
        return false;
    }
    else return true;
}
function JoClick(s, e) {//nese nuk do 2 rreshta me artikull njesoj
    click = false;

}
function PoClick(s, e) {//po

    var isValid = myFaqeCelje.validim(s, e);
    if (isValid && isValidKoka()) {
        Utils.shfaqLoadingGif();;
        RuajClick(s, e);
    }
    else {
        e.processOnServer = false;
    }
  
   // btn.DoClick();

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
        myFaqeCelje.kontrolloTeDrejta('CRMAnketa.aspx?shtim_modifikim=shtim', true);
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
        if (isValid && isValidKoka(s,e)) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
        }
    }

    else if (e.item.name === 'Pastro') {
        myFaqeCelje.kontrolloTeDrejta('CRMAnketa.aspx?shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
        popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name === 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('CRMListaAnketa.aspx');
        e.processOnServer = false;
        click = false;
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('CRMListaAnketa.aspx?ruaj=po');

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
    if (isValidKoka(s,e)) {


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

    pastroFushatKokes(); pastro();
    var hf = document.getElementById("hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);

    ndryshoKonfigurimin();
    hf.value = "shtim";
    myMenu.menuSipasTeDrejtaRegjistrimPaDraft($("input[id$='hfShtimModifikim']"), hfTeDrejta);
    $('#ASPxSplitter1_hl').empty(); UpdateButtonState();
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}


function UpdateButtonState() {
    btnDjathtasGjitha.SetEnabled(lbxFushat.GetItemCount() > 0);
    btnMajtaGjitha.SetEnabled(gvZgjedhur.GetVisibleRowsOnPage() > 0);
    btnDjathtas1.SetEnabled(lbxFushat.GetSelectedItems().length > 0);
    btnMajtas1.SetEnabled(gvZgjedhur.GetSelectedRowCount() > 0);

    gvZgjedhur.MakeRowVisible(gvZgjedhur.GetFocusedRowIndex());

}

function disablebtn() {
    Utils.shfaqLoadingGif();;
}


function CheckedChanged(editor, field, key) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajFushaAnketa"),
            data: JSON.stringify({ vlera: editor.GetChecked(), fusha: field, index: key })
        }).done(SucceededCallbackFusha);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }

}
function SucceededCallbackFusha(result) {
    if (result.Status) 
        myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
   else 
        myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
}
function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}