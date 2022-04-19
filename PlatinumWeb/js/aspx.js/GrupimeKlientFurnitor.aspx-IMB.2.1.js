; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvKodifikimKlientFurnitor";
var identifikuesPerPopupKodifikimin = "Kodifikim";
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
    $('#hfRuaj').val( 'Filtra');
    if (grida == "gvKodifikimKlientFurnitor")
        myMenu.aplikoFiltra(s, e, gvKodifikimKlientFurnitor, "", '');
    else if (grida == "gvKodifikimKlientFurnitorGrupim2")
        myMenu.aplikoFiltra(s, e, gvKodifikimKlientFurnitorGrupim2, "", '');
    else if (grida == "gvKodifikimKlientFurnitorGrupim3")
        myMenu.aplikoFiltra(s, e, gvKodifikimKlientFurnitorGrupim3, "", '');

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
    grida = 'gvKodifikimKlientFurnitorGrupim2';
}
function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvKodifikimKlientFurnitor';
}
function RowDblClickGrida3(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvKodifikimKlientFurnitorGrupim3';
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKodifikimArtikulli, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKodifikimArtikulli, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKodifikimArtikulli, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKodifikimArtikulli, indexSel);
}


function GrupiKF_Click() {
    var llojikf = Utils.getUrlVar("kf");
    if (grida == "gvKodifikimKlientFurnitor")
        myButtonClickLupa.LupaUniversal_Click('Zgjidh grupin e klient/furnitorit', 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + 1 + '&kf=' + llojikf, 500, 500);
    else if (grida == "gvKodifikimKlientFurnitorGrupim2")
        myButtonClickLupa.LupaUniversal_Click('Zgjidh grupin e klient/furnitorit', 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + 2 + '&kf=' + llojikf, 500, 500);
    else if (grida == "gvKodifikimKlientFurnitorGrupim3")
        myButtonClickLupa.LupaUniversal_Click('Zgjidh grupin e klient/furnitorit', 'LupaGrupimeKlientFurnitor.aspx?llojKodifikimi=' + 3 + '&kf=' + llojikf, 500, 500);
}

function InitPrind() {
    var hf = $('#hfPrindi');
    var hf1 = $('#hfNiveli');
    editorPrind = Utils.ktheKontroll('IdPrindi');
    if (!editorPrind) return;
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    //if ($('#hfRuaj')[0].value != 'Modifiko')
    //hf.value = editorPrind.GetText();

    editorNiveli = Utils.ktheKontroll('NivelGrupi');
    if (!editorNiveli) return;
    if ($('#hfRuaj').val() != 'Modifiko')
        if (hf1.value != "")
            editorNiveli.SetValue(hf1.val());
    enable();
    if (editorPrind.GetText() != "") {
        if (grida == "gvKodifikimKlientFurnitor")
            gvKodifikimKlientFurnitor.GetRowValues(gvKodifikimKlientFurnitor.GetFocusedRowIndex(), 'IdGrupi;KodGrupi', ktheKodPrind);
        else if (grida == "gvKodifikimKlientFurnitorGrupim2")
            gvKodifikimKlientFurnitorGrupim2.GetRowValues(gvKodifikimKlientFurnitorGrupim2.GetFocusedRowIndex(), 'IdGrupi;KodGrupi', ktheKodPrind);
        else if (grida == "gvKodifikimKlientFurnitorGrupim3")
            gvKodifikimKlientFurnitorGrupim3.GetRowValues(gvKodifikimKlientFurnitorGrupim3.GetFocusedRowIndex(), 'IdGrupi;KodGrupi', ktheKodPrind);
    }
}

function ktheKodPrind(result) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKodPrindiGrupKF"),
        data: JSON.stringify({ idja: result[0] })
    }).done(mbushHfPrindi);
}

function mbushHfPrindi(result) {
    var hf = $('#hfPrindi');
    hf.val ( result);
}

function ndryshimTabi(tab) {

    if (tab.GetText() == "Grupimi 1") {
        grida = "gvKodifikimKlientFurnitor";
        if (gvKodifikimKlientFurnitorGrupim2.IsEditing() == true)
            gvKodifikimKlientFurnitorGrupim2.CancelEdit();
        else if (gvKodifikimKlientFurnitorGrupim3.IsEditing() == true)
            gvKodifikimKlientFurnitorGrupim3.CancelEdit();
    }
    else if (tab.GetText() == "Grupimi 2") {
        grida = "gvKodifikimKlientFurnitorGrupim2";
        if (gvKodifikimKlientFurnitor.IsEditing() == true)
            gvKodifikimKlientFurnitor.CancelEdit();
        else if (gvKodifikimKlientFurnitorGrupim3.IsEditing() == true)
            gvKodifikimKlientFurnitorGrupim3.CancelEdit();
    }
    else if (tab.GetText() == "Grupimi 3") {
        grida = "gvKodifikimKlientFurnitorGrupim3";
        if (gvKodifikimKlientFurnitor.IsEditing() == true)
            gvKodifikimKlientFurnitor.CancelEdit();
        else if (gvKodifikimKlientFurnitorGrupim2.IsEditing() == true)
            gvKodifikimKlientFurnitorGrupim2.CancelEdit();
    }
}

function pastro() {
    var hf = $("#hfPrindi");
    hf.val( "");
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

function callWebservice() {
    var emer = 'GrupimeKlientFurnitor.aspx?kf=' + Utils.getUrlVar("kf");
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko, grida);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}

function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    pastro();
    if (grida == "gvKodifikimKlientFurnitor")
        gvKodifikimKlientFurnitor.StartEditRow(index);
    else
        if (grida == "gvKodifikimKlientFurnitorGrupim2")
            gvKodifikimKlientFurnitorGrupim2.StartEditRow(index);
        else
            if (grida == "gvKodifikimKlientFurnitorGrupim3")
                gvKodifikimKlientFurnitorGrupim3.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName();
    editmode = false;
    indexEdit = -1;
}
function changeName() {
    myFaqeCelje.changeNameRegjistrime('GrupimeKlientFurnitor.aspx?kf=' + Utils.getUrlVar("kf"),0);
}


var editorPrind;
var editorNiveli;
var editorKodi;
var editorGrupi;

function KeyPresPrind(kodi, editor, key) {//kur shtypet nje key per kolonen Autorizimeve
    if (kodi == 13) {
        indeksi = key;
        editorPrind = Utils.ktheKontroll('IdPrindi');
        editorNiveli = Utils.ktheKontroll('NivelGrupi');
        if (!editorNiveli || !editorPrind) return;
        KodifikimArtikulli_Click();
        var hf1 = $("#hfNiveli");
        hf1.val( editorNiveli.GetText());
    }
}

function LostFocusPrind(key) {//kur humb fokusin kolona Autorizimeve
    indeksi = key;
    editorPrind = Utils.ktheKontroll('IdPrindi');
    editorNiveli = Utils.ktheKontroll('NivelGrupi');
    if (!editorNiveli || !editorPrind) return;
    var hf1 = $("#hfNiveli");
    hf1.val( editorNiveli.GetText());
}

function enable() {//kur humb fokusin kolona Autorizimeve      
    editorPrind = Utils.ktheKontroll('IdPrindi');
    if (!editorPrind) return;
    if ($("#hfRuaj").val() == "Modifiko") {
        editorPrind.SetEnabled(true);
        editorKodi = Utils.ktheKontroll('KodGrupi');
        if (!editorKodi) return;
        editorKodi.SetEnabled(false);
    }
    else {
        editorPrind.SetEnabled(true);
        editorKodi = Utils.ktheKontroll('KodGrupi');
        if (!editorKodi) return;
        editorKodi.SetEnabled(true);
    }
}

function ButtonClickedPrind(editor, key) {//kur klikon butonin e kolones Autorizimeve
    indeksi = key;
    editorPrind = Utils.ktheKontroll('IdPrindi');
    editorNiveli = Utils.ktheKontroll('NivelGrupi');
    if (!editorNiveli || !editorPrind) return;
    GrupiKF_Click();
    var hf1 = $("#hfNiveli");
    hf1.val( editorNiveli.GetText());
}

function TextChangedPrind(key) {//kur ndryshon texti tek kolona Autorizimeve
    indeksi = key;
    editorPrind = Utils.ktheKontroll('IdPrindi');
    editorNiveli = Utils.ktheKontroll('NivelGrupi');
    if (!editorNiveli || !editorPrind) return;
    var a = new Array();
    a = editorPrind.GetText().toString().split(',');
    editorPrind.SetText(a[1]);
    if (a[2] != undefined)
        editorNiveli.SetText(parseInt(a[2]) + 1);
    var hf = $("#hfPrindi");
    hf.val( a[0]);
    var hf1 = $("#hfNiveli");
    hf1.val( editorNiveli.GetText());
}

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (grida == "gvKodifikimKlientFurnitor")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKodifikimKlientFurnitor, hfTeDrejta);
    else if (grida == "gvKodifikimKlientFurnitorGrupim2")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKodifikimKlientFurnitorGrupim2, hfTeDrejta);
    else if (grida == "gvKodifikimKlientFurnitorGrupim3")
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKodifikimKlientFurnitorGrupim3, hfTeDrejta);

    if (e.item.name == 'Shto') {
        var hf1 = $("#hfNiveli");
        hf1.val( 1);
        pastro();
    }

    //    if (e.item.name == 'Ruaj' && grida == "gvKodifikimKlientFurnitor") {
    //        gvKodifikimKlientFurnitor.UpdateEdit();
    //        e.processOnServer = false;
    //        //gvKodifikimArtikulli.Refresh();
    //        //  btn.DoClick();
    //    }

    //    if (e.item.name == 'Ruaj' && grida == "gvKodifikimKlientFurnitorGrupim2") {
    //        gvKodifikimKlientFurnitorGrupim2.UpdateEdit();
    //        e.processOnServer = false;
    //    }

    //    if (e.item.name == 'Ruaj' && grida == "gvKodifikimKlientFurnitorGrupim3") {
    //        gvKodifikimKlientFurnitorGrupim3.UpdateEdit();
    //        e.processOnServer = false;
    //    }

    if (e.item.name == 'Modifiko') {
        //var hf1 = $("#hfNiveli")[0];
        //hf1.value = '';
        pastro();
    }
}

function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val()== 'Modifiko' || $('#hfRuaj').val() == 'Ruaj') {
        $.ajax({          
            url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
            data: JSON.stringify({})
        }).done(SucceededCallbackMesazhi);
    }
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else
        if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}