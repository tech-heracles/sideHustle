;
var arr = new Array(); // array ku ruhen vlerat per griden kontrollet
var arrTrupi = new Array(); // array ku ruhen vlerat per griden trupi
var arrKusht = new Array(); // array ku ruhen vlerat per griden e kushteve
var nrRreshtash = 0;
var nrRreshtashTrupi = 0;
var nrRreshtashKusht = 0;
var editorKF;
var editorPunonjes;
var editorLloji;
var editorFormula;
var identikuesPerPopupKlientFurnitori;
var identifikuesPerPopupBanka;
var identikuesPerPopupLupa;
var identifikuesPerPopupAutorizime = 'KonfigurimDokumentash';
var editorSkemaKontabelRegjistrime;
var identifikuesPerPopupKodifikimin = 'KonfigurimDokumentash';
var editorMT;
var editorKD;
var editorAGJ;
var editorM;
var editorKP;
var editorNJA;
var editorNjProdhimi;
var editorLupa;
var cou1 = 0;
var arrKodi = new Array();
var editorKodi;
var editorValues = new Object();
var identikuesPerPopupFormula;
var editorAutorizime;
var editorGrupKlientFurnitor;
var identifikuesPerPopupFormatNr = 'KonfigurimDokumentash';
var identikuesPerPopupDegaAdministrative;
var identifikuesPerPopupNjesiProdhimi;
var pageState = {

    txtVleraDefault: 'txtVleraDefault',
    fushaLlogarie: null
};
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
                break;
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
    });

});

function ButtonClickedKlientFurnitor(editor, key, veprimi) {
    editorKF = editor;
    if (veprimi == 1)
        url = 'LupaKlientFurnitor.aspx?veprimi=1';
    else if (veprimi == 2)
        url = 'LupaKlientFurnitor.aspx?veprimi=2';
    else url = 'LupaKlientFurnitor.aspx';
    identikuesPerPopupKlientFurnitori = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh klient/furnitorin', url, 700, 560);
}

function ButtonClickedKF(editor, key) {

    var KF = Kategoria_ComboBox.GetText();

    if (KF == "Shitje")
        ButtonClickedKlientFurnitor(editor, key, 1);
    else if (KF == "Blerje")
        ButtonClickedKlientFurnitor(editor, key, 2);
    else
        ButtonClickedKlientFurnitor(editor, key, 0);

}
function ButtonClickedSubjektiDefault(editor, key) {

    var llojSubjekti = eval('Vlera' + $("input[id$='hfLlojSubjektiDefaultVlera']").val()).GetText();
    switch (llojSubjekti) {
        case "Klient":
            ButtonClickedKlientFurnitor(editor, key, 1);
            break;
        case "Furnitor":
            ButtonClickedKlientFurnitor(editor, key, 2);
            break;
        case "Llogari":
            ButtonClickedLLogaria(editor, key, "KonfigurimDokumentashArketim");
            break;
        case "Punonjes":
            ButtonClickedPunonjes(editor);
            break;
    }
}

function ButtonClickedPunonjes(editor) {
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Punonjes', 'LupaPunonjes.aspx?vjenNga=Konfigurimi', 800, 600);
}

function ButtonClickedTransportues(editor, key, llojGrupi) {
    identikuesPerPopupTransportuesi = "Import";
    editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Transportues', 'Shto_Transportues.aspx?lupe=true', 1070, 750);
}

function CmbTextChanged(editor, key, s, e) {
    if (isNaN(editor.GetValue())) {
        editor.SetText('');
        editor.Focus();
        return;
    }
}

function ButtonClickedBanka(editor, key) {
    editorKF = editor;
    var KF = Kategoria_ComboBox.GetText();

    identifikuesPerPopupBanka = "KonfigurimDokumentash";
    if (KF == "Arka" || KF == "Veprime Arke")
        url = 'LupaBanka.aspx?arkabanka=3&arka=false';
    // url = 'LupaBanka.aspx?arkabanka=3';
    else if (KF == "Banka" || KF == "Veprime Banke")
        url = 'LupaBanka.aspx?arkabanka=4&arka=true';

    myButtonClickLupa.LupaUniversal_Click('Zgjidh arka/banka', url, 700, 560);
}

function ButtonClickedMT(editor, key) {
    editorMT = editor;
    identikuesPerPopupMenyraTransporti = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh menyren e transportit', 'LupaMenyraTransporti.aspx', 700, 560);
}

function ButtonClickedKD(editor, key) {
    editorKD = editor;
    identikuesPerPopupKushteDergimi = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Kushtet e dergimit', 'LupaKushteDergimi.aspx', 700, 560);
}
function ButtonClickedPer(editor, key) {
    editorMag = editor;
    editorkodbar = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Mag';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh perdoruesin', 'LupaPerdorues.aspx', 700, 560);
}
function ButtonClickedAGJ(editor, key) {
    editorAGJ = editor;
    identikuesPerPopupAgjenteShitje = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh agjentin e shitjes', 'LupaAgjenteShitje.aspx', 700, 560);
}

function ButtonClickedM(editor, key) {
    editorM = editor;
    identikuesPerPopupAfateMaturimi = "KonfigurimDokumentash";
    var KF = Kategoria_ComboBox.GetText();
    if (KF == "Shitje")
        url = 'LupaAfateMaturimi.aspx?veprimi=2';
    else if (KF == "Blerje")
        url = 'LupaAfateMaturimi.aspx?veprimi=1';
    else url = 'LupaAfateMaturimi.aspx';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh afatin e maturimit', url, 700, 650);
}

function ButtonClickedKP(editor, key) {
    editorKP = editor;
    identikuesPerPopupKushtePagese = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Kushtet e pageses', 'LupaKushtePagese.aspx', 700, 560);
}

function ButtonClickedNJA(editor, key) {
    editorNJA = editor;
    identifikuesPerPopupMagazina = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh magazinen', 'LupaMagazina.aspx', 700, 560);
}

function ButtonClickedPkShF(editor, key) {
    editorPkShF = editor;
    identifikuesPerPopupMagazina = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Pike Shitje Furnizimi', 'LupaPikeShitjeFurnizimi.aspx', 700, 560);
}

function ButtonClickedDege(editor, key) {
    editorPkShF = editor;
    identikuesPerPopupDegaAdministrative = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Dege Administrative', 'LupaDegaAdministrative.aspx', 700, 560);
}

function ButtonClickedNjesiProdhimi(editor, key) {
    editorNjProdhimi = Utils.ktheKontroll(editor);
    identifikuesPerPopupNjesiProdhimi = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Njesi Prodhimi', 'LupaNjesiProdhimi.aspx', 700, 560);
}

function ButtonClickedArka(editor, key) {
    editorKF = editor;
    identifikuesPerPopupBanka = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh banken/arken', 'LupaBanka.aspx', 700, 560);
    //  myButtonClickLupa.LupaUniversal_Click('Zgjidh banken/arken', 'LupaBanka.aspx?arka=true', 700, 560);
}

var keySK;
function ButtonClickedSKA(editor, key) {

    VendosFushaLlogarieNePageState();
    firstPageElementIndex = (50 * (grid_kontrollet.cpNoPage));
    lastPageElementIndex = (50 * (grid_kontrollet.cpNoPage + 1)) - 1;
    editorSKA = editor;
    keySK = key;

    if (pageState.fushaLlogarie.llogariInv.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariInv.index <= lastPageElementIndex)
        editorLLI = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariInv.index);
    if (pageState.fushaLlogarie.llogariB.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariB.index <= lastPageElementIndex)
        editorLLB = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariB.index);
    if (pageState.fushaLlogarie.llogariSh.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariSh.index <= lastPageElementIndex)
        editorLLSh = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariSh.index);
    if (pageState.fushaLlogarie.llogariTr.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariTr.index <= lastPageElementIndex)
        editorLLT = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariTr.index);
    if (pageState.fushaLlogarie.llogariShp.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariShp.index <= lastPageElementIndex)
        editorLLShp = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariShp.index);
    if (pageState.fushaLlogarie.llogariA.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariA.index <= lastPageElementIndex)
        editorLLA = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariA.index);
    identifikuesPerPopupSkema = "KonfigurimDokumentash";
    myButtonClickLupa.LupaUniversal_Click('Zgjidh skemen e kontabilitetit te artikullit', 'LupaSkemaKontabelArtikulli.aspx', 700, 560);
}
function ButtonClickedLLogaria(editor, key, identifikues) {
    editorLL = editor;
    identifikuesPerPopupLlogari = identifikues;
    var ambjenti = "LupaLlogaria.aspx" + ((identifikues == 'KonfigurimDokumentashBuxhetim') ? ("?vjenNga=" + identifikues) : "");
    myButtonClickLupa.LupaUniversal_Click('Zgjidh llogarine', ambjenti, 750, 650);
}
function ButtonClickedLL(editor, key) {
    ButtonClickedLLogaria(editor, key, "KonfigurimDokumentash");
}

var colAtribute;
function shtoRreshtatEkzistuesNeMatrice() {
    hf = $("input[id$='hfVleratFillestareKontrollet']");
    colAtribute = JSON.parse(hf.val());
}

function merrTeDhena() {//merren te dhenat qe ka grida
    var idGjuha = hfState.Get("_idGjuha");
    if (grid_kontrollet.cpNoRows > 50 * (grid_kontrollet.cpNoPage + 1))
        for (i = 50 * grid_kontrollet.cpNoPage; i < 50 * (grid_kontrollet.cpNoPage + 1); i++) {
            if (idGjuha == 0)
                colAtribute[i].VlereDefault = Utils.ktheKontroll(pageState.txtVleraDefault + i).GetValue();
            else if (idGjuha == 1)
                colAtribute[i].VlereDefaultEng = Utils.ktheKontroll(pageState.txtVleraDefault + i).GetValue();
            else
                colAtribute[i].VlereDefault_fr = Utils.ktheKontroll(pageState.txtVleraDefault + i).GetValue();
            colAtribute[i].Visible = Utils.ktheKontroll('cmbVisible' + i).GetValue();
            colAtribute[i].Rreshti = Utils.ktheKontroll('txtRreshti' + i).GetText();
            colAtribute[i].Kolona = Utils.ktheKontroll('txtKolona' + i).GetText();
            colAtribute[i].Enabled = Utils.ktheKontroll('cmbEnabled' + i).GetValue();
            colAtribute[i].Identifikues = Utils.ktheKontroll('cmbIdentifikues' + i).GetValue();
            colAtribute[i].Detyrueshme = Utils.ktheKontroll('cmbDetyrueshme' + i).GetValue();
            colAtribute[i].IdNrAutomatik = Utils.ktheKontroll('cmbNrAutomatik' + i).GetValue();
            colAtribute[i].KodLupa = $("input[id$='txtKodLupa" + i + "']").val();
            colAtribute[i].ShfaqMobile = Utils.ktheKontroll('cmbShfaqMobile' + i).GetValue();
            colAtribute[i].RenditjaMobile = Utils.ktheKontroll('txtRenditjaMobile' + i).GetText();
            colAtribute[i].Unike = Utils.ktheKontroll('cmbUnike' + i).GetValue();
        }
    else {
        for (i = 50 * grid_kontrollet.cpNoPage; i < grid_kontrollet.cpNoRows; i++) {
            if (idGjuha == 0)
                colAtribute[i].VlereDefault = Utils.ktheKontroll(pageState.txtVleraDefault + i).GetValue();
            else if (idGjuha == 1)
                colAtribute[i].VlereDefaultEng = Utils.ktheKontroll(pageState.txtVleraDefault + i).GetValue();
            else
                colAtribute[i].VlereDefault_fr = Utils.ktheKontroll(pageState.txtVleraDefault + i).GetValue();
            colAtribute[i].Visible = Utils.ktheKontroll('cmbVisible' + i).GetValue();
            colAtribute[i].Rreshti = Utils.ktheKontroll('txtRreshti' + i).GetText();
            colAtribute[i].Kolona = Utils.ktheKontroll('txtKolona' + i).GetText();
            colAtribute[i].Enabled = Utils.ktheKontroll('cmbEnabled' + i).GetValue();
            colAtribute[i].Identifikues = Utils.ktheKontroll('cmbIdentifikues' + i).GetValue();
            colAtribute[i].Detyrueshme = Utils.ktheKontroll('cmbDetyrueshme' + i).GetValue();
            colAtribute[i].IdNrAutomatik = Utils.ktheKontroll('cmbNrAutomatik' + i).GetValue();
            colAtribute[i].KodLupa = $("input[id$='txtKodLupa" + i + "']").val();
            colAtribute[i].ShfaqMobile = Utils.ktheKontroll('cmbShfaqMobile' + i).GetValue();
            colAtribute[i].RenditjaMobile = Utils.ktheKontroll('txtRenditjaMobile' + i).GetText();
            colAtribute[i].Unike = Utils.ktheKontroll('cmbUnike' + i).GetValue();
        }
    }
}

function ShfaqTeDhenat() {
    var idGjuha = hfState.Get("_idGjuha");
    if (grid_kontrollet.cpNoRows > 50 * (grid_kontrollet.cpNoPage + 1))

        for (i = 50 * grid_kontrollet.cpNoPage; i < 50 * (grid_kontrollet.cpNoPage + 1); i++) {
            if (colAtribute[i].Enabled == true)
                colAtribute[i].Enabled = "Checked";
            else if (colAtribute[i].Enabled == false)
                colAtribute[i].Enabled = "Unchecked";
            if (colAtribute[i].Visible == true)
                colAtribute[i].Visible = "Checked";
            else if (colAtribute[i].Visible == false)
                colAtribute[i].Visible = "Unchecked";
            if (colAtribute[i].Detyrueshme == true)
                colAtribute[i].Detyrueshme = "Checked";
            else if (colAtribute[i].Detyrueshme == false)
                colAtribute[i].Detyrueshme = "Unchecked";

            if (colAtribute[i].ShfaqMobile == true)
                colAtribute[i].ShfaqMobile = "Checked";
            else if (colAtribute[i].ShfaqMobile == false)
                colAtribute[i].ShfaqMobile = "Unchecked";

            if (colAtribute[i].Unike == true)
                colAtribute[i].Unike = "Checked";
            else if (colAtribute[i].Unike == false)
                colAtribute[i].Unike = "Unchecked";

            Utils.ktheKontroll(pageState.txtVleraDefault + i).SetValue(idGjuha == 0 ? colAtribute[i].VlereDefault : idGjuha == 1 ? colAtribute[i].VlereDefaultEng : colAtribute[i].VlereDefault_fr);
            Utils.ktheKontroll('cmbVisible' + i).SetValue(colAtribute[i].Visible);
            Utils.ktheKontroll('txtRreshti' + i).SetText(colAtribute[i].Rreshti);
            Utils.ktheKontroll('txtKolona' + i).SetText(colAtribute[i].Kolona);
            Utils.ktheKontroll('cmbEnabled' + i).SetValue(colAtribute[i].Enabled);
            Utils.ktheKontroll('cmbIdentifikues' + i).SetValue(colAtribute[i].Identifikues);
            Utils.ktheKontroll('cmbDetyrueshme' + i).SetValue(colAtribute[i].Detyrueshme);
            Utils.ktheKontroll('cmbNrAutomatik' + i).SetValue(colAtribute[i].IdNrAutomatik);
            $("input[id$='txtKodLupa" + i + "']").val(colAtribute[i].KodLupa);

            Utils.ktheKontroll('cmbShfaqMobile' + i).SetValue(colAtribute[i].ShfaqMobile);
            Utils.ktheKontroll('txtRenditjaMobile' + i).SetText(colAtribute[i].RenditjaMobile);
            Utils.ktheKontroll('cmbUnike' + i).SetValue(colAtribute[i].Unike);
        }
    else {
        for (i = 50 * grid_kontrollet.cpNoPage; i < grid_kontrollet.cpNoRows; i++) {
            if (colAtribute[i].Enabled == true)
                colAtribute[i].Enabled = "Checked";
            else if (colAtribute[i].Enabled == false)
                colAtribute[i].Enabled = "Unchecked";
            if (colAtribute[i].Visible == true)
                colAtribute[i].Visible = "Checked";
            else if (colAtribute[i].Visible == false)
                colAtribute[i].Visible = "Unchecked";
            if (colAtribute[i].Detyrueshme == true)
                colAtribute[i].Detyrueshme = "Checked";
            else if (colAtribute[i].Detyrueshme == false)
                colAtribute[i].Detyrueshme = "Unchecked";
            if (colAtribute[i].ShfaqMobile == true)
                colAtribute[i].ShfaqMobile = "Checked";
            else if (colAtribute[i].ShfaqMobile == false)
                colAtribute[i].ShfaqMobile = "Unchecked";

            if (colAtribute[i].Unike == true)
                colAtribute[i].Unike = "Checked";
            else if (colAtribute[i].Unike == false)
                colAtribute[i].Unike = "Unchecked";

            Utils.ktheKontroll(pageState.txtVleraDefault + i).SetValue(idGjuha == 0 ? colAtribute[i].VlereDefault : idGjuha == 1 ? colAtribute[i].VlereDefaultEng : colAtribute[i].VlereDefault_fr);
            Utils.ktheKontroll('cmbVisible' + i).SetValue(colAtribute[i].Visible);
            Utils.ktheKontroll('txtRreshti' + i).SetText(colAtribute[i].Rreshti);
            Utils.ktheKontroll('txtKolona' + i).SetText(colAtribute[i].Kolona);
            Utils.ktheKontroll('cmbEnabled' + i).SetValue(colAtribute[i].Enabled);
            Utils.ktheKontroll('cmbIdentifikues' + i).SetValue(colAtribute[i].Identifikues);
            Utils.ktheKontroll('cmbDetyrueshme' + i).SetValue(colAtribute[i].Detyrueshme);
            Utils.ktheKontroll('cmbNrAutomatik' + i).SetValue(colAtribute[i].IdNrAutomatik);
            $("input[id$='txtKodLupa" + i + "']").val(colAtribute[i].KodLupa);
            Utils.ktheKontroll('cmbShfaqMobile' + i).SetValue(colAtribute[i].ShfaqMobile);
            Utils.ktheKontroll('txtRenditjaMobile' + i).SetText(colAtribute[i].RenditjaMobile);
            Utils.ktheKontroll('cmbUnike' + i).SetValue(colAtribute[i].Unike);

        }
    }
}

var keyLlogarite;
function TextChangedVleraSKA(editor, field, key) {
    var selectedEditorItem = editor.GetSelectedItem();
    colAtribute[key].VlereDefault = editor.GetText();
    keyLlogarite = key;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheIdLlogNgaNumri"),
        data: JSON.stringify({
            nrLlogInv: selectedEditorItem.GetColumnText('NrLlogariInventari'),
            nrLlogBle: selectedEditorItem.GetColumnText('NrLlogariBlerje'),
            nrLlogShit: selectedEditorItem.GetColumnText('NrLlogariShitje'),
            nrLlogTret: selectedEditorItem.GetColumnText('NrLlogariTekTeTretet'),
            nrLlogShpenz: selectedEditorItem.GetColumnText('NrLlogariShpenzimi'),
            nrLlogAmort: selectedEditorItem.GetColumnText('NrLlogariAmortizimi')
        })
    }).done(SucceededCallbackIdLlogarite);


}

function SucceededCallbackIdLlogarite(result) {

    VendosFushaLlogarieNePageState();
    firstPageElementIndex = (50 * (grid_kontrollet.cpNoPage));
    lastPageElementIndex = (50 * (grid_kontrollet.cpNoPage + 1)) - 1;

    if (result[0] != 0)

        pageState.fushaLlogarie.llogariInv.fusha.VlereDefault = result[0];
    else pageState.fushaLlogarie.llogariInv.fusha.VlereDefault = "";
    if (result[1] != 0)

        pageState.fushaLlogarie.llogariB.fusha.VlereDefault = result[1];
    else pageState.fushaLlogarie.llogariB.fusha.VlereDefault = "";
    if (result[2] != 0)

        pageState.fushaLlogarie.llogariSh.fusha.VlereDefault = result[2];
    else pageState.fushaLlogarie.llogariSh.fusha.VlereDefault = "";
    if (result[3] != 0)

        pageState.fushaLlogarie.llogariTr.fusha.VlereDefault = result[3];
    else pageState.fushaLlogarie.llogariTr.fusha.VlereDefault = "";
    if (result[4] != 0)

        pageState.fushaLlogarie.llogariShp.fusha.VlereDefault = result[4];
    else pageState.fushaLlogarie.llogariShp.fusha.VlereDefault = "";
    if (result[5] != 0)

        pageState.fushaLlogarie.llogariA.fusha.VlereDefault = result[5];
    else pageState.fushaLlogarie.llogariA.fusha.VlereDefault = "";
    if (pageState.fushaLlogarie.llogariInv.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariInv.index <= lastPageElementIndex) {
        editorLLI = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariInv.index);
        if (result[0] != 0)
            Utils.SelectComboItem(editorLLI, result[0], null);
        else Utils.SelectComboItem(editorLLI, "", null);
    }

    if (pageState.fushaLlogarie.llogariB.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariB.index <= lastPageElementIndex) {
        editorLLB = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariB.index);
        if (result[1] != 0)
            Utils.SelectComboItem(editorLLB, result[1], null);
        else editorLLB.SetText('');
    }
    if (pageState.fushaLlogarie.llogariSh.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariSh.index <= lastPageElementIndex) {
        editorLlSh = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariSh.index);
        if (result[2] != 0)

            Utils.SelectComboItem(editorLlSh, result[2], null);
        else editorLlSh.SetText('');
    }
    if (pageState.fushaLlogarie.llogariTr.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariTr.index <= lastPageElementIndex) {
        editorLLT = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariTr.index);
        if (result[3] != 0)
            Utils.SelectComboItem(editorLLT, result[3], null);
        else editorLLT.SetText('');
    }
    if (pageState.fushaLlogarie.llogariShp.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariShp.index <= lastPageElementIndex) {
        editorLLShp = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariShp.index);
        if (result[4] != 0)
            Utils.SelectComboItem(editorLLShp, result[4], null);
        else editorLLShp.SetText('');
    }
    if (pageState.fushaLlogarie.llogariA.index >= firstPageElementIndex && pageState.fushaLlogarie.llogariA.index <= lastPageElementIndex) {
        editorLLA = Utils.ktheKontroll(pageState.txtVleraDefault + pageState.fushaLlogarie.llogariA.index);
        if (result[5] != 0)
            Utils.SelectComboItem(editorLLA, result[5], null);
        else editorLLA.SetText('');
    }
}


keyKursi = 0;
function SelectedIndexChanged(editor, field, key) {
    editor.processOnServer = true;
    keyKursi = key + 2;
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheLlojeKurseshPerMonedhe"),
        data: JSON.stringify({ id: editor.GetValue() })
    }).done(SucceededCallbackKurset);
}

function LostFocusKushtMinimumShitje(editor, key) {
    var vleratext = editor.GetText();
    if (vleratext != "" || vleratext != null)
        vleratext = vleratext.trim();


    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKodVlereMinimum"),
        data: JSON.stringify({ vlera: vleratext })
    }).done(function (result) { SucceededCallbackkodiMinimumShitje(result, editor, editor.GetText()); });
}

function SucceededCallbackkodiMinimumShitje(idAlternativa, editor, vleraText) {
    editor.ClearItems();
    editor.AddItem(vleraText, idAlternativa);
    editor.SetSelectedIndex(0);
}

function LostFocusKushtLlogariBuxheti(editor, key) {
    var vleratext = editor.GetText();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheVlereLlogariBuxheti"),
        data: JSON.stringify({ vlera: vleratext })
    }).done(function (result) {
        SucceededCallbackkodiMinimumShitje(result, editor, editor.GetText());
    });
}

function SucceededCallbackKurset(text) {
    if (keyKursi + 2 < 50 * (grid_kontrollet.cpNoPage + 1)) {
        var editor = Utils.ktheKontroll(pageState.txtVleraDefault + keyKursi);
        editor.ClearItems();
        var arr = text.split(';');
        for (i = 0; i < arr.length - 1; i++)
            editor.AddItem(arr[i].split(',')[0], arr[i].split(',')[1]);
    }
}

function TextChangedEnabled(editor, field, key, idTipKontrolli) {
    var editorNrAutomatik = Utils.ktheKontroll('cmbNrAutomatik' + key);
    if (idTipKontrolli == 1) {
        if (editor.GetSelectedIndex() == 1) {
            editorNrAutomatik.SetEnabled(true);
        }
        else {
            //  editorNrAutomatik.SetText('');
            editorNrAutomatik.SetEnabled(false);
        }
    }
}

function TextChangedIdentifikues(editor, field, key) {
    var editorDetyrueshme = Utils.ktheKontroll('cmbDetyrueshme' + key);
    if (editor.GetValue() === "1") {
        colAtribute[key].Detyrueshme = "Checked";
        editorDetyrueshme.SetText('Po');
        editorDetyrueshme.SetEnabled(false);
    }
    else editorDetyrueshme.SetEnabled(true);
}


function TextChangedShfaqMobile(editor, field, key) {

}

function TextChangedNrAutomatik(editor, field, key) {
    if (editor.GetValue() != 0)
        for (i = 0; i < colAtribute.length; i++) {
            if (i != key)
                if (colAtribute[i].IdNrAutomatik == editor.GetValue()) {
                    myMesazh.ShtoMesazhGabimi("Ky nr automatik eshte perdorur njehere ne kete ambjent!");
                    editor.SetValue(0);
                    break;
                }
        }
}

function KeyPressLupa(editor, key) {
    if (event.keyCode == 13) {
        editorLupa = document.getElementById(editor);
        myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e lupes', 'LupaKonfigurimDokument.aspx', 700, 560);
        event.returnValue = false;
        event.cancel = true;
    }
}

function ButtonClickedLupa(editor, key) {
    editorLupa = document.getElementById(editor);
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e lupes', 'LupaKonfigurimDokument.aspx', 700, 560);

}

var colTrupi;
function shtoRreshtatEkzistuesTrupitNeMatrice() {
    hf1 = $("input[id$='hfVleratFillestareTrupi']");
    colTrupi = JSON.parse(hf1.val());
}

function merrTeDhenaTrupi() {//merren te dhenat qe ka grida
    if (grid_trupi.cpNoRows > 15 * (grid_trupi.cpNoPage + 1))
        for (i = 15 * grid_trupi.cpNoPage; i < 15 * (grid_trupi.cpNoPage + 1); i++) {
            mbushTeDhenaTeCollectionTrupi(i);
        }
    else {
        for (i = 15 * grid_trupi.cpNoPage; i < grid_trupi.cpNoRows; i++) {
            mbushTeDhenaTeCollectionTrupi(i);
        }
    }
}

function ShfaqTeDhenatTrupi() {
    if (grid_trupi.cpNoRows > 15 * (grid_trupi.cpNoPage + 1))
        for (i = 15 * grid_trupi.cpNoPage; i < 15 * (grid_trupi.cpNoPage + 1); i++) {
            afishoTeDhenaNgaCollectionTrupi(i);
        }
    else {
        for (i = 15 * grid_trupi.cpNoPage; i < grid_trupi.cpNoRows; i++) {
            afishoTeDhenaNgaCollectionTrupi(i);
        }
    }
}

function mbushTeDhenaTeCollectionTrupi(i) {
    colTrupi[i].IndexTrupi = Utils.ktheKontroll('cmbIndex' + i).GetText();
    colTrupi[i].VisibleTrupi = Utils.ktheKontroll('cmbVisibleTrupi' + i).GetValue() == "Unchecked" ? false : true;
    colTrupi[i].WidthTrupi = Utils.ktheKontroll('txtWidthTrupi' + i).GetText();
    colTrupi[i].ReadonlyTrupi = Utils.ktheKontroll('cmbReadonlyTrupi' + i).GetValue() == "Unchecked" ? false : true;
    colTrupi[i].KodLupa = $("input[id$='txtKodLupaTrupi" + i + "']").val();
    colTrupi[i].ShfaqMobile = Utils.ktheKontroll('cmbShfaqMobileGrida' + i).GetValue() == "Unchecked" ? false : true;
    colTrupi[i].RenditjaMobile = Utils.ktheKontroll('txtRenditjaMobileGrida' + i).GetText();
}

function afishoTeDhenaNgaCollectionTrupi(i) {
    Utils.ktheKontroll('cmbIndex' + i).SetText(colTrupi[i].IndexTrupi);
    Utils.ktheKontroll('cmbVisibleTrupi' + i).SetValue(colTrupi[i].VisibleTrupi ? "Checked" : "Unchecked");
    Utils.ktheKontroll('txtWidthTrupi' + i).SetText(colTrupi[i].WidthTrupi);
    Utils.ktheKontroll('cmbReadonlyTrupi' + i).SetValue(colTrupi[i].ReadonlyTrupi ? "Checked" : "Unchecked");
    $("input[id$='txtKodLupaTrupi" + i + "']").val(colTrupi[i].KodLupa);
    Utils.ktheKontroll('cmbShfaqMobileGrida' + i).SetValue(colTrupi[i].ShfaqMobile ? "Checked" : "Unchecked");
    Utils.ktheKontroll('txtRenditjaMobileGrida' + i).SetText(colTrupi[i].RenditjaMobile);
}

// rasti i caktimit te filtrave default ne konfigurimin e lupave si konfigurim ambjenti
function InitFiltri(editor, key) {
    editor.GetInputElement().readOnly = true;
}


function InitLloji(editor, key) {
    editor.GetInputElement().readOnly = true;
}

function ButtonClickedFiltri(editor, key, koka) {
    editorLloji = editor;
    var hf = $("input[id$='hfFiltri']");
    myButtonClickLupa.LupaUniversal_Click('Zgjidh filtrin', 'LupaFiltra.aspx?IdKoka=' + koka.toString() + '&idKonfigAmbjente=577', 700, 560);
}

function ButtonClickedLloji(editor, key) {
    editorLloji = editor;
    var hf = $("input[id$='hfPrioriteti']");
    myButtonClickLupa.LupaUniversal_Click('Zgjidh llojin', 'LupaLloji.aspx?value=' + hf.val() + '&kat=' + Kategoria_ComboBox.GetText(), 700, 560);
}

var editorMag;
var editorkodbar;
var editorArk;
var editorPag;
var editorBler;
var editorDL;
var indentifikuesPerPopupKonfigurimet;
var editorBart;

function ButtonClickedMag(editor, key) {
    editorMag = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Mag';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e magazines', 'LupaKonfigurime.aspx?veprimi=6', 700, 560);
}
function ButtonClickedArk(editor, key) {
    editorArk = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Ark';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e arketimit', 'LupaKonfigurime.aspx?veprimi=3', 700, 560);
}
function ButtonClickedPag(editor, key) {
    editorPag = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Pag';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e pageses', 'LupaKonfigurime.aspx?veprimi=3', 700, 560);
}
function ButtonClickedBler(editor, key) {
    editorBler = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Bler';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e blerjes', 'LupaKonfigurime.aspx?veprimi=2', 700, 560);
}
function ButtonClickedShit(editor, key) {
    editorMag = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Mag';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e shitjes', 'LupaKonfigurime.aspx?veprimi=1', 700, 560);
}
function ButtonClickedRez(editor, key) {
    editorMag = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Rez';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e rezervimit', 'LupaKonfigurime.aspx?veprimi=78', 700, 560);
}

function ButtonClickedMag2(editor, key) {
    editorMag = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Mag';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e magazines', 'LupaKonfigurime.aspx?veprimi=6', 700, 560);
}

function ButtonClickedPlan(editor, key) {
    editorMag = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Mag';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e magazines', 'LupaKonfigurime.aspx?veprimi=44', 700, 560);
}

function ButtonClickedSkemaWF(editor, key) {
    editorMag = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'Mag';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh skemen', 'LupaSkemaWorkFlow.aspx', 700, 560);
}

function ButtonClickedDL(editor, key) {
    editorDL = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'DL';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e lidhjes se dokumentave', 'LupaKonfigurime.aspx?veprimi=10', 700, 560);
}
function ButtonClickedAM(editor, key) {
    editorDL = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'AM';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e dokumentit te amortizimit', 'LupaKonfigurime.aspx?veprimi=86', 700, 560);
}
function ButtonClickedFleteHyrje(editor, key) {
    editorDL = editor;
    editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'ZFH';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh flete hyrjen', 'LupaKonfigurime.aspx?veprimi=91&kushti=' + kushti, 700, 560);
}

var editorFK;
function ButtonClickedFK(editor, key) {
    editorFK = editor; editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'FK';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e fletes kontabel', 'LupaKonfigurime.aspx?veprimi=5', 700, 560);

}
var editorAD;
function ButtonClickedAD(editor, key) {
    editorAD = editor;
    identikuesPerPopupArtikulli = 'AD';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh artikull default', 'LupaArtikull.aspx', 700, 560);
}

function ButtonClickedKF(editor, key) {
    editorKF = editor; editorGlobal = editor;
    identikuesPerPopupKlientFurnitori = 'KonfigurimDokumentash';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh klient/furnitor', 'LupaKlientFurnitor.aspx', 700, 560);
}
function ButtonClickedRQK(editor, key) {
    editorFK = editor; editorGlobal = editor;
    indentifikuesPerPopupKonfigurimet = 'FK';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh konfigurimin e regjistrimit te qendres se kostos', 'LupaKonfigurime.aspx?veprimi=75', 700, 560);
}

function ButtonClickedSK(editor, key) {
    editorFK = editor; editorGlobal = editor;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh skemen kontabel', 'LupaSkemaKontabelRegjistrime.aspx', 700, 560);
}

var colKushte;
var kushti = true;
function shtoRreshtatEkzistuesKushtiNeMatrice() {
    hf2 = $("input[id$='hfVleratFillestareKushtet']");
    colKushte = JSON.parse(hf2.val());
    var eksistonKushtMag = false;
    var eksistonKushtKontabilizim = false;
    var ekzistonkushtrezervim = false;
    var kushti_dmt = true;
    hfVleramag = $("input[id$='hfVleraMag']");
    hfNKAKNKA = $("input[id$='hfNKAKNKA']");
    hfVleramag2 = $("input[id$='hfVleraMag2']");
    hfVlerarez = $("input[id$='hfRezervim']");
    hfvlerakont = $("input[id$='hfVleraKont']");
    hfLidhjeArketim = $("input[id$='hfLidhjeArketim']");
    hfvlerashit = $("input[id$='hfVleraShit']");
    hfFIDK = $("input[id$='hfFIDK']");
    hffletakont = $("input[id$='hfFletaKont']");
    hfNrAutoDok = $("input[id$='hfNrAutoDok']");
    hfbarkod = $("input[id$='hfVleraBarkod']");
    hfPezull = $("input[id$='hfPezull']");
    hfProces = $("input[id$='hfProces']");
    hfZevendesim = $("input[id$='hfZevendesim']");
    hfZevendesimT = $("input[id$='hfZevendesimVlera']");
    hfVleraFleteHyrje = $("input[id$='hfVleraFleteHyrje']");
    hfBart = $("input[id$='hfBart']");
    hfVleraDokVartes = $("input[id$='hfVleraDokVartes']");
    hfvleraark = $("input[id$='hfVleraArk");
    hfvlerepag = $("input[id$='hfVleraPagese");
    hfvlerebler = $("input[id$='hfVleraBlere");
    hfSHDQKPMD = $("input[id$='hfSHDQKPMD");
    hfVleraRSKDMD = $("input[id$='hfVleraRSKDMD");
    hfTKLL_B = $("input[id$='hfTKLL_B");
    hfZLL_B = $("input[id$='hfZLL_B");
    hfSDAF = $("input[id$='hfSDAF");
    hfZSP = $("input[id$='hfZSP");

    for (i = 0; i < colKushte.length; i++) {
        if (colKushte[i].Kodi == 'GJDM') {
            if (colKushte[i].Vlera == 28) {
                editorMag = Utils.ktheKontroll('Vlera' + hfVleramag.val());
                editorMag.SetEnabled(true);
                if (hfVleramag2.val() != "") {
                    editorMag2 = Utils.ktheKontroll('Vlera' + hfVleramag2.val());
                    editorMag2.SetEnabled(true);
                }
            }
            else if (colKushte[i].Vlera == 29) {
                editorMag = Utils.ktheKontroll('Vlera' + hfVleramag.val());
                editorMag.SetEnabled(false);
                if (hfVleramag2.val() != "") {
                    editorMag2 = Utils.ktheKontroll('Vlera' + hfVleramag2.val());
                    editorMag2.SetEnabled(false);
                }
            }
            eksistonKushtMag = true;
        }
        if (colKushte[i].Kodi == 'RBART') {
            if (hfState.Get("AlternativeRBART") == "Po") {
                editorkodbar = Utils.ktheKontroll('Vlera' + hfNKAKNKA.val());
                editorkodbar.SetEnabled(true);
            }
            else {
                editorkodbar = Utils.ktheKontroll('Vlera' + hfNKAKNKA.val());

                editorkodbar.SetEnabled(false);

            }
        }

        if (colKushte[i].Kodi == 'DMT') {
            if (hfVleraFleteHyrje.val() != "") {
                editorZFH = Utils.ktheKontroll('Vlera' + hfVleraFleteHyrje.val());
                if (colKushte[i].Vlera == 139) {
                    editorZFH.SetEnabled(false);
                    kushti_dmt = false;
                }
            }

        }
        if (colKushte[i].Kodi == 'DOKMEKONF') {
            if (hfVleraFleteHyrje.val() != "") {
                if (kushti_dmt) {
                    editorZFH = Utils.ktheKontroll('Vlera' + hfVleraFleteHyrje.val());
                    if (colKushte[i].Vlera == 146) {
                        kushti = false;
                    }
                    else if (colKushte[i].Vlera == 147) {
                        kushti = true;
                    }
                }
            }
        }

        if (colKushte[i].Kodi == 'GJDSH') {
            if (colKushte[i].Vlera == 120) {
                editorMag = Utils.ktheKontroll('Vlera' + hfvlerashit.val());
                editorMag.SetEnabled(true);

            }
            else if (colKushte[i].Vlera == 121) {
                editorMag = Utils.ktheKontroll('Vlera' + hfvlerashit.val());
                editorMag.SetEnabled(false);

            }
        }
        if (colKushte[i].Kodi == 'GJDV' && cmbNivelRegj.GetText() == "Perfitim Buxheti") {
            editorArk = Utils.ktheKontroll('Vlera' + hfvleraark.val());
            editorArk.SetEnabled(hfState.Get("AlternativeGJDV") == "Po");

            editorMag = Utils.ktheKontroll('Vlera' + hfvlerashit.val());
            editorMag.SetEnabled(hfState.Get("AlternativeGJDV") == "Po");
        }
        if (colKushte[i].Kodi == 'GJDV' && cmbNivelRegj.GetText() == "Ekzekutim Buxheti") {
            editorPag = Utils.ktheKontroll('Vlera' + hfvlerepag.val());
            editorPag.SetEnabled(hfState.Get("AlternativeGJDV") == "Po");

            editorBler = Utils.ktheKontroll('Vlera' + hfvlerebler.val());
            editorBler.SetEnabled(hfState.Get("AlternativeGJDV") == "Po");
        }
        if (colKushte[i].Kodi == 'SHSP') {
            if (colKushte[i].Vlera == 58) {
                editorFormula = Utils.ktheKontroll('Vlera' + hfPezull.val());
                editorFormula.SetEnabled(false);

                editorFormula = Utils.ktheKontroll('Vlera' + hfProces.val());
                editorFormula.SetEnabled(false);

            }
        }
        if (colKushte[i].Kodi == 'SHVF') {
            if (colKushte[i].Vlera == 162) {
                editorFormula = Utils.ktheKontroll('Vlera' + hfProces.val());
                editorFormula.SetEnabled(false);
            }
            else {
                editorFormula = Utils.ktheKontroll('Vlera' + hfProces.val());
                editorFormula.SetEnabled(true);

            }
        } if (colKushte[i].Kodi == 'ZT') {

            if (hfZevendesimT.val() == 'Jo') {
                editorFormula = Utils.ktheKontroll('Vlera' + hfZevendesim.val());
                editorFormula.SetEnabled(false);
            }
            else {
                editorFormula = Utils.ktheKontroll('Vlera' + hfZevendesim.val());
                editorFormula.SetEnabled(true);

            }
        }
        if (colKushte[i].Kodi == 'GJDA') {
            if (colKushte[i].Vlera == 176) {
                editorFormula = Utils.ktheKontroll('Vlera' + hfNrAutoDok.val());
                editorFormula.SetEnabled(true);
                editorFormula.SetSelectedIndex(0);
            }
            else {
                editorFormula = Utils.ktheKontroll('Vlera' + hfNrAutoDok.val());
                editorFormula.SetEnabled(false);
                editorFormula.SetText('');

            }
        }
        if (colKushte[i].Kodi == 'GJDR') {
            if (colKushte[i].Vlera == 65) {
                editorMag = Utils.ktheKontroll('Vlera' + hfVlerarez.val());
                editorMag.SetEnabled(true);
            }
            else if (colKushte[i].Vlera == 66) {
                editorMag = Utils.ktheKontroll('Vlera' + hfVlerarez.val());
                editorMag.SetEnabled(false);
            }
            ekzistonkushtrezervim = true;
        }
        if (colKushte[i].Kodi == 'GJDRONSH') {
            editorMag = Utils.ktheKontroll('Vlera' + hfVlerarez.val());
            editorMag.SetEnabled(hfState.Get("AlternativeGJDRONSH" == "Po"));
            ekzistonkushtrezervim = true;
        }
        if (colKushte[i].Kodi == 'IA') {
            if (colKushte[i].Vlera == 9) {
                editorB = Utils.ktheKontroll('Vlera' + hfbarkod.val());
                editorB.SetEnabled(true);
            }

            else if (colKushte[i].Vlera == 8) {
                editorB = Utils.ktheKontroll('Vlera' + hfbarkod.val());
                editorB.SetText();
                editorB.SetEnabled(false);
            }
        }
        if (colKushte[i].Kodi == 'FIDK') {

            if (hfFIDK.val() != "Bosh") {
                editorB = Utils.ktheKontroll('Vlera' + hfLidhjeArketim.val());
                editorB.SetEnabled(true);
            }

            else {
                editorB = Utils.ktheKontroll('Vlera' + hfLidhjeArketim.val());
                editorB.SetText("Bosh");
                editorB.SetEnabled(false);
            }

        }
        if (colKushte[i].Kodi == 'GJK') {
            if (colKushte[i].Vlera == 37) {
                if (Kategoria_ComboBox.GetValue() == '3' || Kategoria_ComboBox.GetValue() == '4' || Kategoria_ComboBox.GetText() == 'Arka' || Kategoria_ComboBox.GetText() == 'Banka') {
                    editorDL = Utils.ktheKontroll('Vlera' + hfVleramag.val());
                    editorDL.SetEnabled(false);
                }
                if (hfvlerakont.val() != "") {
                    editorFK = Utils.ktheKontroll('Vlera' + hfvlerakont.val());
                    editorFK.SetText();
                    editorFK.SetEnabled(false);
                }
                if (hffletakont.val() != "") {
                    editorFK = Utils.ktheKontroll('Vlera' + hffletakont.val());
                    editorFK.SelectIndex(0);
                    editorFK.SetEnabled(false);
                }
            }
            else if (colKushte[i].Vlera == 39 || colKushte[i].Vlera == 40) {
                editorFK = Utils.ktheKontroll('Vlera' + hfvlerakont.val());

                editorFK.SetEnabled(true);
                if (Kategoria_ComboBox.GetValue() == '3' || Kategoria_ComboBox.GetValue() == '4' || Kategoria_ComboBox.GetText() == 'Arka' || Kategoria_ComboBox.GetText() == 'Banka') {
                    editorDL = Utils.ktheKontroll('Vlera' + hfVleramag.val());
                    editorDL.SetEnabled(true);
                }
                if (hffletakont.val() != "") {
                    editorFK = Utils.ktheKontroll('Vlera' + hffletakont.val());
                    editorFK.SetEnabled(true);
                }
            }
            eksistonKushtKontabilizim = true;
        }
        if (colKushte[i].Kodi == 'RBART' && hfVleraDokVartes.val().toLowerCase() == 'po') {
            editorBart = Utils.ktheKontroll('Vlera' + hfBart.val());
            editorBart.SetEnabled(false);
        }
        if (colKushte[i].Kodi == 'RSKDMD' && hfVleraRSKDMD.val().toLowerCase() == 'po') {
            editorFormula = Utils.ktheKontroll('Vlera' + hfSHDQKPMD.val());
            editorFormula.SetEnabled(false);
        }
        if (colKushte[i].Kodi == 'TKLL_B' && hfTKLL_B.val().toLowerCase() == 'bosh') {
            editorFormula = Utils.ktheKontroll('Vlera' + hfZLL_B.val());
            editorFormula.SetEnabled(false);
        }
        if (colKushte[i].Kodi.toLowerCase() == 'zsp') {
            var vleraZSP = Utils.ktheKontroll('Vlera' + hfZSP.val());
            var editorsdaf = Utils.ktheKontroll('Vlera' + hfSDAF.val());
            if (!$.isEmptyObject(editorsdaf)) {
                if (vleraZSP.GetValue() == '' || vleraZSP.GetValue() == null)
                    editorsdaf.SetEnabled(false);
                else
                    editorsdaf.SetEnabled(true);
            }
        }
    }
}

function merrTeDhenaKushte() {//merren te dhenat qe ka grida
    for (i = 0; i < grid_kushte.cpNoRows; i++) {
        colKushte[i].Vlera = Utils.ktheKontroll('Vlera' + i).GetValue();
        if (colKushte[i].Vlera == undefined)
            colKushte[i].Vlera = 0;
    }
}


//LUPA
//per griden kontolle
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function ValueChangedVleraKusht(editor, field, key, kodKushti) {
    hfVleramag = $("input[id$='hfVleraMag']");
    hfNKAKNKA = $("input[id$='hfNKAKNKA']");
    hfVleramag2 = $("input[id$='hfVleraMag2']");
    hfvlerakont = $("input[id$='hfVleraKont']");
    hfLidhjeArketim = $("input[id$='hfLidhjeArketim']");
    hfRezervim = $("input[id$='hfRezervim']");
    hffletakont = $("input[id$='hfFletaKont']");
    hfshit = $("input[id$='hfVleraShit']");
    hfNrAutoDok = $("input[id$='hfNrAutoDok']");
    hfbarkod = $("input[id$='hfVleraBarkod']");
    hfPezull = $("input[id$='hfPezull']");
    hfProces = $("input[id$='hfProces']");
    hfFormule = $("input[id$='hfFormula']");
    hfZevendesim = $("input[id$='hfZevendesim']");
    hfZevendesimT = $("input[id$='hfZevendesimT']");
    hfVleraFleteHyrje = $("input[id$='hfVleraFleteHyrje']");
    hfark = $("input[id$='hfVleraArk']");
    hfpag = $("input[id$='hfVleraPagese']");
    hfbler = $("input[id$='hfVleraBlere']");
    hfSHDQKPMD = $("input[id$='SHDQKPMD");
    hfZLL_B = $("input[id$='hfZLL_B");
    hfSDAF = $("input[id$='hfSDAF");
    if (kodKushti == "GJDM" && editor.GetText() == "Po") {
        editorMag = Utils.ktheKontroll('Vlera' + hfVleramag.val());
        editorMag.SetEnabled(true);
        if (hfVleramag2.val() != "") {
            editorMag2 = Utils.ktheKontroll('Vlera' + hfVleramag2.val());
            editorMag2.SetEnabled(true);
        }
    }

    else if (kodKushti == "GJDM" && editor.GetText() == "Jo") {
        editorMag = Utils.ktheKontroll('Vlera' + hfVleramag.val());
        editorMag.SetText();
        editorMag.SetEnabled(false);
        $("input[id$='hfMagazina']").val(0);
        if (hfVleramag2.val() != "") {
            editorMag2 = Utils.ktheKontroll('Vlera' + hfVleramag2.val()); editorMag2.SetText();
            editorMag2.SetEnabled(false);
        }
    }


    if (kodKushti == "RBART" && editor.GetText() == "Po") {
        editorkodbar = Utils.ktheKontroll('Vlera' + hfNKAKNKA.val());
        editorkodbar.SetEnabled(true);
        hfState.Set("AlternativeRBART", "Po");
    }
    else if (kodKushti == "RBART" && editor.GetText() == "Jo") {
        editorkodbar.SetText("Po");
        editorkodbar.SetEnabled(false);
        hfState.Set("AlternativeRBART", "Po");
    }

    if (kodKushti == "GJDSH" && editor.GetText() == "Po") {
        editorMag = Utils.ktheKontroll('Vlera' + hfshit.val());
        editorMag.SetEnabled(true);

    }
    else if (kodKushti == "GJDSH" && editor.GetText() == "Jo") {
        editorMag = Utils.ktheKontroll('Vlera' + hfshit.val());
        editorMag.SetText();
        editorMag.SetEnabled(false);
        $("input[id$='hfShitja']").val(0);
    }
    if (kodKushti == "GJDV") {
        if (editor.GetText() == "Po") {
            if (cmbNivelRegj.GetText() == "Perfitim Buxheti") {
                editorArk = Utils.ktheKontroll('Vlera' + hfark.val());
                editorArk.SetEnabled(true);

                editorMag = Utils.ktheKontroll('Vlera' + hfshit.val());
                editorMag.SetEnabled(true);
            }
            if (cmbNivelRegj.GetText() == "Ekzekutim Buxheti") {
                editorPag = Utils.ktheKontroll('Vlera' + hfpag.val());
                editorPag.SetEnabled(true);

                editorBler = Utils.ktheKontroll('Vlera' + hfbler.val());
                editorBler.SetEnabled(true);
            }
            hfState.Set("AlternativeGJDV", "Po");
        }
        else {
            if (cmbNivelRegj.GetText() == "Perfitim Buxheti") {
                editorMag = Utils.ktheKontroll('Vlera' + hfshit.val());
                editorMag.SetText();
                editorMag.SetEnabled(false);
                $("input[id$='hfShitja']").val(0);

                editorArk = Utils.ktheKontroll('Vlera' + hfark.val());
                editorArk.SetText();
                editorArk.SetEnabled(false);
                $("input[id$='hfArketim']").val(0);
            }
            if (cmbNivelRegj.GetText() == "Ekzekutim Buxheti") {
                editorPag = Utils.ktheKontroll('Vlera' + hfpag.val());
                editorPag.SetText();
                editorPag.SetEnabled(false);
                $("input[id$='hfPagese']").val(0);

                editorBler = Utils.ktheKontroll('Vlera' + hfbler.val());
                editorBler.SetText();
                editorBler.SetEnabled(false);
                $("input[id$='hfBlere']").val(0);
            }
            hfState.Set("AlternativeGJDV", "Jo");
        }
    }
    if (key == hfZevendesimT.val()) {
        if (editor.GetText() == 'Zëvendësim brenda llojit' || editor.GetText() == 'Zëvendësim lloje të ndryshme' || editor.GetText() == 'Zëvendësim pa kontroll') {
            editorFormula = Utils.ktheKontroll('Vlera' + hfZevendesim.val());
            editorFormula.SetEnabled(true);

        }
        else {
            if (hfZevendesim.val() != "") {
                editorFormula = Utils.ktheKontroll('Vlera' + hfZevendesim.val());
                editorFormula.SetEnabled(false);
            }
        }

    }
    if (kodKushti == "GJDR" && editor.GetText() == "Po") {
        editorMag = Utils.ktheKontroll('Vlera' + hfRezervim.val());
        editorMag.SetEnabled(true);
    }
    else if (kodKushti == "GJDR" && editor.GetText() == "Jo") {
        editorMag = Utils.ktheKontroll('Vlera' + hfRezervim.val());
        editorMag.SetText();
        editorMag.SetEnabled(false);
        hfState.Set("IdKonfigRezervime", 0);
    }
    if (kodKushti == "GJDRONSH") {
        if (editor.GetText() == "Po") {
            editorMag = Utils.ktheKontroll('Vlera' + hfRezervim.val());
            editorMag.SetEnabled(true);
        }
        else {
            editorMag = Utils.ktheKontroll('Vlera' + hfRezervim.val());
            editorMag.SetText();
            editorMag.SetEnabled(false);
            hfState.Set("IdKonfigRezervime", 0);
        }
    }
    if (kodKushti == "IA" && editor.GetText() == "Kodbar") {
        editorB = Utils.ktheKontroll('Vlera' + hfbarkod.val());
        editorB.SetEnabled(true);
    }
    else if (kodKushti == "IA" && editor.GetText() == "Kod") {
        editorB = Utils.ktheKontroll('Vlera' + hfbarkod.val());
        editorB.SetText();
        editorB.SetEnabled(false);
    }
    if (editor.GetText() == "Bosh" && kodKushti != 'TKLL_B') {
        editorB = Utils.ktheKontroll('Vlera' + hfLidhjeArketim.val());
        editorB.SetText("Bosh");
        editorB.SetEnabled(false);

    }
    else if (hfLidhjeArketim.val() != "") {
        editorB = Utils.ktheKontroll('Vlera' + hfLidhjeArketim.val());
        editorB.SetEnabled(true);
    }
    if (kodKushti == "GJK" && editor.GetText() == "Jo") {
        editorFK = Utils.ktheKontroll('Vlera' + hfvlerakont.val());
        editorDL = Utils.ktheKontroll('Vlera' + hfVleramag.val());
        editorFK.SetText();
        editorFK.SetEnabled(false);
        if (Kategoria_ComboBox.GetValue() == '3' || Kategoria_ComboBox.GetValue() == '4' || Kategoria_ComboBox.GetText() == 'Arka' || Kategoria_ComboBox.GetText() == 'Banka') {
            editorDL.SetText();
            editorDL.SetEnabled(false);
        }
        if (hffletakont.val() != "") {
            editorFK = Utils.ktheKontroll('Vlera' + hffletakont.val());
            editorFK.SelectIndex(0);
            editorFK.SetEnabled(false);
        }
        $("input[id$='hfSkemaKontabelRegjistrime']").val(0);
        $("input[id$='hfMagazina']").val(0);
    }
    else if (kodKushti == "GJK" && (editor.GetText() == "Direkt" || editor.GetText() == "Indirekt")) {
        editorFK = Utils.ktheKontroll('Vlera' + hfvlerakont.val());
        editorFK.SetEnabled(true);
        if (Kategoria_ComboBox.GetValue() == '3' || Kategoria_ComboBox.GetValue() == '4' || Kategoria_ComboBox.GetText() == 'Arka' || Kategoria_ComboBox.GetText() == 'Banka') {
            editorDL.SetEnabled(true); editorDL = Utils.ktheKontroll('Vlera' + hfVleramag.val());
        }
        if (hffletakont.val() != "") {
            editorFK = Utils.ktheKontroll('Vlera' + hffletakont.val());
            editorFK.SetEnabled(true);
        }
    }
    if (kodKushti == "V" && editor.GetText() == "Jo") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfFormule.val());
        editorFormula.SetEnabled(true);
    }
    if (kodKushti == "SHVF" && editor.GetText() == "Po") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfProces.val());
        editorFormula.SetEnabled(false);
        editorFormula.SetText('Jo');
    }
    if (kodKushti == "SHVF" && editor.GetText() == "Jo") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfProces.val());
        editorFormula.SetEnabled(true);
    }
    if (kodKushti == "GJDA" && editor.GetText() == "Po") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfNrAutoDok.val());
        editorFormula.SetEnabled(true);
        editorFormula.SetSelectedIndex(0);
    }
    if (kodKushti == "GJDA" && editor.GetText() == "Jo") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfNrAutoDok.val());
        editorFormula.SetEnabled(false);
        editorFormula.SetText('');
    }
    if (kodKushti == "SHSP" && editor.GetText() == "Po") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfPezull.val());
        editorFormula.SetEnabled(true);
        editorFormula = Utils.ktheKontroll('Vlera' + hfProces.val());
        editorFormula.SetEnabled(true);
    }
    if (kodKushti == "SHSP" && editor.GetValue() == "Jo") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfPezull.val());
        editorFormula.SetEnabled(false);
        editorFormula.SetText('Po');
        editorFormula = Utils.ktheKontroll('Vlera' + hfProces.val());
        editorFormula.SetEnabled(false);
        editorFormula.SetText('Po');
    }
    if (kodKushti == "RSKDMD" && editor.GetText().toLowerCase() == "jo") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfSHDQKPMD.val());
        editorFormula.SetEnabled(true);
    }
    if (kodKushti == "RSKDMD" && editor.GetText().toLowerCase() == "po") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfSHDQKPMD.val());
        editorFormula.SetEnabled(false);
        editorFormula.SetText('Jo');
    }
    if (kodKushti == "TKLL_B" && editor.GetText().toLowerCase() != "bosh") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfZLL_B.val());
        editorFormula.SetEnabled(true);
    }
    if (kodKushti == "TKLL_B" && editor.GetText().toLowerCase() == "bosh") {
        editorFormula = Utils.ktheKontroll('Vlera' + hfZLL_B.val());
        editorFormula.SetEnabled(false);
    }
    if (kodKushti.toLowerCase() == 'zsp') {
        var editorSDAF = Utils.ktheKontroll('Vlera' + hfSDAF.val());
        if (!$.isEmptyObject(editorSDAF)) {
            if (editor.GetText() == "") {
                editorSDAF.SetEnabled(false);
                editorSDAF.SetSelectedItem(editorSDAF.FindItemByText('Draft'));
            }
            else
                editorSDAF.SetEnabled(true);
        }
    }
}

function LostFocusLloji(editor, key) {
    var hf = $("input[id$='hfPrioriteti']");
}

function LostFocusMag(editor, key) {
    var hf = $("input[id$='hfMagazina']");
    hf.val(editor.GetValue());
}
function LostFocusShit(editor, key) {
    var hf = $("input[id$='hfShitja']");
    hf.val(editor.GetValue());
}
function LostFocusArk(editor, key) {
    var hf = $("input[id$='hfArketim']");
    hf.val(editor.GetValue());
}
function LostFocusPag(editor, key) {
    var hf = $("input[id$='hfPagese']");
    hf.val(editor.GetValue());
}
function LostFocusBler(editor, key) {
    var hf = $("input[id$='hfBlere']");
    hf.val(editor.GetValue());
}
function LostFocusRez(editor, key) {

    hfState.Set("IdKonfigRezervime", editor.GetValue());
}

function LostFocusMag2(editor, key) {
    var hf = $("input[id$='hfMagazina']");
    hf.val(editor.GetValue());
}

function LostFocusPlan(editor, key) {
    var hf = $("input[id$='hfMagazina']");
    hf.val(editor.GetValue());
}
function LostFocusPer(editor, key) {

}
function LostFocusSkemaWF(editor, key) {
    //var hf = $("input[id$='hfMagazina']");
    //hf.val(editor.GetValue());
}

function LostFocusDL(editor, key) {
    var hf = $("input[id$='hfMagazina']");
    hf.val(editor.GetValue());
}
function LostFocusAM(editor, key) {
    var hf = $("input[id$='hfAmortizimi']");
    hf.val(editor.GetValue());
}

function LostFocusFH(editor, key) {
    var hf = $("input[id$='hfVleraFleteHyrje']");
    hf.val(editor.GetValue());
}

function LostFocusRQK(editor, key) {
    var hf = $("input[id$='hfQK']");
    hf.val(editor.GetValue());
}

function LostFocusFK(editor, key) {
    var hf = $("input[id$='hfSkemaKontabelRegjistrime']");
    hf.val(editor.GetValue());
}

function LostFocusFiltri(editor, key) {
    var hf = $("input[id$='hfFiltri']");
    hf.val(editor.GetValue());
}

function Init() {
    myMenu.menuSipasTeDrejtaRegjistrimPaDraft($("input[id$='hfShtimModifikim']"), hfTeDrejta);
    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblInformacion",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi",
        init: true,
        visible: true
    });
    cmbAutorizimi.SetText($("#cmbAutorizimiHf").val());
    shtoRreshtatEkzistuesNeMatrice(); //shton vlerat e konfigurimit default te kontrolleve
    shtoRreshtatEkzistuesTrupitNeMatrice(); //shton vlerat e konfigurimit default te grides se trupit
    shtoRreshtatEkzistuesKushtiNeMatrice(); //shton vlerat e konfigurimit default te grides se kushteve
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('KonfigurimDokumentash.aspx?idsuperkat=' + Utils.getUrlVar('idsuperkat'), 0);
    myCookies.createCookie('adresa', window.location.href, 1);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    if ($("input[id$='hfShtimModifikim']").val() == "modifikim") {
        Kategoria_ComboBox.SetEnabled(false);
        cmbNivelRegj.SetEnabled(false);
        kodKonfig_TextBox.SetEnabled(false);
    }
    var idGjuha = hfState.Get("_idGjuha");
    pageState.txtVleraDefault = idGjuha == 0 ? 'txtVleraDefault' : idGjuha == 1 ? 'txtVleraDefaultEng' : 'txtVleraDefault_fr';

}
function RuajClick(s, e) {
    var hf4 = $("input[id$='hfMagazina']");
    var hf2 = $("input[id$='hfShitja']");
    var hfArka = $("input[id$='hfArketim']");
    var hfPag = $("input[id$='hfPagese']");
    var hfBler = $("input[id$='hfBlere']");
    var hf1 = $("input[id$='hfSkemaKontabelRegjistrime']");
    merrTeDhena();
    merrTeDhenaTrupi();
    merrTeDhenaKushte();
    for (o = 0; o < colKushte.length; o++) {
        if (colKushte[o].Kodi == "GJDM" && colKushte[o].Vlera == 28 && hf4.val() == 0) {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e dokumentit te magazines!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "GJDSH" && colKushte[o].Vlera == 120 && hf2.val() == 0) {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e dokumentit te shitjes!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "GJDV" && hfState.Get("AlternativeGJDV") == "Po" && hfArka.val() == 0 && cmbNivelRegj.GetText() == "Perfitim Buxheti") {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e dokumentit te arketimit!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "GJDV" && hfState.Get("AlternativeGJDV") == "Po" && hfPag.val() == 0 && cmbNivelRegj.GetText() == "Ekzekutim Buxheti") {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e dokumentit te pageses!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "GJDV" && hfState.Get("AlternativeGJDV") == "Po" && hfBler.val() == 0 && cmbNivelRegj.GetText() == "Ekzekutim Buxheti") {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e dokumentit te blerjes!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "GJDV" && hfState.Get("AlternativeGJDV") == "Po" && hf2.val() == 0 && cmbNivelRegj.GetText() == "Perfitim Buxheti") {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e dokumentit te shitjes!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "ZKR" && colKushte[o].Vlera == 66 && hfState.Get("IdKonfigRezervime") == 0) {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e dokumentit te rezervimit!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "GJK" && (colKushte[o].Vlera == 39 || colKushte[o].Vlera == 40) && hf1.val() == 0 && (Kategoria_ComboBox.GetValue() != '5' && Kategoria_ComboBox.GetText() != 'Flete Kontabel')) {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e fletes kontabel!');
            e.processOnServer = false;
            return;
        }
        if (colKushte[o].Kodi == "GJK" && (colKushte[o].Vlera == 39 || colKushte[o].Vlera == 40) && hf4.val() == 0 && (Kategoria_ComboBox.GetValue() == '3' || Kategoria_ComboBox.GetValue() == '4' || Kategoria_ComboBox.GetText() == 'Arka' || Kategoria_ComboBox.GetText() == 'Banka')) {
            myMesazh.ShtoMesazhGabimi('Ju lutem jepni konfigurimin e lidhjes se dokumentave!');
            e.processOnServer = false;
            return;
        }
    }
    Utils.shfaqLoadingGif();
    valido(s, e);

    hf = $("input[id$='hfVleratFillestareKontrollet']");
    hf.val(JSON.stringify(colAtribute));
    hf1 = $("input[id$='hfVleratFillestareTrupi']");
    hf1.val(JSON.stringify(colTrupi));
    hf2 = $("input[id$='hfVleratFillestareKushtet']");
    hf2.val(JSON.stringify(colKushte));
}

function valido(s, e) {
    isvalid = ASPxClientEdit.ValidateGroup("entries");
    if (isvalid == false) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
    }
}

function index_EndCallback() {
    shtoRreshtatEkzistuesKushtiNeMatrice();
    shtoRreshtatEkzistuesNeMatrice();
    shtoRreshtatEkzistuesTrupitNeMatrice();
}

function indexChange() {
    panel.PerformCallback();
}

function EndRequestHandler(sender, args) {
    Utils.hiqLoadingGif();
}

function menu_click(s, e) {//po
    switch (e.item.name) {
        case 'Ruaj':
            Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
            myFaqeCelje.validim(s, e);
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
            break;
        case 'Shto':
            PastroClick(s, e);
            break;
        case 'Anullo':
            myFaqeCelje.kontrolloTeDrejta("KonfigDokumentash.aspx?idsuperkat=" + Utils.getUrlVar('idsuperkat'));
            e.processOnServer = false;
            click = false;
            break;
        case 'Klono':
            Utils.shfaqLoadingGif();
            myFaqeCelje.kontrolloTeDrejta("KonfigurimDokumentash.aspx?idsuperkat=" + Utils.getUrlVar('idsuperkat') + '&id=' + Utils.getUrlVar('id') + '&shtim_modifikim=klonim');
            break;
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("KonfigDokumentash.aspx?idsuperkat=" + Utils.getUrlVar('idsuperkat') + "&ruaj=po");
}

function PastroClick(s, e) {
    $("input[id$='hfShtimModifikim']").val("shtim");
    $("input[id$='hfMagazina']").val('');
    $("input[id$='hfShitja']").val('');
    $("input[id$='hfArketim']").val('');
    $("input[id$='hfPagese']").val('');
    $("input[id$='hfBlere']").val('');
    $("input[id$='hfQK']").val('');
    $("input[id$='hfSkemaKontabelRegjistrime']").val('');
    $("input[id$='hfFormula']").val('');
    $("input[id$='hfVleraFleteHyrje']").val('');
    Kategoria_ComboBox.SelectIndex(-1);
    Kategoria_ComboBox.SetEnabled(true);
    cmbNivelRegj.SetEnabled(true);
    kodKonfig_TextBox.SetText('');
    pershkrimKonfig_TextBox.SetText('');
    pershkrimKonfigEng_TextBox.SetText('');
    pershkrimKonfigFr_TextBox.SetText('');
    cmbAutorizimi.SetText('');
    radhaTextBox.SetText('');
    kodKonfig_TextBox.SetEnabled(true); e.processOnServer = false;
    textChange();
    ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    myMenu.menuSipasTeDrejtaRegjistrimPaDraft($("input[id$='hfShtimModifikim']"), hfTeDrejta);
}

function textChange() {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNiveleSipasKat"),
        data: JSON.stringify({ kategoria: Kategoria_ComboBox.GetText(), idNdermarrje: hfState.Get("_idNdermarrje"), idPerdoruesi: hfState.Get("_idPerdoruesi") })
    }).done(SucceededCallbackNiveli);
}

function SucceededCallbackNiveli(result) {
    cmbNivelRegj.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbNivelRegj.AddItem(result[i].Pershkrimi, result[i].IdNivel); //AddItem(teksti, vlera);
    cmbNivelRegj.SelectIndex(0);
    indexChange();
}

var KPF;

function Autorizime_Click() {//po
    identifikuesPerPopupAutorizime = 'KonfigurimDokumentashLupaAutorizim';
    var queryStr = '?autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.LupaUniversal_Click('Zgjidh autorizimet', 'LupaAutorizim.aspx' + queryStr, 800, 800);
}

function ButtonClickedFormula(editor, key, koka) {
    editorFormula = editor;
    identikuesPerPopupFormula = 'KonfigurimDokumentash';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh formulën', 'LupaFormula.aspx', 850, 600);
}

function LostFocusFormula(editor, key) {
    editorFormula = editor;
    var hf = $("input[id$='hfFormula']");
    hf.val(editor.GetValue());
}

function ButtonClickedAutorizime(editor, index) {
    editorAutorizime = Utils.ktheKontroll(editor);
    myButtonClickLupa.LupaUniversal_Click('Zgjidh autorizimet', 'LupaAutorizim.aspx', 800, 800);
}

function ButtonClickedGrupimKlienti(editor, index) {
    editorGrupKlientFurnitor = Utils.ktheKontroll(editor);
    var llojikf;
    if (cmbNivelRegj.GetText().indexOf('Klient') != -1)
        llojikf = 'klient';
    else if (cmbNivelRegj.GetText().indexOf('Furnitor') != -1)
        llojikf = 'furnitor';
    myButtonClickLupa.LupaUniversal_Click('Zgjidh grupin e klient/furnitorit', 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=1&kf=' + llojikf, 700, 600);
}

function FormatNumri_Click() {
    var idKategoria = -1;
    var kategoria = Kategoria_ComboBox.GetValue();
    if (kategoria != undefined && kategoria != null && kategoria != "")
        idKategoria = kategoria;
    myButtonClickLupa.LupaUniversal_Click('Zgjidh formatin e numrit', 'LupaFormatNumrash.aspx?idKategoria=' + idKategoria, 700, 550);
}

function Kategoria_ComboBoxSelectedIndexChanged(s, e) {
    cmbNivelRegj.SetText('');
    cmbFormatNumri.SetText('');
    cmbFormatNumri.SetSelectedIndex(-1);
    textChange();
}

function cmbLlojSubjektiDefaultSelectedIndexChanged() {
    var comboSubjekti = eval('Vlera' + $("input[id$='hfSubjektiDefaultVlera']").val());
    comboSubjekti.ClearItems();
}

function cmbDokumtVarteSelectedIndexChanged(s, e) {
    if (s.GetText().toLowerCase() == 'po')
        editorBart.SetEnabled(false);
    else
        editorBart.SetEnabled(true);
}

function VendosFushaLlogarieNePageState() {

    var llogariInv = colAtribute.filter(function (item) { return item.PershkrimKontroll == 'Caktimi i llogarise se inventarit'; })[0];
    var llogariB = colAtribute.filter(function (item) { return item.PershkrimKontroll == 'Caktimi i llogarise se blerjes'; })[0];
    var llogariSh = colAtribute.filter(function (item) { return item.PershkrimKontroll == 'Caktimi i llogarise se shitjes'; })[0];
    var llogariTr = colAtribute.filter(function (item) { return item.PershkrimKontroll == 'Caktimi i llogarise me te tretet'; })[0];
    var llogariShp = colAtribute.filter(function (item) { return item.PershkrimKontroll == 'Caktimi i llogarise se shpenzimeve'; })[0];
    var llogariA = colAtribute.filter(function (item) { return item.PershkrimKontroll == 'Caktimi i llogarise se amortizimit'; })[0];
    var fushaLlogarie = {
        llogariInv: { index: colAtribute.indexOf(llogariInv), fusha: llogariInv },
        llogariB: { index: colAtribute.indexOf(llogariB), fusha: llogariB },
        llogariSh: { index: colAtribute.indexOf(llogariSh), fusha: llogariSh },
        llogariTr: { index: colAtribute.indexOf(llogariTr), fusha: llogariTr },
        llogariShp: { index: colAtribute.indexOf(llogariShp), fusha: llogariShp },
        llogariA: { index: colAtribute.indexOf(llogariA), fusha: llogariA }
    };
    pageState.fushaLlogarie = fushaLlogarie;
}