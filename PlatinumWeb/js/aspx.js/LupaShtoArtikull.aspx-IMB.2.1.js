function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaShtoArt.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

var arrKod = new Array();
var arrEmertimiA = new Array();
var arrPrioritetiA = new Array();
var identikuesPerPopupArtikulli = "artikull";

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

var cou1 = 0;
var indeksi = -1;
var editorKod;
var editorEmertimiA;
var editorPrioritetiA;
var indexCounter = 0;

function merrTeDhena() {//merren te dhenat qe ka grida
    arrKod = new Array();
    arrEmertimiA = new Array();
    arrPrioritetiA = new Array();
    cou1 = 0;
    indexCounter = 0;

    for (i = 0; i < gvLupaShtoArt.cpNoRows; i++) {
        editorKod = Utils.ktheKontroll('txtKod' + i);
        editorEmertimiA = Utils.ktheKontroll('txtEmertimi' + i);
        editorPrioritetiA = Utils.ktheKontroll('txtPrioritetiA' + i);

        arrKod[cou1] = i.toString() + ":" + editorKod.GetText();
        arrEmertimiA[cou1] = i.toString() + ":" + editorEmertimiA.GetText();
        arrPrioritetiA[cou1] = i.toString() + ":" + editorPrioritetiA.GetText();

        //cou1 += 1;
        cou1 =cou1 + 1;
    }

    hfArtikulli.SetText(arrKod);
    hfEmertimiA.SetText(arrEmertimiA);
    hfPrioritetiA.SetText(arrPrioritetiA);

    window.parent.document.getElementById("hfArtikulli").value = hfArtikulli.GetText();  //shton vleren tek hidden field e cila perdoret me vone nga kodi
    window.parent.document.getElementById("hfEmertimiA").value = hfEmertimiA.GetText();
    window.parent.document.getElementById("hfPrioritetiA").value = hfPrioritetiA.GetText();
}

//kontrollon nese jemi ne rreshtin e fundit
function KeyPress(kodi, editor, key, idKonfig) {
    if (kodi == 13) {
        arrKod = editor;
        popupUniversal.SetHeaderText('Zgjidh Artikullin');
        editorEmertimiA = Utils.ktheKontroll('txtEmertimi' + key);
        document.getElementById('<%= Container.ClientID %>').src = 'LupaArtikull.aspx?idKonfigAmbjente=' + idkonfig;
        popupUniversal.Show();
    }
}

//kontrollon nese jemi ne rreshtin e fundit
function LostFocus(editor, editorEmer, key) {
    if (key == 0) {
        window.parent.btneArtikuj.SetText(editor.GetText());
    }

    if (key == gvLupaShtoArt.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvLupaShtoArt.PerformCallback();
    }
    merrTeDhena();
}

//kontrollon nese jemi ne rreshtin e fundit
function ButtonClicked(editor, key, idKonfigambjenti) {
    editorKod = editor;
    editorEmertimiA = Utils.ktheKontroll('txtEmertimi' + key);
    popupUniversal.SetHeaderText('Zgjidh Artikullin');
    document.getElementById('<%= Container.ClientID %>').src = 'LupaArtikull.aspx?idKonfigAmbjente=' + idKonfigambjenti;
    popupUniversal.Show();
}

//kontrollon nese jemi ne rreshtin e fundit
function TextChanged(editor, editorEmer, key) {
    var a = new Array();
    a = editor.GetText().toString().split(',');
    editor.SetText(a[0]);
    editorEmer.SetText(a[1]);
    if (key == 0) {
        window.parent.btneArtikuj.SetText(editor.GetText());
    }
    if (key == gvLupaShtoArt.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvLupaShtoArt.PerformCallback();
    }
    merrTeDhena();
}

function GotFocus(editor, key) {
    if (key == gvLupaShtoArt.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvLupaShtoArt.PerformCallback();
    }
}

//kontrollon nese jemi ne rreshtin e fundit
function TextChangedPrioritetiA(editor, key) {
    if (key == gvLupaShtoArt.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvLupaShtoArt.PerformCallback();
    }
    merrTeDhena();
}

//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    gvLupaShtoArt.PerformCallback(key);
    merrTeDhena();
}
function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaShtoArt&page=LupaShtoArtikull.aspx&idKonfigAmbjente=0';
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
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaShtoArt.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaShtoArt.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');