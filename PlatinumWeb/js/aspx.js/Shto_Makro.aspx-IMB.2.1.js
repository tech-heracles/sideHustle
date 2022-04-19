
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
                                                    evt.keyCode : evt.charCode;
}
function getClientID(key) {
    //return constanteParashtese + key + constantePrapashtese + 'txtNrLlogari' + key;
    return 'txtProdukti' + key;
}
var identikuesPerPopupArtikulli = 'Makro';
function enable() {
    for (i = 0; i < gvTrupi.cpNoRows; i++) {
        editorLloji = Utils.ktheKontroll('cmbLloji' + i);
        var nrProduktClientID = getClientID(i);
        editorProdukti = $("input[id$=" + nrProduktClientID + "]")[0]
        //            
        editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + i);
        editorFunksioni = Utils.ktheKontroll('cmbFunksioni' + i);
        editorVlera = Utils.ktheKontroll('txtVlera' + i);
        editorRenditja = Utils.ktheKontroll('cmbRenditja' + i);
        var editorButon = Utils.ktheKontroll('btnHap' + i);
        if (editorLloji.GetValue() == 1) {
            editorFunksioni.SetEnabled(true);
            editorPershkrimi.SetEnabled(false);
            editorVlera.SetEnabled(true);
            editorRenditja.SetEnabled(true);

            editorProdukti.disabled = false;
            editorButon.SetEnabled(true);

        }
        else if (editorLloji.GetValue() == 2) {
            editorFunksioni.SetEnabled(false);
            editorPershkrimi.SetEnabled(false);
            editorVlera.SetEnabled(false);
            editorRenditja.SetEnabled(false);

            editorProdukti.disabled = false;
            editorButon.SetEnabled(true);
        }
        else if (editorLloji.GetValue() == 3) {
            editorFunksioni.SetEnabled(false);
            editorPershkrimi.SetEnabled(true);
            editorVlera.SetEnabled(false);
            editorRenditja.SetEnabled(false);

            editorProdukti.disabled = true;
            editorButon.SetEnabled(false);
        }

    }
}

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
    window.parent.callWebServiceKtheInfoLart('Shto_Makro.aspx', 0);
    ////            callWebservicePerUserLabel(emrimenuse);
    window.parent.createCookie('adresa', 'Shto_Makro.aspx', 1);

}
var KPF;
function Autorizime_Click() {
    popupUniversal.SetHeaderText('Zgjidh autorizimet');
    //     document.getElementById().src = 'LupaAutorizim.aspx';

    popupUniversal.Show();
}

function Artikull_Click() {
    popupUniversal.SetHeaderText('Zgjidh artikullin');

    //   document.getElementById().src = 'LupaArtikull.aspx';

    popupUniversal.Show();
}
function Makro_Click() {
    popupUniversal.SetHeaderText('Zgjidh makron');

    //   document.getElementById().src = 'LupaMakro.aspx';

    popupUniversal.Show();
}
var editorValues = new Object();
function InitAutorizim() {
    var hf = document.getElementById('HiddenField1');

    if (hf.value == '') {
        editorValues["KodiKokaMakro"] = "";
        editorValues["PershkrimiKokaMakro"] = "";
        editorValues["IdNivelAutorizimi"] = "";

        hf.value = editorValues["KodiKokaMakro"] + ";" + editorValues["PershkrimiKokaMakro"] + ";"
    + editorValues["IdNivelAutorizimi"] + ";";
    }

    var listeFushash = hf.value.split(';');

    KodiKokaMakro.SetText(listeFushash[0]);

    PershkrimiKokaMakro.SetText(listeFushash[1]);
    IdNivelAutorizimi.SetText(listeFushash[2]);


}

//pastron fushat
function Init() {
    changeName();
    enable();

}

var grida


//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {

}
var arrLloj = new Array();
var arrProdukti = new Array();
var arrPershkrimi = new Array();
var arrFunksioni = new Array();
var arrVlera = new Array();
var arrRenditja = new Array();
var cou1 = 0;

var indeksi = -1;
var editorLloji;
var editorProdukti;
var editorPershkrimi;
var editorFunksioni;
var editorVlera;
var editorRenditja;
var valid = false;
var indexCounter = 0;
function merrTeDhena() {//merren te dhenat qe ka grida
    arrLloj = new Array();
    arrProdukti = new Array();
    arrPershkrimi = new Array();
    arrFunksioni = new Array();
    arrVlera = new Array();
    arrRenditja = new Array();
    cou1 = 0;
    indexCounter = 0;
    var hidField1 = document.getElementById("hfLloji"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidfield2 = document.getElementById("hfProdukti");
    var hidField3 = document.getElementById("hfPershkrimi");
    var hidField4 = document.getElementById("hfFunksioni");
    var hidField5 = document.getElementById("hfVlera");
    var hidField6 = document.getElementById("hfRenditja");
    for (i = 0; i < gvTrupi.cpNoRows; i++) {
        editorLloji = Utils.ktheKontroll('cmbLloji' + i);
        var nrProduktClientID = getClientID(i);
        editorProdukti = $("input[id$=" + nrProduktClientID + "]")[0]

        editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + i);
        editorFunksioni = Utils.ktheKontroll('cmbFunksioni' + i);
        editorVlera = Utils.ktheKontroll('txtVlera' + i);
        editorRenditja = Utils.ktheKontroll('cmbRenditja' + i);

        if (editorLloji.GetText() != '') {

            valid = true;
        }
        arrLloj[cou1] = i.toString() + ":" + editorLloji.GetValue();

        arrProdukti[cou1] = i.toString() + ":" + editorProdukti.value;
        arrPershkrimi[cou1] = i.toString() + ":" + editorPershkrimi.GetText();
        arrFunksioni[cou1] = i.toString() + ":" + editorFunksioni.GetValue();
        arrVlera[cou1] = i.toString() + ":" + editorVlera.GetText();
        arrRenditja[cou1] = i.toString() + ":" + editorRenditja.GetText();

        //cou1 += 1;
        cou1 = cou1 + 1;

    }

    hidField1.value = arrLloj;
    hidfield2.value = arrProdukti;
    hidField3.value = arrPershkrimi;
    hidField4.value = arrFunksioni;
    hidField5.value = arrVlera;
    hidField6.value = arrRenditja;


}
var constanteParashtese = 'ASPxRoundPanel1_ASPxCallbackPanel1_gvTrupi_cell';
var constantePrapashtese = '_4_';
//kur zgjidhet nje element nga autosuggesti qe te vendoset edhe pershkrimi i llogarise
function OnContactSelected(source, eventArgs) {
    //alert('kot');
    var gjatesiaParashteses = constanteParashtese.length;
    var tempStr = source.get_element().id.substring(gjatesiaParashteses, source.get_element().id.length);
    var poziocionVize = tempStr.indexOf("_");
    var indexRreshti = tempStr.substring(0, poziocionVize);
    editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + indexRreshti);
    editorPershkrimi.SetText(eventArgs.get_value());
}
var indeksPerEmerLlogarie
function LostFocusProdukti(editor, key) {
    indeksPerEmerLlogarie = key;
    var value = document.getElementById(editor).value
    if (value != '')
        callWebservice(value, key);
    indeksi = indexCounter;
    merrTeDhena();
    ////            if (key == gvFleteKontabelTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
    ////                indexCounter = indexCounter + 1;
    ////                merrTeDhena();
    ////                gvFleteKontabelTrupi.PerformCallback('shto,' + 'NrLlogari');
    ////            }                
}
function callWebservice(name, key) {
    editorLloji = Utils.ktheKontroll('cmbLloji' + key);
    if (editorLloji.GetValue() == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimArtikulli"),
            data: JSON.stringify({ prefixText: name })
        }).done(SucceededCallback);
    }
    else {
        if (editorLloji.GetValue() == 2) {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimMakro"),
                data: JSON.stringify({ prefixText: name, idNderViti: hfState.Get("idNderViti"), idNdermarrje: hfState.Get("idNdermarrje"), idPerdoruesi: hfState.Get("idPerd") })
            }).done(SucceededCallback);
        }
    }


}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + indeksPerEmerLlogarie);
    editorPershkrimi.SetText(result.split(';')[0]);
    if (indeksPerEmerLlogarie == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit
function ButtonClickedProdukti(editor, key) {
    editorProdukti = document.getElementById(editor);
    editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + key);
    editorLloji = Utils.ktheKontroll('cmbLloji' + key);
    if (editorLloji.GetValue() == 1) {
        Artikull_Click();
    }
    else {
        Makro_Click();
    }

}
function KeyPressProdukti(editor, key) {
    if (event.keyCode == 13) {
        editorProdukti = document.getElementById(editor);
        editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + key);
        editorLloji = Utils.ktheKontroll('cmbLloji' + key);
        if (editorLloji.GetValue() == 1) {
            Artikull_Click();
        }
        else {
            Makro_Click();
        }
        event.returnValue = false;
        event.cancel = true;
    }

}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedProdukti(editor, editorEmer, key) {
    ////            indeksi = indexCounter;
    ////////            alert('ok');
    ////////            var a = new Array();
    ////////            a = editor.GetText().toString().split(',');
    ////            //document.getElementById(editor).value;
    ////            //editor.SetText(a[0]);
    ////            //editorEmer.SetText(a[1]);

    ////            if (key == gvFleteKontabelTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
    ////                indexCounter = indexCounter + 1;
    ////                merrTeDhena();
    ////                gvFleteKontabelTrupi.PerformCallback('shto,' + 'NrLlogari');
    ////            }
}
function GotFocusProdukti(editor, key) {
    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
//kontrollon nese jemi ne rreshtin e fundit

function TextChangedLloji(editor, key) {
    indeksi = indexCounter;
    enable();
    var nrProduktClientID = getClientID(key);
    editorProdukti = $("input[id$=" + nrProduktClientID + "]")[0]

    editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + key);
    editorFunksioni = Utils.ktheKontroll('cmbFunksioni' + key);
    editorVlera = Utils.ktheKontroll('txtVlera' + key);
    editorRenditja = Utils.ktheKontroll('cmbRenditja' + key);
    editorProdukti.value = '';
    editorPershkrimi.SetText('');
    editorFunksioni.SetText('');
    editorVlera.SetText('0');
    editorRenditja.SetText('');
    var auComplete = $find('behaviortxtProdukti' + key);
    if (editor.GetValue() == 1) {
        auComplete.set_contextKey('Artikull');
    }
    else {
        if (editor.GetValue() == 2) {
            auComplete.set_contextKey('Makro');
        }
    }

    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
function TextChangedPershkrimi(editor, key) {
    indeksi = indexCounter;

    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
function TextChangedFunksioni(editor, key) {
    indeksi = indexCounter;

    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
function TextChangedVlera(editor, key) {
    indeksi = indexCounter;
    if (isNaN(editor.GetText())) {
        alert('Vlera duhet te jete numer');
        editor.SetText(0);
    }
    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
function TextChangedRenditja(editor, key) {
    indeksi = indexCounter;

    if (key == gvTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvTrupi.PerformCallback();
    }
}
//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    merrTeDhena();
    gvTrupi.PerformCallback(key);
}



//metodat per marrjen e te dhenave nga grida dhe ruajtja e tyre ne nje hidden field
function ProcessTextCahnged(fieldName, value) {
    editorValues[fieldName] = value;
    var hf = document.getElementById("HiddenField1");
    hf.value = editorValues["KodiKokaMakro"] + ";" + editorValues["PershkrimiKokaMakro"] + ";"
  + editorValues["IdNivelAutorizimi"] + ";";
}


var editorAutorizime;

function KeyPresAutorizime(kodi, editor, key) {//kur shtypet nje key per kolonen Autorizimeve
    if (kodi == 13) {
        indeksi = key;
        editorAutorizime = Utils.ktheKontroll('IdNivelAutorizimi');

        Autorizime_Click();
        var hf = document.getElementById("hfAutorizime");
        hf.value = editorAutorizime.GetText();
        editorValues["IdNivelAutorizimi"] = editorAutorizime.GetText();
        var hf1 = document.getElementById("HiddenField1");
        hf1.value = editorValues["KodiKokaMakro"] + ";" + editorValues["PershkrimiKokaMakro"] + ";"
   + editorValues["IdNivelAutorizimi"] + ";";

    }
}
function LostFocusAutorizime(key) {//kur humb fokusin kolona Autorizimeve
    indeksi = key;
    editorAutorizime = Utils.ktheKontroll('IdNivelAutorizimi');
    var hf = document.getElementById("hfAutorizime");
    hf.value = editorAutorizime.GetText();
    editorValues["IdNivelAutorizimi"] = editorAutorizime.GetText();
    var hf1 = document.getElementById("HiddenField1");
    hf1.value = editorValues["KodiKokaMakro"] + ";" + editorValues["PershkrimiKokaMakro"] + ";"
   + editorValues["IdNivelAutorizimi"] + ";";
}

function ButtonClickedAutorizime(editor, key) {//kur klikon butonin e kolones Autorizimeve
    indeksi = key;
    editorAutorizime = editor;

    Autorizime_Click();


}
function TextChangedAutorizime(key) {//kur ndryshon texti tek kolona Autorizimeve
    indeksi = key;
    editorAutorizime = Utils.ktheKontroll('IdNivelAutorizimi');
    var hf = document.getElementById("hfAutorizime");
    hf.value = editorAutorizime.GetText();
    editorValues["IdNivelAutorizimi"] = editorAutorizime.GetText();
    var hf1 = document.getElementById("HiddenField1");
    hf1.value = editorValues["KodiKokaMakro"] + ";" + editorValues["PershkrimiKokaMakro"] + ";"
   + editorValues["IdNivelAutorizimi"] + ";";

}


function valido(s, e) {
    var activeTabIndex = PageControl.GetActiveTab().index;
    var tabPageCount = PageControl.GetTabCount();

    for (var i = 1; i < tabPageCount; i++) {
        PageControl.SetActiveTab(PageControl.GetTab(i));
        isvalid = ASPxClientEdit.ValidateGroup("entries");
        if (isvalid == false) {
            e.processOnServer = false;
            PageControl.SetActiveTab(PageControl.GetTab(i));
            break;
        }
        else
            PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex));
    }
}
