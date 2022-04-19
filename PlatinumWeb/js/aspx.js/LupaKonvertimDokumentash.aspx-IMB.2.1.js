function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKonvDok, "624", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

var identikuesPerPopupKlientFurnitori = "Dokumenta";
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonvDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonvDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();

        gvLupaKonvDok.SetFocusedRowIndex(0);
        btnOk.Focus();
        if (rbNga.GetChecked() == false) {
            dteNga.SetEnabled(false);
            dteDeri.SetEnabled(false);
        }
        else {
            dteNga.SetEnabled(true);
            dteDeri.SetEnabled(true);
        }

    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
function KeyPresKF(kodi, editor, key) {//kur shtypet nje key per kolonen KPF debi
    if (kodi == 13) {
        indeksi = key;
        editorKF = Utils.ktheKontroll('IdKlientFurnitori');
        KK_Click();


    }
}

function LostFocusKF(key) {//kur humb fokusin kolona KPF debi
    indeksi = key;
    editorKF = Utils.ktheKontroll('IdKlientFurnitori');
    vlera = 0;


}
function ButtonClickedKF(editor, key) {//kur klikon butonin e kolones KPF debi
    indeksi = key;
    editorKF = editor;
    KF_Click();


}
var editorKF;
function TextChangedKF(key) {//kur ndryshon texti tek kolona KPF debi
    indeksi = key;
    var editor = Utils.ktheKontroll('IdKlientFurnitori');

    editorKF = editor;

}
function KF_Click() {
    var editorniveli = Utils.ktheKontroll('IdNiveli');
    popupUniversal.SetHeaderText('Zgjidh klient/furnitorin');
    if (editorniveli.GetText() == 'FB' || editorniveli.GetText() == 'KB' || editorniveli.GetText() == 'OB' || editorniveli.GetText() == 'UB')
        document.getElementById('<%= Container.ClientID %>').src = 'LupaKlientFurnitor.aspx?veprimi=1';
    else if (editorniveli.GetText() == 'FSH' || editorniveli.GetText() == 'KSH' || editorniveli.GetText() == 'OSH' || editorniveli.GetText() == 'USH')

        document.getElementById('<%= Container.ClientID %>').src = 'LupaKlientFurnitor.aspx?veprimi=2';
    else
        document.getElementById('<%= Container.ClientID %>').src = 'LupaKlientFurnitor.aspx';

    popupUniversal.Show();
}
function ProcessKeyPress() {
    var currentIndex = gvLupaKonvDok.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKonvDok.GetVisibleRowsOnPage() - 1) {
            gvLupaKonvDok.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKonvDok.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKonvDok.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function buttonClick() {
    vlera = 0;
    if (rbAktuale.GetChecked() == true)
        vlera = 1;
    if (rbVitiUshtrimor.GetChecked() == true)
        vlera = 2;
    if (rbNga.GetChecked() == true)
        vlera = "3";//:" + dteNga.GetText() + ":" + dteDeri.GetText();
    gvLupaKonvDok.PerformCallback(vlera);
}
function OnGridSelectionChanged() {
    //gvLupaKonvDok.GetSelectedFieldValues('NrLlogariKF;EmertimiKF', OnGridSelectionComplete);
    gvLupaKonvDok.GetSelectedFieldValues('IdDokumenti;NrDokumenti;Pershkrimi;IdNiveli;IdKlientFurnitori', OnGridSelectionComplete);
}
var id;
function OnGridSelectionComplete(values) {
    if (values.length === 0) {
        alert('Duhet te zgjidhni te pakten nje dokument per te konvertuar!')
        return;
    }
    var merrTeDhena = false;


    if (window.parent.countkonvertime == 0) {
        var idnivel = values[0][3];
        merrTeDhena = true;
    }
    else idnivel = window.parent.resultkonvertime[0][3];

    if (Utils.getUrlVar('idklient') != "null" && Utils.getUrlVar('idklient') != 0 && Utils.getUrlVar('idklient') != -1) {
        var idklienti = Utils.getUrlVar('idklient');
        if (values[0][4] != 0 && idklienti != values[0][4]) {
            alert('Dokumentat e zgjedhur duhet te kene te njejtin klient furnitor!');
            return;
        }
    }
    else idklienti = values[0][4];
    var count = window.parent.countkonvertime;
    var result = new Array();
    var countresult = 0;
    if (values.length > 1)
        merrTeDhena = false;
    for (i = 0; i < values.length; i++) {
        if (values[i][3] != idnivel) {
            alert('Dokumentat e zgjedhur duhet te jene te te njejtes nenkategori!');
            return;
        }

        else if (idklienti == 0 && values[i][4] != 0)
            idklienti = values[i][4];
        else if (values[i][4] != 0 && idklienti != values[i][4]) {
            alert('Dokumentat e zgjedhur duhet te kene te njejtin klient furnitor!');
            return;
        }
        var ekziston = false;
        for (m = 0; m < window.parent.countkonvertime; m++)
            if (values[i][0] == window.parent.resultkonvertime[m][0] && values[i][3] == window.parent.resultkonvertime[m][3]) {
                alert('Dokumenti me nr ' + values[i][1] + ' eshte konvertuar.');
                ekziston = true;
                break;
            }
        if (!ekziston) {
            window.parent.resultkonvertime[count] = values[i];
            count++;
            result[countresult] = values[i];
            countresult++;

        }
    }
    window.parent.countkonvertime = count;


    if (window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimDokumentash.aspx") {

        window.parent.Konverto(result, merrTeDhena);
        ////////////////////////////              //      window.parent.location = "Shto_RegjistrimDokumentash.aspx?shitje_blerje="+Utils.getUrlVar('shitje_blerje')+"&id=" + id + "&shtim_modifikim=konvertim&niveli=" +Utils.getUrlVar('niveli')+"&nivelkonvertues="+nivel+"&konfigurim="+Utils.getUrlVar('konfigurim');
    }

    window.parent.popupUniversal.Hide();
}
function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Iframe1.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKonvDok&page=LupaKonvertimDokumentash.aspx&idKonfigAmbjente=577';
    popFiltra.Show();
}

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}
function CheckedChanged_rbVitiUshtrimor(s, e) {
    if(rbVitiUshtrimor.GetChecked()==true)
    {rbAktuale.SetChecked(false);
        rbNga.SetChecked(false);
        dteNga.SetEnabled(false);
        dteDeri.SetEnabled(false); buttonClick();
    }
}
function CheckedChanged_rbNga(s, e) {
    if(rbNga.GetChecked()==true)
    {rbAktuale.SetChecked(false);
        rbVitiUshtrimor.SetChecked(false);
        dteNga.SetEnabled(true);
        dteDeri.SetEnabled(true); buttonClick();
    }
}
function CheckedChanged_rbAktuale(s, e) {
    if(rbAktuale.GetChecked()==true)
    {rbVitiUshtrimor.SetChecked(false);
        rbNga.SetChecked(false);
        dteNga.SetEnabled(false);
        dteDeri.SetEnabled(false);  buttonClick();
    }
}