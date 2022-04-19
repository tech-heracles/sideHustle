;
var identifikuesPerPopupDokumentat, identikuesPerPopupKlientFurnitori, identikuesPerPopupArtikulli;
var resultKonf, colKontrollet, colAtrTrupi;
function Init() {
    if (typeof (isPostBack) == "undefined") {

        ndryshoKonfigurimin();
        identifikuesPerPopupDokumentat = "RecetaOptike.aspx";
        identikuesPerPopupKlientFurnitori = "RecetaOptike";
        identikuesPerPopupArtikulli = "RecetaOptike";

        changeName();
        myMesazh.shtoHandler();
        Utils.konfiguroAccorditionNeDocReady(identikuesPerPopupKlientFurnitori);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);

    }
}
function EndRequestHandler(sender, args) { }
jQuery(document).ready(function () {


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
    });
});

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    if (window.parent)
        window.parent.callWebServiceKtheInfoLart('Shto_RecetaOptike.aspx', Utils.getNumberOrDefaultFromUrl('id'));

    myCookies.createCookie('adresa', window.location.href, 1);
}



/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {
    var niveli;
    if (cmbNiveli.GetText() != "")
        niveli = cmbNiveli.GetValue();
    else niveli = 1;
    var queryString = {
        veprimi: 'RecetaOptike',
        niveli: niveli,
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click('Zgjidh dokumentin', 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);

}

/*
Function: callWebserviceKonfigurimi
    
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('IdGjuha') })
        }).done(SucceededCallbackKonfig);

    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate transferimit te te dhenave!');
    }
}
function DateChanged(s, e) {

    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(colAtrTrupi, colKontrollet);

}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDateDokumenti.GetDate());
}

var kushtet; var colKushte; var colAlterKusht;
var fokusi = 0; var info = false;
var colGrida;
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfStatus = $("input[id$='hfStatusDokumenti']");
    resultKonf = result;
    colKontrollet = result.colKontrollet;
    colAtrTrupi = result.colAtrTrupi;
    var arrPrind = ["dvFillim"];
    var arrTabela = ["tblFillim"];
    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, $("#hfKontrollet"), undefined, "", arrTabela, undefined, undefined, undefined, arrPrind);
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet)); $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        hfNrAuto.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    Utils.konfiguroAccorditionPasKonfigDokumenti(identikuesPerPopupKlientFurnitori);
    $("#accordition").removeClass("not_visible");
    cmbNiveli.SetSelectedIndex(0);
    cmbKonfigurimi.SetSelectedIndex(0);

    if (hf.val() == "modifikim" && hfStatus.val() == "Ruaj") {
        SetEnabledFushaNrDokDtDok(false);
    }
    if(hf.val() == "modifikim")
        ASPxMenu1.GetItemByName('Draft').SetVisible(false);
    if(hf.val() == "shtim")

        ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
}


/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var item = cmbKonfigurimi.GetSelectedItem();
    $('#kokeKonfigurimi').text('Koke Dokumenti: ' + item.GetColumnText("PershkrimKonfigAmbjente"));
    callWebserviceKonfigurimi(3058, item.GetColumnText("KodKonfigAmbjente"));
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    switch(e.item.name){
        case 'Ruaj':
        case 'Draft':
            if (valido(s, e)) {
                Utils.shfaqLoadingGif();;
                merrTeDhenaTrupi();

                if (gvRecetaTrupi.batchEditApi.HasChanges()) {
                    Utils.shtoFunksionNeRadhe(onRuajClick, hfState.Get("idGjuha"), this, "gvRecetaTrupi");
                    gvRecetaTrupi.UpdateEdit();
                }
                else {
                    onRuajClick();
                }
                $("#hfStatusDokumenti").val(e.item.Name);
            }
            break;
        case 'Klono':
            hfNrAuto.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
            $('#hfShtimModifikim').val('shtim');
            ASPxMenu1.GetItemByName('Draft').SetVisible(true);
            ASPxMenu1.GetItemByName('Klono').SetVisible(false);
            SetEnabledFushaNrDokDtDok(true);
            break;
        case "Kerko":
            myFaqeCelje.kontrolloTeDrejta('RecetaOptike.aspx', null, true);
            break;
        case "Shto":
            pastrofusha();
            gvRecetaTrupi.PerformCallback("Pastro");
            ndryshoKonfigurimin();
            ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
            break;
        case "PrintPreview":
            window.open("RaportiShpejte.aspx?Sesioni=false&idraporti=" + gvRecetaTrupi["cpIdRaporti"] + "&idDokumenti=" + gvRecetaTrupi["cpIdKoka"] + "&printo=0&raportdyte=jo&iddesign=" + cmbFormatPrintimi.GetValue() + "&scopeID=" + Utils.getUrlVar("scopeID"));
            break;
        case "Anullo":
            location.href = "RecetaOptike.aspx";
 
    }
    e.processOnServer = false;
}

function SetEnabledFushaNrDokDtDok(enabled) {
    
    txtNumri.SetEnabled(MerrDisabledOseVlereKonfigurimiPerKontrollin("txtNumri", enabled));
    dteDateDokumenti.SetEnabled(MerrDisabledOseVlereKonfigurimiPerKontrollin("dteDateDokumenti", enabled));
    cmbKonfigurimi.SetEnabled(MerrDisabledOseVlereKonfigurimiPerKontrollin("cmbKonfigurimi", enabled));
    cmbNiveli.SetEnabled(MerrDisabledOseVlereKonfigurimiPerKontrollin("cmbNiveli", enabled));
}

function MerrDisabledOseVlereKonfigurimiPerKontrollin(KodKontrolli, enabled) {
    return enabled ? MerrVlereKonfigurimiPerAtributin(KodKontrolli, "Enabled") : enabled;
}

function MerrVlereKonfigurimiPerAtributin(kodKontrolli, atributi) {
    var idKontrolli = Utils.findFieldValueByAttribute(colKontrollet, "KodKontrolli", "IdKontrolli", kodKontrolli);
    return Utils.findFieldValueByAttribute(colAtrTrupi, "IdKontroll", atributi, idKontrolli);
}


function merrTeDhenaTrupi() {//merren te dhenat qe ka grida
    var fushat = [];
    var syriMajteValues = [];
    var syriDjatheValues = [];
    for (i = 0; i < gvFushatRO.cpNoRows; i++) {
        fushat.push(Utils.ktheKontroll('IdFusha' + i).GetText());
        syriMajteValues.push(Utils.ktheKontroll('SyriMajte' + i).GetText());
        syriDjatheValues.push(Utils.ktheKontroll('SyriDjathte' + i).GetText());
    }
    hfState.Set("fushat", JSON.stringify(fushat));
    hfState.Set("syriMajteValues", JSON.stringify(syriMajteValues));
    hfState.Set("syriDjatheValues", JSON.stringify(syriDjatheValues));
}

function CustomButtonsClick(s, e) {
    if (e.buttonID == 'btnDelete') {
        s.DeleteRow(e.visibleIndex);
        AplikoRenditjeBatchEdit();
    }
}


function pastrofusha() {
    $("input[id$='hfShtimModifikim']").val("shtim");
    hfState.Set("Id", '0');
    txtNumri.SetText("");
    txtNrSerial.SetText("");
    cmbKlienti.SetValue();
    txtAdresa.SetText("");
    dteDateDokumenti.SetDate(Utils.ktheDateServeriFromCookies());
    txtShenime.SetText("");
    txtDIAfer.SetText("");
    txtDILarg.SetText("");
    txtReferimi.SetText("");
    txtLartesia.SetText("");
    checkPrint.SetChecked(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    SetEnabledFushaNrDokDtDok(true);
    pastroFushatRO();
}

function pastroFushatRO() {
    hfState.Set("fushat", "");
    hfState.Set("syriMajteValues", "");
    hfState.Set("syriDjatheValues", "");
    Utils.ktheKontroll('SyriMajte0').SetSelectedIndex(-1);
    Utils.ktheKontroll('SyriDjathte0').SetSelectedIndex(-1);
    for (i = 1; i < gvFushatRO.cpNoRows; i++) {

        Utils.ktheKontroll('SyriMajte' + i).SetText("");
        Utils.ktheKontroll('SyriDjathte' + i).SetText("");
    }
}


function onRuajClick(){
    gvRecetaTrupi.PerformCallback('Ruaj');
}

function BeginCallback(s, e) {
    Utils.shfaqLoadingGif();;
}

function EndCallback(s, e) {
    if (Utils.KaFunksionPendingGrida("gvRecetaTrupi")) {
        Utils.execFunksionNeRadhe("gvRecetaTrupi");
        return;
    }
    var mesazhi = Utils.MerrMesazhNgaGrida(s);
    if (mesazhi.Kodi !== 1000) {
        if (mesazhi.Status) {
            //ruajta u be me sukses
            printoFature(isPrintChecked);
            myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
            pastrofusha();
            ndryshoKonfigurimin();
        } else {
            myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
        }
    }
    Utils.hiqLoadingGif();;
    
}

function StartEditing(s, e) {
    if(e.focusedColumn.fieldName == 'NrFaza')
        e.cancel = true;
    AplikoRenditjeBatchEdit(s);
}
function EndEditing(s, e) {

}

function AplikoRenditjeBatchEdit(s) {
    for (var i = 0; i < s.batchEditApi.GetRowVisibleIndices().length; i++) s.batchEditApi.SetCellValue(s.batchEditApi.GetRowVisibleIndices()[i], "NrFaza", i + 1)
}
function KlientFurnitoriChanged() {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheAdresatKlientFurnitor"),
        data: JSON.stringify({ prefixText: cmbKlienti.GetSelectedItem().texts[0], idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(function (result) {
        if (result == undefined) return;
        for(var i = 0; i < result.length; i++)
            if (result[i].Adresa.length > 0) {
                txtAdresa.SetText(result[i].Adresa);
                return;
            }

    });
}
var editorkf;
function ButtonClickKlienti() {//po
    editorkf = cmbKlienti;

    popupUniversal.SetHeaderText("Zgjidhni Klientin");
    popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=1');
    popupUniversal.SetSize(800, 600);
    popupUniversal.Show();
}

function valido(s, e) {
    if (txtNumri.GetText().length == 0) {
        myMesazh.ShtoMesazhGabimi("Ju lutem plotesoni numrin e dokumentit!");
        return false;
    }
    if (dteDateDokumenti.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi("Ju lutem plotesoni daten e dokumentit!");
        return false;
    }
    return myFaqeCelje.validim(s, e);
}

function aktivizoFusha() { }

var editorArt
function ButtonClickedArtikulli(editor, key) {
    editorArt = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Artikullin', 'LupaArtikull.aspx', 700, 560);
}

var isPrintChecked = false;
function PrintoChecked(s, e) {
    isPrintChecked = s.GetChecked();
}


function printoFature(printo) {
    if (printo == true)
        $("#Container2").attr("src", "RaportiShpejte.aspx?Sesioni=false&idraporti=" + gvRecetaTrupi["cpIdRaporti"] + "&idDokumenti=" + gvRecetaTrupi["cpIdKoka"] + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatPrintimi.GetValue());
}