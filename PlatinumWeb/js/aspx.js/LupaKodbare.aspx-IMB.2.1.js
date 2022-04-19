function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaKodbar.SetFocusedRowIndex(0);
        Utils.ktheKontroll('txtPershkrimi1').SetFocus(true);
        Utils.ktheKontroll('txtPershkrimi0').SetFocus(true);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

$(document).keydown(function (e) {
    if (e.which == 13) {
        e.preventDefault();
        if (indeksi == gvLupaKodbar.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
            indexCounter = indexCounter + 1;
            gvLupaKodbar.PerformCallback();
        }
        else Utils.ktheKontroll('txtPershkrimi' + parseFloat(parseFloat(indeksi) + parseFloat(1))).SetFocus(true);
        //                e.returnValue = false;
        //                e.cancel = true;
    }
});
var indeksi;
function SetKey(key) {
    indeksi = key;
}

function TextChangedPershkrimi(editor, key) {
    indeksi = key;
    if (editor.GetText().indexOf(',') != -1) {
        myMesazh.ShtoMesazhGabimi('Kodbari nuk mund te permbaje presje!');        
        return;
    }
    else if (editor.GetText().indexOf(' ') != -1) {
        myMesazh.ShtoMesazhGabimi('Kodbari nuk mund te permbaje Hapesira!');
        return;
    }
//    if (key == 0) {
//        window.parent.btneKodbari.SetText(editor.GetText());
//    }
    if (key == gvLupaKodbar.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        gvLupaKodbar.PerformCallback();
    }
    merrTeDhenaNew();
}
function TextChangedNjesia(editor, key) {

    if (key == gvLupaKodbar.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        gvLupaKodbar.PerformCallback();
    }
    merrTeDhenaNew();

}

var indexCounter;
//therret callbackun per te fshire nje rresht
function FshiClicked(key) {
    indeksi = key - 1;
    gvLupaKodbar.PerformCallback(key);
    merrTeDhenaNew();
}

var kodbaretString;
var editorEmer;


var KodbarMePluse;
function merrTeDhenaNew() {//merren te dhenat qe ka grida dhe ruhen tek hidden filedet e kodbareve per lupen dhe per lupen e ambjentit te celjes se artikullit
    var kodbaret = new Array();
    kodbaretString = '';
    var neRregull;
    for (i = 0; i < gvLupaKodbar.cpNoRows; i++) {
        editorEmer = Utils.ktheKontroll('txtPershkrimi' + i);
        editorNjesia = Utils.ktheKontroll('cmbNjesia' + i);
        editorDetajimi1 = Utils.ktheKontroll('cmbDetajimi1t' + i);
        editorDetajimi2 = Utils.ktheKontroll('cmbDetajimi2t' + i);
        neRregull = true;
        KodbarMePluse = true;
        if (editorEmer.GetText().indexOf(',') != -1) {
            myMesazh.ShtoMesazhGabimi('Kodbari nuk mund te permbaje presje!');
            editorEmer.SetText('');
            neRregull = false;
            break;
        }
        if (Utils.ktheKontroll('txtPershkrimi' + i).GetText().indexOf('+') != -1) {
            KodbarMePluse = false;
            break;
        }
        if (editorEmer.GetText().indexOf(' ') != -1) {
            editorEmer.SetText('');
            neRregull = false;
            break;
        }
        if (editorEmer.GetText() == '' || editorEmer.GetText() == ' ' || editorEmer.GetText() == null)
            continue;
        //kodbaret[i] = { indeksi: i, pershkrimi: editorEmer.GetText(), njesia: editorNjesia.GetValue(), detajimi1: editorDetajimi1.GetText(), detajimi2: editorDetajimi2.GetText() };
        kodbaret[i] = { indeksi: i, pershkrimi: editorEmer.GetText(), njesia: editorNjesia.GetValue(), detajimi1: editorDetajimi1.GetValue() == null ? "0" : editorDetajimi1.GetValue(), detajimi2: editorDetajimi2.GetValue() == null ? "0" : editorDetajimi2.GetValue() };
        if (i != gvLupaKodbar.cpNoRows - 1)
            kodbaretString += editorEmer.GetText() + ',';
        else kodbaretString += editorEmer.GetText();
        
    }    
    $('#hfKodbare').val(JSON.stringify(kodbaret)); //i ruajme tek hidden fieldi i kodbareve te vet lupes.
    return neRregull;
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "OK":
            var neRRegull = merrTeDhenaNew();
            if (!KodbarMePluse) {
                myMesazh.ShtoMesazhGabimi('Kodbari nuk mund te permbaje +!');
                e.processOnServer = false;
                return;
            }
            if (!neRRegull) {
                e.processOnServer = false;
                return;
            }
            window.parent.btneKodbari.SetFocus(true);
            window.parent.btneKodbari.SetText(kodbaretString);
            var kodbaret = $('#hfKodbare').val();
            window.parent.$('#hfKodbaret').val(kodbaret); //i ruajme tek hidden fieldi i kodbareve te ambjentit te celjes se artikullit (i shto_artikull)
            window.parent.popupUniversal.Hide();

            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKodbar&page=LupaKodbare.aspx&idKonfigAmbjente=577';
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


var editorGlobal;
function ButtonClickDet1(editor,key)
{
    editorGlobal = Utils.ktheKontroll(editor);
    var vleraKodit = window.parent.$('#hfId').val();
    var queryString = 'mag=' + "" + '&vjenNga=kodbar&lloji=' + 1 + '&idKonfigAmbjente=' + 0;
    if (Utils.getUrlVar('klonim') == "true")
        queryString += "&klonim=true";
    else
        queryString += '&idArtikulli=' + vleraKodit;
    popupUniversal.SetHeaderText("Zgjidhni detajimin");
    popupUniversal.SetContentUrl('LupaDetajimArtikulliRegjistrim.aspx?' + queryString);
    popupUniversal.SetSize(1000, 700);
    popupUniversal.Show();
   
}
var identifikuesPerPopupDetajime = "Kodbar";
function ButtonClickDet2(editor, key)
{
    editorGlobal = Utils.ktheKontroll(editor);
    var vleraKodit = window.parent.$('#hfId').val();
    var queryString = 'mag=' + "" + '&vjenNga=kodbar&lloji=' + 2 + '&idKonfigAmbjente=' + 0;
    if (Utils.getUrlVar('klonim') == "true")
        queryString += "&klonim=true";
    else
        queryString += '&idArtikulli=' + vleraKodit;
    popupUniversal.SetHeaderText("Zgjidhni detajimin");
    popupUniversal.SetContentUrl('LupaDetajimArtikulliRegjistrim.aspx?' + queryString);
    popupUniversal.SetSize(1000, 700);
    popupUniversal.Show();

}