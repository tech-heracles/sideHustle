; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var btnFiltrat;
var pageState = {
    emerKomponente: "DetajimeArtikulli.aspx"
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

function Autorizime_Click() {
    //popupUniversal.SetHeaderText('Zgjidh autorizimet');
    //document.getElementById('').src = 'LupaAutorizime.aspx';
    //popupUniversal.Show();
}



function Filtra_Click() {
    //document.getElementById('').src = 'LupaFiltra.aspx?grida=gvDetajimArtikulli&page=DetajimeArtikulli.aspx';
    //popFiltra.Show();
}


function pastro() {
    var hf = document.getElementById("hfAutorizime");
    hf.value = "";
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

function RowDblClickGrida(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
}

function callWebservice() {   
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: pageState.emerKomponente, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true") {
        indexModifiko = gvDetajimArtikulli.GetFocusedRowIndex();
        gvDetajimArtikulli.GetRowValues(indexModifiko, 'IdDetajimArtikulli;KodDetajimArtikulli;LlojDetajimArtikulli;PershkrimDetajimArtikulli;KategoriDetajimi;IdNdermarje', eshteDetajimLidhur);
    }
    else {
        alert("Nuk keni të drejta për të kryer këtë veprim!");
    }
}

function eshteDetajimLidhur(result) {
    //LlojDetajimArtikulli.SetValue(result[2]);
    //KategoriDetajimi.SetValue(result[4]);    
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteDetajimLidhur"),
        data: JSON.stringify({ kodi: result[1], idNdermarrje: result[5] })
    }).done(vazhdoModifikim);
}

function vazhdoModifikim(result) {
    if (result !== "true") {
        switchEditMode(indexModifiko);
    }
    else {
        myMesazh.ShtoMesazhGabimi('Ky detajim është i lidhur dhe nuk mund të modifikohet!');
    }
}

function OnGetRowValues(values) {
    if (values[0] == "3")//date skadence
    {
        LlojDetajimArtikulli.ClearItems();
        LlojDetajimArtikulli.AddItem("Date", 3);
        LlojDetajimArtikulli.SetEnabled(false);
        LlojDetajimArtikulli.SetSelectedIndex(0);
        PershkrimDetajimArtikulli.SetEnabled(false);
    }
    else if (values[0] == "2")//Serial
    {
        LlojDetajimArtikulli.ClearItems();
        LlojDetajimArtikulli.AddItem("Alfanumerik", 1);
        LlojDetajimArtikulli.AddItem("Numerik", 2);
        LlojDetajimArtikulli.SetEnabled(true);
        LlojDetajimArtikulli.SetSelectedIndex(0);
        PershkrimDetajimArtikulli.SetEnabled(true);
    }
    else if (values[0] == "1")//Detajim
    {
        LlojDetajimArtikulli.ClearItems();
        LlojDetajimArtikulli.AddItem("Alfanumerik", 1);
        LlojDetajimArtikulli.AddItem("Numerik", 2);
        LlojDetajimArtikulli.AddItem("Date", 3);
        LlojDetajimArtikulli.SetEnabled(true);
        LlojDetajimArtikulli.SetSelectedIndex(0);
        PershkrimDetajimArtikulli.SetEnabled(true);
    }
    else if (values[0] == "4") { //seri
        LlojDetajimArtikulli.ClearItems();
        LlojDetajimArtikulli.AddItem("Alfanumerik", 1);
        LlojDetajimArtikulli.AddItem("Numerik", 2);
        LlojDetajimArtikulli.SetEnabled(false);
        LlojDetajimArtikulli.SetSelectedIndex(0);
        PershkrimDetajimArtikulli.SetEnabled(false);
    }

    if (values[1] == 3)
        LlojDetajimArtikulli.SetText('Date');
    else if (values[1] == 2)
        LlojDetajimArtikulli.SetText('Numerik');
    else if (values[1] == 1 || values[1] == 4)
        LlojDetajimArtikulli.SetText('Alfanumerik');
}

function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);    
    gvDetajimArtikulli.StartEditRow(index);
    indexEdit = index;
    disableKategoria();
    TextChangedKategoria(index);
}

function disableKategoria() {
    if (Utils.getUrlVar('lupe')!='undefined' && Utils.getUrlVar('lupe')=='true' && Utils.getUrlVar('veprimi')) {
        var kategoriaDefault = Utils.getUrlVar('veprimi');
        var editorKategoriDetajimi = Utils.ktheKontroll('KategoriDetajimi');
        editorKategoriDetajimi.SetValue(kategoriaDefault);
        editorKategoriDetajimi.SetEnabled(false);
    }
}

function Init() {
    changeName();
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}

function changeName() {
    if (Utils.getUrlVar('lupe'))
        return;
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart(pageState.emerKomponente, 0);
    window.parent.createCookie('adresa', pageState.emerKomponente, 1);
}

var editorAutorizime;

function KeyPresAutorizime(kodi, editor, key) {//kur shtypet nje key per kolonen Autorizimeve
    if (kodi == 13) {
        indeksi = key;
        var editorAutorizime = Utils.ktheKontroll('IdNivelAutorizimi');
        if (!editorAutorizime) return;
       

        Autorizime_Click();
        var hf = document.getElementById("hfAutorizime");
        hf.value = editorAutorizime.GetText();
        //                editorValues["IdNivelAutorizimi"] = editorAutorizime.GetText();
        //                var hf1 = document.getElementById("HiddenField1");
        //                hf1.value = editorValues["KodiKokaMakro"] + ";" + editorValues["PershkrimiKokaMakro"] + ";"
        //            + editorValues["IdNivelAutorizimi"] + ";";

    }
}

function LostFocusAutorizime(key) {//kur humb fokusin kolona Autorizimeve
    indeksi = key;
    var editorAutorizime = Utils.ktheKontroll('IdNivelAutorizimi');
    if (!editorAutorizime) return;
    var hf = document.getElementById("hfAutorizime");
    hf.value = editorAutorizime.GetText();
    //            editorValues["IdNivelAutorizimi"] = editorAutorizime.GetText();
    //            var hf1 = document.getElementById("HiddenField1");
    //            hf1.value = editorValues["KodiKokaMakro"] + ";" + editorValues["PershkrimiKokaMakro"] + ";"
    //            + editorValues["IdNivelAutorizimi"] + ";";
}

function ButtonClickedAutorizime(editor, key) {//kur klikon butonin e kolones Autorizimeve
    indeksi = key;
    editorAutorizime = editor;
    Autorizime_Click();
}

function TextChangedAutorizime(key) {//kur ndryshon texti tek kolona Autorizimeve
    indeksi = key;
    var editorAutorizime = Utils.ktheKontroll('IdNivelAutorizimi');
    if (!editorAutorizime) return;
    var hf = document.getElementById("hfAutorizime");
    hf.value = editorAutorizime.GetText();
}

function TextChangedKategoria(indexModifiko) {
    disableKategoria();
    var editorKategoriDetajimi = Utils.ktheKontroll('KategoriDetajimi');
    if (!editorKategoriDetajimi)
        return;

    if (!(($("#hfRuaj").val() == 'Modifiko' && indexModifiko != undefined && indexModifiko != -1) || $("#hfRuaj").val() == 'Ruaj'))
        return;
        
    switch (editorKategoriDetajimi.GetValue()) {
        case 1:
            LlojDetajimArtikulli.ClearItems();
            LlojDetajimArtikulli.AddItem("Alfanumerik", 1);
            LlojDetajimArtikulli.AddItem("Numerik", 2);
            LlojDetajimArtikulli.AddItem("Date", 3);
            LlojDetajimArtikulli.SetSelectedIndex(0);
            LlojDetajimArtikulli.SetEnabled(true);
            PershkrimDetajimArtikulli.SetEnabled(true);
            if ($("#hfRuaj").val() == 'Modifiko' && (indexModifiko != undefined || indexModifiko != -1))
                KodDetajimArtikulli.SetEnabled(false);
            break;
        case 2:
            var indexiSelektuar = LlojDetajimArtikulli.GetSelectedIndex();
            LlojDetajimArtikulli.ClearItems();
            LlojDetajimArtikulli.AddItem("Alfanumerik", 1);
            LlojDetajimArtikulli.AddItem("Numerik", 2);
            LlojDetajimArtikulli.SetEnabled(true);
            PershkrimDetajimArtikulli.SetEnabled(true);
            if ($("#hfRuaj").val() == 'Modifiko' && (indexModifiko != undefined || indexModifiko != -1)) {
                KodDetajimArtikulli.SetEnabled(false);
                LlojDetajimArtikulli.SetSelectedIndex(indexiSelektuar);
            }
            break;
        case 3:
            LlojDetajimArtikulli.ClearItems();
            LlojDetajimArtikulli.AddItem("Date", 3);
            LlojDetajimArtikulli.SetEnabled(false);
            LlojDetajimArtikulli.SetSelectedIndex(0);
            PershkrimDetajimArtikulli.SetEnabled(false);
            PershkrimDetajimArtikulli.SetText(' ');
            if ($("#hfRuaj").val() == 'Modifiko' && indexModifiko != undefined && indexModifiko != -1)
                KodDetajimArtikulli.SetEnabled(false);
            break;
        case 4:
            LlojDetajimArtikulli.ClearItems();
            LlojDetajimArtikulli.AddItem("Alfanumerik", 1);
            LlojDetajimArtikulli.AddItem("Numerik", 2);
            LlojDetajimArtikulli.SetEnabled(true);
            LlojDetajimArtikulli.SetSelectedIndex(0);
            PershkrimDetajimArtikulli.SetEnabled(false);
            PershkrimDetajimArtikulli.SetText(' ');
            if ($("#hfRuaj").val() == 'Modifiko' && (indexModifiko != undefined || indexModifiko != -1))
                KodDetajimArtikulli.SetEnabled(false);
            break;
        default:
            break;
    }
}

//function ProcessTextChanged(fieldName, value) {
   
//    if (fieldName == "PershkrimDetajimArtikulli" || fieldName == "LlojDetajimArtikulli") {
//        if (LlojDetajimArtikulli.GetText() == "Date") {
//            if ((Date.parseLocale(PershkrimDetajimArtikulli.GetText(), "dd/MM/yyyy")) == null && PershkrimDetajimArtikulli.GetText() != '') {
//                myMesazh.ShtoMesazhGabimi("Jepni nje date te vlefshme");
//                PershkrimDetajimArtikulli.SetText(new Date().format("dd/MM/yyyy"));
//            }
//        }
//    }
//    else 
//    if (fieldName == "KategoriDetajimi")
//        TextChangedKategoria();
//}

function OnGridSelectionChanged() {
    gvDetajimArtikulli.GetSelectedFieldValues('KodDetajimArtikulli;KategoriDetajimi;PershkrimDetajimArtikulli', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var kodi = '';

    for (var i = 0; i < values.length; i++) {
        if (kodi == '')
            kodi = values[i][0];
        else
            kodi = kodi + ',' + values[i][0];
    }



    var pershkrimi = '';

    for (var i = 0; i < values.length; i++) {
        if (pershkrimi == '')
            pershkrimi = values[i][2];
        else
            pershkrimi = pershkrimi + ',' + values[i][2];
    }


    if (window.parent.identifikuesPerPopupDetajime == "RegjistrimDokumentash") {
        window.parent.editorDetajimi.value = kodi;
        window.parent.arr[3][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + kodi;
        window.parent.popupUniversal.Hide();
    }
    else if (window.parent.identifikuesPerPopupDetajime == "raportDetajime") {
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.Focus();
        window.parent.popupUniversal.Hide();
    }
    else if (window.parent.identifikuesPerPopupDetajime == "raportDetajimePershkrim") {
        window.parent.editorGlobal.SetText(pershkrimi);
        window.parent.editorGlobal.Focus();
        window.parent.popupUniversal.Hide();
    }
    else if (window.parent.identifikuesPerPopupDetajime == "Import") {
        window.parent.editordetajimi.SetText(kodi);
        window.parent.editordetajimi.Focus();
        window.parent.popupUniversal.Hide();
        //$.ajax({
        //    url: Utils.getServerApiUrl("Konfigurime", "kontrolloDetajimePerVeprime"),
        //    data: JSON.stringify({ detajimereja: kodi, detajimevjetra: Utils.getUrlVar('detajime'), kodartikulli: Utils.getUrlVar('kodArtikulli') })
        //}).done(SuccededDetajime);
    }
    else {//kur thirret nga Shto_Artikull dhe Modifiko_Artikull
        window.parent.editordetajimi.SetText(kodi);
        if (values[0] != undefined)
            window.parent.editorKategoriDetajimi.SetValue(values[0][1]);
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "kontrolloDetajimePerVeprime"),
            data: JSON.stringify({ detajimereja: kodi, detajimevjetra: Utils.getUrlVar('detajime'), kodartikulli: Utils.getUrlVar('kodArtikulli'), idNdermarrje: hfState.Get('idNdermarrje') })
        }).done(SuccededDetajime);
    }
}

function SuccededDetajime(result) {
    if (result != "") {
        window.parent.editordetajimi.SetText(Utils.getUrlVar('detajime'));
        myMesazh.ShtoMesazhGabimi(result);
    }
    else
        window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    if (e.item.name == 'Modifiko') {
        hfRuaj.val('Modifiko');
        e.processOnServer = false;
        indexModifiko = gvDetajimArtikulli.GetFocusedRowIndex();
        gvDetajimArtikulli.GetRowValues(indexModifiko, 'IdDetajimArtikulli;KodDetajimArtikulli;LlojDetajimArtikulli;PershkrimDetajimArtikulli;KategoriDetajimi;IdNdermarje', eshteDetajimLidhur);
    }
    else {
        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvDetajimArtikulli, hfTeDrejta);
    }

    if (hfState.Get("lupe")) {
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
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj')[0].value = 'Filtra';
    myMenu.aplikoFiltra(s, e, gvDetajimArtikulli, "", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko') {
        TextChangedKategoria(indexModifiko);
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        TextChangedKategoria(indexModifiko);
    }
    $.ajax({       
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
    Utils.hiqLoadingGif();
    Utils.LexoMesazhetNgaGrida(s); 
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green")
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();
    gvDetajimArtikulli.PerformCallback("Fshi");
}
function Click_ButtonCancel(s, e) {
    popFshi.Hide();
}