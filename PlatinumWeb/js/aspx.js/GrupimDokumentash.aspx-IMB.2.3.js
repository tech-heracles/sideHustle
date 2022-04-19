; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvGrupet";
var identifikuesPerPopupAutorizimet = 'GrupimDokumentash';

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
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
                break;
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
    });

});

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val('Filtra');
    if (grida == "gvGrupet")
        myMenu.aplikoFiltra(s, e, gvGrupet, "", '');
    else if (grida == "gvGrupet2")
        myMenu.aplikoFiltra(s, e, gvGrupet2, "", '');
    else if (grida == "gvGrupet3")
        myMenu.aplikoFiltra(s, e, gvGrupet3, "", '');

}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function RowDblClickGrida2(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvGrupet2';
}
function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvGrupet';
}
function RowDblClickGrida3(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvGrupet3';
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvGrupet, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvGrupet, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvGrupet, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvGrupet, indexSel);
}





function ndryshimTabi(tab) {
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    if (tab.GetText() == "Grupimi 1") {
        grida = "gvGrupet";
        if (gvGrupet2.IsEditing() == true)
            gvGrupet2.CancelEdit();
        else if (gvGrupet3.IsEditing() == true)
            gvGrupet3.CancelEdit();
    }
    else if (tab.GetText() == "Grupimi 2") {
        grida = "gvGrupet2";
        if (gvGrupet.IsEditing() == true)
            gvGrupet.CancelEdit();
        else if (gvGrupet3.IsEditing() == true)
            gvGrupet3.CancelEdit();
    }
    else if (tab.GetText() == "Grupimi 3") {
        grida = "gvGrupet3";
        if (gvGrupet.IsEditing() == true)
            gvGrupet.CancelEdit();
        else if (gvGrupet2.IsEditing() == true)
            gvGrupet2.CancelEdit();
    }
}

function pastro() {


    ////unselect all - tocheck doesn't work
    //var items = new Array();
    //try {
    //    for (var i = 0; i < arrKategoria.length - 1; i++)
    //        lbxKategoria.FindItemByText(arrKategoria[i]).selected= false;

    //} catch (ee) {
    //}
    if (typeof (cmbAutorizimi) !== "undefined" && cmbAutorizimi.GetListBoxControl() != null && cmbAutorizimi.GetText())
        cmbAutorizimi.SetText('');
    arrKategoria = new Array();
    arrLloji = new Array();
    //lbxKategoria.selectedIndex = -1
    //lbxKategoria.ClearItems();
    //lbxLloji.ClearItems();
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function callWebservice() {
    var emer = 'GrupimDokumentash.aspx';
    $.ajax({        
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true")
        switchEditMode(indexModifiko, grida);
    else
        myMesazh.ShtoMesazhGabimi(hfTeDrejta.Get("msgNukKeniDrejtaPerVeprim"));
}
var arrKategoria = new Array();
var arrLloji = new Array();
var mod = false;
function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    pastro();
    indexEdit = index;
    arrKategoria = new Array();
    arrLloji = new Array();
    mod = true;
    if (grida == "gvGrupet") {
        gvGrupet.StartEditRow(index);
        gvGrupet.GetRowValues(index, "Kategoria;Lloji;Autorizimet", OnCallback);
    }
    else
        if (grida == "gvGrupet2") {
            gvGrupet2.StartEditRow(index);
            gvGrupet2.GetRowValues(index, "Kategoria;Lloji;Autorizimet", OnCallback);
        }
        else
            if (grida == "gvGrupet3") {
                gvGrupet3.StartEditRow(index);
                gvGrupet3.GetRowValues(index, "Kategoria;Lloji;Autorizimet", OnCallback);
            }
}
function OnCallback(result) {
    if ($("#hfRuaj").val() == "Klonim") {
        if (cmbAutorizimi.GetInputElement()!=null)
        cmbAutorizimi.SetText(result.Autorizimet);
        if (grida == "gvGrupet") {
            //gvGrupet = ASPxClientGridView.Cast(gvGrupet);
            gvGrupet.SetEditValue("Kodi", result.Kodi);
            gvGrupet.SetEditValue("Pershkrimi", result.Pershkrimi);
        }
        if (grida == "gvGrupet2") {
            gvGrupet2.SetEditValue("Kodi", result.Kodi);
            gvGrupet2.SetEditValue("Pershkrimi", result.Pershkrimi);
        }
        if (grida == "gvGrupet3") {

            gvGrupet3.SetEditValue("Kodi", result.Kodi);
            gvGrupet3.SetEditValue("Pershkrimi", result.Pershkrimi);
        }
        arrKategoria = result.Kategoria.split(';');
        arrLloji = result.Lloji.split(';');
    }
    else {
        arrKategoria = result[0].split(';');
        arrLloji = result[1].split(';');
    }
    var items = new Array();
    try {
        for (var i = 0; i < arrKategoria.length - 1; i++)
            items.push(lbxKategoria.FindItemByText(arrKategoria[i]));
        lbxKategoria.SelectItems(items);
        merrLlojeMod(lbxKategoria);
    } catch (ee) {
    }

}
function Init() {
    changeName();
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
function changeName() {
    myFaqeCelje.changeNameRegjistrime('GrupimDokumentash.aspx', 0);
}


var editorPrind;
var editorNiveli;
var editorKodi;
var editorGrupi;


function enable() {//kur humb fokusin kolona Autorizimeve
    editorKodi = Utils.ktheKontroll('Kodi');
    if (!editorKodi) return;
    editorKodi.SetEnabled($("#hfRuaj").val() == "Modifiko" ? false : true);
}



function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (grida == "gvGrupet")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvGrupet, hfTeDrejta);
    else if (grida == "gvGrupet2")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvGrupet2, hfTeDrejta);
    else if (grida == "gvGrupet3")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvGrupet3, hfTeDrejta);

    if (e.item.name == 'Shto') {
        $('#hfRuaj').val('Shto');
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        pastro();
    }
    if (e.item.name == 'Modifiko') {
        pastro();
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        mod = true;
        if (grida == "gvGrupet")
            gvGrupet.GetRowValues(indexModifiko, "Kategoria;Lloji;Autorizimet", OnCallback);
        else if (grida == "gvGrupet2")
            gvGrupet2.GetRowValues(indexModifiko, "Kategoria;Lloji;Autorizimet", OnCallback);
        else if (grida == "gvGrupet3")
            gvGrupet3.GetRowValues(indexModifiko, "Kategoria;Lloji;Autorizimet", OnCallback);
    }
    if (e.item.name == 'Klono') {
        $('#hfRuaj').val('Klonim');
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        pastro();

        var keyValue = undefined;
        if (grida == "gvGrupet") {
            if (gvGrupet.cpRowCount != 0) {
                keyValue = gvGrupet.GetRowKey(gvGrupet.GetFocusedRowIndex());
                gvGrupet.PerformCallback("Klonim;" + keyValue);
            }
            else
             {
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
                return;
            }
        }
        else if (grida == "gvGrupet2") {
            if (gvGrupet2.cpRowCount != 0) {
                keyValue = gvGrupet2.GetRowKey(gvGrupet2.GetFocusedRowIndex());
                gvGrupet2.PerformCallback("Klonim;" + keyValue);
            }
            else
             {
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
                return;
            }
        }
        else if (grida == "gvGrupet3") {
            if (gvGrupet3.cpRowCount != 0) {
                keyValue = gvGrupet3.GetRowKey(gvGrupet3.GetFocusedRowIndex());
                gvGrupet3.PerformCallback("Klonim;" + keyValue);
            }
            else
            {
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
                return;
            }
        }

        e.processOnServer = false; 
    }
}

function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko' || $('#hfRuaj').val() == 'Ruaj') {
        $.ajax({           
            url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
            data: JSON.stringify({})
        }).done(SucceededCallbackMesazhi);
    }
    else if ($('#hfRuaj').val() == 'Klonim') {
        if (grida == "gvGrupet")
            OnCallback(JSON.parse(gvGrupet.cpKategoria));
        if (grida == "gvGrupet2")
            OnCallback(JSON.parse(gvGrupet2.cpKategoria));
        if (grida == "gvGrupet3")
            OnCallback(JSON.parse(gvGrupet3.cpKategoria));
    }

}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green") {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    }
    else
        if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
}

function merrLloje(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrLlojeNgaKategoria"),
        data: JSON.stringify({ kat: s.GetSelectedValues() })
    }).done(SucceededCallbackLloje);
}
function merrLlojeMod(s) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrLlojeNgaKategoria"),
        data: JSON.stringify({ kat: s.GetSelectedValues() })
    }).done(SucceededCallbackLlojeMod);
}
function SucceededCallbackLloje(result) {
    var items = lbxLloji.GetSelectedValues();
    lbxLloji.ClearItems();
    for (i = 0; i < result.length; i++) {
        if (lbxLloji.FindItemByText(result[i].KodKonfigAmbjente) == null)
            lbxLloji.AddItem(result[i].KodKonfigAmbjente, result[i].IdKonfigAmbjente);
    }
        lbxLloji.SelectValues(items);

}
function SucceededCallbackLlojeMod(result) {
    if (result.length > 0 && lbxLloji.GetInputElement() != null)
        lbxLloji.ClearItems();
    for (i = 0; i < result.length; i++) {
        if (lbxLloji.FindItemByText(result[i].KodKonfigAmbjente) == null)
            lbxLloji.AddItem(result[i].KodKonfigAmbjente, result[i].IdKonfigAmbjente);
    }
    try {
        var items = new Array();
        for (var i = 0; i < arrLloji.length - 1; i++)
            if(lbxLloji.FindItemByText(arrLloji[i])!=null)
                items.push(lbxLloji.FindItemByText(arrLloji[i]));
        lbxLloji.SelectItems(items);

    }
    catch (ee) {
    }


}

function InitKat(s, e) {
    enable();
    var items = new Array();
    if ($('#hfRuaj').val() == 'Klonim')
        arrKategoria.length = 0;
        try {
            for (var i = 0; i < arrKategoria.length - 1; i++)
                items.push(lbxKategoria.FindItemByText(arrKategoria[i]));
            if ($("#hfRuaj").val() == "Shto")
                lbxKategoria.UnselectAll();
            else
                lbxKategoria.SelectItems(items);
            
            if (mod) {
                merrLlojeMod(s);
                mod = false;
            }
            else
                merrLloje(s);
        }
        catch (ee) {
        }
    
}
function Autorizime_Click() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidh autorizimet', 'LupaAutorizim.aspx?autorizimet=' + cmbAutorizimi.GetText(), 500, 600);
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}